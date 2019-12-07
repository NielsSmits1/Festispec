using Festispec_WPF.Pdf;
using System;
using System.Collections.Generic;

namespace Festispec_WPF.Helpers
{
    public class RapportageInfo
    {
        public string CustomerName { get; set; }
        public string InspectionTitle { get; set; }
        public string Introduction { get; set; }
        public string SummaryOfInspection { get; set; }
        public string Advice { get; set; }

        public List<Question> Questions { get; set; }

        public DateTime Date { get; set; }
    }
}
