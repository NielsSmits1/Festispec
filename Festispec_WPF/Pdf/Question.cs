using Festispec_WPF.Helpers;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.Pdf
{
    public class Question : ViewModelBase
    {
        public int Id { get; set; }
        public string QuestionTitle { get; set; }
        public string Type { get; set; }
        public string TabelHeadQuestion { get; set; }
        public string TabelHeadAwnser { get; set; }
        public int TabelHeadCount { get{ return GivenAwnsers.Count / TabelHeaders.Count + 1; } }
        public List<string> TabelHeaders { get; set; }
        public List<string> GivenAwnsers { get; set; }
        public string _chart { get; set; }

        public string Visible { get; set; }

        public Question(string chart = "")
        {
            _chart = chart;
        }

        public string ChartHtml
        {
            get
            {
                // Let user set property for generatring diffrent types of charts
                if (_chart != "")
                    return ChartHelper.GenerateChart(GivenAwnsers, _chart);

                return _chart;
            }
        }

        public string Chart
        {
            get { return _chart; }
            set
            {
                _chart = value;
                RaisePropertyChanged();
            }
        }
    }
}
