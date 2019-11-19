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
        private CertificateVM _selected;
        private InspectorVM _inspector;
        public ICommand AddInspectorCommand { get; set; }
        public ICommand MoveToAvailableCommand { get; set; }
        public ICommand MoveToChosenCommand { get; set; }
        public ICommand OpenEditInspectorCommand { get; set; }
        public ICommand MoveToLeftoverCommand { get; set; }
        public ICommand MoveToChosenSelectedCommand { get; set; }
        public ObservableCollection<InspectorVM> Inspectors { get; set; }
        public ICommand CreateNewInspectorCommand { get; set; }
        public ICommand SafeEditInspectorCommand { get; set; }
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
        public ObservableCollection<CertificateVM> LeftoverCertificates { get; set; }
        public InspectorCrudVM()
        {

            
            UOW = new ViewModelLocator().UOW;
            NewInspector = new InspectorVM();
            Inspectors = new ObservableCollection<InspectorVM>(UOW.NAWInspectors.GetAll().ToList().Select(a => new InspectorVM(a)));
            var list = UOW.Certificates.GetAll().Select(certificaat => new CertificateVM(certificaat));
            AvailableCertificates = new ObservableCollection<CertificateVM>(list);
            UOW.Complete();


            MoveToAvailableCommand = new RelayCommand(MoveCertificateToAvailable);
            MoveToChosenCommand = new RelayCommand(MoveCertificateToChosen);
            MoveToLeftoverCommand = new RelayCommand(MoveCertificateToLeftover);
            MoveToChosenSelectedCommand = new RelayCommand(MoveCertificateToSelectedChosen);
            AddInspectorCommand = new RelayCommand(AddInspector);
            CreateNewInspectorCommand = new RelayCommand(CreateNewInspector);
            OpenEditInspectorCommand = new RelayCommand(OpenEditInspector);
            SafeEditInspectorCommand = new RelayCommand(SafeEditInspector);
        }

        public void OpenEditInspector()
        {
            SelectedInspector.InspectorData = UOW.Inspectors.GetAll().FirstOrDefault(i => i.NAW == SelectedInspector.NAWInspector_ID);
            var NawPhonenumber = UOW.PhonenumberInspectors.GetAll().FirstOrDefault(t => t.NAW_Inspecteur_ID == SelectedInspector.NAWInspector_ID);
            //.Select(t => new Telefoonnummer_inspecteur { Telefoonnummer = t.Telefoonnummer, NAW_Inspecteur_ID = t.NAW_Inspecteur_ID}).ToList();
            // var certificates
            if(NawPhonenumber == null)
            {
                return;
            }
            SelectedInspector.PhonenumberModel = NawPhonenumber;
            SelectedInspector.ChosenCertificates = new ObservableCollection<CertificateVM>(UOW.Inspectors.GetCertificatesInspector(SelectedInspector.Inspector_ID).Select(c => new CertificateVM(c)));
            LeftoverCertificates = new ObservableCollection<CertificateVM>(UOW.Inspectors.GetMissingCertificates(SelectedInspector.Inspector_ID).Select(c => new CertificateVM(c)));
            _editInspectorWindow = new EditInspectorWindow();
            _editInspectorWindow.Show();
        }

        public void SafeEditInspector()
        {
            var NAW = UOW.NAWInspectors.GetAll().FirstOrDefault(ins => ins.ID == SelectedInspector.NAWInspector_ID);
            NAW = SelectedInspector.NAWInspector;
           // UOW.Context.Telefoonnummer_inspecteur.Remove(UOW.Context.Telefoonnummer_inspecteur.ToList().FirstOrDefault(ins => ins.NAW_Inspecteur_ID == SelectedInspector.NAWInspector_ID));
            //UOW.Context.Telefoonnummer_inspecteur.Add(SelectedInspector.PhonenumberModel);
            var inspector = UOW.Inspectors.GetAll().FirstOrDefault(ins => ins.ID == SelectedInspector.Inspector_ID);
            inspector = SelectedInspector.InspectorData;

            UOW.Inspectors.Get(SelectedInspector.Inspector_ID).Certificaat.Clear();
            foreach (var item in SelectedInspector.ChosenCertificates)
            {
                UOW.Inspectors.Get(SelectedInspector.Inspector_ID).Certificaat.Add(item.Certificate);
            }

            try
            {
                UOW.Complete();
                MessageBox.Show("De aanpassingen zijn doorgevoerd", "Fout bij invoeren velden",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
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
        public void MoveCertificateToSelectedChosen()
        {
            SelectedInspector.ChosenCertificates.Add(_selected);
            LeftoverCertificates.Remove(_selected);
        }

        public void MoveCertificateToLeftover()
        {
            LeftoverCertificates.Add(_selected);
            SelectedInspector.ChosenCertificates.Remove(_selected);
        }




        public void CreateNewInspector()
        {
            NewInspector = new InspectorVM();
        }

        public void AddInspector()
        {
   
            UOW.NAWInspectors.Add(NewInspector.NAWInspector);
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
            AvailableCertificates = new ObservableCollection<CertificateVM>(UOW.Certificates.GetAll().Select(certificaat => new CertificateVM(certificaat)));
            RaisePropertyChanged(() => AvailableCertificates);
            NewInspector.EmptyAll();
        }
    }
}
