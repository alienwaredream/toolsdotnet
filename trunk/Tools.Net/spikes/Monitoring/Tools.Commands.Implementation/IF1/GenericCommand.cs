using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tools.Commands.Implementation
{
    [Serializable]
    public class GenericCommand
    {
        public string ActivityId { get; set; }
        //"COMMAND_TYPE" NUMBER, 
        [XmlAttribute]
        public Decimal CommandType { get; set; }
//    "REQ_ID" NUMBER NOT NULL ENABLE, 
        [XmlAttribute]
        public Decimal ReqId { get; set; }
//    "REQ_TIME" DATE, 
        [XmlAttribute]
        public DateTime ReqTime { get; set; }
//    "TIS_CUSTOMER_ID" Varchar(15), 
        [XmlAttribute]
        public string TisCustomerId { get; set; }
//    "TIS_WALLET_ID" Varchar(20), 
        [XmlAttribute]
        public string TisWalletId { get; set; }
//    "TIS_TD_ID" NUMBER, 
        [XmlAttribute]
        public Decimal TisTDId { get; set; }
//    "CUSTOMER_TYPE" CHAR(1), 
        [XmlAttribute]
        public string CustomerType { get; set; }
//    "NAME" Varchar(200), 
        [XmlAttribute]
        public string Name { get; set; }
//    "BILLING_CYCLE" NUMBER, 

        public Decimal? BillingCycle { get; set; }
//    "TAX_GROUP" Varchar(1), 
        [XmlAttribute]
        public string TaxGroup { get; set; }
//    "TD_TYPE" NUMBER, 
        [XmlAttribute]
        public Decimal TDType { get; set; }
//    "MONTHLY_LIMIT" NUMBER(22,4),
        public decimal? MonthlyLimit { get; set; }
//    "ICCID" Varchar(64), 
        [XmlAttribute]
        public string IccId { get; set; }
//    "PHONE_NUMBER" Varchar(64), 
        [XmlAttribute]
        public string PhoneNumber { get; set; }
//    "BLOCK_REASON" Varchar(20), 
        [XmlAttribute]
        public string BlockReason { get; set; }
//    "BLOCK_STATUS" NUMBER, 

        public Decimal? BlockStatus { get; set; }
//    "VPN_PROFILE" NUMBER, 
        [XmlAttribute]
        public Decimal VpnProfile { get; set; }
//    "SHORT_NUMBER" Varchar(64), 
        [XmlAttribute]
        public string ShortNumber { get; set; }
//    "NEW_PHONE_NUMBER" Varchar(64), 
        [XmlAttribute]
        public string NewPhoneNumber { get; set; }
//    "NEW_ICCID" Varchar(64), 
        [XmlAttribute]
        public string NewIccId { get; set; }
//    "CONTRACT_END_DATE" DATE, 
        public DateTime? ContractEndDate { get; set; }
//    "VALID_FROM" DATE, 
        [XmlAttribute]
        public DateTime ValidFrom { get; set; }
//    "P2P" Varchar(2), 
        [XmlAttribute]
        public string P2P { get; set; }
//    "ONETIME_LIMIT_AMOUNT" NUMBER(10,2),
        [XmlAttribute]
        public decimal OnetimeLimitAmount { get; set; }
//    "STATUS" Varchar(20) DEFAULT 'NEW', 
        [XmlAttribute]
        public string Status { get; set; }
//    "TIS_POSAO_ID" Varchar(15), 
        [XmlAttribute]
        public string TisPosAOId { get; set; }
//    "PRIORITY" NUMBER, 
        [XmlAttribute]
        public Decimal Priority {get; set;}

        public List<MarketingPackage> MarketingPackages { get; set; }

        public GenericCommand()
        {
            MarketingPackages = new List<MarketingPackage>();
        }
    }
}