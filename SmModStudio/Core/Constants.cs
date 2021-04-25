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

        public static readonly BitmapImage ImgFolder = (BitmapImage)Application.Current.Resources["ImgFolder"];
        public static readonly BitmapImage ImgModFolder = (BitmapImage)Application.Current.Resources["ImgModFolder"];
        public static readonly BitmapImage ImgWrittenFile = (BitmapImage)Application.Current.Resources["ImgWrittenFile"];
        
        public static readonly string TxtDialogTitle = (string)Application.Current.Resources["TxtDialogTitle"];
        public static readonly string TxtDialogMsg0 = (string)Application.Current.Resources["TxtDialogMsg0"];
        public static readonly string TxtDialogMsg1 = (string)Application.Current.Resources["TxtDialogMsg1"];
        public static readonly string TxtDialogMsg2 = (string)Application.Current.Resources["TxtDialogMsg2"];
        public static readonly string TxtDialogMsg3 = (string)Application.Current.Resources["TxtDialogMsg3"];
        public static readonly string TxtDialogMsg4 = (string)Application.Current.Resources["TxtDialogMsg4"];
        public static readonly string TxtDialogMsg5 = (string)Application.Current.Resources["TxtDialogMsg5"];
        public static readonly string TxtDialogMsg6 = (string)Application.Current.Resources["TxtDialogMsg6"];
        public static readonly string TxtDialogMsg7 = (string)Application.Current.Resources["TxtDialogMsg7"];
        public static readonly string TxtDialogMsg8 = (string)Application.Current.Resources["TxtDialogMsg8"];
        public static readonly string TxtDialogMsg9 = (string)Application.Current.Resources["TxtDialogMsg9"];

        public static readonly string TxtOtherMsg0 = (string)Application.Current.Resources["TxtOtherMsg0"];
        public static readonly string TxtOtherMsg1 = (string)Application.Current.Resources["TxtOtherMsg1"];

    }

}