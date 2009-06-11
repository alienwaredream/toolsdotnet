using System;

namespace Tools.Commands.Implementation
{
    public class GenericCommand
    {
        //"COMMAND_TYPE" NUMBER, 
        public Int32 CommandType { get; set; }
//    "REQ_ID" NUMBER NOT NULL ENABLE, 
        public Decimal ReqId { get; set; }
//    "REQ_TIME" DATE, 
        public DateTime ReqTime { get; set; }
//    "TIS_CUSTOMER_ID" VARCHAR2(15), 
        public string TisCustomerId { get; set; }
//    "TIS_WALLET_ID" VARCHAR2(20), 
        public string TisWalletId { get; set; }
//    "TIS_TD_ID" NUMBER, 
        public Int32 TisTDId { get; set; }
//    "CUSTOMER_TYPE" CHAR(1), 
        public string CustomerType { get; set; }
//    "NAME" VARCHAR2(200), 
        public string Name { get; set; }
//    "BILLING_CYCLE" NUMBER, 
        public Int32 BillingCycle { get; set; }
//    "TAX_GROUP" VARCHAR2(1), 
        public string TaxGroup { get; set; }
//    "TD_TYPE" NUMBER, 
        public Int32 TDType { get; set; }
//    "MONTHLY_LIMIT" NUMBER(22,4), 
        public decimal MonthlyLimit { get; set; }
//    "ICCID" VARCHAR2(64), 
        public string IccId { get; set; }
//    "PHONE_NUMBER" VARCHAR2(64), 
        public string PhoneNumber { get; set; }
//    "BLOCK_REASON" VARCHAR2(20), 
        public string BlockReason { get; set; }
//    "BLOCK_STATUS" NUMBER, 
        public Int32 BlockStatus { get; set; }
//    "VPN_PROFILE" NUMBER, 
        public Int32 VpnProfile { get; set; }
//    "SHORT_NUMBER" VARCHAR2(64), 
        public string ShortNumber { get; set; }
//    "NEW_PHONE_NUMBER" VARCHAR2(64), 
        public string NewPhoneNumber { get; set; }
//    "NEW_ICCID" VARCHAR2(64), 
        public string NewIccId { get; set; }
//    "CONTRACT_END_DATE" DATE, 
        public DateTime ContractEndDate { get; set; }
//    "VALID_FROM" DATE, 
        public DateTime ValidFrom { get; set; }
//    "P2P" VARCHAR2(2), 
        public string P2P { get; set; }
//    "ONETIME_LIMIT_AMOUNT" NUMBER(10,2),
        public decimal OnetimeLimitAmount { get; set; }
//    "STATUS" VARCHAR2(20) DEFAULT 'NEW', 
        public string Status { get; set; }
//    "TIS_POSAO_ID" VARCHAR2(15), 
        public string TisPosAOId { get; set; }
//    "PRIORITY" NUMBER, 
        public Int32 Priority {get; set;}
    }
}