using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec_WPF.View;
using GalaSoft.MvvmLight.Command;

namespace Festispec_WPF.ViewModel
{
    public class QuestionnaireVM: ViewModelBase
    {
        //variables
        private ObservableCollection<IQuestion> _questions;
        public ObservableCollection<IQuestion> questions 
        {
            get { return _questions; }
            set { _questions = value;
                RaisePropertyChanged();
            }
        }
        public ICommand CreateNewQuestionnaireCommand { get; set; }


        public QuestionnaireVM()
        {
            questions = new ObservableCollection<IQuestion>();
            CreateNewQuestionnaireCommand = new RelayCommand(OpenCreateQuestionnaire);

        }

        private void OpenCreateQuestionnaire()
        {
            CreateQuestionnaire window = new CreateQuestionnaire();
            window.Show();
        }
    }
}
