﻿using System;
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
using System.Windows;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Festispec_WPF.ViewModel
{
    public class CRCustomerVM : ViewModelBase
    {
        private UnitOfWork UOW;
        private ObservableCollection<CustomerVM> _customers;
        private CustomerCreateWindow _customerCreateWindow;
        private CustomerCrudWindow _customerCrudWindow;

        public ICommand CustomerCreateCommand { get; set; }
        public ICommand ViewCreateCustomer { get; set; }
        public ICommand ViewCreateContactPerson { get; set; }
        public ICommand CreateCustomerCommand { get; set; }
        public ICommand CloseCreateCustomerCommand { get; set; }
        public ICommand EditCustomerCommand { get; set; }

        #region visibility properties
        private string _viewAddCustomer;

        public string ViewAddCustomer
        {
            get { return _viewAddCustomer; }
            set
            {
                _viewAddCustomer = value;
                base.RaisePropertyChanged();
            }
        }

        private string _viewAddContactPerson;

        public string ViewAddContactPerson
        {
            get { return _viewAddContactPerson; }
            set
            {
                _viewAddContactPerson = value;
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

            CustomerCreateCommand = new RelayCommand(OpenCustomerCreateWindow);
            ViewCreateCustomer = new RelayCommand(ViewCustomerCreate);
            ViewCreateContactPerson = new RelayCommand(ViewContactPersonCreate);
            CreateCustomerCommand = new RelayCommand(AddCustomer);
            CloseCreateCustomerCommand = new RelayCommand(CloseCreateCustomerWindow);
            EditCustomerCommand = new RelayCommand(OpenEditCustomerWindow);

            SearchText = "Zoek klant";

            ViewCustomerCreate();
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

        private void CloseCreateCustomerWindow()
        {
            var currentWindow = System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().SingleOrDefault(x => x.IsActive);
            _customerCrudWindow = new CustomerCrudWindow();
            _customerCrudWindow.Show();
            currentWindow.Close();
        }

        private void AddCustomer()
        {
            UOW.NAWCustomers.Add(NewCustomer.NAW_Klant.toModel());

            UOW.Customers.Add(NewCustomer.CustomerData);

            UOW.ContactPersons.Add(NewcontactPerson.ContactPersonData);

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

            var currentWindow = System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().SingleOrDefault(x => x.IsActive);
            _customerCrudWindow = new CustomerCrudWindow();
            _customerCrudWindow.Show();
            currentWindow.Close();
        }

        private void ViewContactPersonCreate()
        {
            ViewAddCustomer = "Hidden";
            ViewAddContactPerson = "Visible";
        }

        private void ViewCustomerCreate()
        {
            ViewAddCustomer = "Visible";
            ViewAddContactPerson = "Hidden";
        }

        private void OpenCustomerCreateWindow()
        {
            var currentWindow = System.Windows.Application.Current.Windows.OfType<System.Windows.Window>().SingleOrDefault(x => x.IsActive);
            _customerCreateWindow = new CustomerCreateWindow();
            _customerCreateWindow.Show();
            currentWindow.Close();
        }
    }
}