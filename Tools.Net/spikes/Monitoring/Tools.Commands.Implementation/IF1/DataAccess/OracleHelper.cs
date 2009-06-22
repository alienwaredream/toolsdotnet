using System;
using System.Data.OracleClient;

namespace Tools.Commands.Implementation
{
    public static class OracleHelper
    {
        public static void AssignOracleParameter2String(string paramName, string valueToAssign, OracleCommand cmd)
        {

            if (String.IsNullOrEmpty(valueToAssign))
            {
                cmd.Parameters[paramName].Value = DBNull.Value;
            }
            else
            {
                cmd.Parameters[paramName].Value = valueToAssign;
            }
        }

    }
}
