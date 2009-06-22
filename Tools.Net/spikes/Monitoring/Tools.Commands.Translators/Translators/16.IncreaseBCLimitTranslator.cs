using System;
using Tools.Commands.Implementation;
using Tools.Core.Utils;
using System.Collections.Generic;
using Tools.Core.Asserts;
using System.Globalization;
using System.Xml.Schema;
using System.Xml;
using System.IO;

using Tools.Commands.Implementation.IF1.Ibc;

namespace Tools.Commands.Translators
{
    public class IncreaseBCLimitTranslator : TranslatorBase
    {
        public IncreaseBCLimitTranslator()
        {
            Schemas.Add("http://www.tibco.com/schemas/UsageBlocking/SharedResources/XSD/Schema.xsd", @"IF1\xsd\IncreaseBC.xsd");
        }
        #region ICommand2MessageTranslator Members

        public override MessageShim TranslateToShim(GenericCommand command)
        {
            InreaseBCLimit ibc = new  InreaseBCLimit();

            req req = new req();
            ibc.req = req;

            req.phoneNumber = command.PhoneNumber;
            req.amount = command.OnetimeLimitAmount;


            #region Reference code
            //            IncreaseBCLimitRequest request = 
            //    (IncreaseBCLimitRequest) getTransportObject().get("request");

            //InreaseBCLimit increaseBCLimit = new InreaseBCLimit();

            //com.telekomsrbija.foris.commandtypes.increasebc.Req req = 
            //    new com.telekomsrbija.foris.commandtypes.increasebc.Req();

            //req.setPhoneNumber(request.getTdElements().getPhoneNumber());

            //req.setAmount(request.getAmount());

            ///*req.setReqId(request.getRequestId().toString());
            //req.setReqTime(
            //        BasicConverter.timestampToXMLGregorianCalendar(request.getRequestTime()));*/

            //increaseBCLimit.setReq(req);

            //out.add("payload", req);
            //out.add("id", request.getRequestId().toString());
            //out.add("commandName", "IncreaseBCLimit");//increaseBCLimit.getClass().getSimpleName());


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