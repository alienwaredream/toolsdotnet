
namespace Tools.Commands.Implementation
{
    public enum CommandMessages
    {
        CommandDispatchedFromTheDatabase = 16001,
        CommandPreparedToBeSentToRequestQueue = 16002,
        WorkOnCommandCommitted = 16003,

        ErrorDispatchingCommandFromTheDatabase = 16051,
        ErrorWhileExecutingTheCommand = 16052,
    }
}