using System;

namespace Tools.TeamBuild.Tasks
{
    public interface IDateProvider
    {
        DateTime GetTimeStamp();
    }
}
