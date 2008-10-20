using System;
using System.Collections;
using System.Threading;

namespace Tools.Processes.Core
{
    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='Tools.Core.configuration.IProcess'/> objects.
    ///    </para>
    /// </summary>
    /// <seealso cref='Tools.Core.configuration.IProcessCollection'/>
    [Serializable]
    public class IProcessCollection : CollectionBase
    {
        #region Constructors

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.IProcessCollection'/>.
        ///    </para>
        /// </summary>
        public IProcessCollection()
        {
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.IProcessCollection'/> based on another <see cref='Tools.Core.configuration.IProcessCollection'/>.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A <see cref='Tools.Core.configuration.IProcessCollection'/> from which the contents are copied
        /// </param>
        public IProcessCollection(IProcessCollection value)
        {
            AddRange(value);
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Core.configuration.IProcessCollection'/> containing any array of <see cref='Tools.Core.configuration.IProcess'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='Tools.Core.configuration.IProcess'/> objects with which to intialize the collection
        /// </param>
        public IProcessCollection(IProcess[] value)
        {
            AddRange(value);
        }

        #endregion

        #region Indexers

        /// <summary>
        /// <para>Represents the entry at the specified index of the <see cref='Tools.Core.configuration.IProcess'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public IProcess this[int index]
        {
            get { lock (this) return ((IProcess) (List[index])); }
            set { lock (this) List[index] = value; }
        }

        public IProcess this[string name]
        {
            get
            {
                lock (this)
                {
                    //Trace.WriteLine("Inside this[string name] get, Thread: " + Thread.CurrentThread.Name);
                    Thread.Sleep(50);
                    foreach (IProcess fdf in this)
                    {
                        if (fdf.Name == name)
                        {
                            return fdf;
                        }
                    }
                    return null;
                }
            }
            set
            {
                lock (this)
                {
                    //Trace.WriteLine("Inside this[string name] set, Thread: " + Thread.CurrentThread.Name);
                    Thread.Sleep(50);
                    for (int i = 0; i < List.Count; i++)
                    {
                        if (((IProcess) List[i]).Name == name)
                        {
                            List[i] = value;
                            return;
                        }
                    }
                    Add(value);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        ///    <para>Adds a <see cref='Tools.Core.configuration.IProcess'/> with the specified value to the 
        ///    <see cref='Tools.Core.configuration.IProcessCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.IProcess'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.IProcessCollection.AddRange'/>
        public int Add(IProcess value)
        {
            lock (this)
            {
                //Trace.WriteLine("Inside Add(IProcess value), Thread: " + Thread.CurrentThread.Name);
                Thread.Sleep(50);
                return List.Add(value);
            }
        }

        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='Tools.Core.configuration.IProcessCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='Tools.Core.configuration.IProcess'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.IProcessCollection.Add'/>
        public void AddRange(IProcess[] value)
        {
            for (int i = 0; (i < value.Length); i = (i + 1))
            {
                IProcess nv = GetEntry(value[i].Name);
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
        ///       Adds the contents of another <see cref='Tools.Core.configuration.IProcessCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='Tools.Core.configuration.IProcessCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.IProcessCollection.Add'/>
        public void AddRange(IProcessCollection value)
        {
            for (int i = 0; (i < value.Count); i = (i + 1))
            {
                Add(value[i]);
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='Tools.Core.configuration.IProcessCollection'/> contains the specified <see cref='Tools.Core.configuration.IProcess'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.IProcess'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='Tools.Core.configuration.IProcess'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.IProcessCollection.IndexOf'/>
        public bool Contains(IProcess value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// <para>Copies the <see cref='Tools.Core.configuration.IProcessCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='Tools.Core.configuration.IProcessCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='Tools.Core.configuration.IProcessCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(IProcess[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <summary>
        ///    <para>Returns the index of a <see cref='Tools.Core.configuration.IProcess'/> in 
        ///       the <see cref='Tools.Core.configuration.IProcessCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.IProcess'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='Tools.Core.configuration.IProcess'/> of <paramref name='value'/> in the 
        /// <see cref='Tools.Core.configuration.IProcessCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='Tools.Core.configuration.IProcessCollection.Contains'/>
        public int IndexOf(IProcess value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// <para>Inserts a <see cref='Tools.Core.configuration.IProcess'/> into the <see cref='Tools.Core.configuration.IProcessCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='Tools.Core.configuration.IProcess'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='Tools.Core.configuration.IProcessCollection.Add'/>
        public void Insert(int index, IProcess value)
        {
            lock (this)
            {
                //Trace.WriteLine("Inside Insert(int index, IProcess value), Thread: " + Thread.CurrentThread.Name);
                Thread.Sleep(50);
                List.Insert(index, value);
            }
        }

        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='Tools.Core.configuration.IProcessCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new IProcessEnumerator GetEnumerator()
        {
            return new IProcessEnumerator(this);
        }

        /// <summary>
        ///    <para> Removes a specific <see cref='Tools.Core.configuration.IProcess'/> from the 
        ///    <see cref='Tools.Core.configuration.IProcessCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='Tools.Core.configuration.IProcess'/> to remove from the <see cref='Tools.Core.configuration.IProcessCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(IProcess value)
        {
            lock (this)
            {
                //Trace.WriteLine("Inside Insert(int index, IProcess value), Thread: " + Thread.CurrentThread.Name);
                Thread.Sleep(50);
                List.Remove(value);
            }
        }

        /// <summary>
        /// Gets an entry for the supplied name.
        /// </summary>
        /// <param name="name">Entry name.</param>
        /// <returns>Entry if exists or null otherwise.</returns>
        public IProcess GetEntry(string name)
        {
            IProcessEnumerator ce = GetEnumerator();
            while (ce.MoveNext())
            {
                if (ce.Current.Name == name) return ce.Current;
            }
            return null;
        }

        #endregion

        #region IProcessEnumerator class

        public class IProcessEnumerator : object, IEnumerator
        {
            #region Fields

            private readonly IEnumerator baseEnumerator;
            private readonly IEnumerable temp;

            #endregion

            #region Constructors

            public IProcessEnumerator(IProcessCollection mappings)
            {
                temp = ((mappings));
                baseEnumerator = temp.GetEnumerator();
            }

            #endregion

            #region Properties

            public IProcess Current
            {
                get { return ((IProcess) (baseEnumerator.Current)); }
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