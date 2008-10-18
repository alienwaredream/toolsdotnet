using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.UI.Windows.Descriptors
{
    partial class DescriptiveListEditorControl
	{
		private DescriptiveNameValueControl descriptiveNameValueControl;

		private void initializeDescriptiveNameValueControl
			(
			DescriptiveNameValueDomainsProvider domainsProvider
			)
		{
			descriptiveNameValueControl =
				new Tools.UI.Windows.Descriptors.DescriptiveNameValueControl
					(
                    domainsProvider
					);
			this.descriptiveNameValueControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.descriptiveNameValueControl.Location = new System.Drawing.Point(0, 0);
			this.descriptiveNameValueControl.MinimumSize = new System.Drawing.Size(179, 107);
			this.descriptiveNameValueControl.Name = "descriptiveNameValueControl";
			this.descriptiveNameValueControl.Size = new System.Drawing.Size(313, 139);
			this.descriptiveNameValueControl.TabIndex = 0;
			this.descriptiveNameValueControl.Load += new System.EventHandler(this.descriptiveNameValueControl1_Load);
			this.splitContainer.Panel1.Controls.Add(this.descriptiveNameValueControl);

		}
	}
}
