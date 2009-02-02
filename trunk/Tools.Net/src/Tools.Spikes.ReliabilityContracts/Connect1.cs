
namespace Tools.Spikes.ReliabilityContracts
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.ConstrainedExecution;
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>http://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=97407</remarks>
    class Program
    {
        public void StackOverFlowTester()
        {
            try
            {
                _tryCode = new RuntimeHelpers.TryCode(this.GenerateStackOverFlow);
                _cleanupCode = new RuntimeHelpers.CleanupCode(this.CleanupCode);
            }
            finally
            {
                RuntimeHelpers.ExecuteCodeWithGuaranteedCleanup(Program._tryCode, Program._cleanupCode, 1);
            }
        }
        internal static RuntimeHelpers.TryCode _tryCode;
        internal static RuntimeHelpers.CleanupCode _cleanupCode;
        internal static int StackCount = 0;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public void GenerateStackOverFlow(object userdata)
        {
            int curStackFrame = (int)userdata + 1;
            StackCount = curStackFrame;
            /*
            if (StackCount == 1000)
            throw new InvalidCastException("haha");
            */
            GenerateStackOverFlow(curStackFrame);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        [ReliabilityContract(Consistency.MayCorruptInstance, Cer.Success)]
        [PrePrepareMethod]
        public void CleanupCode(object userdata, bool exceptionThrown)
        {
            Console.WriteLine("Recovered from Stack Overflow with depth: " + StackCount);
        }

        static void Main(string[] args)
        {
            RuntimeHelpers.PrepareConstrainedRegions();
            try
            {
                Program p = new Program();
                p.StackOverFlowTester();
            }
            finally
            {
                Console.WriteLine("Hello we terminate now.");
            }
        }
    }
}
