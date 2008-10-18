using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Tools.Core.Utils;
using Tools.Core;
using Tools.Core.Context;

namespace Tools.UI.Windows.Descriptors
{
	public class ContextHolderIdDescriptorPointerDomainsProvider : IDomainsProvider<ContextHolderIdDescriptorPointer>
	{
		#region IDomainsProvider Members

		public string[] GetDomainValues(ContextHolderIdDescriptorPointer chdp)
		{
			return new string[4]
			{
                chdp.ContextHolderId.ToString(),
				chdp.Name,
				chdp.Description,
				chdp.Url
			};
		}

		public string[] GetDomainNames()
		{
			return new string[4]
			{
				"CHId",
				"Name",
				"Description",
                "Url"
			};
		}
        public ContextHolderIdDescriptorPointerDomainsProvider()
        {
        }
        public ContextHolderIdDescriptorPointer GetNewDefaultInstance()
		{
            return new ContextHolderIdDescriptorPointer
			(
				
				"Edit Name",
				"Edit Description",
                -1,
                "Edit Url"
			);
		}
		#endregion
}
}
