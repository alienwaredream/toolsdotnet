using Tools.Remoting.Host;
using Tools.Wcf.Host;

namespace Tools.RemotingWcf.Host
{
    /// <summary>
    /// A program to host wcf and remoting services.
    /// </summary>
    /// <remarks>Supposed to be called only on the single thread</remarks>
    public class RemotingWcfHostProgram : WcfHostProgram
    {
        protected override void start()
        {
            base.start();

            RemotingRegistrator.Register();
        }
        public override void Stop()
        {
            base.Stop();

            RemotingRegistrator.UnRegister();
        }
    }
}