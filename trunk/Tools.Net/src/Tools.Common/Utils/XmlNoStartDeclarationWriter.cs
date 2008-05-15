using System;
using System.Xml;
using System.IO;

namespace Tools.Common.Utils
{
	/// <summary>
	/// Only required for 1.x
	/// in 2.0 that is solved as:
	/// XmlWriterSettings settings = new XmlWriterSettings();
	/// settings.Indent = true;
	/// settings.OmitXmlDeclaration = true;
	/// settings.NewLineOnAttributes = true;
	/// writer = XmlWriter.Create(Console.Out, settings);
	/// </summary>
	public class XmlNoStartDeclarationWriter : XmlTextWriter
	{
		
		public XmlNoStartDeclarationWriter(string filePath, System.Text.Encoding encoding)
			: base(filePath, encoding)
		{
		}
		public XmlNoStartDeclarationWriter(Stream stream, System.Text.Encoding encoding)
			: base(stream, encoding)
		{
		}
		public XmlNoStartDeclarationWriter(TextWriter writer)
			: base(writer)
		{
		}
		public override void WriteStartDocument()
		{
			// Only creates new line for higher readability for a moment
			this.BaseStream.Write(new byte[2] {13,10}, 0, 2);
		}

	}
}
