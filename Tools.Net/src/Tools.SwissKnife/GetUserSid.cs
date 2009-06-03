using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using System.Security.Principal;

namespace Tools.SwissKnife
{
    class GetUserSid
    {
        internal static void Main(string[] args)
        {
            if (args == null || args.Length != 2)
            {
                System.Console.WriteLine(
                    "Get sid for the username. Usage: sid \"domain\\accountName\"");
                System.Console.WriteLine(
                    "Get username for the sid. Usage: un sid");
                System.Console.WriteLine(
                    "Sample: sid \"localPC\\localAccount\"");
                System.Console.WriteLine(
                    "Sample: sid \"domain\\domainAccount\"");
                System.Console.WriteLine(
                    "Sample: un S-1-5-21-589166251-1203392894-1708575535-1118");

                return;

            }


            try
            {
                if (args.Length == 2 && args[0] == "un")
                {
                    SecurityIdentifier sid = new SecurityIdentifier(args[1]);

                    System.Console.WriteLine("User: " + sid.Translate(typeof(System.Security.Principal.NTAccount)).Value);
                    return;

                }


                if (args.Length == 2 && args[0] == "sid")
                {
                    NTAccount account = new NTAccount(args[0]);

                    System.Console.WriteLine("SID: " + account.Translate(typeof(System.Security.Principal.SecurityIdentifier)).Value);
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Exception happened when trying to map account for " +
                    args[1] + ". Exception text" + ex.ToString());
            }

        }
    }
}
