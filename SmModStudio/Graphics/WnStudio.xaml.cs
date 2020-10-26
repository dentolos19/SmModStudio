using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
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

        private readonly int _originalHierarchyWidth;
        private readonly int _originalSplitterWidth;
        private readonly ObservableCollection<ViewTabBinding> _tabs;

        private string _modName;
        private string _modPath;
        private FileSystemWatcher _hierarchyUpdater;

        public WnStudio()
        {
            InitializeComponent();
            _tabs = new ObservableCollection<ViewTabBinding>();
            Views.ItemsSource = _tabs;
            ShowHierarchyCheckBox.IsChecked = true;
            _originalHierarchyWidth = (int)HierarchyColumn.MinWidth;
            _originalSplitterWidth = int.Parse(new GridLengthConverter().ConvertToString(SplitterColumn.Width)!);
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            VersionText.Text = $"v{version!.Major}.{version.Minor}.{version.Build}";
            #if DEBUG
            VersionText.Text += "-DEBUG";
            #endif
        }

        private void Open(object sender, ExecutedRoutedEventArgs args)
        {
            var dialog = new WnOpenProject { Owner = this };
            if (dialog.ShowDialog() != true)
                return;
            SaveAll(null, null);
            _tabs.Clear();
            _hierarchyUpdater = new FileSystemWatcher(dialog.ModPath);
            _hierarchyUpdater.Created += (s, a) => { UpdateHierarchy(); };
            _hierarchyUpdater.Changed += (s, a) => { UpdateHierarchy(); };
            _hierarchyUpdater.Deleted += (s, a) => { UpdateHierarchy(); };
            _hierarchyUpdater.Renamed += (s, a) => { UpdateHierarchy(); };
            _hierarchyUpdater.EnableRaisingEvents = true;
            UpdateHierarchy(dialog.ModName, dialog.ModPath);
        }

        private void SaveAll(object sender, ExecutedRoutedEventArgs args)
        {
            for (var index = 0; index < _tabs.Count; index++)
                SaveEditorTabIndex(index);
        }

        private void Exit(object sender, RoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }

        private void ShowPreferences(object sender, RoutedEventArgs args)
        {
            new WnPreferences { Owner = this }.ShowDialog();
        }

        private void CloseViewTab(object sender, MouseButtonEventArgs args)
        {
            var tab = _tabs[Views.SelectedIndex];
            if (App.Settings.AutoSaveClosingFile)
                SaveEditorTabIndex(Views.SelectedIndex);
            _tabs.Remove(tab);
        }

        private void OpenHierarchyFile(object sender, MouseButtonEventArgs args)
        {
            var item = (HierarchyItemBinding)Hierarchy.SelectedItem;
            if (item == null)
                return;
            if (Utilities.IsPathDirectory(item.Path))
                return;
            foreach (var tab in _tabs)
            {
                if (tab.Path != item.Path)
                    continue;
                Views.SelectedIndex = _tabs.IndexOf(tab);
                return;
            }
            Page page = null;
            if (Utilities.IsFileEditable(item.Path))
            {
                if (item.Path == Path.Combine(_modPath, "description.json"))
                {
                    page = new PgDescriptionEditor(item.Path);
                }
                else if (Path.GetFileName(item.Path) == "inventoryDescriptions.json")
                {
                    page = new PgLanguageEditor(item.Path);
                }
                else
                {
                    page = new PgCodeEditor(item.Path);
                }
            }
            else if (Utilities.IsImagePreviewable(item.Path))
            {
                page = new PgImagePreviewer(item.Path);
            }
            if (page == null)
                return;
            var binding = new ViewTabBinding { Content = page, Path = item.Path };
            _tabs.Add(binding);
            Views.SelectedIndex = _tabs.IndexOf(binding);
        }

        private void DeleteHierarchyFile(object sender, ExecutedRoutedEventArgs args)
        {
            var item = (HierarchyItemBinding)Hierarchy.SelectedItem;
            if (item == null)
                return;
            if (AdonisMessageBox.Show(Constants.TxtDialogMsg8, Constants.TxtDialogTitle, AdonisMessageBoxButton.YesNo) != AdonisMessageBoxResult.Yes)
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

        private void CreateFolder(object sender, RoutedEventArgs args)
        {
            var item = (HierarchyItemBinding)Hierarchy.SelectedItem;
            if (item == null)
                return;
            if (!Utilities.IsPathDirectory(item.Path))
                return;
            new WnNewFolder(item.Path) { Owner = this }.ShowDialog();
        }

        private void CreateFile(object sender, ExecutedRoutedEventArgs args)
        {
            var item = (HierarchyItemBinding)Hierarchy.SelectedItem;
            if (item == null)
                return;
            if (!Utilities.IsPathDirectory(item.Path))
                return;
            new WnNewFile(item.Path) { Owner = this }.ShowDialog();
        }

        private void CanExecuteNew(object sender, CanExecuteRoutedEventArgs args)
        {
            var item = (HierarchyItemBinding)Hierarchy.SelectedItem;
            if (item == null)
                return;
            if (Utilities.IsPathDirectory(item.Path))
                args.CanExecute = true;
        }

        private void CanExecuteSaveAll(object sender, CanExecuteRoutedEventArgs args)
        {
            if (_tabs.Count >= 1)
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

        private void LaunchGame(object sender, RoutedEventArgs args)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "steam://run/387990",
                UseShellExecute = true
            });
        }

        private void VerifyGameFiles(object sender, RoutedEventArgs args)
        {
            if (AdonisMessageBox.Show(Constants.TxtDialogMsg9, Constants.TxtDialogTitle, AdonisMessageBoxButton.YesNo) != AdonisMessageBoxResult.Yes)
                return;
            Process.Start(new ProcessStartInfo
            {
                FileName = "steam://validate/387990",
                UseShellExecute = true
            });
        }

        private void LaunchModTool(object sender, RoutedEventArgs args)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "steam://run/588870",
                UseShellExecute = true
            });
        }

        private void LaunchBackupWizard(object sender, RoutedEventArgs args)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "steam://backup/387990",
                UseShellExecute = true
            });
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
                        Icon = Constants.ImgModFolder,
                        Name = _modName,
                        Path = _modPath,
                        Items = Utilities.GenerateHierarchyItems(_modPath)
                    }
                };
            });
        }

        private void SaveEditorTabIndex(int index)
        {
            switch (_tabs[index].Content)
            {
                case PgCodeEditor codeEditor:
                    codeEditor.Save(null, null);
                    break;
                case PgDescriptionEditor descriptionEditor:
                    descriptionEditor.Save(null, null);
                    break;
                case PgLanguageEditor languageEditor:
                    languageEditor.Save(null, null);
                    break;
            }
        }

        private void ToggleHierarchyView(object sender, RoutedEventArgs args)
        {
            if (ShowHierarchyCheckBox.IsChecked)
            {
                HierarchyColumn.MinWidth = _originalHierarchyWidth;
                HierarchyColumn.Width = (GridLength)new GridLengthConverter().ConvertFrom("Auto")!;
                SplitterColumn.Width = (GridLength)new GridLengthConverter().ConvertFrom(_originalSplitterWidth)!;
            }
            else
            {
                HierarchyColumn.MinWidth = 0;
                HierarchyColumn.Width = (GridLength) new GridLengthConverter().ConvertFrom(0)!;
                SplitterColumn.Width = (GridLength)new GridLengthConverter().ConvertFrom(0)!;
            }
        }

    }

}