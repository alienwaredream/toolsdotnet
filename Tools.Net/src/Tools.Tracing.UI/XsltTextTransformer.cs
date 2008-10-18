using System;
using System.Xml;
using System.Xml.Xsl;
using System.Text;

namespace Tools.Tracing.UI
{
	/// <summary>
	/// Summary description for XsltTextTransformer.
	/// </summary>
	public class XsltTextTransformer : ITextTransformer
	{
		private string xsltFilePath = null;
		private System.Xml.Xsl.XslCompiledTransform xsltTransformer = null;

		public XsltTextTransformer(string xsltFilePath)
		{
			this.xsltFilePath = xsltFilePath;
		}
		#region ITextTransformer Members

		public string TransformText(string text)
		{
			if (xsltTransformer==null)
			{
                xsltTransformer = new XslCompiledTransform(false);
				xsltTransformer.Load
					(
					xsltFilePath
					);
                //xsltTransformer.
			}
			System.IO.StringReader stringReader = new System.IO.StringReader(text);

            System.Xml.XmlReader xmlReader =
                XmlReader.Create
                (
                stringReader
                );

			StringBuilder sb = new StringBuilder();

            System.Xml.XmlWriter xmlWriter =
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
