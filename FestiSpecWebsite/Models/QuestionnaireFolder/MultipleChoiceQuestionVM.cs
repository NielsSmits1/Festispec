using FestiSpec.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FestiSpecWebsite.Models.QuestionnaireFolder
{
    public class MultipleChoiceQuestionVM : IQuestion
    {
        private Meerkeuzevraag multiplechoiceQuestion;

        public MultipleChoiceQuestionVM(Meerkeuzevraag multiplechoiceQuestion)
        {
            this.multiplechoiceQuestion = multiplechoiceQuestion;
            answers = new List<string>();
            foreach( var answer in multiplechoiceQuestion.Meerkeuzevraag_antwoord)
            {
                answers.Add(answer.Antwoord);
            }
        }
        public MultipleChoiceQuestionVM()
        {
            multiplechoiceQuestion = new Meerkeuzevraag();

        }
        public string Type => "MultipleChoiceQuestion";

        public List<string> answers { get; set; }
        [Required]
        public string Answer { get; set; }
        public string Question { get => multiplechoiceQuestion.Vraag; set => multiplechoiceQuestion.Vraag = value; }
        public int Id { get => multiplechoiceQuestion.ID; set => multiplechoiceQuestion.ID = value; }
        public int ListPosition { get; set; }
    }
}