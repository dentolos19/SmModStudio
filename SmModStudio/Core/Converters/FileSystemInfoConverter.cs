using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using SmModStudio.Core.Bindings;

namespace SmModStudio.Core.Converters
{

    public class FileSystemInfoConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is DirectoryInfo info))
                return null;
            var list = new List<ProjectListItemBinding>();
            foreach (var item in info.GetFileSystemInfos())
                list.Add(new ProjectListItemBinding(item));
            return list.ToArray();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

    }

}