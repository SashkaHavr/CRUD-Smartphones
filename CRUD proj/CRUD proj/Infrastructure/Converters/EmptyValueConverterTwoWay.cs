using System;
using System.Globalization;
using System.Windows.Data;

namespace CRUD_proj.Infrastructure.Converters
{
    class EmptyValueConverterTwoWay : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string str = value.ToString();
                if (str == "0")
                    return string.Empty;
                return str;
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string str = value.ToString();
                if (str == string.Empty)
                    return 0;
                return str;
            }
            return 0;
        }
    }
}
