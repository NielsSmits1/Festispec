using Festispec_WPF.View.QuestionnairePages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
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
        private Page MultipleChoiceQuestionPage;
        private Page OpenQuestionPage;
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
        public ICommand Command1 { get; set; }
        public ICommand Command2 { get; set; }
             
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
            MultipleChoiceQuestionPage = new MulitpleChoiceQuestionPage();
            OpenQuestionPage = new OpenQuestionPage();
            CurrentPage = OpenQuestionPage;

            Command1 = new RelayCommand(ChangeToMultipleChoice);
            Command2 = new RelayCommand(ChangeToOpenQuestion);
        }
        private void ChangeToMultipleChoice()
        {
            CurrentPage = MultipleChoiceQuestionPage;
        }
        private void ChangeToOpenQuestion()
        {
            CurrentPage = OpenQuestionPage;
        }

        private void changeQuestionType(QuestionTypes.QuestionsTypesEnum type)
        {
            switch (type)
            {
                case QuestionTypes.QuestionsTypesEnum.OpenQuestion:
                    CurrentPage = OpenQuestionPage;
                    break;
                case QuestionTypes.QuestionsTypesEnum.MultipleChoiceQuestion:
                    CurrentPage = MultipleChoiceQuestionPage;
                    break;
                case QuestionTypes.QuestionsTypesEnum.TableQuestion:
                    break;
                case QuestionTypes.QuestionsTypesEnum.MapQuestion:
                    break;
                case QuestionTypes.QuestionsTypesEnum.AppendixQuestion:
                    break;
                default:
                    break;
            }
        }

    }
}
