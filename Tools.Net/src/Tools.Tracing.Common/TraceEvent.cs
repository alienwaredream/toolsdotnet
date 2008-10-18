using System;
using System.Threading;
using System.Reflection;
using System.Xml.Serialization;
using System.Security.Principal;
using Tools.Core.Context;
using Tools.Core.Utils;
using System.Diagnostics;

namespace Tools.Tracing.Common
{
	/// <summary>
	/// As an interim solution for Xml serializability ErrorEvent in XmlInclude is
	/// referenced here and is located in the same assembly. That will be subject to change (SD).
	/// After some elaboration this class is transformed to none abstract giving a chance for 
	/// default instances existance with the type of the ApplicationEventType.Info
	/// </summary>
	[Serializable()]
	public class TraceEvent
	{
		#region Global Declaration

		private object								_eventId			= null;
		private string								_eventIdText		= null;
		private TraceEventType				_type				=
            TraceEventType.Verbose;
		/// <summary>
		/// ApplicationLifeCycleType.Runtime by default
		/// </summary>
		private ApplicationLifeCycleType			_lifeCycleType		= 
			ApplicationLifeCycleType.Runtime;
		private EventCategory						_category		=
			EventCategory.None;
		private string								_message			= null;
		private TraceEventLocation			_location			= null;
		private TraceEventPrincipal			_principal			= null;
		private DateTime							_time;
		private ContextIdentifier					_contextIdentifier	= null;
		private object								_context			= null;
		private bool								_handled			= false;
	
		#endregion Global Declaration

		#region Properties
		/// <summary>
		/// 
		/// </summary>
		public object EventId
		{
			get
			{
				return _eventId;
			}
			set
			{
				_eventId = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[XmlAttribute()]
		public string EventIdText
		{
			get
			{
				return _eventIdText;
			}
			set
			{
				_eventIdText = value;
			}
		}
		/// <summary>
		/// Message with event description.
		/// </summary>
		public string Message
		{
			get
			{
				return _message;
			}
			set
			{
				_message = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[XmlAttribute()]
		public virtual TraceEventType Type
		{
			get
			{
				return _type;
			}
			set
			{
				// present only for serialization needs.
				_type = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[XmlAttribute()]
		public virtual ApplicationLifeCycleType	LifeCycleType
		{
			get
			{
				return _lifeCycleType;
			}
			set
			{
			}
		}
		[XmlAttribute()]
		public virtual EventCategory Category
		{
			get
			{
				return _category;
			}
			set
			{
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public TraceEventLocation Location
		{
			get
			{
				return _location;
			}
			set
			{
				_location = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public TraceEventPrincipal Principal
		{
			get
			{
				return _principal;
			}
			set
			{
				_principal = value;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		[XmlAttribute()]
		public DateTime Time
		{
			get
			{
				return _time;
			}
			set
			{
				_time = value;
			}
		}
		public object Context
		{
			get
			{
				return _context;
			}
			set
			{
				_context = value;
			}
		}
		/// <summary>
		/// Set is left here due to xml serializability.
		/// </summary>
		public ContextIdentifier ContextIdentifier
		{
			get
			{
				return _contextIdentifier;
			}
			set
			{
				_contextIdentifier = value;
			}
		}
		/// <summary>
		/// A flag set if event has passed through the handling chain.
		/// </summary>
		[XmlAttribute()]
		public bool Handled
		{
			get
			{
				return _handled;
			}
			set
			{
				_handled = value;
			}
		}
		#endregion Properties

		#region Constructors
		/// <summary>
		/// Public only due to the Xml serialization
		/// </summary>
		public TraceEvent()
		{
			_time = DateTime.UtcNow;
			_principal	= GetPrincipal();
			_location = GetLocation(7);
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
			): this()
		{
			try
			{
				_eventIdText	= eventId.ToString();
				_eventId		= Convert.ToInt32(eventId);
			}
			catch (Exception e)
			{
				_eventId	= 0;
				// TODO: Add loggin of that as a warning! (SD)
			}
			_type		= type;
			_lifeCycleType = lifeCycleType;

			// This conversion is done here in order to de-couple all
			// possible enumeration types that can be encountered here.
			// This is done according to the fact that other app domains
			// would need to know them then, and it is not the point.
			_message	= (string.IsNullOrEmpty(message)) ? eventId.ToString() : message;

			_contextIdentifier = contextIdentifier ?? new ContextIdentifier();
			_category = category;
			_context = context;
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
						(mi==null) ? String.Empty :  mi.Name, 
						(mi==null) ? String.Empty : mi.ReflectedType.AssemblyQualifiedName, 
						System.Environment.MachineName, 
						System.Diagnostics.Process.GetCurrentProcess().Id.ToString(),
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
