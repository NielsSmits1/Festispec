using Festispec_WPF.Model.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Model.Repositories
{
    public class ContactPersonRepository : Repository<Contactpersoon>, IContactPersonRepository
    {
        public ContactPersonRepository(FestiSpecEntities context) : base(context)
        {
        }
    }
}
