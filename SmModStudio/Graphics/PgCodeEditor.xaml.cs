using System.IO;
using SmModStudio.Core;

namespace SmModStudio.Graphics
{

    public partial class PgCodeEditor
    {

        public PgCodeEditor(string filePath)
        {
            InitializeComponent();
            Editor.SyntaxHighlighting = Path.GetExtension(filePath).ToLower() switch
            {
                ".json" => Constants.JsonSyntax,
                ".lua" => Constants.LuaSyntax,
                ".xml" => Constants.XmlSyntax,
                _ => null
            };
            Editor.Load(filePath);
        }

    }

}