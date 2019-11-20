using Festispec_WPF.Model.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Model.Repositories
{
    public class PhonenumberInspectorRepository : Repository<Telefoonnummer_inspecteur>, IPhonenumberInspectorRepository
    {
        public PhonenumberInspectorRepository(FestiSpecEntities context) : base(context)
        {

        }
    }
}
