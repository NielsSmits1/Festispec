using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.ViewModel
{
    public class QuestionnaireVM: ViewModelBase
    {
        private ObservableCollection<IQuestion> _questions;
        public ObservableCollection<IQuestion> questions 
        {
            get { return _questions; }
            set { _questions = value;
                RaisePropertyChanged();
            }
        }
        public QuestionnaireVM()
        {
            questions = new ObservableCollection<IQuestion>();
        }

    }
}
