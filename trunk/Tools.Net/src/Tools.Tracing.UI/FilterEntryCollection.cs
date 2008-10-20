using System;
using System.Collections;

using Tools.Core;

namespace Tools.Tracing.UI
{
    
    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='Tools..FilterEntry'/> objects.
    ///    </para>
    /// </summary>
    /// <seealso cref='Tools..FilterEntryCollection'/>
    [Serializable()]
    public class FilterEntryCollection : CollectionBase, IChangeEventRaiser
	{
        
		#region Constructors

		/// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools..FilterEntryCollection'/>.
        ///    </para>
        /// </summary>
        public FilterEntryCollection() 
		{
        }
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools..FilterEntryCollection'/> based on another <see cref='Tools..FilterEntryCollection'/>.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A <see cref='Tools..FilterEntryCollection'/> from which the contents are copied
        /// </param>
        public FilterEntryCollection(FilterEntryCollection value) {
            this.AddRange(value);
        }
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools..FilterEntryCollection'/> containing any array of <see cref='Tools..FilterEntry'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='Tools..FilterEntry'/> objects with which to intialize the collection
        /// </param>
        public FilterEntryCollection(FilterEntry[] value) {
            this.AddRange(value);
        }
        

		#endregion

		#region Indexers

        /// <summary>
        /// <para>Represents the entry at the specified index of the <see cref='Tools..FilterEntry'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public FilterEntry this[int index] 
		{
            get {
                return ((FilterEntry)(List[index]));
            }
            set {
                List[index] = value;
            }
        }
		private void reAssignOnChangeHandler(FilterEntry oldFilter, FilterEntry newFilter)
		{
			oldFilter.Changed -= new System.EventHandler(this.filterEntryChanged);
			newFilter.Changed += new System.EventHandler(this.filterEntryChanged);
		}
		public FilterEntry this[string path] 
		{
			get 
			{
				foreach (FilterEntry fdf in this)
				{
					if (fdf.Path==path) 
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
					if (((FilterEntry)List[i]).Path==path) 
					{
						List[i] = value;
						reAssignOnChangeHandler((FilterEntry)List[i], value);
						OnChanged();
						return;
					}
				}
				this.Add(value);
			}
		}          


		#endregion

		#region Methods

        /// <summary>
        ///    <para>Adds a <see cref='Tools..FilterEntry'/> with the specified value to the 
        ///    <see cref='Tools..FilterEntryCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools..FilterEntry'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='Tools..FilterEntryCollection.AddRange'/>
        public int Add(FilterEntry value) 
		{
			FilterEntry nv = this.GetEntry(value.Path);

			if (nv!=null)
			{ 
				throw new ArgumentException("Can't add the item with duplicate path of " + nv.Path);
			}
			int ret = List.Add(value);
			value.Changed += new System.EventHandler(this.filterEntryChanged);
			OnChanged();
			return ret;

        }
        
        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='Tools..FilterEntryCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='Tools..FilterEntry'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools..FilterEntryCollection.Add'/>
        public void AddRange(FilterEntry[] value) {
			for (int i = 0; (i < value.Length); i = (i + 1)) 
			{
				this.Add(value[i]);
			}
        }
        
        /// <summary>
        ///     <para>
        ///       Adds the contents of another <see cref='Tools..FilterEntryCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='Tools..FilterEntryCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools..FilterEntryCollection.Add'/>
        public void AddRange(FilterEntryCollection value) {
            for (int i = 0; (i < value.Count); i = (i + 1)) {
                this.Add(value[i]);
            }
        }
        
        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='Tools..FilterEntryCollection'/> contains the specified <see cref='Tools..FilterEntry'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools..FilterEntry'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='Tools..FilterEntry'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='Tools..FilterEntryCollection.IndexOf'/>
        public bool Contains(FilterEntry value) {
            return List.Contains(value);
        }
        
        /// <summary>
        /// <para>Copies the <see cref='Tools..FilterEntryCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='Tools..FilterEntryCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='Tools..FilterEntryCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(FilterEntry[] array, int index) {
            List.CopyTo(array, index);
        }
        
        /// <summary>
        ///    <para>Returns the index of a <see cref='Tools..FilterEntry'/> in 
        ///       the <see cref='Tools..FilterEntryCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools..FilterEntry'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='Tools..FilterEntry'/> of <paramref name='value'/> in the 
        /// <see cref='Tools..FilterEntryCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='Tools..FilterEntryCollection.Contains'/>
        public int IndexOf(FilterEntry value) {
            return List.IndexOf(value);
        }
        
        /// <summary>
        /// <para>Inserts a <see cref='Tools..FilterEntry'/> into the <see cref='Tools..FilterEntryCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='Tools..FilterEntry'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='Tools..FilterEntryCollection.Add'/>
        public void Insert(int index, FilterEntry value) {
            List.Insert(index, value);
        }
        
        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='Tools..FilterEntryCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new FilterEntryEnumerator GetEnumerator() {
            return new FilterEntryEnumerator(this);
        }
        
        /// <summary>
        ///    <para> Removes a specific <see cref='Tools..FilterEntry'/> from the 
        ///    <see cref='Tools..FilterEntryCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools..FilterEntry'/> to remove from the <see cref='Tools..FilterEntryCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(FilterEntry value) 
		{
			value.Changed -= new System.EventHandler(this.filterEntryChanged);
            List.Remove(value);
			OnChanged();
        }
		/// <summary>
		/// Gets an entry for the supplied name.
		/// </summary>
		/// <param name="name">Entry name.</param>
		/// <returns>Entry if exists or null otherwise.</returns>
		public  FilterEntry GetEntry(string name)
		{
			FilterEntryEnumerator ce = this.GetEnumerator();
			while (ce.MoveNext())
			{
				if (ce.Current.Name == name) return ce.Current;
			}
			return null;
		}
        

		#endregion
		
		#region FilterEntryEnumerator class
		
		public class FilterEntryEnumerator : object, IEnumerator 
		{
            
			#region Global declarations
           
			private IEnumerator baseEnumerator;
            private IEnumerable temp;

			#endregion
            
			#region Constructors
			
			public FilterEntryEnumerator(FilterEntryCollection mappings) 
			{
                this.temp = ((IEnumerable)(mappings));
                this.baseEnumerator = temp.GetEnumerator();
            }

		
			#endregion
            
			#region Properties
		
			public FilterEntry Current 
			{
                get {
                    return ((FilterEntry)(baseEnumerator.Current));
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

		private void filterEntryChanged(object sender, EventArgs e)
		{
			OnChanged();
		}

		#region IChangeEventRaiser Members

		private void OnChanged()
		{
			if (Changed!=null) Changed(this, EventArgs.Empty);
		}

		public event System.EventHandler Changed;

		#endregion
	}
}
