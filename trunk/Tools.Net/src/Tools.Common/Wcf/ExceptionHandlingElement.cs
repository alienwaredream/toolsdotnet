using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel.Configuration;
using System.Configuration;

namespace Tools.Common.Wcf
{
    public class ExceptionHandlingElement : BehaviorExtensionElement
    {
        #region BehaviorExtensionElement

        public override Type BehaviorType
        {
            get { return typeof(ExceptionHandlingBehavior); }
        }

        protected override object CreateBehavior()
        {
            return new ExceptionHandlingBehavior(ExceptionHandlingPolicyName);
        }

        #endregion

        #region Properties

        [ConfigurationProperty("exceptionHandlingPolicyName", IsRequired=true)]
        public string ExceptionHandlingPolicyName
        {
            get
            {
                return (string)base["exceptionHandlingPolicyName"];
            }
            set
            {
                base["exceptionHandlingPolicyName"] = value;
            }
        }


        #endregion
    }
}
