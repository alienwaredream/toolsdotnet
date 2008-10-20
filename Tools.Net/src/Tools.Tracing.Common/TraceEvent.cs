using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Xml.Serialization;
using Tools.Core.Context;
using Tools.Core.Utils;

namespace Tools.Tracing.Common
{
    /// <summary>
    /// As an interim solution for Xml serializability ErrorEvent in XmlInclude is
    /// referenced here and is located in the same assembly. That will be subject to change (SD).
    /// After some elaboration this class is transformed to none abstract giving a chance for 
    /// default instances existance with the type of the ApplicationEventType.Info
    /// </summary>
    [Serializable]
    public class TraceEvent
    {
        #region Global Declaration

        private readonly EventCategory _category =
            EventCategory.None;

        /// <summary>
        /// ApplicationLifeCycleType.Runtime by default
        /// </summary>
        private readonly ApplicationLifeCycleType _lifeCycleType =
            ApplicationLifeCycleType.Runtime;

        private TraceEventType _type =
            TraceEventType.Verbose;

        #endregion Global Declaration

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public object EventId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public string EventIdText { get; set; }

        /// <summary>
        /// Message with event description.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public virtual TraceEventType Type
        {
            get { return _type; }
            set
            {
                // present only for serialization needs.
                _type = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public virtual ApplicationLifeCycleType LifeCycleType
        {
            get { return _lifeCycleType; }
            set { }
        }

        [XmlAttribute]
        public virtual EventCategory Category
        {
            get { return _category; }
            set { }
        }

        /// <summary>
        /// 
        /// </summary>
        public TraceEventLocation Location { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TraceEventPrincipal Principal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlAttribute]
        public DateTime Time { get; set; }

        public object Context { get; set; }

        /// <summary>
        /// Set is left here due to xml serializability.
        /// </summary>
        public ContextIdentifier ContextIdentifier { get; set; }

        /// <summary>
        /// A flag set if event has passed through the handling chain.
        /// </summary>
        [XmlAttribute]
        public bool Handled { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Public only due to the Xml serialization
        /// </summary>
        public TraceEvent()
        {
            Time = DateTime.UtcNow;
            Principal = GetPrincipal();
            Location = GetLocation(7);
        }

        public TraceEvent
            (
            object eventId,
            string message
            )
            : this
                (
                eventId,
                TraceEventType.Verbose,
                message,
                new ContextIdentifier(),
                null
                )
        {
        }

        public TraceEvent
            (
            object eventId,
            TraceEventType type,
            string message
            )
            : this
                (
                eventId,
                type,
                message,
                new ContextIdentifier(),
                null
                )
        {
        }

        public TraceEvent
            (
            object eventId,
            TraceEventType type,
            string message,
            ContextIdentifier contextIdentifier
            )
            : this
                (
                eventId,
                type,
                message,
                contextIdentifier,
                null
                )
        {
        }

        public TraceEvent(
            object eventId,
            string message,
            ContextIdentifier contextIdentifier
            )
            : this
                (
                eventId,
                TraceEventType.Verbose,
                message,
                contextIdentifier,
                null
                )
        {
        }

        public TraceEvent(
            object eventId,
            string message,
            ContextIdentifier contextIdentifier,
            object context
            )
            : this
                (
                eventId,
                TraceEventType.Verbose,
                message,
                contextIdentifier,
                context
                )
        {
        }

        public TraceEvent(
            object eventId,
            TraceEventType type,
            string message,
            ContextIdentifier contextIdentifier,
            object context
            )
            : this
                (
                eventId,
                type,
                ApplicationLifeCycleType.Runtime,
                message,
                contextIdentifier,
                context
                )
        {
        }

        public TraceEvent(
            object eventId,
            TraceEventType type,
            ApplicationLifeCycleType lifeCycleType,
            string message,
            ContextIdentifier contextIdentifier,
            object context
            )
            : this
                (
                eventId,
                type,
                lifeCycleType,
                EventCategory.None,
                message,
                contextIdentifier,
                context
                )
        {
        }

        public TraceEvent(
            object eventId,
            TraceEventType type,
            ApplicationLifeCycleType lifeCycleType,
            EventCategory category,
            string message,
            ContextIdentifier contextIdentifier,
            object context
            ) : this()
        {
            try
            {
                EventIdText = eventId.ToString();
                EventId = Convert.ToInt32(eventId);
            }
            catch (Exception e)
            {
                EventId = 0;
                // TODO: Add loggin of that as a warning! (SD)
            }
            _type = type;
            _lifeCycleType = lifeCycleType;

            // This conversion is done here in order to de-couple all
            // possible enumeration types that can be encountered here.
            // This is done according to the fact that other app domains
            // would need to know them then, and it is not the point.
            Message = (string.IsNullOrEmpty(message)) ? eventId.ToString() : message;

            ContextIdentifier = contextIdentifier ?? new ContextIdentifier();
            _category = category;
            Context = context;
        }

        #endregion Constructors

        #region Functions

        private TraceEventPrincipal GetPrincipal()
        {
            // TODO: the bellow to be refactored to the utility so this call will be only once
            // for the AppDomain
            // For dotRez we can think about using of the GenericPrincipal dotRez uses.
            AppDomain myDomain = Thread.GetDomain();
            Thread.GetDomain().SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

            // WindowsPrincipal myPrincipal = (WindowsPrincipal)Thread.CurrentPrincipal;

            return new TraceEventPrincipal(Thread.CurrentPrincipal.Identity.Name);
        }

        private TraceEventLocation GetLocation(int stackDelta)
        {
#warning Subject to review ASAP!! (SD) The bellow is very interim!
            // TODO: Analyze the robustness of the bellow and how
            // does it work once the transitive exception is being 
            // added to or removed from the bubbling hierachy.
            MethodInfo mi = null;
//			if (TraceEventHandlerManager.Instance.LocationDetailLevel==LocationDetailLevel.High)
//			{
//				mi = StackTraceUtility.GetStackEntryMethodInfo(stackDelta);
//			}


            return new TraceEventLocation(
                String.Empty, //TraceEventHandlerManager.Instance.GetBaseSourceName(),
                (mi == null) ? String.Empty : mi.Name,
                (mi == null) ? String.Empty : mi.ReflectedType.AssemblyQualifiedName,
                Environment.MachineName,
                Process.GetCurrentProcess().Id.ToString(),
                AppDomain.CurrentDomain.FriendlyName,
                AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                Thread.CurrentThread.Name
                );
        }

        #endregion Functions

        #region Overrides

        // TODO: think if this should not be a different method (SD)
        public override string ToString()
        {
            return SerializationUtility.Serialize2String(this);
        }

        public string ToString(IEventFormatter formatter)
        {
            return formatter.Format(this);
        }

        #endregion
    }
}