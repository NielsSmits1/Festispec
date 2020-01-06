using FestiSpec.Domain.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiSpec.Domain.Model.Interface_Repositories
{
    public interface ICustomerRepository : IRepository<Klant>
    {
        List<Contactpersoon> GetContactPersons(int id);
    }
}
