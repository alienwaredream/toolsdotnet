using System;
using Tools.Commands.Implementation;
using Tools.Core.Utils;
using System.Collections.Generic;
using Tools.Core.Asserts;
using System.Globalization;
using System.Xml.Schema;
using System.Xml;
using System.IO;
using Tools.Commands.Implementation.IF1.ChangeSIMCard;

namespace Tools.Commands.Translators
{
    public class ChangeSIMCardTranslator : TranslatorBase
    {

        public ChangeSIMCardTranslator()
        {
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllTypes.xsd", @"IF1\xsd\AllTypes.xsd");
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/ChangeSIMCard.xsd", @"IF1\xsd\ChangeSIMCard.xsd");
        }

        #region ICommand2MessageTranslator Members

        public override MessageShim TranslateToShim(GenericCommand command)
        {
            #region Reference code

//            ChangeSIMCard changeSIMCard = new ChangeSIMCard();

//            com.telekomsrbija.foris.commandtypes.changesimcard.Req req =
//                new com.telekomsrbija.foris.commandtypes.changesimcard.Req();

//            req.setTISTdid(request.getTisTerminalDeviceId());
//            req.setPhoneNumber(request.getTdElements().getPhoneNumber());
//            req.setReqId(request.getRequestId().toString());
//            req.setReqTime(
//                    BasicConverter.timestampToXMLGregorianCalendar(request.getRequestTime()));
//            req.setNewICCID(request.getNewIccid());

            #endregion

            ChangeSIMCard changeSIMCard = new ChangeSIMCard();

            req req = new req();

            changeSIMCard.req = req;

            req.reqId = command.ReqId.ToString(CultureInfo.InvariantCulture);
            req.phoneNumber = command.PhoneNumber;
            req.TISTDid = command.TisTDId.ToString();

            req.newICCID = command.NewIccId;
            req.reqTime = command.ReqTime;

            return new MessageShim
            {
                CorrelationId = command.ReqId.ToString(),
                Text = PrepareAndWrapMessageText(req)
            };
        }

        #endregion
    }
}