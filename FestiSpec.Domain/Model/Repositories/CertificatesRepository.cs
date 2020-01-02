using FestiSpec.Domain.Model.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiSpec.Domain.Model.Repositories
{
    public class CertificatesRepository : Repository<Certificaat>, ICertficatesRepository
    {
        public CertificatesRepository(FestiSpecEntities1 context) : base(context)
        {

        }

    }
}
