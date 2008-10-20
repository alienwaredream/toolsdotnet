using System;
using System.Collections;

namespace Tools.Tracing.Common
{
    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='Tools.Core.configuration.IApplicationEventFilter'/> objects.
    ///    </para>
    /// </summary>
    /// <seealso cref='Tools.Core.configuration.IApplicationEventFilterCollection'/>
    [Serializable]
    public class ITraceEventFilterCollection : CollectionBase
    {
        #region Constructors

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/>.
        ///    </para>
        /// </summary>
        public ITraceEventFilterCollection()
        {
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/> based on another <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/>.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/> from which the contents are copied
        /// </param>
        public ITraceEventFilterCollection(ITraceEventFilterCollection value)
        {
            AddRange(value);
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/> containing any array of <see cref='Tools.Core.configuration.IApplicationEventFilter'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='Tools.Core.configuration.IApplicationEventFilter'/> objects with which to intialize the collection
        /// </param>
        public ITraceEventFilterCollection(ITraceEventFilter[] value)
        {
            AddRange(value);
        }

        #endregion

        #region Indexers

        /// <summary>
        /// <para>Represents the entry at the specified index of the <see cref='Tools.Core.configuration.IApplicationEventFilter'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public ITraceEventFilter this[int index]
        {
            get { return ((ITraceEventFilter) (List[index])); }
            set { List[index] = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///    <para>Adds a <see cref='Tools.Core.configuration.IApplicationEventFilter'/> with the specified value to the 
        ///    <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.IApplicationEventFilter'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.IApplicationEventFilterCollection.AddRange'/>
        public int Add(ITraceEventFilter value)
        {
            return List.Add(value);
        }

        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='Tools.Core.configuration.IApplicationEventFilter'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.IApplicationEventFilterCollection.Add'/>
        public void AddRange(ITraceEventFilter[] value)
        {
            for (int i = 0; (i < value.Length); i = (i + 1))
            {
                Add(value[i]);
            }
        }

        /// <summary>
        ///     <para>
        ///       Adds the contents of another <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.IApplicationEventFilterCollection.Add'/>
        public void AddRange(ITraceEventFilterCollection value)
        {
            for (int i = 0; (i < value.Count); i = (i + 1))
            {
                Add(value[i]);
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/> contains the specified <see cref='Tools.Core.configuration.IApplicationEventFilter'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.IApplicationEventFilter'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='Tools.Core.configuration.IApplicationEventFilter'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.IApplicationEventFilterCollection.IndexOf'/>
        public bool Contains(ITraceEventFilter value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// <para>Copies the <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(ITraceEventFilter[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <summary>
        ///    <para>Returns the index of a <see cref='Tools.Core.configuration.IApplicationEventFilter'/> in 
        ///       the <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.IApplicationEventFilter'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='Tools.Core.configuration.IApplicationEventFilter'/> of <paramref name='value'/> in the 
        /// <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.IApplicationEventFilterCollection.Contains'/>
        public int IndexOf(ITraceEventFilter value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// <para>Inserts a <see cref='Tools.Core.configuration.IApplicationEventFilter'/> into the <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='Tools.Core.configuration.IApplicationEventFilter'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='Tools.Core.configuration.IApplicationEventFilterCollection.Add'/>
        public void Insert(int index, ITraceEventFilter value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new IApplicationEventFilterEnumerator GetEnumerator()
        {
            return new IApplicationEventFilterEnumerator(this);
        }

        /// <summary>
        ///    <para> Removes a specific <see cref='Tools.Core.configuration.IApplicationEventFilter'/> from the 
        ///    <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.IApplicationEventFilter'/> to remove from the <see cref='Tools.Core.configuration.IApplicationEventFilterCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(ITraceEventFilter value)
        {
            List.Remove(value);
        }

        #endregion

        #region IApplicationEventFilterEnumerator class

        public class IApplicationEventFilterEnumerator : object, IEnumerator
        {
            #region Global declarations

            private readonly IEnumerator baseEnumerator;
            private readonly IEnumerable temp;

            #endregion

            #region Constructors

            public IApplicationEventFilterEnumerator(ITraceEventFilterCollection mappings)
            {
                temp = ((mappings));
                baseEnumerator = temp.GetEnumerator();
            }

            #endregion

            #region Properties

            public ITraceEventFilter Current
            {
                get { return ((ITraceEventFilter) (baseEnumerator.Current)); }
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