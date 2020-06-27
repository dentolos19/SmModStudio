using System.IO;
using SmModStudio.Core;

namespace SmModStudio.Graphics
{
    public partial class PgEditor
    {
        
        public string CurrentFile { get; private set; }
        
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
        }

    }
}