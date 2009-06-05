using System;
using System.Xml.Serialization;
using Tools.Core;

namespace Tools.Coordination.WorkItems
{
    /// <summary>
    /// Summary description for WorkItemSlotsConfiguration.
    /// </summary>
    [Serializable]
    public class WorkItemSlotsConfiguration : Descriptor
    {

        public WorkItemSlotsConfiguration()
            : this("GenericWorkItemSlots", "Name generically assigned")
        {
            PrioritySlotCounts = new PrioritySlotsCountCollection();
        }

        public WorkItemSlotsConfiguration(string name, string description)
            : base(name, description)
        {
            PrioritySlotCounts = new PrioritySlotsCountCollection();
        }

        public PrioritySlotsCountCollection PrioritySlotCounts { get; set; }
    }
}