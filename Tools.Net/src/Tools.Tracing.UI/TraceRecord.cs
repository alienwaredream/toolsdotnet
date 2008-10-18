using System;

using Tools.Tracing.Common;
using Tools.Core.Context;

namespace Tools.Tracing.UI
{
	/// <summary>
	/// Summary description for TraceRecord.
	/// </summary>
	public class TraceRecord
	{
		private TraceEvent _e;

		public string Time
		{
			get
			{
				return _e.Time.ToString("yyyy-MM-dd HH-mm-ss (fff)");
			}
		}
        public string Guid
        {
            get
            {
                return _e.ContextIdentifier.ContextGuid.ToString();
            }
        }
		public string ContextHolderId
		{
			get
			{
				return _e.ContextIdentifier.ContextHolderId.ToString();
			}
		}
		public string InternalId
		{
			get
			{
				return _e.ContextIdentifier.InternalId.ToString();
			}
		}
		public string InternalParentId
		{
			get
			{
				return _e.ContextIdentifier.InternalParentId.ToString();
			}
		}
		public string ExternalId
		{
			get
			{
				return _e.ContextIdentifier.ExternalId.ToString();
			}
		}
		public string ExternalReference
		{
			get
			{
				return _e.ContextIdentifier.ExternalReference.ToString();
			}
		}
		public string EventId
		{
			get
			{
				return Convert.ToInt32(_e.EventId).ToString();
			}
		}
		public string EventName
		{
			get
			{
				return _e.EventIdText;
			}
		}
		public string EventType
		{
			get
			{
				return _e.Type.ToString();
			}
		}
		public string Message
		{
			get
			{
				return 
					(_e.Message == null) ? 
					null :
					((_e.Message.Length > 30) ? 
					_e.Message.Substring(0, 30) :
					_e.Message);
			}
		}


		public string PrincipalName
		{
			get
			{
				return _e.Principal.Name;
			}
		}
		public string HostName
		{
			get
			{
				return _e.Location.HostName;
			}
		}
		public string AppDomainName
		{
			get
			{
				return _e.Location.AppDomainName;
			}
		}
		public string ClassName
		{
			get
			{
				return _e.Location.ClassName;
			}
		}
		public string MethodName
		{
			get
			{
				return _e.Location.MethodName;
			}
		}
		public string ThreadName
		{
			get
			{
				return _e.Location.ThreadName;
			}
		}
        public string Source
        {
            get
            {
                return _e.Location.Source;
            }
        }
        public static string GetFieldValueByName(string traceFieldName, TraceEvent ae)
        {
            switch (traceFieldName)
            {
                case 	"Time" :
                    return new TraceRecord(ae).Time;
                case    "ContextHolderId" :
                    return new TraceRecord(ae).ContextHolderId;
                case    "InternalId" :
                    return new TraceRecord(ae).InternalId;
                case    "InternalParentId" :
                    return new TraceRecord(ae).InternalParentId;
                case    "ExternalId" :
                    return new TraceRecord(ae).ExternalId;
                case    "ExternalReference" :
                    return new TraceRecord(ae).ExternalReference;
                case    "EventId" :
                    return new TraceRecord(ae).EventId;
                case    "EventName" :
                    return new TraceRecord(ae).EventName;
                case    "EventType" :
                    return new TraceRecord(ae).EventType;
                case    "Message" :
                    return new TraceRecord(ae).Message;
                case    "ThreadName" :
                    return new TraceRecord(ae).ThreadName;
                case    "HostName":
                    return new TraceRecord(ae).HostName;
                case    "Guid":
                    return new TraceRecord(ae).Guid;
                case "Source":
                    return new TraceRecord(ae).Source;
                default:
                    throw new ArgumentException
                    (
                    "Can't map value of " + traceFieldName + "to any of the TraceEvent fields."
                    );
            }
        }
        public static string[] CorrelationFieldNames
        {
            get
            {
                return new string[]
                {
                        //"InternalId",
                        //"InternalParentId",
						"ExternalId",
						"ExternalReference",
						"EventId",
						"EventName",
						"EventType",
                        //"Message",
						"ThreadName",
                        "Guid"
                        //"HostName",
                        //"PrincipalName",
                        //"AppDomainName",
                        //"ClassName",
                        //"MethodName"
                };
            }
        }
        public static string[] CorrelationFieldXPathExpressions
        {
            get
            {
                return new string[]
                {
                        "child::TraceEvent[child::ContextIdentifier[ExternalId='{%Value}']]",
						"child::TraceEvent[child::ContextIdentifier[ExternalReference='{%Value}']]",
						"child::TraceEvent[EventId='{%Value}']",
						"child::TraceEvent[@EventIdText='{%Value}']",
						"child::TraceEvent[@Type='{%Value}']",
                        //"Message",
						"child::TraceEvent[child::Location[@ThreadName='{%Value}']]",
                        "child::TraceEvent[child::ContextIdentifier[@ContextGuid='{%Value}']]"
                        //"HostName",
                        //"PrincipalName",
                        //"AppDomainName",
                        //"ClassName",
                        //"MethodName"
                };
            }
        }
        public static string[] FieldNames
        {
            get
            {
                return new string[]
                {
						"Time",
						"CHId",
						"IId",
						"IPId",
						"EId",
						"ER",
						"EventId",
						"EventName",
						"EventType",
						"Message",
						"Thread",
						"Host",
						"Principal",
                        "Source",
                        "Guid"
                };
            }
        }
        public static int[] FieldUILengths
        {
            get
            {
                return new int[]
                {
						103,
						46,
						50,
						50,
						50,
						100,
						53,
						150,
						61,
						200,
						100,
						80,
						100,
                        60,
                        30
                };
            }
        }
		public string[] PropertiesArray
		{
			get
			{
				return new string[]
					{
						Time,
						ContextHolderId,
						InternalId,
						InternalParentId,
						ExternalId,
						ExternalReference,
						EventId,
						EventName,
						EventType,
						Message,
						ThreadName,
						HostName,
						PrincipalName,
                        Source,
                        Guid
					};
			}
		}
	
		public TraceRecord()
		{
			_e = new TraceEvent();
		}
		public TraceRecord(TraceEvent e) :
			this()
		{
			_e = e;
		}
	}
}
