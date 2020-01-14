using Festispec_WPF.Model.UnitOfWork;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using Festispec_WPF.View;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Festispec_WPF.ViewModel
{
    public class EmployeeCrudVM : ViewModelBase
    {
        private RegisterView _window;
        private UnitOfWork UOW;
        private EmployeeVM _employee;
        private bool isSelected;

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                RaisePropertyChanged("IsSelected");
            }
        }

        private string _editVisibility;

        public string EditVisibility
        {
            get => _editVisibility;
            set
            {
                _editVisibility = value;
                base.RaisePropertyChanged();
            }
        }

        private string _enableEdit;
        public string EnableEdit
        {
            get => _enableEdit;
            set
            {
                _enableEdit = value;
                base.RaisePropertyChanged();
            }
        }

        private string _employeeVisibility;

        public string EmployeeVisibility
        {
            get => _employeeVisibility;
            set
            {
                _employeeVisibility = value;
                base.RaisePropertyChanged();
            }
        }

        public ObservableCollection<EmployeeVM> Employees { get; set; }

        //Update Employee Commands
        public ICommand LoadEditEmployeeCommand { get; set; }
        public ICommand SafeEditEmployeeCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public ICommand OpenRegisterCommand { get; set; }
        public ICommand ChangeViewToEdit { get; set; }
        public ICommand CancelEdit { get; set; }
        public ICommand SearchDataGrid { get; set; }

        public EmployeeVM SelectedEmployee
        {
            get => _employee;
            set
            {
                _employee = value;
                IsSelected = true;
                EnableEdit = "Visible";
                RaisePropertyChanged();
            }
        }

        private string _searchText;

        public string searchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                base.RaisePropertyChanged();
            }
        }

        public EmployeeVM NewEmployee { get; set; }

        public EmployeeCrudVM()
        {
            IsSelected = false;
            UOW = ViewModelLocator.UOW;
            NewEmployee = new EmployeeVM();

            EmployeeVisibility = "Visible";
            EditVisibility = "Hidden";
            EnableEdit = "Hidden";
            searchText = "Zoek voor medewerker";

            //List of all Emmployees - Read
            LoadAll();

            LoadEditEmployeeCommand = new RelayCommand(LoadEditData);
            SafeEditEmployeeCommand = new RelayCommand(SaveEditEmployee);
            RegisterCommand = new RelayCommand(HandleRegister);
            OpenRegisterCommand = new RelayCommand(OpenRegister);
            ChangeViewToEdit = new RelayCommand(OpenEditView);
            CancelEdit = new RelayCommand(MakeEmployeeVisible);
            SearchDataGrid = new RelayCommand(searchDatagrid);
        }

        private void searchDatagrid()
        {
            if (searchText == "")
            {
                Employees = new ObservableCollection<EmployeeVM>(UOW.NawEmployee.GetAll().ToList().Select(e => new EmployeeVM(e)));
                RaisePropertyChanged("Employees");
            }
            else
            {
                Employees = new ObservableCollection<EmployeeVM>(UOW.NawEmployee.GetAll().ToList().Select(e => new EmployeeVM(e)).Where(e => e.FullName.ToLower().Contains(searchText.ToLower())));
                RaisePropertyChanged("Employees");
            }

            if (Employees.Count == 0 && searchText != "")
            {
                var employee = new EmployeeVM();
                employee.LastName = "Geen zoekresultaten";
                Employees.Add(employee);
            }
        }

        private void MakeEmployeeVisible()
        {
            EditVisibility = "Hidden";
            EmployeeVisibility = "Visible";
        }

        private void OpenEditView()
        {
            if (SelectedEmployee.LastName != "Geen zoekresultaten")
            {
                EditVisibility = "Visible";
                EmployeeVisibility = "Hidden";
            }
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
                MakeEmployeeVisible();
            }
            catch
            {
                MessageBox.Show("Fout bij invoeren velden", "Er is iets fout gegaan",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HandleRegister()
        {
            try
            {
                UOW.NawEmployee.Add(NewEmployee.NAWWerknemer);
                UOW.Employee.Add(NewEmployee.Werknemer);
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
