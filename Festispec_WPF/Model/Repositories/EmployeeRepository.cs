using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec_WPF.Model.Interface_Repositories;

namespace Festispec_WPF.Model.Repositories
{
    public class EmployeeRepository : Repository<Werknemer>, IEmployeeRepository
    {
        public EmployeeRepository(FestiSpecEntities context) : base(context)
        {
            
        }
    }
}
