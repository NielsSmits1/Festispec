﻿using FestiSpec.Domain.Model;
using FestiSpec.Domain.Model.Interface_Repositories;
using FestiSpec.Domain.Model.Repositories;
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

        public UnitOfWork()
        {
            if(_context == null)
            {
                _context = new FestiSpecEntities();
            }
            
            Inspectors = new InspectorRepository(_context);
            NawEmployee = new NAWEmployeeRepository(_context);
            Employee = new EmployeeRepository(_context);
            RoleEmployee = new RoleEmployeeRepository(_context);
            NAWInspectors = new NAWInspector_Repository(_context);
            Questionnaires = new QuestionnaireRepository(_context);
            Inspections = new InspectionRepository(_context);
            InspectionLocations = new LocationRepository(_context);
            Customers = new CustomerRepository(_context);
            ContactPersons = new ContactPersonRepository(_context);
            Certificates = new CertificatesRepository(_context);
            NAWCustomers = new NAWCustomerRepository(_context);
        }

        public FestiSpecEntities Context => _context;

        public IInspectorRepository Inspectors { get; private set; }
        public INAWEmployeeRepository NawEmployee { get; private set; }
        public IEmployeeRepository Employee { get; private set; }
        public IRoleEmployee RoleEmployee { get; private set; }
        public INAWInspectorRepository NAWInspectors { get; private set; }
        public IQuestionnaireRepository Questionnaires { get; private set; }
        public ICertficatesRepository Certificates { get; private set; }
        public IInspectionRepository Inspections { get; private set; }
        public ILocationRepository InspectionLocations { get; private set; }
        public IContactPersonRepository ContactPersons { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public INAWCustomerRepository NAWCustomers { get; private set; }

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
