using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.View.QuestionnairePages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private IQuestion _selectedItem;
        public ObservableCollection<QuestionnaireVM> templates { get; set; }
        private QuestionnaireVM _selectedTemplate;
        private bool basedOfTemplate = false;
        public QuestionnaireVM selectedTemplate
        {
            get
            {
                return _selectedTemplate;
            }
            set
            {
                _selectedTemplate = value;

                if (value != null)
                {
                    _selectedTemplate.questions = new ObservableCollection<IQuestion>();
                    _selectedTemplate.loadQuestions();

                    FixPostions(_selectedTemplate.questions);
                    newQuestionnaireVM.questions = _selectedTemplate.questions;
                    RaisePropertyChanged();
                    basedOfTemplate = true;
                }
                else
                {
                    basedOfTemplate = false;
                }

            }
        }

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
        public ICommand SubmitCommand { get; set; }
        public ICommand PositionUpCommand { get; set; }
        public ICommand PositionDownCommand { get; set; }

        public ICommand DeleteQuestionCommand { get; set; }

        public string TemplateType { get; set; }

        public ICommand CreateTemplateCommand { get; set; }
        public QuestionnaireVM newQuestionnaireVM
        {
            get { return _newQuestionnaireVM; }
            set
            {
                _newQuestionnaireVM = value;
                RaisePropertyChanged("newQuestionnaireVM");
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
            UOW = ViewModelLocator.UOW;
            newQuestionnaireVM = new QuestionnaireVM();
            CurrentPage = new OpenQuestionPage();
            Messenger.Default.Register<IQuestion>(this, (newQuestion) =>
            {
                newQuestionnaireVM.questions.Add(newQuestion);
                newQuestion.Position = newQuestionnaireVM.questions.IndexOf(newQuestion);
            });

            SubmitCommand = new RelayCommand(SubmitCreatedQuestionnaire);
            CreateTemplateCommand = new RelayCommand(CreateTemplate);

            PositionUpCommand = new RelayCommand(changePositionUP);
            PositionDownCommand = new RelayCommand(changePositionDOWN);

            DeleteQuestionCommand = new RelayCommand(DeleteQuestion);

            templates = new ObservableCollection<QuestionnaireVM>(UOW.Questionnaires.getTemplates().Select(tp => new QuestionnaireVM(tp)));
        }

        private void CreateTemplate()
        {
            SubmitQuestionnaire();

            Template newTemplate = new Template();
            newTemplate.Vragenlijst_ID = newQuestionnaireVM.ID;
            newTemplate.Type = TemplateType;
            UOW.Context.Template.Add(newTemplate);

            var temp = UOW.Questionnaires.Get(newQuestionnaireVM.ID);
            temp.Template_ID = newTemplate.ID;

            saveToDatabase();
            newQuestionnaireVM = new QuestionnaireVM();
            TemplateType = null;
            RaisePropertyChanged("TemplateType");
            templates = new ObservableCollection<QuestionnaireVM>(UOW.Questionnaires.getTemplates().Select(tp => new QuestionnaireVM(tp)));
            RaisePropertyChanged("templates");

            if (selectedTemplate != null)
            {
                clearSelectedTemplate(selectedTemplate);
            }
        }
        private void SubmitQuestionnaire()
        {
            newQuestionnaireVM.IsFilled = false;
            newQuestionnaireVM.IsActive = true;
            UOW.Questionnaires.Add(newQuestionnaireVM.ToModel());

            saveToDatabase();

            if (basedOfTemplate)
            {
                FixPostions(newQuestionnaireVM.questions);

                var deletelist = new ObservableCollection<IQuestion>();
                foreach (IQuestion question in newQuestionnaireVM.questions)
                {
                    if (question.ID != 0)
                    {
                        deletelist.Add(question);
                    }
                }
                foreach (var q in deletelist)
                {
                    newQuestionnaireVM.questions.Remove(q);
                    q.addNewLink(newQuestionnaireVM.ID);
                }
            }

            foreach (IQuestion question in newQuestionnaireVM.questions)
            {
                if (!basedOfTemplate)
                {
                    question.Position = newQuestionnaireVM.questions.IndexOf(question);
                }

                question.toDatabase(newQuestionnaireVM.ID);
            }

            if (selectedTemplate != null)
            {
                UOW.Context.Vragenlijst.Find(_newQuestionnaireVM.ID).Stamt_af_van_ID = UOW.Context.Template.Where(t => t.Vragenlijst_ID == selectedTemplate.ID).Select(t => t.ID).FirstOrDefault();
                
                clearSelectedTemplate(selectedTemplate);

                saveToDatabase();

            }

        }

        private void SubmitCreatedQuestionnaire()
        {
            SubmitQuestionnaire();
            newQuestionnaireVM = new QuestionnaireVM();
            basedOfTemplate = false;
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


            if (newPosition >= 0 && newPosition < _newQuestionnaireVM.questions.Count)
            {
                IQuestion newposQuestion = _newQuestionnaireVM.questions[newPosition];
                IQuestion oldposQuestion = SelectedItem;

                _newQuestionnaireVM.questions[SelectedItem.Position] = newposQuestion;
                _newQuestionnaireVM.questions[newPosition] = oldposQuestion;

                newposQuestion.Position = _newQuestionnaireVM.questions.IndexOf(newposQuestion);
                oldposQuestion.Position = _newQuestionnaireVM.questions.IndexOf(oldposQuestion);

            }
        }

        private void DeleteQuestion()
        {
            int selecteditemPos = SelectedItem.Position;
            _newQuestionnaireVM.questions.Remove(SelectedItem);

            for (int i = selecteditemPos; i < _newQuestionnaireVM.questions.Count; i++)
            {
                _newQuestionnaireVM.questions[i].Position = _newQuestionnaireVM.questions.IndexOf(_newQuestionnaireVM.questions[i]);
            }
        }

        private void FixPostions(ObservableCollection<IQuestion> _questions)
        {
            foreach (var q in _questions)
            {
                q.Position = _questions.IndexOf(q);
            }
        }

        private void clearSelectedTemplate(QuestionnaireVM template)
        {
            template = null;
        }

        private void saveToDatabase()
        {
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
    }
}
