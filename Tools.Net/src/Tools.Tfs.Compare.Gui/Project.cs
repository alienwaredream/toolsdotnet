using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Tfs.Compare.Gui
{
    [Serializable()]
    public class Project
    {
        private List<Workspace> workspaces = new List<Workspace>();

        public List<Workspace> Workspaces { get { return workspaces; } set { workspaces = value; } }
    }
}
