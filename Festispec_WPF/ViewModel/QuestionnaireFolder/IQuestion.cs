﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.ViewModel
{
    public interface IQuestion
    {
        string Question { get; set; }
        string QuestionType { get; set; }
        int Position { get;set; }
        void toDatabase(int questionnaireId);
    }
}