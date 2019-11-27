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
using Festispec_WPF.Model;
using Festispec_WPF.Model.Repositories;
using Festispec_WPF.View;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Windows.Data;

namespace Festispec_WPF.ViewModel
{
    public class EmployeeCrudVM : ViewModelBase
    {
        private RegisterView _window;
        private UnitOfWork UOW;
        private EmployeeVM _employee;

        public ObservableCollection<EmployeeVM> Employees { get; set; }

        //Update Employee Commands
        public ICommand LoadEditEmployeeCommand { get; set; }
        public ICommand SafeEditEmployeeCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand OpenRegisterCommand { get; set; }

        public EmployeeVM SelectedEmployee
        {
            get => _employee;
            set
            {
                _employee = value;
                RaisePropertyChanged();
            }
        }

        public EmployeeVM NewEmployee { get; set; }

        public EmployeeCrudVM()
        {
            UOW = new ViewModelLocator().UOW;
            NewEmployee = new EmployeeVM();


            //List of all Emmployees - Read
            LoadAll();

            LoadEditEmployeeCommand = new RelayCommand(LoadEditData);
            SafeEditEmployeeCommand = new RelayCommand(SaveEditEmployee);
            RegisterCommand = new RelayCommand(HandleRegister);
            OpenRegisterCommand = new RelayCommand(OpenRegister);

        }

        private void OpenRegister()
        {
            NewEmployee = new EmployeeVM();
           _window = new RegisterView();
           _window.Show();
        }


        private void LoadAll()
        {
            Employees = new ObservableCollection<EmployeeVM>(UOW.NawEmployee.GetAll().ToList().Select(e => new EmployeeVM(e)));
            RaisePropertyChanged(() => Employees);
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
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                RaisePropertyChanged();
                LoadAll();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleRegister()
        {
            try
            {
                IRepository<Telefoonnummer> phonenumber = new Repository<Telefoonnummer>(UOW.Context);
                UOW.NawEmployee.Add(NewEmployee.NAWWerknemer);
                UOW.Employee.Add(NewEmployee.Werknemer);
                phonenumber.Add(NewEmployee.PhonenumberModel);
                UOW.Complete();
                _window.Close();
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
