using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Model.Interface_Repositories
{
    public interface ICustomerRepository : IRepository<Klant>
    {

    List<Contactpersoon> GetContactPersons(int id);
    }
}
