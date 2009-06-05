using System;
using System.Collections;

namespace Tools.Coordination.WorkItems
{

    #region PriorityWorkItemsRequestedCounterCollection class

    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='Tools.Coordination.Core.PriorityWorkItemsRequestedCounter'/> objects.
    ///    </para>
    /// </summary>
    [Serializable]
    public class PriorityWorkItemsRequestedCounterCollection : CollectionBase
    {
        #region Fields

        private readonly object _syncRootInsert = new object();

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Coordination.Core.PriorityWorkItemsRequestedCounterCollection'/>.
        ///    </para>
        /// </summary>
        public PriorityWorkItemsRequestedCounterCollection()
        {
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Coordination.Core.PriorityWorkItemsRequestedCounterCollection'/>.
        ///    </para>
        /// </summary>
        public PriorityWorkItemsRequestedCounterCollection(PriorityWorkItemsRequestedCounterCollection value)
        {
            AddRange(value);
        }


        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Coordination.Core.PriorityWorkItemsRequestedCounter'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='Tools.Coordination.Core.PriorityWorkItemsRequestedCounter'/> objects with which to intialize the collection
        /// </param>
        public PriorityWorkItemsRequestedCounterCollection(PriorityWorkItemsRequestedCounter[] value)
        {
            AddRange(value);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Synchronically gets PriorityWorkItemsRequestedCounter entries count in PriorityWorkItemsRequestedCounterCollection.
        /// </summary>
        public int SynchronizedCount
        {
            get
            {
                lock (this)
                {
                    return Count;
                }
            }
        }

        public object SyncRootInsert
        {
            get { return _syncRootInsert; }
        }

        #endregion

        #region Indexers

        /// <summary>
        /// <para>Represents the entry at the specified index of the <see cref='PriorityWorkItemsRequestedCounter'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public PriorityWorkItemsRequestedCounter this[int index]
        {
            get { return ((PriorityWorkItemsRequestedCounter) (List[index])); }
            set { List[index] = value; }
        }

        public PriorityWorkItemsRequestedCounter this[SubmissionPriority priority]
        {
            get
            {
                foreach (PriorityWorkItemsRequestedCounter qwi in this)
                {
                    if (qwi.SubmissionPriority == priority)
                    {
                        return qwi;
                    }
                }
                return null;
            }
            set
            {
                for (int i = 0; i < List.Count; i++)
                {
                    if (((PriorityWorkItemsRequestedCounter) List[i]).SubmissionPriority == priority)
                    {
                        List[i] = value;
                        return;
                    }
                }
                Add(value);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///    <para>Adds a <see cref='PriorityWorkItemsRequestedCounter'/> with the specified value to the 
        ///    <see cref='PriorityWorkItemsRequestedCounterCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='PriorityWorkItemsRequestedCounter'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='PriorityWorkItemsRequestedCounterCollection.AddRange'/>
        public int Add(PriorityWorkItemsRequestedCounter value)
        {
            return List.Add(value);
        }

        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='PriorityWorkItemsRequestedCounterCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='PriorityWorkItemsRequestedCounter'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='PriorityWorkItemsRequestedCounterCollection.Add'/>
        public void AddRange(PriorityWorkItemsRequestedCounter[] value)
        {
            for (int i = 0; (i < value.Length); i = (i + 1))
            {
                Add(value[i]);
            }
        }

        /// <summary>
        ///     <para>
        ///       Adds the contents of another <see cref='PriorityWorkItemsRequestedCounterCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='PriorityWorkItemsRequestedCounterCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='PriorityWorkItemsRequestedCounterCollection.Add'/>
        public void AddRange(PriorityWorkItemsRequestedCounterCollection value)
        {
            for (int i = 0; (i < value.Count); i = (i + 1))
            {
                Add(value[i]);
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='PriorityWorkItemsRequestedCounterCollection'/> contains the specified <see cref='PriorityWorkItemsRequestedCounter'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='PriorityWorkItemsRequestedCounter'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='PriorityWorkItemsRequestedCounter'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='PriorityWorkItemsRequestedCounterCollection.IndexOf'/>
        public bool Contains(PriorityWorkItemsRequestedCounter value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// <para>Copies the <see cref='PriorityWorkItemsRequestedCounterCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='PriorityWorkItemsRequestedCounterCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='PriorityWorkItemsRequestedCounterCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(PriorityWorkItemsRequestedCounter[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <summary>
        ///    <para>Returns the index of a <see cref='PriorityWorkItemsRequestedCounter'/> in 
        ///       the <see cref='PriorityWorkItemsRequestedCounterCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='PriorityWorkItemsRequestedCounter'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='PriorityWorkItemsRequestedCounter'/> of <paramref name='value'/> in the 
        /// <see cref='PriorityWorkItemsRequestedCounterCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='PriorityWorkItemsRequestedCounterCollection.Contains'/>
        public int IndexOf(PriorityWorkItemsRequestedCounter value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// <para>Inserts a <see cref='PriorityWorkItemsRequestedCounter'/> into the <see cref='PriorityWorkItemsRequestedCounterCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='PriorityWorkItemsRequestedCounter'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='PriorityWorkItemsRequestedCounterCollection.Add'/>
        public void Insert(int index, PriorityWorkItemsRequestedCounter value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='PriorityWorkItemsRequestedCounterCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new PriorityWorkItemsRequestedCounterEnumerator GetEnumerator()
        {
            return new PriorityWorkItemsRequestedCounterEnumerator(this);
        }

        /// <summary>
        ///    <para> Synchronically removes a specific <see cref='PriorityWorkItemsRequestedCounter'/> from the 
        ///    <see cref='PriorityWorkItemsRequestedCounterCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='PriorityWorkItemsRequestedCounter'/> to remove from the <see cref='PriorityWorkItemsRequestedCounterCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void SynchronizedRemove(PriorityWorkItemsRequestedCounter value)
        {
            lock (this)
            {
                List.Remove(value);
            }
        }

        /// <summary>
        ///    <para> Removes a specific <see cref='PriorityWorkItemsRequestedCounter'/> from the 
        ///    <see cref='PriorityWorkItemsRequestedCounterCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='PriorityWorkItemsRequestedCounter'/> to remove from the <see cref='PriorityWorkItemsRequestedCounterCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(PriorityWorkItemsRequestedCounter value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// Gets an entry for the supplied name.
        /// </summary>
        /// <param name="name">Entry name.</param>
        /// <returns>Entry if exists or null otherwise.</returns>
//		public  PriorityWorkItemsRequestedCounter GetEntry(string idHash)
//		{
//			PriorityWorkItemsRequestedCounterEnumerator ce = this.GetEnumerator();
//			
//			while (ce.MoveNext())
//			{
//				if (ce.Current.IdHash == idHash) return ce.Current;
//			}
//
//			return null;
//		}
        /// <summary>
        /// Get first PriorityWorkItemsRequestedCounter entry from PriorityWorkItemsRequestedCounterCollection and remove it.
        /// </summary>
        /// <returns>
        /// The first PriorityWorkItemsRequestedCounter entry from PriorityWorkItemsRequestedCounterCollection.
        /// </returns>
        public PriorityWorkItemsRequestedCounter GetFirstEntry()
        {
            PriorityWorkItemsRequestedCounter qwi = null;
            if (Count > 0)
            {
                qwi = this[0];
                RemoveAt(0);
            }

            return qwi;
        }

        #endregion

        #region PriorityWorkItemsRequestedCounterEnumerator class

        public class PriorityWorkItemsRequestedCounterEnumerator : object, IEnumerator
        {
            #region Fields

            private readonly IEnumerator baseEnumerator;
            private readonly IEnumerable temp;

            #endregion

            #region Constructors

            public PriorityWorkItemsRequestedCounterEnumerator(PriorityWorkItemsRequestedCounterCollection mappings)
            {
                temp = ((mappings));
                baseEnumerator = temp.GetEnumerator();
            }

            #endregion

            #region Properties

            public PriorityWorkItemsRequestedCounter Current
            {
                get { return ((PriorityWorkItemsRequestedCounter) (baseEnumerator.Current)); }
            }

            #endregion

            #region IEnumerator implementation

            object IEnumerator.Current
            {
                get { return baseEnumerator.Current; }
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

    #endregion
}