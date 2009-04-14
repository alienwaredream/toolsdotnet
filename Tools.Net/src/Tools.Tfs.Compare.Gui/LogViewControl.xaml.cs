using System;
using System.Windows;
using System.Windows.Controls;

namespace Tools.Tfs.Compare.Gui
{
    /// <summary>
    /// Interaction logic for LogViewControl.xaml
    /// </summary>
    public partial class LogViewControl : UserControl, ILogger
    {

        LogItemCollection logItems = new LogItemCollection();

        public LogViewControl()
        {
            InitializeComponent();

            logListView.DataContext = logItems;

        }
        public void Log(string category, string text)
        {
            logItems.Add(new LogItem { Time = DateTime.UtcNow, Category = category, Text = text });
            logListView.SelectedIndex = logItems.Count - 1;
            logListView.ScrollIntoView(logListView.SelectedItem);
        }

        private void cleanTheLog_Click(object sender, RoutedEventArgs e)
        {
            logItems.Clear();
        }

        #region ILogger Members

        public void Log(LogItem logItem)
        {
            Log(logItem.Category, logItem.Text);
        }

        #endregion
    }
}
