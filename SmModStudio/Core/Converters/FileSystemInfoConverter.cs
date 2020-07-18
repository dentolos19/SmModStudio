using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace SmModStudio.Core.Converters
{

    public class FileSystemInfoConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DirectoryInfo info)
                return info.GetFileSystemInfos();
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }

}