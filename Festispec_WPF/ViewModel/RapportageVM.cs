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
            UOW = new ViewModelLocator().UOW;

            GenerateCommand = new RelayCommand(GeneratePdf);
        }

        public void GeneratePdf()
        {
            var helper = new PdfHelper();
            var location = helper.GeneratePdf(selectedInspection);
        }

    }
}
