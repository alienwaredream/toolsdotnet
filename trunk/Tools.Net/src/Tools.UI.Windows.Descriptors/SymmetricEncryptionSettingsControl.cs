using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Tools.UI.Windows.Descriptors
{
    public partial class SymmetricEncryptionSettingsControl : UserControl
    {
        // RijndaelManaged, DESCryptoServiceProvider, RC2CryptoServiceProvider, and TripleDESCryptoServiceProvider 
        private List<string> algorithms = new List<string>(10);

        private int _blockSize;

        public int BlockSize
        {
            get { return _blockSize; }
            set { _blockSize = value; }
        }
        //private int _feedbackSize;
        private int _keySize;

        public int KeySize
        {
            get { return Convert.ToInt32(this.keySizeComboBox.Text); }
            set { _keySize = value; }
        }
        private CipherMode _mode;

        public CipherMode Mode
        {
            get { return (CipherMode)Enum.Parse(typeof(CipherMode), this.cipherModeComboBox.Text); }
            set { _mode = value; }
        }
        private PaddingMode _paddingMode;

        public PaddingMode PaddingMode
        {
            get { return (PaddingMode)Enum.Parse(typeof(PaddingMode), this.paddingTypeComboBox.Text); }
            set { _paddingMode = value; }
        }

        private string _algorithmName;

        public string AlgorithmName
        {
            get
            {
                return this.algorithmNameComboBox.Text;
            }
            set { _algorithmName = value; }
        }

        public SymmetricEncryptionSettingsControl()
        {
            InitializeComponent();
            this.cipherModeComboBox.DataSource =
                Enum.GetValues(typeof(System.Security.Cryptography.CipherMode));
            this.paddingTypeComboBox.DataSource =
                Enum.GetValues(typeof(System.Security.Cryptography.PaddingMode));

            //SymmetricAlgorithm.Create(
            algorithms.Add("<Default>");
            algorithms.Add("Rijndael");
            algorithms.Add("DES");
            algorithms.Add("RC2");
            algorithms.Add("3DES");

            this.algorithmNameComboBox.DataSource =
                algorithms;
        }

        private void algorithmNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selAlgorithmValue = algorithmNameComboBox.SelectedValue.ToString();

            SymmetricAlgorithm alg = null;

            if (selAlgorithmValue == "<Default>")
            {
                alg = SymmetricAlgorithm.Create();
            }
            else
            {
                alg = SymmetricAlgorithm.Create(selAlgorithmValue);
            }

            keySizeComboBox.DataSource = getKeySizesIntArray(alg.LegalKeySizes);

            blockSizeComboBox.DataSource = getKeySizesIntArray(alg.LegalBlockSizes);

            paddingTypeComboBox.SelectedIndex = 
                getSelectedIndexForValue
                (
                paddingTypeComboBox,
                alg.Padding.ToString()
                );
            cipherModeComboBox.SelectedIndex = 
                getSelectedIndexForValue
                (
                cipherModeComboBox,
                alg.Mode.ToString()
                );
            keySizeComboBox.SelectedIndex = 
                getSelectedIndexForValue
                (
                keySizeComboBox,
                alg.KeySize.ToString()
                );
            blockSizeComboBox.SelectedIndex = 
                getSelectedIndexForValue
                (
                blockSizeComboBox,
                alg.BlockSize.ToString()
                );

        }
        private int getSelectedIndexForValue(ComboBox comboBox, string val)
        {
            for (int i = 0; i < comboBox.Items.Count; i++ )
            {
                if (comboBox.Items[i].ToString() == val)
                {
                    return i;
                }
            }
            return -1;
        }

        private int[] getKeySizesIntArray(KeySizes[] keySizes)
        {
            int[] sizes = new int[1];

            if (keySizes[0].SkipSize != 0)
            {
                sizes = new int[((keySizes[0].MaxSize - keySizes[0].MinSize) / keySizes[0].SkipSize)+1];
            }
            for (int i = 0; i < sizes.Length; i++)
            {
                sizes[i] = keySizes[0].MinSize + keySizes[0].SkipSize * i;
            }
            return sizes;
        }
    }
}
