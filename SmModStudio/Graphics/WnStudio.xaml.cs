using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using Ookii.Dialogs.Wpf;
using SmModStudio.Core;

namespace SmModStudio.Graphics
{

    public partial class WnStudio
    {

        private string _selectedPath;
        private string _modDescPath => Path.Combine(_selectedPath, "description.json");

        public WnStudio()
        {
            InitializeComponent();
        }

        private void SetHierarchy(string path)
        {
            ProjectListing.DataContext = new[] { new DirectoryInfo(path) };
        }

        private bool OpenAnyFile(string path)
        {
            if (path == _modDescPath)
            {
                ShowProjectProperties(null, null);
                return true;
            }
            if (Utilities.IsFileAn3DObject(path))
            {
                PageView.Navigate(App.PageObjectPreviewer);
                App.PageObjectPreviewer.SetPreview(path);
                return true;
            }
            if (Utilities.IsFileEditable(path))
            {
                PageView.Navigate(App.PageEditor);
                App.PageEditor.EditFile(path);
                return true;
            }
            if (Utilities.IsFileAnImage(path))
            {
                PageView.Navigate(App.PageImagePreviewer);
                App.PageImagePreviewer.SetPreview(path);
                return true;
            }
            return false;
        }

        private void StudioLoaded(object sender, RoutedEventArgs args)
        {
            if (string.IsNullOrEmpty(Constants.GameUserPath))
            {
                MessageBox.Show("Scrap Mechanic is not installed or haven't initialized, this program will shut down.");
                Application.Current.Shutdown();
            }
            if (string.IsNullOrEmpty(App.Settings.GameDataPath))
            {
                new WnIntro { Owner = this }.ShowDialog();
                if (string.IsNullOrEmpty(App.Settings.GameDataPath))
                    Application.Current.Shutdown();
            }
        }

        private void OpenProject(object sender, RoutedEventArgs args)
        {
            var dialog = new WnOpen(Path.Combine(Constants.GameUserPath, "Mods")) { Owner = this };
            if (dialog.ShowDialog() != true)
                return;
            _selectedPath = dialog.SelectedPath;
            SetHierarchy(_selectedPath);
            ProjectMenu.IsEnabled = true;
        }

        private void OpenFile(object sender, RoutedEventArgs args)
        {
            var dialog = new OpenFileDialog { Filter = "Lua Source File|*.lua|JSON Source File|*.json|All Files|*.*" };
            if (dialog.ShowDialog() == true)
                OpenAnyFile(dialog.FileName);
        }

        private void SaveFile(object sender, RoutedEventArgs args)
        {
            if (!PageView.Content.Equals(App.PageEditor))
                return;
            if (string.IsNullOrEmpty(App.PageEditor.CurrentFile))
            {
                SaveFileAs(null, null);
                return;
            }
            App.PageEditor.Save();
        }

        private void SaveFileAs(object sender, RoutedEventArgs args)
        {
            if (!PageView.Content.Equals(App.PageEditor))
                return;
            var dialog = new SaveFileDialog { Filter = "Lua Source File|*.lua|JSON Source File|*.json|All Files|*.*" };
            if (dialog.ShowDialog() == true)
                App.PageEditor.Save(dialog.FileName);
        }

        private void ExitStudio(object sender, RoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }

        private void UndoText(object sender, RoutedEventArgs args)
        {
            if (App.PageEditor.CodeEditor.CanUndo && EditMenu.IsEnabled)
                App.PageEditor.CodeEditor.Undo();
        }

        private void RedoText(object sender, RoutedEventArgs args)
        {
            if (App.PageEditor.CodeEditor.CanRedo && EditMenu.IsEnabled)
                App.PageEditor.CodeEditor.Redo();
        }

        private void CutText(object sender, RoutedEventArgs args)
        {
            if (EditMenu.IsEnabled)
                App.PageEditor.CodeEditor.Cut();
        }

