using System;
using Tools.Commands.Implementation;
using Tools.Core.Utils;
using System.Collections.Generic;
using Tools.Core.Asserts;
using System.Globalization;
using System.Xml.Schema;
using System.Xml;
using System.IO;

using Tools.Commands.Implementation.IF1.AddAddonMP;

namespace Tools.Commands.Translators
{
    public class AddTDLevelAddonMPTranslator : TranslatorBase
    {
        public AddTDLevelAddonMPTranslator()
        {
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllTypes.xsd", AppDomain.CurrentDomain.BaseDirectory + @"\IF1\xsd\AllTypes.xsd");
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AddAddonMP.xsd", AppDomain.CurrentDomain.BaseDirectory + @"\IF1\xsd\AddAddonMP.xsd");
        }
        #region ICommand2MessageTranslator Members

        public override MessageShim TranslateToShim(GenericCommand command)
        {
            Tools.Commands.Implementation.IF1.AddAddonMP.AddAddonMP addon = new AddAddonMP();
            req req = new req();
            addon.req = req;

            cid c = new cid();
            req.cid = c;
            c.Item = command.TisTDId.ToString();
            c.ItemElementName = ItemChoiceType.TISTDid;

            req.reqId = command.ReqId.ToString();
            req.reqTime = command.ReqTime;
            req.phoneNumber = command.PhoneNumber;
            req.TISwalletId = command.TisWalletId;

            if (ErrorTrap.AddAssertion(command.MarketingPackages.Count == 1, "There should be exactly one  marketing package in this command.") &&
    ErrorTrap.AddAssertion(command.MarketingPackages[0].MPType.ToUpper() == "ADDON",
    "One and only ADDON marketing package is allowed and required for this command."))
            {
                MarketingPackage mp = command.MarketingPackages[0];

                if (mp.MPId.HasValue)
                {

                    AddonMP amp = new AddonMP { id = mp.MPId.ToString() };

                    List<@params> parameters = new List<@params>();

                    foreach (PackageParameter pp in mp.Parameters)
                    {
                        // Skip parameters with product code of N/A. That is taken from Milorad's code.
                        if (pp.ProductCode.ToUpper() == "N/A")
                            continue;

                        parameters.Add(new @params
                        {
                            code = (pp.ParamCode == "N/A") ? String.Empty : pp.ParamCode,
                            productCode = pp.ProductCode,
                            value = pp.Value
                        });
                    }

                    amp.@params = parameters.ToArray();

                    req.AddonMP = amp;
                }
                else
                {
                    ErrorTrap.AddAssertion(false, String.Format("Marketing package ID is missing (MP_ID command field is null) External mp_instance_id is {0}.", mp.MPInstanceId));
                }
            }


            #region Reference code

            #endregion

            return new MessageShim
            {
                CorrelationId = command.ReqId.ToString(),
                Text = PrepareAndWrapMessageText(req)
            };
        }

        #endregion
    }
}