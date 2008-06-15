using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Tools.Processes.Host
{
    delegate void SetStringDelegate(string text);
    delegate void AddListViewItem(ListViewItem listItem);

    public class TextControlTextWriter : TextWriter
    {
        //TODO: (SD) Find more generic form
        ListView viewControl;
        private object syncObject = new object();
        //private int logLength = 0;
        private StringBuilder sb = new StringBuilder();
        private Regex descriptionRegex;
        

        public void SetDescriptionRegexString(string pattern)
        {
            lock (syncObject)
            {
                descriptionRegex = new Regex(pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
        }

        public override Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }

        public TextControlTextWriter(ListView viewControl, string descriptionPattern)
        {
            this.viewControl = viewControl;
            descriptionRegex = new Regex(descriptionPattern, RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }
        private void AddControlText(string text)
        {
            if (String.IsNullOrEmpty(text)) return;

            Match match = descriptionRegex.Match(text);

            string description = text;

            if (match != null && match.Groups.Count > 1)
            {
                description = match.Groups[1].Value;
            }

            ListViewItem item = new ListViewItem(description);
            item.Tag = text;
            viewControl.Items.Add(item);
            viewControl.EnsureVisible(viewControl.Items.Count - 1);

        }

        public override void Write(string value)
        {
            lock (syncObject)
            {
                //textControl.BeginInvoke(new SetStringDelegate(AddControlText), new object[] { value });
                viewControl.Invoke(new SetStringDelegate(this.AddControlText), value);
            }
        }
        public override void Write(string format, params object[] arg)
        {
            Write(String.Format(format, arg));
        }
        public override void Write(object value)
        {
            if (value != null)
            {
                Write(value.ToString());
            }
        }
        public override void Write(string format, object arg0)
        {
            Write(String.Format(format, arg0));
        }
        public override void WriteLine(string value)
        {
            lock (syncObject)
            {
                viewControl.Invoke(new SetStringDelegate(this.AddControlText), value);
            }
        }
        public override void WriteLine(string format, params object[] arg)
        {
            Write(String.Format(format + Environment.NewLine, arg));
        }
        public override void WriteLine(object value)
        {
            if (value != null)
            {
                Write(value.ToString() + Environment.NewLine);
            }
        }
        public override void WriteLine(string format, object arg0)
        {
            Write(String.Format(format + Environment.NewLine, arg0));

        }
    }
}
