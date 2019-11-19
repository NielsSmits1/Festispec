using Festispec_WPF.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data.Entity;
using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.Model.Repositories;
using Festispec_WPF.View;
using System.Data.SqlClient;
using System.Windows.Media;
using System.Windows.Forms;

namespace Festispec_WPF.ViewModel
{
    public class InspectorCrudVM : ViewModelBase
    {
        private UnitOfWork UOW;
        private EditInspectorWindow _editInspectorWindow;
        private IRepository<Certificaat> Certificates { get; set; }
        private CertificateVM _selected;
        private InspectorVM _inspector;
        public ICommand AddInspectorCommand { get; set; }
        public ICommand MoveToAvailableCommand { get; set; }
        public ICommand MoveToChosenCommand { get; set; }
        public ICommand OpenEditInspectorCommand { get; set; }
        public ObservableCollection<InspectorVM> Inspectors { get; set; }
        public ICommand CreateNewInspectorCommand { get; set; }
        public InspectorVM NewInspector { get; set; }

        public string ErrorProperty { get; set; }

        public InspectorVM SelectedInspector
        {
            get
            {
                return _inspector;
            }
            set
            {
                _inspector = value;
            }
        }
        public CertificateVM SelectedCertificate
        {
            get
            {
                return _selected;
            }
            set
            {
                _selected = value;
            }
        }

        public ObservableCollection<CertificateVM> AvailableCertificates { get; set; }

        public InspectorCrudVM()
        {

            
            UOW = new ViewModelLocator().UOW;
            NewInspector = new InspectorVM();
            Inspectors = new ObservableCollection<InspectorVM>(UOW.NAWInspectors.GetAll().ToList().Select(a => new InspectorVM(a)));
            Certificates = new Repository<Certificaat>(UOW.Context);
            var list = Certificates.GetAll().ToList().Select(certificaat => new CertificateVM(certificaat));
            AvailableCertificates = new ObservableCollection<CertificateVM>(list);
            UOW.Complete();


            MoveToAvailableCommand = new RelayCommand(MoveCertificateToAvailable);
            MoveToChosenCommand = new RelayCommand(MoveCertificateToChosen);
            AddInspectorCommand = new RelayCommand(AddInspector);
            CreateNewInspectorCommand = new RelayCommand(CreateNewInspector);
            OpenEditInspectorCommand = new RelayCommand(OpenEditInspector);
        }

        public void OpenEditInspector()
        {
            SelectedInspector.InspectorData = UOW.Inspectors.Get(SelectedInspector.NAWInspector_ID);
            var NawPhonenumber = UOW.PhonenumberInspectors.GetAll().FirstOrDefault(t => t.NAW_Inspecteur_ID == SelectedInspector.NAWInspector_ID);
            //.Select(t => new Telefoonnummer_inspecteur { Telefoonnummer = t.Telefoonnummer, NAW_Inspecteur_ID = t.NAW_Inspecteur_ID}).ToList();
            // var certificates
            if(NawPhonenumber == null)
            {
                return;
            }
            SelectedInspector.PhonenumberModel = NawPhonenumber;
           // SelectedInspector.ChosenCertificates = UOW
            _editInspectorWindow = new EditInspectorWindow();
            _editInspectorWindow.Show();
        }

        public void MoveCertificateToChosen()
        {
            NewInspector.ChosenCertificates.Add(_selected);
            AvailableCertificates.Remove(_selected);
        }

        public void MoveCertificateToAvailable()
        {
            AvailableCertificates.Add(_selected);
            NewInspector.ChosenCertificates.Remove(_selected);
        }

        public void CreateNewInspector()
        {
            NewInspector = new InspectorVM();
        }

        public void AddInspector()
        {

            Repository<NAW_inspecteur> NAW = new Repository<NAW_inspecteur>(UOW.Context);
            
            NAW.Add(NewInspector.NAWInspector);
            UOW.Context.Telefoonnummer_inspecteur.Add(NewInspector.PhonenumberModel);
            UOW.Inspectors.Add(NewInspector.InspectorData);
            
            foreach (var item in NewInspector.ChosenCertificates)
            {
                UOW.Inspectors.Get(NewInspector.Inspector_ID).Certificaat.Add(item.Certificate);
            }

            try
            {
                UOW.Complete();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            

            Inspectors = new ObservableCollection<InspectorVM>(UOW.NAWInspectors.GetAll().ToList().Select(a => new InspectorVM(a)));
            RaisePropertyChanged(() => Inspectors);
            NewInspector.EmptyAll();
        }
    }
}
