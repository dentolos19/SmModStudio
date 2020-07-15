﻿using System.Windows;
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

        public static Configuration Settings { get; private set; }

        public static WnStudio WindowStudio { get; private set; }
        public static PgEditor PageEditor { get; private set; }
        public static PgImagePreviewer PageImagePreviewer { get; private set; }

        private void Initialize(object sender, StartupEventArgs args)
        {
            Settings = Configuration.Load();
            if (Settings.EnableRichPresence)
                RichPresence.Instance.Activate();
            if (Settings.EnableDeveloperAnalytics)
                AppCenter.Start(Constants.AppCenterAppSecret, typeof(Analytics), typeof(Crashes));
            Utilities.SetAppTheme(Settings.AccentName, Settings.EnableDarkMode);
            PageEditor = new PgEditor();
            PageImagePreviewer = new PgImagePreviewer();
            WindowStudio = new WnStudio();
            WindowStudio.Show();
        }

    }

}