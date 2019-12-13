using Festispec_WPF.Model.UnitOfWork;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Festispec_WPF.ViewModel.QuestionnaireFolder
{
    public class AppendixQuestionVM : ViewModelBase, IQuestion
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
                RaisePropertyChanged();
            }
        }
        public string QuestionType { get => questiontype; set => questiontype = value; }
        public AppendixQuestionVM()
        {
            UOW = new ViewModelLocator().UOW;
            appendixQuestionModel = new Bijlagevraag();
            questiontype = "Bijlagevraag";
        }
        public AppendixQuestionVM(Bijlagevraag appendixQuestionModel)
        {
            UOW = new ViewModelLocator().UOW;
            this.appendixQuestionModel = appendixQuestionModel;
            questiontype = "Bijlagevraag";
        }
        public void toDatabase(int questionnaireId)
        {
            UOW.Context.Bijlagevraag.Add(appendixQuestionModel);
            Bijlagevraag_vragenlijst Link = new Bijlagevraag_vragenlijst();
            Link.Bijlagevraag_ID = appendixQuestionModel.ID;
            Link.Vragenlijst_ID = questionnaireId;
            Link.Positie = Position;
            UOW.Context.Bijlagevraag_vragenlijst.Add(Link);
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
