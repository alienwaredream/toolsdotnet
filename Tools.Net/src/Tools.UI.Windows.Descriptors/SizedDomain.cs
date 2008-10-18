using System;
using System.Collections.Generic;
using System.Text;

using Tools.Core;

namespace Tools.UI.Windows.Descriptors
{
	class SizedDomain : Descriptor
	{
		#region Global declarations

		private int _width = 30;

		#endregion Global declarations

		#region Properties

		public int Width
		{
			get { return _width; }
			set { _width = value; }
		}

		#endregion Properties

		#region Constructors

		public SizedDomain
		(
		string name,
		string description,
		int width
		)
			:
		base
		(
		name,
		description
			)
		{
			_width = width;
		}

		#endregion Constructors
	}
}
