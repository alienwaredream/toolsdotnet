using System;
using Tools.Commands.Implementation;
using Tools.Core.Utils;
using System.Collections.Generic;
using Tools.Core.Asserts;
using System.Globalization;
using System.Xml.Schema;
using System.Xml;
using System.IO;

using Tools.Commands.Implementation.IF1.ModifyCustomer;

namespace Tools.Commands.Translators
{
    public class ModifyCustomerTranslator : TranslatorBase
    {
        public ModifyCustomerTranslator()
        {
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllTypes.xsd", @"IF1\xsd\AllTypes.xsd");
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/ModifyCustomer.xsd", @"IF1\xsd\ModifyCustomer.xsd");
        }
        #region ICommand2MessageTranslator Members

        public override MessageShim TranslateToShim(GenericCommand command)
        {
            #region Reference code

            //            ModifyCustomer modifyCustomer = new ModifyCustomer();

            //            com.telekomsrbija.foris.commandtypes.modifycustomer.Req req =
            //                new com.telekomsrbija.foris.commandtypes.modifycustomer.Req();
            //            req.setReqId(request.getRequestId().toString());
            //            req.setReqTime(BasicConverter.timestampToXMLGregorianCalendar(request.getRequestTime()));
            //            req.setName(request.getName());
            //            req.setTISCustomerId(request.getTisCustomerId());
            //
            //            modifyCustomer.setReq(req);

            #endregion

            ModifyCustomer modifyCustomer = new ModifyCustomer();

            req req = new req();
            modifyCustomer.req = req;


            req.reqId = command.ReqId.ToString();
            req.reqTime = command.ReqTime;
            req.TIScustomerId = command.TisCustomerId;
            req.name = command.Name;

            return new MessageShim
            {
                CorrelationId = command.ReqId.ToString(),
                Text = PrepareAndWrapMessageText(req)
            };
        }

        #endregion
    }
}