        private void CopyText(object sender, RoutedEventArgs args)
        {
            if (EditMenu.IsEnabled)
                App.PageEditor.CodeEditor.Copy();
        }

        private void PasteText(object sender, RoutedEventArgs args)
        {
            if (EditMenu.IsEnabled)
                App.PageEditor.CodeEditor.Paste();
        }

        private void DeleteText(object sender, RoutedEventArgs args)
        {
            if (EditMenu.IsEnabled)
                App.PageEditor.CodeEditor.Delete();
        }

        private void SelectAllText(object sender, RoutedEventArgs args)
        {
            if (EditMenu.IsEnabled)
                App.PageEditor.CodeEditor.SelectAll();
        }

        private void OpenFileInListing(object sender, MouseButtonEventArgs args)
        {
            if (!(ProjectListing.SelectedItem is FileSystemInfo info))
                return;
            if (info.Attributes.HasFlag(FileAttributes.Directory) || info.Attributes.HasFlag(FileAttributes.Hidden))
                return;
            if (!OpenAnyFile(info.FullName))
                MessageBox.Show("This file is not viewable by any previewers or editors!", "SmModStudio");
        }

        private void ContextOpenedInListing(object sender, RoutedEventArgs args)
        {
            ContextNewFile.IsEnabled = false;
            ContextNewDirectory.IsEnabled = false;
            ContextImportDirectory.IsEnabled = false;
            ContextImportFile.IsEnabled = false;
            ContextExportFile.IsEnabled = false;
            ContextCopyPath.IsEnabled = false;
            ContextDelete.IsEnabled = false;
            ContextRename.IsEnabled = false;
            if (!(ProjectListing.SelectedItem is FileSystemInfo info))
                return;
            ContextCopyPath.IsEnabled = true;
            ContextDelete.IsEnabled = true;
            ContextRename.IsEnabled = true;
            if (info.Attributes.HasFlag(FileAttributes.Directory))
            {
                ContextNewFile.IsEnabled = true;
                ContextNewDirectory.IsEnabled = true;
                ContextImportDirectory.IsEnabled = true;
                ContextImportFile.IsEnabled = true;
            }
            else
            {
                ContextExportFile.IsEnabled = true;
            }
            if (info.FullName == _modDescPath || info.FullName == _selectedPath)
            {
                ContextRename.IsEnabled = false;
                ContextDelete.IsEnabled = false;
            }
        }

        private async void NewFile(object sender, RoutedEventArgs args)
        {
            if (ContextNewFile.IsEnabled == false)
                return;
            if (!(ProjectListing.SelectedItem is FileSystemInfo info))
                return;
            var input = await this.ShowInputAsync("Creating a new file", "Enter the name for the new file.", new MetroDialogSettings
            {
                AffirmativeButtonText = "Create",
                DefaultText = "source.lua"
            });
            if (string.IsNullOrEmpty(input))
                return;
            await File.WriteAllTextAsync(Path.Combine(info.FullName, input), "Hello world! Write your code here.");
            SetHierarchy(_selectedPath);
        }

        private async void NewDirectory(object sender, RoutedEventArgs args)
        {
            if (ContextNewFile.IsEnabled == false)
                return;
            if (!(ProjectListing.SelectedItem is FileSystemInfo info))
                return;
            var input = await this.ShowInputAsync("Creating a new directory", "Enter the name for the new directory.", new MetroDialogSettings
            {
                AffirmativeButtonText = "Create",
                DefaultText = "Sources"
            });
            if (string.IsNullOrEmpty(input))
                return;
            Directory.CreateDirectory(Path.Combine(info.FullName, input));
            SetHierarchy(_selectedPath);
        }

