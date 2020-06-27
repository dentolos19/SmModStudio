using System.Windows;
using SmModStudio.Core;
using SmModStudio.Core.Features;
using SmModStudio.Graphics;

namespace SmModStudio
{

    public partial class App
    {

        public static Configuration Settings { get; private set; }
        
        public static RichPresence RichPresenceFeature { get; private set; }

        private void Initialize(object sender, StartupEventArgs args)
        {
            Settings = Configuration.Load();
            RichPresenceFeature = new RichPresence();
            if (Settings.EnableRichPresence)
                RichPresenceFeature.Activate();
            Utilities.SetAppTheme(Utilities.GetRandomAccent());
            new WnMain().Show();
        }

    }

}