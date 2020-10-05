using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows.Input;
using SmModStudio.Core;
using SmModStudio.Core.Bindings;

namespace SmModStudio.Graphics
{

    public partial class WnStudio
    {

        private readonly ObservableCollection<ContentTabBinding> _tabs;

        public WnStudio()
        {
            InitializeComponent();
            _tabs = new ObservableCollection<ContentTabBinding>();
            Views.ItemsSource = _tabs;
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
            var binding = new ContentTabBinding { Header = Path.GetFileName(item.Path), Content = page, Path = item.Path };
            _tabs.Add(binding);
        }

        private void CloseTab(object sender, MouseButtonEventArgs args)
        {
            _tabs.RemoveAt(Views.SelectedIndex);
        }

    }

}