using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Tools.Processes.Host
{
    internal delegate void SetStringDelegate(string text);

    internal delegate void AddListViewItem(ListViewItem listItem);

    public class TextControlTextWriter : TextWriter
    {
        //TODO: (SD) Find more generic form
        private readonly object syncObject = new object();
        private readonly ListView viewControl;
        //private int logLength = 0;
        private Regex descriptionRegex;
        private StringBuilder detail = new StringBuilder();
        private StringBuilder entry = new StringBuilder();

        public TextControlTextWriter(ListView viewControl, string descriptionPattern)
        {
            this.viewControl = viewControl;
            Enabled = true;
            AutoScroll = true;
            descriptionRegex = new Regex(descriptionPattern,
                                         RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
        }

        public bool Enabled { get; set; }
        public bool AutoScroll { get; set; }


        public override Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }

        public void SetDescriptionRegexString(string pattern)
        {
            lock (syncObject)
            {
                descriptionRegex = new Regex(pattern,
                                             RegexOptions.Multiline | RegexOptions.IgnoreCase | RegexOptions.Compiled);
            }
        }

        /// <summary>
        /// Writes text to the listview item in the gui.
        /// </summary>
        /// <param name="text">Text to write.</param>
        /// <remarks>
        /// Accumulates writes until empty entry is submitted. This relies onto indent being on.
        /// </remarks>
        private void AddControlText(string text)
        {
            if (!Enabled) return;

            if (String.IsNullOrEmpty(text)) return;


            if (String.IsNullOrEmpty(text.Trim()))
            {
                WrapLogMessage();
            }
            else
            {
                Match match = descriptionRegex.Match(text);

                if (match != null && match.Groups.Count > 1)
                {
                    entry.Append(match.Groups[1].Value);
                    detail.Append(text);
                    WrapLogMessage();
                }
                else
                {
                    entry.Append(text);
                    detail.Append(text);
                }
            }
        }

        private void WrapLogMessage()
        {
            var item = new ListViewItem {Tag = detail.ToString(), Text = entry.ToString()};
            viewControl.SuspendLayout();
            viewControl.Items.Add(item);

            if (AutoScroll)
            {
                viewControl.EnsureVisible(viewControl.Items.Count - 1);
            }

            viewControl.ResumeLayout();

            entry = new StringBuilder();
            detail = new StringBuilder();
        }

        public override void Write(string value)
        {
            lock (syncObject)
            {
                try
                {
                    viewControl.BeginInvoke(new SetStringDelegate(AddControlText), new object[] { value });
                }
                catch { }
                //viewControl.Invoke(new SetStringDelegate(AddControlText), value);
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
                viewControl.BeginInvoke(new SetStringDelegate(AddControlText), new object[] { value });
                //viewControl.Invoke(new SetStringDelegate(AddControlText), value);
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
                Write(value + Environment.NewLine);
            }
        }

        public override void WriteLine(string format, object arg0)
        {
            Write(String.Format(format + Environment.NewLine, arg0));
        }
    }
}