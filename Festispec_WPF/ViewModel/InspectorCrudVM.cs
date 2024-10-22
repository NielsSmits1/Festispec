﻿using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Data.Entity;
using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.View;
using System.Data.SqlClient;
using System.Windows.Media;
using System.Windows.Forms;
using FestiSpec.Domain.Model;

namespace Festispec_WPF.ViewModel
{
    public class InspectorCrudVM : ViewModelBase
    {
        private UnitOfWork UOW;
        private EditInspectorWindow _editInspectorWindow;
        private CreateInspectorWindow _createInspectorWindow;
        private CertificateVM _selected;
        private CertificateVM _appSelected;
        private InspectorVM _inspector;
        private int _currentlist;

        // Create Inspector Properties
        public ICommand AddInspectorCommand { get; set; }
        public ICommand MoveToAvailableCommand { get; set; }
        public ICommand MoveToChosenCommand { get; set; }
        public ICommand OpenCreateCommand { get; set; }
        public ICommand CloseCreateCommand { get; set; }
        public ICommand RecruitApplicantCommand { get; set; }
        public InspectorVM NewInspector { get; set; }

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

        public CertificateVM SelectedAppCertificate
        {
            get => _appSelected;
            set
            {
                if (!AvailableAppCertificates.Contains(value))
                {
                    AvailableAppCertificates.Add(value); NewAppInspector.ChosenCertificates.Remove(value);
                }
                else
                {
                    NewAppInspector.ChosenCertificates.Add(value); AvailableAppCertificates.Remove(value);
                }
            }
        }

        public ObservableCollection<CertificateVM> AvailableCertificates { get; set; }
        public ObservableCollection<CertificateVM> AvailableAppCertificates { get; set; }
        //

        //Update Inspector Commands
        public ICommand OpenEditInspectorCommand { get; set; }
        public ICommand MoveToLeftoverCommand { get; set; }
        public ICommand MoveToChosenSelectedCommand { get; set; }
        public ICommand SafeEditInspectorCommand { get; set; }
        public ObservableCollection<CertificateVM> LeftoverCertificates { get; set; }

        //Read Inspector Properties
        public ObservableCollection<InspectorVM> Inspectors { get; set; }
        public ObservableCollection<ApplicantVM> Applicants { get; set; }
        public ICommand ListOfActiveCommand { get; set; }
        public ICommand ListOfInactiveCommand { get; set; }
        public ICommand ListOfLicensedCommand { get; set; }
        public bool AllChecked { get; set; }
        public ICommand ListOfAllCommand { get; set; }
        public ICommand SetInspectorInactiveCommand { get; set; }
   

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

        private ApplicantVM _selectedApplicant;

