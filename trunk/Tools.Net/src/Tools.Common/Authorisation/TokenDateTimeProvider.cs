using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Tools.Common.Authorisation
{
    public class TokenDateTimeProvider : ITokenVolatileDataProvider
    {
        #region ITokenVolatileDataProvider Members

        public string GetVolatileData()
        {
            return DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
        }

        #endregion
    }
}
