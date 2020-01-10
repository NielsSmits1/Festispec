using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Festispec_WPF.Validation_rules
{
    class PostalcodeValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool isValid = true;
            Regex regex = new Regex(@"^\d{4}?\w{2}$");
            isValid = regex.IsMatch((string)value);

            return new ValidationResult(isValid, "Dit is geen geldige postcode");
        }
    }
}
