
namespace Tools.Commands.Implementation
{
    public enum CommandMessages
    {
        CommandDispatchedFromTheDatabase = 16001,
        CommandPreparedToBeSentToRequestQueue = 16002,
        WorkOnCommandCommitted = 16003,
        StartingScheduledExecutionIteration = 16004,

        ErrorDispatchingCommandFromTheDatabase = 16051,
        ErrorWhileExecutingTheCommand = 16052,
        ScheduledIterationFailed = 16053,
    }
}