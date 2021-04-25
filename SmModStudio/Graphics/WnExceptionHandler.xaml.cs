using System;
using System.Windows;
using SmModStudio.Core;

namespace SmModStudio.Graphics
{

    public partial class WnExceptionHandler
    {

        public WnExceptionHandler(Exception error)
        {
            InitializeComponent();
            MessageText.Text = error.Message;
            StackTraceText.Text = error.StackTrace ?? Constants.TxtOtherMsg1;
        }

        private void Restart(object sender, RoutedEventArgs args)
        {
            Utilities.RestartApp();
        }

        private void Exit(object sender, RoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }

    }

}