using System;
using System.Collections;


namespace Tools.Core
{
    
    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='Tools.Core.configuration.DescriptiveNameValue'/> objects.
    ///    </para>
    /// </summary>
    /// <seealso cref='Tools.Core.configuration.DescriptiveNameValueCollection'/>
    [Serializable()]
    public class DescriptiveNameValueCollection : CollectionBase 
	{
        
		#region Constructors

		/// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/>.
        ///    </para>
        /// </summary>
        public DescriptiveNameValueCollection() 
		{
        }
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/> based on another <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/>.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/> from which the contents are copied
        /// </param>
        public DescriptiveNameValueCollection(DescriptiveNameValueCollection value) {
            this.AddRange(value);
        }
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/> containing any array of <see cref='Tools.Core.configuration.DescriptiveNameValue'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='Tools.Core.configuration.DescriptiveNameValue'/> objects with which to intialize the collection
        /// </param>
        public DescriptiveNameValueCollection(DescriptiveNameValue[] value) {
            this.AddRange(value);
        }
        

		#endregion

		#region Indexers

        /// <summary>
        /// <para>Represents the entry at the specified index of the <see cref='Tools.Core.configuration.DescriptiveNameValue'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public DescriptiveNameValue this[int index] 
		{
            get {
                return ((DescriptiveNameValue)(List[index]));
            }
            set {
                List[index] = value;
            }
        }
		public DescriptiveNameValue this[string name] 
		{
			get 
			{
				foreach (DescriptiveNameValue fdf in this)
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
					if (((DescriptiveNameValue)List[i]).Name==name) 
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
        ///    <para>Adds a <see cref='Tools.Core.configuration.DescriptiveNameValue'/> with the specified value to the 
        ///    <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.DescriptiveNameValue'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.DescriptiveNameValueCollection.AddRange'/>
        public int Add(DescriptiveNameValue value) 
		{
            return List.Add(value);
        }
        
        /// <summary>
        /// <para>Merges the elements of an array to the end of the <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='Tools.Core.configuration.DescriptiveNameValue'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.DescriptiveNameValueCollection.Add'/>
        public void AddRange(DescriptiveNameValue[] value) {
            for (int i = 0; (i < value.Length); i = (i + 1)) {
				DescriptiveNameValue nv = this.GetEntry(value[i].Name);
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
        ///       Adds the contents of another <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.DescriptiveNameValueCollection.Add'/>
        public void AddRange(DescriptiveNameValueCollection value) {
			for (int i = 0; (i < value.Count); i = (i + 1)) 
			{
				DescriptiveNameValue nv = this.GetEntry(value[i].Name);
				if (nv!=null)
				{ 
					this[value[i].Name] = value[i];
					int nn = 0;
				}
				else
				{
					this.Add(value[i]);
				}
			}
        }
        
        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/> contains the specified <see cref='Tools.Core.configuration.DescriptiveNameValue'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.DescriptiveNameValue'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='Tools.Core.configuration.DescriptiveNameValue'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.DescriptiveNameValueCollection.IndexOf'/>
        public bool Contains(DescriptiveNameValue value) {
            return List.Contains(value);
        }

		/// <summary>
		/// <para>Gets a value indicating whether the 
		///    <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/> contains <see cref='Tools.Core.configuration.NameValue'/> with specified name.</para>
		/// </summary>
		/// <param name='itemName'>Name if the <see cref='Tools.Core.configuration.DescriptiveNameValue'/> to locate.</param>
		/// <returns>
		/// <para><see langword='true'/> if the <see cref='Tools.Core.configuration.DescriptiveNameValue'/> is contained in the collection; 
		///   otherwise, <see langword='false'/>.</para>
		/// </returns>
		/// <seealso cref='Tools.Core.configuration.NameValueCollection.IndexOf'/>
		public bool Contains(string itemName) 
		{
			foreach(DescriptiveNameValue nameValue in List)
			{
				if(nameValue.Name == itemName)
				{
					return true;
				}
			}
			return false;
		}
        
        /// <summary>
        /// <para>Copies the <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(DescriptiveNameValue[] array, int index) {
            List.CopyTo(array, index);
        }
        
        /// <summary>
        ///    <para>Returns the index of a <see cref='Tools.Core.configuration.DescriptiveNameValue'/> in 
        ///       the <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.DescriptiveNameValue'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='Tools.Core.configuration.DescriptiveNameValue'/> of <paramref name='value'/> in the 
        /// <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.DescriptiveNameValueCollection.Contains'/>
        public int IndexOf(DescriptiveNameValue value) {
            return List.IndexOf(value);
        }
        
        /// <summary>
        /// <para>Inserts a <see cref='Tools.Core.configuration.DescriptiveNameValue'/> into the <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='Tools.Core.configuration.DescriptiveNameValue'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='Tools.Core.configuration.DescriptiveNameValueCollection.Add'/>
        public void Insert(int index, DescriptiveNameValue value) {
            List.Insert(index, value);
        }
        
        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new DescriptiveNameValueEnumerator GetEnumerator() {
            return new DescriptiveNameValueEnumerator(this);
        }
        
        /// <summary>
        ///    <para> Removes a specific <see cref='Tools.Core.configuration.DescriptiveNameValue'/> from the 
        ///    <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.DescriptiveNameValue'/> to remove from the <see cref='Tools.Core.configuration.DescriptiveNameValueCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(DescriptiveNameValue value) {
            List.Remove(value);
        }
		/// <summary>
		/// Gets an entry for the supplied name.
		/// </summary>
		/// <param name="name">Entry name.</param>
		/// <returns>Entry if exists or null otherwise.</returns>
		public  DescriptiveNameValue GetEntry(string name)
		{
			DescriptiveNameValueEnumerator ce = this.GetEnumerator();
			while (ce.MoveNext())
			{
				if (ce.Current.Name == name) return ce.Current;
			}
			return null;
		}
        

		#endregion
		
		#region DescriptiveNameValueEnumerator class
		
		public class DescriptiveNameValueEnumerator : object, IEnumerator 
		{
            
			#region Fields
           
			private IEnumerator baseEnumerator;
            private IEnumerable temp;

			#endregion
            
			#region Constructors
			
			public DescriptiveNameValueEnumerator(DescriptiveNameValueCollection mappings) 
			{
                this.temp = ((IEnumerable)(mappings));
                this.baseEnumerator = temp.GetEnumerator();
            }

		
			#endregion
            
			#region Properties
		
			public DescriptiveNameValue Current 
			{
                get {
                    return ((DescriptiveNameValue)(baseEnumerator.Current));
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
