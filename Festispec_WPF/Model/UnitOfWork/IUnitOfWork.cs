using FestiSpec.Domain.Model.Interface_Repositories;
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
        INAWEmployeeRepository NawEmployee { get; }
        IEmployeeRepository Employee { get; }
        IRoleEmployee RoleEmployee { get; }
        INAWInspectorRepository NAWInspectors { get; }
        IQuestionnaireRepository Questionnaires { get; }
        ICustomerRepository Customers { get; }
        IContactPersonRepository ContactPersons { get; }
        ICertficatesRepository Certificates { get; }
        IInspectionRepository Inspections { get; }
        ILocationRepository InspectionLocations { get; }
        INAWCustomerRepository NAWCustomers { get; }
        int Complete();
    }
}
