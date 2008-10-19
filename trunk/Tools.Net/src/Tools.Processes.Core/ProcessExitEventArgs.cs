using System;

namespace Tools.Processes.Core
{
    [Serializable]
    public class ProcessExitEventArgs : EventArgs
    {
        public object CompletionState { get; set; }
    }
}