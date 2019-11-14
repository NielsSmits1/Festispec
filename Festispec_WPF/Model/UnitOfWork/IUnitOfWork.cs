using Festispec_WPF.Model.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Model.UnitOfWork
{
    interface IUnitOfWork : IDisposable
    {
        IInspectorRepository Inspectors { get; }
        INAWInspectorRepository NAWInspectors { get;}
        int Complete();
    }
}
