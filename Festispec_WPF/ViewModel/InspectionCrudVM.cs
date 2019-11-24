using Festispec_WPF.View;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec_WPF.ViewModel
{
    public class InspectionCrudVM : ViewModelBase
    {
        public ObservableCollection<InspectionVM> Inspection { get; set; }

        public InspectionVM NewInspection { get; set; }

        public ICommand OpenCreateWindowCommand { get; set; }

        public InspectionCrudVM()
        {
            Inspection = new ObservableCollection<InspectionVM>();

        }

        private void OpenCreateWindow()
        {
            NewInspection = new InspectionVM();
            CreateInspectionWindow create = new CreateInspectionWindow();
            create.Show();
        }
    }
}
