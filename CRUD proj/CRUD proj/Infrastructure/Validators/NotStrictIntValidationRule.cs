﻿using CRUD_proj.Models;
using System.Globalization;
using System.Windows.Controls;

namespace CRUD_proj.Infrastructure.Validators
{
    class NotStrictIntValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                string str = value.ToString();
                if (str == string.Empty || (int.TryParse(str, out int number) && number >= 0))
                    return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, LocalizationManager.GetLocalizationManager().WrongValueError);
        }
    }
}
