using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Common.Utils
{
    public static class FormatterUtility
    {
        public static string GetEnumMemberNameForLogging(Enum messageId)
        {
            return messageId.GetType().Name + "." + messageId.ToString();
        }
    }
}
