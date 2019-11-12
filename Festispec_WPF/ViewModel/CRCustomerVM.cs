using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec_WPF.ViewModel
{
    public class CRCustomerVM
    {
        public ICommand CreateCustomer { get; set; }
        public ICommand EditCustomer { get; set; }
        public CustomerVM NewCustomer { get; set; }
        public ContactPersonVM NewcontactPerson { get; set; }
        public ObservableCollection<CustomerVM> Customers { get; set; }
        public CRCustomerVM()
        {
            NewCustomer = new CustomerVM();
            NewcontactPerson = new ContactPersonVM();
            NewcontactPerson.customer = NewCustomer; 

        }

    }
}
