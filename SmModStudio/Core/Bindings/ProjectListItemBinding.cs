using System;
using System.Drawing;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SmModStudio.Core.Bindings
{

    public class ProjectListItemBinding : FileSystemInfo
    {

        private FileSystemInfo _info;
        
        public ProjectListItemBinding(FileSystemInfo info)
        {
            _info = info;
            Name = info.Name;
            FullName = info.FullName;
            try
            {
                if (info.Attributes.HasFlag(FileAttributes.Directory))
                {
                    Image = Utilities.CreateBitmapFromUri(new Uri("pack://application:,,,/SmModStudio;component/Resources/Assets/Folder.png"));
                }
                else
                {
                    Image = Icon.ExtractAssociatedIcon(info.FullName).ToImageSource();
                }
            }
            catch
            {
                Image = Utilities.CreateBitmapFromUri(new Uri("pack://application:,,,/SmModStudio;component/Resources/Assets/Unknown.png"));
            }
        }
        
        public override string Name { get; }
        
        public override string FullName { get; }
        
        public ImageSource Image { get; }
        
        public override bool Exists => _info.Exists;

        public override void Delete()
        {
            _info.Delete();
        }

    }

}