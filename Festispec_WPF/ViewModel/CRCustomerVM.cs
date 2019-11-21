using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using Festispec_WPF.Model.UnitOfWork;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Festispec_WPF.View;
using Festispec_WPF.Model.Repositories;
using Festispec_WPF.Model;

namespace Festispec_WPF.ViewModel
{
    public class CRCustomerVM: ViewModelBase
    {

        public ICommand CreateCustomer { get; set; }
        public ICommand EditCustomer { get; set; }

        private UnitOfWork UOW;
        private ObservableCollection<CustomerVM> _customers;
        public ICommand CreateCustomerCommand { get; set; }
        public ICommand EditCustomerCommand { get; set; }
        public ICommand AddCustomerCommand { get; set; }

        public ICommand EditCustomerWindowCommand { get; set; }
        public ICommand EditContactPersonWindowCommand { get; set; }
        public ICommand EditCustomerDataCommand { get; set; }
        public ICommand AddContactPersonCommand { get; set; }
        public ICommand EditContactPersonDataCommand { get; set; }
        public ICommand OpenContactPersonCreateWindow { get; set; }

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
            UOW = new ViewModelLocator().UOW;
            
            NewCustomer = new CustomerVM();
            NewcontactPerson = new ContactPersonVM();

            NewcontactPerson.customer = NewCustomer;
            Customers = new ObservableCollection<CustomerVM>();
            var customerslist = UOW.Customers.GetAll().ToList();
            foreach (var item in customerslist)
            {
                Customers.Add(new CustomerVM(item));
            }

            NewcontactPerson.customer = NewCustomer;
            
            UOW.Complete();

            AddCustomerCommand = new RelayCommand(AddCustomer);
            EditCustomerCommand = new RelayCommand(OpenEditCustomerWindow);
            EditCustomerDataCommand = new RelayCommand(EditCustomerData);
            AddContactPersonCommand = new RelayCommand(AddContactPerson);
            OpenContactPersonCreateWindow = new RelayCommand(AddContactPersonWindow);
            EditContactPersonWindowCommand = new RelayCommand(EditContactPersonWindow);
            EditContactPersonDataCommand = new RelayCommand(EditContactPersonData);
        }

        private void EditContactPersonData()
        {
            var contactpersonid = SelectedContactPerson.ContactPersonData.ID;
            var databaseCustomer = UOW.ContactPerson.Find(t => t.ID == contactpersonid).First();

            databaseCustomer = SelectedContactPerson.ContactPersonData;

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
                SelectedCustomer = BeforeChangeCustomer;
                return;
            }

        }
        private void AddContactPerson()
        {
            NewcontactPerson.ContactPersonData.Klant = SelectedCustomer.CustomerData;

            UOW.ContactPerson.Add(NewcontactPerson.ContactPersonData);
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
                return;
            }
            
            NewcontactPerson = new ContactPersonVM();
            RaisePropertyChanged(() => NewcontactPerson);
        }
        private void AddContactPersonWindow()
        {
            ContactPersonCreateWindow contactPersonCreateWindow = new ContactPersonCreateWindow();
            contactPersonCreateWindow.Show();
        }
        private void OpenEditCustomerWindow()
        {
            //BeforeChangeCustomer = new CustomerVM(SelectedCustomer.CustomerData);

            var customerid = SelectedCustomer.CustomerData.ID;
            var list = UOW.Customers.GetContactPersons(customerid).Select(Contactpersoon => new ContactPersonVM(Contactpersoon));
            SelectedCustomer.ContactPersons = new ObservableCollection<ContactPersonVM>(list);

            

            CustomerUpdateWindow customerUpdateWindow = new CustomerUpdateWindow();
            customerUpdateWindow.Show();
        }
        private void EditContactPersonWindow()
        {
            SelectedCustomer = new CustomerVM(UOW.Customers.Find(t => t.ID == SelectedCustomer.CustomerData.ID).First());
            ContactPersonUpdateWindow contactPersonUpdateWindow = new ContactPersonUpdateWindow();
            contactPersonUpdateWindow.Show();
        }

        private void AddCustomer()
        {

            Repository<NAW_Klant> NAW = new Repository<NAW_Klant>(UOW.Context);

            NAW.Add(NewCustomer.NAW_Klant.toModel());
            
            UOW.Customers.Add(NewCustomer.CustomerData);

            Repository<Contactpersoon> ContactPerson = new Repository<Contactpersoon>(UOW.Context);
            ContactPerson.Add(NewcontactPerson.ContactPersonData);


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

    }
}