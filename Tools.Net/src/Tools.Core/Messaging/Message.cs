using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Tools.Core.Messaging
{

    #region Class Message

    /// <summary>
    /// Encapsulates a Messsage concept with id, name, text properties as well
    /// as service and phase for which it is used.
    /// </summary>
    [DataContract]
    [Serializable]
    public class Message : ICloneable
    {
        #region Fields

        private MessageType _messageType = MessageType.None;

        private MessageExtension<string, string> _stringNameValueExtension =
            new MessageExtension<string, string>();

        #endregion

        #region Methods

        public void AddExtensionItem(string key, string value)
        {
            //if(_stringNameValueExtension==null)
            //    _stringNameValueExtension = new MessageExtension<string, string>();
            var nv = new NameValue<string, string>(key, value);
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
                return StringNameValueExtension[key].Value;
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
            return Id + ": " + MessageType + ":" + Text;
        }

        #endregion

        #region Properties

        [DataMember]
        public MessageExtension<string, string> StringNameValueExtension
        {
            get { return _stringNameValueExtension; }
            set { _stringNameValueExtension = value; }
        }

        [DataMember]
        [XmlAttribute]
        public string Name { get; set; }

        [DataMember]
        public string Text { get; set; }

        [DataMember]
        [XmlAttribute]
        public MessageType MessageType
        {
            get { return _messageType; }
            set { _messageType = value; }
        }

        [DataMember]
        [XmlAttribute]
        public int Id { get; set; }

        [DataMember]
        [XmlAttribute]
        public string CultureName { get; set; }

        [DataMember]
        [XmlAttribute]
        public string Phase { get; set; }

        [DataMember]
        [XmlAttribute]
        public string ServiceName { get; set; }


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
            ServiceName = serviceName;
            Phase = phase;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="messageId">The message id.</param>
        /// <param name="text">The text.</param>
        public Message(object messageId, string text)
        {
            Id = Convert.ToInt32(messageId);
            Name = messageId.ToString();
            Text = text;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="text">The text.</param>
        public Message(int id, string name, string text)
        {
            Id = id;
            Name = name;
            Text = text;
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
            return new Message(Id, Name, Text, MessageType);
        }

        #endregion
    }

    #endregion
}