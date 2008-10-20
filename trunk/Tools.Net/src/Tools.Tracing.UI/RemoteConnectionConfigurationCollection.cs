using System;
using System.Collections;

namespace Tools.Tracing.UI
{
    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='Tools..RemoteConnectionConfiguration'/> objects.
    ///    </para>
    /// </summary>
    /// <seealso cref='Tools..RemoteConnectionConfigurationCollection'/>
    [Serializable]
    public class RemoteConnectionConfigurationCollection : CollectionBase
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
        ///       Initializes a new instance of <see cref='Tools..RemoteConnectionConfigurationCollection'/>.
        ///    </para>
        /// </summary>
        public RemoteConnectionConfigurationCollection()
        {
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools..RemoteConnectionConfigurationCollection'/> based on another <see cref='Tools..RemoteConnectionConfigurationCollection'/>.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A <see cref='Tools..RemoteConnectionConfigurationCollection'/> from which the contents are copied
        /// </param>
        public RemoteConnectionConfigurationCollection(RemoteConnectionConfigurationCollection value)
        {
            AddRange(value);
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools..RemoteConnectionConfigurationCollection'/> containing any array of <see cref='Tools..RemoteConnectionConfiguration'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='Tools..RemoteConnectionConfiguration'/> objects with which to intialize the collection
        /// </param>
        public RemoteConnectionConfigurationCollection(RemoteConnectionConfiguration[] value)
        {
            AddRange(value);
        }

        #endregion

        #region Indexers

        /// <summary>
        /// <para>Represents the entry at the specified index of the <see cref='Tools..RemoteConnectionConfiguration'/>.</para>
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
            get { return ((RemoteConnectionConfiguration) (List[index])); }
            set { List[index] = value; }
        }

        public RemoteConnectionConfiguration this[string name]
        {
            get
            {
                foreach (RemoteConnectionConfiguration fdf in this)
                {
                    if (fdf.Name == name)
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
                    if (((RemoteConnectionConfiguration) List[i]).Name == name)
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
        ///    <para>Adds a <see cref='Tools..RemoteConnectionConfiguration'/> with the specified value to the 
        ///    <see cref='Tools..RemoteConnectionConfigurationCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools..RemoteConnectionConfiguration'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='Tools..RemoteConnectionConfigurationCollection.AddRange'/>
        public int Add(RemoteConnectionConfiguration value)
        {
            return List.Add(value);
        }

        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='Tools..RemoteConnectionConfigurationCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='Tools..RemoteConnectionConfiguration'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools..RemoteConnectionConfigurationCollection.Add'/>
        public void AddRange(RemoteConnectionConfiguration[] value)
        {
            for (int i = 0; (i < value.Length); i = (i + 1))
            {
                RemoteConnectionConfiguration nv = GetEntry(value[i].Name);
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
        ///       Adds the contents of another <see cref='Tools..RemoteConnectionConfigurationCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='Tools..RemoteConnectionConfigurationCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools..RemoteConnectionConfigurationCollection.Add'/>
        public void AddRange(RemoteConnectionConfigurationCollection value)
        {
            for (int i = 0; (i < value.Count); i = (i + 1))
            {
                Add(value[i]);
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='Tools..RemoteConnectionConfigurationCollection'/> contains the specified <see cref='Tools..RemoteConnectionConfiguration'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools..RemoteConnectionConfiguration'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='Tools..RemoteConnectionConfiguration'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='Tools..RemoteConnectionConfigurationCollection.IndexOf'/>
        public bool Contains(RemoteConnectionConfiguration value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// <para>Copies the <see cref='Tools..RemoteConnectionConfigurationCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='Tools..RemoteConnectionConfigurationCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='Tools..RemoteConnectionConfigurationCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(RemoteConnectionConfiguration[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <summary>
        ///    <para>Returns the index of a <see cref='Tools..RemoteConnectionConfiguration'/> in 
        ///       the <see cref='Tools..RemoteConnectionConfigurationCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools..RemoteConnectionConfiguration'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='Tools..RemoteConnectionConfiguration'/> of <paramref name='value'/> in the 
        /// <see cref='Tools..RemoteConnectionConfigurationCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='Tools..RemoteConnectionConfigurationCollection.Contains'/>
        public int IndexOf(RemoteConnectionConfiguration value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// <para>Inserts a <see cref='Tools..RemoteConnectionConfiguration'/> into the <see cref='Tools..RemoteConnectionConfigurationCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='Tools..RemoteConnectionConfiguration'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='Tools..RemoteConnectionConfigurationCollection.Add'/>
        public void Insert(int index, RemoteConnectionConfiguration value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='Tools..RemoteConnectionConfigurationCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new RemoteConnectionConfigurationEnumerator GetEnumerator()
        {
            return new RemoteConnectionConfigurationEnumerator(this);
        }

        /// <summary>
        ///    <para> Removes a specific <see cref='Tools..RemoteConnectionConfiguration'/> from the 
        ///    <see cref='Tools..RemoteConnectionConfigurationCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools..RemoteConnectionConfiguration'/> to remove from the <see cref='Tools..RemoteConnectionConfigurationCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(RemoteConnectionConfiguration value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// Gets an entry for the supplied name.
        /// </summary>
        /// <param name="name">Entry name.</param>
        /// <returns>Entry if exists or null otherwise.</returns>
        public RemoteConnectionConfiguration GetEntry(string name)
        {
            RemoteConnectionConfigurationEnumerator ce = GetEnumerator();
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

            private readonly IEnumerator baseEnumerator;
            private readonly IEnumerable temp;

            #endregion

            #region Constructors

            public RemoteConnectionConfigurationEnumerator(RemoteConnectionConfigurationCollection mappings)
            {
                temp = ((mappings));
                baseEnumerator = temp.GetEnumerator();
            }

            #endregion

            #region Properties

            public RemoteConnectionConfiguration Current
            {
                get { return ((RemoteConnectionConfiguration) (baseEnumerator.Current)); }
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