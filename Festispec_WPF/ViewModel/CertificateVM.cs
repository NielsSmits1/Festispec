using FestiSpec.Domain.Model;
using GalaSoft.MvvmLight;

namespace Festispec_WPF.ViewModel
{
    public class CertificateVM : ViewModelBase
    {
        private Certificaat _certificate;

        public CertificateVM(Certificaat newCertificate)
        {
            _certificate = newCertificate;
        }

        public Certificaat Certificate
        {
            get
            {
                return _certificate;
            }
        }

        public int ID
        {
            get => _certificate.ID;
            set { _certificate.ID = value; RaisePropertyChanged("ID"); }
        }

        public string Name
        {
            get => _certificate.Name;
            set { _certificate.Name = value; RaisePropertyChanged("Name"); }
        }
    }
}
