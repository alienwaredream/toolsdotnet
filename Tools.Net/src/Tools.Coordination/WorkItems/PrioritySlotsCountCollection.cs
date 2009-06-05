using System;
using System.Collections;

namespace Tools.Coordination.WorkItems
{

    #region PrioritySlotsCountCollection class

    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='Tools.Coordination.Core.PrioritySlotsCount'/> objects.
    ///    </para>
    /// </summary>
    [Serializable]
    public class PrioritySlotsCountCollection : CollectionBase
    {
        #region Fields

        private readonly object _syncRootInsert = new object();

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Coordination.Core.PrioritySlotsCountCollection'/>.
        ///    </para>
        /// </summary>
        public PrioritySlotsCountCollection()
        {
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Coordination.Core.PrioritySlotsCountCollection'/>.
        ///    </para>
        /// </summary>
        public PrioritySlotsCountCollection(PrioritySlotsCountCollection value)
        {
            AddRange(value);
        }


        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Coordination.Core.PrioritySlotsCount'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='Tools.Coordination.Core.PrioritySlotsCount'/> objects with which to intialize the collection
        /// </param>
        public PrioritySlotsCountCollection(PrioritySlotsConfiguration[] value)
        {
            AddRange(value);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Synchronically gets PrioritySlotsConfiguration entries count in PrioritySlotsCountCollection.
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
        /// <para>Represents the entry at the specified index of the <see cref='PrioritySlotsConfiguration'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public PrioritySlotsConfiguration this[int index]
        {
            get { return ((PrioritySlotsConfiguration) (List[index])); }
            set { List[index] = value; }
        }

        public PrioritySlotsConfiguration this[SubmissionPriority priority]
        {
            get
            {
                foreach (PrioritySlotsConfiguration qwi in this)
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
                    if (((PrioritySlotsConfiguration) List[i]).SubmissionPriority == priority)
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
        ///    <para>Adds a <see cref='PrioritySlotsConfiguration'/> with the specified value to the 
        ///    <see cref='PrioritySlotsCountCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='PrioritySlotsConfiguration'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='PrioritySlotsCountCollection.AddRange'/>
        public int Add(PrioritySlotsConfiguration value)
        {
            return List.Add(value);
        }

        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='PrioritySlotsCountCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='PrioritySlotsConfiguration'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='PrioritySlotsCountCollection.Add'/>
        public void AddRange(PrioritySlotsConfiguration[] value)
        {
            for (int i = 0; (i < value.Length); i = (i + 1))
            {
                Add(value[i]);
            }
        }

        /// <summary>
        ///     <para>
        ///       Adds the contents of another <see cref='PrioritySlotsCountCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='PrioritySlotsCountCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='PrioritySlotsCountCollection.Add'/>
        public void AddRange(PrioritySlotsCountCollection value)
        {
            for (int i = 0; (i < value.Count); i = (i + 1))
            {
                Add(value[i]);
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='PrioritySlotsCountCollection'/> contains the specified <see cref='PrioritySlotsConfiguration'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='PrioritySlotsConfiguration'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='PrioritySlotsConfiguration'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='PrioritySlotsCountCollection.IndexOf'/>
        public bool Contains(PrioritySlotsConfiguration value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// <para>Copies the <see cref='PrioritySlotsCountCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='PrioritySlotsCountCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='PrioritySlotsCountCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(PrioritySlotsConfiguration[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <summary>
        ///    <para>Returns the index of a <see cref='PrioritySlotsConfiguration'/> in 
        ///       the <see cref='PrioritySlotsCountCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='PrioritySlotsConfiguration'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='PrioritySlotsConfiguration'/> of <paramref name='value'/> in the 
        /// <see cref='PrioritySlotsCountCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='PrioritySlotsCountCollection.Contains'/>
        public int IndexOf(PrioritySlotsConfiguration value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// <para>Inserts a <see cref='PrioritySlotsConfiguration'/> into the <see cref='PrioritySlotsCountCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='PrioritySlotsConfiguration'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='PrioritySlotsCountCollection.Add'/>
        public void Insert(int index, PrioritySlotsConfiguration value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='PrioritySlotsCountCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new PrioritySlotsCountEnumerator GetEnumerator()
        {
            return new PrioritySlotsCountEnumerator(this);
        }

        /// <summary>
        ///    <para> Synchronically removes a specific <see cref='PrioritySlotsConfiguration'/> from the 
        ///    <see cref='PrioritySlotsCountCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='PrioritySlotsConfiguration'/> to remove from the <see cref='PrioritySlotsCountCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void SynchronizedRemove(PrioritySlotsConfiguration value)
        {
            lock (this)
            {
                List.Remove(value);
            }
        }

        /// <summary>
        ///    <para> Removes a specific <see cref='PrioritySlotsConfiguration'/> from the 
        ///    <see cref='PrioritySlotsCountCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='PrioritySlotsConfiguration'/> to remove from the <see cref='PrioritySlotsCountCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(PrioritySlotsConfiguration value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// Gets an entry for the supplied name.
        /// </summary>
        /// <param name="name">Entry name.</param>
        /// <returns>Entry if exists or null otherwise.</returns>
//		public  PrioritySlotsConfiguration GetEntry(string idHash)
//		{
//			PrioritySlotsCountEnumerator ce = this.GetEnumerator();
//			
//			while (ce.MoveNext())
//			{
//				if (ce.Current.IdHash == idHash) return ce.Current;
//			}
//
//			return null;
//		}
        /// <summary>
        /// Get first PrioritySlotsConfiguration entry from PrioritySlotsCountCollection and remove it.
        /// </summary>
        /// <returns>
        /// The first PrioritySlotsConfiguration entry from PrioritySlotsCountCollection.
        /// </returns>
        public PrioritySlotsConfiguration GetFirstEntry()
        {
            PrioritySlotsConfiguration qwi = null;
            if (Count > 0)
            {
                qwi = this[0];
                RemoveAt(0);
            }

            return qwi;
        }

        #endregion

        #region PrioritySlotsCountEnumerator class

        public class PrioritySlotsCountEnumerator : object, IEnumerator
        {
            #region Fields

            private readonly IEnumerator baseEnumerator;
            private readonly IEnumerable temp;

            #endregion

            #region Constructors

            public PrioritySlotsCountEnumerator(PrioritySlotsCountCollection mappings)
            {
                temp = ((mappings));
                baseEnumerator = temp.GetEnumerator();
            }

            #endregion

            #region Properties

            public PrioritySlotsConfiguration Current
            {
                get { return ((PrioritySlotsConfiguration) (baseEnumerator.Current)); }
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