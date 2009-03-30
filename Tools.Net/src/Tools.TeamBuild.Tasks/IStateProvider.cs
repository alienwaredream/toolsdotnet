using System;

namespace Tools.TeamBuild.Tasks
{
    public interface IStateProvider
    {
        string AcquireState();
    }
}
