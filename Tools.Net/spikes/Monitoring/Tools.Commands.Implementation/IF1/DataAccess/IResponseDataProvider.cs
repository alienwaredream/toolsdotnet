using System;
using System.Data.OracleClient;
namespace Tools.Commands.Implementation
{

    public interface IResponseDataProvider
    {
        bool UpdateResponseToFtPro(decimal reqId, string processingStatus, string code, string updateMechanism, DateTime responseTime, string errorDesc, string prepaidCredit);
        bool UpdateResponseToFtPro(decimal reqId, string processingStatus, string code, string updateMechanism, DateTime responseTime, string errorDesc, string prepaidCredit, OracleConnection con, OracleTransaction tx);
    }
}