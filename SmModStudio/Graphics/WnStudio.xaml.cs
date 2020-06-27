using System;
using System.ComponentModel;
using System.Windows;

namespace SmModStudio.Graphics
{

    public partial class WnStudio
    {

        public WnStudio()
        {
            InitializeComponent();
            App.WindowPreferences.Show();
        }

        public void LoadProject(string filePath)
        {
            // TODO: Load Project
        }

        private void StudioClosing(object sender, CancelEventArgs args)
        {
            // TODO: Add Unsaved Project Safety
        }

        private void StudioClosed(object sender, EventArgs args)
        {
            Application.Current.Shutdown();
        }
    }

}