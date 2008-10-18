using System;
using System.Collections;
using System.Xml.Serialization;


namespace Tools.Tracing.Common
{
    
    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='Tools.Core.configuration.EventIdentifier'/> objects.
    ///    </para>
    /// </summary>
    /// <seealso cref='Tools.Core.configuration.EventIdentifierCollection'/>
    [Serializable()]
    public class EventIdentifierCollection : CollectionBase 
	{
        
		#region Constructors

		/// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.EventIdentifierCollection'/>.
        ///    </para>
        /// </summary>
        public EventIdentifierCollection() 
		{
        }
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.EventIdentifierCollection'/> based on another <see cref='Tools.Core.configuration.EventIdentifierCollection'/>.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A <see cref='Tools.Core.configuration.EventIdentifierCollection'/> from which the contents are copied
        /// </param>
        public EventIdentifierCollection(EventIdentifierCollection value) {
            this.AddRange(value);
        }
        
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.EventIdentifierCollection'/> containing any array of <see cref='Tools.Core.configuration.EventIdentifier'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='Tools.Core.configuration.EventIdentifier'/> objects with which to intialize the collection
        /// </param>
        public EventIdentifierCollection(EventIdentifier[] value) {
            this.AddRange(value);
        }
        

		#endregion

		#region Indexers

        /// <summary>
        /// <para>Represents the entry at the specified index of the <see cref='Tools.Core.configuration.EventIdentifier'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public EventIdentifier this[int index] 
		{
            get {
                return ((EventIdentifier)(List[index]));
            }
            set {
                List[index] = value;
            }
        }
		public EventIdentifier this[string name] 
		{
			get 
			{
				foreach (EventIdentifier fdf in this)
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
					if (((EventIdentifier)List[i]).Name==name) 
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
        ///    <para>Adds a <see cref='Tools.Core.configuration.EventIdentifier'/> with the specified value to the 
        ///    <see cref='Tools.Core.configuration.EventIdentifierCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.EventIdentifier'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.EventIdentifierCollection.AddRange'/>
        public int Add(EventIdentifier value) 
		{
            return List.Add(value);
        }
        
        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='Tools.Core.configuration.EventIdentifierCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='Tools.Core.configuration.EventIdentifier'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.EventIdentifierCollection.Add'/>
        public void AddRange(EventIdentifier[] value) {
            for (int i = 0; (i < value.Length); i = (i + 1)) {
				EventIdentifier nv = this.GetEntry(value[i].Name);
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
        ///       Adds the contents of another <see cref='Tools.Core.configuration.EventIdentifierCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='Tools.Core.configuration.EventIdentifierCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.EventIdentifierCollection.Add'/>
        public void AddRange(EventIdentifierCollection value) {
            for (int i = 0; (i < value.Count); i = (i + 1)) {
                this.Add(value[i]);
            }
        }
        
        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='Tools.Core.configuration.EventIdentifierCollection'/> contains the specified <see cref='Tools.Core.configuration.EventIdentifier'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.EventIdentifier'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='Tools.Core.configuration.EventIdentifier'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.EventIdentifierCollection.IndexOf'/>
        public bool Contains(EventIdentifier value) {
            return List.Contains(value);
        }
        
        /// <summary>
        /// <para>Copies the <see cref='Tools.Core.configuration.EventIdentifierCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='Tools.Core.configuration.EventIdentifierCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='Tools.Core.configuration.EventIdentifierCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(EventIdentifier[] array, int index) {
            List.CopyTo(array, index);
        }
        
        /// <summary>
        ///    <para>Returns the index of a <see cref='Tools.Core.configuration.EventIdentifier'/> in 
        ///       the <see cref='Tools.Core.configuration.EventIdentifierCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.EventIdentifier'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='Tools.Core.configuration.EventIdentifier'/> of <paramref name='value'/> in the 
        /// <see cref='Tools.Core.configuration.EventIdentifierCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.EventIdentifierCollection.Contains'/>
        public int IndexOf(EventIdentifier value) {
            return List.IndexOf(value);
        }
        
        /// <summary>
        /// <para>Inserts a <see cref='Tools.Core.configuration.EventIdentifier'/> into the <see cref='Tools.Core.configuration.EventIdentifierCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='Tools.Core.configuration.EventIdentifier'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='Tools.Core.configuration.EventIdentifierCollection.Add'/>
        public void Insert(int index, EventIdentifier value) {
            List.Insert(index, value);
        }
        
        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='Tools.Core.configuration.EventIdentifierCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new EventIdentifierEnumerator GetEnumerator() {
            return new EventIdentifierEnumerator(this);
        }
        
        /// <summary>
        ///    <para> Removes a specific <see cref='Tools.Core.configuration.EventIdentifier'/> from the 
        ///    <see cref='Tools.Core.configuration.EventIdentifierCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.EventIdentifier'/> to remove from the <see cref='Tools.Core.configuration.EventIdentifierCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(EventIdentifier value) {
            List.Remove(value);
        }
		/// <summary>
		/// Gets an entry for the supplied name.
		/// </summary>
		/// <param name="name">Entry name.</param>
		/// <returns>Entry if exists or null otherwise.</returns>
		public  EventIdentifier GetEntry(string name)
		{
			EventIdentifierEnumerator ce = this.GetEnumerator();
			while (ce.MoveNext())
			{
				if (ce.Current.Name == name) return ce.Current;
			}
			return null;
		}
		/// <summary>
		/// Gets an entry for the supplied name.
		/// </summary>
		/// <param name="name">Entry name.</param>
		/// <returns>Entry if exists or null otherwise.</returns>
		public  EventIdentifier GetEntryById(int id)
		{
			EventIdentifierEnumerator ce = this.GetEnumerator();
			while (ce.MoveNext())
			{
				if (ce.Current.Id == id) return ce.Current;
			}
			return null;
		}        

		#endregion
		
		#region EventIdentifierEnumerator class
		
		public class EventIdentifierEnumerator : object, IEnumerator 
		{
            
			#region Global declarations
           
			private IEnumerator baseEnumerator;
            private IEnumerable temp;

			#endregion
            
			#region Constructors
			
			public EventIdentifierEnumerator(EventIdentifierCollection mappings) 
			{
                this.temp = ((IEnumerable)(mappings));
                this.baseEnumerator = temp.GetEnumerator();
            }

		
			#endregion
            
			#region Properties
		
			public EventIdentifier Current 
			{
                get {
                    return ((EventIdentifier)(baseEnumerator.Current));
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
