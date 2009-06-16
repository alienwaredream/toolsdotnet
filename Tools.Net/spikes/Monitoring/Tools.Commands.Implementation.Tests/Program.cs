using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tools.Coordination.Ems;
using Spring.Context.Support;

namespace Tools.Commands.Implementation.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                #region Logging setup
                //Tools.Commands.Implementation.Log.Source.Listeners.Add(new ConsoleTraceListener());
                //Tools.Commands.Implementation.Log.Source.Switch.Level = SourceLevels.All;
                #endregion

                #region Test response data

                //ResponseData rd = new ResponseData("prov_test_standa.updateresponsetoftpro");

                //Console.WriteLine("Result - updated " + rd.UpdateResponseToFtPro(
                //    1,
                //    "E",
                //    "error",
                //    "tested",
                //    DateTime.Now,
                //    "There is an error",
                //    "300"
                //    ).ToString());

                #endregion

                CommandsProcessor cd = ContextRegistry.GetContext().GetObject("CommandsProcessor") as CommandsProcessor;

                cd.ExecuteNextCommand();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine(ex.ToString());

            }

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
    }
}
