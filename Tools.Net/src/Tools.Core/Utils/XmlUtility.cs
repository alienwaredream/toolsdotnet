using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Core.Utils
{
    public static class XmlUtility
    {
        public static string Encode(char input)
        {
            switch (input)
            {
                case '\n':
                    return "&#xA;";

                case '\r':
                    return "&#xD;";

                case '&':
                    return "&amp;";

                case '\'':
                    return "&apos;";

                case '"':
                    return "&quot;";

                case '<':
                    return "&lt;";

                case '>':
                    return "&gt;";
                default:
                    return new string(input, 1);
            }
        }
    }
}
