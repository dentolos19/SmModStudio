using System.IO;
using System.Windows;
using System.Windows.Threading;
using ICSharpCode.AvalonEdit.Highlighting;
using SmModStudio.Core;
using SmModStudio.Graphics;

namespace SmModStudio
{

    public partial class App
    {

        internal static Configuration Settings { get; private set; }

        private void InitializeApp(object sender, StartupEventArgs args)
        {
            Settings = Configuration.Load();
            if (string.IsNullOrEmpty(Settings.GameDataPath) || string.IsNullOrEmpty(Settings.WorkshopPath) || string.IsNullOrEmpty(Settings.UserDataPath))
            {
                var steamPath = Utilities.GetSteamLocation();
                if (string.IsNullOrEmpty(steamPath))
                    goto SkipToPrerequisites;
                if (Utilities.CheckSteamLocation(steamPath))
                {
                    Settings.GameDataPath = Path.Combine(steamPath, "steamapps", "common", "Scrap Mechanic");
                    Settings.WorkshopPath = Path.Combine(steamPath, "steamapps", "workshop", "content", Constants.GameId.ToString());
                    Settings.UserDataPath = Directory.GetDirectories(Constants.UsersDataPath)[0];
                    Settings.Save();
                    goto SkipToStartup;
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
                    goto SkipToStartup;
                }
                SkipToPrerequisites:
                var dialog = new WnPrerequisites();
                if (dialog.ShowDialog() == true)
                    goto SkipToStartup;
                Current.Shutdown();
                return;
            }
            SkipToStartup:
            HighlightingManager.Instance.RegisterHighlighting("RblxLua", new[] { ".rlua", ".lua" }, Utilities.CreateDefinition(Utilities.RetrieveResourceString("SmModStudio.Resources.Documents.Lua.xshd")));
            MainWindow = new WnStudio();
            MainWindow.Show();
        }

        private void HandleError(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            args.Handled = true;
            new WnErrorHandler(args.Exception).ShowDialog();
        }

    }

}