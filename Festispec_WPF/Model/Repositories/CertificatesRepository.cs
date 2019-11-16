using Festispec_WPF.Model.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Model.Repositories
{
    public class CertificatesRepository : Repository<Certificaat>, ICertficatesRepository
    {
        public CertificatesRepository(FestiSpecEntities context) : base(context)
        {

        }

        public List<Certificaat> GetCertificatesFromInspectorID(int id)
        {
            List<Certificaat> InspectorCertificates = new List<Certificaat>();
            return InspectorCertificates;
        }
    }
}
