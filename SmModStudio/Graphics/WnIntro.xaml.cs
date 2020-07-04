using System.Windows;
using Ookii.Dialogs.Wpf;

namespace SmModStudio.Graphics
{

    public partial class WnIntro
    {

        public WnIntro()
        {
            InitializeComponent();
        }

        private void Browse(object sender, RoutedEventArgs args)
        {
            var dialog = new VistaFolderBrowserDialog();
            if (dialog.ShowDialog() == true)
                GameDataPathBox.Text = dialog.SelectedPath;
        }

        private void Continue(object sender, RoutedEventArgs args)
        {
            if (string.IsNullOrEmpty(GameDataPathBox.Text))
            {
                MessageBox.Show("Please enter the game data path before continuing!");
                return;
            }
            App.Settings.GameDataPath = GameDataPathBox.Text;
            App.Settings.Save();
            Close();
        }

    }

}