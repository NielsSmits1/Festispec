using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using FestiSpec.Domain.Model;
using Festispec_WPF.Model.UnitOfWork;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace Festispec_WPF.ViewModel.QuestionnaireFolder
{
    public class OpenQuestionVM : ViewModelBase, IQuestion
    {
        private UnitOfWork UOW;
        private Openvraag openQuestionModel;
        private int position;
        private string questiontype;

        public string Question { get => openQuestionModel.Vraag; set =>  openQuestionModel.Vraag = value; }
        public int ID { get => openQuestionModel.ID; }
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
        public OpenQuestionVM()
        {
            UOW = ViewModelLocator.UOW;
            openQuestionModel = new Openvraag();
            questiontype = "Openvraag";
        }
        public OpenQuestionVM(Openvraag openQuestionModel)
        {
            UOW = ViewModelLocator.UOW;
            this.openQuestionModel = openQuestionModel;
            questiontype = "Openvraag";
        }

        public void toDatabase(int questionnaireId)
        {
            UOW.Context.Openvraag.Add(openQuestionModel);
            addNewLink(questionnaireId);

        }
        public void deleteConnection(int questionnaireId)
        {
            var connection = UOW.Context.Openvraag_vragenlijst.Find(questionnaireId, openQuestionModel.ID);
            UOW.Context.Openvraag_vragenlijst.Remove(connection);
        }

        public void updateLink(int questionnaireId)
        {
            var connection = UOW.Context.Openvraag_vragenlijst.Find(questionnaireId,openQuestionModel.ID);
            connection.Positie = position;
        }

        public void addNewLink(int questionnaireId)
        {
            Openvraag_vragenlijst Link = new Openvraag_vragenlijst();
            Link.Openvraag_ID = openQuestionModel.ID;
            Link.Vragenlijst_ID = questionnaireId;
            Link.Positie = Position;
            UOW.Context.Openvraag_vragenlijst.Add(Link);
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
