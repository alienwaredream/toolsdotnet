namespace Tools.Processes.Core
{
    // TODO: This will be moved somewhere else!! (SD)
    /// <summary>
    /// Summary description for ProcessManagementWrapper.
    /// </summary>
    public class ProcessManager : Process
    {
        private static readonly object syncRoot = new object();
        private static ProcessManager _instance;
        // TODO: resolve possible concurrency issues here while initializing the chain (SD)
        private readonly IProcessCollection _processes;

        protected ProcessManager()
        {
            _processes = new IProcessCollection();
        }

        protected ProcessManager(string name, string description)
            : base(name, description)
        {
        }

        public static ProcessManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                        {
                            _instance = new ProcessManager();
                        }
                    }
                }
                return _instance;
            }
        }


        public bool IsEmpty
        {
            get
            {
                // TODO: think if there is a need to lock a collection here,
                // if count is thread safe or not.
                return _processes.Count == 0;
            }
        }

        public void AddProcess(IProcess process)
        {
            _processes.Add(process);

            if (ExecutionState == ProcessExecutionState.Running)
            {
                // TODO: handle exceptions and atomicity
                process.Start();
            }
        }

        public void RemoveProcess(IProcess process)
        {
            if (process == null) return; // TODO: think if this is appropriate (SD)

            if (process.ExecutionState != ProcessExecutionState.Finished
                || process.ExecutionState != ProcessExecutionState.Stopped)
            {
                // TODO: handle exceptions
                process.Abort();
            }

            _processes.Remove(process);
        }

        public void RemoveProcess(string processName)
        {
            IProcess process = _processes.GetEntry(processName);
            // TODO: think about strategy for this
            if (process == null) return;

            if (process.ExecutionState != ProcessExecutionState.Finished
                || process.ExecutionState != ProcessExecutionState.Stopped)
            {
                // TODO: handle exceptions
                process.Abort();
            }

            _processes.Remove(process);
        }

        public override void Start()
        {
            // TODO: resolve synchronization issues
            // TODO: resolve whole-part atomicity for Start


            lock (_processes)
            {
                foreach (IProcess process in _processes)
                {
                    // TODO: Handle the non-ability to start
                    process.Start();
                }
            }

            SetExecutionState(ProcessExecutionState.Running);
        }

        public override void Abort()
        {
            // TODO: resolve synchronization issues
            // TODO: resolve whole-part atomicity for Abort
            base.Abort();

            lock (_processes)
            {
                foreach (IProcess process in _processes)
                {
                    // TODO: Handle different abortion scenarios and exceptions
                    process.Abort();
                }
            }
        }

        public override void Stop()
        {
            // TODO: resolve synchronization issues
            // TODO: resolve whole-part atomicity for Abort
            base.Stop();

            lock (_processes)
            {
                foreach (IProcess process in _processes)
                {
                    // TODO: Handle different abortion scenarios and exceptions
                    process.Stop();
                }
            }
        }
    }
}