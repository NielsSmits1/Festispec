using Festispec_WPF.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Festispec_WPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        private CertificateVM _selected;
        public ObservableCollection<CertificateVM> AvailableCertificates { get; set; }
        public ObservableCollection<CertificateVM> ChosenCertificates { get; set; }
  
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

        public ICommand MoveToAvailableCommand { get; set; }
        public ICommand MoveToChosenCommand { get; set; }
        public MainViewModel()
        {
            //ChosenCertificates = new ObservableCollection<CertificateVM>();
            //using(var context = new FestiSpecEntities())
            //{
            //    certificateList = new BaseRepository<Certificaat>(context);
            //    var list = certificateList.Get().Select(certificaat => new CertificateVM(certificaat)).ToList();
            //    AvailableCertificates = new ObservableCollection<CertificateVM>(list);
            //    context.SaveChanges();
            //}

            //MoveToAvailableCommand = new RelayCommand(MoveCertificateToAvailable);
            //MoveToChosenCommand = new RelayCommand(MoveCertificateToChosen);
            
        }

        public void MoveCertificateToChosen()
        {
            ChosenCertificates.Add(_selected);
            AvailableCertificates.Remove(_selected);
        }

        public void MoveCertificateToAvailable()
        {
            AvailableCertificates.Add(_selected);
            ChosenCertificates.Remove(_selected);
        }

        public void SaveCertificatesToInspector(int inspectorID, FestiSpecEntities context)
        {
            //To be found out
            //    IRepository<Inspecteur> InspectorList = new BaseRepository<Inspecteur>(context);
            //   // var inspector = context.Inspecteur.Include("Certificaat_inspecteur").Where(c => c.ID == inspectorID);
            //    var inspector = InspectorList.GetByID(inspectorID);
            //foreach (var item in ChosenCertificates)
            //{
            //    inspector.Certificaat.Add(item.Certificate);
            //}
            //context.SaveChanges();

            //ChosenCertificates.Clear();
            //AvailableCertificates.Clear();
        }
    }
}