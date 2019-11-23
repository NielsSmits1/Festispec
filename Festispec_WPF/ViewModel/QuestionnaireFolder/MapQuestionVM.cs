using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec_WPF.ViewModel.QuestionnaireFolder
{
    public class MapQuestionVM : IQuestion
    {
        //constructor
        public MapQuestionVM()
        {
            LoadImageCommand = new RelayCommand(OpenFileExplorer);

        }

   

        //references
        public ICommand LoadImageCommand { get; set; }
        private void OpenFileExplorer()
        {
            Process.Start("explorer.exe", @"C:\Users");
        }

        public string Question { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
