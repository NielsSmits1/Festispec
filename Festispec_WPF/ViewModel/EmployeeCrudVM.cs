using Festispec_WPF.Model.UnitOfWork;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Festispec_WPF.ViewModel
{
    public class EmployeeCrudVM : ViewModelBase
    {
        private UnitOfWork UOW;
        private EmployeeVM _employee;

        public ObservableCollection<EmployeeVM> Employees { get; set; }
        public ObservableCollection<EmployeeVM> Employees1 { get; set; }

        //Update Employee Commands
        public ICommand LoadEditEmployeeCommand { get; set; }
        public ICommand SafeEditEmployeeCommand { get; set; }

        public EmployeeVM SelectedEmployee
        {
            get => _employee;
            set
            {
                _employee = value;
                RaisePropertyChanged();
            }
        }

        public EmployeeCrudVM()
        {
            UOW = new ViewModelLocator().UOW;

            //List of all Emmployees - Read
            LoadAll();

            LoadEditEmployeeCommand = new RelayCommand(LoadEditData);
            SafeEditEmployeeCommand = new RelayCommand(SaveEditEmployee);
        }

        private void LoadAll()
        {
            Employees = new ObservableCollection<EmployeeVM>(UOW.NawEmployee.GetAll().ToList().Select(e => new EmployeeVM(e)));
        }

        //update
        private void LoadEditData()
        {
            SelectedEmployee.EmployeeData = UOW.Employee.GetAll().FirstOrDefault(e => e.NAW == SelectedEmployee.NAWEmployee_iD);
        }


        private void SaveEditEmployee()
        {
            var NAW = UOW.NawEmployee.GetAll().FirstOrDefault(we => we.ID == SelectedEmployee.NAWEmployee_iD);
            NAW = SelectedEmployee.NAWWerknemer;

            var employee = UOW.Employee.GetAll().FirstOrDefault(we => we.ID == SelectedEmployee.Employee_ID);
            employee = SelectedEmployee.EmployeeData;

            try
            {
                UOW.Complete();
                MessageBox.Show("De aanpassingen zijn doorgevoerd", "Het is gelukt!",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                RaisePropertyChanged();
                LoadAll();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
