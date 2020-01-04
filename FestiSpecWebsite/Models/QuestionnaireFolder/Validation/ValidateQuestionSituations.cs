using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FestiSpecWebsite.Models.QuestionnaireFolder.Validation
{
    public class ValidateQuestionSituations: RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            List<string> i = value as List<string>;
            if( i.GroupBy(n=> n).Any( c => c.Count()> 1))
            {
                return false;
            }
            return true;
        }
    }
}
