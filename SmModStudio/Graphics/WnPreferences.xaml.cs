using System.IO;
using System.Windows;
using Ookii.Dialogs.Wpf;
using SmModStudio.Core;
using SmModStudio.Core.Enums;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;
using MessageBoxButton = AdonisUI.Controls.MessageBoxButton;
using AdonisMessageBoxResult = AdonisUI.Controls.MessageBoxResult;

namespace SmModStudio.Graphics
{

    public partial class WnPreferences
    {

        public WnPreferences()
        {
            InitializeComponent();
            CreditsText.Text = Utilities.RetrieveResourceString("SmModStudio.Resources.Documents.Credits.txt");
            AppLanguageBox.SelectedIndex = App.Settings.AppLanguage switch
            {
                AppLanguageOptions.Chinese => 1,
                _ => 0
            };
            AppThemeBox.SelectedIndex = App.Settings.AppTheme switch
            {
                AppThemeOptions.Light => 0,
                _ => 1
            };
            GameDataPathBox.Text = App.Settings.GameDataPath;
            WorkshopPathBox.Text = App.Settings.WorkshopPath;
            foreach (var userDataPath in Directory.GetDirectories(Constants.UsersDataPath))
                UserDataPathBox.Items.Add(userDataPath);
            UserDataPathBox.SelectedIndex = 0;
            AutoSaveClosingFileOption.IsChecked = App.Settings.AutoSaveClosingFile;
            EnableCodeCompletionOption.IsChecked = App.Settings.EnableCodeCompletion;
        }

        private void Save(object sender, RoutedEventArgs args)
        {
            App.Settings.AppLanguage = AppLanguageBox.SelectedIndex switch
            {
                1 => AppLanguageOptions.Chinese,
                _ => AppLanguageOptions.English
            };
            App.Settings.AppTheme = AppThemeBox.SelectedIndex switch
            {
                0 => AppThemeOptions.Light,
                _ => AppThemeOptions.Dark
            };
            App.Settings.GameDataPath = GameDataPathBox.Text;
            App.Settings.WorkshopPath = WorkshopPathBox.Text;
            App.Settings.UserDataPath = UserDataPathBox.Text;
            App.Settings.AutoSaveClosingFile = AutoSaveClosingFileOption.IsChecked == true;
            App.Settings.EnableCodeCompletion = EnableCodeCompletionOption.IsChecked == true;
            App.Settings.Save();
            if (AdonisMessageBox.Show(Constants.TxtDialogMsg2, Constants.TxtDialogTitle, MessageBoxButton.YesNo) == AdonisMessageBoxResult.Yes)
                Utilities.RestartApp();
        }

        private void Reset(object sender, RoutedEventArgs args)
        {
            if (AdonisMessageBox.Show(Constants.TxtDialogMsg3, Constants.TxtDialogTitle, MessageBoxButton.YesNo) != AdonisMessageBoxResult.Yes)
                return;
            App.Settings.Reset();
            Utilities.RestartApp();
        }

        private void BrowseGameDataPath(object sender, RoutedEventArgs args)
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog(Application.Current.MainWindow) != true)
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
            if (dialog.ShowDialog(Application.Current.MainWindow) == true)
                WorkshopPathBox.Text = dialog.SelectedPath;
        }

    }

}