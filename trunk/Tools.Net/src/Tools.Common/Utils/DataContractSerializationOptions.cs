using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Common.Utils
{
    /// <summary>
    /// Contains only values now to drive XmlDictionaryWriter Create factory.
    /// </summary>
    public enum DataContractSerializationOptions
    {
        /// <summary>
        /// Leaves defaults. TODO:(SD) find out what exactly is a default.
        /// </summary>
        None = 0,
        Binary = 1,
        Dictionary = 2,
        Mtom = 3,
        Text = 4,
    }
}
