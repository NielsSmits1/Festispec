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
    public class AppendixQuestionPageVM : ViewModelBase
    {
        private AppendixQuestionVM _newAppendixQuestion;
        public AppendixQuestionVM NewAppendixQuestion
        {
            get => _newAppendixQuestion;
            set
            {
                _newAppendixQuestion = value;
                RaisePropertyChanged();
            }
        }
        public ICommand SubmitQuestion { get; set; }
        public AppendixQuestionPageVM()
        {
            SubmitQuestion = new RelayCommand(Submit);
            NewAppendixQuestion = new AppendixQuestionVM();
        }

        private void Submit()
        {
            if (NewAppendixQuestion.Question == null)
                return;
            IQuestion newQuestion = NewAppendixQuestion;
            NewAppendixQuestion = new AppendixQuestionVM();
            Messenger.Default.Send(newQuestion);
        }
    }
}
