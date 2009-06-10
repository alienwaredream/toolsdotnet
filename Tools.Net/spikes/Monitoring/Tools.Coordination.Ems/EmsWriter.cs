using System;
using Tools.Coordination.Core;
using Tools.Core.Asserts;
using System.Transactions;
using Tools.Core.Utils;

namespace Tools.Coordination.Ems
{
    public class EmsWriter : IJobProcessor<string>
    {

        EmsWriterQueue queue;

        public EmsWriter(EmsWriterQueue queue)
        {
            this.queue = queue;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (queue != null) queue.Dispose();
        }

        #endregion

        #region IJobProcessor<object> Members

        public void ProcessJobWithEventCallback(string job, Tools.Coordination.WorkItems.WorkItem workItem, JobProcessedDelegate jobProcessedDelegate, SubmittingJobDelegate submittingJobDelegate)
        {

            var transaction = workItem.Transaction as CommittableTransaction;

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

            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Suppress))
                {
                    queue.Open();
                    queue.WriteTextMessage(job, Guid.NewGuid().ToString(), "changebclimit");

                    Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Information, EmsCoordinationMessages.MessageDispatchedByEmsWriter,
    job);
                    scope.Complete();
                }

                Log.Source.TraceData(System.Diagnostics.TraceEventType.Verbose,
                                     0, "Before calling commit on item " + workItem.ContextIdentifier);

                // first commit the queue, second commit the outside transaction.
                // The problem here is that there is no single distributed dtc anyway.
                queue.Commit();
                transaction.Commit();

                Log.Source.TraceData(System.Diagnostics.TraceEventType.Verbose,
                                     0, "Commit called on item " + workItem.ContextIdentifier);
            }
            catch
            {
                
                transaction.Rollback();
                queue.Rollback();


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
                transaction.Dispose();
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


    }
}