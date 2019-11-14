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
            NAWInspectors = new NAWInspector_Repository(_context);
        }

        public FestiSpecEntities Context
        {
            get
            {
                return _context;
            }
        }
        public IInspectorRepository Inspectors { get; private set; }
        public INAWInspectorRepository NAWInspectors { get; private set; }

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
