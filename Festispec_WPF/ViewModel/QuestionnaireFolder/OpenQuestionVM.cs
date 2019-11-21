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
        //reference commands
        public ICommand OpenQuestionCommand { get; set; }

        //constructor
        public OpenQuestionVM()
        {
            OpenQuestionCommand = new RelayCommand(HandleOpenQuestionSubmit);

        }

        private void HandleOpenQuestionSubmit()
        {
            
        }

        //properties
        public string OpenQuestion { get; set; }
    }
}
