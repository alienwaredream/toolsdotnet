using System;
using Tools.Commands.Implementation;
using Tools.Core.Utils;
using System.Collections.Generic;
using Tools.Core.Asserts;
using System.Globalization;
using System.Xml.Schema;
using System.Xml;
using System.IO;
using Tools.Commands.Implementation.IF1.ModifyTerminalDevice;

namespace Tools.Commands.Translators
{
    public class ModifyTerminalDeviceTranslator : TranslatorBase
    {

        public ModifyTerminalDeviceTranslator()
        {
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllTypes.xsd", AppDomain.CurrentDomain.BaseDirectory + @"\IF1\xsd\AllTypes.xsd");
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/ModifyTerminalDevice.xsd", AppDomain.CurrentDomain.BaseDirectory + @"\IF1\xsd\ModifyTerminalDevice.xsd");
        }

        #region ICommand2MessageTranslator Members

        public override MessageShim TranslateToShim(GenericCommand command)
        {
            #region Reference code

            //            ModifyTerminalDevice modifyTerminalDevice = new ModifyTerminalDevice();

            //com.telekomsrbija.foris.commandtypes.modifyterminaldevice.Req req = 
            //    new com.telekomsrbija.foris.commandtypes.modifyterminaldevice.Req();

            //req.setTISTdid(request.getTisTerminalDeviceId());
            //req.setPhoneNumber(request.getTdElements().getPhoneNumber());
            //req.setReqId(request.getRequestId().toString());
            //req.setReqTime(
            //        BasicConverter.timestampToXMLGregorianCalendar(request.getRequestTime()));
            //req.setContractEnd(
            //        BasicConverter.timestampToXMLGregorianCalendar(request.getRequestTime()).toString());
            //req.setMonthlyLimit(request.getMonthlyLimit());
            //req.setTISwalletId(request.getTisWalletId());

            #endregion

            ModifyTerminalDevice modifyTerminalDevice = new ModifyTerminalDevice();

            req req = new req();
            modifyTerminalDevice.req = req;

            req.reqId = command.ReqId.ToString(CultureInfo.InvariantCulture);
            req.phoneNumber = command.PhoneNumber;

            if (!String.IsNullOrEmpty(command.TisWalletId))
            {
                req.TISwalletId = command.TisWalletId.Trim();
            }


            req.TISTDid = command.TisTDId.ToString();

            if (command.ContractEndDate.HasValue)
            {
                req.contractEnd = command.ContractEndDate.Value.ToString("yyyy-MM-ddTHH:mm:ss");
            }
            if (command.MonthlyLimit.HasValue)
            {
                req.monthlyLimit = Convert.ToDouble(command.MonthlyLimit.Value);
                req.monthlyLimitSpecified = true;
            }

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