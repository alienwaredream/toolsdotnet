using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Tools.Core;

namespace Tools.UI.Windows.Descriptors
{
    //TODO: POC only, very far from being final (SD)
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class EnumEditControl<T> : UserControl, IChangeEventRaiser
        //where T: System.IConvertible
    {
        System.Type enumType = typeof(T);
        private List<CheckBox> enumCheckBoxes;
        int zeroIndex = -1;

        public T Value
        {
            get
            {
                return getValueFromUI();
            }
            set
            {
                setValueToUI(value);
            }
        }

        public EnumEditControl
            (
            T initialValue
            )
        {
            InitializeComponent();

            enumCheckBoxes = new List<CheckBox>(10);

            addEnumOptionsToControl
            (
            this
            );

            setValueToUI(initialValue);
        }

        private T getValueFromUI()
        {
            int valCandidate = getIntValueFromUI();

            //if (!Enum.IsDefined(enumType, valCandidate))
            //{
            //    throw
            //        new Exception
            //        (
            //        "Value of the control can't be parsed to the enum value!" +
            //        " Value selected is " + valCandidate.ToString() + "."
            //        );
            //}
            //else
            //{
                return
                    (T)Enum.ToObject(enumType, valCandidate);
            //}
        }

        private int getIntValueFromUI()
        {
            Array ar = Enum.GetValues(enumType);
            int resultValue = 0;

            for (int i = 0; i < ar.Length; i++)
            {
                int intTest = Convert.ToInt32(ar.GetValue(i));

                if (enumCheckBoxes[i].Checked)
                {
                    if (Convert.ToInt32(enumCheckBoxes[i].Tag) == 0) return 0;

                    resultValue += Convert.ToInt32(enumCheckBoxes[i].Tag);
                }
            }
            return resultValue;
        }
        private void setValueToUI(T val)
        {
            Array ar = Enum.GetValues(enumType);

            for (int i = 0; i < ar.Length; i++)
            {
                int intTest = Convert.ToInt32(ar.GetValue(i));

                if ((intTest == 0 && Convert.ToInt32(val) == 0) || (intTest & Convert.ToInt32(val)) != 0)
                {
                    enumCheckBoxes[i].Checked = true;
                }
            }
        }
        /// <summary>
        /// Adds check boxes to the control to setup enum options.
        /// Principles of the solution are specific to compatibility with CF.
        /// </summary>
        /// <param name="container"></param>
        private void addEnumOptionsToControl(Control container)
        {
            string[] names = Enum.GetNames(enumType);
            Array values = Enum.GetValues(enumType);

            for (int i = 0; i < names.Length; i++)
            {
                CheckBox enumCheckBox =
                    new CheckBox();
                enumCheckBox.Text = names[i];
                enumCheckBox.Tag = values.GetValue(i);
                if (Convert.ToInt32(enumCheckBox.Tag) == 0) zeroIndex = i;
                enumCheckBox.Top = 25 + enumCheckBox.Height * i + 3;
                enumCheckBox.Left = 10;
                enumCheckBox.CheckedChanged += new EventHandler(enumCheckBox_CheckedChanged);
                container.Controls.Add(enumCheckBox);
                enumCheckBoxes.Add(enumCheckBox);
            }
        }

        void enumCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(((CheckBox)sender).Tag) == 0 && ((CheckBox)sender).Checked)
            {
                for (int i = 0; i < enumCheckBoxes.Count; i++)
                {
                    if (enumCheckBoxes[i] != (CheckBox)sender)
                    {
                        enumCheckBoxes[i].Checked = false;
                    }
                }
            }
            else if (((CheckBox)sender).Checked&&zeroIndex!=-1)
            {
                enumCheckBoxes[zeroIndex].Checked = false;
            }


            string errorText = validateEnumState();

            if (!String.IsNullOrEmpty(errorText))
            {
                MessageBox.Show(errorText + " Change will be discarded.", "Error", MessageBoxButtons.OK);
                ((CheckBox)sender).Checked = !((CheckBox)sender).Checked;
                return;
            }

            onChanged();
        }
        private string validateEnumState()
        {
            int valCandidate = getIntValueFromUI();

            //if (!Enum.IsDefined(enumType, valCandidate))
            //{
            //    return
            //        "Value of the control can't be parsed to the enum value!" +
            //        " Value selected is " + valCandidate.ToString() + ".";
            //}
            //else
            //{
                return
                    null;
            //}
        }

        #region IChangeEventRaiser Members

        public event EventHandler Changed;

        private void onChanged()
        {
            if (!String.IsNullOrEmpty(validateEnumState())) return;

            if (Changed != null)
            {
                Changed(this, EventArgs.Empty);
            }
        }



        #endregion
}
}
