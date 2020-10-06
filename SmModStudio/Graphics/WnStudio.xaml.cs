using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SmModStudio.Core;
using SmModStudio.Core.Bindings;

namespace SmModStudio.Graphics
{

    public partial class WnStudio
    {

        private readonly ObservableCollection<ViewTabBinding> _tabs;

        public WnStudio()
        {
            InitializeComponent();
            _tabs = new ObservableCollection<ViewTabBinding>();
            Views.ItemsSource = _tabs;
        }

        private void GoToLine(object sender, RoutedEventArgs args)
        {
            // TODO
        }

        private void FindReplace(object sender, RoutedEventArgs args)
        {
            // TODO
        }

        private void Undo(object sender, RoutedEventArgs args)
        {
            // TODO
        }

        private void Redo(object sender, RoutedEventArgs args)
        {
            // TODO
        }

        private void Cut(object sender, RoutedEventArgs args)
        {
            // TODO
        }

        private void Copy(object sender, RoutedEventArgs args)
        {
            // TODO
        }

        private void Paste(object sender, RoutedEventArgs args)
        {
            // TODO
        }

        private void Delete(object sender, RoutedEventArgs args)
        {
            // TODO
        }

        private void SelectAll(object sender, RoutedEventArgs args)
        {
            // TODO
        }

        private void ShowPreferences(object sender, RoutedEventArgs args)
        {
            // TODO
        }

        private void ShowAbout(object sender, RoutedEventArgs args)
        {
            // TODO
        }

        private void OpenFileInHierarchy(object sender, MouseButtonEventArgs args)
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
        }

        private void CloseTab(object sender, MouseButtonEventArgs args)
        {
            _tabs.RemoveAt(Views.SelectedIndex);
        }

    }

}