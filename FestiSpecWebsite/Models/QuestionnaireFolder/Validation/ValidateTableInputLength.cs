using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FestiSpecWebsite.Models.QuestionnaireFolder.Validation
{
    public class ValidateTableInputLength: RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            List<string> i = value as List<string>;
            foreach (string s in i)
            {
                if (s.Length> 50)
                {
                    return false;
                }
            }
            return true;
        }
    }
}