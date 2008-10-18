using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using Tools.Core;

namespace Tools.UI.Windows.Descriptors
{
	class GuiStyle : Descriptor
	{
		private System.Drawing.Font _defaultFont =
			new System.Drawing.Font
			(
			FontFamily.GenericSansSerif,
			8f,
			FontStyle.Regular
			);
		//private System.Drawing.Font _ont


		public GuiStyle
			(
			string name,
			string description
			)
			: base
			(
			name,
			description
			)
		{
		}
	}
}
