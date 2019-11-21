using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.ViewModel.QuestionnaireFolder
{
    public class AddQuestionsClass
    {
        
        public AddQuestionsClass(List<IQuestion> questions)
        {
            
        }
        public Meerkeuzevraag AddQuestion(Meerkeuzevraag multipleChoiceQuestion)
        {
            return multipleChoiceQuestion;
        }
        public Openvraag AddQuestion(Openvraag OpenQuestion)
        {
            return OpenQuestion;
        }
    }
}
