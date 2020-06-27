using System.Windows;
using SmModStudio.Core;
using SmModStudio.Graphics;

namespace SmModStudio
{

    public partial class App
    {

        public static Configuration Settings { get; private set; }

        private void Initialize(object sender, StartupEventArgs args)
        {
            Settings = Configuration.Load();
            Utilities.SetAppTheme(Utilities.GetRandomAccent());
            new WnMain().Show();
        }

    }

}