using System;
using System.IO;
using System.Text;
//
namespace Tools.Common.Logging
{
	public class HttpLoggerFilter : HttpFilter
	{
		private StringBuilder _rawContentSB = null;
		private Encoding _logEncoding = Encoding.UTF8;

		public string RawContent
		{
			get
			{
				return _rawContentSB.ToString();
			}
		}
		public Encoding LogEncoding
		{
			get
			{
				return _logEncoding;
			}
			set
			{
				_logEncoding = value;
			}
		}

		public HttpLoggerFilter(Stream baseStream) : base(baseStream) 
		{
			_rawContentSB = new StringBuilder();
		}

		public override void Write(byte[] buffer, int offset, int count) 
		{
			BaseStream.Write(buffer, offset, count);
			_rawContentSB.Append(LogEncoding.GetChars(buffer));
		}
	}
}
