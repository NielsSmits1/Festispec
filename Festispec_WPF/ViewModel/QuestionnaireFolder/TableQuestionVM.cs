﻿using Festispec_WPF.Model.UnitOfWork;
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
    public class TableQuestionVM : ViewModelBase,  IQuestion
    {
        private UnitOfWork UOW;
        private Tabelvraag tableQuestionModel;
        private int position;
        private string questiontype;


        public string Question { get => tableQuestionModel.Vraag; set => tableQuestionModel.Vraag = value; }
        public string QuestionHead { get => tableQuestionModel.VraagKop; set => tableQuestionModel.VraagKop = value; }
        public string AnswerHead { get => tableQuestionModel.AntwoordKop; set => tableQuestionModel.AntwoordKop = value; }
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
        public TableQuestionVM()
        {
            UOW = new ViewModelLocator().UOW;
            tableQuestionModel = new Tabelvraag();
            questiontype = "Tabelvraag";
        }

        public TableQuestionVM(Tabelvraag tableQuestionModel)
        {
            UOW = new ViewModelLocator().UOW;
            this.tableQuestionModel = tableQuestionModel;
            questiontype = "Tabelvraag";
        }

        public void toDatabase(int questionnaireId)
        {
            UOW.Context.Tabelvraag.Add(tableQuestionModel);
            Tabelvraag_vragenlijst Link = new Tabelvraag_vragenlijst();
            Link.Tabelvraag_ID = tableQuestionModel.ID;
            Link.Vragenlijst_ID = questionnaireId;
            Link.Positie = Position;
            UOW.Context.Tabelvraag_vragenlijst.Add(Link);
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