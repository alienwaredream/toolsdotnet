using System;

namespace Tools.Tracing.UI
{
	/// <summary>
	/// Summary description for TransparentTextTransformer.
	/// </summary>
	public class TransparentTextTransformer : ITextTransformer
	{
		public TransparentTextTransformer()
		{
		}
		#region ITextTransformer Members

		public string TransformText(string text)
		{
			return text;
		}

		#endregion
	}
}
