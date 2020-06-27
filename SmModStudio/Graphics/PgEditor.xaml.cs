using System.IO;
using SmModStudio.Core;

namespace SmModStudio.Graphics
{
    public partial class PgEditor
    {

        public string CurrentFile { get; private set; }
        public string SaveExtension { get; private set; }
        
        public PgEditor()
        {
            InitializeComponent();
        }

        public void EditFile(string filePath)
        {
            var extension = Path.GetExtension(filePath);
            switch (extension.ToLower())
            {
                case ".lua":
                    CodeEditor.SyntaxHighlighting = Constants.LuaSyntax;
                    SaveExtension = ".lua";
                    break;
                case ".json":
                    CodeEditor.SyntaxHighlighting = Constants.JsonSyntax;
                    SaveExtension = ".json";
                    break;
                default:
                    CodeEditor.SyntaxHighlighting = null;
                    SaveExtension = string.Empty;
                    break;
            }

            CurrentFile = filePath;
            CodeEditor.Load(CurrentFile);
        }

        public void Save(string filePath = null)
        {
            if (!string.IsNullOrEmpty(filePath))
                CurrentFile = filePath;
            CodeEditor.Save(CurrentFile);
        }

    }
}