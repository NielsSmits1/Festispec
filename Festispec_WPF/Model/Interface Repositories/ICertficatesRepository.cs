﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Model.Interface_Repositories
{
    public interface ICertficatesRepository : IRepository<Certificaat>
    {
        List<Certificaat> GetCertificatesFromInspectorID(int id);
    }
}