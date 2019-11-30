using Festispec_WPF.Model.UnitOfWork;
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
using System.Windows.Forms;
using System.Windows.Input;

namespace Festispec_WPF.ViewModel.QuestionnaireFolder
{
    public class CreateQuestionaireVM : ViewModelBase
    {
        private QuestionnaireVM _newQuestionnaireVM;
        private UnitOfWork UOW;
        public ICommand SubmitCommand { get; set; }
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
            UOW = new ViewModelLocator().UOW;
            newQuestionnaireVM = new QuestionnaireVM();
            CurrentPage = new OpenQuestionPage();
            Messenger.Default.Register<IQuestion>(this, (newQuestion) =>
            {
                newQuestionnaireVM.questions.Add(newQuestion);
                newQuestion.Position = newQuestionnaireVM.questions.IndexOf(newQuestion);
            });
            SubmitCommand = new RelayCommand(SubmitQuestionnaire);
        }

        private void SubmitQuestionnaire()
        {
            newQuestionnaireVM.IsFilled = false;
            UOW.Questionnaires.Add(newQuestionnaireVM.ToModel());
            try
            {
                UOW.Complete();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            foreach (IQuestion question in newQuestionnaireVM.questions)
            {
                question.Position = newQuestionnaireVM.questions.IndexOf(question);
                question.toDatabase(newQuestionnaireVM.ID);
            }



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
