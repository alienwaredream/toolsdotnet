﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3082
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Tools.Commands.Implementation.IF1.Ctd
{
    using System.Xml.Serialization;

    // 
    // This source code was auto-generated by xsd, Version=2.0.50727.3038.
    // 


    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd", IsNullable = false)]
    public partial class CreateTerminalDevice
    {

        private req reqField;

        private res resField;

        /// <remarks/>
        public req req
        {
            get
            {
                return this.reqField;
            }
            set
            {
                this.reqField = value;
            }
        }

        /// <remarks/>
        public res res
        {
            get
            {
                return this.resField;
            }
            set
            {
                this.resField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd", IsNullable = false)]
    public partial class req
    {

        private string tIScustomerIdField;

        private string tISwalletIdField;

        private TDtype tDtypeField;

        private string tISTDidField;

        private double monthlyLimitField;

        private bool monthlyLimitFieldSpecified;

        private baseMP baseMPField;

        private addOnMPs[] addOnMPsField;

        private TDelements tDelementsField;

        private System.DateTime contractEndField;

        private bool contractEndFieldSpecified;

        private string p2pField;

        private string reqIdField;

        private System.DateTime reqTImeField;

        /// <remarks/>
        public string TIScustomerId
        {
            get
            {
                return this.tIScustomerIdField;
            }
            set
            {
                this.tIScustomerIdField = value;
            }
        }

        /// <remarks/>
        public string TISwalletId
        {
            get
            {
                return this.tISwalletIdField;
            }
            set
            {
                this.tISwalletIdField = value;
            }
        }

        /// <remarks/>
        public TDtype TDtype
        {
            get
            {
                return this.tDtypeField;
            }
            set
            {
                this.tDtypeField = value;
            }
        }

        /// <remarks/>
        public string TISTDid
        {
            get
            {
                return this.tISTDidField;
            }
            set
            {
                this.tISTDidField = value;
            }
        }

        /// <remarks/>
        public double monthlyLimit
        {
            get
            {
                return this.monthlyLimitField;
            }
            set
            {
                this.monthlyLimitField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool monthlyLimitSpecified
        {
            get
            {
                return this.monthlyLimitFieldSpecified;
            }
            set
            {
                this.monthlyLimitFieldSpecified = value;
            }
        }

        /// <remarks/>
        public baseMP baseMP
        {
            get
            {
                return this.baseMPField;
            }
            set
            {
                this.baseMPField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("addOnMPs")]
        public addOnMPs[] addOnMPs
        {
            get
            {
                return this.addOnMPsField;
            }
            set
            {
                this.addOnMPsField = value;
            }
        }

        /// <remarks/>
        public TDelements TDelements
        {
            get
            {
                return this.tDelementsField;
            }
            set
            {
                this.tDelementsField = value;
            }
        }

        /// <remarks/>
        public System.DateTime contractEnd
        {
            get
            {
                return this.contractEndField;
            }
            set
            {
                this.contractEndField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool contractEndSpecified
        {
            get
            {
                return this.contractEndFieldSpecified;
            }
            set
            {
                this.contractEndFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string p2p
        {
            get
            {
                return this.p2pField;
            }
            set
            {
                this.p2pField = value;
            }
        }

        /// <remarks/>
        public string reqId
        {
            get
            {
                return this.reqIdField;
            }
            set
            {
                this.reqIdField = value;
            }
        }

        /// <remarks/>
        public System.DateTime reqTIme
        {
            get
            {
                return this.reqTImeField;
            }
            set
            {
                this.reqTImeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd", IsNullable = false)]
    public enum TDtype
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("1")]
        Item1 = 1,

        /// <remarks/>
        [System.Xml.Serialization.XmlEnumAttribute("2")]
        Item2 = 2,
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd", IsNullable = false)]
    public partial class baseMP
    {

        private string idField;

        private @params[] paramsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger")]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("params")]
        public @params[] @params
        {
            get
            {
                return this.paramsField;
            }
            set
            {
                this.paramsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllT" +
        "ypes.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd", IsNullable = false)]
    public partial class @params
    {

        private string codeField;

        private string valueField;

        private string productCodeField;

        /// <remarks/>
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        public string value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }

        /// <remarks/>
        public string productCode
        {
            get
            {
                return this.productCodeField;
            }
            set
            {
                this.productCodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd", IsNullable = false)]
    public partial class addOnMPs
    {

        private string idField;

        private @params[] paramsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(DataType = "positiveInteger")]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("params")]
        public @params[] @params
        {
            get
            {
                return this.paramsField;
            }
            set
            {
                this.paramsField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd", IsNullable = false)]
    public partial class TDelements
    {

        private object itemField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("fix", typeof(fix))]
        [System.Xml.Serialization.XmlElementAttribute("mobile", typeof(mobile))]
        public object Item
        {
            get
            {
                return this.itemField;
            }
            set
            {
                this.itemField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd", IsNullable = false)]
    public partial class fix
    {

        private string phoneNumberField;

        /// <remarks/>
        public string phoneNumber
        {
            get
            {
                return this.phoneNumberField;
            }
            set
            {
                this.phoneNumberField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd", IsNullable = false)]
    public partial class mobile
    {

        private string iCCIDField;

        private string phoneNumberField;

        private string profileField;

        private string shortNumberField;

        /// <remarks/>
        public string ICCID
        {
            get
            {
                return this.iCCIDField;
            }
            set
            {
                this.iCCIDField = value;
            }
        }

        /// <remarks/>
        public string phoneNumber
        {
            get
            {
                return this.phoneNumberField;
            }
            set
            {
                this.phoneNumberField = value;
            }
        }

        /// <remarks/>
        public string profile
        {
            get
            {
                return this.profileField;
            }
            set
            {
                this.profileField = value;
            }
        }

        /// <remarks/>
        public string shortNumber
        {
            get
            {
                return this.shortNumberField;
            }
            set
            {
                this.shortNumberField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd", IsNullable = false)]
    public partial class res
    {

        private result resultField;

        /// <remarks/>
        public result result
        {
            get
            {
                return this.resultField;
            }
            set
            {
                this.resultField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllT" +
        "ypes.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Crea" +
        "teTerminalDevice.xsd", IsNullable = false)]
    public partial class result
    {

        private string codeField;

        private string descField;

        /// <remarks/>
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        public string desc
        {
            get
            {
                return this.descField;
            }
            set
            {
                this.descField = value;
            }
        }
    }
}