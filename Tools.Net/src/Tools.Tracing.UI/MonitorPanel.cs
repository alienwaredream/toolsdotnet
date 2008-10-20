#region Using directives

using System;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace Tools.Tracing.UI
{
    public partial class MonitorPanel : UserControl
    {
        private bool _gridLines;
        private int _messagesCount;
        private int listOldHeight;

        public MonitorPanel()
        {
            InitializeComponent();
            scrollableListView.Scroll += listView1_Scrolled;
            for (int i = 0; i < TraceRecord.FieldNames.Length; i++)
            {
                var ch =
                    new ColumnHeader
                        (
                        );
                ch.Name = TraceRecord.FieldNames[i] + "Header";
                ch.Text = TraceRecord.FieldNames[i];
                ch.Width = TraceRecord.FieldUILengths[i];
                scrollableListView.Columns.Add(ch);
            }
        }

        public bool GridLines
        {
            get { return scrollableListView.GridLines; }
            set { scrollableListView.GridLines = value; }
        }

        public int CountToShow
        {
            get { return scrollableListView.CountToShow; }
            set { scrollableListView.CountToShow = value; }
        }

        /// <summary>
        /// How many messages has been filtered out.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int FilteredOutCount { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int MessagesCount
        {
            get { return _messagesCount; }
            set
            {
                _messagesCount = value;
                scrollableListView.SetMaxIndex(_messagesCount);
                //this.BeginInvoke(
                //    new VoidDelegate
                //    (
                //    this.showMessagesCount
                //    ));
            }
        }

        public ScrollableListView ItemsListView
        {
            get { return scrollableListView; }
        }

        public event ScrollEventHandler Scroll;

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
            scrollableListView.InsertToTop
                (
                lvi
                );
        }

        public void ClearItems()
        {
            scrollableListView.SuspendLayout();
            scrollableListView.ClearItems();
            //shownCountToolStripTextBox.Text = "0";
            scrollableListView.ResumeLayout();
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