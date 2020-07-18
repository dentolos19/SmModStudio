using System.IO;
using SmModStudio.Core;
using SmModStudio.Core.Features;

namespace SmModStudio.Graphics
{

    public partial class PgEditor
    {

        public PgEditor()
        {
            InitializeComponent();
        }

        public string CurrentFile { get; private set; }

        public void EditFile(string filePath, string projectName = "nothing")
        {
            var extension = Path.GetExtension(filePath);
            switch (extension.ToLower())
            {
                case ".lua":
                    CodeEditor.SyntaxHighlighting = Constants.LuaSyntax;
                    break;
                case ".json":
                    CodeEditor.SyntaxHighlighting = Constants.JsonSyntax;
                    break;
                default:
                    CodeEditor.SyntaxHighlighting = null;
                    break;
            }
            CurrentFile = filePath;
            CodeEditor.Load(CurrentFile);
            if (RichPresence.Instance.IsActivated)
                RichPresence.Instance.SetWorkPresence(projectName, Path.GetFileName(filePath));
        }

        public void Save(string filePath = null)
        {
            if (!string.IsNullOrEmpty(filePath))
                CurrentFile = filePath;
            CodeEditor.Save(CurrentFile);
        }

    }

}