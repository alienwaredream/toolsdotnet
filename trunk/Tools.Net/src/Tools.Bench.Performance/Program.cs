using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Bench.Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start");
            Console.Read();

            new SealedExampleRunner().Run();
            new SealedExampleRunner().Run();
            new SealedExampleRunner().Run();
            new SealedExampleRunner().Run();

            Console.WriteLine("Press any key to exit");
            Console.Read();
            Console.Read();
        }
    }
}
