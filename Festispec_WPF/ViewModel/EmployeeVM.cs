﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
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
                FailedRegisterView registerFailedView = new FailedRegisterView();
                registerFailedView.Show();
            }
     
        }

        //Gets all roles from the database (Rol_werknemers) in which he adds it do the RegisterView dropdown combobox.
        private void GetAllRoles()
        {
            RolesCollection = new ObservableCollection<string>(UOW.RoleEmployee.GetAll().Select(e=> (e.Rol)));
        }
    }
}