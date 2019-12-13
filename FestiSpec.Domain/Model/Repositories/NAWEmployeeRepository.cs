using FestiSpec.Domain.Model.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiSpec.Domain.Model.Repositories
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
