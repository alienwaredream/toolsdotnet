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
    public class TerminateAddonTDLevelMPTranslator : TranslatorBase
    {
        public TerminateAddonTDLevelMPTranslator()
        {
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllTypes.xsd", @"IF1\xsd\AllTypes.xsd");
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/TerminateAddonMP.xsd", @"IF1\xsd\TerminateAddonMP.xsd");
        }

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
            terminateAddon.req.TISwalletId = command.TisWalletId;

            //MP mp = new MP();
            MP marketingPackage = new MP();

            //for (MPInstance mpInstance : request.getMarketingPackages()) {
            //    AddonInstance a = (AddonInstance) mpInstance;
            //    mp.setId(new BigInteger(a.getMarketingPackageId().toString()));
            //}

            if (ErrorTrap.AddAssertion(command.MarketingPackages.Count == 1, "There should be exactly one  marketing package in this command.") &&
    ErrorTrap.AddAssertion(command.MarketingPackages[0].MPType.ToUpper() == "ADDON",
    "One and only ADDON marketing package is allowed and required for this command."))
            {
                MarketingPackage mpInstance = command.MarketingPackages[0];

                if (mpInstance.MPId.HasValue)
                {
                    marketingPackage.id = mpInstance.MPId.ToString();
                    //req.setMP(mp);
                    terminateAddon.req.MP = marketingPackage;
                }
                else
                {
                    ErrorTrap.AddAssertion(false, String.Format("Marketing package ID is missing (MP_ID command field is null) External mp_instance_id is {0}.", mpInstance.MPInstanceId));
                }
            }


            return new MessageShim
            {
                CorrelationId = command.ReqId.ToString(),
                Text = PrepareAndWrapMessageText(terminateAddon.req)
            };
        }

        #endregion
    }
}