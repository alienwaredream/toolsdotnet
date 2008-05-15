using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Tools.Common.Utils
{
	/// <summary>
	/// The utility as  a helper to CultureInfo and formatting related issues. 
	/// </summary>
	public class FormatProviderUtility
	{

		public static DateTimeStyles DefaultDateTimeStyle
		{
			get
			{
				return DateTimeStyles.None;
			}
		}

		/// <summary>
		/// The default formatter that should be used application wide.
		/// </summary>
		public static CultureInfo DefaultFormatter
		{
			get
			{
				return CultureInfo.InvariantCulture;
			}
		}

		/// <summary>
		/// The default number style that is needed for TryParse methods. 
		/// </summary>
		public static NumberStyles DefaultNumberStyle
		{
			get
			{
				return NumberStyles.Any;
			}
		}
		
	}
}
