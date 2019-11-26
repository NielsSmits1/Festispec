using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using Festispec_WPF.Model;
using Festispec_WPF.Model.Repositories;
using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec_WPF.ViewModel
{
    public class EmployeeVM : ViewModelBase
    {
        //private variables
        private NAW_werknemer _nawWerknemer;
        private Werknemer _werknemer;
        private Telefoonnummer _werknemerTelefoonNummer;
        private UnitOfWork UOW;

        //constructor
        public EmployeeVM()
        {
            UOW = new ViewModelLocator().UOW;
            GetAllRoles();
            RegisterCommand = new RelayCommand(HandleRegister);
            _werknemer = new Werknemer();
            _nawWerknemer = new NAW_werknemer();
            _werknemerTelefoonNummer = new Telefoonnummer();
        }

        public EmployeeVM(Werknemer werknemer)
        {
            UOW = new ViewModelLocator().UOW;
            _werknemer = werknemer;
            _nawWerknemer = UOW.NawEmployee.Get(werknemer.NAW);
        }

        public EmployeeVM(NAW_werknemer ne)
        {
            UOW = new ViewModelLocator().UOW;
            _nawWerknemer = ne;
            _werknemer = UOW.Employee.GetEmployeeByNAW(NAWEmployee_iD);
        }

        //public variables
        public ObservableCollection<string> RolesCollection { get; set; }

        //properties
        public Werknemer Werknemer
        {
            get { return _werknemer; }
        }

        public NAW_werknemer NAWWerknemer
        {
            get { return _nawWerknemer; }
        }

        // NAW Employee
        public int NAWEmployee_iD
        {
            get { return _nawWerknemer.ID; }
            set { _nawWerknemer.ID = value; RaisePropertyChanged(); }
        }

        public string FullName
        {
            get
            {
                return FirstName + " " + InfixName + " " + LastName;
            }
        }

        public string FirstName
        {
            get { return _nawWerknemer.Voornaam; }            
            set
            {
                _nawWerknemer.Voornaam = value;
                RaisePropertyChanged();
            }
        }

        public string InfixName
        {
            get => _nawWerknemer.Tussenvoegsel;
            set
            {
                _nawWerknemer.Tussenvoegsel = value;
                RaisePropertyChanged();
            }
        }

        public string LastName
        {
            get => _nawWerknemer.Achternaam;
            set
            {
                _nawWerknemer.Achternaam = value;
                RaisePropertyChanged();
            }
        }

        public string Fullname
        {
            get
            {
                return FirstName + " " + InfixName + " " + LastName;
            }
        }

        public string Postcode
        {
            get => _nawWerknemer.Postcode;
            set
            {
                _nawWerknemer.Postcode = value;
                RaisePropertyChanged();
            }
        }

        public string HouseNumber
        {
            get => _nawWerknemer.Huisnummer;
            set
            {
                _nawWerknemer.Huisnummer = value;
                RaisePropertyChanged();
            }
        }
        public string TownName
        {
            get => _nawWerknemer.Plaatsnaam;
            set
            {
                _nawWerknemer.Plaatsnaam = value;
                RaisePropertyChanged();
            }
        }

        public DateTime DoB
        {
            get => _nawWerknemer.GeboorteDatum;
            set
            {
                _nawWerknemer.GeboorteDatum = value;
                RaisePropertyChanged();
            }
        }

        public string Iban
        {
            get => _nawWerknemer.IBAN;
            set
            {
                _nawWerknemer.IBAN = value;
                RaisePropertyChanged();
            }
        }

        public string MailAdress
        {
            get => _nawWerknemer.Email;
            set
            {
                _nawWerknemer.Email = value;
                RaisePropertyChanged();
            }
        }
        
        // employee
        public int Employee_ID
        {
            get { return _werknemer.ID; }
            set { _werknemer.ID = value; }
        }

        public string Role
        {
            get { return _werknemer.Rol; }
            set
            {
                _werknemer.Rol = value;
                RaisePropertyChanged();
            }
        }

        public string Username
        {
            get => _werknemer.Username;
            set
            {
                _werknemer.Username = value;
                RaisePropertyChanged();
            }
        }

        public string Password
        {
            get => _werknemer.Wachtwoord;
            set
            {
                _werknemer.Wachtwoord = value;
                RaisePropertyChanged();
            }
        }

        public Boolean Active
        {
            get => _werknemer.Actief;
            set { _werknemer.Actief = value; }
        }

        public int NAW
        {
            get { return _werknemer.NAW; }
            set { _werknemer.NAW = value; }
        }

        public Werknemer EmployeeData
        {
            get
            {
                return _werknemer;
            }
            set
            {
                _werknemer = value;
            }
        }

        //command references
        public ICommand RegisterCommand { get; set; }

        public string Phonenumber
        {
            get => _werknemerTelefoonNummer.Telefoonnummer1;

            set
            {
                _werknemerTelefoonNummer.Telefoonnummer1 = value;
                RaisePropertyChanged();
            }
        }
        private void HandleRegister()
        {
            try
            {
                IRepository<Telefoonnummer> phonenumber = new Repository<Telefoonnummer>(UOW.Context);
                UOW.NawEmployee.Add(_nawWerknemer);
                UOW.Employee.Add(_werknemer);
                phonenumber.Add(_werknemerTelefoonNummer);
                UOW.Complete();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
     
        }

        //Gets all roles from the database (Rol_werknemers) in which he adds it do the RegisterView dropdown combobox.
        private void GetAllRoles()
        {
            RolesCollection = new ObservableCollection<string>(UOW.RoleEmployee.GetAll().Select(e=> (e.Rol)));
        }
    }
}