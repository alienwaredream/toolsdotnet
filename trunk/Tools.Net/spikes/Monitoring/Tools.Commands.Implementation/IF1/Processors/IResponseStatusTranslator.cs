using System;

namespace Tools.Commands.Implementation.IF1.Processors
{
    public interface IResponseStatusTranslator
    {
        string CommandStatus { get; }
        string LogStatus { get; }
        string Description { get; }

        void SetResponse(string response);
        void SetResponse(string response, bool canResubmit, string errorType);
    }
}