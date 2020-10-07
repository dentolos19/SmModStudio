﻿using System.Windows.Input;

namespace SmModStudio.Graphics
{

    public partial class PgCodeEditor
    {

        private readonly string _currentFilePath;

        #region Methods

        public PgCodeEditor(string filePath)
        {
            InitializeComponent();
            _currentFilePath = filePath;
            Editor.Load(_currentFilePath);
        }

        #endregion

        #region Events

        public void Save(object sender, ExecutedRoutedEventArgs args)
        {
            Editor.Save(_currentFilePath);
        }

        private void GoToLine(object sender, ExecutedRoutedEventArgs args)
        {
            // TODO
        }

        private void FindReplace(object sender, ExecutedRoutedEventArgs args)
        {
            // TODO
        }

        #endregion

    }

}