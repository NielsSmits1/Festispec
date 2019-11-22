using Festispec_WPF.View.QuestionnairePages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Festispec_WPF.ViewModel.QuestionnaireFolder
{
    public class CreateQuestionaireVM : ViewModelBase
    {
        private QuestionnaireVM _newQuestionnaireVM;
        public QuestionnaireVM newQuestionnaireVM 
        {
            get { return _newQuestionnaireVM; }
            set { _newQuestionnaireVM = value;
                
            }
        }
        private Page _currentPage;
        public Dictionary<QuestionTypes.QuestionsTypesEnum, string> _QuestionTypes
        {
            get { return QuestionTypes.QuestionTypeDictionary; }
        }
        private QuestionTypes.QuestionsTypesEnum _SelectedQuestionType;
        public QuestionTypes.QuestionsTypesEnum SelectedQuestionType 
        {
            get
            {
                return _SelectedQuestionType;
            }
            set
            {
                _SelectedQuestionType = value;
                changeQuestionType(_SelectedQuestionType);
            }
        }

             
        public Page CurrentPage
        {
            get { return _currentPage; }
            set 
            {
                _currentPage = value; 
                RaisePropertyChanged(() => CurrentPage); 
            }
        }

        public CreateQuestionaireVM()
        {
            newQuestionnaireVM = new QuestionnaireVM();
            CurrentPage = new OpenQuestionPage();
            Messenger.Default.Register<IQuestion>(this, (newQuestion) =>
            {
                newQuestionnaireVM.questions.Add(newQuestion);
            });
        }


        private void changeQuestionType(QuestionTypes.QuestionsTypesEnum type)
        {
            switch (type)
            {
                case QuestionTypes.QuestionsTypesEnum.OpenQuestion:
                    
                    CurrentPage = new OpenQuestionPage();
                    
                    break;
                case QuestionTypes.QuestionsTypesEnum.MultipleChoiceQuestion:
                    CurrentPage = new MulitpleChoiceQuestionPage();
                    break;
                case QuestionTypes.QuestionsTypesEnum.TableQuestion:
                    CurrentPage = new TableQuestionPage();
                    break;
                case QuestionTypes.QuestionsTypesEnum.MapQuestion:
                    CurrentPage = new MapQuestionPage();
                    break;
                case QuestionTypes.QuestionsTypesEnum.AppendixQuestion:
                    CurrentPage = new AppendixQuestionPage();
                    break;
                default:
                    break;
            }
        }

    }
}