        private void ImportDirectory(object sender, RoutedEventArgs args)
        {
            if (ContextImportDirectory.IsEnabled == false)
                return;
            if (!(ProjectListing.SelectedItem is FileSystemInfo info))
                return;
            if (info.Attributes.HasFlag(FileAttributes.Directory))
                return;
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
            {
                Utilities.CopyDirectory(dialog.SelectedPath, Path.Combine(info.FullName, new DirectoryInfo(dialog.SelectedPath).Name));
                SetHierarchy(_selectedPath);
            }
        }

        private void ImportFile(object sender, RoutedEventArgs args)
        {
            if (ContextImportFile.IsEnabled == false)
                return;
            if (!(ProjectListing.SelectedItem is FileSystemInfo info))
                return;
            if (info.Attributes.HasFlag(FileAttributes.Directory))
                return;
            var dialog = new OpenFileDialog { Filter = "All Files|*.*"};
            if (dialog.ShowDialog() == true)
            {
                File.Copy(dialog.FileName, Path.Combine(info.FullName, Path.GetFileName(dialog.FileName)));
                SetHierarchy(_selectedPath);
            }
        }

        private void ExportFile(object sender, RoutedEventArgs args)
        {
            if (ContextExportFile.IsEnabled == false)
                return;
            if (!(ProjectListing.SelectedItem is FileSystemInfo info))
                return;
            var dialog = new SaveFileDialog { Filter = "All Files|*.*" };
            if (dialog.ShowDialog() == true)
                File.Copy(info.FullName, dialog.FileName);
        }

        private void CopyPath(object sender, RoutedEventArgs args)
        {
            if (ContextCopyPath.IsEnabled == false)
                return;
            if (!(ProjectListing.SelectedItem is FileSystemInfo info))
                return;
            Clipboard.SetText(info.FullName);
        }

        private async void RenameItem(object sender, RoutedEventArgs args)
        {
            if (ContextRename.IsEnabled == false)
                return;
            if (!(ProjectListing.SelectedItem is FileSystemInfo info))
                return;
            var input = await this.ShowInputAsync("Renaming something", "Enter the new name for this file or directory.", new MetroDialogSettings
            {
                AffirmativeButtonText = "Rename",
                DefaultText = info.Name
            });
            if (string.IsNullOrEmpty(input))
                return;
            if (info.Attributes.HasFlag(FileAttributes.Directory))
            {
                Directory.Move(info.FullName, Path.Combine(Path.GetDirectoryName(info.FullName)!, input));
            }
            else
            {
                if (!input.Contains("."))
                {
                    if (MessageBox.Show("Your file name must have an extension, but do you still want to continue?", "SmModStudio", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                        return;
                    File.Move(info.FullName, Path.Combine(Path.GetDirectoryName(info.FullName)!, input));
                }
            }
            SetHierarchy(_selectedPath);
        }

        private void DeleteItem(object sender, RoutedEventArgs args)
        {
            if (ContextDelete.IsEnabled == false)
                return;
            if (!(ProjectListing.SelectedItem is FileSystemInfo info))
                return;
            if (MessageBox.Show("Are you sure that you want to delete this file or directory?", "SmModStudio", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            if (info.Attributes.HasFlag(FileAttributes.Directory))
                Directory.Delete(info.FullName);
            else
                File.Delete(info.FullName);
            SetHierarchy(_selectedPath);
        }

        private void ShowProjectProperties(object sender, RoutedEventArgs args)
        {
            new WnProperties(_modDescPath) { Owner = this }.ShowDialog();
        }

        private void PageViewNavigated(object sender, NavigationEventArgs args)
        {
            EditMenu.IsEnabled = PageView.Content.Equals(App.PageEditor);
        }

        private void ShowPreferences(object sender, RoutedEventArgs args)
        {
            new WnPreferences { Owner = this }.Show();
        }

        private async void ShowAbout(object sender, RoutedEventArgs args)
        {
            await this.ShowMessageAsync("About this program", "This program was created by Dennise Catolos.\n\nContact me on Discord, @dentolos19#6996.\nFind me on GitHub, @dentolos19.\nFind me on Twitter, @dentolos19.");
        }

    }

}