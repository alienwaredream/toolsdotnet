using System;
using System.Text;

namespace Tools.Core.Utils
{
    public static class XmlUtility
    {
        public static string Encode(char input)
        {
            switch (input)
            {
                case '\n': return "&#xA;";
                case '\r': return "&#xD;";
                case '&': return "&amp;";
                case '\'': return "&apos;";
                case '"': return "&quot;";
                case '<': return "&lt;";
                case '>': return "&gt;";
                default: return new string(input, 1);
            }
        }
        public static string Encode(string input)
        {
            if (String.IsNullOrEmpty(input)) return input;

            int length = input.Length;

            StringBuilder builder = new StringBuilder(length + 8);

            for (int i = 0; i < length; i++)
            {
                builder.Append(Encode(input[i]));
            }
            return builder.ToString();
        }

    }
}
