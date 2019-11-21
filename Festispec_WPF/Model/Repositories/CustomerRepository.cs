using Festispec_WPF.Model.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Model.Repositories
{
    public class CustomerRepository : Repository<Klant>, ICustomerRepository
    {
        public CustomerRepository(FestiSpecEntities context) : base(context)
        {
        }

            public List<Contactpersoon> GetContactPersons(int id)
            {
                return Context.Contactpersoon.Where(a => a.Klant_ID == id).ToList();
            }
        
    }
}
