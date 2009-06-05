using System;
using System.Collections;
using System.Diagnostics;
using System.Xml.Serialization;
using Tools.Core;
// Temp;

namespace Tools.Coordination.WorkItems
{

    #region WorkItemCollection class

    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='WorkItem'/> objects.
    ///    </para>
    /// </summary>
    [Serializable]
    public class WorkItemCollection : CollectionBase, IDescriptor
    {
        #region Implementation of IDescriptor

        private string _description;
        private string _name;

        public WorkItemCollection(string name, string description)
        {
            _name = name;
            _description = description;
        }

        [XmlAttribute]
        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [XmlElement]
        public virtual string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        #endregion Implementation of IDescriptor

        #region Fields

        private readonly object _syncRootInsert = new object();

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Coordination.Core.WorkItemCollection'/>.
        ///    </para>
        /// </summary>
        public WorkItemCollection()
        {
        }

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Coordination.Core.WorkItemCollection'/>.
        ///    </para>
        /// </summary>
        public WorkItemCollection(WorkItemCollection value)
        {
            AddRange(value);
        }


        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='Tools.Coordination.Core.WorkItem'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='Tools.Coordination.Core.WorkItem'/> objects with which to intialize the collection
        /// </param>
        public WorkItemCollection(WorkItem[] value)
        {
            AddRange(value);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Synchronically gets WorkItem entries count in WorkItemCollection.
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
        /// <para>Represents the entry at the specified index of the <see cref='WorkItem'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public WorkItem this[int index]
        {
            get { return ((WorkItem) (List[index])); }
            set { List[index] = value; }
        }

        /// <summary>
        /// <para>Represents the <see cref='WorkItem'/> entry with specified Id.</para>
        /// </summary>
        /// <param name='index'><para>The Id of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> 
        ///		The <see cref='WorkItem'/> entry with specified Id. 
        ///    </para>
        /// </value>
        public WorkItem this[string idHash]
        {
            get
            {
                foreach (WorkItem qwi in this)
                {
                    if (qwi.IdHash == idHash)
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
                    if (((WorkItem) List[i]).IdHash == idHash)
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
        ///    <para>Adds a <see cref='WorkItem'/> with the specified value to the 
        ///    <see cref='WorkItemCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='WorkItem'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        /// <seealso cref='WorkItemCollection.AddRange'/>
        public int Add(WorkItem value)
        {
            #region WorkItem Diagnostics

            //if (Tools.Instrumentation.Common.InstrumentationManager.Level==InstrumentationLevel.High)
            //{
            value.AttachNote("Added to the " + Name + " IQ");
            //}

            #endregion WorkItem Diagnostics

            return List.Add(value);
        }

        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='WorkItemCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='WorkItem'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='WorkItemCollection.Add'/>
        public void AddRange(WorkItem[] value)
        {
            for (int i = 0; (i < value.Length); i = (i + 1))
            {
                WorkItem nv = GetEntry(value[i].IdHash);
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
        ///       Adds the contents of another <see cref='WorkItemCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='WorkItemCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='WorkItemCollection.Add'/>
        public void AddRange(WorkItemCollection value)
        {
            for (int i = 0; (i < value.Count); i = (i + 1))
            {
                Add(value[i]);
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='WorkItemCollection'/> contains the specified <see cref='WorkItem'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='WorkItem'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='WorkItem'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='WorkItemCollection.IndexOf'/>
        public bool Contains(WorkItem value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// <para>Copies the <see cref='WorkItemCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='WorkItemCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <exception cref='System.ArgumentException'><para><paramref name='array'/> is multidimensional.</para> <para>-or-</para> <para>The number of elements in the <see cref='WorkItemCollection'/> is greater than the available space between <paramref name='arrayIndex'/> and the end of <paramref name='array'/>.</para></exception>
        /// <exception cref='System.ArgumentNullException'><paramref name='array'/> is <see langword='null'/>. </exception>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='arrayIndex'/> is less than <paramref name='array'/>'s lowbound. </exception>
        /// <seealso cref='System.Array'/>
        public void CopyTo(WorkItem[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <summary>
        ///    <para>Returns the index of a <see cref='WorkItem'/> in 
        ///       the <see cref='WorkItemCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='WorkItem'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='WorkItem'/> of <paramref name='value'/> in the 
        /// <see cref='WorkItemCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='WorkItemCollection.Contains'/>
        public int IndexOf(WorkItem value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// <para>Inserts a <see cref='WorkItem'/> into the <see cref='WorkItemCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='WorkItem'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='WorkItemCollection.Add'/>
        public void Insert(int index, WorkItem value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='WorkItemCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new WorkItemEnumerator GetEnumerator()
        {
            return new WorkItemEnumerator(this);
        }

        /// <summary>
        ///    <para> Synchronically removes a specific <see cref='WorkItem'/> from the 
        ///    <see cref='WorkItemCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='WorkItem'/> to remove from the <see cref='WorkItemCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void SynchronizedRemove(WorkItem value)
        {
            lock (this)
            {
                List.Remove(value);
            }
        }

        /// <summary>
        ///    <para> Removes a specific <see cref='WorkItem'/> from the 
        ///    <see cref='WorkItemCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='WorkItem'/> to remove from the <see cref='WorkItemCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(WorkItem value)
        {
            #region WorkItem Diagnostics

            //if (Tools.Instrumentation.Common.InstrumentationManager.Level==InstrumentationLevel.High)
            //{
            value.AttachNote("Removed from the " + Name + " IQ");
            Trace.WriteLine(value.ToString());
            //}

            #endregion WorkItem Diagnostics

            List.Remove(value);
        }

        /// <summary>
        /// Gets an entry for the supplied name.
        /// </summary>
        /// <param name="name">Entry name.</param>
        /// <returns>Entry if exists or null otherwise.</returns>
        public WorkItem GetEntry(string idHash)
        {
            WorkItemEnumerator ce = GetEnumerator();

            while (ce.MoveNext())
            {
                if (ce.Current.IdHash == idHash)
                {
                    #region WorkItem Diagnostics

                    //if (Tools.Instrumentation.Common.InstrumentationManager.Level==InstrumentationLevel.High)
                    //{
                    ce.Current.AttachNote("Got from " + Name + " IQ");
                    //}

                    #endregion WorkItem Diagnostics

                    return ce.Current;
                }
            }

            return null;
        }


        /// <summary>
        /// Get first WorkItem entry from WorkItemCollection and remove it.
        /// </summary>
        /// <returns>
        /// The first WorkItem entry from WorkItemCollection.
        /// </returns>
        public WorkItem GetFirstEntry()
        {
            WorkItem qwi = null;
            if (Count > 0)
            {
                qwi = this[0];
                RemoveAt(0);
            }

            return qwi;
        }

        #endregion

        #region WorkItemEnumerator class

        public class WorkItemEnumerator : object, IEnumerator
        {
            #region Fields

            private readonly IEnumerator baseEnumerator;
            private readonly IEnumerable temp;

            #endregion

            #region Constructors

            public WorkItemEnumerator(WorkItemCollection mappings)
            {
                temp = ((mappings));
                baseEnumerator = temp.GetEnumerator();
            }

            #endregion

            #region Properties

            public WorkItem Current
            {
                get { return ((WorkItem) (baseEnumerator.Current)); }
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