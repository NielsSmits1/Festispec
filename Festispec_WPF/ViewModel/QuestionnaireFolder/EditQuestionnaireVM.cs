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
    public class EditQuestionnaireVM: ViewModelBase
    {
        private UnitOfWork UOW;
        private List<IQuestion> newQuestions;
        private List<IQuestion> deleteQuestions;
        private QuestionnaireVM _editetQuestionnaireVM;
        public ICommand PositionUpCommand { get; set; }
        public ICommand PositionDownCommand { get; set; }
        public ICommand DeleteQuestionCommand { get; set; }
        public ICommand SubmitCommand { get; set; }
        public IQuestion SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
            }
        }
        public QuestionnaireVM EditetQuestionnaireVM
        {
            get { return _editetQuestionnaireVM; }
            set
            {
                _editetQuestionnaireVM = value;
                RaisePropertyChanged("EditetQuestionnaireVM");
            }
        }
        public Dictionary<QuestionTypes.QuestionsTypesEnum, string> _QuestionTypes
        {
            get { return QuestionTypes.QuestionTypeDictionary; }
        }
        private QuestionTypes.QuestionsTypesEnum _SelectedQuestionType;
        private IQuestion _selectedItem;
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
        private Page _currentPage;
       

        public Page CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                RaisePropertyChanged(() => CurrentPage);
            }
        }
        public EditQuestionnaireVM()
        {
            UOW = ViewModelLocator.UOW;
            newQuestions = new List<IQuestion>();
            deleteQuestions = new List<IQuestion>();
            CurrentPage = new OpenQuestionPage();
            Messenger.Default.Register<QuestionnaireVM>(this, (Questionnaire) =>
            {
                EditetQuestionnaireVM = Questionnaire;
                EditetQuestionnaireVM.loadQuestions();
                foreach(IQuestion q in EditetQuestionnaireVM.questions)
                {
                    q.Position = EditetQuestionnaireVM.questions.IndexOf(q);
                }
            });
            Messenger.Default.Register<IQuestion>(this, (newQuestion) =>
            {
                EditetQuestionnaireVM.questions.Add(newQuestion);
                newQuestion.Position = EditetQuestionnaireVM.questions.IndexOf(newQuestion);
                newQuestions.Add(newQuestion);
            });
            PositionUpCommand = new RelayCommand(changePositionUP);
            PositionDownCommand = new RelayCommand(changePositionDOWN);
            DeleteQuestionCommand = new RelayCommand(DeleteQuestion);
            SubmitCommand = new RelayCommand(Submit);
        }

        private void Submit()
        {
            var updateQuestionnaire = UOW.Context.Vragenlijst.FirstOrDefault(q => q.ID == EditetQuestionnaireVM.ID);
            updateQuestionnaire = EditetQuestionnaireVM.questionnaireData;
            foreach(IQuestion q in newQuestions)
            {
                EditetQuestionnaireVM.questions.Remove(q);
                q.toDatabase(EditetQuestionnaireVM.ID);
            }
            foreach(IQuestion q in deleteQuestions)
            {
                q.deleteConnection(EditetQuestionnaireVM.ID);
            }
            foreach(IQuestion q in EditetQuestionnaireVM.questions)
            {
                q.updateLink(EditetQuestionnaireVM.ID);
            }
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
        private void changePositionUP()
        {
            changePosition("UP");
        }

        private void changePositionDOWN()
        {
            changePosition("DOWN");
        }

        private void changePosition(string upordown)
        {

            int newPosition = -1;
            int currentPosition = SelectedItem.Position;

            if (upordown == "DOWN")
            {
                newPosition = SelectedItem.Position + 1;
            }
            if (upordown == "UP")
            {
                newPosition = SelectedItem.Position - 1;
            }


            if (newPosition >= 0 && newPosition < EditetQuestionnaireVM.questions.Count)
            {
                IQuestion newposQuestion = EditetQuestionnaireVM.questions[newPosition];
                IQuestion oldposQuestion = SelectedItem;

                EditetQuestionnaireVM.questions[SelectedItem.Position] = newposQuestion;
                EditetQuestionnaireVM.questions[newPosition] = oldposQuestion;

                newposQuestion.Position = EditetQuestionnaireVM.questions.IndexOf(newposQuestion);
                oldposQuestion.Position = EditetQuestionnaireVM.questions.IndexOf(oldposQuestion);

            }
        }

        private void DeleteQuestion()
        {
            int selecteditemPos = SelectedItem.Position;
            if (!newQuestions.Contains(SelectedItem))
            {
                deleteQuestions.Add(SelectedItem);
            }
            else
            {
                newQuestions.Remove(SelectedItem);
            }
            EditetQuestionnaireVM.questions.Remove(SelectedItem);

            for (int i = selecteditemPos; i < EditetQuestionnaireVM.questions.Count; i++)
            {
                EditetQuestionnaireVM.questions[i].Position = EditetQuestionnaireVM.questions.IndexOf(EditetQuestionnaireVM.questions[i]);
            }
        }
    }
}
