using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Tools.Tfs.Compare.Gui
{
    [Serializable()]
    public class Workspace : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<string> queryHistory = new List<string>();

        private string name;
        private string description;
        private string tfsUrl;
        private string changeset;



        public string Name 
        {
            get { return name; }
            set { if (name != value) { name = value; Notify("Name"); } } 
        }
        public string Description
        {
            get { return description; }
            set { if (description != value) { description = value; Notify("Description"); } }
        }

        void Notify(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }


    }
}
