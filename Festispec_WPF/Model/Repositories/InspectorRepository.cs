using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Model.Repositories
{
    public class InspectorRepository : Repository<Inspecteur>, IInspectorRepository
    {

        public InspectorRepository(FestiSpecEntities context) : base(context)
        {
           
        }

        public Inspecteur GetValidatedInspector(int id)
        {
            return Context.Inspecteur.Include(a => a.Certificaat).SingleOrDefault(a => a.ID == id);
        }

        public List<Certificaat> GetCertificatesInspector(int id)
        {
            return Context.Inspecteur.Find(id).Certificaat.ToList();
        }

        public List<Certificaat> GetMissingCertificates(int id)
        {
            var CertificatesInspector = Context.Inspecteur.Find(id).Certificaat;
            List<Certificaat> certificates = Context.Certificaat.ToList();
            List<Certificaat> final = new List<Certificaat>();
            foreach(var certificate in certificates)
            {
                if (!CertificatesInspector.Contains(certificate))
                {
                    final.Add(certificate);
                }
            }

            return final;
        }

        public override void Add(Inspecteur entity)
        {
            entity.Actief = true;
            base.Add(entity);
        }

        public void SetInspectorInactive(int id)
        {
            var spec = Find(ins => ins.NAW == id).FirstOrDefault();

            if (spec.Actief)
            {
                spec.Actief = false;
            }
            else
            {
                spec.Actief = true;
            }
        }


    }
}
