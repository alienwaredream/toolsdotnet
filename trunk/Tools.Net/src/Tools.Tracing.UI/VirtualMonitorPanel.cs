#region Using directives

using System;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace Tools.Tracing.UI
{
    public partial class VirtualMonitorPanel : UserControl
    {
        private int _filteredOutCount;
        private int _messagesCount;
        private int listOldHeight;

        public VirtualMonitorPanel()
        {
            InitializeComponent();
            monitorListView.Scroll += listView1_Scrolled;
        }

        /// <summary>
        /// How many messages has been filtered out.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FilteredOutCount
        {
            get { return _filteredOutCount; }
            set
            {
                _filteredOutCount = value;

                //if (this.InvokeRequired)
                //{
                //    //System.Delegate del = delegate()
                //    //{
                //    //    filteredOutCountToolStripTextBox.Text = _filteredOutCount.ToString();
                //    //};
                Invoke
                    (
                    new Action<int>
                        (
                        setfilteredOutCountGui
                        ),
                    new object[] {value}
                    );
                //}
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MessagesCount
        {
            get { return _messagesCount; }
            set
            {
                _messagesCount = value;
                monitorListView.SetMaxIndex(_messagesCount);
                BeginInvoke(
                    new Action
                        (
                        showMessagesCount
                        ));
            }
        }

        public ScrollableListView ItemsListView
        {
            get { return monitorListView; }
        }

        public event ScrollEventHandler Scroll;

        private void setfilteredOutCountGui(int val)
        {
            filteredOutCountToolStripTextBox.Text = val.ToString();
        }

        protected override void OnScroll(ScrollEventArgs e)
        {
            if (Scroll != null)
            {
                Scroll(this, e);
            }
        }

        protected virtual void listView1_Scrolled(object source, ScrollEventArgs e)
        {
            OnScroll(e);
        }

        public void AddItem(ListViewItem lvi)
        {
            monitorListView.InsertToTop
                (
                lvi
                );
        }

        private void showMessagesCount()
        {
            shownCountToolStripTextBox.Text = _messagesCount.ToString();
        }

        public void ClearItems()
        {
            monitorListView.SuspendLayout();
            monitorListView.ClearItems();
            shownCountToolStripTextBox.Text = "0";
            monitorListView.ResumeLayout();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void toolStripSplitButton1_Click(object sender, EventArgs e)
        {
        }

        private void nameStripPanel_Click(object sender, EventArgs e)
        {
        }

        private void statusStrip1_Click(object sender, EventArgs e)
        {
        }

        private void ToolStripStatusLabel1_Click(object sender, EventArgs e)
        {
        }

        private void mToolStripStatusLabel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Minimize pressed");
            listOldHeight = ItemsListView.Height;
            ItemsListView.Height = 0;
        }

        private void genericNameToolStripStatusLabel_Click(object sender, EventArgs e)
        {
        }

        private void minimizeToolStripSplitButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Maximize pressed");
            ItemsListView.Height = listOldHeight;
        }
    }
}