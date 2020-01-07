using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Festispec_WPF.Validation_rules
{
    public class NumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            double result = 0.0;
            bool canConvert = double.TryParse(value as string, out result);
            return new ValidationResult(canConvert, "Dit is geen geldig getal");
        }
    }
}
