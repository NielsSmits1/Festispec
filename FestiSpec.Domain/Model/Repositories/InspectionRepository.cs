using FestiSpec.Domain.Model;
using FestiSpec.Domain.Model.Interface_Repositories;
using FestiSpec.Domain.Model.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiSpec.Domain.Model.Repositories
{
    public class InspectionRepository : Repository<Inspectie>, IInspectionRepository
    {
        public InspectionRepository(FestiSpecEntities context) : base(context)
        {

        }

        public void AddInspectorToInspection(Inspectie inspection, Inspecteur inspecteur)
        {
            Get(inspection.Inspectienummer).Inspecteur.Add(inspecteur);
        }

        public IEnumerable<Certificaat> GetCertificatesByInspection(int id)
        {
            return GetAll().FirstOrDefault(ins => ins.Inspectienummer == id).Certificaat;
        }

        public List<Certificaat> GetMissingCertificates(int id)
        {
            var CertificatesInspection = Context.Inspectie.Find(id).Certificaat;
            List<Certificaat> certificates = Context.Certificaat.ToList();
            List<Certificaat> final = new List<Certificaat>();
            foreach (var certificate in certificates)
            {
                if (!CertificatesInspection.Contains(certificate))
                {
                    final.Add(certificate);
                }
            }

            return final;
        }

        public List<Vragenlijst> GetMissingQuestionnaires(int id)
        {
            var QuestionInspection = Context.Inspectie.Find(id).Vragenlijst;
            List<Vragenlijst> questionnaires = Context.Vragenlijst.Where(q => q.Actief == true).ToList();
            List<Vragenlijst> final = new List<Vragenlijst>();
            foreach (var question in questionnaires)
            {
                if (!QuestionInspection.Contains(question))
                {
                    final.Add(question);
                }
            }

            return final;
        }


        public List<Certificaat> GetCertificatesInspection(int id)
        {
            return Context.Inspectie.Find(id).Certificaat.ToList();
        }
    }
}
