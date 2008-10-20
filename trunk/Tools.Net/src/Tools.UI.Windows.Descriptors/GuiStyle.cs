using System.Drawing;
using Tools.Core;

namespace Tools.UI.Windows.Descriptors
{
    internal class GuiStyle : Descriptor
    {
        private Font _defaultFont =
            new Font
                (
                FontFamily.GenericSansSerif,
                8f,
                FontStyle.Regular
                );

        //private System.Drawing.Font _ont


        public GuiStyle
            (
            string name,
            string description
            )
            : base
                (
                name,
                description
                )
        {
        }
    }
}