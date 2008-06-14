using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Tools.Processes.Core
{
    public class IdleThreadedProcess : ThreadedProcess
    {
        protected override void start()
        {
            Console.WriteLine("IdleProcess started, press any key to complete");
            Console.Read();
            this.OnCompleted(new ProcessExitEventArgs(100));
        }
    }
}
