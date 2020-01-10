using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ICommand SubmitQuestion { get; set; }
        public TableQuestionPageVM()
        {
            SubmitQuestion = new RelayCommand(Submit);
            NewTableQuestion = new TableQuestionVM();
        }

        private void Submit()
        {
            if (NewTableQuestion.Question == null || NewTableQuestion.QuestionHead == null || NewTableQuestion.AnswerHead == null)
                return;
            IQuestion newQuestion = NewTableQuestion;
            NewTableQuestion = new TableQuestionVM();
            Messenger.Default.Send(newQuestion);
        }
    }
    
}
