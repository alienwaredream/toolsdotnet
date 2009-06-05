using System;
using System.Collections;

namespace Tools.Failover
{
    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='Tools.Core.configuration.FailureConfiguration'/> objects.
    ///    </para>
    /// </summary>
    /// <seealso cref='Tools.Core.configuration.FailureConfigurationCollection'/>
    [Serializable]
    public class FailureConfigurationCollection : CollectionBase
    {
        #region Constructors

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.FailureConfigurationCollection'/>.
        ///    </para>
        /// </summary>
        public FailureConfigurationCollection()
        {
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.FailureConfigurationCollection'/> based on another <see cref='Tools.Core.configuration.FailureConfigurationCollection'/>.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A <see cref='Tools.Core.configuration.FailureConfigurationCollection'/> from which the contents are copied
        /// </param>
        public FailureConfigurationCollection(FailureConfigurationCollection value)
        {
            AddRange(value);
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.FailureConfigurationCollection'/> containing any array of <see cref='Tools.Core.configuration.FailureConfiguration'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='Tools.Core.configuration.FailureConfiguration'/> objects with which to intialize the collection
        /// </param>
        public FailureConfigurationCollection(FailureConfiguration[] value)
        {
            AddRange(value);
        }

        #endregion

        #region Indexers

        /// <summary>
        /// <para>Represents the entry at the specified index of the <see cref='Tools.Core.configuration.FailureConfiguration'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public FailureConfiguration this[int index]
        {
            get { return ((FailureConfiguration) (List[index])); }
            set { List[index] = value; }
        }

        public FailureConfiguration this[string name]
        {
            get
            {
                foreach (FailureConfiguration fdf in this)
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
                    if (((FailureConfiguration) List[i]).Name == name)
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
        ///    <para>Adds a <see cref='Tools.Core.configuration.FailureConfiguration'/> with the specified value to the 
        ///    <see cref='Tools.Core.configuration.FailureConfigurationCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.FailureConfiguration'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.FailureConfigurationCollection.AddRange'/>
        public int Add(FailureConfiguration value)
        {
            return List.Add(value);
        }

        /// <summary>
        /// <para>Merges the elements of an array to the end of the <see cref='Tools.Core.configuration.FailureConfigurationCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='Tools.Core.configuration.FailureConfiguration'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.FailureConfigurationCollection.Add'/>
        public void AddRange(FailureConfiguration[] value)
        {
            for (int i = 0; (i < value.Length); i = (i + 1))
            {
                FailureConfiguration nv = GetEntry(value[i].Name);
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
        ///       Adds the contents of another <see cref='Tools.Core.configuration.FailureConfigurationCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='Tools.Core.configuration.FailureConfigurationCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.FailureConfigurationCollection.Add'/>
        public void AddRange(FailureConfigurationCollection value)
        {
            for (int i = 0; (i < value.Count); i = (i + 1))
            {
                Add(value[i]);
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='Tools.Core.configuration.FailureConfigurationCollection'/> contains the specified <see cref='Tools.Core.configuration.FailureConfiguration'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.FailureConfiguration'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='Tools.Core.configuration.FailureConfiguration'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.FailureConfigurationCollection.IndexOf'/>
        public bool Contains(FailureConfiguration value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// <para>Copies the <see cref='Tools.Core.configuration.FailureConfigurationCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='Tools.Core.configuration.FailureConfigurationCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='Tools.Core.configuration.FailureConfigurationCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(FailureConfiguration[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <summary>
        ///    <para>Returns the index of a <see cref='Tools.Core.configuration.FailureConfiguration'/> in 
        ///       the <see cref='Tools.Core.configuration.FailureConfigurationCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.FailureConfiguration'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='Tools.Core.configuration.FailureConfiguration'/> of <paramref name='value'/> in the 
        /// <see cref='Tools.Core.configuration.FailureConfigurationCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.FailureConfigurationCollection.Contains'/>
        public int IndexOf(FailureConfiguration value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// <para>Inserts a <see cref='Tools.Core.configuration.FailureConfiguration'/> into the <see cref='Tools.Core.configuration.FailureConfigurationCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='Tools.Core.configuration.FailureConfiguration'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='Tools.Core.configuration.FailureConfigurationCollection.Add'/>
        public void Insert(int index, FailureConfiguration value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='Tools.Core.configuration.FailureConfigurationCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new FailureConfigurationEnumerator GetEnumerator()
        {
            return new FailureConfigurationEnumerator(this);
        }

        /// <summary>
        ///    <para> Removes a specific <see cref='Tools.Core.configuration.FailureConfiguration'/> from the 
        ///    <see cref='Tools.Core.configuration.FailureConfigurationCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.FailureConfiguration'/> to remove from the <see cref='Tools.Core.configuration.FailureConfigurationCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(FailureConfiguration value)
        {
            List.Remove(value);
        }

        /// <summary>
        /// Gets an entry for the supplied name.
        /// </summary>
        /// <param name="name">Entry name.</param>
        /// <returns>Entry if exists or null otherwise.</returns>
        public FailureConfiguration GetEntry(string name)
        {
            FailureConfigurationEnumerator ce = GetEnumerator();
            while (ce.MoveNext())
            {
                if (ce.Current.Name == name) return ce.Current;
            }
            return null;
        }

        #endregion

        #region FailureConfigurationEnumerator class

        public class FailureConfigurationEnumerator : object, IEnumerator
        {
            #region Fields

            private readonly IEnumerator baseEnumerator;
            private readonly IEnumerable temp;

            #endregion

            #region Constructors

            public FailureConfigurationEnumerator(FailureConfigurationCollection mappings)
            {
                temp = ((mappings));
                baseEnumerator = temp.GetEnumerator();
            }

            #endregion

            #region Properties

            public FailureConfiguration Current
            {
                get { return ((FailureConfiguration) (baseEnumerator.Current)); }
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