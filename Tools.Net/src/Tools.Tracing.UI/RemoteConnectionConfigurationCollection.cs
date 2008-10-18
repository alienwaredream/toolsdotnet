using System;
using System.Collections;


namespace Tools.Tracing.UI
{
    
    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/> objects.
    ///    </para>
    /// </summary>
    /// <seealso cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/>
    [Serializable()]
    public class RemoteConnectionConfigurationCollection : CollectionBase
	{

		private string defaultConnectionName = "Connection";
		private int defaultConnectionIndex = 1;

		public string GetDefaultConnectionName() // Given this, it might be moved to Component smth. (SD)
		{
			for (int i = defaultConnectionIndex; i < 200; i++)
			{
				string nameCandidate = defaultConnectionName + i.ToString();

				if (GetEntry(nameCandidate) == null)
				{
					return nameCandidate;
				}
			}
			return null;

		}
        
		#region Constructors

		/// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/>.
        ///    </para>
        /// </summary>
        public RemoteConnectionConfigurationCollection() 
		{
        }
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/> based on another <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/>.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/> from which the contents are copied
        /// </param>
        public RemoteConnectionConfigurationCollection(RemoteConnectionConfigurationCollection value) {
            this.AddRange(value);
        }
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/> containing any array of <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/> objects with which to intialize the collection
        /// </param>
        public RemoteConnectionConfigurationCollection(RemoteConnectionConfiguration[] value) {
            this.AddRange(value);
        }
        

		#endregion

		#region Indexers

        /// <summary>
        /// <para>Represents the entry at the specified index of the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public RemoteConnectionConfiguration this[int index] 
		{
            get {
                return ((RemoteConnectionConfiguration)(List[index]));
            }
            set {
                List[index] = value;
            }
        }
		public RemoteConnectionConfiguration this[string name] 
		{
			get 
			{
				foreach (RemoteConnectionConfiguration fdf in this)
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
					if (((RemoteConnectionConfiguration)List[i]).Name==name) 
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
        ///    <para>Adds a <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/> with the specified value to the 
        ///    <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection.AddRange'/>
        public int Add(RemoteConnectionConfiguration value) 
		{
            return List.Add(value);
        }
        
        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection.Add'/>
        public void AddRange(RemoteConnectionConfiguration[] value) {
            for (int i = 0; (i < value.Length); i = (i + 1)) {
				RemoteConnectionConfiguration nv = this.GetEntry(value[i].Name);
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
        ///       Adds the contents of another <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection.Add'/>
        public void AddRange(RemoteConnectionConfigurationCollection value) {
            for (int i = 0; (i < value.Count); i = (i + 1)) {
                this.Add(value[i]);
            }
        }
        
        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/> contains the specified <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection.IndexOf'/>
        public bool Contains(RemoteConnectionConfiguration value) {
            return List.Contains(value);
        }
        
        /// <summary>
        /// <para>Copies the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(RemoteConnectionConfiguration[] array, int index) {
            List.CopyTo(array, index);
        }
        
        /// <summary>
        ///    <para>Returns the index of a <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/> in 
        ///       the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/> of <paramref name='value'/> in the 
        /// <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection.Contains'/>
        public int IndexOf(RemoteConnectionConfiguration value) {
            return List.IndexOf(value);
        }
        
        /// <summary>
        /// <para>Inserts a <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/> into the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection.Add'/>
        public void Insert(int index, RemoteConnectionConfiguration value) {
            List.Insert(index, value);
        }
        
        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new RemoteConnectionConfigurationEnumerator GetEnumerator() {
            return new RemoteConnectionConfigurationEnumerator(this);
        }
        
        /// <summary>
        ///    <para> Removes a specific <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/> from the 
        ///    <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfiguration'/> to remove from the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionConfigurationCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(RemoteConnectionConfiguration value) {
            List.Remove(value);
        }
		/// <summary>
		/// Gets an entry for the supplied name.
		/// </summary>
		/// <param name="name">Entry name.</param>
		/// <returns>Entry if exists or null otherwise.</returns>
		public  RemoteConnectionConfiguration GetEntry(string name)
		{
			RemoteConnectionConfigurationEnumerator ce = this.GetEnumerator();
			while (ce.MoveNext())
			{
				if (ce.Current.Name == name) return ce.Current;
			}
			return null;
		}
        

		#endregion
		
		#region RemoteConnectionConfigurationEnumerator class
		
		public class RemoteConnectionConfigurationEnumerator : object, IEnumerator 
		{
            
			#region Global declarations
           
			private IEnumerator baseEnumerator;
            private IEnumerable temp;

			#endregion
            
			#region Constructors
			
			public RemoteConnectionConfigurationEnumerator(RemoteConnectionConfigurationCollection mappings) 
			{
                this.temp = ((IEnumerable)(mappings));
                this.baseEnumerator = temp.GetEnumerator();
            }

		
			#endregion
            
			#region Properties
		
			public RemoteConnectionConfiguration Current 
			{
                get {
                    return ((RemoteConnectionConfiguration)(baseEnumerator.Current));
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
