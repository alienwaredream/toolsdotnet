using System;
using System.Collections.Generic;
using System.Data;

using System.Data.Common;
using System.Configuration;

using Tools.Core.Asserts;
using System.Data.OracleClient;


namespace Tools.Commands.Implementation
{
    public class ResponseDataProvider : IResponseDataProvider
    {
        string updateResponseSPName;

        public ResponseDataProvider(string updateResponseSPName)
        {
            Init(updateResponseSPName);
        }
        private void Init(string updateResponseSPName)
        {
            ErrorTrap.AddAssertion(!String.IsNullOrEmpty(updateResponseSPName), "updateResponseSPName should be assign in the ctor of " + this.GetType().FullName + ". Please correct the configuration and restart.");

            ErrorTrap.RaiseTrappedErrors<ConfigurationErrorsException>();

            this.updateResponseSPName = updateResponseSPName;
        }

        public bool UpdateResponseToFtPro(
    decimal reqId,
    string processingStatus,
    string code,
    string updateMechanism,
    DateTime responseTime,
    string errorDesc,
    string prepaidCredit,
            OracleConnection con,
            OracleTransaction tx
    )
        {
            // create the command object and set attributes
            // "prov_test_standa.updateresponsetoftpro" - test one
            using (OracleCommand cmd = new OracleCommand(updateResponseSPName, con, tx))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.
                //cmd.BindByName = true;

                //OracleCommandBuilder.DeriveParameters(cmd);

                //foreach (OracleParameter p in cmd.Parameters)
                //{
                //    Console.WriteLine(String.Format("Name: {0}, OracleDbType: {1}", p.ParameterName, p.OracleDbType));
                //}

                OracleParameter pReqId = new OracleParameter("p_REQ_ID", OracleType.Number);
                pReqId.Direction = ParameterDirection.Input;
                pReqId.Value = reqId;
                cmd.Parameters.Add(pReqId);

                OracleParameter pProcessingStatus = new OracleParameter("p_PROCESSING_STATUS", OracleType.VarChar);
                pProcessingStatus.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(pProcessingStatus);
                OracleHelper.AssignOracleParameter2String("p_PROCESSING_STATUS", processingStatus, cmd);


                OracleParameter pCode = new OracleParameter("p_CODE", OracleType.VarChar);
                pCode.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(pCode);
                OracleHelper.AssignOracleParameter2String("p_CODE", code, cmd);

                OracleParameter pUpdateMechanism = new OracleParameter("p_UPDATE_MECHANISM", OracleType.VarChar);
                pUpdateMechanism.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(pUpdateMechanism);
                OracleHelper.AssignOracleParameter2String("p_UPDATE_MECHANISM", updateMechanism, cmd);

                OracleParameter pResponseTime = new OracleParameter("p_RESPONSE_TIME", OracleType.DateTime);
                pResponseTime.Direction = ParameterDirection.Input;
                pResponseTime.Value = responseTime;
                cmd.Parameters.Add(pResponseTime);

                string errorDescription = null;
                // Truncate to the max column length.
                if (!String.IsNullOrEmpty(errorDesc))
                {
                    errorDescription = (errorDesc.Length <= 1000) ? errorDesc : errorDesc.Substring(0, 1000);
                }

                OracleParameter pErrorDesc = new OracleParameter("p_ERROR_DESC", OracleType.Clob);
                pErrorDesc.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(pErrorDesc);
                OracleHelper.AssignOracleParameter2String("p_ERROR_DESC", errorDescription, cmd);


                OracleParameter pPrepaidCredit = new OracleParameter("p_PREPAID_CREDIT", OracleType.VarChar);
                pPrepaidCredit.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(pPrepaidCredit);
                OracleHelper.AssignOracleParameter2String("p_PREPAID_CREDIT", prepaidCredit, cmd);

                OracleParameter pUpdated = new OracleParameter("p_UPDATED", OracleType.Number);
                pUpdated.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pUpdated);

                cmd.ExecuteNonQuery();

                return (decimal)pUpdated.Value == 0;
            }
        }

        public bool UpdateResponseToFtPro(
            decimal reqId,
            string processingStatus,
            string code,
            string updateMechanism,
            DateTime responseTime,
            string errorDesc,
            string prepaidCredit
            )
        {
            using (OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SourceDB"].ConnectionString))
            {

                con.Open();
                OracleTransaction tx = null;

                try
                {
                    tx = con.BeginTransaction();

                    bool res = UpdateResponseToFtPro(
                        reqId, processingStatus, code, updateMechanism, responseTime, errorDesc, prepaidCredit, con, tx);
                    tx.Commit();
                    return res;
                }
                catch (Exception ex)
                {
                    if (tx != null) tx.Rollback();
                    throw;
                }



            }
        }
    }
}