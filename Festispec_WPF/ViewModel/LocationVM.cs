using Festispec_WPF.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.ViewModel
{
    public class LocationVM : ViewModelBase
    {
        private Locatie _location;

        public LocationVM()
        {
            _location = new Locatie();
        }

        public LocationVM(Locatie loc)
        {
            _location = loc;
        }

        public int ID
        {
            get => _location.ID;
            set
            {
                _location.ID = value; RaisePropertyChanged(() => ID);
            }
        }

        public string ZipCode
        {
            get => _location.Postcode;
            set
            {
                _location.Postcode = value; RaisePropertyChanged(() => ZipCode);
            }
        }

        public string HomeNumber
        {
            get => _location.Huisnummer;
            set
            {
                _location.Huisnummer = value; RaisePropertyChanged(() => HomeNumber);
            }
        }

        public string StreetName
        {
            get => _location.Plaatsnaam;
            set
            {
                _location.Plaatsnaam = value; RaisePropertyChanged(() => StreetName);
            }
        }
        public string FullAdress
        {
            get => ZipCode + " " + HomeNumber + " " + StreetName;
        }
    }
}
