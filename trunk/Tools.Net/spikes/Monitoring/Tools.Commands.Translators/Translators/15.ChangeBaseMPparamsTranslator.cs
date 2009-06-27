using System;
using Tools.Commands.Implementation;
using Tools.Core.Utils;
using System.Collections.Generic;
using Tools.Core.Asserts;
using System.Globalization;
using System.Xml.Schema;
using System.Xml;
using System.IO;

using Tools.Commands.Implementation.IF1.ChangeBaseMPParams;

namespace Tools.Commands.Translators
{
    public class ChangeBaseMPparamsTranslator : TranslatorBase
    {
        public ChangeBaseMPparamsTranslator()
        {
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllTypes.xsd", AppDomain.CurrentDomain.BaseDirectory + @"\IF1\xsd\AllTypes.xsd");
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/ChangeBaseMPparams.xsd", AppDomain.CurrentDomain.BaseDirectory + @"\IF1\xsd\ChangeBaseMPparams.xsd");
        }
        #region ICommand2MessageTranslator Members

        public override MessageShim TranslateToShim(GenericCommand command)
        {
            ChangeBaseMP cBMP = new ChangeBaseMP();
            req req = new req();
            cBMP.req = req;

            #region Reference code for mapping

            //com.telekomsrbija.foris.commandtypes.changebasempparams.Req req = 
            //    new com.telekomsrbija.foris.commandtypes.changebasempparams.Req();

            //req.setTISTdid(request.getTisTerminalDeviceId());
            //req.setPhoneNumber(request.getTdElements().getPhoneNumber());
            //req.setReqId(request.getRequestId().toString());
            //req.setReqTime(
            //        BasicConverter.timestampToXMLGregorianCalendar(request.getRequestTime()));

            //com.telekomsrbija.foris.commandtypes.changebasempparams.BaseMP baseMP = 
            //    new com.telekomsrbija.foris.commandtypes.changebasempparams.BaseMP();

            //for (MPInstance mpInstance : request.getMarketingPackages()) {
            //    BaseMPInstance b = (BaseMPInstance) mpInstance;
            //    //baseMP.setId(new BigInteger(b.getMarketingPackageId().toString()));

            //    for (Param param : b.getParams()) {
            //        com.telekomsrbija.foris.commandtypes.alltypes.Params p = 
            //            new com.telekomsrbija.foris.commandtypes.alltypes.Params();
            //        p.setCode((param.getPk().getParamCode().equals("N/A") 
            //                ? "" : param.getPk().getParamCode()));
            //        p.setProductCode(param.getPk().getProductCode());
            //        p.setValue(param.getValue());

            //        baseMP.getParams().add(p);
            //    }
            //}

            //req.setBaseMP(baseMP);

            //changeBaseMP.setReq(req);

            #endregion

            req.reqId = command.ReqId.ToString();
            req.reqTime = command.ReqTime;
            req.phoneNumber = command.PhoneNumber;
            req.TISTDid = command.TisTDId.ToString();

            if (ErrorTrap.AddAssertion(command.MarketingPackages.Count == 1, "There should be exactly one base marketing package in this command.") &&
                ErrorTrap.AddAssertion(command.MarketingPackages[0].MPType.ToUpper() == "BASE",
                "One and only BASE marketing package is allowed for this command."))
            {
                MarketingPackage mp = command.MarketingPackages[0];

                List<@params> parameters = new List<@params>();

                foreach (PackageParameter pp in mp.Parameters)
                {
                    // Skip parameters with product code of N/A.
                    if (pp.ProductCode.ToUpper() == "N/A")
                        continue;

                    parameters.Add(new @params
                    {
                        code = (pp.ParamCode == "N/A") ? String.Empty : pp.ParamCode,
                        productCode = pp.ProductCode,
                        value = pp.Value
                    });
                }

                req.BaseMP = parameters.ToArray();
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