﻿using System;
using System.Windows;
using SmModStudio.Core;

namespace SmModStudio.Graphics
{

    public partial class WnErrorHandler
    {

        #region Methods

        public WnErrorHandler(Exception error)
        {
            InitializeComponent();
            MessageText.Text = error.Message;
            StackTraceText.Text = error.StackTrace ?? "The error info doesn't contain stack trace data.";
        }

        #endregion

        #region Events

        private void Restart(object sender, RoutedEventArgs args)
        {
            Utilities.RestartApp();
        }

        private void Exit(object sender, RoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }

        #endregion

    }

}