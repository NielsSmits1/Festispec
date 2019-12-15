using FestiSpec.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestiSpec.Domain.Model.Interface_Repositories
{
    public interface IQuestionnaireRepository  : IRepository<Vragenlijst>
    {
        List<Vragenlijst> getTemplates();

    }
}
