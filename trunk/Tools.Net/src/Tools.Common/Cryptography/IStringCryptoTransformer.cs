using System;
using System.Collections.Generic;

using System.Text;

namespace Tools.Common.Cryptography
{
    public interface IStringCryptoTransformer
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
        
    }
}
