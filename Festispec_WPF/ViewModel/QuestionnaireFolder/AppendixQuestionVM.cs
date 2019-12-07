using Festispec_WPF.Model.UnitOfWork;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Festispec_WPF.ViewModel.QuestionnaireFolder
{
    public class AppendixQuestionVM : IQuestion
    {

        private UnitOfWork UOW;
        private Bijlagevraag appendixQuestionModel;
        private int position;
        private string questiontype;

        public string Question { get => appendixQuestionModel.Vraag; set => appendixQuestionModel.Vraag = value; }
        public int Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                IQuestion changedPositionQuestion = this;
                Messenger.Default.Send(changedPositionQuestion);
            }
        }
        public string QuestionType { get => questiontype; set => questiontype = value; }
        public AppendixQuestionVM()
        {
            UOW = new ViewModelLocator().UOW;
            appendixQuestionModel = new Bijlagevraag();
            questiontype = "Bijlagevraag";
        }
        public void toDatabase(int questionnaireId)
        {
            UOW.Context.Bijlagevraag.Add(appendixQuestionModel);
            Bijlagevraag_Vragenlijst Link = new Bijlagevraag_Vragenlijst();
            Link.Bijlage_ID = appendixQuestionModel.ID;
            Link.Vragenlijst_ID = questionnaireId;
            Link.Position = Position;
            UOW.Context.Bijlagevraag_Vragenlijst.Add(Link);
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
    }
}
