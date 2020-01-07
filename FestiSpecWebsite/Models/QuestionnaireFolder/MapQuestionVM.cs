using FestiSpec.Domain.Model;
using FestiSpecWebsite.Models.QuestionnaireFolder.Validation;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace FestiSpecWebsite.Models.QuestionnaireFolder
{
    public class MapQuestionVM : IQuestion
    {
        private Kaartvraag mapQuestion;
        public MapQuestionVM(Kaartvraag mapQuestion)
        {
            this.mapQuestion = mapQuestion;
        }
        public MapQuestionVM()
        {
            mapQuestion = new Kaartvraag();
        }

        

        public string Type => "MapQuestion";

        public int Id { get => mapQuestion.ID; set => mapQuestion.ID = value; }
        public int ListPosition { get; set; }
        public string Question { get => mapQuestion.Vraag; set => mapQuestion.Vraag = value; }
        public byte[] ByteArray { get => mapQuestion.FileBytes; set => mapQuestion.FileBytes = value; }
        public string imageData { get; set; }
        public string note { get; set; }
        
        public string extension { get; set; }
        [Required]
        [ValidateFileType(ErrorMessage = "Antwoord moet .png file zijn.")]
        public HttpPostedFileBase imageFile { get; set; }
    }
}