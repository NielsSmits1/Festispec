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

        public ObservableCollection<EmployeeVM> Employees { get; set; }

        public EmployeeVM SelectedEmployee
        {
            get
            {
                return _employee;
            }
            set
            {
                _employee = value;
            }
        }

        public EmployeeCrudVM()
        {
            UOW = new ViewModelLocator().UOW;
            Employees = new ObservableCollection<EmployeeVM>(UOW.Employee.GetAll().ToList().Select(a => new EmployeeVM(a)));
            
            var nawEmployee = UOW.NawEmployee.GetAll().FirstOrDefault(e => e.ID == 2);
        }
    }
}
