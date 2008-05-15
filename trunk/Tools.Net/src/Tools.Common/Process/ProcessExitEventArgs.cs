using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Common.Process
{
    [Serializable()]
    public class ProcessExitEventArgs : System.EventArgs
    {
        private object completionState;

        public string CompletionStateString
        {
            get
            {
                return ((completionState == null) ? null : completionState.ToString());
            }
        }

        public ProcessExitEventArgs(object completionState)
        {
            this.completionState = completionState;
        }
    }
}
