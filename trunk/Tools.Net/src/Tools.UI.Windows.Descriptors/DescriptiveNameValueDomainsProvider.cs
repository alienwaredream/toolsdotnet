using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using Tools.Core;
using Tools.Core.Utils;

namespace Tools.UI.Windows.Descriptors
{
	public class DescriptiveNameValueDomainsProvider : IDomainsProvider<DescriptiveNameValue<string>>
	{
		#region Globals
		private IDictionary _markValues;
		private MarksPresentationType _marksPresentationType = MarksPresentationType.Encoded;

		#endregion

		#region Properties

        public IDictionary MarkValues
		{
			get { return _markValues; }
			set { _markValues = value; }
		}
		public MarksPresentationType MarksPresentationType
		{
			get { return _marksPresentationType; }
			set { _marksPresentationType = value; }
		}

		#endregion

		#region IDomainsProvider Members

		public string[] GetDomainValues(DescriptiveNameValue<string> dnv)
		{
			return new string[3]
			{
				dnv.Name,
				dnv.Description,
				getValue(dnv.Value)
			};
		}

		private string getValue(string source)
		{
			if (_marksPresentationType == MarksPresentationType.Encoded)
				return source;
            //string dateTimeDecodedValue = ScriptParams.DecodePathTimeMarks(source, DateTime.UtcNow);
            //return ScriptParams.ParseToString
            //(
            //this._markValues,
            //dateTimeDecodedValue
            //);
		    return null;
		}
		public string[] GetDomainNames()
		{
			return new string[3]
			{
				"Name",
				"Description",
				"Value"
			};
		}
		public DescriptiveNameValueDomainsProvider()
			: this(new Hashtable())
		{
		}
		public DescriptiveNameValueDomainsProvider
			(
			IDictionary markValues
			)
		{
			_markValues = markValues;
		}
		public DescriptiveNameValue<string> GetNewDefaultInstance()
		{
			return new DescriptiveNameValue <string>
			(
				"Name",
				"Value",
				"Description"
			);
		}
		#endregion
}
}
