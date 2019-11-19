using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Model.Repositories
{
    public class InspectorRepository : Repository<Inspecteur>, IInspectorRepository
    {

        public InspectorRepository(FestiSpecEntities context) : base(context)
        {
           
        }

        public Inspecteur GetValidatedInspector(int id)
        {
            return Context.Inspecteur.Include(a => a.Certificaat).SingleOrDefault(a => a.ID == id);
        }
    }
}
