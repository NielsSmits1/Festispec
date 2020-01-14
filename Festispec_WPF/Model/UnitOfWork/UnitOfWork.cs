using FestiSpec.Domain.Model;
using FestiSpec.Domain.Model.Interface_Repositories;
using FestiSpec.Domain.Model.Repositories;
using Festispec_WPF.Model.Repositories;

namespace Festispec_WPF.Model.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork()
        {
            if (Context == null) Context = new FestiSpecEntities();

            Inspectors = new InspectorRepository(Context);
            NawEmployee = new NAWEmployeeRepository(Context);
            Employee = new EmployeeRepository(Context);
            RoleEmployee = new RoleEmployeeRepository(Context);
            NAWInspectors = new NAWInspector_Repository(Context);
            Questionnaires = new QuestionnaireRepository(Context);
            Inspections = new InspectionRepository(Context);
            InspectionLocations = new LocationRepository(Context);
            Customers = new CustomerRepository(Context);
            ContactPersons = new ContactPersonRepository(Context);
            Certificates = new CertificatesRepository(Context);
            NAWCustomers = new NAWCustomerRepository(Context);
        }

        public FestiSpecEntities Context { get; }
        public IInspectorRepository Inspectors { get; }
        public INAWEmployeeRepository NawEmployee { get; }
        public IEmployeeRepository Employee { get; }
        public IRoleEmployee RoleEmployee { get; }
        public INAWInspectorRepository NAWInspectors { get; }
        public IQuestionnaireRepository Questionnaires { get; }
        public ICertficatesRepository Certificates { get; }
        public IInspectionRepository Inspections { get; }
        public ILocationRepository InspectionLocations { get; }
        public IContactPersonRepository ContactPersons { get; }
        public ICustomerRepository Customers { get; }
        public INAWCustomerRepository NAWCustomers { get; }

        public int Complete()
        {
            return Context.SaveChanges();
        }
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}