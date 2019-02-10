using CRUD_proj.Models;
using System;
using System.Globalization;
using System.Windows.Data;

namespace CRUD_proj.Infrastructure.Converters
{
    class BoolDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return "-";
            else if ((bool?)value == true)
                return LocalizationManager.GetLocalizationManager().TrueText;
            else
                return LocalizationManager.GetLocalizationManager().FalseText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                string str = value.ToString();
                if (str == "-")
                    return null;
                else if (str == LocalizationManager.GetLocalizationManager().TrueText)
                    return new bool?(true);
                else
                    return new bool?(false);
            }
            return value;
        }
    }
}
