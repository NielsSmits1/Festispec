using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec_WPF.ViewModel
{
    public class QuestionaireCrudVM : ViewModelBase
    {
        public ObservableCollection<QuestionnaireVM> Questionnaires {get;set;}
        private UnitOfWork UOW;
        public QuestionnaireVM SelectedQuestionnaire { get; set; }
        public ICommand OpenCreateQuestionnaireWindowCommand { get; set; } 
        public QuestionaireCrudVM()
        {
            OpenCreateQuestionnaireWindowCommand = new RelayCommand(OpenCreateQuestionnaireWindow);
            Questionnaires = new ObservableCollection<QuestionnaireVM>();
            UOW = new ViewModelLocator().UOW;
            foreach (var item in UOW.Context.Vragenlijst)
            {
                Questionnaires.Add(new QuestionnaireVM(item));
            }
            
        }

        private void OpenCreateQuestionnaireWindow()
        {
            var temp = new CreateQuestionnaire();
            temp.Show();
        }
    }
}
