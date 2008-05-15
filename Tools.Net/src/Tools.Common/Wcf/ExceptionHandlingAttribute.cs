using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Tools.Common.Wcf
{


    #region WorkflowFireEventAttribute
    /// <summary>
    /// Operation to fire a workflow event
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ExceptionHandlingAttribute : Attribute, IOperationBehavior
    {

        #region Constructors
        public ExceptionHandlingAttribute()
        {
        }
        #endregion

        #region IOperationBehavior Members

        public void ApplyDispatchBehavior(OperationDescription description, DispatchOperation dispatch)
        {
            if (dispatch.Invoker is ExceptionHandlingOperationInvoker)
                return;

            dispatch.Invoker = new ExceptionHandlingOperationInvoker(dispatch.Invoker);
        }
        public void AddBindingParameters(OperationDescription description, BindingParameterCollection parameters) { }
        public void ApplyClientBehavior(OperationDescription description, ClientOperation proxy) { }
        public void Validate(OperationDescription description) { }
        #endregion
    }
    #endregion


}