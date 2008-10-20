//#define USEIE
using System.Windows.Forms;

namespace Tools.Tracing.UI
{
    /// <summary>
    /// Summary description for TextTransformerTabPage.
    /// </summary>
    public class TextTransformerTabPage : TabPage
    {
#if !USEIE
        private readonly RichTextBox outTextBox;
        private readonly ITextTransformer textTransformer;
#else
        private WebBrowser outBrowser = null;
#endif


        public TextTransformerTabPage()
        {
            textTransformer = new TransparentTextTransformer();
#if !USEIE
            outTextBox = new RichTextBox();
            outTextBox.Dock = DockStyle.Fill;
            Controls.Add(outTextBox);
#else
            outBrowser = new WebBrowser();
            outBrowser.Dock = DockStyle.Fill;
            Controls.Add(outBrowser);
#endif
        }

        public TextTransformerTabPage(ITextTransformer textTransformer)
            : this()
        {
            this.textTransformer = textTransformer;
        }

        public void SetText(string text)
        {
#if !USEIE
            outTextBox.Text = textTransformer.TransformText(text);
#else
            outBrowser.DocumentText = textTransformer.TransformText(text);
#endif
        }
    }
}