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
using GalaSoft.MvvmLight.Messaging;

namespace Festispec_WPF.ViewModel
{
    public class QuestionnaireVM: ViewModelBase
    {
        private Vragenlijst _questionnaire;
        //variables
        private ObservableCollection<IQuestion> _questions;
        public ObservableCollection<IQuestion> questions 
        {
            get { return _questions; }
            set { _questions = value;
                RaisePropertyChanged();
            }

        }

        public string Title { get { return _questionnaire.Titel; } set { _questionnaire.Titel = value; } }
        public string Version { get { return _questionnaire.Versie; } set { _questionnaire.Versie = value; } }
        public string Note { get { return _questionnaire.Opmerking; } set { _questionnaire.Opmerking = value; } }
        public bool IsFilled { get { return _questionnaire.Is_Ingevuld;  } set { _questionnaire.Is_Ingevuld = value; } }
        public int ID { get { return _questionnaire.ID; } }
        public ICommand CreateNewQuestionnaireCommand { get; set; }


        public QuestionnaireVM()
        {
            questions = new ObservableCollection<IQuestion>();
            CreateNewQuestionnaireCommand = new RelayCommand(OpenCreateQuestionnaire);
            _questionnaire = new Vragenlijst();
        }

        private void OpenCreateQuestionnaire()
        {
            CreateQuestionnaire window = new CreateQuestionnaire();
            window.Show();
        }

        public Vragenlijst ToModel()
        {
            return _questionnaire;
        }

    }
}
