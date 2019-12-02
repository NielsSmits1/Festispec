using Festispec_WPF.Model.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Model.Repositories
{
    public class LocationRepository : Repository<Locatie>, ILocationRepository
    {

        public LocationRepository(FestiSpecEntities context) : base(context)
        {

        }

        
    }
}
