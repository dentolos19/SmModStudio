using System.Diagnostics;
using System.Windows;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using SmModStudio.Core;
using SmModStudio.Core.Features;
using SmModStudio.Graphics;

namespace SmModStudio
{

    public partial class App
    {

        public static bool OnDebugMode { get; private set; }

        public static Configuration Settings { get; private set; }

        public static PgEditor PageEditor { get; private set; }
        public static PgImagePreviewer PageImagePreviewer { get; private set; }
        public static PgObjectPreviewer PageObjectPreviewer { get; private set; }

        private void Initialize(object sender, StartupEventArgs args)
        {
#if DEBUG
            OnDebugMode = true;
#endif
            if (Debugger.IsAttached)
                OnDebugMode = true;
            Settings = Configuration.Load();
            if (OnDebugMode)
                Settings.EnableDeveloperConsole = true;
            if (Settings.EnableRichPresence)
                RichPresence.Instance.Activate();
            if (Settings.EnableDeveloperAnalytics)
                AppCenter.Start(Constants.AppCenterAppSecret, typeof(Analytics), typeof(Crashes));
            if (Settings.EnableDeveloperConsole)
            {
                DeveloperConsole.Instance.Activate();
                if (OnDebugMode)
                    DeveloperConsole.Instance.AlsoUseDebug = true;
            }
            Utilities.SetAppTheme(Settings.AccentName, Settings.EnableDarkMode);
            PageEditor = new PgEditor();
            PageImagePreviewer = new PgImagePreviewer();
            PageObjectPreviewer = new PgObjectPreviewer();
            new WnStudio().Show();
        }

        private void Deinitialize(object sender, ExitEventArgs args)
        {
            RichPresence.Instance.Deactivate();
            DeveloperConsole.Instance.Deactivate();
        }

    }

}