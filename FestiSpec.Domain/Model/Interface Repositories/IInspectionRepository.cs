using FestiSpec.Domain.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiSpec.Domain.Model.Interface_Repositories
{
    public interface IInspectionRepository : IRepository<Inspectie>
    {
        void AddInspectorToInspection(Inspectie inspection, Inspecteur inspecteur);

        IEnumerable<Certificaat> GetCertificatesByInspection(int id);
        List<Certificaat> GetMissingCertificates(int id);
        List<Certificaat> GetCertificatesInspection(int id);

    }
}
