using System;

namespace Tools.TeamBuild.Tasks
{
    public interface IStatePersistor
    {
        void CleanState();
        void WriteState(string content);
        bool ContainsBreak {get;}

        string BreakDate { get; }
        string BreakerDisplayName { get; }
        string BreakerEmailAddress { get; }
    }
}
