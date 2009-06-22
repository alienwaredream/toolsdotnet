using System;

namespace Tools.Commands.Translators
{
    public interface IMessageWrapper
    {
        string Wrap(string input);
    }
}