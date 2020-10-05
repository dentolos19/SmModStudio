using System.Windows;
using System.Windows.Threading;
using SmModStudio.Core;
using SmModStudio.Graphics;

namespace SmModStudio
{

    public partial class App
    {

        internal static Configuration Settings { get; private set; }

        internal static WnStudio WindowStudio { get; private set; }

        private void InitializeApp(object sender, StartupEventArgs args)
        {
            Settings = Configuration.Load();
            WindowStudio = new WnStudio();
            WindowStudio.Show();
        }

        private void HandleError(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            args.Handled = true;
            new WnErrorHandler(args.Exception).ShowDialog();
        }

    }

}