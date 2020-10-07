using System;
using System.Windows.Media.Imaging;

namespace SmModStudio.Graphics
{

    public partial class PgImagePreviewer
    {

        #region Methods

        public PgImagePreviewer(string path)
        {
            InitializeComponent();
            Previewer.Source = BitmapFrame.Create(new Uri(path));
        }

        #endregion

    }

}