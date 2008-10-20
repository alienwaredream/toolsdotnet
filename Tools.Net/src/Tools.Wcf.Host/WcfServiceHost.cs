using Tools.Processes.Host;

namespace Tools.Wcf.Host
{
    public class WcfServiceHost :
        ProcessServiceHost<WcfHostProgram>
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            EntryPoint<WcfServiceHost>(args);
        }
    }
}