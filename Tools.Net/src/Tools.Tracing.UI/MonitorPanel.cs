#region Using directives

using System;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace Tools.Tracing.UI
{
	public partial class MonitorPanel : UserControl
	{
		public event ScrollEventHandler Scroll;
		private int _messagesCount = 0;
		private int listOldHeight = 0;
        private int _filteredOutCount = 0;
        private bool _gridLines = false;

        public bool GridLines
        {
            get { return scrollableListView.GridLines; }
            set { scrollableListView.GridLines = value; }
        }

        public int CountToShow
        {
            get
            {
                return scrollableListView.CountToShow;
            }
            set
            {
                scrollableListView.CountToShow = value;
            }
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
                //this.Invoke
                //(
                //new SetIntAccessorDelegate
                //(
                //setfilteredOutCountGui
                //),
                //new object[] { value}
                //);
                //}
            }
        }

		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int MessagesCount
		{
			get
			{
				return _messagesCount;
			}
			set
			{
				_messagesCount = value;
				this.scrollableListView.SetMaxIndex(_messagesCount);
                //this.BeginInvoke(
                //    new VoidDelegate
                //    (
                //    this.showMessagesCount
                //    ));
			}
		}

		protected override void OnScroll(ScrollEventArgs e)
		{
			if (Scroll != null)
			{
				Scroll(this, e);
			}
		}
		public ScrollableListView ItemsListView
		{
			get
			{
				return scrollableListView;
			}
		}
		protected virtual void listView1_Scrolled(object source, ScrollEventArgs e)
		{
			OnScroll(e);
		}
		public MonitorPanel()
		{
			InitializeComponent();
			scrollableListView.Scroll += new ScrollEventHandler(listView1_Scrolled);
            for (int i = 0; i < TraceRecord.FieldNames.Length; i++)
            {
                ColumnHeader ch =
                    new ColumnHeader
                    (
                    );
                ch.Name = TraceRecord.FieldNames[i] + "Header";
                ch.Text = TraceRecord.FieldNames[i];
                ch.Width = TraceRecord.FieldUILengths[i];
                scrollableListView.Columns.Add(ch);
            }
			
		}
		public void AddItem(ListViewItem lvi)
		{
			this.scrollableListView.InsertToTop
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
			listOldHeight = this.ItemsListView.Height;
			this.ItemsListView.Height = 0;
		}

		private void genericNameToolStripStatusLabel_Click(object sender, EventArgs e)
		{
		
		}

		private void minimizeToolStripSplitButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show("Maximize pressed");
			this.ItemsListView.Height = listOldHeight;

		}
	}
}
