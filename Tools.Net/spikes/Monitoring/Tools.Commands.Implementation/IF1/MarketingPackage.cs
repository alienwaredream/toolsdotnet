using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tools.Commands.Implementation
{
    [Serializable]
    public class MarketingPackage
    {
        //"REQ_ID" NUMBER NOT NULL ENABLE, 
        [XmlAttribute]
        public decimal ReqId { get; set; }
        //"MP_INSTANCE_ID" NUMBER NOT NULL ENABLE, 
        [XmlAttribute]
        public decimal MPInstanceId {get; set;}
        //"MP_ID" NUMBER, 
        [XmlAttribute]
        public decimal MPId { get; set; }
        //"MP_TYPE" VARCHAR2(20), 
        [XmlAttribute]
        public string MPType { get; set; }


        public List<PackageParameter> Parameters { get; set; }

        public MarketingPackage()
        {
            Parameters = new List<PackageParameter>();
        }
    }
}