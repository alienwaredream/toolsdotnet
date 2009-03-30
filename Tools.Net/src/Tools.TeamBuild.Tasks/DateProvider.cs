using System;

namespace Tools.TeamBuild.Tasks
{

    internal class DateProvider : IDateProvider
    {

        #region IDateProvider Members

        public DateTime GetTimeStamp()
        {
            return DateTime.Now;
        }

        #endregion
    }

}
