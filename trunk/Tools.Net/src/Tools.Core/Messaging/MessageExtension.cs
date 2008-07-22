using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Diagnostics.CodeAnalysis;

namespace Tools.Core.Messaging
{
    [DataContract()]
    [Serializable()]
    public class MessageExtension<TKey, TValue> where TKey : IComparable
    {
        #region Fields
        private List<NameValue<TKey, TValue>> _nameValueList = new List<NameValue<TKey, TValue>>();
        #endregion

        #region Properties

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification="By design.")]
        [DataMember()]
        public List<NameValue<TKey, TValue>> NameValueList
        {
            get { return _nameValueList; }
            set { _nameValueList = value; }
        }
        #endregion

        #region Constructors
        public MessageExtension() 
        {
            //_info = new List<KeyValuePair<string, string>>();
        }
        #endregion

        #region Methods
        public void AddNameValue(TKey key, TValue value)
        {
            AddNameValue(new NameValue<TKey, TValue>(key, value));
        }
        public void AddNameValue(NameValue<TKey, TValue> nv)
        {
            _nameValueList.Add(nv);
        }
        public NameValue<TKey, TValue> this[TKey key]
        {
            get
            {
                return NameValueList.Find(delegate(NameValue<TKey, TValue> nv) { return nv.Name.CompareTo(key) == 0; });
            }
            set
            {
                AddNameValue(value);
            }
        }
        #endregion
    }
}
