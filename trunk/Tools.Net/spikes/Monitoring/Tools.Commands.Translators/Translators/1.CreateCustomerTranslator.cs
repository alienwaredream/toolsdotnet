using System;
using Tools.Commands.Implementation;
using Tools.Core.Utils;
using System.Collections.Generic;
using Tools.Core.Asserts;
using System.Globalization;
using System.Xml.Schema;
using System.Xml;
using System.IO;

using Tools.Commands.Implementation.IF1.CreateCustomer;

namespace Tools.Commands.Translators
{
    public class CreateCustomerTranslator : TranslatorBase
    {
        public CreateCustomerTranslator()
        {
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/AllTypes.xsd", @"IF1\xsd\AllTypes.xsd");
            Schemas.Add("http://www.tibco.com/schemas/SDPRO_Observer/Observer/SharedResources/XSD/IF1/CreateCustomer.xsd", @"IF1\xsd\CreateCustomer.xsd");
        }

        #region ICommand2MessageTranslator Members

        public override MessageShim TranslateToShim(GenericCommand command)
        {


            //req.setCustomerType(request.getTisCustomerType());
            //req.setName(request.getName());
            //req.setReqId(request.getRequestId().toString());
            //req.setReqTime(BasicConverter.timestampToXMLGregorianCalendar(request.getRequestTime()));
            //req.setTaxGroup(request.getTaxGroup());
            //req.setTISCustomerId(request.getTisCustomerId());
            //req.setTISwalletId(request.getTisWalletId());

            CreateCustomer createCustomer = new CreateCustomer();
            createCustomer.req = new Tools.Commands.Implementation.IF1.CreateCustomer.req();

            if (command.BillingCycle.HasValue)
            {
                createCustomer.req.billingCycle = command.BillingCycle.ToString();
            }

            createCustomer.req.reqId = command.ReqId.ToString();
            createCustomer.req.reqTime = command.ReqTime;
            createCustomer.req.taxGroup = command.TaxGroup;
            createCustomer.req.TIScustomerId = command.TisCustomerId;
            createCustomer.req.TISwalletId = command.TisWalletId;
            createCustomer.req.customerType = (customerType)Enum.Parse(typeof(customerType), command.CustomerType);
            createCustomer.req.name = command.Name;

            return new MessageShim
            {
                CorrelationId = command.ReqId.ToString(),
                Text = PrepareAndWrapMessageText(createCustomer.req)
            };
        }

        #endregion
    }
}