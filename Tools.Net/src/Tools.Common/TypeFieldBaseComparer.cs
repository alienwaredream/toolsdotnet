using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Tools.Common
{


	public class TypeFieldBaseComparer<T, P> : IComparer<T> 
		where T : class
		where P : IComparable
	{
		public delegate P PropertyInvoker(T source);

		private OrderDirection direction;
		private PropertyInvoker propertyInvoker;

		public OrderDirection Direction
		{
			get { return this.direction; }
		}

		public TypeFieldBaseComparer(OrderDirection direction, PropertyInvoker propertyInvoker)
		{
			this.direction = direction;
			this.propertyInvoker = propertyInvoker;
		}



		#region IComparer<T> Members

		//TODO: (SD) Handle null values for T itself
		public int Compare(T x, T y)
		{

			if (this.Direction == OrderDirection.None || this.Direction == OrderDirection.Ascending)
			{
				return compare(propertyInvoker(x),propertyInvoker(y));
			}
			return compare(propertyInvoker(x), propertyInvoker(y)) * -1;
		}

		private int compare(P x, P y)
		{
			if (typeof(P).IsClass)
			{
				if (x == null && y == null)
					return 0;
				if (x != null && y == null)
					return 1;
				if (x == null && y != null)
					return -1;
			}
			return x.CompareTo(y);
		}


		#endregion
	}
}
