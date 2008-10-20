using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace Tools.UI.Windows.Descriptors
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof (IDesigner))]
    public partial class CollapsibleContainer : UserControl
    {
        private bool _collapsed;
        private Control _containedControl;
        private DockStyle _expandDockStyle = DockStyle.None;
        private int _expandHeight;

        private string _title;

        public CollapsibleContainer
            (
            )
        {
            InitializeComponent();
            collapseToolBar.Collapsed += collapseToolBar_Collapse;
            collapseToolBar.Expanded += collapseToolBar_Expand;
            Resize += CollapsibleContainer_Resize;
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                setTitle(value);
            }
        }

        public Control ContainedControl
        {
            get { return _containedControl; }
            set
            {
                _containedControl = value;
                setContainedControl(_containedControl);
            }
        }

        public event EventHandler Collapsed;
        public event EventHandler Expanded;

        private void setTitle(string title)
        {
            collapseToolBar.Title = title;
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

        private void setContainedControl(Control containedControl)
        {
            if (containedControl != null)
            {
                _containedControl = containedControl;
                _containedControl.Dock = DockStyle.Fill;
                containerPanel.Controls.Add(_containedControl);
            }
        }

        private void CollapsibleContainer_Resize(object sender, EventArgs e)
        {
            collapseToolBar.Width = ClientSize.Width;
            if (Parent != null)
            {
                Width = Parent.ClientSize.Width;
            }
        }

        public void Collapse()
        {
            SuspendLayout();
            _expandHeight = Height;
            _expandDockStyle = Dock;
            Dock = DockStyle.None;
            containerPanel.Visible = false;
            Height = collapseToolBar.Height;
            ResumeLayout();
            OnCollapsed();
            collapseToolBar.Collapse(true);
        }

        public void Expand()
        {
            _collapsed = false;

            SuspendLayout();
            containerPanel.Visible = true;
            Height = _expandHeight;
            Dock = _expandDockStyle;
            ResumeLayout();
            OnExpanded();
            collapseToolBar.Expand(true);
        }

        private void collapseToolBar_Expand(object sender, EventArgs e)
        {
            Expand();
        }

        private void collapseToolBar_Collapse(object sender, EventArgs e)
        {
            Collapse();
        }

        private void collapseToolBar_Load(object sender, EventArgs e)
        {
        }
    }
}