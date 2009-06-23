
namespace Tools.Commands.Implementation
{
    public enum CommandMessages
    {
        CommandDispatchedFromTheDatabase = 16001,
        CommandDeliveredToRequestQueue = 16002,
        WorkOnCommandCommitted = 16003,
        StartingScheduledExecutionIteration = 16004,
        NoCommandsFound = 16005,
        CleaningConnectionsPool = 16006,

        ErrorDispatchingCommandFromTheDatabase = 16051,
        ErrorWhileExecutingTheCommand = 16052,
        ScheduledIterationFailed = 16053,
    }
}