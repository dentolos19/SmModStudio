using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace SmModStudio.Graphics
{

    public partial class PgImagePreviewer
    {

        #region Methods

        public PgImagePreviewer(string imagePath)
        {
            InitializeComponent();
            Previewer.Source = BitmapFrame.Create(new Uri(imagePath));
            Title = Path.GetFileName(imagePath);
        }

        #endregion

    }

}