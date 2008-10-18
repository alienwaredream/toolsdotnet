using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Tools.UI.Windows.Descriptors
{
    public partial class CollapseToolBar : UserControl
    {
        private string _title = null;
        private bool _isCollapsed = false;

        public string Title
        {
            get { return _title; }
            set 
            { 
                _title = value;
                setTitle(value);
            }
        }

        private void setTitle(string title)
        {
            if (title == null) return;
            int charsToShow = titleLabel.Width / Convert.ToInt32(titleLabel.Font.SizeInPoints);
            if (charsToShow < title.Length)
            {
                titleLabel.Text = title.Substring(0, charsToShow - 4);
                titleLabel.Text += " ...";
                return;
            }
            titleLabel.Text = title;
        }

        public bool IsCollapsed
        {
            get { return _isCollapsed; }
        }


        public event EventHandler Collapsed;
        public event EventHandler Expanded;
        

        protected virtual void OnCollapsed()
        {
            if (Collapsed != null)
            {
                Collapsed(this, EventArgs.Empty);
            }
        }
        protected virtual void OnExpanded()
        {
            if (Expanded != null)
            {
                Expanded(this, EventArgs.Empty);
            }
        }
        private void setCollapseButtons()
        {
            collapseButton.Enabled = !_isCollapsed;
            expandButton.Enabled = _isCollapsed;
        }
        public CollapseToolBar
            (
            )
        {
            InitializeComponent();
            setCollapseButtons();
            this.Resize += new EventHandler(CollapseToolBar_Resize);
        }

        void CollapseToolBar_Resize(object sender, EventArgs e)
        {
            setTitle(_title);
        }

        public void Collapse(bool forced)
        {
            _isCollapsed = true;
            setCollapseButtons();
            if (!forced) OnCollapsed();
        }
        public void Expand(bool forced)
        { 
            _isCollapsed = false;
            setCollapseButtons();
            if (!forced) OnExpanded();
        }
        private void collapseButton_Click(object sender, EventArgs e)
        {
            Collapse(false);
        }

        private void expandButton_Click(object sender, EventArgs e)
        {
            Expand(false);
        }

    }
}
