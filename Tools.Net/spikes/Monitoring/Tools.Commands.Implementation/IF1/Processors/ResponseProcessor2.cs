using System;
using Tools.Coordination.Core;
using Tools.Core.Asserts;
using System.Transactions;
using Tools.Coordination.Ems;
using Tools.Core.Utils;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using Tools.Coordination.Batch;
using TIBCO.EMS;
using Tools.Processes.Core;

namespace Tools.Commands.Implementation.IF1.Processors
{
    public class ResponseProcessor : ScheduleTaskProcessor
    {
        //TODO: (SD) change to interface for testing later on
        private ResponseDataProvider dataCommand;
        private IResponseStatusTranslator responseTranslator;
        private EmsReaderQueue queue;

        private bool logErrorForMissingCommands = true;
        /// <summary>
        /// If true, then error is logged when there is no command with such reqId to update.
        /// </summary>
        public bool LogErrorForMissingCommands
        {
            get { return logErrorForMissingCommands; }
            set { logErrorForMissingCommands = value; }
        }

        #region Constructors

        public ResponseProcessor(string responseSPName, IResponseStatusTranslator responseTranslator, EmsReaderQueue queue)
        {
            Init(new ResponseDataProvider(responseSPName), responseTranslator, queue);
        }

        public ResponseProcessor(ResponseDataProvider dataCommand, IResponseStatusTranslator responseTranslator, EmsReaderQueue queue)
        {
            Init(dataCommand, responseTranslator, queue);
        }

        private void Init(ResponseDataProvider dataCommand, IResponseStatusTranslator responseTranslator, EmsReaderQueue queue)
        {
            ErrorTrap.AddAssertion(dataCommand != null, "ResponseData dataCommand can't be null for the" + this.GetType().FullName + ". Please correct the configuration and restart.");

            ErrorTrap.AddAssertion(responseTranslator != null, "IResponseStatusTranslator responseTranslator can't be null. Please correct the configuration for " + this.GetType() + " and restart.");

            ErrorTrap.RaiseTrappedErrors<ConfigurationErrorsException>();

            this.dataCommand = dataCommand;
            this.responseTranslator = responseTranslator;
            this.queue = queue;
        }

        #endregion

        #region ScheduleTaskProcessor Members

        public override void Stop()
        {
            // Close implementation never throws
            queue.Close();

            this.SetExecutionState(ProcessExecutionState.StopRequested);

            base.Stop();

            this.SetExecutionState(ProcessExecutionState.Stopped);
        }

        protected override void ExecuteSheduleTask()
        {
            try
            {
                Trace.CorrelationManager.ActivityId = Guid.NewGuid();
                Log.TraceData(Log.Source, TraceEventType.Verbose, 10000, String.Format("Listening [{0}:{1}]",
                    queue.ServerConfig.Url, queue.QueueConfig.Name));

                base.ExecuteSheduleTask();

                try
                {
                    queue.Open();
                }
                catch (Exception ex)
                {
                    if (!queue.RecoverFromConnectionError(ex))
                    {
                        Stop();
                    }
                    throw;
                }

                //var transaction = new CommittableTransaction();
                Message msgTest = queue.ReadNext();

                TextMessage message = msgTest as TextMessage;

                if (msgTest == null)
                {
                    Log.TraceData(Log.Source, TraceEventType.Verbose, 0, String.Format("Null message on timeout on queue [{0}:{1}]", queue.ServerConfig.Url, queue.QueueConfig.Name));
                    queue.Commit();
                    return;
                }

                if (message == null && msgTest != null)
                {
                    Log.TraceData(Log.Source, TraceEventType.Error, 10001, String.Format("Incorrect message [CID:{0}|MID:{1}] of type {2}. Only expected message type is a TextMessage", msgTest.CorrelationID, msgTest.MessageID, msgTest.GetType().FullName));
                    queue.Commit();
                    return;
                }
                //Trace.CorrelationManager.ActivityId = Guid.NewGuid();
                Log.TraceData(Log.Source, TraceEventType.Start, 0, String.Format("{0} [{1}:{2}, {3}]", message.CorrelationID, queue.ServerConfig.Url, queue.QueueConfig.Name, message.MessageID));

                string job = message.Text;

                bool resubmitFlag = message.GetBooleanProperty("doRetry");
                string errorType = message.GetStringProperty("errorType");

                Log.TraceData(Log.Source, TraceEventType.Verbose, 15020, String.Format("Message: \r\n{0}\r\n Resubmit flag:{1}\r\nErrorType: {2}", job, resubmitFlag, errorType));

                responseTranslator.SetResponse(job, resubmitFlag, errorType);

                if (ErrorTrap.HasErrors)
                {
                    Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Error, 15052, String.Format("Invalid message! Message will be removed from the queue!\r\n {1}", message.CorrelationID, ErrorTrap.Text));
                    ErrorTrap.Reset();

                    queue.Commit();
                    return;
                }

                decimal id = 0;

                if (!Decimal.TryParse(message.CorrelationID, out id))
                {
                    Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Error, 15052, String.Format("Invalid correlation id of {0} was provided! Message will be removed from the queue!\r\n {1}", message.CorrelationID, job));

                    queue.Commit();
                    return;
                }

                if (!dataCommand.UpdateResponseToFtPro(id, responseTranslator.LogStatus, responseTranslator.CommandStatus, "JMS", DateTime.Now, responseTranslator.Description, String.Empty) && LogErrorForMissingCommands)
                {
                    Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Error, 15051, String.Format("No command with id {0} was present in the commands table. Response was still logged and message will be removed from the queue. {1}", message.CorrelationID, job));

                }

                queue.Commit();

                Log.Source.TraceData(System.Diagnostics.TraceEventType.Verbose,
                                     0, String.Format("Message {0} - read committed [{1}:{2}, {3}]", message.CorrelationID, queue.ServerConfig.Url, queue.QueueConfig.Name, message.MessageID));

                this.Schedule.SetForImmidiateRun();
            }
            catch (Exception ex)
            {
                try
                {
                    if (queue.IsInitialized) queue.Rollback();
                }
                catch (Exception ex2)
                {
                    Log.Source.TraceData(System.Diagnostics.TraceEventType.Error,
10060, String.Format("Exception while rolling back the EMS session: {0}", ex2));
                }

                Log.Source.TraceData(System.Diagnostics.TraceEventType.Error,
     10061, String.Format("Exception while executing the iteration. \r\n{0}", ex));

                // Cleanup will never throw.
                if (ex is EMSException) queue.Close();
                // If there is an exception wait for some time.
                Thread.Sleep(60000);
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {

        }

        #endregion
    }
}