using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec_WPF.ViewModel.QuestionnaireFolder
{
    public class MultipleChoiceQuestionPageVM : ViewModelBase
    {
        public string newAnwserOption { get; set; }

        private MultipleChoiceQuestionVM _newMultipleChoiceQuestion;

        public MultipleChoiceQuestionVM NewMultipleChoiceQuestion
        {
            get => _newMultipleChoiceQuestion;
            set
            {
                _newMultipleChoiceQuestion = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> AnwserOptions
        {
            get
            {
                return NewMultipleChoiceQuestion.AnwserOptions;
            }

            set
            {
                NewMultipleChoiceQuestion.AnwserOptions = AnwserOptions;
            }
        }
        public ICommand SubmitQuestion { get; set; }
        public ICommand SubmitAnwserOption { get; set; }
        public MultipleChoiceQuestionPageVM()
        {
            
            SubmitAnwserOption = new RelayCommand(SubmitNewAnwserOption);
            SubmitQuestion = new RelayCommand(Submit);
            NewMultipleChoiceQuestion = new MultipleChoiceQuestionVM();
            AnwserOptions = new ObservableCollection<string>();
        }

        private void Submit()
        {
            if (AnwserOptions.Count < 2)
            {
                return;
            }
            if (NewMultipleChoiceQuestion.Question == null)
                return;
            IQuestion newQuestion = NewMultipleChoiceQuestion;
            NewMultipleChoiceQuestion = new MultipleChoiceQuestionVM();
            AnwserOptions = new ObservableCollection<string>();
            RaisePropertyChanged("AnwserOptions");
            Messenger.Default.Send(newQuestion);
        }

        private void SubmitNewAnwserOption()
        {
            if (newAnwserOption == null)
            {
                return;
            }
            NewMultipleChoiceQuestion.AnwserOptions.Add(newAnwserOption);
            newAnwserOption = null;
            RaisePropertyChanged("newAnwserOption");
        }
    }
}

