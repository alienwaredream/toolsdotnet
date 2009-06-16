using System;
using Tools.Commands.Implementation;
using Tools.Commands.Implementation.IF1.Ctd;
using Tools.Core.Utils;
using System.Collections.Generic;
using Tools.Core.Asserts;
using System.Globalization;
using System.Xml.Schema;
using System.Xml;
using System.IO;

namespace Tools.Commands.Translators
{
    public class CreateTerminalDeviceTranslator : TranslatorBase
    {

        #region ICommand2MessageTranslator Members

        public override MessageShim TranslateToShim(GenericCommand command)
        {
            CreateTerminalDevice ctd = new CreateTerminalDevice();

            ctd.req = new req();

            ctd.req.reqId = command.ReqId.ToString(CultureInfo.InvariantCulture);
            ctd.req.TIScustomerId = command.TisCustomerId;
            ctd.req.TISTDid = command.TisTDId.ToString(CultureInfo.InvariantCulture);
            ctd.req.TISwalletId = command.TisWalletId;

            List<addOnMPs> addOnMPs = new List<addOnMPs>();
            bool isBaseMPSet = false;

            //TODO: (SD) can't use extension methods
            foreach (MarketingPackage mp in command.MarketingPackages)
            {
                // Base package is discriminated by the BASE MPType
                // Only allow one base marketing package, more than one is an error.
                if (mp.MPType.ToUpper() == "BASE")
                {
                    if (ErrorTrap.AddAssertion(!isBaseMPSet, CommandValidationError.DuplicateBaseMarketingPackage))
                    {

                        isBaseMPSet = true;

                        baseMP bmp = new baseMP { id = mp.MPId.ToString() };

                        List<@params> parameters = new List<@params>();

                        foreach (PackageParameter pp in mp.Parameters)
                        {
                            parameters.Add(new @params
                            {
                                code = pp.ParamCode,
                                productCode = pp.ProductCode,
                                value = pp.Value
                            });
                        }

                        bmp.@params = parameters.ToArray();

                        ctd.req.baseMP = bmp;
                    }

                    continue;
                }
                // Base package is discriminated by the BASE MPType
                if (mp.MPType.ToUpper() == "ADDON")
                {
                    addOnMPs aomp = new addOnMPs { id = mp.MPId.ToString() };

                    List<@params> parameters = new List<@params>();

                    foreach (PackageParameter pp in mp.Parameters)
                    {
                        parameters.Add(new @params
                        {
                            code = pp.ParamCode,
                            productCode = pp.ProductCode,
                            value = pp.Value
                        });
                    }

                    aomp.@params = parameters.ToArray();

                    addOnMPs.Add(aomp);

                    continue;
                }

                ctd.req.addOnMPs = addOnMPs.ToArray();
            }

            ctd.req.contractEnd = command.ContractEndDate;
            ctd.req.monthlyLimit = Convert.ToDouble(command.MonthlyLimit);
            ctd.req.reqTIme = command.ReqTime;
            ctd.req.p2p = command.P2P;

            bool canSerialize = true;

            //if (ErrorTrap.AddAssertion(Enum.IsDefined(typeof(TDtype), command.TDType.ToString()),
            //    String.Format("Command TDType value {0} doesn't fall in the allowed range.", command.TDType)))
            //{

            ctd.req.TDtype = (TDtype)command.TDType;
            //}
            //else
            //{
            //    canSerialize = false;
            //}

            TDelements tdElements = new TDelements();

            //TODO: (SD) Find out if this is required actually or not
            if (!String.IsNullOrEmpty(command.IccId))
            {
                mobile m = new mobile();
                m.ICCID = command.IccId;
                m.phoneNumber = command.PhoneNumber;
                m.profile = command.VpnProfile.ToString();
                m.shortNumber = command.ShortNumber;

                tdElements.Item = m;
                ctd.req.TDelements = tdElements;
            }

            //ErrorTrap.AddAssertion(false, "test assert failed!");
            // If there are any errors accumulated, raise them now.
            //ErrorTrap.RaiseTrappedErrors<CommandValidationError>();
            string messageText = null;

            if (canSerialize)
            {
                try
                {
                    messageText = SerializationUtility.Serialize2String(ctd.req);


                    // if xsd is provided, execute xsd validation
                    //if (!String.IsNullOrEmpty(XsdPath))
                    //{
                    XmlSchemaSet sc = new XmlSchemaSet();

                    //// Add the schema to the collection.
                    sc.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllTypes.xsd", @"IF1\xsd\AllTypes.xsd");
                    sc.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/CreateTerminalDevice.xsd", @"IF1\xsd\CreateTerminalDevice.xsd");

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