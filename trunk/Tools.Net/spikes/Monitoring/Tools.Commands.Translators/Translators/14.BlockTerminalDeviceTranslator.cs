using System;
using Tools.Commands.Implementation;
using Tools.Core.Utils;
using System.Collections.Generic;
using Tools.Core.Asserts;
using System.Globalization;
using System.Xml.Schema;
using System.Xml;
using System.IO;
using Tools.Commands.Implementation.IF1.BlockTerminalDevice;

namespace Tools.Commands.Translators
{
    public class BlockTerminalDeviceTranslator : TranslatorBase
    {

        public BlockTerminalDeviceTranslator()
        {
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllTypes.xsd", @"IF1\xsd\AllTypes.xsd");
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/BlockTerminalDevice.xsd", @"IF1\xsd\BlockTerminalDevice.xsd");
        }

        #region ICommand2MessageTranslator Members

        public override MessageShim TranslateToShim(GenericCommand command)
        {
            #region Reference code

//            BlockTerminalDeviceRequest request =
//                (BlockTerminalDeviceRequest)getTransportObject().get("request");

//            BlockTerminalDevice blockTerminalDevice = new BlockTerminalDevice();

//            com.telekomsrbija.foris.commandtypes.blockterminaldevice.Req req =
//                new com.telekomsrbija.foris.commandtypes.blockterminaldevice.Req();

//            req.setBlockReason(BlockReason.fromValue(request.getBlockReason()));
//            req.setBlockStatus(new BigInteger(request.getBlockStatus().toString()));
//            req.setPhoneNumber(request.getTdElements().getPhoneNumber());
//            req.setTISTdid(request.getTisTerminalDeviceId());
//
//            req.setReqId(request.getRequestId().toString());
//            req.setReqTime(
//                    BasicConverter.timestampToXMLGregorianCalendar(request.getRequestTime()));

            #endregion

            BlockTerminalDevice blockTerminalDevice = new BlockTerminalDevice();

            req req = new req();
            blockTerminalDevice.req = req;

            req.reqId = command.ReqId.ToString(CultureInfo.InvariantCulture);
            req.phoneNumber = command.PhoneNumber;
            req.TISTDid = command.TisTDId.ToString();
            req.reqTime = command.ReqTime;

            if (!String.IsNullOrEmpty(command.BlockReason))
            {
                req.blockReason = (blockReason)Enum.Parse(typeof(blockReason), command.BlockReason);
            }
            if (command.BlockStatus.HasValue)
            {
                req.blockStatus = command.BlockStatus.Value.ToString();
            }

            return new MessageShim
            {
                CorrelationId = command.ReqId.ToString(),
                Text = PrepareAndWrapMessageText(req)
            };
        }

        #endregion
    }
}