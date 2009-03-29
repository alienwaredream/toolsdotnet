using System;

using Microsoft.TeamFoundation.Client;
using System.Net;
using Microsoft.TeamFoundation.Server;

namespace Tools.TeamBuild.Tasks
{

    public class ResolveUser : Microsoft.Build.Utilities.Task
    {
        string tfsUrl = "http://bbtfs01:8080";
        /// <summary>
        /// Windows Account Name.
        /// </summary>
        [Microsoft.Build.Framework.Required()]
        public string WindowsAccountName { get; set; }
        /// <summary>
        /// TFS url, better in the format of http://TFSHOSTNAME:port
        /// </summary>
        public string TfsUrl { get { return tfsUrl; } set { tfsUrl = value; } }

        [Microsoft.Build.Framework.Output()]
        public string MailAddress { get; set; }
        [Microsoft.Build.Framework.Output()]
        public string DisplayName { get; set; }

        public ResolveUser() { }

        public ResolveUser(string tfsUrl)
        {
            this.tfsUrl = tfsUrl;
        }

        public override bool Execute()
        {
            try
            {
                TeamFoundationServer server = new TeamFoundationServer(tfsUrl,
                                                                       CredentialCache.DefaultNetworkCredentials);

                IGroupSecurityService gss = (IGroupSecurityService)server.GetService(typeof(IGroupSecurityService));

                Identity ident = gss.ReadIdentity(SearchFactor.AccountName, WindowsAccountName, QueryMembership.None);

                MailAddress = ident.MailAddress;
                DisplayName = ident.DisplayName;

                return true;

            }
            catch (Exception ex)
            {
                Log.LogErrorFromException(ex, true);
                return false;
            }
        }

    }
}
