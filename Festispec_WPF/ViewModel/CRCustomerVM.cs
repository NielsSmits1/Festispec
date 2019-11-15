using Festispec_WPF.Model;
using Festispec_WPF.Model.Repositories;
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
using System.Windows.Input;

namespace Festispec_WPF.ViewModel
{
    public class CRCustomerVM : ViewModelBase
    {
        private UnitOfWork UOW;
        private ObservableCollection<CustomerVM> _customers;
        public ICommand CreateCustomerCommand { get; set; }
        public ICommand EditCustomerCommand { get; set; }
        public ICommand AddCustomerCommand { get; set; }

        private ICommand EditCustomerWindow { get; set; }
        public CustomerVM NewCustomer { get; set; }
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
            Customers = new ObservableCollection<CustomerVM>(UOW.Customers.GetAll().ToList().Select(a => new CustomerVM(a)));

            NewcontactPerson = new ContactPersonVM();
            NewcontactPerson.customer = NewCustomer;

            UOW.Complete();

            AddCustomerCommand = new RelayCommand(AddCustomer);
            EditCustomerCommand = new RelayCommand(OpenEditCustomerWindow);
        }

        private void OpenEditCustomerWindow()
        {
            CustomerUpdateWindow customerUpdateWindow = new CustomerUpdateWindow();
            customerUpdateWindow.Show();
        }

        private void AddCustomer()
        {

            Repository<NAW_Klant> NAW = new Repository<NAW_Klant>(UOW.Context);

            NAW.Add(NewCustomer.NAW_Klant.toModel());
            
            UOW.Customers.Add(NewCustomer.CustomerData);

            Repository<Contactpersoon> ContactPerson = new Repository<Contactpersoon>(UOW.Context);
            ContactPerson.Add(NewcontactPerson.toModel());




            UOW.Complete();
            Customers = new ObservableCollection<CustomerVM>(UOW.Customers.GetAll().ToList().Select(a => new CustomerVM(a)));

        }

    }
}
