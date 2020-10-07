using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace SmModStudio.Core
{

    public static class Constants
    {

        public const uint GameId = 387990;

        public static readonly string UsersDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Axolot Games", "Scrap Mechanic", "User");

        public static readonly BitmapImage FolderIcon = new BitmapImage(new Uri("pack://application:,,,/SmModStudio;component/Resources/Assets/Folder.png"));
        public static readonly BitmapImage FileIcon = new BitmapImage(new Uri("pack://application:,,,/SmModStudio;component/Resources/Assets/File.png"));
        public static readonly BitmapImage ModIcon = new BitmapImage(new Uri("pack://application:,,,/SmModStudio;component/Resources/Assets/Mod.png"));

    }

}