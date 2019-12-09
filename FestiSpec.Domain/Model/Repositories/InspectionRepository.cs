using FestiSpec.Domain.Model;
using FestiSpec.Domain.Model.Interface_Repositories;
using FestiSpec.Domain.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiSpec.Domain.Model.Repositories
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

        public IEnumerable<Certificaat> GetCertificatesByInspection(int id)
        {
            return GetAll().FirstOrDefault(ins => ins.Inspectienummer == id).Certificaat;
        }
    }
}
