using System;
using System.Collections;
using System.Globalization;
using System.Xml.Serialization;
using Tools.Core;

namespace Tools.Coordination.WorkItems
{

    #region WorkItemSlotCollection class

    /// <summary>
    ///     <para>
    ///       A collection that stores <see cref='WorkItemSlot'/> objects.
    ///    </para>
    /// </summary>
    [Serializable]
    public class WorkItemSlotCollection : CollectionBase, IDescriptor
    {
        #region Implementation of IDescriptor

        private string _description;
        private string _name;

        public WorkItemSlotCollection(string name, string description)
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

        private readonly WorkItemSlotsConfiguration _configuration;

        private readonly PriorityWorkItemsRequestedCounterCollection _counters =
            new PriorityWorkItemsRequestedCounterCollection();

        private readonly PrioritySlotsIndexCollection _indexes = new PrioritySlotsIndexCollection();
        private readonly object _syncRootInsert = new object();

        /// <summary>
        /// Ad-hoc approach for fairness in the slots walking, to be revised when 
        /// queue is used instead of array/collection (SD)
        /// </summary>
        private int walkerIndex;

        #endregion Fields

        #region Constructors

        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='WorkItemSlotCollection'/>.
        ///    </para>
        /// </summary>
        //        public WorkItemSlotCollection() 
        //		{
        //			indexes = new PrioritySlotsIndexCollection();
        //        }
        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='WorkItemSlotCollection'/>.
        ///    </para>
        /// </summary>
        public WorkItemSlotCollection(WorkItemSlotCollection value)
        {
            AddRange(value);
        }


