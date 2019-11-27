using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec_WPF.Model.Interface_Repositories;

namespace Festispec_WPF.Model.Repositories
{
    public class NAWEmployeeRepository : Repository<NAW_werknemer>, INAWEmployeeRepository
    {
        public NAWEmployeeRepository(FestiSpecEntities context) : base(context)
        {
            {

            }
        }
    }
}
