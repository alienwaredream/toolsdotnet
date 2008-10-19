using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Tools.Core
{
    /// <summary>
    /// Purpose of this class is to cover a blank space for NameValueCollection
    /// serialization issues.
    /// </summary>
    [DataContract]
    [Serializable]
    public class NameValue<TKey, TValue>
    {
        #region Fields

        #endregion

        #region Properties

        [DataMember]
        [XmlAttribute]
        public TKey Name { get; set; }

        [DataMember]
        [XmlAttribute]
        public TValue Value { get; set; }

        #endregion

        #region Constuctors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public NameValue()
        {
        }

        public NameValue(TKey name, TValue value)
        {
            Name = name;
            Value = value;
        }

        public override bool Equals(object obj)
        {
            var nv = obj as NameValue<TKey, TValue>;


            if (nv == null)
                return false;
            return Name.Equals(nv.Name) && Value.Equals(nv.Value);
        }

        //public static bool operator ==(NameValue<TKey, TValue> a, NameValue<TKey, TValue> b)
        //{
        //    if (System.Object.ReferenceEquals(a, b))
        //    {
        //        return true;
        //    }


        //    return (a.Name.Equals(b.Name)) && (a.Value.Equals(b.Value));
        //}
        //public static bool operator !=(NameValue<TKey, TValue> a, NameValue<TKey, TValue> b)
        //{
        //    return !(a==b);
        //}
        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Value.GetHashCode();
        }

        #endregion
    }
}