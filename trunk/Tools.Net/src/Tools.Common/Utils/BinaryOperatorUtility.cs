using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Common.Utils
{
    public static class BinaryOperatorUtility
    {
        public static bool CheckIfContains(Enum flaggedEnum, Enum testValue)
        {
            return
                ((Convert.ToInt32(flaggedEnum) & Convert.ToInt32(testValue)) != 0);
        }
    }
}
