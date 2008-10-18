using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tools.Tracing.UI
{
	/// <summary>
	/// Summary description for ScrollableListView.
	/// </summary>
	public class ScrollableListView : System.Windows.Forms.ListView
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private VScrollBar vScrollBar1;

		private int _maxIndex = 1;
		private int _minIndex = 0;
		private int _currentIndex = 0;
		private int _countToShow = 20;

		public int CountToShow
		{
			get
			{
				return _countToShow;
			}
			set
			{
				_countToShow = value;
			}
		}
		//POC: Part of the closest control Invoke POC,
        // whereas you can of course find normal Invoke pattern via creating the wrapping
        // method anyway (SD),
        // But this concept is not worth for trying no more as it fails when initializing the
        // control and setting this value at the design time or initialization while handling
        // has not been yet created (SD)
        private Action<int> setvScrollBar1MaxValueDelegate = null;

		public void ClearItems()
		{
			Items.Clear();
			//MaxIndex = 1;
		}
		public void InsertToTop(ListViewItem lvi)
		{
			this.SuspendLayout();

			this.Items.Insert(0, lvi);

			if (this.Items.Count > _countToShow)
			{
				this.Items.RemoveAt(_countToShow);
			}

			this.ResumeLayout();
		}
		public void SetMaxIndex(int n)
		{
			MaxIndex = n;
		}
		protected int MaxIndex
		{
			get 
			{
				return _maxIndex;
			}
			set
			{
				_maxIndex = value;
				if (value == 0) return; // To return to later (SD)
                
//                vScrollBar1.Invoke
//                    (
//                    setvScrollBar1MaxValueDelegate,
//                    _maxIndex
//                    );
//                if (this.Handle != null)
//                {
                    this.Invoke
                        (
                        setvScrollBar1MaxValueDelegate,
                        _maxIndex
                        );
//                }

            }
		}
        internal void SetScrollBarMaxValue(int val)
        {
            vScrollBar1.Maximum = val;
        }
        public int MinIndex
		{
			get 
			{
				return _minIndex;
			}
			set
			{
				_minIndex = value;
				vScrollBar1.Minimum = _minIndex;
			}
		}
		public int CurrentIndex
		{
			get 
			{
				return this.vScrollBar1.Value;
			}
		}
		public event ScrollEventHandler Scroll;

		protected virtual void OnScrolled(ScrollEventArgs e)
		{
			if (Scroll!=null) Scroll(this, e);
		}

//		private const int WM_VSCROLL = 0x115;

//		protected override void WndProc(ref Message msg)
//		{
//			// Look for the WM_VSCROLL or the WM_HSCROLL messages.
//			if (msg.Msg == WM_VSCROLL)
//			{
//				// Move focus to the ListView to cause ComboBox to lose focus.
//				OnScrolled(); 
//			}
//
//			// Pass message to default handler.
//			base.WndProc(ref msg);
//		} 
		public ScrollableListView()
			: this(20)
		{

		}
		public ScrollableListView
			(
			int countToShow
			)
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			this.Scrollable	= true;
			_countToShow = countToShow;
			// Create and initialize a VScrollBar.
			vScrollBar1 = new VScrollBar();
			vScrollBar1.Size = new Size(15, this.Height);
			// Dock the scroll bar to the right side of the form.
			vScrollBar1.Dock = DockStyle.Right;

			vScrollBar1.Scroll +=new ScrollEventHandler(vScrollBar1_Scroll);
    
			// Add the scroll bar to the form.
			Controls.Add(vScrollBar1);
            setvScrollBar1MaxValueDelegate =
                new Action<int>
                (
                this.SetScrollBarMaxValue
                );
//                Delegate.CreateDelegate
//                (
//                typeof(SetIntAccessorDelegate),
//                vScrollBar1,
//                "set_Maximum"
//                ) as SetIntAccessorDelegate;
			this.Resize += new EventHandler(ScrollableListView_Resize);

		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
		{
			OnScrolled(e);
		}

		void ScrollableListView_Resize(object sender, EventArgs e)
		{
            //_countToShow =
            //    (int)Math.Floor
            //    (
            //    Convert.ToDouble(ClientRectangle.Height) /
            //    realLVFontSize
            //    );
            this.Invalidate();
		}
	}
}
