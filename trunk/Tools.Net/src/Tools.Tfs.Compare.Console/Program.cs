using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.TeamFoundation.VersionControl.Client;
using Microsoft.TeamFoundation.Client;
using System.IO;
using System.Threading;
using System.Net;

namespace Tools.Tfs.Compare.Console
{
    class Program
    {
        //string tfsUrl = "http://bbtfs01:8080";

        static void Main(string[] args)
        {
            string tfsUrl = "http://bbtfs01:8080";

            if (args.Length > 0 && !String.IsNullOrEmpty(args[0]))
            {
                tfsUrl = args[0];
            }


            TeamFoundationServer server = new TeamFoundationServer(tfsUrl,
                                                                       CredentialCache.DefaultNetworkCredentials);

            VersionControlServer vcs = (VersionControlServer)server.GetService(typeof(VersionControlServer));

            string serverPath = @"$/FORIS/Production/4.3 A1.1 Prd";
            string localPath = @"c:\dev2\";

            if (!Directory.Exists(localPath))
            {
                Directory.CreateDirectory(localPath);
            }

            List<WorkingFolder> wFolders = new List<WorkingFolder> { new WorkingFolder(serverPath, localPath) };

            //vcs.CreateWorkspace("Tools.Net TfsCompare", Thread.CurrentPrincipal.Identity.ToString(),
                //"Created by Tools.Net Compare", wFolders, Environment.MachineName);
            try
            {

                //int currentOldest = vcs.GetLatestChangesetId();
                //int notBefore = 237311;

                //if (notBefore >= currentOldest)
                //{
                //   // return notBefore;
                //}

               // LabelVersionSpec lSpecFrom = new LabelVersionSpec();

                    bool foundChanges = false;
                DateVersionSpec dSpecFrom = new DateVersionSpec(new DateTime(2009, 3, 12));
                DateVersionSpec dSpecTo = new DateVersionSpec(DateTime.Now);

                    foreach (Changeset latest in vcs.QueryHistory(serverPath,
                        VersionSpec.Latest,
                        0,
                        RecursionType.Full,
                        null,
                        dSpecFrom,
                        dSpecTo,
                        1000,
                        true,
                        true))
                    {
                        // we want to use the oldest starting point - if result is newer then use the older
                        System.Console.WriteLine("Changeset found:" + latest.ChangesetId + " at date: " + latest.CreationDate);

                        foreach (Change c in latest.Changes)
                        {
                            System.Console.WriteLine("\t" + c.Item.ServerItem);
                        }

                    }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.ToString());
            }

            System.Console.ReadKey();

        }



    }
}
