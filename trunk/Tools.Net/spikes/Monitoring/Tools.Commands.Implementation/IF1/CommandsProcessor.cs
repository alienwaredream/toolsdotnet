using System;
using System.Collections.Generic;
using System.Data;

using System.Data.Common;
using System.Configuration;
using Oracle.DataAccess.Client;
using Tools.Core.Asserts;
using System.Transactions;
using Tools.Core.Utils;
using System.Runtime.Serialization;
using System.Text;


namespace Tools.Commands.Implementation
{


    public class CommandsProcessor
    {
        string readerSPName;

        Dictionary<decimal, ICommandExecutor> executors;

        public CommandsProcessor(string readerSPName, Dictionary<decimal, ICommandExecutor> executors)
        {
            Init(readerSPName, executors);
        }
        private void Init(string readerSPName, Dictionary<decimal, ICommandExecutor> executors)
        {
            ErrorTrap.AddAssertion(!String.IsNullOrEmpty(readerSPName), "readerSPName should be assign in the ctor of " + this.GetType().FullName + ". Please correct the configuration and restart.");

            ErrorTrap.AddAssertion(executors != null && executors.Count != 0, "Dictionary<decimal, ICommandExecutor> executors can't be null. Please correct the configuration for " + this.GetType().FullName + " and restart.");

            ErrorTrap.RaiseTrappedErrors<ConfigurationErrorsException>();

            this.readerSPName = readerSPName;
            this.executors = executors;
        }

