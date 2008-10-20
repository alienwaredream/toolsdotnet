using System.Drawing;
using System.Windows.Forms;

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
                new DescriptiveNameValueControl
                    (
                    domainsProvider
                    );
            descriptiveNameValueControl.Dock = DockStyle.Fill;
            descriptiveNameValueControl.Location = new Point(0, 0);
            descriptiveNameValueControl.MinimumSize = new Size(179, 107);
            descriptiveNameValueControl.Name = "descriptiveNameValueControl";
            descriptiveNameValueControl.Size = new Size(313, 139);
            descriptiveNameValueControl.TabIndex = 0;
            descriptiveNameValueControl.Load += descriptiveNameValueControl1_Load;
            splitContainer.Panel1.Controls.Add(descriptiveNameValueControl);
        }
    }
}