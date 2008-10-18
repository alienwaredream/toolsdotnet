using System;
using System.Collections;


namespace Tools.Tracing.Common
{
    
    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/> objects.
    ///    </para>
    /// </summary>
    /// <seealso cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/>
    [Serializable()]
    public class TraceEventFilterConfigurationCollection : CollectionBase 
	{
        
		#region Constructors

		/// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/>.
        ///    </para>
        /// </summary>
        public TraceEventFilterConfigurationCollection() 
		{
        }
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/> based on another <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/>.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/> from which the contents are copied
        /// </param>
        public TraceEventFilterConfigurationCollection(TraceEventFilterConfigurationCollection value) {
            this.AddRange(value);
        }
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/> containing any array of <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/> objects with which to intialize the collection
        /// </param>
        public TraceEventFilterConfigurationCollection(TraceEventFilterConfiguration[] value) {
            this.AddRange(value);
        }
        

		#endregion

		#region Indexers

        /// <summary>
        /// <para>Represents the entry at the specified index of the <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public TraceEventFilterConfiguration this[int index] 
		{
            get {
                return ((TraceEventFilterConfiguration)(List[index]));
            }
            set {
                List[index] = value;
            }
        }
		public TraceEventFilterConfiguration this[string name] 
		{
			get 
			{
				foreach (TraceEventFilterConfiguration fdf in this)
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
					if (((TraceEventFilterConfiguration)List[i]).Name==name) 
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
        ///    <para>Adds a <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/> with the specified value to the 
        ///    <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection.AddRange'/>
        public int Add(TraceEventFilterConfiguration value) 
		{
            return List.Add(value);
        }
        
        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection.Add'/>
        public void AddRange(TraceEventFilterConfiguration[] value) {
            for (int i = 0; (i < value.Length); i = (i + 1)) {
				TraceEventFilterConfiguration nv = this.GetEntry(value[i].Name);
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
        ///       Adds the contents of another <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection.Add'/>
        public void AddRange(TraceEventFilterConfigurationCollection value) {
            for (int i = 0; (i < value.Count); i = (i + 1)) {
                this.Add(value[i]);
            }
        }
        
        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/> contains the specified <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection.IndexOf'/>
        public bool Contains(TraceEventFilterConfiguration value) {
            return List.Contains(value);
        }
        
        /// <summary>
        /// <para>Copies the <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(TraceEventFilterConfiguration[] array, int index) {
            List.CopyTo(array, index);
        }
        
        /// <summary>
        ///    <para>Returns the index of a <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/> in 
        ///       the <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/> of <paramref name='value'/> in the 
        /// <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection.Contains'/>
        public int IndexOf(TraceEventFilterConfiguration value) {
            return List.IndexOf(value);
        }
        
        /// <summary>
        /// <para>Inserts a <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/> into the <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection.Add'/>
        public void Insert(int index, TraceEventFilterConfiguration value) {
            List.Insert(index, value);
        }
        
        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new ApplicationEventFilterConfigurationEnumerator GetEnumerator() {
            return new ApplicationEventFilterConfigurationEnumerator(this);
        }
        
        /// <summary>
        ///    <para> Removes a specific <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/> from the 
        ///    <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.ApplicationEventFilterConfiguration'/> to remove from the <see cref='Tools.Core.configuration.ApplicationEventFilterConfigurationCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(TraceEventFilterConfiguration value) {
            List.Remove(value);
        }
		/// <summary>
		/// Gets an entry for the supplied name.
		/// </summary>
		/// <param name="name">Entry name.</param>
		/// <returns>Entry if exists or null otherwise.</returns>
		public  TraceEventFilterConfiguration GetEntry(string name)
		{
			ApplicationEventFilterConfigurationEnumerator ce = this.GetEnumerator();
			while (ce.MoveNext())
			{
				if (ce.Current.Name == name) return ce.Current;
			}
			return null;
		}
        

		#endregion
		
		#region ApplicationEventFilterConfigurationEnumerator class
		
		public class ApplicationEventFilterConfigurationEnumerator : object, IEnumerator 
		{
            
			#region Global declarations
           
			private IEnumerator baseEnumerator;
            private IEnumerable temp;

			#endregion
            
			#region Constructors
			
			public ApplicationEventFilterConfigurationEnumerator(TraceEventFilterConfigurationCollection mappings) 
			{
                this.temp = ((IEnumerable)(mappings));
                this.baseEnumerator = temp.GetEnumerator();
            }

		
			#endregion
            
			#region Properties
		
			public TraceEventFilterConfiguration Current 
			{
                get {
                    return ((TraceEventFilterConfiguration)(baseEnumerator.Current));
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
