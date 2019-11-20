using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Model
{
    public interface IInspectorRepository : IRepository<Inspecteur>
    {
        Inspecteur GetValidatedInspector(int id);

        List<Certificaat> GetCertificatesInspector(int id);

        List<Certificaat> GetMissingCertificates(int id);

        void SetInspectorInactive(int id);

    }
}
