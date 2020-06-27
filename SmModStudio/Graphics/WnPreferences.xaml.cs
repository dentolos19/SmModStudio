using System.ComponentModel;

namespace SmModStudio.Graphics
{

    public partial class WnPreferences
    {

        public WnPreferences()
        {
            Owner = App.WindowStudio;
            InitializeComponent();
        }

        private void PreferencesClosing(object sender, CancelEventArgs args)
        {
            args.Cancel = true;
            Hide();
        }
    }

}