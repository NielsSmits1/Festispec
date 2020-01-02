using FestiSpec.Domain.Model;
using Festispec_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.ViewModel
{
    public class ContactPersonVM
    {
        private Contactpersoon _contactPerson;
        private CustomerVM _customer;
        public string FirstName
        {
            get { return _contactPerson.Voornaam; }
            set { _contactPerson.Voornaam = value; }
        }
        public string Affix
        {
            get { return _contactPerson.Tussenvoegsel; }
            set { _contactPerson.Tussenvoegsel = value; }
        }
        public string LastName
        {
            get { return _contactPerson.Achternaam; }
            set { _contactPerson.Achternaam = value; }
        }
        public string PhoneNumber
        {
            get { return _contactPerson.Telefoonnummer; }
            set { _contactPerson.Telefoonnummer = value; }
        }
        public string Email
        {
            get { return _contactPerson.Email; }
            set { _contactPerson.Email = value; }
        }
        public CustomerVM customer
        {
            get { return _customer; }
            set { _customer = value; }
        }
        public ContactPersonVM(Contactpersoon cp)
        {
            _contactPerson = cp;
            _customer = new CustomerVM(cp.Klant);
        }
        public ContactPersonVM()
        {
            _contactPerson = new Contactpersoon();
            _customer = new CustomerVM();
        }


        public Contactpersoon ContactPersonData
        {
            get
            {
                return _contactPerson;
            }
            set
            {
                _contactPerson = value;
            }
        }

    }
}
