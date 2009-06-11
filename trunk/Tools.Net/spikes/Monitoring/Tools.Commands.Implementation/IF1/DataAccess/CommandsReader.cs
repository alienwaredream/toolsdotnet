using System;
using System.Collections.Generic;
using System.Data;

using System.Data.Common;
using System.Configuration;
using Oracle.DataAccess.Client;


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

        public List<GenericCommand> readNextCommandBatch(string partitionName, string batchId, Int32 commandTypeId, Int32 batchSize, string machineName, string reservationId)
        {
            using (OracleConnection con = new OracleConnection(ConfigurationManager.ConnectionStrings["SourceDB"].ConnectionString))
            {
                // create the command object and set attributes
                using (OracleCommand cmd = new OracleCommand(readerSPName, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.BindByName = true;

                    // create parameter object for the cursor
                    using (OracleParameter pRefCursor = new OracleParameter("p_Commands", OracleDbType.RefCursor))
                    {

                        // this is an output parameter so we must indicate that fact
                        pRefCursor.Direction = ParameterDirection.Output;

                        // add the parameter to the collection
                        cmd.Parameters.Add(pRefCursor);

                        OracleParameter pPartitionName = new OracleParameter("p_PartitionName", OracleDbType.Varchar2);
                        pPartitionName.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(pPartitionName);

                        OracleParameter pBatchId = new OracleParameter("p_BatchId", OracleDbType.Varchar2);
                        pBatchId.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(pBatchId);

                        OracleParameter pCommandType = new OracleParameter("p_CommandType", OracleDbType.Int32);
                        pCommandType.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(pCommandType);

                        OracleParameter pBatchSize = new OracleParameter("p_BatchSize", OracleDbType.Int32);
                        pBatchSize.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(pBatchSize);

                        OracleParameter pMachineName = new OracleParameter("p_MachineName", OracleDbType.Varchar2);
                        pMachineName.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(pMachineName);
                
                        con.Open();
                        OracleTransaction tran = con.BeginTransaction();

                        

                        using (IDataReader dr = cmd.ExecuteReader())
                        {

                            while (dr.Read())
                            {
                                //GenericCommand command = new GenericCommand {
                                //     ReqId = (OracleDbType.Decimal)(dr.GetOrdinal("REQ_ID")
                            }
                        }

                    }
                }


                return null;
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