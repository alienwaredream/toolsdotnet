using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Tools.Common
{

	public class ValueTypeBaseComparer<T> : IComparer<T> where T : struct, IComparable
	{
		private OrderDirection direction;

		public OrderDirection Direction
		{
			get { return this.direction; }
		}

		public ValueTypeBaseComparer(OrderDirection direction)
		{
			this.direction = direction;
		}
		#region IComparer<T> Members


		public int Compare(T x, T y)
		{
			if (this.Direction == OrderDirection.None || this.Direction == OrderDirection.Ascending)
			{
				return compare(x, y);
			}
			return compare(x, y) * -1;
		}

		private int compare(T x, T y)
		{
			return x.CompareTo(y);
		}

		#endregion
	}
}
