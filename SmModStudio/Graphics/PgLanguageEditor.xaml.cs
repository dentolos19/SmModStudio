using System.IO;
using SmModStudio.Core;

namespace SmModStudio.Graphics
{

    public partial class PgLanguageEditor
    {

        private readonly string _currentFilePath;

        public PgLanguageEditor(string filePath)
        {
            InitializeComponent();
            _currentFilePath = filePath;
            var descriptions = Utilities.LoadInventoryDescriptions(filePath);
            // TODO
            Title = Path.GetFileName(_currentFilePath);
        }

    }

}