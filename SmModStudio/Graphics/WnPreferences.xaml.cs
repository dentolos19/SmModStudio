﻿using System.IO;
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

        #region Methods

        public WnPreferences()
        {
            InitializeComponent();
            AppLanguageBox.SelectedIndex = App.Settings.AppLanguage switch
            {
                _ => 0 // English
            };
            AppThemeBox.SelectedIndex = App.Settings.AppTheme switch
            {
                AppThemeOptions.Light => 0,
                _ => 1 // Dark
            };
            GameDataPathBox.Text = App.Settings.GameDataPath;
            WorkshopPathBox.Text = App.Settings.WorkshopPath;
            foreach (var userDataPath in Directory.GetDirectories(Constants.UsersDataPath))
                UserDataPathBox.Items.Add(userDataPath);
            UserDataPathBox.SelectedIndex = 0;
        }

        #endregion

        #region Events

        private void Save(object sender, RoutedEventArgs args)
        {
            App.Settings.AppLanguage = AppLanguageBox.SelectedIndex switch
            {
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
            App.Settings.Save();
            if (AdonisMessageBox.Show("All settings has been saved! Would you like to restart this app?", "SmModStudio", MessageBoxButton.YesNo) == AdonisMessageBoxResult.Yes)
                Utilities.RestartApp();
        }

        private void Reset(object sender, RoutedEventArgs args)
        {
            if (AdonisMessageBox.Show("Are you sure that you want to reset to default settings?", "SmModStudio", MessageBoxButton.YesNo) != AdonisMessageBoxResult.Yes)
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
                MessageBox.Show("The selected path doesn't contain the game's executable!", "SmModStudio");
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

        #endregion

    }

}