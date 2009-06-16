using System;
using Tools.Commands.Implementation;
using Tools.Commands.Implementation.IF1.Tao;
using Tools.Core.Utils;
using System.Collections.Generic;
using Tools.Core.Asserts;
using System.Globalization;
using System.Xml.Schema;
using System.Xml;
using System.IO;

namespace Tools.Commands.Translators
{
    public class TerminateAddonTranslator : TranslatorBase
    {


        #region ICommand2MessageTranslator Members

        public override MessageShim TranslateToShim(GenericCommand command)
        {
            TerminateAddonMP terminateAddon = new TerminateAddonMP();
            terminateAddon.req = new req();

            cid c = new cid();
            terminateAddon.req.cid = c;
            c.Item = command.TisTDId.ToString();
            c.ItemElementName = ItemChoiceType.TISTDid;

            terminateAddon.req.reqId = command.ReqId.ToString();
            terminateAddon.req.reqTime = command.ReqTime;
            terminateAddon.req.phoneNumber = command.PhoneNumber;

            //MP mp = new MP();
            MP marketingPackage = new MP();

            //for (MPInstance mpInstance : request.getMarketingPackages()) {
            //    AddonInstance a = (AddonInstance) mpInstance;
            //    mp.setId(new BigInteger(a.getMarketingPackageId().toString()));
            //}
            foreach (MarketingPackage mpInstance in command.MarketingPackages)
            {
                marketingPackage.id = mpInstance.MPId.ToString();
            }
            //req.setMP(mp);
            terminateAddon.req.MP = marketingPackage;

            //out.add("commandName", "TerminateAddonTDLevelMP");

            terminateAddon.req.TISwalletId = command.TisWalletId;

            //ctd.req..contractEnd = command.ContractEndDate;
            //ctd.req.monthlyLimit = Convert.ToDouble(command.MonthlyLimit);
            //ctd.req.reqTIme = command.ReqTime;
            //ctd.req.p2p = command.P2P;

            bool canSerialize = true;


            //ErrorTrap.AddAssertion(false, "test assert failed!");
            // If there are any errors accumulated, raise them now.
            //ErrorTrap.RaiseTrappedErrors<CommandValidationError>();
            string messageText = null;

            if (canSerialize)
            {
                try
                {
                    messageText = SerializationUtility.Serialize2String(terminateAddon.req);


                    // if xsd is provided, execute xsd validation
                    //if (!String.IsNullOrEmpty(XsdPath))
                    //{
                    XmlSchemaSet sc = new XmlSchemaSet();

                    //// Add the schema to the collection.
                    sc.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllTypes.xsd", @"IF1\xsd\AllTypes.xsd");
                    sc.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/TerminateAddonMP.xsd", @"IF1\xsd\TerminateAddonMP.xsd");

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