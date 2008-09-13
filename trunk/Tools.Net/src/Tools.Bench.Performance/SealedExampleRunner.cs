using System.Diagnostics;
using System;

namespace Tools.Bench.Performance
{
    class SealedExampleRunner
    {
        private const int NumberOfIterations = 100000000;

        internal void Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            
            NonSealedClassSample nonSealedInstance = new NonSealedClassSample();
            SealedClassSample sealedInstance = new SealedClassSample();

            stopwatch.Start();

            for (int i = 0; i < NumberOfIterations; i++)
            {
                sealedInstance.DoWork();
            }
            stopwatch.Stop();

            Console.WriteLine(String.Format("Executed sealed for {0} iterations: {1}ms", NumberOfIterations,
                stopwatch.ElapsedMilliseconds));

            

            stopwatch.Reset();
            stopwatch.Start();

            for (int i = 0; i < NumberOfIterations; i++)
            {
                nonSealedInstance.DoWork();
            }
            stopwatch.Stop();

            Console.WriteLine(String.Format("Executed non sealed for {0} iterations: {1}ms", NumberOfIterations,
                stopwatch.ElapsedMilliseconds));
        }
    }
}
