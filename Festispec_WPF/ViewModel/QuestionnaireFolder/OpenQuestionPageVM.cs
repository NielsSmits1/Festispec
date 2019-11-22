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
    public class OpenQuestionPageVM : ViewModelBase
    {
        private OpenQuestionVM _newOpenQuestion;
        public OpenQuestionVM NewOpenQuestion
        {
            get { return _newOpenQuestion; }
            set { _newOpenQuestion = value;
                RaisePropertyChanged();
            }
        }
        public ICommand SubmitQuestion { get; set; }
        public OpenQuestionPageVM()
        {
            SubmitQuestion = new RelayCommand(Submit);
            NewOpenQuestion = new OpenQuestionVM();
        }

        private void Submit()
        {
            if(NewOpenQuestion.Question != null)
            {
                IQuestion newQuestion = NewOpenQuestion;
                NewOpenQuestion = new OpenQuestionVM();
                Messenger.Default.Send(newQuestion);
                

            }
            
            
        }
    }
}
