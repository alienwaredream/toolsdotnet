using System;
using Tools.Commands.Implementation;
using System.Collections.Generic;
using Tools.Core.Utils;
using System.Xml.Schema;
using System.Xml;
using System.IO;
using Tools.Core.Asserts;

namespace Tools.Commands.Translators
{
    public abstract class TranslatorBase : ICommand2MessageTranslator
    {
        IMessageWrapper messageWrapper = new SimpleReqRepMessageWrapper();
        Dictionary<string, string> schemas = new Dictionary<string, string>();

        public Dictionary<string, string> Schemas
        {
            get { return schemas; }
            set { schemas = value; }
        }

        protected IMessageWrapper MessageWrapper { get { return messageWrapper; } set { messageWrapper = value; } }


        #region ICommand2MessageTranslator Members

        public abstract MessageShim TranslateToShim(GenericCommand command);

        #endregion

        protected string PrepareXmlMessage(object req)
        {
            string retVal = null;

            try
            {
                retVal = SerializationUtility.Serialize2String(req);


                // if xsd is provided, execute xsd validation
                //if (!String.IsNullOrEmpty(XsdPath))
                //{
                XmlSchemaSet sc = new XmlSchemaSet();

                //// Add the schema to the collection.
                foreach (string key in Schemas.Keys)
                {
                    sc.Add(key, Schemas[key]);
                }

                // Set the validation settings.
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = sc;
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

                using (StringReader sReader = new StringReader(retVal))
                {
                    using (XmlReader reader = XmlReader.Create(sReader, settings))
                    {
                        while (reader.Read()) ;
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorTrap.AddAssertion(false, ex.ToString());
            }
            return retVal;
        }
        // Display any validation errors.
        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            ErrorTrap.AddAssertion(false, "xsd error at pos(" + e.Exception.LineNumber + "," + e.Exception.LinePosition + ") " + e.Message + ". Schema: " + e.Exception.SourceSchemaObject);
        }

        protected string PrepareAndWrapMessageText(object msg)
        {
            return messageWrapper.Wrap(PrepareXmlMessage(msg));
        }
    }
}