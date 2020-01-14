
using GalaSoft.MvvmLight;
﻿using FestiSpec.Domain.Model;
using System.Collections.ObjectModel;

namespace Festispec_WPF.ViewModel
{
    public class CustomerVM : ViewModelBase
    {
        private Klant _customer;
        private NAW_KlantVM _NAW_Klant;
        private NAW_KlantVM _NAW_Customer;
        private ObservableCollection<ContactPersonVM> _ContactPersons;
        
        public NAW_KlantVM NAW_Klant
        {
            get => _NAW_Klant;
            set => _NAW_Klant = value;
        }

        public int ID
        {
            get => _customer.ID;
            set
            {
                _customer.ID = value; RaisePropertyChanged(() => ID);
            }

        }
        public ObservableCollection<ContactPersonVM> ContactPersons
        {
            get => _ContactPersons;
            set => _ContactPersons = value;
        }
        public string CompanyName
        {
            get => _customer.Bedrijfsnaam;
            set => _customer.Bedrijfsnaam = value;
        }
        public CustomerVM(Klant customer)
        {
            _customer = customer;
            _NAW_Klant = new NAW_KlantVM(_customer.NAW_Klant);
        }
        public CustomerVM()
        {
            _customer = new Klant();
            _NAW_Klant = new NAW_KlantVM();
            _NAW_Customer = new NAW_KlantVM();
        }
        public Klant CustomerData
        {
            get => _customer;
            set => _customer = value;
        }
    }
}
