using Festispec_WPF.Model.UnitOfWork;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Festispec_WPF.ViewModel.QuestionnaireFolder
{
    public class MultipleChoiceQuestionVM : ViewModelBase,  IQuestion
    {
        private UnitOfWork UOW;
        private Meerkeuzevraag MultipleChoiceQuestionModel;
        private int position;
        private string questiontype;


        public ObservableCollection<string> AnwserOptions { get; set; }

        public string Question { get => MultipleChoiceQuestionModel.Vraag; set => MultipleChoiceQuestionModel.Vraag = value; }
        public int Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                RaisePropertyChanged();
            }
        }
        public string QuestionType { get => questiontype; set => questiontype = value; }
        public MultipleChoiceQuestionVM()
        {
            AnwserOptions = new ObservableCollection<string>();
            UOW = ViewModelLocator.UOW;
            MultipleChoiceQuestionModel = new Meerkeuzevraag();
            questiontype = "Meerkeuzevraag";
        }

        public MultipleChoiceQuestionVM(Meerkeuzevraag MultipleChoiceQuestionModel)
        {
            AnwserOptions = new ObservableCollection<string>();
            UOW = ViewModelLocator.UOW;
            this.MultipleChoiceQuestionModel = MultipleChoiceQuestionModel;
            questiontype = "Meerkeuzevraag";
        }

        public void toDatabase(int questionnaireId)
        {
            UOW.Context.Meerkeuzevraag.Add(MultipleChoiceQuestionModel);

            Meerkeuzevraag_vragenlijst Link = new Meerkeuzevraag_vragenlijst();
            Link.Meerkeuzevraag_ID = MultipleChoiceQuestionModel.ID;
            Link.Vragenlijst_ID = questionnaireId;
            Link.Positie = Position;
            UOW.Context.Meerkeuzevraag_vragenlijst.Add(Link);

            foreach (var Anwseroption in AnwserOptions)
            {
                Meerkeuzevraag_antwoord anwserOptionmodel = new Meerkeuzevraag_antwoord();
                anwserOptionmodel.Meerkeuzevraag_ID = MultipleChoiceQuestionModel.ID;
                anwserOptionmodel.Antwoord = Anwseroption;
                UOW.Context.Meerkeuzevraag_antwoord.Add(anwserOptionmodel);
            }
            try
            {
                UOW.Complete();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        public void deleteConnection(int questionnaireId)
        {
            var connection = UOW.Context.Meerkeuzevraag_vragenlijst.Find(questionnaireId, MultipleChoiceQuestionModel.ID);
            UOW.Context.Meerkeuzevraag_vragenlijst.Remove(connection);
        }

        public void updateLink(int questionnaireId)
        {
            var connection = UOW.Context.Meerkeuzevraag_vragenlijst.Find(questionnaireId, MultipleChoiceQuestionModel.ID );
            connection.Positie = position;
        }
    }
}
