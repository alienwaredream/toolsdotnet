namespace Tools.Processes.Core
{
    // TODO: This will be moved somewhere else!! (SD)
    /// <summary>
    /// Summary description for ProcessManagerWrapper.
    /// </summary>
    public class ProcessManagerWrapper : Process
    {
        public ProcessManagerWrapper()
        {
        }

        public ProcessManagerWrapper(string name, string description)
            : base(name, description)
        {
        }

        public bool IsEmpty
        {
            get { return ProcessManager.Instance.IsEmpty; }
        }

        public override void Start()
        {
            ProcessManager.Instance.Start();
        }

        public override void Abort()
        {
            ProcessManager.Instance.Abort();
        }

        public override void Stop()
        {
            ProcessManager.Instance.Stop();
        }
    }
}