        public ApplicantVM SelectedApplicant
        {
            get
            {
                return _selectedApplicant;
            }
            set
            {
                _selectedApplicant = value;
                if(value != null)
                {
                    NewAppInspector.FillNAW(value);
                }
                RaisePropertyChanged(() => SelectedApplicant);
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public InspectorVM NewAppInspector { get; set; }

        public InspectorCrudVM()
        {
            //UOW
            UOW = ViewModelLocator.UOW;

            //New Inspector - Create
            NewInspector = new InspectorVM();
            NewAppInspector = new InspectorVM();
            //List of Inspectors - Read
            LoadAll();
            AllChecked = true;

            //All Certificates - Create
            var list = UOW.Certificates.GetAll().Select(certificaat => new CertificateVM(certificaat));
            AvailableCertificates = new ObservableCollection<CertificateVM>(list);
            AvailableAppCertificates = new ObservableCollection<CertificateVM>(list);

            //Save changes
            UOW.Complete();


            ListOfAllCommand = new RelayCommand(LoadAll);
            MoveToAvailableCommand = new RelayCommand(MoveCertificateToAvailable);
            MoveToChosenCommand = new RelayCommand(MoveCertificateToChosen);
            MoveToLeftoverCommand = new RelayCommand(MoveCertificateToLeftover);
            MoveToChosenSelectedCommand = new RelayCommand(MoveCertificateToSelectedChosen);
            AddInspectorCommand = new RelayCommand(AddInspector);
            OpenEditInspectorCommand = new RelayCommand(OpenEditInspector);
            SafeEditInspectorCommand = new RelayCommand(SafeEditInspector);
            ListOfActiveCommand = new RelayCommand(LoadActive);
            ListOfInactiveCommand = new RelayCommand(LoadInactive);
            ListOfLicensedCommand = new RelayCommand(LoadLicensed);
            SetInspectorInactiveCommand = new RelayCommand(SetInspectorInactive);
            OpenCreateCommand = new RelayCommand(OpenCreate);
            CloseCreateCommand = new RelayCommand(CloseCreate);
            RecruitApplicantCommand = new RelayCommand(RecruitApplicant, CanRecruit);
    }

        // CREATE
        public void AddInspector()
        {

            UOW.NAWInspectors.Add(NewInspector.NAWInspector);
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
            AllChecked = true;
            RaisePropertyChanged(() => AllChecked);

            NewInspector.EmptyAll();

            if(_createInspectorWindow != null)
            {
                _createInspectorWindow.Close();
            }
        }

        public void RecruitApplicant()
        {
            UOW.NAWInspectors.Add(NewAppInspector.NAWInspector);
            UOW.Inspectors.Add(NewAppInspector.InspectorData);

            foreach (var item in NewAppInspector.ChosenCertificates)
            {
                UOW.Inspectors.Get(NewAppInspector.Inspector_ID).Certificaat.Add(item.Certificate);
            }

            UOW.Context.Applicant.Remove(_selectedApplicant.ApplicantModel);

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

            AllChecked = true;
            RaisePropertyChanged(() => AllChecked);
            LoadAll();
            NewAppInspector.EmptyAll();
            AvailableAppCertificates = new ObservableCollection<CertificateVM>(UOW.Certificates.GetAll().Select(certificaat => new CertificateVM(certificaat)));
        }

        public bool CanRecruit()
        {
            return SelectedApplicant != null;
        }

        // UPDATE 
        public void OpenEditInspector()
        {
            SelectedInspector.InspectorData = UOW.Inspectors.GetAll().FirstOrDefault(i => i.NAW == SelectedInspector.NAWInspector_ID);

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
                MessageBox.Show("De aanpassingen zijn doorgevoerd", "Het is gelukt!",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                _editInspectorWindow.Close();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        public void LoadAll()
        {
            Inspectors = new ObservableCollection<InspectorVM>(UOW.NAWInspectors.GetAll().ToList().Select(a => new InspectorVM(a)));
            RaisePropertyChanged(() => Inspectors);
            _currentlist = 1;

            Applicants = new ObservableCollection<ApplicantVM>(UOW.Context.Applicant.ToList().Select(a => new ApplicantVM(a)));
            RaisePropertyChanged(() => Applicants);
        }
        public void SetInspectorInactive()
        {
            UOW.Inspectors.SetInspectorInactive(SelectedInspector.NAWInspector_ID);
            UOW.Complete();
            switch (_currentlist)
            {
                case 1:
                    LoadAll();
                    break;
                case 2:
                    LoadActive();
                    break;
                case 3:
                    LoadInactive();
                    break;
                case 4:
                    LoadLicensed();
                    break;
            }
        }

        public void LoadActive()
        {
            Inspectors = new ObservableCollection<InspectorVM>(UOW.NAWInspectors.ListOfActiveInspectors.Select(ins => new InspectorVM(ins)));
            RaisePropertyChanged(() => Inspectors);
            _currentlist = 2;
        }

        public void LoadInactive()
        {
            Inspectors = new ObservableCollection<InspectorVM>(UOW.NAWInspectors.ListOfInactiveInspectors.Select(ins => new InspectorVM(ins)));
            RaisePropertyChanged(() => Inspectors);
            _currentlist = 3;
        }

        public void LoadLicensed()
        {
            Inspectors = new ObservableCollection<InspectorVM>(UOW.NAWInspectors.ListOfLicensedInspectors.Select(ins => new InspectorVM(ins)));
            RaisePropertyChanged(() => Inspectors);
            _currentlist = 4;
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

        private void OpenCreate()
        {
            _createInspectorWindow = new CreateInspectorWindow();
            _createInspectorWindow.Show();
        }

        private void CloseCreate()
        {
            _createInspectorWindow.Close();
        }

        

       
    }
}
