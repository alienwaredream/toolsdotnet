using System;
using System.Collections;


namespace Tools.Tracing.Common
{
    
    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/> objects.
    ///    </para>
    /// </summary>
    /// <seealso cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/>
    [Serializable()]
    public class TraceEventHandlerConfigurationCollection : CollectionBase 
	{
        
		#region Constructors

		/// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/>.
        ///    </para>
        /// </summary>
        public TraceEventHandlerConfigurationCollection() 
		{
        }
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/> based on another <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/>.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/> from which the contents are copied
        /// </param>
        public TraceEventHandlerConfigurationCollection(TraceEventHandlerConfigurationCollection value) {
            this.AddRange(value);
        }
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/> containing any array of <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/> objects with which to intialize the collection
        /// </param>
        public TraceEventHandlerConfigurationCollection(TraceEventHandlerConfiguration[] value) {
            this.AddRange(value);
        }
        

		#endregion

		#region Indexers

        /// <summary>
        /// <para>Represents the entry at the specified index of the <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public TraceEventHandlerConfiguration this[int index] 
		{
            get {
                return ((TraceEventHandlerConfiguration)(List[index]));
            }
            set {
                List[index] = value;
            }
        }
		public TraceEventHandlerConfiguration this[string name] 
		{
			get 
			{
				foreach (TraceEventHandlerConfiguration fdf in this)
				{
					if (fdf.Name==name) 
					{
						return fdf;
					}
				}
				return null;
			}
			set 
			{
				for (int i = 0; i < List.Count; i++)
				{
					if (((TraceEventHandlerConfiguration)List[i]).Name==name) 
					{
						List[i] = value;
						return;
					}
				}
				this.Add(value);
			}
		}          


		#endregion

		#region Methods

        /// <summary>
        ///    <para>Adds a <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/> with the specified value to the 
        ///    <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection.AddRange'/>
        public int Add(TraceEventHandlerConfiguration value) 
		{
            return List.Add(value);
        }
        
        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection.Add'/>
        public void AddRange(TraceEventHandlerConfiguration[] value) {
            for (int i = 0; (i < value.Length); i = (i + 1)) {
				TraceEventHandlerConfiguration nv = this.GetEntry(value[i].Name);
				if (nv!=null)
				{ 
					nv = value[i];
				}
				else
				{
					this.Add(value[i]);
				}
            }
        }
        
        /// <summary>
        ///     <para>
        ///       Adds the contents of another <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection.Add'/>
        public void AddRange(TraceEventHandlerConfigurationCollection value) {
            for (int i = 0; (i < value.Count); i = (i + 1)) {
                this.Add(value[i]);
            }
        }
        
        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/> contains the specified <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection.IndexOf'/>
        public bool Contains(TraceEventHandlerConfiguration value) {
            return List.Contains(value);
        }
        
        /// <summary>
        /// <para>Copies the <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(TraceEventHandlerConfiguration[] array, int index) {
            List.CopyTo(array, index);
        }
        
        /// <summary>
        ///    <para>Returns the index of a <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/> in 
        ///       the <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/> of <paramref name='value'/> in the 
        /// <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection.Contains'/>
        public int IndexOf(TraceEventHandlerConfiguration value) {
            return List.IndexOf(value);
        }
        
        /// <summary>
        /// <para>Inserts a <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/> into the <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection.Add'/>
        public void Insert(int index, TraceEventHandlerConfiguration value) {
            List.Insert(index, value);
        }
        
        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new ApplicationEventHandlerConfigurationEnumerator GetEnumerator() {
            return new ApplicationEventHandlerConfigurationEnumerator(this);
        }
        
        /// <summary>
        ///    <para> Removes a specific <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/> from the 
        ///    <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.ApplicationEventHandlerConfiguration'/> to remove from the <see cref='Tools.Core.configuration.ApplicationEventHandlerConfigurationCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(TraceEventHandlerConfiguration value) {
            List.Remove(value);
        }
		/// <summary>
		/// Gets an entry for the supplied name.
		/// </summary>
		/// <param name="name">Entry name.</param>
		/// <returns>Entry if exists or null otherwise.</returns>
		public  TraceEventHandlerConfiguration GetEntry(string name)
		{
			ApplicationEventHandlerConfigurationEnumerator ce = this.GetEnumerator();
			while (ce.MoveNext())
			{
				if (ce.Current.Name == name) return ce.Current;
			}
			return null;
		}
        

		#endregion
		
		#region ApplicationEventHandlerConfigurationEnumerator class
		
		public class ApplicationEventHandlerConfigurationEnumerator : object, IEnumerator 
		{
            
			#region Global declarations
           
			private IEnumerator baseEnumerator;
            private IEnumerable temp;

			#endregion
            
			#region Constructors
			
			public ApplicationEventHandlerConfigurationEnumerator(TraceEventHandlerConfigurationCollection mappings) 
			{
                this.temp = ((IEnumerable)(mappings));
                this.baseEnumerator = temp.GetEnumerator();
            }

		
			#endregion
            
			#region Properties
		
			public TraceEventHandlerConfiguration Current 
			{
                get {
                    return ((TraceEventHandlerConfiguration)(baseEnumerator.Current));
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
            
            public void Reset() {
                baseEnumerator.Reset();
            }
            

			#endregion
        }

		#endregion

    }
}
