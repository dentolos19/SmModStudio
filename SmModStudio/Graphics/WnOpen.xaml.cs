using System.IO;
using System.Windows;
using SmModStudio.Bindings;

namespace SmModStudio.Graphics
{

    public partial class WnOpen
    {

        public string SelectedPath { get; private set; }

        public WnOpen(string path)
        {
            InitializeComponent();
            var projects = Directory.GetDirectories(path);
            foreach (var project in projects)
                ProjectList.Items.Add(new ProjectListItem(project));
        }

        private void OpenProject(object sender, RoutedEventArgs args)
        {
            if (ProjectList.SelectedItem == null)
            {
                MessageBox.Show("Please select a project before continuing!");
                return;
            }
            var item = (ProjectListItem) ProjectList.SelectedItem;
            SelectedPath = item.Path;
            DialogResult = true;
            Close();
        }

        private void CopyPath(object sender, RoutedEventArgs args)
        {
            if (ProjectList.SelectedItem == null)
                return;
            var item = (ProjectListItem) ProjectList.SelectedItem;
            Clipboard.SetText(item.Path);
        }

    }
    
}