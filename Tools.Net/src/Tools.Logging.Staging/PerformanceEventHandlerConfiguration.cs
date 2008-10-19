using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tools.Logging
{
    [Serializable]
    public class PerformanceEventHandlerConfiguration
    {
        #region Implementation of IDescriptor

        private string _description;
        private string _name;

        public PerformanceEventHandlerConfiguration()
        {
        }

        public PerformanceEventHandlerConfiguration(string name, string description)
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

        private List<PerfomanceCounterConfiguration> _counters =
            new List<PerfomanceCounterConfiguration>();

        [XmlArray]
        public List<PerfomanceCounterConfiguration> Counters { get; set; }

        [XmlAttribute]
        public string MachineName { get; set; }

        [XmlAttribute]
        public string CategoryName { get; set; }

        /// <summary>
        /// If false, no category and counters setup will take place
        /// during handler initialization.
        /// </summary>
        [XmlAttribute]
        public bool EnableSetupOnInitialization { get; set; }

        /// <summary>
        /// Identifies the format of the suffix of the DynamicCategory
        /// </summary>
        [XmlAttribute]
        public string DynamicCategorySuffixFormat { get; set; }

        /// <summary>
        /// Maximum number of categories to be allowed to create when EnableSetupOnInitialization is true
        /// and category already exists, but there is a need to create a new counter within it.
        /// </summary>
        /// <remarks>
        /// Such a category will be named as OriginalName[i], where i is the next available index 
        /// not present in already existing categories. Once the maximum number is achieved exception will be
        /// thrown.
        /// </remarks>
        [XmlAttribute]
        public uint MaxOfDynamicCategories { get; set; }
    }
}