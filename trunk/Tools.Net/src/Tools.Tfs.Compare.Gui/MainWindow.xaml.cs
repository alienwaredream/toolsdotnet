using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Xml;
using System.Xml.Serialization;
using System.Globalization;
using Tools.Tfs.Compare.Gui.Properties;

namespace Tools.Tfs.Compare.Gui
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        Project project;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            try
            {

                InitializeProject();

            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format(CultureInfo.InvariantCulture,
                    "Unable to load the project from the path {0}. New project is created. Exception detail: {1}",
                    Settings.Default.Project, ex.ToString()));
            }
        }
        private void InitializeProject()
        {
            if (!String.IsNullOrEmpty(Settings.Default.Project))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Project));
                using (XmlReader reader = XmlReader.Create(Settings.Default.Project))
                {
                    project = serializer.Deserialize(reader) as Project;
                }
            }
            else
            {
                CreateNewProject();
            }
        }

        private void CreateNewProject()
        {
            project = new Project();
            Workspace workspace = new Workspace { Name = "Workspace" };
            project.Workspaces.Add(workspace);
            LoadWorkspacesToUI();
            Log("Project", "New project created");
        }

        private void Log(string category, string text)
        {
            logControl.Log(category, text);
        }

        private void saveProjectButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Xml files (*.xml)|*.xml;|All files (*.*)|*.*";

            if (fileDialog.ShowDialog(this).Value)
            {
                using (XmlWriter writer = XmlWriter.Create(fileDialog.FileName))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Project));
                    serializer.Serialize(writer, project);
                }

                Settings.Default.Project = fileDialog.FileName;
                Settings.Default.Save();
            }
        }

        private void WorkspaceControl_Initialized(object sender, EventArgs e)
        {
            (sender as WorkspaceControl).Logger = logControl;
        }

        private void WorkspaceControl_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as WorkspaceControl).Logger = logControl;
        }

        private void itemsTabControl_Loaded(object sender, RoutedEventArgs e)
        {
            LoadWorkspacesToUI();
        }

        private void LoadWorkspacesToUI()
        {
            itemsTabControl.Items.Clear();

            for (int i = 0; i < project.Workspaces.Count; i++)
            {
                WorkspaceControl workspaceControl = new WorkspaceControl(project.Workspaces[i]);
                workspaceControl.DataContext = project.Workspaces[i];

                workspaceControl.Logger = logControl;

                WorkspaceTabItem item = new WorkspaceTabItem();

                item.DataContext = project.Workspaces[i];

                item.Content = workspaceControl;


                itemsTabControl.Items.Add(item);
            }
        }

        private void openProjectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.CheckFileExists = true;
                bool? fileSelected = openFileDialog.ShowDialog(this.Parent as Window);

                if (fileSelected.Value)
                {
                    string contents = null;
                    //MessageBox.Show(openFileDialog.FileName);
                    XmlSerializer serializer = new XmlSerializer(typeof(Project));

                    using (XmlReader reader = XmlReader.Create(Settings.Default.Project))
                    {
                        project = serializer.Deserialize(reader) as Project;
                    }
                    LoadWorkspacesToUI();
                }

            }
            catch (Exception ex)
            {
                Log("Exception", ex.ToString());
            }
        }

        private void newProjectMenuItem_Click(object sender, RoutedEventArgs e)
        {
            CreateNewProject();
        }
    }

    public class LogItemCollection : ObservableCollection<LogItem> { }
}
