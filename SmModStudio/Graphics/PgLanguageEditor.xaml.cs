using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SmModStudio.Core;
using SmModStudio.Core.Models;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace SmModStudio.Graphics
{

    public partial class PgLanguageEditor
    {

        private readonly string _currentFilePath;
        private readonly ObservableCollection<InventoryDescriptionModel> _descriptions;

        private InventoryDescriptionModel _currentDescriptionModel;

        public PgLanguageEditor(string filePath)
        {
            InitializeComponent();
            _currentFilePath = filePath;
            _descriptions = new ObservableCollection<InventoryDescriptionModel>();
            foreach (var description in Utilities.LoadInventoryDescriptions(filePath))
                _descriptions.Add(description);
            DescriptionListBox.DataContext = _descriptions;
            Title = Path.GetFileName(_currentFilePath) + $" ({new DirectoryInfo(Path.GetDirectoryName(_currentFilePath)!).Name})";
        }

        public void Save(object sender, RoutedEventArgs args)
        {
            Utilities.SaveInventoryDescriptions(_descriptions, _currentFilePath);
        }

        private void AddItem(object sender, RoutedEventArgs args)
        {
            AdonisMessageBox.Show(Constants.TxtDialogMsg5, Constants.TxtDialogTitle);
        }

        private void RemoveItem(object sender, RoutedEventArgs args)
        {
            AdonisMessageBox.Show(Constants.TxtDialogMsg5, Constants.TxtDialogTitle);
        }

        private void EditItem(object sender, MouseButtonEventArgs args)
        {
            if (!(DescriptionListBox.SelectedItem is InventoryDescriptionModel item))
                return;
            _currentDescriptionModel = item;
            ItemIdBox.Text = _currentDescriptionModel.Id.ToString();
            ItemTitleBox.Text = _currentDescriptionModel.Title;
            ItemDescriptionBox.Text = _currentDescriptionModel.Description;
            Views.SelectedIndex = 1;
        }

        private void SaveEditing(object sender, RoutedEventArgs args)
        {
            var item = _descriptions.FirstOrDefault(description => description.Id == _currentDescriptionModel.Id);
            if (item != null)
            {
                var index = _descriptions.IndexOf(item);
                item.Title = ItemTitleBox.Text;
                item.Description = ItemDescriptionBox.Text;
                _descriptions[index] = item;
                DescriptionListBox.SelectedIndex = _descriptions.IndexOf(item);
            }
            Views.SelectedIndex = 0;
        }

        private void CancelEditing(object sender, RoutedEventArgs args)
        {
            Views.SelectedIndex = 0;
        }

        private void CopyItemId(object sender, RoutedEventArgs args)
        {
            Clipboard.SetText(ItemIdBox.Text);
        }

    }

}