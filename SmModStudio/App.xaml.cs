using System.Windows;
using SmModStudio.Core;
using SmModStudio.Core.Features;
using SmModStudio.Graphics;

namespace SmModStudio
{

    public partial class App
    {

        public static Configuration Settings { get; private set; }
        
        public static RichPresence FeatureRichPresence { get; private set; }
        
        public static WnStudio WindowStudio { get; private set; }
        public static WnPreferences WindowPreferences { get; private set; }
        public static PgEditor PageEditor { get; private set; }

        private void Initialize(object sender, StartupEventArgs args)
        {
            Settings = Configuration.Load();
            FeatureRichPresence = new RichPresence();
            if (Settings.EnableRichPresence)
                FeatureRichPresence.Activate();
            Utilities.SetAppTheme(Settings.AccentName, Settings.EnableDarkMode);
            PageEditor = new PgEditor();
            WindowPreferences = new WnPreferences();
            WindowStudio = new WnStudio();
            if (args.Args.Length == 1)
                WindowStudio.LoadProject(args.Args[0]);
            WindowStudio.Show();
        }

    }

}