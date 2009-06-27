using System;
using Tools.Commands.Implementation;
using Tools.Core.Utils;
using System.Collections.Generic;
using Tools.Core.Asserts;
using System.Globalization;
using System.Xml.Schema;
using System.Xml;
using System.IO;

using Tools.Commands.Implementation.IF1.ModifyWallet;

namespace Tools.Commands.Translators
{
    public class ModifyWalletTranslator : TranslatorBase
    {
        public ModifyWalletTranslator()
        {
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllTypes.xsd", AppDomain.CurrentDomain.BaseDirectory + @"\IF1\xsd\AllTypes.xsd");
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/ModifyWallet.xsd", AppDomain.CurrentDomain.BaseDirectory + @"\IF1\xsd\ModifyWallet.xsd");
        }

        #region ICommand2MessageTranslator Members

        public override MessageShim TranslateToShim(GenericCommand command)
        {
            #region Reference code

            //ModifyWallet modifyWallet = new ModifyWallet();

            //com.telekomsrbija.foris.commandtypes.modifywallet.Req req =
            //    new com.telekomsrbija.foris.commandtypes.modifywallet.Req();
            //req.setReqId(request.getRequestId().toString());
            //req.setReqTime(BasicConverter.timestampToXMLGregorianCalendar(request.getRequestTime()));
            //req.setTISwalletId(request.getTisWalletId());
            //String billingCycle = (request.getBillingCycle() == null
            //        ? null : String.valueOf(request.getBillingCycle()));
            //req.setBillingCycle(billingCycle);
            //req.setTaxGroup(request.getTaxGroup());

            #endregion 

            ModifyWallet modifyWallet = new ModifyWallet();

            req req = new req();
            modifyWallet.req = req;


            req.reqId = command.ReqId.ToString();
            req.reqTime = command.ReqTime;
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