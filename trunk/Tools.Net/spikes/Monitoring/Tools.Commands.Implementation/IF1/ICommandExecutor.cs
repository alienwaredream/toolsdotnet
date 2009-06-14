using System;

namespace Tools.Commands.Implementation
{
    public interface ICommandExecutor
    {
        bool Execute(GenericCommand command);
        void Commit();
    }
}