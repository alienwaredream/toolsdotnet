using System;
using System.Collections;


namespace Tools.Tracing.UI
{
    
    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/> objects.
    ///    </para>
    /// </summary>
    /// <seealso cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/>
    [Serializable()]
    public class RemoteConnectionInstanceCollection : CollectionBase
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
        ///       Initializes a new instance of <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/>.
        ///    </para>
        /// </summary>
        public RemoteConnectionInstanceCollection() 
		{
        }
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/> based on another <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/>.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/> from which the contents are copied
        /// </param>
        public RemoteConnectionInstanceCollection(RemoteConnectionInstanceCollection value) {
            this.AddRange(value);
        }
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/> containing any array of <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/> objects with which to intialize the collection
        /// </param>
        public RemoteConnectionInstanceCollection(RemoteConnectionInstance[] value) {
            this.AddRange(value);
        }
        

		#endregion

		#region Indexers

        /// <summary>
        /// <para>Represents the entry at the specified index of the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public RemoteConnectionInstance this[int index] 
		{
            get {
                return ((RemoteConnectionInstance)(List[index]));
            }
            set {
                List[index] = value;
            }
        }
		public RemoteConnectionInstance this[string name] 
		{
			get 
			{
				foreach (RemoteConnectionInstance fdf in this)
				{
					if (fdf.Configuration.Name==name) 
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
					if (((RemoteConnectionInstance)List[i]).Configuration.Name==name) 
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
        ///    <para>Adds a <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/> with the specified value to the 
        ///    <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection.AddRange'/>
        public int Add(RemoteConnectionInstance value) 
		{
            return List.Add(value);
        }
        
        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection.Add'/>
        public void AddRange(RemoteConnectionInstance[] value) {
            for (int i = 0; (i < value.Length); i = (i + 1)) {
				RemoteConnectionInstance nv = this.GetEntry(value[i].Configuration.Name);
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
        ///       Adds the contents of another <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection.Add'/>
        public void AddRange(RemoteConnectionInstanceCollection value) {
            for (int i = 0; (i < value.Count); i = (i + 1)) {
                this.Add(value[i]);
            }
        }
        
        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/> contains the specified <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection.IndexOf'/>
        public bool Contains(RemoteConnectionInstance value) {
            return List.Contains(value);
        }
        
        /// <summary>
        /// <para>Copies the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(RemoteConnectionInstance[] array, int index) {
            List.CopyTo(array, index);
        }
        
        /// <summary>
        ///    <para>Returns the index of a <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/> in 
        ///       the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/> of <paramref name='value'/> in the 
        /// <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection.Contains'/>
        public int IndexOf(RemoteConnectionInstance value) {
            return List.IndexOf(value);
        }
        
        /// <summary>
        /// <para>Inserts a <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/> into the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection.Add'/>
        public void Insert(int index, RemoteConnectionInstance value) {
            List.Insert(index, value);
        }
        
        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new RemoteConnectionInstanceEnumerator GetEnumerator() {
            return new RemoteConnectionInstanceEnumerator(this);
        }
        
        /// <summary>
        ///    <para> Removes a specific <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/> from the 
        ///    <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstance'/> to remove from the <see cref='eurocommerce.ie.architecture.root.configuration.RemoteConnectionInstanceCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(RemoteConnectionInstance value) {
            List.Remove(value);
        }
		/// <summary>
		/// Gets an entry for the supplied name.
		/// </summary>
		/// <param name="name">Entry name.</param>
		/// <returns>Entry if exists or null otherwise.</returns>
		public  RemoteConnectionInstance GetEntry(string name)
		{
			RemoteConnectionInstanceEnumerator ce = this.GetEnumerator();
			while (ce.MoveNext())
			{
				if (ce.Current.Configuration.Name == name) return ce.Current;
			}
			return null;
		}
        

		#endregion
		
		#region RemoteConnectionInstanceEnumerator class
		
		public class RemoteConnectionInstanceEnumerator : object, IEnumerator 
		{
            
			#region Global declarations
           
			private IEnumerator baseEnumerator;
            private IEnumerable temp;

			#endregion
            
			#region Constructors
			
			public RemoteConnectionInstanceEnumerator(RemoteConnectionInstanceCollection mappings) 
			{
                this.temp = ((IEnumerable)(mappings));
                this.baseEnumerator = temp.GetEnumerator();
            }

		
			#endregion
            
			#region Properties
		
			public RemoteConnectionInstance Current 
			{
                get {
                    return ((RemoteConnectionInstance)(baseEnumerator.Current));
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
