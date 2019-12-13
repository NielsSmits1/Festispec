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
using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.ViewModel.QuestionnaireFolder;

namespace Festispec_WPF.ViewModel
{
    public class QuestionnaireVM: ViewModelBase
    {
        private Vragenlijst _questionnaire;
        private UnitOfWork UOW;
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
        public bool IsActive { get { return _questionnaire.Actief; } set { _questionnaire.Actief = value; } }


        public QuestionnaireVM()
        {
            questions = new ObservableCollection<IQuestion>();
            _questionnaire = new Vragenlijst();
        }

        public QuestionnaireVM(Vragenlijst questionnaire)
        {
            questions = new ObservableCollection<IQuestion>();
            _questionnaire = questionnaire;
        }

        public void loadQuestions()
        {
            UOW = new ViewModelLocator().UOW;
            Dictionary<int, IQuestion> questionDictionary = new Dictionary<int, IQuestion>();
            LoadOpenQuestions(questionDictionary);
            LoadMapQuestions(questionDictionary);
            LoadTableQuestions(questionDictionary);
            LoadMultilpleChoiceQuestions(questionDictionary);
            LoadAppendixQuestions(questionDictionary);
            for (int i = 0; i < questionDictionary.Count; i++)
            {
                questions.Add(questionDictionary[i]);
            }
        }

        public Vragenlijst ToModel()
        {
            return _questionnaire;
        }

        private void LoadOpenQuestions(Dictionary<int,IQuestion> d)
        {
            var OpenQuestionsConnections = UOW.Context.Openvraag_vragenlijst.Where(q => q.Vragenlijst_ID == ID).ToList();
            foreach (Openvraag_vragenlijst ov in OpenQuestionsConnections)
            {
                int id = ov.Openvraag_ID;
                IQuestion question = new OpenQuestionVM(UOW.Context.Openvraag.Find(id));
                d.Add(ov.Positie, question);
            }
        }
        private void LoadMapQuestions(Dictionary<int, IQuestion> d)
        {
            var MapQuestionsConnections = UOW.Context.Kaartvraag_vragenlijst.Where(q => q.Vragenlijst_ID == ID).ToList();
            foreach (Kaartvraag_vragenlijst ov in MapQuestionsConnections)
            {
                int id = ov.Kaartvraag_ID;
                IQuestion question = new MapQuestionVM(UOW.Context.Kaartvraag.Find(id));
                d.Add(ov.Positie, question);
            }
        }
        private void LoadTableQuestions(Dictionary<int, IQuestion> d)
        {
            var TableQuestionsConnections = UOW.Context.Tabelvraag_vragenlijst.Where(q => q.Vragenlijst_ID == ID).ToList();
            foreach (Tabelvraag_vragenlijst ov in TableQuestionsConnections)
            {
                int id = ov.Tabelvraag_ID;
                IQuestion question = new TableQuestionVM(UOW.Context.Tabelvraag.Find(id));
                d.Add(ov.Positie, question);
            }
            
        }
        private void LoadMultilpleChoiceQuestions(Dictionary<int, IQuestion> d)
        {
            var MultipleChoiceQuestionsConnections = UOW.Context.Meerkeuzevraag_vragenlijst.Where(q => q.Vragenlijst_ID == ID).ToList();
            foreach (Meerkeuzevraag_vragenlijst ov in MultipleChoiceQuestionsConnections)
            {
                int id = ov.Meerkeuzevraag_ID;
                IQuestion question = new MultipleChoiceQuestionVM(UOW.Context.Meerkeuzevraag.Find(id));
                d.Add(ov.Positie, question);
            }

        }
        private void LoadAppendixQuestions(Dictionary<int, IQuestion> d)
        {
            var AppendixQuestionsConnections = UOW.Context.Bijlagevraag_vragenlijst.Where(q => q.Vragenlijst_ID == ID).ToList();
            foreach (Bijlagevraag_vragenlijst ov in AppendixQuestionsConnections)
            {
                int id = ov.Bijlagevraag_ID;
                IQuestion question = new AppendixQuestionVM(UOW.Context.Bijlagevraag.Find(id));
                d.Add(ov.Positie, question);
            }

        }
    }
}
