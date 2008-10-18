using System;
using System.Collections;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

using Tools.Core;

namespace Tools.Tracing.UI
{
	/// <summary>
	/// Summary description for FilterEntry.
	/// </summary>
	[Serializable()]
	public class FilterEntry : Descriptor, IEnabled, IChangeEventRaiser
	{
		private bool _enabled = false;
		/// <summary>
		/// Regular expression to compare with.
		/// </summary>
		private string _expression;
		/// <summary>
		/// Property graph path. 
		/// No generic use of this for current iteration.
		/// </summary>
		private string _path;

		private Regex _regExpression = null;
		/// <summary>
		/// Regular expression to compare with.
		/// </summary>	
		[Description("Regular expression to compare with. Iteration 0.")]		
		[XmlAttribute()]
		public string Expression
		{
			get
			{
				return _expression;
			}
			set
			{
				_expression = value;
				// Clear regex so it is recalculated next time when required.
				_regExpression = null;
				OnChanged();
			}
		}
		
		/// <summary>
		/// Property graph path. 
		/// No generic use of this for current iteration.
		/// Example: 
		/// Type.Property1.Property2 -> [Type]?Property1Property2
		/// </summary>
		[ReadOnly(true)]
		[Description(" Property graph path. Iteration 0.")]
		[XmlAttribute()]
		public string Path
		{
			get
			{
				return _path;
			}
			set
			{
				_path = value;
				OnChanged();
			}
		}
		[Browsable(false)]
		[XmlIgnore()]
		public Regex RegExpression
		{
			get
			{
				if (_regExpression!=null) return _regExpression;
				_regExpression = new Regex
					(
					Expression, 
					RegexOptions.Compiled|RegexOptions.IgnoreCase
					);
				return _regExpression;
			}
		}
		public FilterEntry()
		{

		}
		public FilterEntry(string propertyPath)
		{
			_path = propertyPath;
		}
		public bool Test(string value)
		{
			if (value==null) return false;

			if (_expression==null||_expression==String.Empty) return true;
			
			return RegExpression.IsMatch
				(
				value
				);
		}
		private void OnEnabledChanged()
		{
			if (EnabledChanged!=null)
			{
				EnabledChanged(this, EventArgs.Empty);
				OnChanged();
			}
		}
		
		#region IChangeEventRaiser Members

		private void OnChanged()
		{
			if (Changed!=null) Changed(this, EventArgs.Empty);
		}

		public event System.EventHandler Changed;

		#endregion

		#region IEnabled Members

		public event System.EventHandler EnabledChanged;

		[XmlAttribute()]
		public bool Enabled
		{
			get
			{
				
				return _enabled;
			}
			set
			{
				if (_enabled == value) return;

				_enabled = value;
				OnEnabledChanged();
				OnChanged();
			}
		}

		#endregion
	}
}
