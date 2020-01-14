using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.ViewModel
{
    public class HomeVM
    {
        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection InspectorsOnInspectionCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> YFormatter { get; set; }
        public Func<int, string> Formatter { get; set; }
        public int AmountOfInspectionsLastMonth { get; set; }
        public int AmountOfInspectionsCompletedLastMonth { get; set; }
        public int AmountOfInspectionsNotCompletedLastMonth { get; set; }
        public List<InspectorVM> MostWorkingInspectors { get; set; }
        public HomeVM()
        {
            DateTime prev = DateTime.Now.AddMonths(-1);
            AmountOfInspectionsCompletedLastMonth = ViewModelLocator.UOW.Inspections.Find(ins => ins.StartDate >= prev && ins.Voltooid == true).Select(ins => ins.Voltooid).ToList().Count;
            AmountOfInspectionsNotCompletedLastMonth = ViewModelLocator.UOW.Inspections.Find(ins => ins.StartDate >= prev && ins.Voltooid == false).Select(ins => ins.Voltooid).ToList().Count;
            MostWorkingInspectors = new List<InspectorVM>(ViewModelLocator.UOW.Inspectors.GetTop5PlannedInspectors().Select(ins => new InspectorVM(ins)));
            
            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Voltooide inspecties",
                    Values = new ChartValues<int>{AmountOfInspectionsCompletedLastMonth},
                    DataLabels = true,
                    LabelPoint = point => point.Participation.ToString("P"),
                    FontSize = 10
                }
                ,
                new PieSeries
                {
                    Title = "Niet - Voltooide inspecties",
                    Values = new ChartValues<int>{AmountOfInspectionsNotCompletedLastMonth},
                    LabelPoint = point => point.Participation.ToString("P"),

                    DataLabels = true,
                    FontSize = 10
                }
            };

            InspectorsOnInspectionCollection = new SeriesCollection();
            List<int> pastYearValues = new List<int>();
            foreach (var item in MostWorkingInspectors)
            {
                pastYearValues.Add(item.GetInspectionCount(DateTime.Now.AddYears(-1).Year));
            }

            List<int> currentYearValues = new List<int>();
            foreach (var item in MostWorkingInspectors)
            {
                currentYearValues.Add(item.GetInspectionCount(DateTime.Now.Year));
            }

            InspectorsOnInspectionCollection.Add(new RowSeries
            {
                Title = DateTime.Now.AddYears(-1).Year.ToString(),
                Values = new ChartValues<int>(pastYearValues)
            });
            InspectorsOnInspectionCollection.Add(new RowSeries
            {
                Title = DateTime.Now.Year.ToString(),
                Values = new ChartValues<int>(currentYearValues)
            });

            Labels = MostWorkingInspectors.Select(ins => ins.FirstName).ToArray();
            YFormatter = value => value.ToString("C");
            Formatter = value => value.ToString("N");

        }
    }
}
