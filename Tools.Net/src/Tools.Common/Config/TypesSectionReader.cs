using System;
using System.Collections.Generic;

using System.Text;
using Tools.Common.Utils;
using System.Configuration;
using System.Xml;

namespace Tools.Common.Config
{
    //TODO: (SD) Subject to discard when Spring is used here.
    #region class TypeSectionReader
    /// <summary>
    /// Summary description for TypesCollectionSectionReader.
    /// </summary>
    public class TypesSectionReader : IConfigurationSectionHandler
    {
        public TypesSectionReader()
        {
        }
        #region IConfigurationSectionHandler Members

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            object _parent = parent;

            object _configContext = configContext;
            // TODO: check for null
            XmlNode _section = section;
            // The bellow one is from the non-real area, but still.
            if (_section == null)
            {
                throw new Exception
                    (
                    "Supplied section represents a null xml node");
            }

            XmlNode typeAttribute = _section.Attributes.GetNamedItem("type");

            if (typeAttribute == null)
            {
                throw new Exception
                    (
                    String.Format
                        (
                        "Attribute \"type\" is required for the section main element, section {0}",
                        _section.Name
                        ));
            }
            Type type = null;
            try
            {
                // Rather going for an error here so we can report 
                // more details in that exceptional case.
                type = Type.GetType(typeAttribute.Value, true);
            }
            catch (Exception e)
            {
                throw new Exception
                    (
                    String.Format
                    (
                    "Exception happened while trying to create the type {0}, from section {1}",
                    typeAttribute.Value,
                    _section.Name
                    ), e);
            }
            object result = null;

            //XmlNode typeNode = _section.SelectSingleNode(@"child::" + type.Name);

            XmlNode typeNode = _section.SelectSingleNode(@"child::*");

            if (typeNode == null)
            {
                throw new Exception
                    (
                    String.Format
                    (
                    "Attribute \"type\" has different type name {0} from the type element," +
                    " section {1}",
                    typeAttribute.Value,
                    _section.Name
                    ));
            }
            try
            {
                result = SerializationUtility.DeserializeFromString(
                    typeNode.OuterXml,
                    type);
            }
            catch (Exception e)
            {
                throw new Exception
                    (
                    String.Format
                    (
                    "Exception happened while trying to deserialize the type {0}, from section content of {1}{2}",
                    typeAttribute.Value,
                    System.Environment.NewLine,
                    _section.OuterXml
                    ),
                    e
                    );
            }
            return result;
        }

        #endregion
    }
    
    #endregion
}
