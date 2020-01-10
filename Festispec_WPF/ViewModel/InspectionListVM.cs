using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Festispec_WPF.ViewModel
{
    public class InspectionListVM : ViewModelBase
    {
        private UnitOfWork UOW;
        public ObservableCollection<InspectionVM> Accomplished { get; set; }
        public ObservableCollection<InspectionVM> NonAccomplished { get; set; }
        public ICommand InspectionToAccomplishedCommand { get; set; }
        public ICommand MakeRapportCommand { get; set; }
        private InspectionVM _selectedInspection;

        public InspectionVM SelectedInspection
        {
            get { return _selectedInspection; }
            set { _selectedInspection = value; RaisePropertyChanged(() => SelectedInspection); }
        }

        private InspectionVM _selectedAccomplishedInspection;

        public InspectionVM SelectedAccInspection
        {
            get { return _selectedAccomplishedInspection; }
            set { _selectedAccomplishedInspection = value; RaisePropertyChanged(() => SelectedAccInspection); }
        }


        public InspectionListVM()
        {
            UOW = ViewModelLocator.UOW;
            InspectionToAccomplishedCommand = new RelayCommand(inspectionToAccomplished);
            MakeRapportCommand = new RelayCommand(makeRapport);
            Init();
        }

        public void Init()
        {
            Accomplished = new ObservableCollection<InspectionVM>(UOW.Inspections.GetAccomplishedInspections().Select(ins => new InspectionVM(ins)));
            NonAccomplished = new ObservableCollection<InspectionVM>(UOW.Inspections.GetNonAccomplishedInspections().Select(ins => new InspectionVM(ins)));
            RaisePropertyChanged(() => Accomplished);
            RaisePropertyChanged(() => NonAccomplished);
        }

        private void inspectionToAccomplished()
        {
            SelectedInspection.Accomplished = true;
            UOW.Complete();
            Init();
        }

        private void makeRapport()
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var rapportageWindow = new RapportageView(SelectedAccInspection.Inspection);
            rapportageWindow.Show();
            currentWindow.Close();
        }
    }
}
