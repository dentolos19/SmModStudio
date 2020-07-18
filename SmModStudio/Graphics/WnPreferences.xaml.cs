using System.Windows;
using Ookii.Dialogs.Wpf;
using SmModStudio.Core;

namespace SmModStudio.Graphics
{

    public partial class WnPreferences
    {

        public WnPreferences()
        {
            InitializeComponent();
        }

        private void PreferencesLoaded(object sender, RoutedEventArgs args)
        {
            GameDataPathBox.Text = App.Settings.GameDataPath;
            AccentNameBox.Text = App.Settings.AccentName;
            EnableDarkModeSwitch.IsChecked = App.Settings.EnableDarkMode;
            EnableRichPresenceSwitch.IsChecked = App.Settings.EnableRichPresence;
            EnableDeveloperAnalyticsSwitch.IsChecked = App.Settings.EnableDeveloperAnalytics;
            EnableDeveloperConsoleSwitch.IsChecked = App.Settings.EnableDeveloperConsole;
            if (App.OnDebugMode)
            {
                EnableDeveloperConsoleSwitch.IsEnabled = false;
                EnableDeveloperConsoleSwitch.Content += " (Required For Debugging Purposes)";
            }
        }

        private void SaveSettings(object sender, RoutedEventArgs args)
        {
            App.Settings.GameDataPath = GameDataPathBox.Text;
            App.Settings.AccentName = AccentNameBox.Text;
            App.Settings.EnableDarkMode = EnableDarkModeSwitch.IsChecked == true;
            App.Settings.EnableRichPresence = EnableRichPresenceSwitch.IsChecked == true;
            App.Settings.EnableDeveloperAnalytics = EnableDeveloperAnalyticsSwitch.IsChecked == true;
            App.Settings.EnableDeveloperConsole = EnableDeveloperConsoleSwitch.IsChecked == true;
            App.Settings.Save();
            if (MessageBox.Show("All settings has been saved, do you want to restart this program?", "SmModStudio", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Utilities.RestartApp();
        }

        private void ResetSettings(object sender, RoutedEventArgs args)
        {
            App.Settings.Reset();
            MessageBox.Show("All settings has changed to their default settings, after closing this window this program will restart.", "SmModStudio");
            Utilities.RestartApp();
        }

        private void BrowseGameDataPath(object sender, RoutedEventArgs args)
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
                GameDataPathBox.Text = dialog.SelectedPath;
        }

    }

}