using System;
using System.Collections;

namespace Tools.Common.Process
{
	#region IProcessEnumerator class
		
	public class IProcessEnumerator : object, IEnumerator 
	{
            
		#region Global declarations
           
		private IEnumerator baseEnumerator;
		private IEnumerable temp;

		#endregion
            
		#region Constructors
			
		public IProcessEnumerator(IProcessCollection mappings) 
		{
			this.temp = ((IEnumerable)(mappings));
			this.baseEnumerator = temp.GetEnumerator();
		}

		
		#endregion
            
		#region Properties
		
		public IProcess Current 
		{
			get 
			{
				return ((IProcess)(baseEnumerator.Current));
			}
		}
            

		#endregion
           
		#region IEnumerator implementation

		object IEnumerator.Current 
		{
			get 
			{
				return baseEnumerator.Current;
			}
		}

		bool IEnumerator.MoveNext() 
		{
			return baseEnumerator.MoveNext();
		}
            
		void IEnumerator.Reset() 
		{
			baseEnumerator.Reset();
		}

			
		#endregion

		#region Methods

		public bool MoveNext() 
		{
			return baseEnumerator.MoveNext();
		}
            
		public void Reset() 
		{
			baseEnumerator.Reset();
		}
            

		#endregion
	
	}	
	#endregion
}

