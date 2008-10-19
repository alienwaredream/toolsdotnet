using System;
using System.Xml.Serialization;

namespace Tools.Core
{
    /// <summary>
    /// Provides the default implementation of the <see cref="IDescriptor"/>.
    /// </summary>
    /// This is a change on the branch
    [Serializable]
    public class Descriptor : IDescriptor
    {
        #region Implementation of IDescriptor

        private string _description;
        private string _name;

        public Descriptor()
        {
        }

        public Descriptor(string name, string description)
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
        /// <summary>
        /// Probes for the name of the descriptor instance.
        /// </summary>
        /// <remarks></remarks>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string ProbeForName(object source)
        {
            if (source == null)
                return String.Empty;

            var testObject = source as Descriptor;

            if (testObject != null)
                return testObject.Name;

            return source.GetType().FullName;
        }
    }
}