using FestiSpec.Domain.Model;

namespace Festispec_WPF.ViewModel
{
    public class ContactPersonVM
    {
        private Contactpersoon _contactPerson;
        private CustomerVM _customer;
        public string FirstName
        {
            get => _contactPerson.Voornaam;
            set => _contactPerson.Voornaam = value;
        }
        public string Affix
        {
            get => _contactPerson.Tussenvoegsel;
            set => _contactPerson.Tussenvoegsel = value;
        }
        public string LastName
        {
            get => _contactPerson.Achternaam;
            set => _contactPerson.Achternaam = value;
        }
        public string PhoneNumber
        {
            get => _contactPerson.Telefoonnummer;
            set => _contactPerson.Telefoonnummer = value;
        }
        public string Email
        {
            get => _contactPerson.Email;
            set => _contactPerson.Email = value;
        }
        public CustomerVM customer
        {
            get => _customer;
            set => _customer = value;
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
            get => _contactPerson;
            set => _contactPerson = value;
        }
    }
}
