using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Tools.Core;
using Tools.Core.Utils;

namespace Tools.UI.Windows.Descriptors
{
    /// <summary>
    /// Provides list gui for the generic list, requires a default ctor to exist.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class GenericCollectionControl<T, SettingsType> : UserControl
        where T : ICloneable, new()
        where SettingsType : IListSettings, new()
    {
        #region Globals

        private readonly IDomainsProvider<T> domainsProvider;
        private readonly SettingsType settings = new SettingsType();
        private ListViewItem _previousListViewItem;
        private T _previousValue;
        private ListViewItem _selectedListViewItem;
        private T _selectedValue;
        private ICollection<T> _values;

        #region Events

        public event ValueSelectedDelegate<T> ValueSelected;

        #endregion Events

        #endregion Globals

        #region Properties

        public T SelectedValue
        {
            get { return _selectedValue; }
        }

        public ICollection<T> Values
        {
            get { return _values; }
            set
            {
                _values = value;
                renderValues();
            }
        }

        #endregion Properties

        #region Constructors

        public GenericCollectionControl
            (
            IDomainsProvider<T> domainsProvider,
            ICollection<T> values,
            IDescriptor descriptor,
            SettingsType settings
            )
        {
            InitializeComponent();
            this.settings = settings;

            itemsListView.SelectedIndexChanged += itemsListView_SelectedIndexChanged;
            itemsListView.MultiSelect = true;
            itemsListView.HeaderStyle = ColumnHeaderStyle.Clickable;
            itemsListView.FullRowSelect = true;
            itemsListView.AllowColumnReorder = true;
            //itemsListView.Dock = DockStyle.Fill;

            this.domainsProvider = domainsProvider;

            foreach (string columnName in domainsProvider.GetDomainNames())
            {
                itemsListView.Columns.Add
                    (
                    columnName
                    );
            }
            Values = values;
            ContextMenuStrip = contextMenuStrip;


            descriptorContainer.Collapsed += descriptorContainer_Collapse;
            descriptorContainer.Expanded += descriptorContainer_Expand;

            descriptorControl1 = new DescriptorControl();
            descriptorControl1.Descriptor = descriptor;
            descriptorContainer.ContainedControl = descriptorControl1;
            descriptorContainer.Title = descriptor.Name + " (" + descriptor.Description + ")";


            if (!settings.ListViewSettings.ShowListNameDescription)
            {
                descriptorContainer.Collapse();
            }
            // That is for the resizing bug in the .NET2.0B2
            Resize += GenericCollectionControl_Resize;
        }

        private void descriptorContainer_Expand(object sender, EventArgs e)
        {
            layoutControls();
            settings.ListViewSettings.ShowListNameDescription = true;
        }

        private void descriptorContainer_Collapse(object sender, EventArgs e)
        {
            layoutControls();
            settings.ListViewSettings.ShowListNameDescription = false;
        }

        private void layoutControls()
        {
            itemsListView.Height = ClientSize.Height - descriptorContainer.Height;
            itemsListView.Top = descriptorContainer.Bottom;
        }


        private void GenericCollectionControl_Resize(object sender, EventArgs e)
        {
            descriptorContainer.Width = ClientSize.Width;
            itemsListView.Width = ClientSize.Width;
            itemsListView.Height = ClientSize.Height - descriptorContainer.Height;
        }

        #endregion Constructors

        #region List view navigation

        private void itemsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (itemsListView.SelectedItems.Count == 0)
            {
                if (_previousValue != null)
                {
                    applyOrdinaryStyle(_previousListViewItem);
                }
                return;
            }
            if (itemsListView.SelectedItems.Count > 1)
            {
                _selectedValue = default(T);
                //Given a variable t of a parameterized type T, the statement t = null is only 
                //valid if T is a reference type and t = 0 will only work for numeric 
                //value types but not for structs. The solution is to use the default 
                //keyword, which will return null for reference types and zero for numeric value types.
                //For structs, it will return each member of the struct initialized to zero or 
                //null depending on whether they are value or reference types.
                OnValueSelected();
                return;
            }

            _selectedListViewItem = itemsListView.SelectedItems[0];
            _selectedValue = (T) _selectedListViewItem.Tag;
            // apply styles

            applySelectedStyle(_selectedListViewItem);

            OnValueSelected();

            updateListViewItem(_previousListViewItem);

            _previousListViewItem = _selectedListViewItem;
            _previousValue = (T) _previousListViewItem.Tag;
        }

