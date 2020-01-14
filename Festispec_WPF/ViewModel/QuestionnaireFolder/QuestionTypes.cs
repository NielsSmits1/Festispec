using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.ViewModel.QuestionnaireFolder
{
    public static class QuestionTypes
    {
        public enum QuestionsTypesEnum
        {
            OpenQuestion,
            MultipleChoiceQuestion,
            TableQuestion,
            MapQuestion,
            AppendixQuestion
        }
        public static Dictionary<QuestionsTypesEnum, string> QuestionTypeDictionary = new Dictionary<QuestionsTypesEnum, string>()
        {
            {QuestionsTypesEnum.OpenQuestion, "Open vraag"},
            {QuestionsTypesEnum.MultipleChoiceQuestion, "Meerkeuze vraag"},
            {QuestionsTypesEnum.TableQuestion, "Tabel vraag"},
            {QuestionsTypesEnum.MapQuestion, "Kaart vraag"},
            {QuestionsTypesEnum.AppendixQuestion, "Bijlage vraag"},
        };
    }
}
