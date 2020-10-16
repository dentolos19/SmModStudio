﻿using System.IO;
using System.Windows;
using System.Windows.Controls;
using SmModStudio.Core;
using SmModStudio.Core.Models;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace SmModStudio.Graphics
{

    public partial class WnOpenProject
    {

        public string ModName { get; private set; }
        public string ModPath { get; private set; }

        public WnOpenProject()
        {
            InitializeComponent();
            ProjectLocationBox.SelectedIndex = 1;
        }

        private void Continue(object sender, RoutedEventArgs args)
        {
            var item = (ListBoxItem)ProjectListBox.SelectedItem;
            if (item?.Tag == null)
            {
                AdonisMessageBox.Show(Constants.TxtDialogMsg4, Constants.TxtDialogTitle);
                return;
            }
            ModName = (string)item.Content;
            ModPath = (string)item.Tag;
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
                        return;
                    var name = new DirectoryInfo(path).Name;
                    if (File.Exists(Path.Combine(path, "description.json")))
                        name = ModDescriptionModel.Load(Path.Combine(path, "description.json")).Name;
                    var item = new ListBoxItem { Content = name, Tag = path };
                    ProjectListBox.Items.Add(item);
                }
            }
            
        }

    }

}