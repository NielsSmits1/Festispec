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
        public string chartType { get; set; }

        public Question(string _chartType = "")
        {
            chartType = _chartType;
        }

        public string ChartHtml
        {
            get
            {
                // Let user set property for generatring diffrent types of charts
                if(chartType != "")
                    return ChartHelper.GenerateChart(GivenAwnsers, chartType);

                return chartType;
            }
        }
    }
}
