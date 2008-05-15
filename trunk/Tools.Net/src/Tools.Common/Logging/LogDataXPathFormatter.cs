using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.XPath;
using System.IO;
using System.Xml;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

namespace Tools.Common.Logging
{
    public class LogDataXPathFormatter : IXPathFormatter
    {

        #region IXPathFormatter Members

        public virtual XPathNavigator Format(object data)
        {
            if (data is XPathNavigator) return data as XPathNavigator; // Somebody already did the job

            String sData = data as String;

            if (!String.IsNullOrEmpty(sData))
            {
                return new XPathDocument(new StringReader(
                    CombineTraceStringForMessageOnly(sData))).CreateNavigator();
            }

            ContextualLogEntry cLogEntry = data as ContextualLogEntry;

            if (cLogEntry != null)
            {
                return new XPathDocument(new StringReader(
                    CombineTraceStringForMessageOnly(cLogEntry.Message))).CreateNavigator();
            }

            Exception exEntry = data as Exception;

            if (exEntry != null)
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder exInfo = new StringBuilder();

                using (XmlWriter xWriter = XmlWriter.Create(exInfo,
                    new XmlWriterSettings { OmitXmlDeclaration = false }))
                {
                    AddExceptionToTraceString(xWriter, exEntry);
                    sb.Append("<TraceRecord xmlns=\"http://schemas.microsoft.com/2004/10/E2ETraceEvent/TraceRecord\">").
                    Append("<TraceIdentifier>http://code.google.com/p/tools/log.aspx</TraceIdentifier>").

                        Append("<Description>Exception</Description>").
                        Append("<Exception>").Append(exInfo.ToString()).
                        Append("</Exception>").
                        Append("</Description></TraceRecord>");
                    return new XPathDocument(new StringReader(sb.ToString())).CreateNavigator();
                }
            }
            return null;
        }

        #endregion

        #region Methods - helper


        protected static string CombineTraceStringForMessageOnly(string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<TraceRecord xmlns=\"http://schemas.microsoft.com/2004/10/E2ETraceEvent/TraceRecord\">").
                Append("<TraceIdentifier>http://code.google.com/p/tools/log.aspx</TraceIdentifier>").
                Append("<Description>").Append(XmlEncode(message)).
                Append("</Description></TraceRecord>");
            return sb.ToString();
        }

        private void AddExceptionToTraceString(XmlWriter xml, Exception exception)
        {
            xml.WriteElementString("ExceptionType", XmlEncode(exception.GetType().AssemblyQualifiedName));
            xml.WriteElementString("Message", XmlEncode(exception.Message));
            xml.WriteElementString("StackTrace", XmlEncode(this.StackTraceString(exception)));
            xml.WriteElementString("ExceptionString", XmlEncode(exception.ToString()));

            Win32Exception exception2 = exception as Win32Exception;

            if (exception2 != null)
            {
                xml.WriteElementString("NativeErrorCode", exception2.NativeErrorCode.ToString("X", CultureInfo.InvariantCulture));
            }
            if ((exception.Data != null) && (exception.Data.Count > 0))
            {
                xml.WriteStartElement("DataItems");
                foreach (object obj2 in exception.Data.Keys)
                {
                    xml.WriteStartElement("Data");
                    xml.WriteElementString("Key", XmlEncode(obj2.ToString()));
                    xml.WriteElementString("Value", XmlEncode(exception.Data[obj2].ToString()));
                    xml.WriteEndElement();
                }
                xml.WriteEndElement();
            }
            if (exception.InnerException != null)
            {
                xml.WriteStartElement("InnerException");
                this.AddExceptionToTraceString(xml, exception.InnerException);
                xml.WriteEndElement();
            }
        }
        private string StackTraceString(Exception exception)
        {
            string stackTrace = exception.StackTrace;
            if (!string.IsNullOrEmpty(stackTrace))
            {
                return stackTrace;
            }
            StackFrame[] frames = new StackTrace(false).GetFrames();
            int skipFrames = 0;
            bool flag = false;
            foreach (StackFrame frame in frames)
            {
                string str3;
                string name = frame.GetMethod().Name;
                if (((str3 = name) != null) && (((str3 == "StackTraceString") || (str3 == "AddExceptionToTraceString")) || (((str3 == "BuildTrace") || (str3 == "TraceEvent")) || (str3 == "TraceException"))))
                {
                    skipFrames++;
                }
                else if (name.StartsWith("ThrowHelper", StringComparison.Ordinal))
                {
                    skipFrames++;
                }
                else
                {
                    flag = true;
                }
                if (flag)
                {
                    break;
                }
            }
            StackTrace trace = new StackTrace(skipFrames, false);
            return trace.ToString();
        }

        public static string XmlEncode(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }
            int length = text.Length;
            StringBuilder builder = new StringBuilder(length + 8);
            for (int i = 0; i < length; i++)
            {
                char ch = text[i];
                switch (ch)
                {
                    case '<':
                        {
                            builder.Append("&lt;");
                            continue;
                        }
                    case '>':
                        {
                            builder.Append("&gt;");
                            continue;
                        }
                    case '&':
                        {
                            builder.Append("&amp;");
                            continue;
                        }
                }
                builder.Append(ch);
            }
            return builder.ToString();
        }

        #endregion
    }
}
