using FestiSpec.Domain.Model;
using Festispec_WPF.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            get { return _certificate.ID; }
            set { _certificate.ID = value; RaisePropertyChanged("ID"); }
        }

        public string Name
        {
            get { return _certificate.Name; }
            set { _certificate.Name = value; RaisePropertyChanged("Name"); }
        }
    }
}
