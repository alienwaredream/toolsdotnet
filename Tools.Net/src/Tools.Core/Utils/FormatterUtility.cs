using System;

namespace Tools.Core.Utils
{
    public static class FormatterUtility
    {
        public static string GetEnumMemberNameForLogging(Enum messageId)
        {
            return messageId.GetType().Name + "." + messageId;
        }
    }
}