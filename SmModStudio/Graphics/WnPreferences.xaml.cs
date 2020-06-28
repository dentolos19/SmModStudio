using System.ComponentModel;
using System.Windows;
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
            foreach (var accent in Constants.AccentNames)
                AccentNameBox.Items.Add(accent);
            GameDataPathBox.Text = App.Settings.GameDataPath;
            AccentNameBox.Text = App.Settings.AccentName;
            EnableDarkModeSwitch.IsChecked = App.Settings.EnableDarkMode;
            EnableRichPresenceSwitch.IsChecked = App.Settings.EnableRichPresence;
        }
        
        private void PreferencesClosing(object sender, CancelEventArgs args)
        {
            Hide();
            args.Cancel = true;
        }

        private void SaveSettings(object sender, RoutedEventArgs args)
        {
            App.Settings.GameDataPath = GameDataPathBox.Text;
            App.Settings.AccentName = AccentNameBox.Text;
            App.Settings.EnableDarkMode = EnableDarkModeSwitch.IsChecked == true;
            App.Settings.EnableRichPresence = EnableRichPresenceSwitch.IsChecked == true;
            App.Settings.Save();
            if (MessageBox.Show("All settings has been saved, do you want to restart this program?", "SmModStudio", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Utilities.RestartApp();
        }

        private void ResetSettings(object sender, RoutedEventArgs args)
        {
            App.Settings.Reset();
            if (MessageBox.Show("All settings has changed to their default settings, do you want to restart this program?", "SmModStudio", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Utilities.RestartApp();
        }
        
    }

}