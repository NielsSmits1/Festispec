using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Festispec_WPF.Validation_rules
{
    class EmptyValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            bool isValid = true;

            if(value == "" || value == null)
            {
                isValid = false;
            }

            return new ValidationResult(isValid, "Dit veld is verplicht");
        }
    }
}
