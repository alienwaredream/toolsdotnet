using System;

namespace Tools.TeamBuild.Tasks
{
    class Program
    {
        static void Main(string[] args)
        {
            //if (args == null || args.Length)
            //{
            //    //Console.WriteLine(String.Format(CultureInfo.InvariantCulture(
            //    //    "Usage: tfutil " + 
            //}
            ResolveUser resUser = new ResolveUser(args[0]);

            resUser.WindowsAccountName = args[1];

            resUser.Execute();

            Console.WriteLine(String.Format("{0}-{1}", resUser.MailAddress, resUser.DisplayName));
            Console.ReadKey();
        }
    }
}
