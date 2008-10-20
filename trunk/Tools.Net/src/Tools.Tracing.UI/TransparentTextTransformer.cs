namespace Tools.Tracing.UI
{
    /// <summary>
    /// Summary description for TransparentTextTransformer.
    /// </summary>
    public class TransparentTextTransformer : ITextTransformer
    {
        #region ITextTransformer Members

        public string TransformText(string text)
        {
            return text;
        }

        #endregion
    }
}