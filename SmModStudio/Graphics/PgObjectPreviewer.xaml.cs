using System.IO;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace SmModStudio.Graphics
{

    public partial class PgObjectPreviewer
    {

        public PgObjectPreviewer()
        {
            InitializeComponent();
        }

        public void SetPreview(string path)
        {
            Model3DGroup model = null;
            switch (Path.GetExtension(path).ToLower())
            {
                case ".3ds":
                    var reader1 = new StudioReader();
                    model = reader1.Read(path);
                    break;
                case ".lwo":
                    var reader2 = new LwoReader();
                    model = reader2.Read(path);
                    break;
                case ".off":
                    var reader3 = new OffReader();
                    model = reader3.Read(path);
                    break;
                case ".obj":
                    var reader4 = new ObjReader();
                    model = reader4.Read(path);
                    break;
                case ".stl":
                    var reader5 = new StLReader();
                    model = reader5.Read(path);
                    break;
            }
            if (model == null)
                return;
            Preview.Children.Clear();
            var visual = new ModelVisual3D();
            Preview.Children.Add(new DefaultLights());
            visual.Content = model;
            Preview.Children.Add(visual);
        }

    }

}