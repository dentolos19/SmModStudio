using System;
using System.Windows.Media.Imaging;

namespace SmModStudio.Graphics
{

    public partial class PgImagePreviewer
    {

        public PgImagePreviewer()
        {
            InitializeComponent();
        }

        public void SetPreview(string path)
        {
            ImageFrame.Source = BitmapFrame.Create(new Uri(path));
        }

    }

}