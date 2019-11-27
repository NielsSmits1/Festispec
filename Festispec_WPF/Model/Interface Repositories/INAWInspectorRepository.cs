using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Model.Interface_Repositories
{
    public interface INAWInspectorRepository : IRepository<NAW_inspecteur>
    {

        IEnumerable<NAW_inspecteur> ListOfActiveInspectors { get; }

        IEnumerable<NAW_inspecteur> ListOfInactiveInspectors { get; }

        IEnumerable<NAW_inspecteur> ListOfLicensedInspectors { get; }
    }


}
