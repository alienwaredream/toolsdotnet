using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Tfs.Compare.Gui
{
    public struct LogItem
    {
        public DateTime Time { get; set; }
        public string Category { get; set; }
        public string Text { get; set; }


    }
}
