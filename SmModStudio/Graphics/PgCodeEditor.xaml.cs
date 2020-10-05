namespace SmModStudio.Graphics
{

    public partial class PgCodeEditor
    {

        public PgCodeEditor(string path)
        {
            InitializeComponent();
            Editor.Load(path);
        }

    }

}