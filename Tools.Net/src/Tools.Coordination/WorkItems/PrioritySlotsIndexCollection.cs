using System;
using System.Collections;

namespace Tools.Coordination.WorkItems
{

    #region PrioritySlotsIndexCollection class

    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='PrioritySlotsIndex'/> objects.
    ///    </para>
    /// </summary>
    [Serializable]
    public class PrioritySlotsIndexCollection : CollectionBase
    {
        #region Fields

        private readonly object _syncRootInsert = new object();

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Coordination.Core.PrioritySlotsIndexCollection'/>.
        ///    </para>
        /// </summary>
        public PrioritySlotsIndexCollection()
        {
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Coordination.Core.PrioritySlotsIndexCollection'/>.
        ///    </para>
        /// </summary>
        public PrioritySlotsIndexCollection(PrioritySlotsIndexCollection value)
        {
            AddRange(value);
        }


        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Coordination.Core.PrioritySlotsIndex'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='Tools.Coordination.Core.PrioritySlotsIndex'/> objects with which to intialize the collection
        /// </param>
        public PrioritySlotsIndexCollection(PrioritySlotsIndex[] value)
        {
            AddRange(value);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Synchronically gets PrioritySlotsIndex entries count in PrioritySlotsIndexCollection.
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
        /// <para>Represents the entry at the specified index of the <see cref='PrioritySlotsIndex'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public PrioritySlotsIndex this[int index]
        {
            get { return ((PrioritySlotsIndex) (List[index])); }
            set { List[index] = value; }
        }

        public PrioritySlotsIndex this[SubmissionPriority priority]
        {
            get
            {
                foreach (PrioritySlotsIndex qwi in this)
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
                    if (((PrioritySlotsIndex) List[i]).SubmissionPriority == priority)
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
        ///    <para>Adds a <see cref='PrioritySlotsIndex'/> with the specified value to the 
        ///    <see cref='PrioritySlotsIndexCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='PrioritySlotsIndex'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='PrioritySlotsIndexCollection.AddRange'/>
        public int Add(PrioritySlotsIndex value)
        {
            return List.Add(value);
        }

        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='PrioritySlotsIndexCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='PrioritySlotsIndex'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='PrioritySlotsIndexCollection.Add'/>
        public void AddRange(PrioritySlotsIndex[] value)
        {
            for (int i = 0; (i < value.Length); i = (i + 1))
            {
                Add(value[i]);
            }
        }

        /// <summary>
        ///     <para>
        ///       Adds the contents of another <see cref='PrioritySlotsIndexCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='PrioritySlotsIndexCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='PrioritySlotsIndexCollection.Add'/>
        public void AddRange(PrioritySlotsIndexCollection value)
        {
            for (int i = 0; (i < value.Count); i = (i + 1))
            {
                Add(value[i]);
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='PrioritySlotsIndexCollection'/> contains the specified <see cref='PrioritySlotsIndex'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='PrioritySlotsIndex'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='PrioritySlotsIndex'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='PrioritySlotsIndexCollection.IndexOf'/>
        public bool Contains(PrioritySlotsIndex value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// <para>Copies the <see cref='PrioritySlotsIndexCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='PrioritySlotsIndexCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='PrioritySlotsIndexCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(PrioritySlotsIndex[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <summary>
        ///    <para>Returns the index of a <see cref='PrioritySlotsIndex'/> in 
        ///       the <see cref='PrioritySlotsIndexCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='PrioritySlotsIndex'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='PrioritySlotsIndex'/> of <paramref name='value'/> in the 
        /// <see cref='PrioritySlotsIndexCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='PrioritySlotsIndexCollection.Contains'/>
        public int IndexOf(PrioritySlotsIndex value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// <para>Inserts a <see cref='PrioritySlotsIndex'/> into the <see cref='PrioritySlotsIndexCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='PrioritySlotsIndex'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='PrioritySlotsIndexCollection.Add'/>
        public void Insert(int index, PrioritySlotsIndex value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='PrioritySlotsIndexCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new PrioritySlotsIndexEnumerator GetEnumerator()
        {
            return new PrioritySlotsIndexEnumerator(this);
        }

        /// <summary>
        ///    <para> Synchronically removes a specific <see cref='PrioritySlotsIndex'/> from the 
        ///    <see cref='PrioritySlotsIndexCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='PrioritySlotsIndex'/> to remove from the <see cref='PrioritySlotsIndexCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void SynchronizedRemove(PrioritySlotsIndex value)
        {
            lock (this)
            {
                List.Remove(value);
            }
        }

        /// <summary>
        ///    <para> Removes a specific <see cref='PrioritySlotsIndex'/> from the 
        ///    <see cref='PrioritySlotsIndexCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='PrioritySlotsIndex'/> to remove from the <see cref='PrioritySlotsIndexCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(PrioritySlotsIndex value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// Gets an entry for the supplied name.
        /// </summary>
        /// <param name="name">Entry name.</param>
        /// <returns>Entry if exists or null otherwise.</returns>
//		public  PrioritySlotsIndex GetEntry(string idHash)
//		{
//			PrioritySlotsIndexEnumerator ce = this.GetEnumerator();
//			
//			while (ce.MoveNext())
//			{
//				if (ce.Current.IdHash == idHash) return ce.Current;
//			}
//
//			return null;
//		}
        /// <summary>
        /// Get first PrioritySlotsIndex entry from PrioritySlotsIndexCollection and remove it.
        /// </summary>
        /// <returns>
        /// The first PrioritySlotsIndex entry from PrioritySlotsIndexCollection.
        /// </returns>
        public PrioritySlotsIndex GetFirstEntry()
        {
            PrioritySlotsIndex qwi = null;
            if (Count > 0)
            {
                qwi = this[0];
                RemoveAt(0);
            }

            return qwi;
        }

        #endregion

        #region PrioritySlotsIndexEnumerator class

        public class PrioritySlotsIndexEnumerator : object, IEnumerator
        {
            #region Fields

            private readonly IEnumerator baseEnumerator;
            private readonly IEnumerable temp;

            #endregion

            #region Constructors

            public PrioritySlotsIndexEnumerator(PrioritySlotsIndexCollection mappings)
            {
                temp = ((mappings));
                baseEnumerator = temp.GetEnumerator();
            }

            #endregion

            #region Properties

            public PrioritySlotsIndex Current
            {
                get { return ((PrioritySlotsIndex) (baseEnumerator.Current)); }
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