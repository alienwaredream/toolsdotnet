using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Xml.Linq;
using Microsoft.Win32;
using Tools.Tfs.Compare.Gui;

namespace Tools.Tfs.Compare.Gui
{
    /// <summary>
    /// Interaction logic for WorkspaceControl.xaml
    /// </summary>
    public partial class WorkspaceControl : UserControl
    {

        public ILogger Logger { get; set; }
        Workspace workspace;

        List<XElement> items = new List<XElement>();

        public WorkspaceControl(Workspace workspace)
        {
            this.workspace = workspace;

            InitializeComponent();
        }

        private void Log(string category, string text)
        {
            CheckForLogger(Logger);
            Logger.Log(new LogItem { Text = text, Category = category });
        }


        private void CheckForLogger(ILogger logger)
        {
            Debug.Assert(logger != null, "failed: logger != null, logger should be set!");
        }

        private void runTheCode_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
