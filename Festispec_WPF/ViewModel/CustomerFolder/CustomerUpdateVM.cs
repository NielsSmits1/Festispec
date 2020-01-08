using FestiSpec.Domain.Model;
using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using Application = System.Windows.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Festispec_WPF.ViewModel
{
    public class CustomerUpdateVM : ViewModelBase
    {
        private CRCustomerVM _crCustomerVM;

        private CustomerCrudWindow _customerCrudWindow;
        private UnitOfWork UOW;
        private ContactPersonCreateWindow _contactPersonCreateWindow;
        private CustomerUpdateWindow _customerUpdateWindow;

        public CustomerVM SelectedCustomer { get; set; }
        public ObservableCollection<ContactPersonVM> ContactPersons { get; set; }
        public ContactPersonVM NewcontactPerson { get; set; }
        public CustomerVM NewCustomer { get; set; }

        private ContactPersonVM _selectedContactPerson;

        public ContactPersonVM SelectedContactPerson
        {
            get { return _selectedContactPerson; }
            set { _selectedContactPerson = value; RaisePropertyChanged(() => SelectedContactPerson); }
        }

        public ICommand ShowCustomerCommand { get; set; }
        public ICommand ShowContactpersonCommand { get; set; }
        public ICommand EditContactPersonCommand { get; set; }

        public ICommand CancelCustomerCommand { get; set; }
        public ICommand SubmitCustomerCommand { get; set; }

        public ICommand CloseCreateContactPersonCommand { get; set; }
        public ICommand OpenCreateContactPersonCommand { get; set; } 
        public ICommand CancelContactPersonCommand { get; set; }
        public ICommand SubmitEditedContactPersonCommand { get; set; }
        public ICommand CreateContactPersonCommand { get; set; }
        public ICommand DeleteContactPersonCommand { get; set; }

        #region Visibility Properties
        private string _customerVisibility;

        public string CustomerVisibility
        {
            get { return _customerVisibility; }
            set
            {
                _customerVisibility = value;
                base.RaisePropertyChanged();
            }
        }

        private string _contactPersonVisibility;

        public string ContactPersonVisibility   
        {
            get { return _contactPersonVisibility; }
            set 
            { 
                _contactPersonVisibility = value;
                base.RaisePropertyChanged();
            }
        }

        private string _customerUpdateVisibility;

        public string CustomerUpdateVisibility
        {
            get { return _customerUpdateVisibility; }
            set
            {
                _customerUpdateVisibility = value;
                base.RaisePropertyChanged();
            }
        }

        private string _contactPersonUpdateVisibility;

        public string ContactPersonUpdateVisibility
        {
            get { return _contactPersonUpdateVisibility; }
            set
            {
                _contactPersonUpdateVisibility = value;
                base.RaisePropertyChanged();
            }
        }
        #endregion

        private string _searchText;

        public string SearchText
        {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
                base.RaisePropertyChanged();
            }
        }


        public CustomerUpdateVM(CRCustomerVM customer)
        {
            UOW = ViewModelLocator.UOW;
            _crCustomerVM = customer;
            SelectedCustomer = _crCustomerVM.SelectedCustomer;

            NewCustomer = new CustomerVM();
            NewcontactPerson = new ContactPersonVM();

            NewcontactPerson.customer = NewCustomer;

            LoadContactPersons();

            _customerCrudWindow = new CustomerCrudWindow();

            ShowCustomerCommand = new RelayCommand(ShowCustomer);
            ShowContactpersonCommand = new RelayCommand(ShowContactPerson);
            EditContactPersonCommand = new RelayCommand(ShowEditContactPerson);
            CancelCustomerCommand = new RelayCommand(CancelCustomer);
            SubmitCustomerCommand = new RelayCommand(SubmitCustomer);

            CloseCreateContactPersonCommand = new RelayCommand(CloseCreateContactPerson);
            OpenCreateContactPersonCommand = new RelayCommand(ShowCreateContactPerson);
            CancelContactPersonCommand = new RelayCommand(ShowContactPerson);
            SubmitEditedContactPersonCommand = new RelayCommand(SubmitContactPerson);
            CreateContactPersonCommand = new RelayCommand(CreateContactPerson);
            DeleteContactPersonCommand = new RelayCommand(DeleteContactPerson);

            SearchText = "Zoek klant";
            UOW.Complete();

            ShowCustomer();
        }

        private void DeleteContactPerson()
        {
            var tempcontactperson = UOW.Context.Contactpersoon.Find(SelectedContactPerson.ContactPersonData.ID);
            tempcontactperson.Actief = false;

            try
            {
                UOW.Complete();
                MessageBox.Show("De aanpassingen zijn doorgevoerd", "De aanpassingen zijn doorgevoerd",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowContactPerson();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            LoadContactPersons();
            RaisePropertyChanged("ContactPersons");
        }

        private void SubmitContactPerson()
        {
            var contactpersonid = SelectedContactPerson.ContactPersonData.ID;

            var databaseCustomer = UOW.ContactPersons.Find(t => t.ID == contactpersonid).First();

            databaseCustomer = SelectedContactPerson.ContactPersonData;

            try
            {
                UOW.Complete();
                MessageBox.Show("De aanpassingen zijn doorgevoerd", "De aanpassingen zijn doorgevoerd",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ShowContactPerson();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void CloseCreateContactPerson()
        {
            var currentWindow = System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().SingleOrDefault(x => x.IsActive);
            _customerUpdateWindow = new CustomerUpdateWindow();
            _customerUpdateWindow.Show();
            currentWindow.Close();
        }

        private void CreateContactPerson()
        {
            NewcontactPerson.ContactPersonData.Klant = SelectedCustomer.CustomerData;
            NewcontactPerson.ContactPersonData.Actief = true;
            UOW.ContactPersons.Add(NewcontactPerson.ContactPersonData);
            try
            {
                UOW.Complete();
                MessageBox.Show("De aanpassingen zijn doorgevoerd", "De aanpassingen zijn doorgevoerd",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
                CloseCreateContactPerson();
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

        private void ShowCreateContactPerson()
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            _contactPersonCreateWindow = new ContactPersonCreateWindow();
            _contactPersonCreateWindow.Show();
            currentWindow.Close();
        }

        private void SubmitCustomer()
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
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

            _customerCrudWindow.Show();
            currentWindow.Close();
        }

        private void CancelCustomer()
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            _customerCrudWindow.Show();
            currentWindow.Close();
        }

        private void LoadContactPersons()
        {
            var customerid = SelectedCustomer.CustomerData.ID;
            var list = UOW.Customers.GetContactPersons(customerid).Select(Contactpersoon => new ContactPersonVM(Contactpersoon));
            ContactPersons = new ObservableCollection<ContactPersonVM>(list);
        }

        private void ShowContactPerson()
        {
            CustomerUpdateVisibility = "Visible";
            CustomerVisibility = "Hidden";
            ContactPersonVisibility = "Visible";

            ContactPersonUpdateVisibility = "Hidden";
        }

        private void ShowCustomer()
        {
            CustomerUpdateVisibility = "Visible";
            CustomerVisibility = "Visible";
            ContactPersonVisibility = "Hidden";

            ContactPersonUpdateVisibility = "Hidden";
        }

        private void ShowEditContactPerson()
        {
            CustomerUpdateVisibility = "Hidden";
            CustomerVisibility = "Hidden";
            ContactPersonVisibility = "Hidden";

            ContactPersonUpdateVisibility = "Visible";
        }
    }
}
