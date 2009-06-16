using System;
using Tools.Commands.Implementation;
using Tools.Core.Utils;
using System.Collections.Generic;
using Tools.Core.Asserts;
using System.Globalization;
using System.Xml.Schema;
using System.Xml;
using System.IO;

using Tools.Commands.Implementation.IF1.CreateCustomer;

namespace Tools.Commands.Translators
{
    public class CreateCustomerTranslator : TranslatorBase
    {
        #region ICommand2MessageTranslator Members

        public override MessageShim TranslateToShim(GenericCommand command)
        {


            //req.setCustomerType(request.getTisCustomerType());
            //req.setName(request.getName());
            //req.setReqId(request.getRequestId().toString());
            //req.setReqTime(BasicConverter.timestampToXMLGregorianCalendar(request.getRequestTime()));
            //req.setTaxGroup(request.getTaxGroup());
            //req.setTISCustomerId(request.getTisCustomerId());
            //req.setTISwalletId(request.getTisWalletId());

            CreateCustomer createCustomer = new CreateCustomer();
            createCustomer.req = new Tools.Commands.Implementation.IF1.CreateCustomer.req();

            if (command.BillingCycle.HasValue)
            {
                createCustomer.req.billingCycle = command.BillingCycle.ToString();
            }

            createCustomer.req.reqId = command.ReqId.ToString();
            createCustomer.req.reqTime = command.ReqTime;
            createCustomer.req.taxGroup = command.TaxGroup;
            createCustomer.req.TIScustomerId = command.TisCustomerId;
            createCustomer.req.TISwalletId = command.TisWalletId;
            createCustomer.req.customerType = (customerType)Enum.Parse(typeof(customerType), command.CustomerType);
            createCustomer.req.name = command.Name;


            bool canSerialize = true;


            //ErrorTrap.AddAssertion(false, "test assert failed!");
            // If there are any errors accumulated, raise them now.
            //ErrorTrap.RaiseTrappedErrors<CommandValidationError>();
            string messageText = null;

            if (canSerialize)
            {
                try
                {
                    messageText = SerializationUtility.Serialize2String(createCustomer.req);


                    // if xsd is provided, execute xsd validation
                    //if (!String.IsNullOrEmpty(XsdPath))
                    //{
                    XmlSchemaSet sc = new XmlSchemaSet();

                    //// Add the schema to the collection.
                    sc.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllTypes.xsd", @"IF1\xsd\AllTypes.xsd");
                    sc.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/CreateCustomer.xsd", @"IF1\xsd\CreateCustomer.xsd");

                    // Set the validation settings.
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.ValidationType = ValidationType.Schema;
                    settings.Schemas = sc;
                    settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);

                    using (StringReader sReader = new StringReader(messageText))
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
            }
            //}


            return new MessageShim
            {
                CorrelationId = command.ReqId.ToString(),
                Text = MessageWrapper.Wrap(messageText)
            };
        }

        // Display any validation errors.
        private static void ValidationCallBack(object sender, ValidationEventArgs e)
        {
            ErrorTrap.AddAssertion(false, "xsd error at pos(" + e.Exception.LineNumber + "," + e.Exception.LinePosition + ") " + e.Message + ". Schema: " + e.Exception.SourceSchemaObject);
        }

        #endregion
    }
}