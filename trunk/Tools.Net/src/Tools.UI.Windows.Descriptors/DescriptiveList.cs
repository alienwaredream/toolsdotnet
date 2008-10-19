using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Tools.Core;

namespace Tools.UI.Windows.Descriptors
{
    [Serializable()]
    public class DescriptiveList<T> : List<T>, IDescriptor
    {
        		#region Implementation of IDescriptor

		private string _name;
		private string _description;

		[XmlAttribute()]
		public string Name
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
		public string Description
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

		public DescriptiveList()
            : this
            (
            "DescriptiveListGenericName",
            "DescriptiveListGenericDescription"
            )
		{
			
		}
		public DescriptiveList
            (
            string name, 
            string description
            )
            : base()
		{
			_name = name;
			_description = description;
		}
        public DescriptiveList
            (
            string name,
            string description,
            IEnumerable <T> collection
            )
            : base
            (
            collection
            )
        {
            _name = name;
            _description = description;
        }

		#endregion Implementation of IDescriptor
    }
}
