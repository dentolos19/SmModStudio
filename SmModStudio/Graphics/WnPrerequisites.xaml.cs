using System.IO;
using System.Windows;
using Ookii.Dialogs.Wpf;
using SmModStudio.Core;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace SmModStudio.Graphics
{

    public partial class WnPrerequisites
    {

        #region Methods

        public WnPrerequisites()
        {
            InitializeComponent();
            foreach (var userDataPath in Directory.GetDirectories(Constants.UsersDataPath))
                UserDataPathBox.Items.Add(userDataPath);
            UserDataPathBox.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(App.Settings.GameDataPath))
                GameDataPathBox.Text = App.Settings.GameDataPath;
            if (!string.IsNullOrEmpty(App.Settings.WorkshopPath))
                WorkshopPathBox.Text = App.Settings.WorkshopPath;
        }

        #endregion

        #region Events

        private void Continue(object sender, RoutedEventArgs args)
        {
            if (string.IsNullOrEmpty(GameDataPathBox.Text) || string.IsNullOrEmpty(UserDataPathBox.Text))
            {
                AdonisMessageBox.Show("Make sure all the inputs are filled!", "SmModStudio");
                return;
            }
            App.Settings.GameDataPath = GameDataPathBox.Text;
            App.Settings.WorkshopPath = WorkshopPathBox.Text;
            App.Settings.UserDataPath = UserDataPathBox.Text;
            App.Settings.Save();
            DialogResult = true;
            Close();
        }

        private void Cancel(object sender, RoutedEventArgs args)
        {
            Application.Current.Shutdown();
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