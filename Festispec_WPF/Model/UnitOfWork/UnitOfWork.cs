using Festispec_WPF.Model.Interface_Repositories;
using Festispec_WPF.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Model.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FestiSpecEntities _context;

        public UnitOfWork(FestiSpecEntities context)
        {
            _context = context;
            Inspectors = new InspectorRepository(_context);
            NawEmployee = new NAWEmployeeRepository(context);
            Employee = new EmployeeRepository(context);
            RoleEmployee = new RoleEmployeeRepository(context);
            NAWInspectors = new NAWInspector_Repository(_context);
            Customers = new CustomerRepository(_context);
            ContactPersons = new ContactPersonRepository(_context);
            PhonenumberInspectors = new PhonenumberInspectorRepository(_context);
            Certificates = new CertificatesRepository(_context);
        }

        public FestiSpecEntities Context => _context;

        public IInspectorRepository Inspectors { get; private set; }
        public INAWEmployeeRepository NawEmployee { get; private set; }
        public IEmployeeRepository Employee { get; private set; }
        public IRoleEmployee RoleEmployee { get; private set; }
        public INAWInspectorRepository NAWInspectors { get; private set; }
        public IContactPersonRepository ContactPersons { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IPhonenumberInspectorRepository PhonenumberInspectors { get; private set; }
        public ICertficatesRepository Certificates { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
