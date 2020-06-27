using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;

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

        public void OpenSpecificFile(string filePath)
        {
            PageView.Navigate(App.PageEditor);
            App.PageEditor.EditFile(filePath);
        }

        private void StudioClosing(object sender, CancelEventArgs args)
        {
            // TODO: Add Unsaved Project Safety
        }

        private void StudioClosed(object sender, EventArgs args)
        {
            Application.Current.Shutdown();
        }

        private void NewProject(object sender, RoutedEventArgs args)
        {
            this.ShowMessageAsync("Hello world!", "Testing New");
        }
        
        private void OpenProject(object sender, RoutedEventArgs args)
        {
            // TODO: Open Project
        }
        
        private void OpenFile(object sender, RoutedEventArgs args)
        {
            var dialog = new OpenFileDialog {Filter = "Lua Source File|*.lua|JSON Source File|*.json|All Files|*.*"};
            if (dialog.ShowDialog() == true)
                OpenSpecificFile(dialog.FileName);
        }
        
        private void SaveFile(object sender, RoutedEventArgs args)
        {
            if (string.IsNullOrEmpty(App.PageEditor.CurrentFile))
            {
                SaveFileAs(null, null);
                return;
            }
            App.PageEditor.Save();
        }
        
        private void SaveFileAs(object sender, RoutedEventArgs args)
        {
            var dialog = new SaveFileDialog {Filter = "Lua Source File|*.lua|JSON Source File|*.json|All Files|*.*", DefaultExt = App.PageEditor.SaveExtension};
            if (dialog.ShowDialog() == true)
                App.PageEditor.Save(dialog.FileName);
        }

        private void ExitStudio(object sender, RoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }

        private void UndoText(object sender, RoutedEventArgs args)
        {
            if (App.PageEditor.CodeEditor.CanUndo)
                App.PageEditor.CodeEditor.Undo();
        }
        
        private void RedoText(object sender, RoutedEventArgs args)
        {
            if (App.PageEditor.CodeEditor.CanRedo)
                App.PageEditor.CodeEditor.Redo();
        }
        
        private void CutText(object sender, RoutedEventArgs args)
        {
            App.PageEditor.CodeEditor.Cut();
        }
        
        private void CopyText(object sender, RoutedEventArgs args)
        {
            App.PageEditor.CodeEditor.Copy();
        }
        
        private void PasteText(object sender, RoutedEventArgs args)
        {
            App.PageEditor.CodeEditor.Paste();
        }
        
        private void DeleteText(object sender, RoutedEventArgs args)
        {
            App.PageEditor.CodeEditor.Delete();
        }
        
        private void SelectAllText(object sender, RoutedEventArgs args)
        {
            App.PageEditor.CodeEditor.SelectAll();
        }
        
        private void ShowPreferences(object sender, RoutedEventArgs args)
        {
            App.WindowPreferences.ShowDialog();
        }
        
        private void ShowAbout(object sender, RoutedEventArgs args)
        {
            
        }
        
    }

}