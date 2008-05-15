using System;
using System.Collections.Generic;

using System.Text;
using System.Security.Cryptography;
using System.Diagnostics;
using System.IO;
using Tools.Common.Config;

namespace Tools.Common.Cryptography
{
    public class StringCryptoTransformer : IStringCryptoTransformer
    {
        IConfigurationValueProvider configProvider;

        public StringCryptoTransformer(IConfigurationValueProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        #region ITokenCryptoTransformer Members

        public string Encrypt(string plainText)
        {
            using (Rijndael cryptoProvider = RijndaelManaged.Create())
            {

                ICryptoTransform encryptor = cryptoProvider.CreateEncryptor(
                    Convert.FromBase64String(configProvider["key"]),
                    Convert.FromBase64String(configProvider["iv"]));

                Debug.WriteLine("Key:" + Convert.ToBase64String(cryptoProvider.Key) + " Length: " + cryptoProvider.Key.Length);
                Debug.WriteLine("IV:" + Convert.ToBase64String(cryptoProvider.IV));
                // Create the streams used for encryption.
                MemoryStream msEncrypt;

                using (msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(
                        msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public string Decrypt(string cipherText)
        {
            using (Rijndael cryptoProvider = RijndaelManaged.Create())
            {
                ICryptoTransform decryptor = cryptoProvider.CreateDecryptor(
                    Convert.FromBase64String(configProvider["key"]),
                    Convert.FromBase64String(configProvider["iv"]));
                // Create the streams used for encryption.
                ;

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(
                        msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }

        #endregion
    }
}
