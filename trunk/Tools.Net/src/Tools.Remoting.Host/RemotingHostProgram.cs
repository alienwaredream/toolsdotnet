#region Using directives

using Tools.Processes.Core;

#endregion

namespace Tools.Remoting.Host
{
    /// <summary>
    /// A program to host wcf services.
    /// </summary>
    /// <remarks>Supposed to be called only on the single thread</remarks>
    public class RemotingHostProgram : ThreadedProcess
    {
        /// <summary>
        /// Stops this instance.
        /// </summary>
        public override void Stop()
        {
            RemotingRegistrator.UnRegister();
            base.Stop();
        }

        /// <summary>
        /// Starts this instance.
        /// </summary>
        protected override void StartInternal()
        {
            RemotingRegistrator.Register();
        }
    }
}