using System.IO;
using System.Windows;
using System.Windows.Controls;
using SmModStudio.Core;
using SmModStudio.Core.Bindings;
using SmModStudio.Core.Models;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace SmModStudio.Graphics
{

    public partial class WnOpenProject
    {

        public WnOpenProject()
        {
            InitializeComponent();
            ProjectLocationBox.SelectedIndex = 1;
        }

        public string ModName { get; private set; }
        public string ModPath { get; private set; }

        private void Continue(object sender, RoutedEventArgs args)
        {
            if (!(ProjectListBox.SelectedItem is ProjectItemBinding item))
            {
                AdonisMessageBox.Show(Constants.TxtDialogMsg4, Constants.TxtDialogTitle);
                return;
            }
            ModName = item.Name;
            ModPath = item.Path;
            DialogResult = true;
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs args)
        {
            Close();
        }

        private void UpdateLocationSelection(object sender, SelectionChangedEventArgs args)
        {
            var tag = (string)((ComboBoxItem)ProjectLocationBox.SelectedItem).Tag;
            var paths = tag switch
            {
                "Local" => Directory.GetDirectories(Path.Combine(App.Settings.UserDataPath, "Mods")),
                "Workshop" => Directory.GetDirectories(Path.Combine(App.Settings.WorkshopPath)),
                _ => null
            };
            if (paths == null)
                return;
            ProjectListBox.Items.Clear();
            if (paths.Length <= 0)
            {
                var item = new ListBoxItem { Content = Constants.TxtOtherMsg0 };
                ProjectListBox.Items.Add(item);
            }
            else
            {
                foreach (var path in paths)
                {
                    if (!File.Exists(Path.Combine(path, "description.json")))
                        continue;
                    var description = ModDescriptionModel.Load(Path.Combine(path, "description.json"));
                    ProjectListBox.Items.Add(new ProjectItemBinding
                    {
                        Name = description.Name,
                        Path = path
                    });
                }
            }
        }

    }

}