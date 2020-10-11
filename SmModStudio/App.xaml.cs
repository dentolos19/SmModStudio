using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.Highlighting;
using SmModStudio.Core;
using SmModStudio.Core.Enums;
using SmModStudio.Graphics;

namespace SmModStudio
{

    public partial class App
    {

        internal static Configuration Settings { get; private set; }

        #region Methods

        private void AutoDetectSteamLocation()
        {
            if (!string.IsNullOrEmpty(Settings.GameDataPath) && !string.IsNullOrEmpty(Settings.WorkshopPath) && !string.IsNullOrEmpty(Settings.UserDataPath))
                return;
            var steamPath = Utilities.GetSteamLocation();
            if (string.IsNullOrEmpty(steamPath))
                goto SkipToPrerequisites;
            if (Utilities.CheckSteamLocation(steamPath))
            {
                Settings.GameDataPath = Path.Combine(steamPath, "steamapps", "common", "Scrap Mechanic");
                Settings.WorkshopPath = Path.Combine(steamPath, "steamapps", "workshop", "content", Constants.GameId.ToString());
                Settings.UserDataPath = Directory.GetDirectories(Constants.UsersDataPath)[0];
                Settings.Save();
                return;
            }
            steamPath = Utilities.GetSteamAppsLocation(steamPath);
            if (string.IsNullOrEmpty(steamPath))
                goto SkipToPrerequisites;
            if (Utilities.CheckSteamLocation(steamPath))
            {
                Settings.GameDataPath = Path.Combine(steamPath, "steamapps", "common", "Scrap Mechanic");
                Settings.WorkshopPath = Path.Combine(steamPath, "steamapps", "workshop", "content", Constants.GameId.ToString());
                Settings.UserDataPath = Directory.GetDirectories(Constants.UsersDataPath)[0];
                Settings.Save();
                return;
            }
            SkipToPrerequisites:
            var dialog = new WnPrerequisites();
            if (dialog.ShowDialog() == true)
                return;
            Current.Shutdown();
        }

        private void SetupHighlightingDefinitions()
        {
            var luaDefinition = Utilities.CreateHighlightingDefinition(Utilities.RetrieveResourceString("SmModStudio.Resources.Documents.Lua.xshd"));
            HighlightingManager.Instance.RegisterHighlighting(luaDefinition.Name, new[] { ".lua", ".rlua" }, luaDefinition);
        }

        private void SetAppTheme()
        {
            var theme = Settings.AppTheme == AppThemeOptions.Light ? new ResourceDictionary { Source = new Uri("pack://application:,,,/Resources/Themes/Light.xaml") } : new ResourceDictionary { Source = new Uri("pack://application:,,,/Resources/Themes/Dark.xaml") };
            Current.Resources.MergedDictionaries.Add(theme);
        }

        private void SetAppLanguage()
        {
            var culture = Settings.AppLanguage switch
            {
                AppLanguageOptions.Spanish => "es-ES",
                _ => "en-US" // English
            };
            // TODO
        }

        #endregion

        #region Events

        private void InitializeApp(object sender, StartupEventArgs args)
        {
            Settings = Configuration.Load();
            AutoDetectSteamLocation();
            SetupHighlightingDefinitions();
            SetAppTheme();
            SetAppLanguage();
            MainWindow = new WnStudio();
            MainWindow.Show();
        }

        private void HandleError(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            args.Handled = true;
            new WnErrorHandler(args.Exception).ShowDialog();
        }

        #endregion

    }

}