using System;
using System.Collections;
using Tools.Coordination.WorkItems;

namespace Tools.Coordination.WorkItems
{
    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='QueueWorkItem'/> objects.
    ///    </para>
    /// </summary>
    [Serializable]
    public class QueueWorkItemCollection : CollectionBase
    {
        #region Fields

        private readonly object _syncRootInsert = new object();

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='QueueWorkItemCollection'/>.
        ///    </para>
        /// </summary>
        public QueueWorkItemCollection()
        {
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='QueueWorkItemCollection'/>.
        ///    </para>
        /// </summary>
        public QueueWorkItemCollection(QueueWorkItemCollection value)
        {
            AddRange(value);
        }


        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='QueueWorkItem'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='QueueWorkItem'/> objects with which to intialize the collection
        /// </param>
        public QueueWorkItemCollection(QueueWorkItem[] value)
        {
            AddRange(value);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Synchronically gets QueueWorkItem entries count in QueueWorkItemCollection.
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
        /// <para>Represents the entry at the specified index of the <see cref='QueueWorkItem'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public QueueWorkItem this[int index]
        {
            get { return ((QueueWorkItem) (List[index])); }
            set { List[index] = value; }
        }

        /// <summary>
        /// <para>Represents the <see cref='QueueWorkItem'/> entry with specified Id.</para>
        /// </summary>
        /// <param name='index'><para>The Id of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> 
        ///		The <see cref='QueueWorkItem'/> entry with specified Id. 
        ///    </para>
        /// </value>
        public QueueWorkItem this[string messageId]
        {
            get
            {
                foreach (QueueWorkItem qwi in this)
                {
                    if (qwi.MessageId == messageId)
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
                    if (((QueueWorkItem) List[i]).MessageId == messageId)
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
        ///    <para>Adds a <see cref='QueueWorkItem'/> with the specified value to the 
        ///    <see cref='QueueWorkItemCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='QueueWorkItem'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='QueueWorkItemCollection.AddRange'/>
        public int Add(QueueWorkItem value)
        {
            return List.Add(value);
        }

        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='QueueWorkItemCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='QueueWorkItem'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='QueueWorkItemCollection.Add'/>
        public void AddRange(QueueWorkItem[] value)
        {
            for (int i = 0; (i < value.Length); i = (i + 1))
            {
                QueueWorkItem nv = GetEntry(value[i].MessageId);
                if (nv != null)
                {
                    nv = value[i];
                }
                else
                {
                    Add(value[i]);
                }
            }
        }

        /// <summary>
        ///     <para>
        ///       Adds the contents of another <see cref='QueueWorkItemCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='QueueWorkItemCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='QueueWorkItemCollection.Add'/>
        public void AddRange(QueueWorkItemCollection value)
        {
            for (int i = 0; (i < value.Count); i = (i + 1))
            {
                Add(value[i]);
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='QueueWorkItemCollection'/> contains the specified <see cref='QueueWorkItem'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='QueueWorkItem'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='QueueWorkItem'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='QueueWorkItemCollection.IndexOf'/>
        public bool Contains(QueueWorkItem value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// <para>Copies the <see cref='QueueWorkItemCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='QueueWorkItemCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='QueueWorkItemCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(QueueWorkItem[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <summary>
        ///    <para>Returns the index of a <see cref='QueueWorkItem'/> in 
        ///       the <see cref='QueueWorkItemCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='QueueWorkItem'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='QueueWorkItem'/> of <paramref name='value'/> in the 
        /// <see cref='QueueWorkItemCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='QueueWorkItemCollection.Contains'/>
        public int IndexOf(QueueWorkItem value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// <para>Inserts a <see cref='QueueWorkItem'/> into the <see cref='QueueWorkItemCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='QueueWorkItem'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='QueueWorkItemCollection.Add'/>
        public void Insert(int index, QueueWorkItem value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='QueueWorkItemCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new QueueWorkItemEnumerator GetEnumerator()
        {
            return new QueueWorkItemEnumerator(this);
        }

        /// <summary>
        ///    <para> Synchronically removes a specific <see cref='QueueWorkItem'/> from the 
        ///    <see cref='QueueWorkItemCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='QueueWorkItem'/> to remove from the <see cref='QueueWorkItemCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void SynchronizedRemove(QueueWorkItem value)
        {
            lock (this)
            {
                List.Remove(value);
            }
        }

        /// <summary>
        ///    <para> Removes a specific <see cref='QueueWorkItem'/> from the 
        ///    <see cref='QueueWorkItemCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='QueueWorkItem'/> to remove from the <see cref='QueueWorkItemCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(QueueWorkItem value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// Gets an entry for the supplied name.
        /// </summary>
        /// <param name="name">Entry name.</param>
        /// <returns>Entry if exists or null otherwise.</returns>
        public QueueWorkItem GetEntry(string messageId)
        {
            QueueWorkItemEnumerator ce = GetEnumerator();

            while (ce.MoveNext())
            {
                if (ce.Current.MessageId == messageId) return ce.Current;
            }

            return null;
        }


        /// <summary>
        /// Get first QueueWorkItem entry from QueueWorkItemCollection and remove it.
        /// </summary>
        /// <returns>
        /// The first QueueWorkItem entry from QueueWorkItemCollection.
        /// </returns>
        public QueueWorkItem GetFirstEntry()
        {
            QueueWorkItem qwi = null;
            if (Count > 0)
            {
                qwi = this[0];
                RemoveAt(0);
            }

            return qwi;
        }

        #endregion

        #region QueueWorkItemEnumerator class

        public class QueueWorkItemEnumerator : object, IEnumerator
        {
            #region Fields

            private readonly IEnumerator baseEnumerator;
            private readonly IEnumerable temp;

            #endregion

            #region Constructors

            public QueueWorkItemEnumerator(QueueWorkItemCollection mappings)
            {
                temp = ((mappings));
                baseEnumerator = temp.GetEnumerator();
            }

            #endregion

            #region Properties

            public QueueWorkItem Current
            {
                get { return ((QueueWorkItem) (baseEnumerator.Current)); }
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
}