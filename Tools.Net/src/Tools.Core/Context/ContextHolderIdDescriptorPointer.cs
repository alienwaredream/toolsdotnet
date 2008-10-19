using System;

namespace Tools.Core.Context
{
    [Serializable()]
    public class ContextHolderIdDescriptorPointer : Descriptor, ICloneable
    {
        private int _contextHolderId;
        private string _url;

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public int ContextHolderId
        {
            get { return _contextHolderId; }
            set { _contextHolderId = value; }
        }
        public ContextHolderIdDescriptorPointer
            (
            )
        {
        }
        public ContextHolderIdDescriptorPointer
            (
            string name,
            string description,
            int contextHolderId,
            string url
            ): base(name, description)
        {
            _contextHolderId = contextHolderId;
            _url = url;
        }


        #region ICloneable Members

        public object Clone()
        {
            return new ContextHolderIdDescriptorPointer
                (
                this.Name,
                this.Description,
                this.ContextHolderId,
                this.Url
                );
        }

        #endregion
    }
}