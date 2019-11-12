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

namespace Festispec_WPF.ViewModel
{
    public class InspectorCrudVM : ViewModelBase
    {
        FestiSpecEntities context;
        private CertificateVM _selected;
        public ICommand AddInspectorCommand { get; set; }
        public ICommand MoveToAvailableCommand { get; set; }
        public ICommand MoveToChosenCommand { get; set; }

        public ICommand CreateNewInspectorCommand { get; set; }
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

        public ObservableCollection<CertificateVM> AvailableCertificates { get; set; }

        public InspectorCrudVM()
        {
            context = new FestiSpecEntities();
            using (context)
            {
                var list = context.Certificaat.ToList().Select(certificaat => new CertificateVM(certificaat));
                AvailableCertificates = new ObservableCollection<CertificateVM>(list);
                context.SaveChanges();
            }

            MoveToAvailableCommand = new RelayCommand(MoveCertificateToAvailable);
            MoveToChosenCommand = new RelayCommand(MoveCertificateToChosen);
            AddInspectorCommand = new RelayCommand(AddInspector);
            CreateNewInspectorCommand = new RelayCommand(CreateNewInspector);
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
            using (context = new FestiSpecEntities())
            {
                NewInspector.Active = true;
                context.NAW_inspecteur.Add(NewInspector.NAWInspector);
                context.Inspecteur.Add(NewInspector.InspectorData);
                context.Telefoonnummer_inspecteur.Add(NewInspector.PhonenumberModel);
                //var inspector = context.Inspecteur.Find(NewInspector.Inspector_ID);
                foreach (var item in NewInspector.ChosenCertificates)
                {
                    NewInspector.InspectorData.Certificaat.Add(item.Certificate);
                }
                context.Entry(NewInspector.InspectorData).State = EntityState.Added;
                context.SaveChanges();
            }
        }
    }
}
