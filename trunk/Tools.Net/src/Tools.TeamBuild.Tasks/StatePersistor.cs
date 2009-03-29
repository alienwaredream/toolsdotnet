using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Tools.TeamBuild.Tasks
{
    internal class StatePersistor : IStatePersistor
    {
        private string filePath;
        private string state;

        internal StatePersistor(string filePath)
        {
            this.filePath = filePath;
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
            state = File.ReadAllText(filePath);
        }
    }
}
