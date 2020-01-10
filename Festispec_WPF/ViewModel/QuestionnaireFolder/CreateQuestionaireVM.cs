using FestiSpec.Domain.Model;
using FestiSpec.Domain.Model.Repositories;
using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.View;
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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using Application = System.Windows.Application;
using MessageBox = System.Windows.Forms.MessageBox;

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
        private bool Template = false;
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

        private string _questionVisibility;

        public string QuestionVisibility
        {
            get { return _questionVisibility; }
            set
            {
                _questionVisibility = value;
                base.RaisePropertyChanged();
            }
        }

        private string _questionaireVisibility;

        public string QuestionaireVisibility
        {
            get { return _questionaireVisibility; }
            set
            {
                _questionaireVisibility = value;
                base.RaisePropertyChanged();
            }
        }

        private string _orderVisibility;

        public string OrderVisibility
        {
            get { return _orderVisibility; }
            set
            {
                _orderVisibility = value;
                base.RaisePropertyChanged();
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
        public ICommand DeleteTemplateCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand ShowQuestionnaire { get; set; }
        public ICommand ShowQuestions { get; set; }
        public ICommand ShowOrder { get; set; }

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
            DeleteTemplateCommand = new RelayCommand(DeleteTemplate);
            ShowQuestionnaire = new RelayCommand(showQuestionnaire);
            ShowQuestions = new RelayCommand(showQuestions);
            ShowOrder = new RelayCommand(showOrder);
            CancelCommand = new RelayCommand(cancelQuestionnaire);

            templates = new ObservableCollection<QuestionnaireVM>(UOW.Questionnaires.getTemplates().Select(tp => new QuestionnaireVM(tp)));

            showQuestionnaire();
        }

        private void cancelQuestionnaire()
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var _questionnaireView = new QuestionnaireCRUD();
            _questionnaireView.Show();
            currentWindow.Close();
        }

        public void showQuestionnaire()
        {
            QuestionVisibility = "Hidden";
            QuestionaireVisibility = "Visible";
            OrderVisibility = "Hidden";
        }

        public void showQuestions()
        {
            QuestionVisibility = "Visible";
            QuestionaireVisibility = "Hidden";
            OrderVisibility = "Hidden";
        }

        public void showOrder()
        {
            QuestionVisibility = "Hidden";
            QuestionaireVisibility = "Hidden";
            OrderVisibility = "Visible";
        }

        private void CreateTemplate()
        {
            Template = true;
            SubmitQuestionnaire();

            Template newTemplate = new Template();
            newTemplate.Vragenlijst_ID = newQuestionnaireVM.ID;
            newTemplate.Type = TemplateType;
            newTemplate.Actief = true;

            if (newTemplate.Type != null)
            {

                UOW.Context.Template.Add(newTemplate);

                var temp = UOW.Questionnaires.Get(newQuestionnaireVM.ID);

                try
                {
                    temp.Template_ID = newTemplate.ID;
                }
                catch
                {
                    return;
                }

                saveToDatabase();

                TemplateType = null;
                RaisePropertyChanged("TemplateType");
                templates = new ObservableCollection<QuestionnaireVM>(UOW.Questionnaires.getTemplates().Select(tp => new QuestionnaireVM(tp)));
                RaisePropertyChanged("templates");

                if (selectedTemplate != null)
                {
                    clearSelectedTemplate(selectedTemplate);
                }
            }
            else
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            newQuestionnaireVM = new QuestionnaireVM();
            Template = false;
        }
        private void SubmitQuestionnaire()
        {
            if (selectedTemplate == null)
            {
                basedOfTemplate = false;
            }
            else
            {
                basedOfTemplate = true;
            }

            if (newQuestionnaireVM.Title != null & newQuestionnaireVM.Version != null)
            {

                newQuestionnaireVM.IsFilled = false;
                if (Template)
                {
                    newQuestionnaireVM.IsActive = false;
                }
                else
                {
                    newQuestionnaireVM.IsActive = true;
                }
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

                if (basedOfTemplate)
                {
                    UOW.Context.Vragenlijst.Find(_newQuestionnaireVM.ID).Stamt_af_van_ID = UOW.Context.Template.Where(t => t.Vragenlijst_ID == selectedTemplate.ID).Select(t => t.ID).FirstOrDefault();

                    clearSelectedTemplate(selectedTemplate);

                    saveToDatabase();
                }
                basedOfTemplate = false;
            }
        }

        private void SubmitCreatedQuestionnaire()
        {
            try
            {
                SubmitQuestionnaire();
                newQuestionnaireVM = new QuestionnaireVM();
                basedOfTemplate = false;

                cancelQuestionnaire();
            }
            catch (Exception)
            {
                MessageBox.Show("Fout bij invoeren velden", "Er is iets fout gegaan",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            var currentWindow = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            selectedTemplate = new QuestionnaireVM();
            var newWindow = new QuestionnaireCRUD();
            currentWindow.Close();
            newWindow.Show();
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

        public void DeleteTemplate()
        {
            UOW.Context.Template.Where(temp => temp.Vragenlijst_ID == selectedTemplate.ID).FirstOrDefault().Actief = false;
            saveToDatabase();

            templates = new ObservableCollection<QuestionnaireVM>(UOW.Questionnaires.getTemplates().Select(tp => new QuestionnaireVM(tp)));
            selectedTemplate = null;
            newQuestionnaireVM = new QuestionnaireVM();
            RaisePropertyChanged("templates");
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
                return;
            }
        }

    }
}