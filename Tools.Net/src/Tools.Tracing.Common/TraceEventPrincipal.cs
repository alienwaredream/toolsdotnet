using System;

namespace Tools.Tracing.Common
{
	/// <summary>
	/// Summary description for TraceEventPrincipal.
	/// </summary>
	[Serializable()]
	public class TraceEventPrincipal
	{
		private string _name	= null;		

		/// <summary>
		/// Principal name.
		/// </summary>
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
		
		public TraceEventPrincipal()
		{
		}
		public TraceEventPrincipal(string name)
			: this()
		{
			_name = name;
		}


	}
}
