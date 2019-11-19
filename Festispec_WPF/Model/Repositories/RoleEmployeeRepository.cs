using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec_WPF.Model.Interface_Repositories;

namespace Festispec_WPF.Model.Repositories
{
    public class RoleEmployeeRepository : Repository<Rol_werknemer>, IRoleEmployee
    {
        public RoleEmployeeRepository(FestiSpecEntities context) : base(context)
        {
        }
    }
}
