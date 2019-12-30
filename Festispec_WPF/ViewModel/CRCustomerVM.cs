using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using Festispec_WPF.View;
using Festispec_WPF.Model.UnitOfWork;
using FestiSpec.Domain.Model;

namespace Festispec_WPF.ViewModel
{
    public class CRCustomerVM : ViewModelBase
    {
        private ContactPersonUpdateWindow _contactPersonUpdateWindow;
        private ContactPersonCreateWindow _contactPersonCreateWindow;
        private UnitOfWork UOW;
        private ObservableCollection<CustomerVM> _customers;

        public ICommand CreateCustomer { get; set; }
        public ICommand EditCustomer { get; set; }
        public ICommand CreateCustomerCommand { get; set; }
        public ICommand EditCustomerCommand { get; set; }
        public ICommand AddCustomerCommand { get; set; }
        public ICommand EditCustomerWindowCommand { get; set; }
        public ICommand EditContactPersonWindowCommand { get; set; }
        public ICommand EditCustomerDataCommand { get; set; }
        public ICommand AddContactPersonCommand { get; set; }
        public ICommand EditContactPersonDataCommand { get; set; }
        public ICommand OpenContactPersonCreateWindow { get; set; }
        public ICommand DeleteContactPersonCommand { get; set; }

        public CustomerVM NewCustomer { get; set; }
        public CustomerVM BeforeChangeCustomer { get; set; }
        public CustomerVM SelectedCustomer { get; set; }
        public ContactPersonVM SelectedContactPerson { get; set; }
        public ContactPersonVM NewcontactPerson { get; set; }

        public ObservableCollection<CustomerVM> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                _customers = value;
                RaisePropertyChanged("Customers");
            }
        }

        public CRCustomerVM()
        {
            UOW = ViewModelLocator.UOW;       

            NewCustomer = new CustomerVM();
            NewcontactPerson = new ContactPersonVM();

            NewcontactPerson.customer = NewCustomer; 

            
            UOW.Complete();

            Customers = new ObservableCollection<CustomerVM>(UOW.Customers.GetAll().ToList().Select(a => new CustomerVM(a)));

            AddCustomerCommand = new RelayCommand(AddCustomer);
            EditCustomerCommand = new RelayCommand(OpenEditCustomerWindow);
            EditCustomerDataCommand = new RelayCommand(EditCustomerData);
            AddContactPersonCommand = new RelayCommand(AddContactPerson);
            OpenContactPersonCreateWindow = new RelayCommand(AddContactPersonWindow);
            EditContactPersonWindowCommand = new RelayCommand(EditContactPersonWindow);
            EditContactPersonDataCommand = new RelayCommand(EditContactPersonData);
            DeleteContactPersonCommand = new RelayCommand(DeleteContactPerson);
        }

        private void EditContactPersonData()
        {
            var contactpersonid = SelectedContactPerson.ContactPersonData.ID;

            var databaseCustomer = UOW.ContactPersons.Find(t => t.ID == contactpersonid).First();

            databaseCustomer = SelectedContactPerson.ContactPersonData;

            try
            {
                UOW.Complete();
                MessageBox.Show("De aanpassingen zijn doorgevoerd", "De aanpassingen zijn doorgevoerd",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _contactPersonUpdateWindow.Close();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void EditCustomerData()
        {
            var customerid = SelectedCustomer.CustomerData.ID;
            var databaseCustomer = UOW.Customers.Find(t => t.ID == customerid).First();

            databaseCustomer = SelectedCustomer.CustomerData;
            
            try
            {
                UOW.Complete();
                MessageBox.Show("De aanpassingen zijn doorgevoerd", "De aanpassingen zijn doorgevoerd",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            UOW.Context.Entry<NAW_Klant>(SelectedCustomer.NAW_Klant.NawData).Reload();
            UOW.Context.Entry<Klant>(SelectedCustomer.CustomerData).Reload();

            RaisePropertyChanged(() => SelectedCustomer);


        }
        private void AddContactPerson()
        {
            NewcontactPerson.ContactPersonData.Klant = SelectedCustomer.CustomerData;
            NewcontactPerson.ContactPersonData.Actief = true;
            UOW.ContactPersons.Add(NewcontactPerson.ContactPersonData);
            try
            {
                UOW.Complete();
                MessageBox.Show("De aanpassingen zijn doorgevoerd", "De aanpassingen zijn doorgevoerd",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                _contactPersonCreateWindow.Close();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            NewcontactPerson = new ContactPersonVM();
            RaisePropertyChanged(() => NewcontactPerson);
        }
        private void AddContactPersonWindow()
        {
            _contactPersonCreateWindow = new ContactPersonCreateWindow();
            _contactPersonCreateWindow.Show();
        }
        private void OpenEditCustomerWindow()
        {

            var customerid = SelectedCustomer.CustomerData.ID;
            var list = UOW.Customers.GetContactPersons(customerid).Select(Contactpersoon => new ContactPersonVM(Contactpersoon));
            SelectedCustomer.ContactPersons = new ObservableCollection<ContactPersonVM>(list);

            var currentWindow = System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().SingleOrDefault(x => x.IsActive);

            CustomerUpdateWindow customerUpdateWindow = new CustomerUpdateWindow();
            customerUpdateWindow.Show();
            currentWindow.Close();
        }
        private void EditContactPersonWindow()
        {
            _contactPersonUpdateWindow = new ContactPersonUpdateWindow();
            _contactPersonUpdateWindow.Show();
        }

        private void AddCustomer()
        {

            UOW.NAWCustomers.Add(NewCustomer.NAW_Klant.toModel());
            
            UOW.Customers.Add(NewCustomer.CustomerData);

            UOW.ContactPersons.Add(NewcontactPerson.ContactPersonData);


            try
            {
                UOW.Complete();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Customers = new ObservableCollection<CustomerVM>(UOW.Customers.GetAll().ToList().Select(a => new CustomerVM(a)));
            NewCustomer = new CustomerVM();
            NewCustomer.NAW_Klant = new NAW_KlantVM();
            NewCustomer.ContactPersons = new ObservableCollection<ContactPersonVM>();
            NewcontactPerson = new ContactPersonVM();
            NewcontactPerson.customer = NewCustomer;
            RaisePropertyChanged(() => NewCustomer);
            RaisePropertyChanged(() => NewCustomer.NAW_Klant);
            RaisePropertyChanged(() => NewCustomer.ContactPersons);
            RaisePropertyChanged(() => NewcontactPerson);
            
        }

        public void DeleteContactPerson()
        {
            UOW.Context.Contactpersoon.Find(SelectedContactPerson.ContactPersonData.ID).Actief = false;

            try
            {
                UOW.Complete();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var list = UOW.Customers.GetContactPersons(SelectedCustomer.ID).Select(Contactpersoon => new ContactPersonVM(Contactpersoon));
            SelectedCustomer.ContactPersons = new ObservableCollection<ContactPersonVM>(list);
            RaisePropertyChanged("SelectedCustomer");
        }
    }
}
