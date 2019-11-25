using System.Windows.Input;
using Festispec_WPF.Helpers;
using Festispec_WPF.Model.UnitOfWork;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec_WPF.ViewModel
{
    public class RapportageVM
    {
        public ICommand GenerateCommand { get; set; }
        private IUnitOfWork UOW;
        public int selectedInspection { get; set; }

        public RapportageVM()
        {
            GenerateCommand = new RelayCommand(GeneratePdf);
            UOW = new ViewModelLocator().UOW;
        }

        public void GeneratePdf()
        {
            var location = PdfHelper.GeneratePdf(selectedInspection);
        }

    }
}
