using System.IO;
using System.Windows;
using Ookii.Dialogs.Wpf;
using SmModStudio.Core;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;
using MessageBoxButton = AdonisUI.Controls.MessageBoxButton;
using MessageBoxResult = AdonisUI.Controls.MessageBoxResult;

namespace SmModStudio.Graphics
{

    public partial class WnPreferences
    {

        #region Methods

        public WnPreferences()
        {
            InitializeComponent();
            foreach (var userDataPath in Directory.GetDirectories(Constants.UsersDataPath))
                UserDataPathBox.Items.Add(userDataPath);
            UserDataPathBox.SelectedIndex = 0;
            GameDataPathBox.Text = App.Settings.GameDataPath;
            WorkshopPathBox.Text = App.Settings.WorkshopPath;
        }

        #endregion

        #region Events

        private void Save(object sender, RoutedEventArgs args)
        {
            App.Settings.GameDataPath = GameDataPathBox.Text;
            App.Settings.WorkshopPath = WorkshopPathBox.Text;
            App.Settings.UserDataPath = UserDataPathBox.Text;
            App.Settings.Save();
            AdonisMessageBox.Show("All settings has been saved!", "SmModStudio");
        }

        private void Reset(object sender, RoutedEventArgs args)
        {
            if (AdonisMessageBox.Show("Are you sure that you want to reset to default settings?", "SmModStudio", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
                return;
            App.Settings.Reset();
            Utilities.RestartApp();
        }

        private void BrowseGameDataPath(object sender, RoutedEventArgs args)
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() != true)
                return;
            if (!File.Exists(Path.Combine(dialog.SelectedPath, "Release", "ScrapMechanic.exe")))
            {
                MessageBox.Show("The selected path doesn't contain the game's executable!", "SmModStudio");
                return;
            }
            GameDataPathBox.Text = dialog.SelectedPath;
        }

        private void BrowseWorkshopPath(object sender, RoutedEventArgs args)
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
                WorkshopPathBox.Text = dialog.SelectedPath;
        }

        #endregion

    }

}