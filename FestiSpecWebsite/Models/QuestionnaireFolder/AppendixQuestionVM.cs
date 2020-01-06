using FestiSpec.Domain.Model;
using FestiSpecWebsite.Models.QuestionnaireFolder.Validation;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace FestiSpecWebsite.Models.QuestionnaireFolder
{
    public class AppendixQuestionVM : IQuestion
    {
        private Bijlagevraag appendixQuestion;

        public AppendixQuestionVM(Bijlagevraag appendixQuestion)
        {
            this.appendixQuestion = appendixQuestion;
        }
        public AppendixQuestionVM()
        {
            appendixQuestion = new Bijlagevraag();
        }


        public string Type => "AppendixQuestion";

        public int Id { get => appendixQuestion.ID; set => appendixQuestion.ID = value; }
        public int ListPosition { get; set; }
        public string Question { get => appendixQuestion.Vraag; set => appendixQuestion.Vraag = value; }
        
        public string extension { get; set; }
        [Required]
        [ValidateFileType(ErrorMessage ="Antwoord moet .png file zijn.")]
        public HttpPostedFileBase imageFile{get;set;}
    }
}