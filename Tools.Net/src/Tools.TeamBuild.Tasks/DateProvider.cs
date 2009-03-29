using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
