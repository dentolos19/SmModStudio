using System.ComponentModel;
using System.Windows;
using Microsoft.Win32;
using SmModStudio.Core;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace SmModStudio.Graphics
{

    public partial class WnStudio
    {

        public WnStudio()
        {
            InitializeComponent();
        }

        public void LoadProject(string filePath)
        {
            // TODO: Load Project
        }

        private void OpenSpecificFile(string filePath)
        {
            PageView.Navigate(App.PageEditor);
            App.PageEditor.EditFile(filePath);
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

        private void StudioClosing(object sender, CancelEventArgs args)
        {
            // TODO: Add unsaved project safety
        }

        private void OpenProject(object sender, RoutedEventArgs args)
        {
            // TODO: Open Project
        }
        
        private void OpenFile(object sender, RoutedEventArgs args)
        {
            var dialog = new OpenFileDialog { Filter = "Lua Source File|*.lua|JSON Source File|*.json|All Files|*.*" };
            if (dialog.ShowDialog() == true)
                OpenSpecificFile(dialog.FileName);
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
            if (App.PageEditor.CodeEditor.CanUndo && PageView.Content.Equals(App.PageEditor))
                App.PageEditor.CodeEditor.Undo();
        }
        
        private void RedoText(object sender, RoutedEventArgs args)
        {
            if (App.PageEditor.CodeEditor.CanRedo && PageView.Content.Equals(App.PageEditor))
                App.PageEditor.CodeEditor.Redo();
        }
        
        private void CutText(object sender, RoutedEventArgs args)
        {
            if (PageView.Content.Equals(App.PageEditor))
                App.PageEditor.CodeEditor.Cut();
        }
        
        private void CopyText(object sender, RoutedEventArgs args)
        {
            if (PageView.Content.Equals(App.PageEditor))
                App.PageEditor.CodeEditor.Copy();
        }
        
        private void PasteText(object sender, RoutedEventArgs args)
        {
            if (PageView.Content.Equals(App.PageEditor))
                App.PageEditor.CodeEditor.Paste();
        }
        
        private void DeleteText(object sender, RoutedEventArgs args)
        {
            if (PageView.Content.Equals(App.PageEditor))
                App.PageEditor.CodeEditor.Delete();
        }
        
        private void SelectAllText(object sender, RoutedEventArgs args)
        {
            if (PageView.Content.Equals(App.PageEditor))
                App.PageEditor.CodeEditor.SelectAll();
        }
        
        private void ShowPreferences(object sender, RoutedEventArgs args)
        {
            new WnPreferences { Owner = this }.Show();
        }
        
        private void ShowAbout(object sender, RoutedEventArgs args)
        {
            // TODO: Show about
        }

    }

}