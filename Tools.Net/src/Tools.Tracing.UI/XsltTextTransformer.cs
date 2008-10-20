using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

namespace Tools.Tracing.UI
{
    /// <summary>
    /// Summary description for XsltTextTransformer.
    /// </summary>
    public class XsltTextTransformer : ITextTransformer
    {
        private readonly string xsltFilePath;
        private XslCompiledTransform xsltTransformer;

        public XsltTextTransformer(string xsltFilePath)
        {
            this.xsltFilePath = xsltFilePath;
        }

        #region ITextTransformer Members

        public string TransformText(string text)
        {
            if (xsltTransformer == null)
            {
                xsltTransformer = new XslCompiledTransform(false);
                xsltTransformer.Load
                    (
                    xsltFilePath
                    );
                //xsltTransformer.
            }
            var stringReader = new StringReader(text);

            XmlReader xmlReader =
                XmlReader.Create
                    (
                    stringReader
                    );

            var sb = new StringBuilder();

            XmlWriter xmlWriter =
                XmlWriter.Create
                    (
                    sb
                    );
            xsltTransformer.Transform
                (
                xmlReader,
                new XsltArgumentList(),
                xmlWriter,
                new XmlUrlResolver()
                );
            return sb.ToString();
        }

        #endregion
    }
}