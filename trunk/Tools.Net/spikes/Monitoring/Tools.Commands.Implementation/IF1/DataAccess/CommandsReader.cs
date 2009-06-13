using System;
using System.Collections.Generic;
using System.Data;

using System.Data.Common;
using System.Configuration;
using Oracle.DataAccess.Client;
using Tools.Core.Asserts;


namespace Tools.Commands.Implementation
{
    //GetCommandRecordsToProcess (
    //                         p_PartitionName   in varchar2,
    //                         p_BatchId         in varchar2,
    //                         p_CommandType in integer,
    //                         p_BatchSize in integer,
    //                         p_MachineName in varchar2,
    //                         p_ReservationId in varchar2,
    //                         p_Commands out SYS_REFCURSOR

    //                         )



    public class CommandsReader
    {
        string readerSPName;

        public CommandsReader(string readerSPName)
        {
            this.readerSPName = readerSPName;
        }

        public void ExecuteNextCommandBatch(string partitionName, string batchId, Int32 commandTypeId, Int32 batchSize, string machineName, string reservationId)
        {
            Dictionary<decimal, GenericCommand> commands = new Dictionary<decimal, GenericCommand>();
            Dictionary<decimal, MarketingPackage> mps = new Dictionary<decimal, MarketingPackage>();

            using (OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SourceDB"].ConnectionString))
            {
                // create the command object and set attributes
                using (OracleCommand cmd = new OracleCommand(readerSPName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    #region test parameters

                    //con.Open();

                    //OracleCommandBuilder.DeriveParameters(cmd);

                    //foreach (OracleParameter p in cmd.Parameters)
                    //{
                    //    Console.WriteLine(String.Format("Name: {0}, OracleDbType: {1}", p.ParameterName, p.OracleDbType));
                    //}

                    //return;

                    #endregion

                    OracleParameter pMpsCursor = new OracleParameter("p_Mps", OracleDbType.RefCursor);
                    OracleParameter pParamsCursor = new OracleParameter("p_Params", OracleDbType.RefCursor);

                    using (OracleParameter pRefCursor = new OracleParameter("P_COMMANDS", OracleDbType.RefCursor))
                    {


                        // this is an output parameter so we must indicate that fact
                        pRefCursor.Direction = ParameterDirection.Output;

                        // add the parameter to the collection
                        cmd.Parameters.Add(pRefCursor);
                        //cmd.Parameters.Add(pMpsCursor);
                        //cmd.Parameters.Add(pParamsCursor);

                        OracleParameter pPartitionName = new OracleParameter("P_PARTITIONNAME", OracleDbType.Varchar2);
                        pPartitionName.Direction = ParameterDirection.Input;
                        pPartitionName.Value = partitionName;
                        cmd.Parameters.Add(pPartitionName);

                        OracleParameter pBatchId = new OracleParameter("P_BATCHID", OracleDbType.Varchar2);
                        pBatchId.Direction = ParameterDirection.Input;
                        pBatchId.Value = batchId;
                        cmd.Parameters.Add(pBatchId);

                        OracleParameter pCommandType = new OracleParameter("P_COMMANDTYPE", OracleDbType.Decimal);
                        pCommandType.Direction = ParameterDirection.Input;
                        pCommandType.Value = commandTypeId;
                        cmd.Parameters.Add(pCommandType);

                        OracleParameter pBatchSize = new OracleParameter("P_BATCHSIZE", OracleDbType.Decimal);
                        pBatchSize.Direction = ParameterDirection.Input;
                        pBatchSize.Value = batchSize;
                        cmd.Parameters.Add(pBatchSize);

                        OracleParameter pMachineName = new OracleParameter("P_MACHINENAME", OracleDbType.Varchar2);
                        pMachineName.Direction = ParameterDirection.Input;
                        pMachineName.Value = machineName;
                        cmd.Parameters.Add(pMachineName);

                        OracleParameter pReservationId = new OracleParameter("P_RESERVATIONID", OracleDbType.Varchar2);
                        pReservationId.Direction = ParameterDirection.Input;
                        pReservationId.Value = reservationId;
                        cmd.Parameters.Add(pReservationId);

                        con.Open();

                        IDbTransaction tran = con.BeginTransaction();


                        try
                        {

                            using (IDataReader dr = cmd.ExecuteReader())
                            {

                                while (dr.Read())
                                {
                                    Console.WriteLine(Convert.ToDecimal(dr["REQ_ID"]) + ":" + dr["Status"]);

                                    GenericCommand gCmd = new GenericCommand();

                                    gCmd.ReqId = GetDefaultDecimal(dr, "REQ_ID", true, 0);
                                    gCmd.CommandType = GetDefaultDecimal(dr, "COMMAND_TYPE", true, 0);
                                    gCmd.ReqTime = GetDefaultDate(dr, "REQ_TIME", true, DateTime.Now);
                                    gCmd.TisCustomerId = GetDefaultString(dr, "TIS_CUSTOMER_ID", true, String.Empty);

                                    gCmd.TisWalletId = GetDefaultString(dr, "TIS_WALLET_ID", false, String.Empty);
                                    gCmd.TisTDId = GetDefaultDecimal(dr, "TIS_TD_ID", false, 0);
                                    gCmd.CustomerType = GetDefaultString(dr, "CUSTOMER_TYPE", false, String.Empty);
                                    gCmd.Name = GetDefaultString(dr, "NAME", false, String.Empty);
                                    gCmd.BillingCycle = GetDefaultDecimal(dr, "BILLING_CYCLE", false, 0);

                                    gCmd.TaxGroup = GetDefaultString(dr, "TAX_GROUP", false, String.Empty);
                                    gCmd.TDType = GetDefaultDecimal(dr, "TD_TYPE", false, 0);

                                    gCmd.MonthlyLimit = GetDefaultDecimal(dr, "MONTHLY_LIMIT", false, 0);

                                    gCmd.IccId = GetDefaultString(dr, "ICCID", false, String.Empty);
                                    gCmd.PhoneNumber = GetDefaultString(dr, "PHONE_NUMBER", false, String.Empty);
                                    gCmd.BlockReason = GetDefaultString(dr, "BLOCK_REASON", false, String.Empty);
                                    gCmd.BlockStatus = GetDefaultDecimal(dr, "BLOCK_STATUS", false, 0);

                                    gCmd.VpnProfile = GetDefaultDecimal(dr, "VPN_PROFILE", false, 0);
                                    gCmd.ShortNumber = GetDefaultString(dr, "SHORT_NUMBER", false, String.Empty);
                                    gCmd.NewPhoneNumber = GetDefaultString(dr, "NEW_PHONE_NUMBER", false, String.Empty);
                                    gCmd.NewIccId = GetDefaultString(dr, "NEW_ICCID", false, String.Empty);
                                    gCmd.ContractEndDate = GetDefaultDate(dr, "CONTRACT_END_DATE", false, DateTime.MinValue);
                                    gCmd.ValidFrom = GetDefaultDate(dr, "VALID_FROM", false, DateTime.MinValue);
                                    gCmd.P2P = GetDefaultString(dr, "P2P", false, String.Empty);
                                    gCmd.OnetimeLimitAmount = GetDefaultDecimal(dr, "ONETIME_LIMIT_AMOUNT", false, 0);
                                    gCmd.Status = GetDefaultString(dr, "STATUS", false, String.Empty);
                                    gCmd.TisPosAOId = GetDefaultString(dr, "TIS_POSAO_ID", false, String.Empty);
                                    gCmd.Priority = GetDefaultDecimal(dr, "PRIORITY", false, 0);

                                    ErrorTrap.RaiseTrappedErrors<InvalidOperationException>();

                                    commands.Add(gCmd.ReqId, gCmd);

                                }
                                // Next result is marketing package instances
                                using (OracleConnection cmp = new OracleConnection(ConfigurationManager.ConnectionStrings["SourceDB"].ConnectionString))
                                {

                                    while (dr.Read())
                                    {
                                        Console.WriteLine(Convert.ToDecimal(dr["REQ_ID"]) + ":" + Convert.ToDecimal(dr["MPInstanceId"]));

                                        MarketingPackage mp = new MarketingPackage
                                        {
                                            ReqId = GetDefaultDecimal(dr, "REQ_ID", true, 0),
                                            MPInstanceId = GetDefaultDecimal(dr, "MP_INSTANCE_ID", true, 0),
                                            MPId = GetDefaultDecimal(dr, "MP_ID", true, 0),
                                            MPType = GetDefaultString(dr, "MP_TYPE", true, String.Empty)
                                        };

                                        ErrorTrap.RaiseTrappedErrors<InvalidOperationException>();

                                        commands[mp.ReqId].MarketingPackages.Add(mp);
                                        mps.Add(mp.MPInstanceId, mp);
                                    }
                                }

                                // Next result is marketing package parameters
                                if (dr.NextResult())
                                {

                                    while (dr.Read())
                                    {
                                        Console.WriteLine(Convert.ToDecimal(dr["REQ_ID"]) + ":" + dr["ParamCode"]);

                                        PackageParameter p = new PackageParameter
                                        {
                                            ReqId = GetDefaultDecimal(dr, "REQ_ID", true, 0),
                                            MPInstanceId = GetDefaultDecimal(dr, "MP_INSTANCE_ID", true, 0),
                                            ParamCode = GetDefaultString(dr, "PARAM_CODE", true, String.Empty),
                                            ProductCode = GetDefaultString(dr, "PRODUCT_CODE", true, String.Empty),
                                            Value = GetDefaultString(dr, "VALUE", true, String.Empty)
                                        };

                                        mps[p.MPInstanceId].Parameters.Add(p);

                                    }
                                }


                                tran.Rollback();
                                //tran.Commit();
                            }
                        }
                        catch (Exception ex)
                        {

                            tran.Rollback();
                            throw;
                        }
                        finally
                        {
                            if (tran != null) tran.Dispose();
                            if (pMpsCursor != null) pMpsCursor.Dispose();
                            if (pParamsCursor != null) pParamsCursor.Dispose();
                        }

                    }
                }


                //return null;
            }
        }
        String GetDefaultString(IDataReader dr, string fieldName, bool mandatory, string defaultValue)
        {
            bool isNull = dr.IsDBNull(dr.GetOrdinal(fieldName));
            if (!ErrorTrap.AddAssertion(!(mandatory && isNull),
                String.Format("Field {0} is mandatoryfor application but its value is null", fieldName)))
            {
                return null;
            }
            if (!mandatory && isNull) return defaultValue;

            return dr.GetString(dr.GetOrdinal(fieldName));
        }

        Decimal GetDefaultDecimal(IDataReader dr, string fieldName, bool mandatory, decimal defaultValue)
        {
            bool isNull = dr.IsDBNull(dr.GetOrdinal(fieldName));
            if (!ErrorTrap.AddAssertion(!(mandatory && isNull),
                String.Format("Field {0} is mandatoryfor application but its value is null", fieldName)))
            {
                return 0;
            }
            if (!mandatory && isNull) return defaultValue;

            return dr.GetDecimal(dr.GetOrdinal(fieldName));
        }
        DateTime GetDefaultDate(IDataReader dr, string fieldName, bool mandatory, DateTime defaultValue)
        {
            bool isNull = dr.IsDBNull(dr.GetOrdinal(fieldName));
            if (!ErrorTrap.AddAssertion(!(mandatory && isNull),
                String.Format("Field {0} is mandatoryfor application but its value is null", fieldName)))
            {
                return DateTime.MinValue;
            }
            if (!mandatory && isNull) return defaultValue;

            return dr.GetDateTime(dr.GetOrdinal(fieldName));
        }
    }
}

#region Commands table dll

//"COMMAND_TYPE" NUMBER, 
//    "REQ_ID" NUMBER NOT NULL ENABLE, 
//    "REQ_TIME" DATE, 
//    "TIS_CUSTOMER_ID" VARCHAR2(15), 
//    "TIS_WALLET_ID" VARCHAR2(20), 
//    "TIS_TD_ID" NUMBER, 
//    "CUSTOMER_TYPE" CHAR(1), 
//    "NAME" VARCHAR2(200), 
//    "BILLING_CYCLE" NUMBER, 
//    "TAX_GROUP" VARCHAR2(1), 
//    "TD_TYPE" NUMBER, 
//    "MONTHLY_LIMIT" NUMBER(22,4), 
//    "ICCID" VARCHAR2(64), 
//    "PHONE_NUMBER" VARCHAR2(64), 
//    "BLOCK_REASON" VARCHAR2(20), 
//    "BLOCK_STATUS" NUMBER, 
//    "VPN_PROFILE" NUMBER, 
//    "SHORT_NUMBER" VARCHAR2(64), 
//    "NEW_PHONE_NUMBER" VARCHAR2(64), 
//    "NEW_ICCID" VARCHAR2(64), 
//    "CONTRACT_END_DATE" DATE, 
//    "VALID_FROM" DATE, 
//    "P2P" VARCHAR2(2), 
//    "ONETIME_LIMIT_AMOUNT" NUMBER(10,2), 
//    "STATUS" VARCHAR2(20) DEFAULT 'NEW', 
//    "TIS_POSAO_ID" VARCHAR2(15), 
//    "PRIORITY" NUMBER, 
//     CONSTRAINT "FORIS_COMMANDS_PROV_PK" PRIMARY KEY ("REQ_ID")
//  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
//  STORAGE(INITIAL 2097152 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
//  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT)
//  TABLESPACE "USERS"  ENABLE, 
//     CONSTRAINT "PROV_BLOCK_CHECK" CHECK (block_reason in ('BLOCK_USER','BLOCK_FINANCIAL','BLOCK_USAGE','BLOCK_ADMIN')) ENABLE, 
//     CONSTRAINT "PROV_C_TYPE_CHECK" CHECK (customer_type in ('O','P')) ENABLE, 
//     CONSTRAINT "PROV_STATUS_CHECK" CHECK (status in ('NEW','PROCESSING','DONE','FAILED','XXX','YYY','OFFLINE','TEST','OFFLINE1','BAD','PP','GOOD','EXISTCM')) ENABLE, 
//     CONSTRAINT "FORIS_COMMAND_PROV_TYPE_FK" FOREIGN KEY ("COMMAND_TYPE")
//      REFERENCES "FTPRO"."FORIS_COMMAND_TYPE" ("COMMAND_TYPE_ID") ENABLE NOVALIDATE
//   ) PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
//  STORAGE(INITIAL 6291456 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
//  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT)
//  TABLESPACE "FTPRO" ;

//  CREATE UNIQUE INDEX "FTPRO"."FORIS_COMMANDS_PROV_PK" ON "FTPRO"."FORIS_COMMANDS_PROV" ("REQ_ID") 
//  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
//  STORAGE(INITIAL 2097152 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
//  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT)
//  TABLESPACE "USERS" ;

//  CREATE INDEX "FTPRO"."I_FCP_PHONE_NUMBER" ON "FTPRO"."FORIS_COMMANDS_PROV" ("PHONE_NUMBER") 
//  PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
//  STORAGE(INITIAL 196608 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
//  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT)
//  TABLESPACE "FTPRO" ;

//  ALTER TABLE "FTPRO"."FORIS_COMMANDS_PROV" ADD CONSTRAINT "FORIS_COMMANDS_PROV_PK" PRIMARY KEY ("REQ_ID")
//  USING INDEX PCTFREE 10 INITRANS 2 MAXTRANS 255 COMPUTE STATISTICS 
//  STORAGE(INITIAL 2097152 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
//  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT)
//  TABLESPACE "USERS"  ENABLE;

#endregion