using System;
using Tools.Commands.Implementation;
using Tools.Commands.Implementation.IF1.Ctd;
using Tools.Core.Utils;
using System.Collections.Generic;
using Tools.Core.Asserts;
using System.Globalization;
using System.Xml.Schema;
using System.Xml;
using System.IO;

namespace Tools.Commands.Translators
{
    public class CreateTerminalDeviceTranslator : TranslatorBase
    {
        /// <summary>
        /// Discarding by default. If required, can be set to false via configuration.
        /// </summary>
        private bool discardP2P = true;

        public CreateTerminalDeviceTranslator()
        {
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllTypes.xsd", AppDomain.CurrentDomain.BaseDirectory + @"\IF1\xsd\AllTypes.xsd");
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/CreateTerminalDevice.xsd", AppDomain.CurrentDomain.BaseDirectory + @"\IF1\xsd\CreateTerminalDevice.xsd");
        }
        #region ICommand2MessageTranslator Members

        public override MessageShim TranslateToShim(GenericCommand command)
        {
            CreateTerminalDevice ctd = new CreateTerminalDevice();

            ctd.req = new req();

            ctd.req.reqId = command.ReqId.ToString(CultureInfo.InvariantCulture);
            ctd.req.TIScustomerId = command.TisCustomerId;
            ctd.req.TISTDid = command.TisTDId.ToString(CultureInfo.InvariantCulture);
            ctd.req.TISwalletId = command.TisWalletId;

            List<addOnMPs> addOnMPs = new List<addOnMPs>();
            bool isBaseMPSet = false;

            //TODO: (SD) can't use extension methods
            foreach (MarketingPackage mp in command.MarketingPackages)
            {
                #region Handle base package
                // Base package is discriminated by the BASE MPType
                // Only allow one base marketing package, more than one is an error.
                if (mp.MPType.ToUpper() == "BASE")
                {
                    if (ErrorTrap.AddAssertion(!isBaseMPSet, CommandValidationError.MoreThanOneMarketingPackageEncounteredForCommand))
                    {

                        isBaseMPSet = true;

                        if (mp.MPId.HasValue)
                        {

                            baseMP bmp = new baseMP { id = mp.MPId.ToString() };

                            List<@params> parameters = new List<@params>();

                            foreach (PackageParameter pp in mp.Parameters)
                            {
                                parameters.Add(new @params
                                {
                                    code = pp.ParamCode,
                                    productCode = pp.ProductCode,
                                    value = pp.Value
                                });
                            }

                            bmp.@params = parameters.ToArray();

                            ctd.req.baseMP = bmp;
                        }
                        else
                        {
                            ErrorTrap.AddAssertion(false, String.Format("Marketing package ID is missing (MP_ID command field is null) External mp_instance_id is {0}.", mp.MPInstanceId));
                        }
                    }

                    continue;
                } 
                #endregion
                // Base package is discriminated by the BASE MPType
                if (mp.MPType.ToUpper() == "ADDON")
                {
                    if (mp.MPId.HasValue)
                    {
                        addOnMPs aomp = new addOnMPs { id = mp.MPId.ToString() };

                        List<@params> parameters = new List<@params>();

                        foreach (PackageParameter pp in mp.Parameters)
                        {
                            parameters.Add(new @params
                            {
                                code = pp.ParamCode,
                                productCode = pp.ProductCode,
                                value = pp.Value
                            });
                        }

                        aomp.@params = parameters.ToArray();

                        addOnMPs.Add(aomp);

                        continue;
                    }
                    else
                    {
                        ErrorTrap.AddAssertion(false, String.Format("Marketing package ID is missing (MP_ID command field is null) External mp_instance_id is {0}.", mp.MPInstanceId));
                    }
                }

                ctd.req.addOnMPs = addOnMPs.ToArray();
            }
            if (command.ContractEndDate.HasValue)
            {
                ctd.req.contractEnd = command.ContractEndDate.Value;
                ctd.req.contractEndSpecified = true;
            }

            if (command.MonthlyLimit.HasValue)
            {
                ctd.req.monthlyLimit = Convert.ToDouble(command.MonthlyLimit.Value);
                ctd.req.monthlyLimitSpecified = true;
            }

            ctd.req.reqTIme = command.ReqTime;

            if (!String.IsNullOrEmpty(command.P2P))
            {
                ctd.req.p2p = command.P2P;
            }

            bool canSerialize = true;

            //if (ErrorTrap.AddAssertion(Enum.IsDefined(typeof(TDtype), command.TDType.ToString()),
            //    String.Format("Command TDType value {0} doesn't fall in the allowed range.", command.TDType)))
            //{

            ctd.req.TDtype = (TDtype)command.TDType;
            //}
            //else
            //{
            //    canSerialize = false;
            //}

            TDelements tdElements = new TDelements();

            //TODO: (SD) Find out if this is required actually or not
            if (!String.IsNullOrEmpty(command.IccId))
            {
                mobile m = new mobile();
                m.ICCID = command.IccId;
                m.phoneNumber = command.PhoneNumber;
                m.profile = command.VpnProfile.ToString();

                if (!String.IsNullOrEmpty(command.ShortNumber))
                {
                    m.shortNumber = command.ShortNumber;
                }

                tdElements.Item = m;
                ctd.req.TDelements = tdElements;
            }

            return new MessageShim
            {
                CorrelationId = command.ReqId.ToString(),
                Text = PrepareAndWrapMessageText(ctd.req)
            };
        }

        #endregion
    }
}