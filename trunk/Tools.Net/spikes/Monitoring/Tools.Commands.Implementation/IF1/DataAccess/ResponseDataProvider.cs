﻿using System;
using System.Collections.Generic;
using System.Data;

using System.Data.Common;
using System.Configuration;
using Oracle.DataAccess.Client;
using Tools.Core.Asserts;


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
            OracleConnection con
    )
        {
            // create the command object and set attributes
            // "prov_test_standa.updateresponsetoftpro" - test one
            using (OracleCommand cmd = new OracleCommand(updateResponseSPName, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.BindByName = true;

                //OracleCommandBuilder.DeriveParameters(cmd);

                //foreach (OracleParameter p in cmd.Parameters)
                //{
                //    Console.WriteLine(String.Format("Name: {0}, OracleDbType: {1}", p.ParameterName, p.OracleDbType));
                //}

                OracleParameter pReqId = new OracleParameter("p_REQ_ID", OracleDbType.Decimal);
                pReqId.Direction = ParameterDirection.Input;
                pReqId.Value = reqId;
                cmd.Parameters.Add(pReqId);

                OracleParameter pProcessingStatus = new OracleParameter("p_PROCESSING_STATUS", OracleDbType.Varchar2);
                pProcessingStatus.Direction = ParameterDirection.Input;
                pProcessingStatus.Value = processingStatus;
                cmd.Parameters.Add(pProcessingStatus);

                OracleParameter pCode = new OracleParameter("p_CODE", OracleDbType.Varchar2);
                pCode.Direction = ParameterDirection.Input;
                pCode.Value = code;
                cmd.Parameters.Add(pCode);

                OracleParameter pUpdateMechanism = new OracleParameter("p_UPDATE_MECHANISM", OracleDbType.Varchar2);
                pUpdateMechanism.Direction = ParameterDirection.Input;
                pUpdateMechanism.Value = updateMechanism;
                cmd.Parameters.Add(pUpdateMechanism);

                OracleParameter pResponseTime = new OracleParameter("p_RESPONSE_TIME", OracleDbType.Date);
                pResponseTime.Direction = ParameterDirection.Input;
                pResponseTime.Value = responseTime;
                cmd.Parameters.Add(pResponseTime);

                OracleParameter pErrorDesc = new OracleParameter("p_ERROR_DESC", OracleDbType.Clob);
                pErrorDesc.Direction = ParameterDirection.Input;
                pErrorDesc.Value = errorDesc;
                cmd.Parameters.Add(pErrorDesc);

                OracleParameter pPrepaidCredit = new OracleParameter("p_PREPAID_CREDIT", OracleDbType.Varchar2);
                pPrepaidCredit.Direction = ParameterDirection.Input;
                pPrepaidCredit.Value = prepaidCredit;
                cmd.Parameters.Add(pPrepaidCredit);

                OracleParameter pUpdated = new OracleParameter("p_UPDATED", OracleDbType.Decimal);
                pUpdated.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(pUpdated);



                cmd.ExecuteNonQuery();

                return ((Oracle.DataAccess.Types.OracleDecimal)(pUpdated.Value)).Value == 0;
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

                return UpdateResponseToFtPro(
                    reqId, processingStatus, code, updateMechanism, responseTime, errorDesc, prepaidCredit);
            
            }
        }
    }
}