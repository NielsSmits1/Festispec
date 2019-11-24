using Festispec_WPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Pdf
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionTitle { get; set; }
        public List<string> GivenAwnsers { get; set; }

        public string ChartHtml
        {
            get
            {
                // Let user set property for generatring diffrent types of charts
                return ChartHelper.GenerateChart(GivenAwnsers);
            }
        }
    }
}
