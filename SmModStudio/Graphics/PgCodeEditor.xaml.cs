using System;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using ICSharpCode.AvalonEdit.Highlighting;
using SmModStudio.Core;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

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
            foreach (var definition in HighlightingManager.Instance.HighlightingDefinitions)
            {
                var item = new ComboBoxItem
                {
                    Content = definition.Name,
                    Tag = definition
                };
                SyntaxHighlightingBox.Items.Add(item);
            }
            var targetDefinition = HighlightingManager.Instance.GetDefinitionByExtension(Path.GetExtension(_currentFilePath));
            if (targetDefinition != null)
            {
                foreach (var item in SyntaxHighlightingBox.Items.OfType<ComboBoxItem>())
                {
                    if (item.Tag != null && item.Tag.Equals(targetDefinition))
                    {
                        SyntaxHighlightingBox.SelectedItem = item;
                    }
                }
            }
            else
            {
                SyntaxHighlightingBox.SelectedIndex = 0;
            }
            Editor.Load(_currentFilePath);
            Title = Path.GetFileName(_currentFilePath);
        }

        #endregion

        #region Events

        public void Save(object sender, ExecutedRoutedEventArgs args)
        {
            Editor.Save(_currentFilePath);
        }

        private void GoToLine(object sender, ExecutedRoutedEventArgs args)
        {
            AdonisMessageBox.Show(Constants.TxtDialogMsg5, Constants.TxtDialogTitle);
        }

        private void FindReplace(object sender, ExecutedRoutedEventArgs args)
        {
            AdonisMessageBox.Show(Constants.TxtDialogMsg5, Constants.TxtDialogTitle);
        }

        private void UpdateSyntaxSelection(object sender, SelectionChangedEventArgs args)
        {
            var item = (ComboBoxItem)SyntaxHighlightingBox.SelectedItem;
            if (item == null)
                return;
            if (item.Tag is IHighlightingDefinition definition)
            {
                Editor.SyntaxHighlighting = definition;
            }
            else
            {
                Editor.SyntaxHighlighting = null;
            }
        }

        #endregion

        

    }

}