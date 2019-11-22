using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec_WPF.ViewModel.QuestionnaireFolder
{
    public class OpenQuestionVM : IQuestion
    {
        private Openvraag openQuestionModel;


        public string Question { get => openQuestionModel.Vraag; set =>  openQuestionModel.Vraag = value; }

        public OpenQuestionVM()
        {
            openQuestionModel = new Openvraag();
        }

    }
}
