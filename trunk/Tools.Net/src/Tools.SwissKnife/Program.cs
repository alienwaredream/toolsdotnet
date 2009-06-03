using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.SwissKnife
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if ((! (args.Length > 0)) || args[0] == "/?") 
                {
                    System.Console.WriteLine("Usage: tools.swissknife command params");
                    System.Console.WriteLine("Commands:");
                    System.Console.WriteLine("SetFileModifiedDate|sfmd");
                    System.Console.WriteLine("ProtectConfigSection|pcs");
                    System.Console.WriteLine("GetUser|gu");
                    System.Console.WriteLine("Use no parameters to get help on a command.");
                    //TODO: (SD) Add usage info
                    return;
                }
                string[] argsProxy = new string[args.Length - 1];
                for (int i = 1; i < args.Length; i++) { argsProxy[i - 1] = args[i]; }

                switch (args[0])
                {
                    case "sfmd" :
                    case "SetFileModifiedDate": SetFileModifiedDate.Main(argsProxy);
                        break;
                    case "pcs" :
                    case "ProtectConfigSection": ProtectConfigSection.Main(argsProxy);
                        break;
                    case "gu" :
                    case "GetUser": GetUserSid.Main(argsProxy);
                        break;
                    default: throw new Exception("Incorrect usage!");
                }
                //System.Console.WriteLine("Press any key to exit.");
                //System.Console.ReadKey();
            }
            catch (Exception ex)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(ex.ToString());
                System.Console.ResetColor();
            }
        }
    }
}
