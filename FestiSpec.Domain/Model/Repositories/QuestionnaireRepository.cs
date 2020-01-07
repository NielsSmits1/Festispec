using FestiSpec.Domain.Model.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiSpec.Domain.Model.Repositories
{
    public class QuestionnaireRepository : Repository<Vragenlijst>, IQuestionnaireRepository
    {
        public QuestionnaireRepository(FestiSpecEntities context) : base(context)
        {
        }


        public List<Vragenlijst> getTemplates()
        {
            var idList = new List<int>();
            foreach (var item in Context.Template.Where(temp => temp.Actief == true).ToList())
            {
                idList.Add(item.Vragenlijst_ID);
            }
         
            var TemplateList = Context.Vragenlijst.Where(vr => idList.Contains(vr.ID)).ToList();
            return TemplateList;
        }
    }
}
