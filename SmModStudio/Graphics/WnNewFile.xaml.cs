using System.IO;
using System.Windows;
using System.Windows.Controls;
using SmModStudio.Core;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace SmModStudio.Graphics
{

    public partial class WnNewFile
    {

        private readonly string _directoryPath;

        public WnNewFile(string directoryPath)
        {
            InitializeComponent();
            _directoryPath = directoryPath;
            FileTypeBox.SelectedIndex = 0;
        }

        private void Create(object sender, RoutedEventArgs args)
        {
            if (string.IsNullOrEmpty(FileNameBox.Text))
            {
                AdonisMessageBox.Show(Constants.TxtDialogMsg6, Constants.TxtDialogTitle);
                return;
            }
            var item = (ComboBoxItem)FileTypeBox.SelectedItem;
            var fileExtension = (string)item.Content switch
            {
                "Lua" => ".lua",
                "Json" => ".json",
                _ => ".txt"
            };
            var filePath = Path.Combine(_directoryPath, FileNameBox.Text + fileExtension);
            if (File.Exists(filePath))
            {
                AdonisMessageBox.Show(Constants.TxtDialogMsg7, Constants.TxtDialogTitle);
                return;
            }
            File.CreateText(filePath);
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs args)
        {
            Close();
        }

    }

}