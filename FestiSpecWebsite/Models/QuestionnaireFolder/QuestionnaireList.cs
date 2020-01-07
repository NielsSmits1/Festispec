using FestiSpec.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FestiSpecWebsite.Models.QuestionnaireFolder
{
    public class QuestionnaireList
    {
        public List<Vragenlijst> questionnaires { get; set; }
        public int? id { get; set; }
        public QuestionnaireList()
        {
        }
    }
}