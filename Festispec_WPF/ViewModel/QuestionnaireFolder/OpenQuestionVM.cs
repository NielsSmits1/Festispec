using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Festispec_WPF.Model.UnitOfWork;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec_WPF.ViewModel.QuestionnaireFolder
{
    public class OpenQuestionVM : IQuestion
    {
        private UnitOfWork UOW;
        private Openvraag openQuestionModel;
        private int position;


        public string Question { get => openQuestionModel.Vraag; set =>  openQuestionModel.Vraag = value; }
        public int Position { get => position; set => position = value; }

        public OpenQuestionVM()
        {
            UOW = new ViewModelLocator().UOW;
            openQuestionModel = new Openvraag();
        }

        public void toDatabase(int questionnaireId)
        {
            UOW.Context.Openvraag.Add(openQuestionModel);
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
