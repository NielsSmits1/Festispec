using FestiSpec.Domain.Model;
using FestiSpec.Domain.Model.Interface_Repositories;
using FestiSpec.Domain.Model.Repositories;
using System;
using System.Collections.Generic;
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

            return Context.Contactpersoon.Where(a => a.Klant_ID == id && a.Actief == true).ToList();
        }

    }
}
