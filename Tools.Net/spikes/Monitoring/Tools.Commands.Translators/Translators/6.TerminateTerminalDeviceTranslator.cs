using System;
using Tools.Commands.Implementation;
using Tools.Core.Utils;
using System.Collections.Generic;
using Tools.Core.Asserts;
using System.Globalization;
using System.Xml.Schema;
using System.Xml;
using System.IO;
using Tools.Commands.Implementation.IF1.TerminateTerminalDevice;

namespace Tools.Commands.Translators
{
    public class TerminateTerminalDeviceTranslator : TranslatorBase
    {

        public TerminateTerminalDeviceTranslator()
        {
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllTypes.xsd", AppDomain.CurrentDomain.BaseDirectory + @"\IF1\xsd\AllTypes.xsd");
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/TerminateTerminalDevice.xsd", AppDomain.CurrentDomain.BaseDirectory + @"\IF1\xsd\TerminateTerminalDevice.xsd");
        }

        #region ICommand2MessageTranslator Members

        public override MessageShim TranslateToShim(GenericCommand command)
        {
            #region Reference code

//            TerminateTerminalDevice terminateTD = new TerminateTerminalDevice();

//            com.telekomsrbija.foris.commandtypes.terminateterminaldevice.Req req =
//                new com.telekomsrbija.foris.commandtypes.terminateterminaldevice.Req();

//            req.setPhoneNumber(request.getTdElements().getPhoneNumber());
//            req.setReqId(request.getRequestId().toString());
//            req.setReqTime(
//                    BasicConverter.timestampToXMLGregorianCalendar(request.getRequestTime()));
//            req.setTISTDid(request.getTisTerminalDeviceId());

            #endregion

            TerminateTerminalDevice terminateTerminalDevice = new TerminateTerminalDevice();

            req req = new req();
            terminateTerminalDevice.req = req;

            req.reqId = command.ReqId.ToString(CultureInfo.InvariantCulture);
            req.phoneNumber = command.PhoneNumber;
            req.TISTDid = command.TisTDId.ToString();
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