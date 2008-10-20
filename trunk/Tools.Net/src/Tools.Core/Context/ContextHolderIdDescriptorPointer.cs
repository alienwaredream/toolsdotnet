using System;

namespace Tools.Core.Context
{
    [Serializable]
    public class ContextHolderIdDescriptorPointer : Descriptor, ICloneable
    {
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
            ) : base(name, description)
        {
            ContextHolderId = contextHolderId;
            Url = url;
        }

        public string Url { get; set; }

        public int ContextHolderId { get; set; }

        #region ICloneable Members

        public object Clone()
        {
            return new ContextHolderIdDescriptorPointer
                (
                Name,
                Description,
                ContextHolderId,
                Url
                );
        }

        #endregion
    }
}