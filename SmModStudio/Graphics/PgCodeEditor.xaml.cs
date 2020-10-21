using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using ICSharpCode.AvalonEdit.CodeCompletion;
using ICSharpCode.AvalonEdit.Highlighting;
using Newtonsoft.Json;
using SmModStudio.Core;
using SmModStudio.Core.Bindings;
using SmModStudio.Core.Models;
using AdonisMessageBox = AdonisUI.Controls.MessageBox;

namespace SmModStudio.Graphics
{

    public partial class PgCodeEditor
    {

        private readonly CcDocumentationModel _documentation;
        private readonly string _currentFilePath;

        private CompletionWindow _completionWindow;

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
            if (App.Settings.EnableCodeCompletion)
            {
                _documentation = JsonConvert.DeserializeObject<CcDocumentationModel>(Utilities.RetrieveResourceString("SmModStudio.Resources.Documents.Documentation.json"));
                Editor.TextArea.TextEntering += CodeCompletionEntering;
                Editor.TextArea.TextEntered += CodeCompletionEntered;
            }
            Editor.Load(_currentFilePath);
            Title = Path.GetFileName(_currentFilePath);
        }

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

        private void CodeCompletionEntering(object sender, TextCompositionEventArgs args)
        {
            _completionWindow = new CompletionWindow(Editor.TextArea);
            foreach (var @namespace in _documentation.Namespaces)
            {
                foreach (var member in @namespace.Members)
                    _completionWindow.CompletionList.CompletionData.Add(new CodeCompletionBinding(@namespace.Name, member));
            }
            _completionWindow.Closed += (o, a) => { _completionWindow = null; };
            _completionWindow.Show();
        }

        private void CodeCompletionEntered(object sender, TextCompositionEventArgs args)
        {
            if (args.Text.Length <= 0 || _completionWindow == null)
                return;
            if (!char.IsLetterOrDigit(args.Text[0]))
                _completionWindow.CompletionList.RequestInsertion(args);
        }

    }

}