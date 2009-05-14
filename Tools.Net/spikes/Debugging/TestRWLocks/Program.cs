using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace TestRWLocks
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage TestRWLocks rwlock \r\n TestRWLocks runforever");
                Environment.Exit(100);
            }
            if (args[0].ToLower() == "rwlock")
                LockOnReaderWriterLock();

            if (args[0].ToLower() == "runforever")
                RunForever();

        }

        private static void RunForever()
        {
            int i = 0;

            while (true)
            {

                i++;
                DummyMethod(i);
                Thread.Sleep(1000);


            }
        }

        private static void DummyMethod(int i)
        {
            int z = 100;
            z++;
            Trace.WriteLine("Iteration " + i);
        }

        private static void LockOnReaderWriterLock()
        {
            Console.WriteLine("About to lock on the ReaderWriterLock. Debug after seeing \"Signaled to acquire the reader lock.\"");
            ReaderWriterLock rwLock = new ReaderWriterLock();

            ManualResetEvent rEvent = new ManualResetEvent(false);
            ManualResetEvent pEvent = new ManualResetEvent(false);

            ThreadPool.QueueUserWorkItem((object state) =>
            {
                rwLock.AcquireWriterLock(-1);
                Console.WriteLine("Writer lock acquired!");
                rEvent.Set();
                pEvent.WaitOne();
            });

            rEvent.WaitOne();

            Console.WriteLine("Signaled to acquire the reader lock.");

            rwLock.AcquireReaderLock(-1);

            Console.WriteLine("Reader lock acquired");

            pEvent.Set();

            Console.WriteLine("About to end the program. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
