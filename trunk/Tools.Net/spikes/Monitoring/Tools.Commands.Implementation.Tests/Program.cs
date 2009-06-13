using System;
using Tools.Monitoring.Implementation;

namespace Tools.Commands.Implementation.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
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

                CommandsReader cd = new CommandsReader("prov_cmd_send.GetCommandRecordsToProcess2");

                cd.ExecuteNextCommandBatch(
                    String.Empty,
                    String.Empty,
                    2,
                    2,
                    "test", "dd");

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
