using System;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Tools.TeamBuild.Tasks
{
    internal class StatePersistor : IStatePersistor
    {
        private string filePath;
        private string state;
        private IStateProvider stateProvider;

        internal StatePersistor(string filePath) : this(filePath, new FileStateProvider(filePath))
        {
        }
        internal StatePersistor(string filePath, IStateProvider stateProvider)
        {
            this.filePath = filePath;
            this.stateProvider = stateProvider;
        }

        #region IStatePersistor Members


        public void CleanState()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public void WriteState(string content)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            File.WriteAllText(filePath, content);
        }

        public virtual bool ContainsBreak
        {
            get { return File.Exists(filePath); }
        }

        public string BreakDate
        {
            get { if (state == null) { AcquireState(); } return state.Split(';')[0]; }
        }

        public string BreakerDisplayName
        {
            get { if (state == null) { AcquireState(); } return state.Split(';')[1]; }
        }

        public string BreakerEmailAddress
        {
            get { if (state == null) { AcquireState(); } return state.Split(';')[2]; }
        }

        #endregion

        private void AcquireState()
        {
            state = stateProvider.AcquireState();
            Trace.WriteLine("**Acquired keeper state is:" + state);
        }
    }
}
