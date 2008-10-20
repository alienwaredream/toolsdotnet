using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Tools.UI.Windows.Descriptors
{
    public partial class SymmetricEncryptionSettingsControl : UserControl
    {
        // RijndaelManaged, DESCryptoServiceProvider, RC2CryptoServiceProvider, and TripleDESCryptoServiceProvider 
        private readonly List<string> algorithms = new List<string>(10);
        private string _algorithmName;

        //private int _feedbackSize;
        private int _keySize;

        private CipherMode _mode;

        private PaddingMode _paddingMode;

        public SymmetricEncryptionSettingsControl()
        {
            InitializeComponent();
            cipherModeComboBox.DataSource =
                Enum.GetValues(typeof (CipherMode));
            paddingTypeComboBox.DataSource =
                Enum.GetValues(typeof (PaddingMode));

            //SymmetricAlgorithm.Create(
            algorithms.Add("<Default>");
            algorithms.Add("Rijndael");
            algorithms.Add("DES");
            algorithms.Add("RC2");
            algorithms.Add("3DES");

            algorithmNameComboBox.DataSource =
                algorithms;
        }

        public int BlockSize { get; set; }

        public int KeySize
        {
            get { return Convert.ToInt32(keySizeComboBox.Text); }
            set { _keySize = value; }
        }

        public CipherMode Mode
        {
            get { return (CipherMode) Enum.Parse(typeof (CipherMode), cipherModeComboBox.Text); }
            set { _mode = value; }
        }

        public PaddingMode PaddingMode
        {
            get { return (PaddingMode) Enum.Parse(typeof (PaddingMode), paddingTypeComboBox.Text); }
            set { _paddingMode = value; }
        }

        public string AlgorithmName
        {
            get { return algorithmNameComboBox.Text; }
            set { _algorithmName = value; }
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
            for (int i = 0; i < comboBox.Items.Count; i++)
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
            var sizes = new int[1];

            if (keySizes[0].SkipSize != 0)
            {
                sizes = new int[((keySizes[0].MaxSize - keySizes[0].MinSize)/keySizes[0].SkipSize) + 1];
            }
            for (int i = 0; i < sizes.Length; i++)
            {
                sizes[i] = keySizes[0].MinSize + keySizes[0].SkipSize*i;
            }
            return sizes;
        }
    }
}