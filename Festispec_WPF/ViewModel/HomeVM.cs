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
        public string[] Labels { get; set; }
        public Func<int, string> YFormatter { get; set; }

        public int AmountOfInspectionsLastMonth { get; set; }
        public int AmountOfInspectionsCompletedLastMonth { get; set; }
        public int AmountOfInspectionsNotCompletedLastMonth { get; set; }
        public HomeVM()
        {
            DateTime prev = DateTime.Now.AddMonths(-1);
            AmountOfInspectionsLastMonth = ViewModelLocator.UOW.Inspections.Find(ins => ins.StartDate >= prev).Select(ins => ins.StartDate).ToList().Count;
            AmountOfInspectionsCompletedLastMonth = ViewModelLocator.UOW.Inspections.Find(ins => ins.StartDate >= prev && ins.Voltooid == true).Select(ins => ins.Voltooid).ToList().Count;
            AmountOfInspectionsNotCompletedLastMonth = ViewModelLocator.UOW.Inspections.Find(ins => ins.StartDate >= prev && ins.Voltooid == false).Select(ins => ins.Voltooid).ToList().Count;


            SeriesCollection = new SeriesCollection
            {
                //new LineSeries
                //{
                //    Title = "Inspecties gehouden afgelopen maand",
                //    Values = new ChartValues<DateTime>(ViewModelLocator.UOW.Inspections.Find(ins => ins.StartDate >= DateTime.Now.AddMonths(-1).Date).Select(ins => ins.StartDate).ToList())

                //},
                //new LineSeries
                //{
                //    Title = "Inspecties gehouden",
                //    Values = new ChartValues<bool>(ViewModelLocator.UOW.Inspections.Find(ins => ins.StartDate >= DateTime.Now.AddMonths(-1).Date && ins.Voltooid == true).Select(ins => ins.Voltooid).ToList()),

                //}
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



            Labels = new[] { "Nov", "Dec" };
            YFormatter = value => value.ToString("C");

        }
    }
}