        public void ExecuteNextCommand(CommandSelectionOptions options)
        {
            ErrorTrap.AddAssertion(options != null, "Filter parameter of ExecuteNextCommand should not be null!");
            ErrorTrap.RaiseTrappedErrors<ArgumentNullException>();

            options.MachineName = Environment.MachineName;
            OracleTransaction transaction = null;
            //NOTE: (SD) The code bellow is written for only one record!!! Even if it is a collection.
            //Different handling of exception/transaction would be required if there are multiple records
            try
            {
                using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["SourceDB"].ConnectionString))
                {

                    connection.Open();

                    transaction = connection.BeginTransaction();

                    Dictionary<decimal, GenericCommand> commands = PopulateCommands(options, connection);

                    LogCommands(commands);



                    foreach (decimal reqId in commands.Keys)
                    {
                        try
                        {
                            GenericCommand command = commands[reqId];
                            executors[command.CommandType].Execute(command);

                        }

                        catch
                        {

                                if (transaction!=null) transaction.Rollback();
                                throw;
                        }
                    }

                    // transaction.Commit();
                    transaction.Rollback();

                }
            }
            catch (Exception ex)
            {
                if (transaction != null) transaction.Rollback();

                throw;
            }
        }

        private static void LogCommands(Dictionary<decimal, GenericCommand> commands)
        {
            StringBuilder sb = new StringBuilder(4000);

            foreach (decimal reqId in commands.Keys)
            {
                sb.Append(SerializationUtility.Serialize2String(commands[reqId]));
            }

            Log.TraceData(Log.Source, System.Diagnostics.TraceEventType.Verbose, CommandMessages.CommandDispatchedFromTheDatabase, sb.ToString());
        }

        private Dictionary<decimal, GenericCommand> PopulateCommands(CommandSelectionOptions options, OracleConnection con)
        {
            Dictionary<decimal, GenericCommand> commands = new Dictionary<decimal, GenericCommand>();

            Dictionary<decimal, MarketingPackage> mps = new Dictionary<decimal, MarketingPackage>();


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
                    pMpsCursor.Direction = ParameterDirection.Output;
                    pParamsCursor.Direction = ParameterDirection.Output;

                    // add the parameter to the collection
                    cmd.Parameters.Add(pRefCursor);
                    cmd.Parameters.Add(pMpsCursor);
                    cmd.Parameters.Add(pParamsCursor);

                    OracleParameter pPartitionName = new OracleParameter("P_PARTITIONNAME", OracleDbType.Varchar2);
                    pPartitionName.Direction = ParameterDirection.Input;
                    pPartitionName.Value = options.PartitionName;
                    cmd.Parameters.Add(pPartitionName);

                    OracleParameter pBatchId = new OracleParameter("P_BATCHID", OracleDbType.Varchar2);
                    pBatchId.Direction = ParameterDirection.Input;
                    pBatchId.Value = options.BatchId;
                    cmd.Parameters.Add(pBatchId);

                    OracleParameter pCommandType = new OracleParameter("P_COMMANDTYPE", OracleDbType.Decimal);
                    pCommandType.Direction = ParameterDirection.Input;
                    pCommandType.Value = options.CommandTypeId;
                    cmd.Parameters.Add(pCommandType);

                    //OracleParameter pBatchSize = new OracleParameter("P_BATCHSIZE", OracleDbType.Decimal);
                    //pBatchSize.Direction = ParameterDirection.Input;
                    //pBatchSize.Value = options.BatchSize;
                    //cmd.Parameters.Add(pBatchSize);

                    OracleParameter pMachineName = new OracleParameter("P_MACHINENAME", OracleDbType.Varchar2);
                    pMachineName.Direction = ParameterDirection.Input;
                    pMachineName.Value = options.MachineName;
                    cmd.Parameters.Add(pMachineName);

                    OracleParameter pUserName = new OracleParameter("P_USERNAME", OracleDbType.Varchar2);
                    pUserName.Direction = ParameterDirection.Input;
                    pUserName.Value = Environment.UserName;
                    cmd.Parameters.Add(pUserName);


                    OracleParameter pActivityId = new OracleParameter("P_ACTIVITYID", OracleDbType.Varchar2);
                    pActivityId.Direction = ParameterDirection.Input;
                    pActivityId.Value = options.ActivityId;
                    cmd.Parameters.Add(pActivityId);

                    try
                    {

                        using (IDataReader dr = cmd.ExecuteReader())
                        {

                            while (dr.Read())
                            {
                                //Console.WriteLine(Convert.ToDecimal(dr["REQ_ID"]) + ":" + dr["Status"]);

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

                                gCmd.ActivityId = options.ActivityId;

                                commands.Add(gCmd.ReqId, gCmd);

                            }
                            // Next result is marketing package instances
                            if (dr.NextResult())
                            {
                                while (dr.Read())
                                {
                                    //Console.WriteLine(Convert.ToDecimal(dr["REQ_ID"]) + ":mpid:" + Convert.ToDecimal(dr["MP_INSTANCE_ID"]));

                                    MarketingPackage mp = new MarketingPackage
                                    {
                                        ReqId = GetDefaultDecimal(dr, "REQ_ID", true, 0),
                                        MPInstanceId = GetDefaultDecimal(dr, "MP_INSTANCE_ID", true, 0),
                                        MPId = GetDefaultDecimal(dr, "MP_ID", true, 0),
                                        MPType = GetDefaultString(dr, "MP_TYPE", true, String.Empty)
                                    };

                                    commands[mp.ReqId].MarketingPackages.Add(mp);
                                    mps.Add(mp.MPInstanceId, mp);
                                }
                                //}

                                // Next result is marketing package parameters
                                if (dr.NextResult())
                                {
                                    while (dr.Read())
                                    {
                                        //Console.WriteLine("mpid: " + Convert.ToDecimal(dr["MP_INSTANCE_ID"]) + ":" + dr["PARAM_CODE"]);

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
                            }

                            ErrorTrap.RaiseTrappedErrors<InvalidOperationException>();
                            return commands;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        if (pMpsCursor != null) pMpsCursor.Dispose();
                        if (pParamsCursor != null) pParamsCursor.Dispose();
                    }
                }
            }
            return commands;
        }

        String GetDefaultString(IDataReader dr, string fieldName, bool mandatory, string defaultValue)
        {
            try
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
            catch (Exception ex)
            {
                ErrorTrap.AddAssertion(ex == null, String.Format("Exception when converting field {0}. \r\n {1}",
                    fieldName, ex));
                return null;
            }
        }

        Decimal GetDefaultDecimal(IDataReader dr, string fieldName, bool mandatory, decimal defaultValue)
        {
            try
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
            catch (Exception ex)
            {
                ErrorTrap.AddAssertion(ex == null, String.Format("Exception when converting field {0}. \r\n {1}",
                    fieldName, ex));
                return 0;
            }
        }
        
        DateTime GetDefaultDate(IDataReader dr, string fieldName, bool mandatory, DateTime defaultValue)
        {
            try
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
            catch (Exception ex)
            {
                ErrorTrap.AddAssertion(ex == null, String.Format("Exception when converting field {0}. \r\n {1}",
                    fieldName, ex));
                return DateTime.MinValue; ;
            }
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