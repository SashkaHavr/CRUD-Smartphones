using CRUD_proj.Models;
using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Controls;

namespace CRUD_proj.Infrastructure.Validators
{
    class PictureSourceValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value != null)
            {
                try
                {
                    string path = value.ToString();
                    string ext = Path.GetExtension(path);
                    if (ext == ".png" || ext == ".jpg" || ext == ".bmp" || ext == ".gif")
                        if (Uri.IsWellFormedUriString(path, UriKind.Absolute))
                        {
                            WebRequest webRequest = WebRequest.Create(path);
                            webRequest.GetResponse();
                            return ValidationResult.ValidResult;
                        }
                        else if (Path.IsPathRooted(path) && File.Exists(path))
                            return ValidationResult.ValidResult;
                        else if (!Path.IsPathRooted(path) && File.Exists(Path.Combine(Environment.CurrentDirectory, path)))
                            return ValidationResult.ValidResult;
                }
                catch { return new ValidationResult(false, LocalizationManager.GetLocalizationManager().LinkError); }
            }
            return new ValidationResult(false, LocalizationManager.GetLocalizationManager().LinkError);
        }
    }
}
