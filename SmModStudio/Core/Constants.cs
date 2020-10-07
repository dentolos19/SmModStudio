using System;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SmModStudio.Core
{

    public static class Constants
    {

        public const uint GameId = 387990;

        public static readonly string UsersDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Axolot Games", "Scrap Mechanic", "User");

        public static readonly BitmapImage FolderIcon = (BitmapImage)Application.Current.Resources["ImgFolder"];
        public static readonly BitmapImage ModFolderIcon = (BitmapImage)Application.Current.Resources["ImgModFolder"];
        public static readonly BitmapImage WrittenFileIcon = (BitmapImage)Application.Current.Resources["ImgWrittenFile"];

    }

}