using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace Tools.Core.Messaging
{
    #region Class Message

    /// <summary>
    /// Encapsulates a Messsage concept with id, name, text properties as well
    /// as service and phase for which it is used.
    /// </summary>
    [DataContract()]
    [Serializable()]
    public class Message : ICloneable
    {
        #region Globals

        private int _id;
        private string _name;
        private string _serviceName;
        private string _phase;
        private string _text;
        private string _cultureName;
        private MessageType _messageType = MessageType.None;

        private MessageExtension<string, string> _stringNameValueExtension =
            new MessageExtension<string, string>();

        #endregion

        #region Methods
        public void AddExtensionItem(string key, string value) 
        {
            //if(_stringNameValueExtension==null)
            //    _stringNameValueExtension = new MessageExtension<string, string>();
            NameValue<string, string> nv = new NameValue<string, string>(key,value);
            _stringNameValueExtension.AddNameValue(nv);
        }
        public bool ContainsExtensionItem(NameValue<string, string> nv)
        {
            return StringNameValueExtension.NameValueList.Exists(
                delegate(NameValue<string, string> nvItem) { return nvItem.Equals(nv); });
        }
        public String GetExtensionValue(String key) 
        {
            if (StringNameValueExtension[key] != null)
            {
                return StringNameValueExtension[key].Value.ToString();
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Overrides

        public override string ToString()
        {
            return this.Id + ": "+ this.MessageType + ":" + this.Text;
        }

        #endregion

        #region Properties
        [DataMember()]
        public MessageExtension<string, string> StringNameValueExtension
        {
            get { return _stringNameValueExtension; }
            set { _stringNameValueExtension = value; }
        }

        [DataMember()]
        [XmlAttribute()]
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [DataMember()]
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }
        [DataMember()]
        [XmlAttribute()]
        public MessageType MessageType
        {
            get { return _messageType; }
            set { _messageType = value; }
        }
        [DataMember()]
        [XmlAttribute()]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [DataMember()]
        [XmlAttribute()]
        public string CultureName
        {
            get { return _cultureName; }
            set { _cultureName = value; }
        }
        [DataMember()]
        [XmlAttribute()]
        public string Phase
        {
            get { return _phase; }
            set { _phase = value; }
        }
        [DataMember()]
        [XmlAttribute()]
        public string ServiceName
        {
            get { return _serviceName; }
            set { _serviceName = value; }
        }


        //public Dictionary<string, string> CustomData
        //{
        //    get { return _customData; }
        //    set { _customData = value; }
        //}
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        public Message()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="messageId">The message id.</param>
        /// <param name="text">The text.</param>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="phase">The phase.</param>
        public Message(object messageId, string text, string serviceName, string phase)
            : this(messageId, text)
        {
            this._serviceName = serviceName;
            this._phase = phase;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="messageId">The message id.</param>
        /// <param name="text">The text.</param>
        public Message(object messageId, string text)
        {
            _id = Convert.ToInt32(messageId);
            _name = messageId.ToString();
            _text = text;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="text">The text.</param>
        public Message(int id, string name, string text)
        {
            _id = id;
            _name = name;
            _text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="text">The text.</param>
        /// <param name="messageType">Type of the message.</param>
        public Message(int id, string name, string text, MessageType messageType)
            : this(id, name, text)
        {
            _messageType = messageType;
        }
        #endregion

        #region ICloneable Members

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            //TODO: (SD) Add for the new fields
            return new Message(this.Id, this.Name, this.Text, this.MessageType);
        }

        #endregion
    }
    #endregion
}
