using System;
using System.Globalization;
using System.Windows.Data;

namespace CRUD_proj.Infrastructure.Converters
{
    class EmptyValueDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "-";
            if(value != null)
            {
                string str = value.ToString();
                if (str == string.Empty || str=="0")
                    return "-";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string str = value.ToString();
                if (str == "-")
                    return string.Empty;
            }
            return value;
        }
    }
}
