using CRUD_proj.Models;
using System.Globalization;
using System.Windows.Controls;

namespace CRUD_proj.Infrastructure.Validators
{
    class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return string.IsNullOrWhiteSpace((value ?? "").ToString())
                ? new ValidationResult(false, LocalizationManager.GetLocalizationManager().EmptyValidError)
                : ValidationResult.ValidResult;
        }
    }
}
