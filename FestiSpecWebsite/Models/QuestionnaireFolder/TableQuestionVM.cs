using FestiSpec.Domain.Model;
using System.Collections.Generic;
using FestiSpecWebsite.Models.QuestionnaireFolder.Validation;

namespace FestiSpecWebsite.Models.QuestionnaireFolder
{
    public class TableQuestionVM : IQuestion
    {
        private Tabelvraag tableQuestion;

        public TableQuestionVM(Tabelvraag tableQuestion)
        {
            Situations = new List<string>();
            Answers = new List<string>();
            populateList(Situations);
            populateList(Answers);
            this.tableQuestion = tableQuestion;
        }
        public TableQuestionVM()
        {
            Situations = new List<string>();
            Answers = new List<string>();
            tableQuestion = new Tabelvraag();
        }


        public string Type => "TableQuestion";

        public int Id { get => tableQuestion.ID; set => tableQuestion.ID = value; }
        public int ListPosition { get; set; }
        public string Question { get => tableQuestion.Vraag; set => tableQuestion.Vraag = value; }
        public string QuestionHead { get => tableQuestion.VraagKop; set => tableQuestion.VraagKop = value; }
        public string AnswerHead { get => tableQuestion.AntwoordKop; set => tableQuestion.AntwoordKop = value; }
        
        [ValidateQuestionSituations(ErrorMessage = "Situatie mag maar een keer voorkomen.")]
        [validateTableInputNull(ErrorMessage = "Situatie mag niet null zijn")]
        [ValidateTableInputLength(ErrorMessage ="Situatie mag maar 50 characters lang zijn")]
        public List<string> Situations { get; set; }
        [validateTableInputNull(ErrorMessage = "Antwoord mag niet leeg zijn")]
        [ValidateTableInputLength(ErrorMessage = "Antwoord mag maar 50 characters lang zijn")]
        public List<string> Answers { get; set; }
        private void populateList(List<string> l)
        {
            for(int i = 0; i < 4; i++)
            {
                l.Add("");
            }
        }
    }
}