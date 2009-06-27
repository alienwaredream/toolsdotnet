using System;
using Tools.Commands.Implementation;
using Tools.Core.Utils;
using System.Collections.Generic;
using Tools.Core.Asserts;
using System.Globalization;
using System.Xml.Schema;
using System.Xml;
using System.IO;

using Tools.Commands.Implementation.IF1.CreateWallet;

namespace Tools.Commands.Translators
{
    public class CreateWalletTranslator : TranslatorBase
    {
        public CreateWalletTranslator()
        {
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllTypes.xsd", AppDomain.CurrentDomain.BaseDirectory + @"\IF1\xsd\AllTypes.xsd");
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/CreateWallet.xsd", AppDomain.CurrentDomain.BaseDirectory + @"\IF1\xsd\CreateWallet.xsd");
        }
        #region ICommand2MessageTranslator Members

        public override MessageShim TranslateToShim(GenericCommand command)
        {
            CreateWallet createWallet = new CreateWallet();

            req req = new req();
            createWallet.req = req;


            req.reqId = command.ReqId.ToString();
            req.reqTime = command.ReqTime;
            req.TIScustomerId = command.TisCustomerId;
            req.TISwalletId = command.TisWalletId;

            if (command.BillingCycle.HasValue)
            {
                req.billingCycle = command.BillingCycle.ToString();
            }

            req.taxGroup = command.TaxGroup;

            return new MessageShim
            {
                CorrelationId = command.ReqId.ToString(),
                Text = PrepareAndWrapMessageText(req)
            };
        }

        #endregion
    }
}