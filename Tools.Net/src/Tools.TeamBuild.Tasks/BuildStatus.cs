using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
