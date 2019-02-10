using CRUD_proj.Models;
using System.Globalization;
using System.Windows.Controls;

namespace CRUD_proj.Infrastructure.Validators
{
    class DoubleValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null && double.TryParse(value.ToString(), out double number))
                if (number > 0)
                    return ValidationResult.ValidResult;
            return new ValidationResult(false, LocalizationManager.GetLocalizationManager().WrongValueError);
        }
    }
}