        private void applySelectedStyle(ListViewItem listItem)
        {
            listItem.ForeColor = Color.DarkBlue;
        }

        private void applyOrdinaryStyle(ListViewItem listItem)
        {
            listItem.ForeColor = Color.Black;
        }

        protected void OnValueSelected()
        {
            if (ValueSelected != null)
            {
                ValueSelected
                    (
                    this,
                    new ValueSelectedEventArgs<T>
                        (
                        _previousValue,
                        _selectedValue
                        ));
            }
        }

        #endregion List view navigation

        #region List view maintenance

        private void resetValues()
        {
            itemsListView.Items.Clear();
        }

        private void renderValues()
        {
            resetValues();
            itemsListView.SuspendLayout();
            foreach (T val in Values)
            {
                SuspendLayout();
                AddValue(val, false);
            }
            itemsListView.ResumeLayout();
        }

        private void updateListViewItem(ListViewItem lvi)
        {
            if (lvi == null) return;

            string[] vals =
                domainsProvider.GetDomainValues
                    (
                    (T) lvi.Tag
                    );
            if (vals.Length != lvi.SubItems.Count)
            {
                throw new Exception
                    (
                    "Count of listview columns is different from domains count!"
                    );
            }
            for (int i = 0; i < vals.Length; i++)
            {
                lvi.SubItems[i].Text = vals[i];
            }
        }

        public void AddValue(T value)
        {
            AddValue(value, true);
        }

        private void AddValue(T value, bool applyToValues)
        {
            var listItem2Add =
                new ListViewItem
                    (
                    domainsProvider.GetDomainValues
                        (
                        value
                        ));

            listItem2Add.Tag = value;
            itemsListView.Items.Insert
                (
                0,
                listItem2Add
                );
            itemsListView.TopItem = listItem2Add;
            if (applyToValues) _values.Add(value);
        }

        #endregion List view maintenance

        #region Item management menu handling

        private void removeStripMenuItem_Click(object sender, EventArgs e)
        {
            removeSelectedValue(true);
        }

        private void removeSelectedValue(bool applyToValues)
        {
            if (itemsListView.SelectedItems.Count == 0) return;

            var dnv = (T) itemsListView.SelectedItems[0].Tag;

            itemsListView.Items.RemoveAt
                (
                itemsListView.SelectedIndices[0]
                );

            if (applyToValues) _values.Remove(dnv);
        }

        private void copyAsNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (itemsListView.SelectedItems.Count == 0)
                return;
            copyAsNew(SelectedValue);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            generateNew();
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (itemsListView.SelectedItems.Count == 0)
                return;
            copySelectedToClipboard();
        }

        private void copySelectedToClipboard()
        {
            try
            {
                ICollection<T> selectedValues =
                    new List<T>();
                int selectedCount = itemsListView.SelectedItems.Count;
                for (int i = 0; i < selectedCount; i++)
                {
                    selectedValues.Add
                        (
                        (T) itemsListView.SelectedItems[i].Tag
                        );
                }
                Clipboard.SetDataObject
                    (
                    SerializationUtility.Serialize2String(selectedValues),
                    true
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion Item management menu handling

        #region Item management - Subject to move elsewhere

        private void copyAsNew(T valueToCopy)
        {
            AddValue
                (
                (T) valueToCopy.Clone(),
                true
                );
        }

        private void generateNew()
        {
            //// Subject for the where:new because we need to create an instance of this type here.
            T newItem = domainsProvider.GetNewDefaultInstance();
            AddValue
                (
                newItem,
                true
                );
        }

        #endregion Item management

        private void descriptorControl1_Load(object sender, EventArgs e)
        {
        }
    }
}