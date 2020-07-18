using System.IO;
using System.Windows;
using SmModStudio.Core.Bindings;

namespace SmModStudio.Graphics
{

    public partial class WnOpen
    {

        public WnOpen(string path)
        {
            InitializeComponent();
            var projects = Directory.GetDirectories(path);
            foreach (var project in projects)
                ProjectList.Items.Add(new ProjectItemBinding(project));
        }

        public string SelectedPath { get; private set; }

        private void OpenProject(object sender, RoutedEventArgs args)
        {
            if (ProjectList.SelectedItem == null)
            {
                MessageBox.Show("Please select a project before continuing!", "SmModStudio");
                return;
            }
            var item = (ProjectItemBinding) ProjectList.SelectedItem;
            SelectedPath = item.Path;
            DialogResult = true;
            Close();
        }

        private void CopyPath(object sender, RoutedEventArgs args)
        {
            if (ProjectList.SelectedItem == null)
                return;
            var item = (ProjectItemBinding) ProjectList.SelectedItem;
            Clipboard.SetText(item.Path);
        }

    }

}