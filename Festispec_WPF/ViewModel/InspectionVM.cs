using Festispec_WPF.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.ViewModel
{
    public class InspectionVM : ViewModelBase
    {

        private Inspectie _inspection;
        private Locatie _location;
        private CustomerVM _customer;
        public InspectionVM()
        {
            _inspection = new Inspectie();
            _location = new Locatie();
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

        public DateTime StartDate
        {
            get => _inspection.StartDate;
            set
            {
                _inspection.StartDate = value; RaisePropertyChanged(() => StartDate);
            }
        }

        public DateTime EndDate
        {
            get => _inspection.EndDate;
            set
            {
                _inspection.EndDate = value; RaisePropertyChanged(() => EndDate);
            }
        }

        public int WerkNemerId
        {
            get => 1;
            set
            {
                _inspection.Werknemer_ID = value; RaisePropertyChanged(() => WerkNemerId);
            }
        }

        public int Customer_ID
        {
            get => _inspection.Klant_ID;
            set
            {
                _inspection.Klant_ID = value; RaisePropertyChanged(() => Customer_ID);
            }

        }

        public string Title
        {
            get => _inspection.Titel;
            set
            {
                _inspection.Titel = value;RaisePropertyChanged(() => Title);
            }
        }

        public string Version
        {
            get => _inspection.Versie;
            set
            {
                _inspection.Versie = value; RaisePropertyChanged(() => Version);
            }
        }

        public int Location_ID
        {
            get => _inspection.Locatie_ID;
            set
            {
                _inspection.Locatie_ID = value; RaisePropertyChanged(() => Location_ID);
            }
        }

        public bool Accomplished
        {
            get => _inspection.Voltooid;
            set
            {
                _inspection.Voltooid = value; RaisePropertyChanged(() => Accomplished);
            }
        }
    }
}
