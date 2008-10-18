using System;
using System.Collections.Generic;
using System.Text;

using Tools.Core;

namespace Tools.UI.Windows.Descriptors
{
	public delegate void ValueSelectedDelegate<T>
	(
		object sender,
		ValueSelectedEventArgs<T> e
	);

}
