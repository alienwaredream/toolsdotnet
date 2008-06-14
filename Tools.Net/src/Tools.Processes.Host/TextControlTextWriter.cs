using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Tools.Processes.Host
{
    delegate void SetStringDelegate(string text);

    public class TextControlTextWriter : TextWriter
    {
        //TODO: (SD) Find more generic form
        RichTextBox textControl;
        private object syncObject = new object();
        //private int logLength = 0;
        private StringBuilder sb = new StringBuilder();

        public override Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }

        public TextControlTextWriter(RichTextBox textControl)
        {
            this.textControl = textControl;
        }
        private void AddControlText(string value)
        {
            sb.Append(value);
            this.textControl.Text = sb.ToString();
            //this.textControl.Text += value;
            //if (value != null) logLength += value.Length;

            this.textControl.Select(sb.Length - 1, 0);
            //this.textControl.Select(this.textControl.Text.Length - 1, 0);
            this.textControl.ScrollToCaret();

        }

        public override void Write(string value)
        {
            lock (syncObject)
            {
                //textControl.BeginInvoke(new SetStringDelegate(AddControlText), new object[] { value });
                textControl.Invoke(new SetStringDelegate(AddControlText), new object[] { value });
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
                //textControl.BeginInvoke(new SetStringDelegate(AddControlText), new object[] { value + Environment.NewLine });
                textControl.Invoke(new SetStringDelegate(AddControlText), new object[] { value + Environment.NewLine });
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
