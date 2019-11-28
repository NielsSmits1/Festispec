using System.Collections.ObjectModel;
using System.Windows.Input;
using Festispec_WPF.Helpers;
using Festispec_WPF.Model.UnitOfWork;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec_WPF.ViewModel
{
    public class RapportageVM : ViewModelBase
    {
        public ICommand GenerateCommand { get; set; }
        private IUnitOfWork UOW;
        public int selectedInspection { get; set; }
        private string _chart { get; set; }
        //public variables
        public ObservableCollection<string> Charts { get; set; }

        public RapportageVM()
        {
            UOW = new ViewModelLocator().UOW;
            Charts = new ObservableCollection<string>()
            {
                "Bar",
                "Row"
            };
            GenerateCommand = new RelayCommand(GeneratePdf);
        }

        public void GeneratePdf()
        {
            var helper = new PdfHelper();
            var location = helper.GeneratePdf(selectedInspection, _chart);
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
