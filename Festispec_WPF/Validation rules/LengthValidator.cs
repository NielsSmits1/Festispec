using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Festispec_WPF.Validation_rules
{
    public class LengthValidator : ValidationRule
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int length = (int)value.ToString().Length;
            bool validated = true;
            string message = "";

            if(length < Min)
            {
                message = "Dit veld moet minimaal " + Min + " karakters lang zijn";
                validated = false;
            }
            else if (length > Max)
            {
                message = "Dit veld mag maximaal " + Max + " karakters lang zijn";
                validated = false;
            }

            return new ValidationResult(validated, message);
        }
    }
}
