using System;
using Tools.Core.Utils;

namespace Tools.Commands.Translators
{
    internal class SimpleReqRepMessageWrapper : IMessageWrapper
    {
        public string Wrap(string input)
        {
            Tools.Commands.Implementation.IF1.SimpleReqRep.req r = new Tools.Commands.Implementation.IF1.SimpleReqRep.req { xmlString = input, updateMechanism = "JMS" };

            return SerializationUtility.Serialize2String(r);
        }
    }
}
