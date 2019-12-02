using FestiSpec.Domain.Model.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiSpec.Domain.Model.Repositories
{
    public class NAWInspector_Repository : Repository<NAW_inspecteur>, INAWInspectorRepository
    {

        public NAWInspector_Repository(FestiSpecEntities context) : base(context)
        {

        }

        public IEnumerable<NAW_inspecteur> ListOfInactiveInspectors
        {
            get
            {
                var inactive = Context.Inspecteur.Where(ins => ins.Actief == false).Select(ins => ins.NAW).ToList();
                
                return Context.NAW_inspecteur.Where(ins => inactive.Contains(ins.ID)).ToList();
            }
        }

        public IEnumerable<NAW_inspecteur> ListOfActiveInspectors
        {
            get
            {
                var active = Context.Inspecteur.Where(ins => ins.Actief == true).Select(ins => ins.NAW).ToList();
                return Context.NAW_inspecteur.Where(ins => active.Contains(ins.ID)).ToList();
            }
        }

        public IEnumerable<NAW_inspecteur> ListOfLicensedInspectors
        {
            get
            {
                var licensed = Context.Inspecteur.Where(ins => ins.Certificaat.Count > 0).Select(ins => ins.NAW).ToList();
                return Context.NAW_inspecteur.Where(ins => licensed.Contains(ins.ID)).ToList();
            }
        }
    }
}
