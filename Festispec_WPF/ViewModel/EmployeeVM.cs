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
using FestiSpec.Domain.Model;

namespace Festispec_WPF.ViewModel
{
    public class EmployeeVM : ViewModelBase
    {
        //private variables
        private NAW_werknemer _nawWerknemer;
        private Werknemer _werknemer;
        private UnitOfWork UOW;

        //constructor
        public EmployeeVM()
        {
            UOW = new ViewModelLocator().UOW;
            GetAllRoles();
            _werknemer = new Werknemer();
            _nawWerknemer = new NAW_werknemer();
            DoB = DateTime.Now.Date;
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
        public Werknemer Werknemer => _werknemer;

        public NAW_werknemer NAWWerknemer => _nawWerknemer;

        // NAW Employee
        public int NAWEmployee_iD
        {
            get => _nawWerknemer.ID;
            set { _nawWerknemer.ID = value; RaisePropertyChanged(); }
        }

        public string FirstName
        {
            get => _nawWerknemer.Voornaam;
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

        //Aanpassen naar straatnaam
        public string TownName
        {
            get => _nawWerknemer.Straatnaam;
            set
            {
                _nawWerknemer.Straatnaam = value;
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
            get => _werknemer.ID;
            set { _werknemer.ID = value; RaisePropertyChanged(); }
        }

        public string Role
        {
            get => _werknemer.Rol;
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

        public int Employee_NAW_Id
        {
            get => _werknemer.NAW;
            set 
            { 
                _werknemer.NAW = value;
                RaisePropertyChanged();
            }
        }

        public bool Active
        {
            get => _werknemer.Actief;
            set
            {
                _werknemer.Actief = value;
                RaisePropertyChanged();
            }
        }

        public Werknemer EmployeeData
        {
            get => _werknemer;
            set
            {
                _werknemer = value;
                RaisePropertyChanged();
            }
        }


        public string Phonenumber
        {
            get => _nawWerknemer.Telefoonnummer;

            set
            {
                _nawWerknemer.Telefoonnummer = value;
                RaisePropertyChanged();
            }
        }

        //Gets all roles from the database (Rol_werknemers) in which he adds it do the RegisterView dropdown combobox.
        private void GetAllRoles()
        {
            RolesCollection = new ObservableCollection<string>(UOW.RoleEmployee.GetAll().Select(e=> (e.Rol)));
        }
    }
}