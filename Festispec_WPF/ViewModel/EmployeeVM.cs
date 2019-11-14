using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Festispec_WPF.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec_WPF.ViewModel
{
    public class EmployeeVM : ViewModelBase
    {
        private readonly NAW_werknemer _nawWerknemer;

        //private variables
        private readonly Werknemer _werknemer;
        private readonly Telefoonnummer _werknemerTelefoonNummer;

        //constructor
        public EmployeeVM()
        {
            GetAllRoles();
            RegisterCommand = new RelayCommand(HandleRegister);
            _werknemer = new Werknemer();
            _nawWerknemer = new NAW_werknemer();
            _werknemerTelefoonNummer = new Telefoonnummer();
        }

        //public variables
        public ObservableCollection<string> RolesCollection { get; set; }

        //properties
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

        //TODO ADD DATEPICKER
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
        //TODO handle double register click (program crash)
        private void HandleRegister()
        {
            using (var context = new FestiSpecEntities())
            {
                context.NAW_werknemer.Add(_nawWerknemer);
                context.Werknemer.Add(_werknemer);
                context.Telefoonnummer.Add(_werknemerTelefoonNummer);
                context.SaveChanges();
            }
        }
        //Gets all roles from the database (Rol_werknemers) in which he adds it do the RegisterView dropdown combobox.
        private void GetAllRoles()
        {
            RolesCollection = new ObservableCollection<string>();
            using (var context = new FestiSpecEntities())
            {
                foreach (var variable in context.Rol_werknemer)
                {
                    RolesCollection.Add(variable.Rol);
                }
            }
        }
    }
}