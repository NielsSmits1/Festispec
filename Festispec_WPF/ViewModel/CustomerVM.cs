using Festispec_WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec_WPF.ViewModel
{
    public class CustomerVM
    {
        private Klant _customer;
        private NAW_KlantVM _NAW_Customer;
        private ObservableCollection<ContactPersonVM> _ContactPersons;

        public NAW_KlantVM NAW_Klant
        {
            get { return _NAW_Customer; }
            set { _NAW_Customer = value; }
        }
        public ObservableCollection<ContactPersonVM> ContactPersons
        {
            get { return _ContactPersons; }
            set { _ContactPersons = value; }
        }
        public string CompanyName
        {
            get { return _customer.Bedrijfsnaam; }
            set { _customer.Bedrijfsnaam = value; }
        }
        public CustomerVM(Klant customer)
        {
            _customer = customer;
            _NAW_Customer = new NAW_KlantVM(_customer.NAW_Klant);
        }
        public CustomerVM()
        {
            _customer = new Klant();
            _NAW_Customer = new NAW_KlantVM();
        }
        public Klant CustomerData
        {
            get
            {
                return _customer;
            }
        }
    }
}
