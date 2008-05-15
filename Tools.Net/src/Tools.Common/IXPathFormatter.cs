using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;

namespace Tools.Common
{
    public interface IXPathFormatter
    {
        XPathNavigator Format(object data);
    }
}