        /// <summary>
        ///     <para>
        ///       Initializes a new instance of <see cref='WorkItemSlot'/> objects.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///       A array of <see cref='WorkItemSlot'/> objects with which to intialize the collection
        /// </param>
        public WorkItemSlotCollection(WorkItemSlot[] value)
        {
            AddRange(value);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Synchronically gets WorkItemSlot entries count in WorkItemSlotCollection.
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

        public PriorityWorkItemsRequestedCounterCollection Counters
        {
            get { return _counters; }
        }

        protected PrioritySlotsIndexCollection Indexes
        {
            get { return _indexes; }
        }

        public WorkItemSlotsConfiguration Configuration
        {
            get { return _configuration; }
        }

        public object SyncRootInsert
        {
            get { return _syncRootInsert; }
        }

        #endregion

        #region Indexers

        /// <summary>
        /// <para>Represents the entry at the specified index of the <see cref='WorkItemSlot'/>.</para>
        /// </summary>
        /// <param name='index'><para>The zero-based index of the entry to locate in the collection.</para></param>
        /// <value>
        ///    <para> The entry at the specified index of the collection.</para>
        /// </value>
        /// <exception cref='System.ArgumentOutOfRangeException'><paramref name='index'/> 
        /// is outside the valid range of indexes for the collection.
        /// </exception>
        public WorkItemSlot this[int index]
        {
            get { return ((WorkItemSlot)(List[index])); }
            set { List[index] = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        ///    <para>Adds a <see cref='WorkItemSlot'/> with the specified value to the 
        ///    <see cref='WorkItemSlotCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='WorkItemSlot'/> to add.</param>
        /// <returns>
        ///    <para>The index at which the new element was inserted.</para>
        /// </returns>
        public int Add(WorkItemSlot value)
        {
            return List.Add(value);
        }

        /// <summary>
        /// <para>Copies the elements of an array to the end of the <see cref='WorkItemSlotCollection'/>.</para>
        /// </summary>
        /// <param name='value'>
        ///    An array of type <see cref='WorkItemSlot'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='WorkItemSlotCollection.Add'/>
        public void AddRange(WorkItemSlot[] value)
        {
            for (int i = 0; (i < value.Length); i = (i + 1))
            {
                //				WorkItemSlot nv = this.GetEntry(value[i].IdHash);
                //				if (nv!=null)
                //				{ 
                //					nv = value[i];
                //				}
                //				else
                //				{
                Add(value[i]);
                //				}
            }
        }

        /// <summary>
        ///     <para>
        ///       Adds the contents of another <see cref='WorkItemSlotCollection'/> to the end of the collection.
        ///    </para>
        /// </summary>
        /// <param name='value'>
        ///    A <see cref='WorkItemSlotCollection'/> containing the objects to add to the collection.
        /// </param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        /// <seealso cref='WorkItemSlotCollection.Add'/>
        public void AddRange(WorkItemSlotCollection value)
        {
            for (int i = 0; (i < value.Count); i = (i + 1))
            {
                Add(value[i]);
            }
        }

        /// <summary>
        /// <para>Gets a value indicating whether the 
        ///    <see cref='WorkItemSlotCollection'/> contains the specified <see cref='WorkItemSlot'/>.</para>
        /// </summary>
        /// <param name='value'>The <see cref='WorkItemSlot'/> to locate.</param>
        /// <returns>
        /// <para><see langword='true'/> if the <see cref='WorkItemSlot'/> is contained in the collection; 
        ///   otherwise, <see langword='false'/>.</para>
        /// </returns>
        /// <seealso cref='WorkItemSlotCollection.IndexOf'/>
        public bool Contains(WorkItemSlot value)
        {
            return List.Contains(value);
        }

        /// <summary>
        /// <para>Copies the <see cref='WorkItemSlotCollection'/> values to a one-dimensional <see cref='System.Array'/> instance at the 
        ///    specified index.</para>
        /// </summary>
        /// <param name='array'><para>The one-dimensional <see cref='System.Array'/> that is the destination of the values copied from <see cref='WorkItemSlotCollection'/> .</para></param>
        /// <param name='index'>The index in <paramref name='array'/> where copying begins.</param>
        /// <returns>
        ///   <para>None.</para>
        /// </returns>
        public void CopyTo(WorkItemSlot[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <summary>
        ///    <para>Returns the index of a <see cref='WorkItemSlot'/> in 
        ///       the <see cref='WorkItemSlotCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='WorkItemSlot'/> to locate.</param>
        /// <returns>
        /// <para>The index of the <see cref='WorkItemSlot'/> of <paramref name='value'/> in the 
        /// <see cref='WorkItemSlotCollection'/>, if found; otherwise, -1.</para>
        /// </returns>
        /// <seealso cref='WorkItemSlotCollection.Contains'/>
        public int IndexOf(WorkItemSlot value)
        {
            return List.IndexOf(value);
        }

        /// <summary>
        /// <para>Inserts a <see cref='WorkItemSlot'/> into the <see cref='WorkItemSlotCollection'/> at the specified index.</para>
        /// </summary>
        /// <param name='index'>The zero-based index where <paramref name='value'/> should be inserted.</param>
        /// <param name=' value'>The <see cref='WorkItemSlot'/> to insert.</param>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='WorkItemSlotCollection.Add'/>
        /// <param name="value"></param>
        public void Insert(int index, WorkItemSlot value)
        {
            List.Insert(index, value);
        }

        /// <summary>
        ///    <para>Returns an enumerator that can iterate through 
        ///       the <see cref='WorkItemSlotCollection'/> .</para>
        /// </summary>
        /// <returns><para>None.</para></returns>
        /// <seealso cref='System.Collections.IEnumerator'/>
        public new WorkItemSlotEnumerator GetEnumerator()
        {
            return new WorkItemSlotEnumerator(this);
        }

        /// <summary>
        ///    <para> Synchronically removes a specific <see cref='WorkItemSlot'/> from the 
        ///    <see cref='WorkItemSlotCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='WorkItemSlot'/> to remove from the <see cref='WorkItemSlotCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void SynchronizedRemove(WorkItemSlot value)
        {
            lock (this)
            {
                List.Remove(value);
            }
        }

        /// <summary>
        ///    <para> Removes a specific <see cref='WorkItemSlot'/> from the 
        ///    <see cref='WorkItemSlotCollection'/> .</para>
        /// </summary>
        /// <param name='value'>The <see cref='WorkItemSlot'/> to remove from the <see cref='WorkItemSlotCollection'/> .</param>
        /// <returns><para>None.</para></returns>
        /// <exception cref='System.ArgumentException'><paramref name='value'/> is not found in the Collection. </exception>
        public void Remove(WorkItemSlot value)
        {
            List.Remove(value);
        }
        /// <summary>
        /// Get first WorkItemSlot entry from WorkItemSlotCollection and remove it.
        /// This method is syncronized by locking on the higher level.
        /// </summary>
        /// <returns>
        /// The first WorkItemSlot entry from WorkItemSlotCollection.
        /// </returns>
        public WorkItem GetTopWorkItem()
        {
            WorkItem ret = null;
            //TODO: Subject to review, only proof of concept (SD)
            //TODO: (SD) Use queue based approach here
            int j = 0;

            for (int i = 0; i < Count; i++)
            {
                if (i < Count - walkerIndex)
                    j = walkerIndex + i;
                else
                    j = i - (Count - walkerIndex);

                if (!this[j].IsEmpty)
                {
                    ret = this[j].RetrieveWorkItem();

                    walkerIndex = (walkerIndex < Count) ? walkerIndex + 1 : 0;

                    _counters[ret.SubmissionPriority].ItemsPresentCount -= 1;

                    #region WorkItem Diagnostics

                    //if (Tools.Instrumentation.Common.InstrumentationManager.Level==InstrumentationLevel.High)
                    //{
                    ret.AttachNote(
                        String.Format(CultureInfo.InvariantCulture,
                                      "Returned from the [{0}] slot by GetTopWorkItem() [{1}], walkerIndex [{2}]",
                                      Name, j, walkerIndex));
                    //}

                    #endregion WorkItem Diagnostics

                    return ret;
                }
            }

            return null;
        }

        public void AddWorkItem(WorkItem workItem)
        {
            int startIndex = _indexes[workItem.SubmissionPriority].StartIndex;
            int endIndex = _indexes[workItem.SubmissionPriority].EndIndex;

            for (int i = startIndex; i < endIndex; i++)
            {
                if (this[i].IsEmpty)
                {
                    this[i].AssignWorkItem(workItem);

                    #region WorkItem Diagnostics

                    //if (Tools.Instrumentation.Common.InstrumentationManager.Level==InstrumentationLevel.High)
                    //{
                    workItem.AttachNote("Added to the " + Name + " slot with index " + i);
                    //}

                    #endregion WorkItem Diagnostics

                    _counters[workItem.SubmissionPriority].ItemsPresentCount += 1;
                    return;
                }
            }

            // TODO: Handle the case when there is no space, that
            // should not happen by design, but anyway ... (SD)
            throw new ApplicationException
                (
                "There is no space to add work item with priority " + workItem.SubmissionPriority +
                " . Value of slot collection lookup indeces are: StartInternal=" + startIndex +
                ", end=" + endIndex
                );
        }

        #endregion

        #region WorkItemSlotEnumerator class

        public class WorkItemSlotEnumerator : object, IEnumerator
        {
            #region Fields

            private readonly IEnumerator baseEnumerator;
            private readonly IEnumerable temp;

            #endregion

            #region Constructors

            public WorkItemSlotEnumerator(IEnumerable mappings)
            {
                temp = ((mappings));
                baseEnumerator = temp.GetEnumerator();
            }

            #endregion

            #region Properties

            public WorkItemSlot Current
            {
                get { return ((WorkItemSlot)(baseEnumerator.Current)); }
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

        #region Factory

        protected WorkItemSlotCollection
            (
            WorkItemSlotsConfiguration configuration
            )
        //: this()
        {
            _configuration = configuration;
            int currentStartIndex = 0;
            int currentEndIndex = 0;

            for (int i = 0; i < _configuration.PrioritySlotCounts.Count; i++)
            {
                if (i > 0) currentStartIndex += currentEndIndex;
                currentEndIndex += _configuration.PrioritySlotCounts[i].Count;

                _indexes.Add
                    (
                    new PrioritySlotsIndex
                        (
                        _configuration.PrioritySlotCounts[i].SubmissionPriority,
                        currentStartIndex,
                        currentEndIndex
                        ));
                _counters.Add
                    (
                    new PriorityWorkItemsRequestedCounter
                        (
                        _configuration.PrioritySlotCounts[i].SubmissionPriority
                        ));

                for (int k = 0; k < _configuration.PrioritySlotCounts[i].Count; k++)
                {
                    Add
                        (
                        WorkItemSlot.Create(_configuration.PrioritySlotCounts[i].SubmissionPriority)
                        );
                }
            }
        }

        // TODO: Sync (SD)
        public static WorkItemSlotCollection Create(WorkItemSlotsConfiguration configuration)
        {
            // TODO: Sync (SD)
            return new WorkItemSlotCollection
                (
                configuration
                );
        }

        #endregion Factory
    }

    #endregion
}