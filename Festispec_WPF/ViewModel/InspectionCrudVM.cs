using Festispec_WPF.Model;
using Festispec_WPF.Model.Repositories;
using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace Festispec_WPF.ViewModel
{
    public class InspectionCrudVM : ViewModelBase
    {
        //KNOW BUG(S):
        // 1.Can't create inspection if you attempt to create a location with wrong zipcode format and then close the create window, 
        //   DB will try to save this wrong location as well in the next complete
        private CreateInspectionWindow _createWindow;
        private CreateLocationWindow _createLocation;
        private UnitOfWork _UOW;
        private CustomerVM _selectedCustomer;
        private LocationVM _selectedLocation;
        private InspectionVM _selectedInspection;
        public ObservableCollection<InspectionVM> Inspection { get; set; }

        public InspectionVM NewInspection { get; set; }

        public LocationVM NewLocation { get; set; }

        public InspectionVM SelectedInspection
        {
            get => _selectedInspection;
            set
            {
                _selectedInspection = value;
                if(value == null)
                {
                    EnableUpdate = false;
                }
                else
                {
                    EnableUpdate = true; 
                }
                RaisePropertyChanged(() => EnableUpdate);
                RaisePropertyChanged(() => SelectedInspection);
            }
        }

        public ObservableCollection<LocationVM> Locations { get; set; }
        public ObservableCollection<CustomerVM> Customers { get; set; }

        public ICommand OpenCreateWindowCommand { get; set; }
        public ICommand CreateNewInspectionCommand { get; set; }

        public ICommand OpenCreateLocationWindowCommand { get; set; }
        public ICommand CreateNewLocationCommand { get; set; }
        public ICommand SafeEditCommand { get; set; }
        public bool EnableUpdate { get; set; }
        public CustomerVM SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value; NewInspection.Customer = value; RaisePropertyChanged(() => SelectedCustomer);
            }
        }

        public CustomerVM SelectedUpdateCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value; SelectedInspection.Customer = value; RaisePropertyChanged(() => SelectedUpdateCustomer);
            }
        }

        public LocationVM SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value; NewInspection.Location = value; RaisePropertyChanged(() => SelectedLocation);
            }
        }

        public LocationVM SelectedUpdateLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value; SelectedInspection.Location = value; RaisePropertyChanged(() => SelectedUpdateLocation);
            }
        }

        public InspectionCrudVM()
        {
            
            _UOW = new ViewModelLocator().UOW;
            Inspection = new ObservableCollection<InspectionVM>(_UOW.Inspections.GetAll().Select(ins => new InspectionVM(ins)));
            Locations = new ObservableCollection<LocationVM>(_UOW.InspectionLocations.GetAll().Select(loc => new LocationVM(loc)));
            Customers = new ObservableCollection<CustomerVM>(new Repository<Klant>(_UOW.Context).GetAll().ToList().Select(cus => new CustomerVM(cus)));
            CreateNewInspectionCommand = new RelayCommand(AddNewInspection);
            OpenCreateLocationWindowCommand = new RelayCommand(OpenCreateLocationWindow);
            CreateNewLocationCommand = new RelayCommand(AddNewLocation);
            OpenCreateWindowCommand = new RelayCommand(OpenCreateWindow);
            SafeEditCommand = new RelayCommand(SafeEditInspection);
        }

        private void OpenCreateWindow()
        {
            NewInspection = new InspectionVM();
            //Managers = new ObservableCollection<EmployeeVM>(_UOW.Employee.GetAllManagers().Select(emp => new EmployeeVM(emp)));
            Locations = new ObservableCollection<LocationVM>(_UOW.InspectionLocations.GetAll().Select(loc => new LocationVM(loc)));
            Customers = new ObservableCollection<CustomerVM>(new Repository<Klant>(_UOW.Context).GetAll().ToList().Select(cus => new CustomerVM(cus)));
            _createWindow = new CreateInspectionWindow();
            _createWindow.Show();
        }

        private void OpenCreateLocationWindow()
        {
            NewLocation = new LocationVM();
            _createLocation = new CreateLocationWindow();
            _createLocation.Show();
        }

        private void AddNewLocation()
        {
            _UOW.InspectionLocations.Add(NewLocation.Locatie);

            try
            {
                _UOW.Complete();
                _createLocation.Close();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Locations = new ObservableCollection<LocationVM>(_UOW.InspectionLocations.GetAll().Select(loc => new LocationVM(loc)));
            RaisePropertyChanged(() => Locations);
        }

        private void AddNewInspection()
        {
            NewLocation = null;
            _UOW.Inspections.Add(NewInspection.Inspection);

            try
            {
                _UOW.Complete();
                _createWindow.Close();
                Inspection = new ObservableCollection<InspectionVM>(_UOW.Inspections.GetAll().Select(ins => new InspectionVM(ins)));

            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void SafeEditInspection()
        {
            try
            {
                _UOW.Complete();
                MessageBox.Show("De aanpassingen zijn doorgevoerd", "Het is gelukt!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
