﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3082
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Tools.Commands.Implementation.IF1.SimpleReqRep
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Simp" +
        "leReqRep.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Simp" +
        "leReqRep.xsd", IsNullable = false)]
    public partial class SimpleReqRep
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
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Simp" +
        "leReqRep.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Simp" +
        "leReqRep.xsd", IsNullable = false)]
    public partial class req
    {

        private string xmlStringField;

        private string updateMechanismField;

        /// <remarks/>
        public string xmlString
        {
            get
            {
                return this.xmlStringField;
            }
            set
            {
                this.xmlStringField = value;
            }
        }

        /// <remarks/>
        public string updateMechanism
        {
            get
            {
                return this.updateMechanismField;
            }
            set
            {
                this.updateMechanismField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Simp" +
        "leReqRep.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Simp" +
        "leReqRep.xsd", IsNullable = false)]
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
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/Simp" +
        "leReqRep.xsd", IsNullable = false)]
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