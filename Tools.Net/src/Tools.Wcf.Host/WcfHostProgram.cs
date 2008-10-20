#region Using directives

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Tools.Core.Asserts;
using Tools.Processes.Core;

#endregion

namespace Tools.Wcf.Host
{
    /// <summary>
    /// A program to host wcf services.
    /// </summary>
    /// <remarks>Supposed to be called only on the single thread</remarks>
    public class WcfHostProgram : ThreadedProcess
    {
        #region Fields

        //private volatile static WcfHostProgram _program;
        //private static object instanceSync = new object();

        private static readonly List<ServiceHost> hosts = new List<ServiceHost>();
        private static readonly ReaderWriterLock hostsListLock = new ReaderWriterLock();
        private readonly List<Type> contracts = new List<Type>();

        #endregion

        #region Public properties

        protected List<Type> Contracts
        {
            get { return contracts; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Supposed to be called only on the single thread
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
            //Contracts.Add(typeof(HostedServicesEnumerator));
            AddContractsFromConfiguration();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public override void Stop()
        {
            try
            {
                hostsListLock.AcquireWriterLock(10000);

                //TODO: (SD) unregister remoting, close hosts
                foreach (ServiceHost sh in hosts)
                {
                    try
                    {
                        sh.Close();
                        //TODO:(SD) Handle lock is not acquired.
                    }
                    catch (Exception ex)
                    {
                        ex.Data.Add("Wcf Service Host", "Exception while trying to close the service host for type " +
                                                        sh.SingletonInstance.GetType().FullName);
                        Log.Source.TraceData(TraceEventType.Error, 0, ex);
                    }
                }
                hosts.Clear();
            }
            finally
            {
                if (hostsListLock.IsWriterLockHeld) hostsListLock.ReleaseLock();
            }
            base.Stop(); // base stop should 
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        protected override void StartInternal()
        {
            //throw new Exception("bla-bla");

            try
            {
                //Debugger.Launch();
                hostsListLock.AcquireWriterLock(10000);

                foreach (Type t in contracts)
                {
                    ServiceHost sh = null;
                    try
                    {
                        sh =
                            new ServiceHost(t);

                        try
                        {
                            sh.Open();

                            //TODO:(SD) Handle lock is not acquired.
                            hosts.Add(sh);
                        }
                        catch (Exception ex)
                        {
                            //TODO:(SD) Handle disposal
                            //TODO: (SD) Put appropriate ExceptionPolicyName
                            ex.Data.Add("Wcf Service Host", "Exception while trying to open a service host for type " +
                                                            t.FullName +
                                                            ", review the configuration and binaries deployment and retry.");
                            Log.Source.TraceData(TraceEventType.Error, 0, ex);
                            throw;
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.Data.Add("Wcf Service Host:" + Guid.NewGuid(),
                                    "Exception while trying to create a service host for type " +
                                    t.FullName + ", review the configuration and binaries deployment and retry.");
                        Log.Source.TraceData(TraceEventType.Error, 0, ex);
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Source.TraceData(TraceEventType.Error, 0, ex);
                throw;
            }
            finally
            {
                if (hostsListLock.IsWriterLockHeld) hostsListLock.ReleaseLock();
            }
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

                foreach (ServiceHost sh in hosts)
                {
                    try
                    {
                        sb.Append("******").Append(Environment.NewLine);
                        sb.Append(sh.Description.ServiceType.FullName).Append(Environment.NewLine);
                    }
                    catch (Exception ex)
                    {
                        ex.Data.Add("Wcf Service Host", "Exception while querying the service host for type " +
                                                        sh.SingletonInstance.GetType().FullName);
                        Log.Source.TraceData(TraceEventType.Error, 0, ex);
                    }
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Wcf Service Host", "Exception while querying to query the status of the WcfServiceHost. ");
                Log.Source.TraceData(TraceEventType.Error, 0, ex);
            }
            finally
            {
                if (hostsListLock.IsReaderLockHeld) hostsListLock.ReleaseLock();
            }
            return "Failed to enumerate registered services";
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Adds the contracts from configuration.
        /// </summary>
        private void AddContractsFromConfiguration()
        {
            var servicesConfig =
                ConfigurationManager.GetSection(
                    "Tools.ServiceModelHost") as NameValueCollection;

            ErrorTrap.AddRaisableAssertion<ConfigurationErrorsException>
                (servicesConfig != null,
                 "Section Tools.ServiceModelHost is not present in the configuration file or is misconfigured!" +
                 " Setup the section properly!");

            foreach (string serviceName in servicesConfig.Keys)
            {
                Type t = Type.GetType(servicesConfig[serviceName], true); //Throw if any error (SD)
                if (t == null)
                {
                    throw new ConfigurationErrorsException(String.Format(
                                                               "Can't create a type for the {0} service name, " +
                                                               "type activation data is: {1}", serviceName,
                                                               servicesConfig[serviceName]));
                }
                Contracts.Add(t);
            }
        }

        #endregion
    }
}