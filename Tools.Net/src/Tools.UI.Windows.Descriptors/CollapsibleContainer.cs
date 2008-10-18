using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Tools.UI.Windows.Descriptors
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public partial class CollapsibleContainer : UserControl
    {
        private Control _containedControl;
        private bool _collapsed = false;
        private int _expandHeight = 0;
        private DockStyle _expandDockStyle = DockStyle.None;

        public event EventHandler Collapsed;
        public event EventHandler Expanded;

        private string _title = null;

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
            this.collapseToolBar.Title = title;
        }

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
        //protected override void OnControlAdded(ControlEventArgs e)
        //{
        //    if (e.Control == this.collapseToolBar||this.containerPanel==e.Control)
        //    {
        //        base.OnControlAdded(e);
        //        return;
        //    }
        //    this.containerPanel.Controls.Add(e.Control);
        //    this.Controls.Remove(e.Control);
        //    base.OnControlAdded(e);
        //}
        public Control ContainedControl
        {
            get { return _containedControl; }
            set 
            { 
                _containedControl = value;
                setContainedControl(_containedControl);
            }
        }

        private void setContainedControl(Control containedControl)
        {
            if (containedControl != null)
            {
                _containedControl = containedControl;
                _containedControl.Dock = DockStyle.Fill;
                containerPanel.Controls.Add(_containedControl);
            }
        }

        public CollapsibleContainer
            (
            )
        {
            InitializeComponent();
            this.collapseToolBar.Collapsed += new EventHandler(collapseToolBar_Collapse);
            this.collapseToolBar.Expanded += new EventHandler(collapseToolBar_Expand);
            this.Resize += new EventHandler(CollapsibleContainer_Resize);
        }

        void CollapsibleContainer_Resize(object sender, EventArgs e)
        {
            this.collapseToolBar.Width = this.ClientSize.Width;
            if (Parent != null)
            {
                this.Width = Parent.ClientSize.Width;
            }
        }
        public void Collapse()
        {
            this.SuspendLayout();
            _expandHeight = this.Height;
            _expandDockStyle = Dock;
            Dock = DockStyle.None;
            this.containerPanel.Visible = false;
            this.Height = collapseToolBar.Height;
            this.ResumeLayout();
            OnCollapsed();
            collapseToolBar.Collapse(true);
        }
        
        public void Expand()
        {
            _collapsed = false;

            this.SuspendLayout();
            this.containerPanel.Visible = true;
            this.Height = _expandHeight;
            this.Dock = _expandDockStyle;
            this.ResumeLayout();
            OnExpanded();
            collapseToolBar.Expand(true);
        }
        void collapseToolBar_Expand(object sender, EventArgs e)
        {
            Expand();
        }

        void collapseToolBar_Collapse(object sender, EventArgs e)
        {
            Collapse();
        }

        private void collapseToolBar_Load(object sender, EventArgs e)
        {

        }
    }
}
