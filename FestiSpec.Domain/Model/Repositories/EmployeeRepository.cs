using FestiSpec.Domain.Model.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiSpec.Domain.Model.Repositories
{
    public class EmployeeRepository : Repository<Werknemer>, IEmployeeRepository
    {
        public EmployeeRepository(FestiSpecEntities1 context) : base(context)
        {
            
        }

        public Werknemer GetEmployeeByNAW(int id)
        {
            return Context.Werknemer.FirstOrDefault(w => w.NAW == id);
        }

        public List<Werknemer> GetAllManagers()
        {
            return GetAll().Where(emp => emp.Rol == "Manager").ToList();
        }

        public Werknemer FindUsername(string username)
        {
            return Context.Werknemer.FirstOrDefault(w => w.Username == username);
        }
    }
}
