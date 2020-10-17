using System.IO;
using System.Windows;
using SmModStudio.Core;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace SmModStudio.Graphics 
{

    public partial class WnNewFolder
    {

        private readonly string _directoryPath;

        public WnNewFolder(string directoryPath)
        {
            InitializeComponent();
            _directoryPath = directoryPath;
        }

        private void Create(object sender, RoutedEventArgs args)
        {
            if (string.IsNullOrEmpty(FolderNameBox.Text))
            {
                AdonisMessageBox.Show(Constants.TxtDialogMsg6, Constants.TxtDialogTitle);
                return;
            }
            Directory.CreateDirectory(Path.Combine(_directoryPath, FolderNameBox.Text));
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs args)
        {
            Close();
        }

    }

}