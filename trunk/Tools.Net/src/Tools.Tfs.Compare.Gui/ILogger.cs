using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Tfs.Compare.Gui
{
    public interface ILogger
    {
        void Log(LogItem logItem);
    }
}
