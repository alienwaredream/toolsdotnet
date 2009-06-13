using System;
using System.Collections.Generic;

namespace Tools.Commands.Implementation
{
    [Serializable]
    public class PackageParameter
    {
        //"REQ_ID" NUMBER NOT NULL ENABLE,
        public decimal ReqId { get; set; }
        //"MP_INSTANCE_ID" NUMBER NOT NULL ENABLE, 
        public decimal MPInstanceId { get; set; }
        //"PARAM_CODE" VARCHAR2(20) DEFAULT 'N/A' NOT NULL ENABLE, 
        public string ParamCode { get; set; } 
        //"PRODUCT_CODE" VARCHAR2(20) DEFAULT 'N/A' NOT NULL ENABLE, 
        public string ProductCode { get; set; }
        //"VALUE" VARCHAR2(20), 
        public string Value { get; set; }
    }
}