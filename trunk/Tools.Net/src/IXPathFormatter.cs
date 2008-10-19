using System;
using System.Collections.Generic;

using System.Text;
using System.Xml.XPath;

namespace Tools.Core
{
    public interface IXPathFormatter
    {
        XPathNavigator Format(object data);
    }
}
