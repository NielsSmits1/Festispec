using FestiSpec.Domain.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace Festispec_WPF.ViewModel
{
    public class OfflineMapVM : ViewModelBase
    {
        public ObservableCollection<InspectionVM> Festivals { get; set; }

        public ICommand openDetailsCommand { get; set; }
        public ICommand openPlannedInspectorsCommand { get; set; }

        private string _plannedInspectorVisibility;

        public string PlannedInspectorVisibility
        {
            get => _plannedInspectorVisibility;
            set
            {
                _plannedInspectorVisibility = value;
                base.RaisePropertyChanged();
            }
        }

        public InspectionVM Festival => Festivals.First();

        private string _searchText;

        public string searchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                base.RaisePropertyChanged();
            }
        }

        public OfflineMapVM()
        {
            searchText = "Zoek naam";

            LoadOfflineFestival();
        }

        private void LoadOfflineFestival()
        {
            openDetailsCommand = new RelayCommand(openDetails);
            openPlannedInspectorsCommand = new RelayCommand(openInspectors);
            PlannedInspectorVisibility = "Hidden";

            Festivals = new ObservableCollection<InspectionVM>();
            string json = File.ReadAllText(@"../../inspection.json");
            var festivalData = JsonConvert.DeserializeObject<Inspectie>((json));
            Festivals.Add(new InspectionVM(festivalData));

            Festivals[0].ChosenCertificates = new ObservableCollection<CertificateVM>();
            foreach (var certificate in festivalData.Certificaat)
            {
                Festivals[0].ChosenCertificates.Add(new CertificateVM(certificate));
            }

            Festivals[0].ChosenQuestionnaires = new ObservableCollection<QuestionnaireVM>();
            foreach (var questionnaire in festivalData.Vragenlijst)
            {
                Festivals[0].ChosenQuestionnaires.Add(new QuestionnaireVM(questionnaire));
            }

            Festivals[0].PlannedInspectors = new ObservableCollection<InspectorVM>();
            foreach (var inspector in festivalData.Inspecteur)
            {
                Festivals[0].PlannedInspectors.Add(new InspectorVM(inspector));
            }

            RaisePropertyChanged(() => Festivals);
        }

        private void openInspectors()
        {
            PlannedInspectorVisibility = "Visible";
        }

        private void openDetails()
        {
            PlannedInspectorVisibility = "Hidden";
        }
    }
}
