using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading;

namespace Tools.Remoting.Host
{
    /// <summary>
    /// Serves for registering remoting services
    /// </summary>
    public static class RemotingRegistrator
    {
        #region Fields

        static readonly ReaderWriterLock hostsListLock = new ReaderWriterLock();

        #endregion

        public static void UnRegister()
        {
            try
            {
                hostsListLock.AcquireWriterLock(10000);

                IChannel[] chn = ChannelServices.RegisteredChannels;
                foreach (IChannel ch in chn)
                {
                    ChannelServices.UnregisterChannel(ch);
                }

            }
            finally
            {
                if (hostsListLock.IsWriterLockHeld) hostsListLock.ReleaseLock();
            }
        }
        public static void Register()
        {
            // configure remoting from the file
            RemotingConfiguration.Configure(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile, false);

            Log.Source.TraceInformation("Hosting report:" + Environment.NewLine + QueryForServices());
        }

        /// <summary>
        /// Queries for services.
        /// </summary>
        /// <returns></returns>
        public static string QueryForServices()
        {
            try
            {
                hostsListLock.AcquireReaderLock(10000);

                var sb = new StringBuilder();
                sb.Append("Enumerating registered services:" + Environment.NewLine);

                foreach
                    (
                    WellKnownServiceTypeEntry typeEntry
                        in RemotingConfiguration.GetRegisteredWellKnownServiceTypes()
                    )
                {
                    sb.Append(String.Format(CultureInfo.InvariantCulture,
                        "Registered service for type: [{0}] with uri [{1}]", typeEntry.ObjectType, typeEntry.ObjectUri));
                }

                foreach (IChannel channel in ChannelServices.RegisteredChannels)
                {
                    sb.Append(String.Format(CultureInfo.InvariantCulture,
                        "Registered channel [{0}]", channel.ChannelName));
                }

                return sb.ToString();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Remoting Service Host", "Exception while querying to query the status of the RemotingServiceHost. ");
                Log.Source.TraceData(TraceEventType.Error, 0, ex);
            }
            finally
            {
                if (hostsListLock.IsReaderLockHeld) hostsListLock.ReleaseLock();
            }
            return "Failed to enumerate registered services";
        }
    }

}

