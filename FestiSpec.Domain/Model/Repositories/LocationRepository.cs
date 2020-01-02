
using FestiSpec.Domain.Model.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiSpec.Domain.Model.Repositories
{
    public class LocationRepository : Repository<Locatie>, ILocationRepository
    {

        public LocationRepository(FestiSpecEntities1 context) : base(context)
        {

        }

        
    }
}
