using System;
using Tools.Coordination.Core;
using Tools.Core.Asserts;
using System.Transactions;
using Tools.Coordination.Ems;
using Tools.Core.Utils;
using Tools.Monitoring.Implementation;
using Tools.Commands.Implementation.IF1.Ibc;
using System.Configuration;
using System.Diagnostics;
using System.Threading;

namespace Tools.Commands.Implementation.IF1.Processors
{
    public class ResponseProcessor : IJobProcessor<string>
    {
        //TODO: (SD) change to interface for testing later on
        private ResponseData dataCommand;
        private IResponseStatusTranslator responseTranslator;

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

        public ResponseProcessor(string responseSPName, IResponseStatusTranslator responseTranslator)
        {
            Init(new ResponseData(responseSPName), responseTranslator);
        }

        public ResponseProcessor(ResponseData dataCommand, IResponseStatusTranslator responseTranslator)
        {
            Init(dataCommand, responseTranslator);
        }

        private void Init(ResponseData dataCommand, IResponseStatusTranslator responseTranslator)
        {
            ErrorTrap.AddAssertion(dataCommand != null, "ResponseData dataCommand can't be null for the" + this.GetType().FullName + ". Please correct the configuration and restart.");

            ErrorTrap.AddAssertion(responseTranslator != null, "IResponseStatusTranslator responseTranslator can't be null. Please correct the configuration for " + this.GetType() + " and restart.");

            ErrorTrap.RaiseTrappedErrors<ConfigurationErrorsException>();

            this.dataCommand = dataCommand;
            this.responseTranslator = responseTranslator;
        }

        #endregion

        #region IJobProcessor<int> Members

        public void ProcessJobWithEventCallback(string job, Tools.Coordination.WorkItems.WorkItem workItem, JobProcessedDelegate jobProcessedDelegate, SubmittingJobDelegate submittingJobDelegate)
        {
            Trace.CorrelationManager.ActivityId = workItem.ContextIdentifier.ContextGuid;

            //var transaction = workItem.Transaction as CommittableTransaction;
            EmsWorkItem ewi = workItem as EmsWorkItem;

            //ErrorTrap.AddRaisableAssertion<InvalidOperationException>(
            //    transaction != null,
            //    "precondition: transaction != null");

            //if (transaction.TransactionInformation.Status == TransactionStatus.Aborted)
            //{
            //    // what happens here is that we have waited for too long, we picked up a job
            //    // but transaction on this has already got aborted, we just do
            //    // nothing then and let transaction to complete abortion process (SD)
            //    return;

            //}

            //        ErrorTrap.AddRaisableAssertion<InvalidOperationException>(
            //transaction.TransactionInformation.Status == TransactionStatus.Active,
            //"transaction.TransactionInformation.Status == TransactionStatus.Active");



            try
            {
                Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Information, EmsCoordinationMessages.MessageDispatchedByStub, String.Format("ReqId: {0}, item: {1}", workItem.ContextIdentifier.ExternalReference, job));

                //using (var scope = new TransactionScope(TransactionScopeOption.Suppress))
                //{


                //if (res == null)
                //{
                //    Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Error, EmsCoordinationMessages.InvalidMessageType, "The message will be removed from the queue: " + job);

                //    if (ewi != null)
                //    {
                //        ewi.Queue.Commit();
                //    }

                //}

                //TODO: The action should be provided here
                responseTranslator.SetResponse(job);
                decimal id = 0;

                if (workItem.ContextIdentifier.ExternalReference == null)
                {
                    Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Error, 15052, String.Format("No correlation id was provided! Message will be removed from the queue! {0}", job));
                    if (ewi != null)
                    {
                        ewi.Queue.Commit();
                    }
                    jobProcessedDelegate(new JobProcessedEventArgs
                        {
                            Retry = false,
                            Success = false,
                            WorkItem = workItem
                        });
                    return;
                }
                if (!Decimal.TryParse(workItem.ContextIdentifier.ExternalReference.ToString(), out id))
                {
                    Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Error, 15052, String.Format("Invalid correlation id of {0} was provided! Message will be removed from the queue! {0}", workItem.ContextIdentifier.ExternalReference, job));
                    if (ewi != null)
                    {
                        ewi.Queue.Commit();
                    }
                    jobProcessedDelegate(new JobProcessedEventArgs
                    {
                        Retry = false,
                        Success = false,
                        WorkItem = workItem
                    });
                    return;
                }

                if (!dataCommand.UpdateResponseToFtPro(Convert.ToDecimal(workItem.ContextIdentifier.ExternalReference), responseTranslator.LogStatus, responseTranslator.CommandStatus, "JMS", DateTime.Now, responseTranslator.Description, String.Empty) && LogErrorForMissingCommands)
                {
                    Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Error, 15051, String.Format("No command with id {0} was present in the commands table. Response was still logged and message will be removed from the queue. {1}", workItem.ContextIdentifier.ExternalReference, job));

                    //    scope.Complete();
                }
                Log.Source.TraceData(System.Diagnostics.TraceEventType.Verbose,
                                     0, "Before calling commit on item " + workItem.ContextIdentifier);
                // ReSharper disable PossibleNullReferenceException - checked with ErrorTrap

                //transaction.Commit();
                if (ewi != null)
                {
                    ewi.Queue.Commit();
                }

                // ReSharper restore PossibleNullReferenceException

                Log.Source.TraceData(System.Diagnostics.TraceEventType.Verbose,
                                     0, "Commit called on item " + workItem.ContextIdentifier);
            }
            catch
            {
                if (ewi != null)
                {
                    Thread.Sleep(60000);
                    ewi.Queue.Rollback();
                }
                // ReSharper disable PossibleNullReferenceException - checked with ErrorTrap
                //transaction.Rollback();
                // ReSharper restore PossibleNullReferenceException

                jobProcessedDelegate(new JobProcessedEventArgs
                {
                    Retry = false,
                    Success = false,
                    WorkItem = workItem
                });

                throw;
            }
            finally
            {
                // ReSharper disable PossibleNullReferenceException - checked with ErrorTrap
                //transaction.Dispose();
                // ReSharper restore PossibleNullReferenceException
            }
            jobProcessedDelegate(new JobProcessedEventArgs
            {
                Retry = false,
                Success = true,
                WorkItem = workItem
            });



        }

        #endregion

        #region IDescriptor Members


        #region IDescriptor Members

        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        #endregion

        #endregion

        #region IDisposable Members

        public void Dispose()
        {

        }

        #endregion
    }
}