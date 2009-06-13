using System;

namespace Tools.Commands.Implementation
{
    public interface ICommand2MessageTranslator
    {
        MessageShim TranslateToShim(GenericCommand command);
    }
}