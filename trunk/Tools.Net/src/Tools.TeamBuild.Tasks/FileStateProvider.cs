using System;
using System.IO;

namespace Tools.TeamBuild.Tasks
{
    internal class FileStateProvider : IStateProvider
    {
        private string filePath;

        internal FileStateProvider(string filePath)
        {
            this.filePath = filePath;
        }

        #region IStateProvider Members

        public string AcquireState()
        {
            if (!File.Exists(filePath))
                return String.Empty;

            return File.ReadAllText(filePath);
        }

        #endregion
    }
}
