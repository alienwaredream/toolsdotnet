using System;

namespace Tools.TeamBuild.Tasks
{

    public enum BuildStatus
    {
        None,
        Success,
        Failure,
        NonDeterministic
    }
}
