using Festispec_WPF.Model.UnitOfWork;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Festispec_WPF.ViewModel.QuestionnaireFolder
{
    public class TableQuestionPageVM : ViewModelBase
    {
        private TableQuestionVM _newTableQuestion;
        public TableQuestionVM NewTableQuestion
        {
            get => _newTableQuestion;
            set
            {
                _newTableQuestion = value;
                RaisePropertyChanged();
            }
        }
        private UnitOfWork UOW;
        public ICommand SubmitQuestion { get; set; }
        public ICommand addSituationCommand { get; set; }

        public ObservableCollection<string> Situations { get; set; }
        public TableQuestionPageVM()
        {
            UOW = ViewModelLocator.UOW;
            SubmitQuestion = new RelayCommand(Submit);
            addSituationCommand = new RelayCommand(addSituation);
            NewTableQuestion = new TableQuestionVM();
            Situations = new ObservableCollection<string>();
        }

        private void addSituation()
        {
            Situations.Add(Situation);
            Situation = "";
            RaisePropertyChanged("Situation");
            RaisePropertyChanged("Situations");
        }

        private void Submit()
        {
            if (this.NewTableQuestion.Question == null || this.NewTableQuestion.QuestionHead == null || this.NewTableQuestion.AnswerHead == null)
                return;

            foreach (var item in Situations)
            {
                var temp = new FestiSpec.Domain.Model.Tabelvraag_situatie();
                temp.Situatie = item;
                this.NewTableQuestion.Situations.Add(temp);
            }
            IQuestion newQuestion = this.NewTableQuestion;
           
            Messenger.Default.Send(newQuestion);
            NewTableQuestion = new TableQuestionVM();
        }

        public string Situation { get; set; }
    }
    
}
