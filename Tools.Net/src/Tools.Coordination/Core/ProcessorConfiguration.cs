using System;
using Tools.Coordination.WorkItems;
using Tools.Core;

namespace Tools.Coordination.Core
{
    [Serializable]
    public class ProcessorConfiguration : Descriptor, IEnabled
    {
        public SubmissionPriority Priority { get; set; }
        public uint Count { get; set; }


        #region IEnabled Members

        private bool enabled = true;

        public bool Enabled
        {
            get { return enabled;}
            set
            {
                if (value == enabled) return;

                enabled = value;
                if (EnabledChanged != null) EnabledChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler EnabledChanged;

        #endregion
    }
}
