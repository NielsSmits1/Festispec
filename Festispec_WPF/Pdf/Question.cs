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
        public List<string> GivenAwnsers { get; set; }
        public string _chart { get; set; }

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
