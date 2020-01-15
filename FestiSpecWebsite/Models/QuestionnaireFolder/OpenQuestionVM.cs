using FestiSpec.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FestiSpecWebsite.Models.QuestionnaireFolder
{
    public class OpenQuestionVM : IQuestion
    {
        private Openvraag openQuestion;
        public string Question { get => openQuestion.Vraag; set { openQuestion.Vraag = value; } }
        [Required(ErrorMessage = "Input is required.")]
        public string Answer { get; set; }
     
        public string Type => "OpenQuestion";

        public int Id { get => openQuestion.ID; set=> openQuestion.ID = value; }
        public int ListPosition { get; set; }

        public OpenQuestionVM(Openvraag openQuestion)
        {
            this.openQuestion = openQuestion;
        }
        public OpenQuestionVM()
        {
            openQuestion = new Openvraag(); 
        }
    }
}