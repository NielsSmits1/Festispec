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
using System.Windows.Input;
using System.Windows.Forms;

namespace Festispec_WPF.ViewModel
{
    public class QuestionaireCrudVM : ViewModelBase
    {
        public ObservableCollection<QuestionnaireVM> Questionnaires {get;set;}
        private UnitOfWork UOW;
        public QuestionnaireVM SelectedQuestionnaire { get; set; }
        public ICommand OpenCreateQuestionnaireWindowCommand { get; set; }
        public ICommand OpenEditQuestionnaireCommand { get; set; }
        public ICommand DeleteQuestionnaireCommand { get; set; }
        public QuestionaireCrudVM()
        {
            OpenCreateQuestionnaireWindowCommand = new RelayCommand(OpenCreateQuestionnaireWindow);
            OpenEditQuestionnaireCommand = new RelayCommand(OpenEditQuestionnaireWindow);
            DeleteQuestionnaireCommand = new RelayCommand(DeleteQuestionnaire);
            Questionnaires = new ObservableCollection<QuestionnaireVM>();
            UOW = ViewModelLocator.UOW;
            foreach (var item in UOW.Context.Vragenlijst.Where(v => v.Actief == true))
            {
                Questionnaires.Add(new QuestionnaireVM(item));
            }
            
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
            var currentWindow = System.Windows.Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var temp = new EditQuestionnaireWindow();
            Messenger.Default.Send(SelectedQuestionnaire);
            temp.Show();
            currentWindow.Close();
        }

        private void DeleteQuestionnaire()
        {
            UOW.Questionnaires.Get(SelectedQuestionnaire.ID).Actief = false;

            saveToDatabase();
            //foreach (var item in UOW.Context.Vragenlijst.Where(v => v.Actief == true))
            //{
            //    Questionnaires.Add(new QuestionnaireVM(item));
            //}

            Questionnaires = new ObservableCollection<QuestionnaireVM>();
            foreach (var item in UOW.Context.Vragenlijst.Where(v => v.Actief == true))
            {
                Questionnaires.Add(new QuestionnaireVM(item));
            }
            RaisePropertyChanged("Questionnaires");
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
    }
}
