using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.View;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Forms;

namespace Festispec_WPF.ViewModel
{
    public class QuestionaireCrudVM : ViewModelBase
    {
        public ObservableCollection<QuestionnaireVM> Questionnaires { get; set; }
        private UnitOfWork UOW;
        public QuestionnaireVM SelectedQuestionnaire { get; set; }
        public ICommand OpenCreateQuestionnaireWindowCommand { get; set; }
        public ICommand OpenEditQuestionnaireCommand { get; set; }
        public ICommand DeleteQuestionnaireCommand { get; set; }
        public ICommand SearchDataGrid { get; set; }

        private string _searchText;

        public string searchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                base.RaisePropertyChanged();
            }
        }
        public QuestionaireCrudVM()
        {
            OpenCreateQuestionnaireWindowCommand = new RelayCommand(OpenCreateQuestionnaireWindow);
            OpenEditQuestionnaireCommand = new RelayCommand(OpenEditQuestionnaireWindow);
            DeleteQuestionnaireCommand = new RelayCommand(DeleteQuestionnaire);
            SearchDataGrid = new RelayCommand(searchDatagrid);
            Questionnaires = new ObservableCollection<QuestionnaireVM>();
            UOW = ViewModelLocator.UOW;
            LoadQuestionnaires();

            searchText = "Zoek naam";
        }

        private void OpenCreateQuestionnaireWindow()
        {
            var currentWindow = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var temp = new CreateQuestionnaire();
            temp.Show();
            currentWindow.Close();
        }
        private void OpenEditQuestionnaireWindow()
        {
            if(SelectedQuestionnaire.Title != "Geen zoekresultaten")
            {
                if (UOW.Context.Vragenlijst.Include("Inspectie").Where(v => v.Actief == true && v.ID == SelectedQuestionnaire.ID) != null)
                {
                    if (UOW.Context.Vragenlijst.Include("Inspectie").Where(v => v.Actief == true && v.ID == SelectedQuestionnaire.ID).First().Inspectie.Count() == 0)
                    {
                        var currentWindow = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                        var temp = new EditQuestionnaireWindow();
                        Messenger.Default.Send(SelectedQuestionnaire);
                        temp.Show();
                        currentWindow.Close();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Deze vragenlijst is reeds ingepland bij een inspectie", "Er is geen mogelijkheid voor deze functionaliteit",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                LoadQuestionnaires();
                RaisePropertyChanged("Questionnaires");
            } 
        }

        private void DeleteQuestionnaire()
        {
            if (SelectedQuestionnaire.Title != "Geen zoekresultaten")
            {
                if (UOW.Context.Vragenlijst.Include("Inspectie").Where(v => v.Actief == true && v.ID == SelectedQuestionnaire.ID) != null)
                {
                    if (UOW.Context.Vragenlijst.Include("Inspectie").Where(v => v.Actief == true && v.ID == SelectedQuestionnaire.ID).First().Inspectie.Count() == 0)
                    {
                        UOW.Questionnaires.Get(SelectedQuestionnaire.ID).Actief = false;
                        saveToDatabase();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Deze vragenlijst is reeds ingepland bij een inspectie", "Er is geen mogelijkheid voor deze functionaliteit",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                LoadQuestionnaires();
                RaisePropertyChanged("Questionnaires");
            }
        }



        private void saveToDatabase()
        {
            try
            {
                UOW.Context.SaveChanges();
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void searchDatagrid()
        {
            if (searchText == "")
            {
                Questionnaires = new ObservableCollection<QuestionnaireVM>();
                foreach (var item in UOW.Context.Vragenlijst)
                {
                    Questionnaires.Add(new QuestionnaireVM(item));
                }
            }
            else
            {
                var questionnairelist = UOW.Questionnaires.GetAll().ToList().Select(q => new QuestionnaireVM(q)).Where(q => q.Title.ToLower().Contains(searchText.ToLower()));
                Questionnaires = new ObservableCollection<QuestionnaireVM>(questionnairelist);
            }

            if (Questionnaires.Count == 0 && searchText != "")
            {
                var questionnaire = new QuestionnaireVM();
                questionnaire.Title = "Geen zoekresultaten";
                Questionnaires.Add(questionnaire);
            }

            RaisePropertyChanged("Questionnaires");
        }

        private void LoadQuestionnaires()
        {
            Questionnaires = new ObservableCollection<QuestionnaireVM>();
            foreach (var item in UOW.Context.Vragenlijst.Include("Inspectie").Where(v => v.Actief == true))
            {
                if (item.Inspectie.Count() == 0)
                {
                    Questionnaires.Add(new QuestionnaireVM(item));
                }

            }
        }
    }
}