using Festispec_WPF.Model.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Model.Repositories
{
    public class InspectionRepository : Repository<Inspectie>, IInspectionRepository
    {
        public InspectionRepository(FestiSpecEntities context) : base(context)
        {

        }

        public void AddInspectorToInspection(Inspectie inspection, Inspecteur inspecteur)
        {
            Get(inspection.Inspectienummer).Inspecteur.Add(inspecteur);
        }
    }
}
