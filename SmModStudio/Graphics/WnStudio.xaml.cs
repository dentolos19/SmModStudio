using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SmModStudio.Core;
using SmModStudio.Core.Bindings;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;
using AdonisMessageBoxResult = AdonisUI.Controls.MessageBoxResult;
using AdonisMessageBoxButton = AdonisUI.Controls.MessageBoxButton;

namespace SmModStudio.Graphics
{

    public partial class WnStudio
    {

        private readonly ObservableCollection<ViewTabBinding> _tabs;

        private string _modName;
        private string _modPath;
        private FileSystemWatcher _hierarchyUpdater;

        #region Methods

        public WnStudio()
        {
            InitializeComponent();
            _tabs = new ObservableCollection<ViewTabBinding>();
            Views.ItemsSource = _tabs;
        }

        private void UpdateHierarchy(string modName = null, string modPath = null)
        {
            if (!string.IsNullOrEmpty(modName))
                _modName = modName;
            if (!string.IsNullOrEmpty(modPath))
                _modPath = modPath;
            Dispatcher.Invoke(() =>
            {
                Hierarchy.DataContext = new[]
                {
                    new HierarchyDirectoryBinding
                    {
                        Icon = Constants.ModFolderIcon,
                        Name = _modName,
                        Path = _modPath,
                        Items = Utilities.GenerateHierarchyItems(_modPath)
                    }
                };
            });
        }

        #endregion

        #region Events

        private void Open(object sender, ExecutedRoutedEventArgs args)
        {
            var dialog = new WnOpenProject { Owner = this };
            if (dialog.ShowDialog() != true)
                return;
            _hierarchyUpdater = new FileSystemWatcher(dialog.ModPath);
            _hierarchyUpdater.Created += (o, a) => { UpdateHierarchy(); };
            _hierarchyUpdater.Deleted += (o, a) => { UpdateHierarchy(); };
            _hierarchyUpdater.Renamed += (o, a) => { UpdateHierarchy(); };
            _hierarchyUpdater.EnableRaisingEvents = true;
            UpdateHierarchy(dialog.ModName, dialog.ModPath);
        }

        private void SaveAll(object sender, ExecutedRoutedEventArgs args)
        {
            foreach (var tab in _tabs)
            {
                if (!(tab.Content is PgCodeEditor editor))
                    continue;
                editor.Save(null, null);
            }
        }

        private void Exit(object sender, RoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }

        private void ShowPreferences(object sender, RoutedEventArgs args)
        {
            new WnPreferences { Owner = this }.ShowDialog();
        }

        private void NewHierarchyFile(object sender, ExecutedRoutedEventArgs args)
        {
            var item = (HierarchyItemBinding)Hierarchy.SelectedItem;
            if (item == null)
                return;
            if (!Utilities.IsPathDirectory(item.Path))
                return;
            new WnNewFile(item.Path) { Owner = this }.ShowDialog();
        }

        private void OpenHierarchyFile(object sender, MouseButtonEventArgs args)
        {
            var item = (HierarchyItemBinding)Hierarchy.SelectedItem;
            if (item == null)
                return;
            if (Utilities.IsPathDirectory(item.Path))
                return;
            Page page = null;
            if (Utilities.IsFileEditable(item.Path))
            {
                page = new PgCodeEditor(item.Path);
            }
            else if (Utilities.IsImagePreviewable(item.Path))
            {
                page = new PgImagePreviewer(item.Path);
            }
            if (page == null)
                return;
            var binding = new ViewTabBinding { Header = Path.GetFileName(item.Path), Content = page, Path = item.Path };
            _tabs.Add(binding);
            Views.SelectedIndex = _tabs.IndexOf(binding);
        }

        private void DeleteHierarchyFile(object sender, ExecutedRoutedEventArgs args)
        {
            var item = (HierarchyItemBinding)Hierarchy.SelectedItem;
            if (item == null)
                return;
            if (AdonisMessageBox.Show("Are you sure that you want to delete this file/folder?", "SmModStudio", AdonisMessageBoxButton.YesNo) != AdonisMessageBoxResult.Yes)
                return;
            if (Utilities.IsPathDirectory(item.Path))
            {
                Directory.Delete(item.Path, true);
            }
            else
            {
                File.Delete(item.Path);
            }
        }

        private void CanExecuteNew(object sender, CanExecuteRoutedEventArgs args)
        {
            var item = (HierarchyItemBinding)Hierarchy.SelectedItem;
            if (item == null)
                return;
            if (Utilities.IsPathDirectory(item.Path))
                args.CanExecute = true;
        }

        private void CanExecuteDelete(object sender, CanExecuteRoutedEventArgs args)
        {
            var item = (HierarchyItemBinding)Hierarchy.SelectedItem;
            if (item == null)
                return;
            if (item.Path == _modPath)
                return;
            args.CanExecute = true;
        }

        private void CopyFullPath(object sender, RoutedEventArgs args)
        {
            var item = (HierarchyItemBinding)Hierarchy.SelectedItem;
            if (item == null)
                return;
            Clipboard.SetText(item.Path);
        }

        private void ViewInFileExplorer(object sender, RoutedEventArgs args)
        {
            var item = (HierarchyItemBinding)Hierarchy.SelectedItem;
            if (item == null)
                return;
            Process.Start("explorer.exe", Utilities.IsPathDirectory(item.Path) ? item.Path : $"/select,\"{item.Path}\"");
        }

        private void CloseViewTab(object sender, MouseButtonEventArgs args)
        {
            _tabs.RemoveAt(Views.SelectedIndex);
        }

        #endregion

        

    }

}