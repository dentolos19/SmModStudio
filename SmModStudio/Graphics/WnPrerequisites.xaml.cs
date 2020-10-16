using System.IO;
using System.Windows;
using Ookii.Dialogs.Wpf;
using SmModStudio.Core;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace SmModStudio.Graphics
{

    public partial class WnPrerequisites
    {

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

        private void Continue(object sender, RoutedEventArgs args)
        {
            if (string.IsNullOrEmpty(GameDataPathBox.Text) || string.IsNullOrEmpty(UserDataPathBox.Text))
            {
                AdonisMessageBox.Show(Constants.TxtDialogMsg0, Constants.TxtDialogTitle);
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
                MessageBox.Show(Constants.TxtDialogMsg1, Constants.TxtDialogTitle);
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

    }

}