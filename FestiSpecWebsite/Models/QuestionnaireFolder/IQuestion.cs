using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using System.Web.Mvc;

namespace FestiSpecWebsite.Models.QuestionnaireFolder
{
    
    public interface IQuestion
    {
        int Id { get; set; }
        int ListPosition { get; set; }
        string Question { get; set; }
        string Type { get; }
    }
}
