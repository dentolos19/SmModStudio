using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using SmModStudio.Core;
using SmModStudio.Core.Models;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace SmModStudio.Graphics
{

    public partial class PgLanguageEditor
    {

        private readonly string _currentFilePath;
        private readonly ObservableCollection<InventoryDescriptionModel> _descriptions;

        public PgLanguageEditor(string filePath)
        {
            InitializeComponent();
            _currentFilePath = filePath;
            _descriptions = new ObservableCollection<InventoryDescriptionModel>();
            foreach (var description in Utilities.LoadInventoryDescriptions(filePath))
                _descriptions.Add(description);
            DescriptionListBox.DataContext = _descriptions;
            Title = Path.GetFileName(_currentFilePath);
        }

        public void Save(object sender, RoutedEventArgs args)
        {
            Utilities.SaveInventoryDescriptions(_descriptions, _currentFilePath);
        }

        private void Add(object sender, RoutedEventArgs args)
        {
            AdonisMessageBox.Show(Constants.TxtDialogMsg5, Constants.TxtDialogTitle);
        }

        private void Remove(object sender, RoutedEventArgs args)
        {
            AdonisMessageBox.Show(Constants.TxtDialogMsg5, Constants.TxtDialogTitle);
        }

    }

}