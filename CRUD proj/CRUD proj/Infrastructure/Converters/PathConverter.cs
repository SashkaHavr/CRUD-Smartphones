using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace CRUD_proj.Infrastructure.Converters
{
    class PathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string path = value.ToString();
                if (Path.IsPathRooted(path) && File.Exists(path))
                    return path.Replace(Environment.CurrentDirectory + '\\', string.Empty);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string path = value.ToString();
                if (!Path.IsPathRooted(path) && File.Exists(Path.Combine(Environment.CurrentDirectory, path)))
                    return Path.Combine(Environment.CurrentDirectory, path);
            }
            return value;
        }
    }
}
