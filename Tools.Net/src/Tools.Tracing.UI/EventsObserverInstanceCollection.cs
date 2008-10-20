using System;
using System.Collections;

namespace Tools.Tracing.UI
{
    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='Tools..EventsObserverInstance'/> objects.
    ///    </para>
    /// </summary>
    /// <seealso cref='Tools..EventsObserverInstanceCollection'/>
    [Serializable]
    public class EventsObserverInstanceCollection : CollectionBase
    {
        private int defaultConnectionIndex = 1;
        private string defaultConnectionName = "Connection";

        public string GetDefaultConnectionName() // Given this, it might be moved to Component smth. (SD)
        {
            for (int i = defaultConnectionIndex; i < 200; i++)
            {
                string nameCandidate = defaultConnectionName + i;

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
        ///       Initializes a new instance of <see cref='Tools..EventsObserverInstanceCollection'/>.
        ///    </para>
        /// </summary>
        public EventsObserverInstanceCollection()
        {
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools..EventsObserverInstanceCollection'/> based on another <see cref='Tools..EventsObserverInstanceCollection'/>.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A <see cref='Tools..EventsObserverInstanceCollection'/> from which the contents are copied
        /// </param>
        public EventsObserverInstanceCollection(EventsObserverInstanceCollection value)
        {
            AddRange(value);
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools..EventsObserverInstanceCollection'/> containing any array of <see cref='Tools..EventsObserverInstance'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='Tools..EventsObserverInstance'/> objects with which to intialize the collection
        /// </param>
        public EventsObserverInstanceCollection(EventsObserverInstance[] value)
        {
            AddRange(value);
        }

        #endregion

        #region Indexers

        /// <summary>
        /// <para>Represents the entry at the specified index of the <see cref='Tools..EventsObserverInstance'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public EventsObserverInstance this[int index]
        {
            get { return ((EventsObserverInstance) (List[index])); }
            set { List[index] = value; }
        }

        public EventsObserverInstance this[string name]
        {
            get
            {
                foreach (EventsObserverInstance fdf in this)
                {
                    if (fdf.Configuration.Name == name)
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
                    if (((EventsObserverInstance) List[i]).Configuration.Name == name)
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
        ///    <para>Adds a <see cref='Tools..EventsObserverInstance'/> with the specified value to the 
        ///    <see cref='Tools..EventsObserverInstanceCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools..EventsObserverInstance'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='Tools..EventsObserverInstanceCollection.AddRange'/>
        public int Add(EventsObserverInstance value)
        {
            return List.Add(value);
        }

        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='Tools..EventsObserverInstanceCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='Tools..EventsObserverInstance'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools..EventsObserverInstanceCollection.Add'/>
        public void AddRange(EventsObserverInstance[] value)
        {
            for (int i = 0; (i < value.Length); i = (i + 1))
            {
                EventsObserverInstance nv = GetEntry(value[i].Configuration.Name);
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
        ///       Adds the contents of another <see cref='Tools..EventsObserverInstanceCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='Tools..EventsObserverInstanceCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools..EventsObserverInstanceCollection.Add'/>
        public void AddRange(EventsObserverInstanceCollection value)
        {
            for (int i = 0; (i < value.Count); i = (i + 1))
            {
                Add(value[i]);
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='Tools..EventsObserverInstanceCollection'/> contains the specified <see cref='Tools..EventsObserverInstance'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools..EventsObserverInstance'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='Tools..EventsObserverInstance'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='Tools..EventsObserverInstanceCollection.IndexOf'/>
        public bool Contains(EventsObserverInstance value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// <para>Copies the <see cref='Tools..EventsObserverInstanceCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='Tools..EventsObserverInstanceCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='Tools..EventsObserverInstanceCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(EventsObserverInstance[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <summary>
        ///    <para>Returns the index of a <see cref='Tools..EventsObserverInstance'/> in 
        ///       the <see cref='Tools..EventsObserverInstanceCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools..EventsObserverInstance'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='Tools..EventsObserverInstance'/> of <paramref name='value'/> in the 
        /// <see cref='Tools..EventsObserverInstanceCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='Tools..EventsObserverInstanceCollection.Contains'/>
        public int IndexOf(EventsObserverInstance value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// <para>Inserts a <see cref='Tools..EventsObserverInstance'/> into the <see cref='Tools..EventsObserverInstanceCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='Tools..EventsObserverInstance'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='Tools..EventsObserverInstanceCollection.Add'/>
        public void Insert(int index, EventsObserverInstance value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='Tools..EventsObserverInstanceCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new EventsObserverInstanceEnumerator GetEnumerator()
        {
            return new EventsObserverInstanceEnumerator(this);
        }

        /// <summary>
        ///    <para> Removes a specific <see cref='Tools..EventsObserverInstance'/> from the 
        ///    <see cref='Tools..EventsObserverInstanceCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools..EventsObserverInstance'/> to remove from the <see cref='Tools..EventsObserverInstanceCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(EventsObserverInstance value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// Gets an entry for the supplied name.
        /// </summary>
        /// <param name="name">Entry name.</param>
        /// <returns>Entry if exists or null otherwise.</returns>
        public EventsObserverInstance GetEntry(string name)
        {
            EventsObserverInstanceEnumerator ce = GetEnumerator();
            while (ce.MoveNext())
            {
                if (ce.Current.Configuration.Name == name) return ce.Current;
            }
            return null;
        }

        #endregion

        #region EventsObserverInstanceEnumerator class

        public class EventsObserverInstanceEnumerator : object, IEnumerator
        {
            #region Global declarations

            private readonly IEnumerator baseEnumerator;
            private readonly IEnumerable temp;

            #endregion

            #region Constructors

            public EventsObserverInstanceEnumerator(EventsObserverInstanceCollection mappings)
            {
                temp = ((mappings));
                baseEnumerator = temp.GetEnumerator();
            }

            #endregion

            #region Properties

            public EventsObserverInstance Current
            {
                get { return ((EventsObserverInstance) (baseEnumerator.Current)); }
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