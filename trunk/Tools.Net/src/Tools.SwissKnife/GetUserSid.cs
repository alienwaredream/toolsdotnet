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
            if (args.Length < 1)
            {
                System.Console.WriteLine(
                    "Usage: \"domain\\accountName\"");
                System.Console.WriteLine(
                    "Sample: \"localPC\\localAccount\"");
                System.Console.WriteLine(
                    "Sample: \"domain\\domainAccount\"");
                return;

            }

            NTAccount account = new NTAccount(args[0]);

            System.Console.WriteLine("SID: " + account.Translate(typeof(System.Security.Principal.SecurityIdentifier)).Value);
        }
    }
}
