using Festispec_WPF.Model.UnitOfWork;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.ViewModel
{
    public class EmployeeCrudVM : ViewModelBase
    {
        private UnitOfWork UOW;
        private EmployeeVM _employee;
        private EmployeeVM _nawEmployee;

        public ObservableCollection<EmployeeVM> Employees { get; set; }
        public ObservableCollection<EmployeeVM> Employees1 { get; set; }

        public EmployeeCrudVM()
        {
            UOW = new ViewModelLocator().UOW;

            //List of all Emmployees - Read
            LoadAll();
        }

        private void LoadAll()
        {
            Employees = new ObservableCollection<EmployeeVM>(UOW.NawEmployee.GetAll().ToList().Select(e => new EmployeeVM(e)));
        }
    }
}
