using System;
using Tools.Coordination.Core;
using Tools.Core.Asserts;
using System.Transactions;
using Tools.Coordination.Ems;
using Tools.Core.Utils;
using Tools.Monitoring.Implementation;
using Tools.Commands.Implementation.IF1.Ibc;

namespace Tools.Commands.Implementation.IF1.Processors
{
    public class ItemProcessorStub : IJobProcessor<string>
    {

        #region IJobProcessor<int> Members

        public void ProcessJobWithEventCallback(string job, Tools.Coordination.WorkItems.WorkItem workItem, JobProcessedDelegate jobProcessedDelegate, SubmittingJobDelegate submittingJobDelegate)
        {

            var transaction = workItem.Transaction as CommittableTransaction;
            EmsWorkItem ewi = workItem as EmsWorkItem;

            ErrorTrap.AddRaisableAssertion<InvalidOperationException>(
                transaction != null,
                "precondition: transaction != null");

            if (transaction.TransactionInformation.Status == TransactionStatus.Aborted)
            {
                // what happens here is that we have waited for too long, we picked up a job
                // but transaction on this has already got aborted, we just do
                // nothing then and let transaction to complete abortion process (SD)
                return;

            }

            ErrorTrap.AddRaisableAssertion<InvalidOperationException>(
    transaction.TransactionInformation.Status == TransactionStatus.Active,
    "transaction.TransactionInformation.Status == TransactionStatus.Active");

            Tools.Commands.Implementation.IF1.Ibc.res res = null;

            try
            {
                Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Information, EmsCoordinationMessages.MessageDispatchedByStub, String.Format("ReqId: {0}, item: {1}", workItem.ContextIdentifier.ExternalReference, job));

                using (var scope = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    res = SerializationUtility.DeserializeFromString(job, typeof(Tools.Commands.Implementation.IF1.Ibc.res)) as Tools.Commands.Implementation.IF1.Ibc.res;

                    if (res == null)
                    {
                        Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Error, EmsCoordinationMessages.InvalidMessageType, "The message will be removed from the queue: " + job);

                        if (ewi != null)
                        {
                            ewi.Queue.Commit();
                        }
                        
                    }

                    //TODO: The action should be provided here

                    string logStatus = (res.code == "ok") ? "P" : "E";
                    string masterStatus = (res.code == "ok") ? "DONE" : "FAILED";

                    if (!new ResponseData().UpdateResponseToFtPro(Convert.ToInt32(workItem.ContextIdentifier.ExternalReference), logStatus, masterStatus, "JMS", DateTime.Now, res.desc, String.Empty))
                    {
                        Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Error, 15051, String.Format("No command with id {0} was present in the commands table. Response was still logged and message will be removed from the queue. {1}", workItem.ContextIdentifier.ExternalReference, job));
                    }

                    if (ewi != null)
                    {
                        ewi.Queue.Commit();
                    }



                    scope.Complete();
                }
                Log.Source.TraceData(System.Diagnostics.TraceEventType.Verbose,
                                     0, "Before calling commit on item " + workItem.ContextIdentifier);
                // ReSharper disable PossibleNullReferenceException - checked with ErrorTrap

                transaction.Commit();


                // ReSharper restore PossibleNullReferenceException

                Log.Source.TraceData(System.Diagnostics.TraceEventType.Verbose,
                                     0, "Commit called on item " + workItem.ContextIdentifier);
            }
            catch
            {
                if (ewi != null)
                {
                    ewi.Queue.Rollback();
                }
                // ReSharper disable PossibleNullReferenceException - checked with ErrorTrap
                transaction.Rollback();
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
                transaction.Dispose();
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