using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Tools.Logging
{
    /// <summary>
    /// Summary description for PerfomanceCounterConfiguration.
    /// </summary>
    [Serializable()]
    public class PerfomanceCounterConfiguration
    {
        #region Implementation of IDescriptor

		private string _name;
		private string _description;

		[XmlAttribute()]
		public virtual string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
		[XmlElement()]
		public virtual string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}

		public PerfomanceCounterConfiguration()
		{
			
		}
        public PerfomanceCounterConfiguration(string name, string description)
		{
			_name = name;
			_description = description;
		}

		#endregion Implementation of IDescriptor

        [XmlAttribute()]
        public PerformanceCounterType CounterType
        {
            get;
            set;
        }
        /// <summary>
        /// EventId the measurement will apply to.
        /// </summary>
        [XmlAttribute()]
        public string EventId
        {
            get;
            set;
        }
        /// <summary>
        /// The number of measurements to skip at the start. Default value is zero.
        /// TODO: To be substituted with some rules configuration (SD).
        /// </summary>
        [XmlAttribute()]
        public int EventsToSkipCount
        {
            get;
            set;
        }
        /// <summary>
        /// Scale. Measurement raw value will scaled by its value. Default value is one.
        /// </summary>
        [XmlAttribute()]
        public decimal Scale
        {
            get;
            set;
        }
        /// <summary>
        /// If true (default value) then raw value of the counter will be set to zero when creating the counter.
        /// Subject to test still as every new app instance recreates counters it might be nonperformant here (SD).
        /// Anyway configuration in the future will tell to the perf infrastructure if there is a need to 
        /// re-create the counters (SD).
        /// </summary>
        [XmlAttribute()]
        public bool ClearOnStart
        {
            get;
            set;
        }



    }
}
