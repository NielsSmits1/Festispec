using Festispec_WPF.Model.Interface_Repositories;
using System;
using System.Collections.Generic;
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
    }
}
