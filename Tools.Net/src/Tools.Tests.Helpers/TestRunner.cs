using System;
using System.Threading;

namespace Tools.Tests.Helpers
{
    public class TestRunner
    {
        private Action action;
        private ApartmentState apartmentState;
        private Exception exception;

        public TestRunner(Action action, ApartmentState apartmentState)
        {
            this.action = action;
            this.apartmentState = apartmentState;
        }
        public void Execute()
        {
            // Setup a worker thread
            Thread workerThread = new Thread(new ThreadStart(ExecuteInternal));
            // Set apartment
            workerThread.SetApartmentState(apartmentState);

            workerThread.Start();
            // Wait until work on the worker thread is done
            workerThread.Join();
            // Probe for unhandled exception
            if (exception != null)
            {
                // If exception is present, rethrow here on the main thread
                throw exception;
            }
        }
        private void ExecuteInternal()
        {
            // wrap our original action in the try/catch
            try
            {
                action();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // Don't consider race to happen here, subject to think more
                exception = ex;
                // Don't rethrow here as that would kill the test host
            }
        }
    }
}