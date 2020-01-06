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
    public class MapQuestionPageVM: ViewModelBase
    {
        private MapQuestionVM _newMapQuestion;
        public MapQuestionVM NewMapQuestion
        {
            get => _newMapQuestion;
            set
            {
                _newMapQuestion = value;
                RaisePropertyChanged();
            }
        }
        public ICommand SubmitQuestion { get; set; }
        public ICommand LoadPicture { get; set; }
        public MapQuestionPageVM()
        {
            SubmitQuestion = new RelayCommand(Submit);
            LoadPicture = new RelayCommand(InsertPicture);
            NewMapQuestion = new MapQuestionVM();
        }

        private void InsertPicture()
        {
            NewMapQuestion.Picture = FileLoader.LoadImage();
        }

        private void Submit()
        {
            if (NewMapQuestion.Question == null || NewMapQuestion.Picture == null)
                return;
            IQuestion newQuestion = NewMapQuestion;
            NewMapQuestion = new MapQuestionVM();
            Messenger.Default.Send(newQuestion);
        }
    }
}

