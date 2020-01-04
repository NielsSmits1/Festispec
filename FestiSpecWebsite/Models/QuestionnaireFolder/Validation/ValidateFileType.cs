using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace FestiSpecWebsite.Models.QuestionnaireFolder.Validation
{
    public class ValidateFileType: RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            HttpPostedFileBase i = value as HttpPostedFileBase;
            string a = Path.GetExtension(i.FileName);
            if (a != ".png")
            {
                return false;
            }
            return true;
        }
    }
}