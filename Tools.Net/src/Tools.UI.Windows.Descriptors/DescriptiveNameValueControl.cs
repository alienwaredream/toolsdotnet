using System;
using System.Windows.Forms;
using Tools.Core;

namespace Tools.UI.Windows.Descriptors
{
    public partial class DescriptiveNameValueControl : UserControl
    {
        private readonly DescriptiveNameValueDomainsProvider _domainsProvider;
        private readonly DescriptorControl descriptorControl;
        private DescriptiveNameValue<string> _currentValue;
        private MarksPresentationType _marksViewType = MarksPresentationType.Encoded;
        private bool _readOnly;
        private DescriptiveNameValue<string> _sourceValue;
        private int oldSplitterPosition;

        #region Properties

        public bool ReadOnly
        {
            get { return _readOnly; }
            set
            {
                if (_readOnly != value)
                {
                    _readOnly = value;
                    valueRichTextBox.ReadOnly = _readOnly;
                    descriptorControl.ReadOnly = _readOnly;
                }
            }
        }

        public DescriptiveNameValue<string> SourceValue
        {
            get { return _sourceValue; }
            set
            {
                _sourceValue = value;
                _currentValue = (_sourceValue == null) ? null : (DescriptiveNameValue<string>) _sourceValue.Clone();
                renderCurrentValue();
            }
        }

        public DescriptiveNameValue<string> CurrentValue
        {
            get
            {
                setValuePropertiesFromUI();
                return _currentValue;
            }
        }

        public MarksPresentationType MarksViewType
        {
            get { return _marksViewType; }
            set
            {
                if (_marksViewType != value)
                {
                    _domainsProvider.MarksPresentationType = value;
                    // Store the most recent value back to the current if decoded is requested,
                    // if encoded is requested there is no need in that as we would be storing
                    // the decoded value then.
                    if (value == MarksPresentationType.Decoded) _currentValue.Value = valueRichTextBox.Text;
                    _marksViewType = value;
                    renderValueForMarksViewType();
                }
            }
        }

        #endregion Properties

        public DescriptiveNameValueControl
            (
            DescriptiveNameValueDomainsProvider domainsProvider
            )
        {
            InitializeComponent();
            //_value = new DescriptiveNameValue<string>
            //(
            //this.nameTextBox.Text,
            //this.valueRichTextBox.Text,
            //this.descriptionRichTextBox.Text
            //);
            _domainsProvider = domainsProvider;
            descriptorControl = new DescriptorControl();
            collapsibleContainer1.ContainedControl = descriptorControl;

            collapsibleContainer1.Collapsed += collapsibleContainer1_Collapsed;
            collapsibleContainer1.Expanded += collapsibleContainer1_Expanded;
            collapsibleContainer1.Collapse();
        }

        private void renderValueForMarksViewType()
        {
            if (_currentValue == null)
            {
                return;
            }


            if (MarksViewType == MarksPresentationType.Decoded)
            {
                //DescriptiveNameValue<string> decodedValue =
                //    (DescriptiveNameValue<string>)_currentValue.Clone();
                //decodedValue.Value = valueRichTextBox.Text;
                //_currentValue.Value = valueRichTextBox.Text;
                String[] values =
                    _domainsProvider.GetDomainValues
                        (
                        _currentValue
                        );
                valueRichTextBox.Text = values[2];
                return;
            }
            valueRichTextBox.Text = _currentValue.Value;
        }

        public void AcceptChanges()
        {
            if (_sourceValue == null) return;

            setValuePropertiesFromUI();

            _sourceValue.Name = _currentValue.Name;
            _sourceValue.Description = _currentValue.Description;
            _sourceValue.Value = _currentValue.Value;
        }

        private void setValuePropertiesFromUI()
        {
            if (_currentValue == null) return;


            _currentValue.Name = descriptorControl.Descriptor.Name;
            _currentValue.Description = descriptorControl.Descriptor.Description;

            if (MarksViewType == MarksPresentationType.Encoded)
            {
                _currentValue.Value = valueRichTextBox.Text;
            }
        }

        private void renderCurrentValue()
        {
            if (_sourceValue == null)
            {
                Clear();
                Enabled = false;
                return;
            }
            Enabled = true;
            descriptorControl.Descriptor =
                new Descriptor
                    (
                    _sourceValue.Name,
                    _sourceValue.Description
                    );
            int length2Show =
                (descriptorControl.Descriptor.Description.Length > 20)
                    ? 20
                    : descriptorControl.Descriptor.Description.Length;
            collapsibleContainer1.Title =
                descriptorControl.Descriptor.Name +
                "(" +
                descriptorControl.Descriptor.Description.Substring(0, length2Show)
                + ")";
            //this.nameTextBox.Text = _sourceValue.Name;
            renderValueForMarksViewType();
        }

        private void Clear()
        {
            valueRichTextBox.Text = null;
            descriptorControl.Clear();
        }

        private void collapsibleContainer1_Collapsed(object sender, EventArgs e)
        {
            oldSplitterPosition = splitContainer1.SplitterDistance;
            splitContainer1.SplitterDistance = ClientRectangle.Height - collapsibleContainer1.Height - 3;
        }

        private void collapsibleContainer1_Expanded(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = oldSplitterPosition;
        }

        private void DescriptiveNameValueControl_Load(object sender, EventArgs e)
        {
        }

        private void statementRichTextBox_TextChanged(object sender, EventArgs e)
        {
        }
    }
}