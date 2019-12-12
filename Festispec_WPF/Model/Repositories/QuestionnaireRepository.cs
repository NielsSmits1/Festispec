using Festispec_WPF.Model.Interface_Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Model.Repositories
{
    public class QuestionnaireRepository : Repository<Vragenlijst>, IQuestionnaireRepository
    {
        public QuestionnaireRepository(FestiSpecEntities context) : base(context)
        {
        }


        public List<Vragenlijst> getTemplates()
        {
            var idList = Context.Template.Select(temp => temp.Vragenlijst_ID).ToList();

            var TemplateList = Context.Vragenlijst.Where(vr => idList.Contains(vr.ID)).ToList();

            return TemplateList;
        }
    }
}
