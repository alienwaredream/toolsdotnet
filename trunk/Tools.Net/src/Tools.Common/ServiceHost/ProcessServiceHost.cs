using System;
using System.Collections.Generic;
using System.Text;
using Tools.Common.Process;
using Tools.Common.Exceptions;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics;
using Tools.Common.Logging;

namespace Tools.Common.ServiceHost
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="ProcessType">The type of the process type.</typeparam>
    public class ProcessServiceHost<ProcessType> : ServiceHost 
        where ProcessType : IProcess, new()
    {
        //TODO:(SD) Refactor to use an interface
        protected IProcess process;

        protected IProcess Process
        {
            get { return process; }
        }

        /// <summary>
        /// Program entry point.
        /// </summary>
        /// <typeparam name="ServiceHostType">The type of the ervice host type.</typeparam>
        /// <param name="args">The args.</param>
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Justification = "By design, there can't be a parameter of type ServiceHostType here.")]
        protected new static void EntryPoint<ServiceHostType>(string[] args)
            where ServiceHostType : ProcessServiceHost<ProcessType>, new()
            
        {
            //The bellow is only for console mode!
            if ((args.Length > 0) && (args[0].ToLower().Contains("console")))
            {
                ProcessServiceHost<ProcessType> sh = new ServiceHostType();
                sh.Mode = HostMode.WindowsConsole;
                sh.process = new ProcessType();
                sh.process.Completed += new EventHandler<ProcessExitEventArgs>(sh.process_Ended);
                sh.process.Terminated += new EventHandler<ProcessExitEventArgs>(sh.process_Ended);
                Console.CancelKeyPress += sh.NeedStop;
                sh.process.Initialize();
                sh.process.Start();

                //AppDomain.CurrentDomain.ProcessExit +=new EventHandler(CurrentDomain_ProcessExit);
                //AppDomain.CurrentDomain.
                //TODO:(SD) Add a timeout option
                sh.process.CompletedHandle.WaitOne();


                Environment.Exit(sh.exitCode);
            }
            else
            {
                ServiceHost.EntryPoint<ServiceHostType>(args);
            }

        }
        protected override void OnStart(string[] args)
        {
            try
            {
                base.OnStart(args);
                process = new ProcessType();
                process.Completed += new EventHandler<ProcessExitEventArgs>(process_Ended);
                process.Terminated += new EventHandler<ProcessExitEventArgs>(process_Ended);
                process.Initialize();
                process.Start();
            }
            catch (Exception ex)
            {
                Log.Source.TraceData(TraceEventType.Error, 3001,
                    ex.ToString());
                throw;
            }
            finally
            {

            }
        }
        protected override void OnStop()
        {
            try
            {
                process.Stop(); //TODO: (SD) provide for abortion
                process.Completed -= new EventHandler<ProcessExitEventArgs>(process_Ended);
                process.Terminated -= new EventHandler<ProcessExitEventArgs>(process_Ended);

            }
            catch (Exception ex)
            {
                Log.Source.TraceData(TraceEventType.Error, 3002,
                    "Excception while trying to stop process service host: " + ex.ToString());
                //throw ex;
            }
            finally
            {

            }
        }
        #region Handlers

        private void NeedStop(object sender, ConsoleCancelEventArgs e)
        {

            Console.WriteLine("Console terminated signal send. Stopping service...");
            Stop();
            Console.CancelKeyPress -= this.NeedStop;
        }

        void process_Ended(object sender, ProcessExitEventArgs e)
        {
            if (!int.TryParse(e.CompletionStateString, out exitCode))
            {
                //TODO: (SD) Log exception e.CompletionStateString
                exitCode = (int)ProcessExitCode.Inconclusive;
            }
        }

        #endregion
    }
}
