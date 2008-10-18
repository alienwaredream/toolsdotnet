using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Tools.UI.Windows.Descriptors
{
    /*
     * For this expiriment T will be DescriptiveNameValue<string>
     * ContainedType will DescritiveList<T>
     * SettingsType will be ListViewSettings
     * */
    public partial class ContainersControl
        <SettingsType, ContainedType, T> : UserControl
        where ContainedType : new()
        where SettingsType : IListSettings, new() 
        where T: ICloneable, new()
    {
        private Dictionary<string, TabPage> tabPages;
        private IDomainsProvider<T> domainsProvider;
        // This is a workaround for the bug of resource manager for generic controls!
        private ImageListHolderControl imageListControl = new ImageListHolderControl();

        #region Events

        public event ValueSelectedDelegate<T> ValueSelected;

        #endregion Events

        #region OnEvents

        protected void OnValueSelected(ValueSelectedEventArgs<T> e)
        {
            if (ValueSelected != null)
            {
                ValueSelected
                (
                this,
                e
                );
            }
        } 

        #endregion

        private DescriptiveList<Container<SettingsType, ContainedType>> _containers;

        public DescriptiveList<Container<SettingsType, ContainedType>> Containers
        {
            get { return _containers; }
            set 
            { 
                _containers = value;
                InitializePages(value);
            }
        }
        public Container<SettingsType, ContainedType> SelectedContainer
        {
            get
            {
                if (this.containersTabControl.SelectedTab == null) return null;
                return this.containersTabControl.SelectedTab.Tag as Container<SettingsType, ContainedType>;
            }
        }

        public ContainersControl
            (
            DescriptiveList<Container<SettingsType, ContainedType>> containers,
            IDomainsProvider<T> domainsProvider
            )
        {
            InitializeComponent();
            this.domainsProvider = domainsProvider;
            this.containersTabControl.ImageList = imageListControl.LockImageList;
            tabPages = new Dictionary<string, TabPage>();
            this._containers = containers;
            InitializePages(_containers);
            //this.containersTabControl.

        }
        private void InitializePages
            (
            DescriptiveList<Container<SettingsType, ContainedType>> containers
            )
        {
            this.SuspendLayout();

            foreach (TabPage tp in containersTabControl.TabPages)
            {
                containersTabControl.TabPages.Remove(tp);
                //tp.Dispose();
            }

            for (int i = 0; i < containers.Count; i++)
            {
                bindContainer(containers[i]);
            }

            this.ResumeLayout(true);
        }

        private void bindContainer
            (
            Container<SettingsType, ContainedType> container
            )
        {

            TabPage page = new TabPage((container as Container<SettingsType, ContainedType>).Name);
            page.ImageIndex = 0;
            page.Tag = container;
            
            containersTabControl.TabPages.Add(page);


            GenericCollectionControl<T, SettingsType> collControl =
                new GenericCollectionControl<T, SettingsType>
                (
                domainsProvider,
                (IList<T>)container.ContainerObject, // This cast is a violation of course.
                container,
                container.Settings
                );

            collControl.ValueSelected +=
                new ValueSelectedDelegate<T>(collControl_ValueSelected);

            page.Controls.Add(collControl);
            //page.an
            collControl.Size = page.ClientSize;
            collControl.Dock = DockStyle.Fill;
            collControl.DockChanged += new EventHandler(collControl_DockChanged);
            collControl.Parent.Dock = DockStyle.Fill;

        }

        void collControl_DockChanged(object sender, EventArgs e)
        {
            throw new Exception("The method or operation is not implemented.");
        }
        private void collControl_ValueSelected(object sender, ValueSelectedEventArgs<T> e)
        {
            OnValueSelected(e);
        }

        private void containersTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addNewListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Container<SettingsType, ContainedType> container =
                new Container<SettingsType, ContainedType>();
            this._containers.Add(container);
            this.bindContainer(container);
        }

        private void removeTheListToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
