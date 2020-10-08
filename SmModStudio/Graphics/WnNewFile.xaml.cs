using System.IO;
using System.Windows;
using System.Windows.Controls;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace SmModStudio.Graphics
{

    public partial class WnNewFile
    {

        private readonly string _directoryPath;

        #region Methods

        public WnNewFile(string directoryPath)
        {
            InitializeComponent();
            _directoryPath = directoryPath;
            FileTypeBox.SelectedIndex = 0;
        }

        #endregion

        #region Events

        private void Create(object sender, RoutedEventArgs args)
        {
            if (string.IsNullOrEmpty(FileNameBox.Text))
            {
                AdonisMessageBox.Show("You must enter a file name to continue!", "SmModStudio");
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
                AdonisMessageBox.Show("File already exists with that name!", "SmModStudio");
                return;
            }
            File.CreateText(filePath);
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs args)
        {
            Close();
        }

        #endregion

    }

}