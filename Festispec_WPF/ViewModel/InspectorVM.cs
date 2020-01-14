using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Geocoding;
using Geocoding.Microsoft;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FestiSpec.Domain.Model;
using Festispec_WPF.Model.UnitOfWork;

namespace Festispec_WPF.ViewModel
{
    public class InspectorVM : ViewModelBase
    {
        public ObservableCollection<CertificateVM> ChosenCertificates { get; set; }
        private UnitOfWork _UOW;
        public InspectorVM()
        {
            ChosenCertificates = new ObservableCollection<CertificateVM>();
            _nawInspecteur = new NAW_inspecteur();
            _inspecteur = new Inspecteur();
            DateOfBirth = DateTime.Today;
        }

        public InspectorVM(NAW_inspecteur NAW)
        {
            _UOW = ViewModelLocator.UOW;
            _nawInspecteur = NAW;
            _inspecteur = _UOW.Inspectors.Find(ins => ins.NAW == _nawInspecteur.ID).FirstOrDefault();
        }

        public InspectorVM(Inspecteur inspector)
        {
            if (inspector.ID == 0)
            {
                _inspecteur = inspector;
            }
            else
            {
                _UOW = ViewModelLocator.UOW;
                _inspecteur = inspector;
                _nawInspecteur = _UOW.NAWInspectors.Find(ins => ins.ID == _inspecteur.NAW).FirstOrDefault();
                ChosenCertificates = new ObservableCollection<CertificateVM>(_UOW.Inspectors.GetCertificatesInspector(Inspector_ID).Select(ct => new CertificateVM(ct)));
            }
        }

        public void FillNAW(ApplicantVM applicant)
        {
            _nawInspecteur = new NAW_inspecteur();
            FirstName = applicant.FirstName;
            InBetween = applicant.InBetween;
            LastName = applicant.LastName;
            Email = applicant.Email;
            DateOfBirth = applicant.DateOfBirth;
            IBAN = applicant.IBAN;
            ZipCode = applicant.ZipCode;
            StreetName = applicant.StreetName;
            HomeNumber = applicant.HomeNumber;
            Phonenumber = applicant.Phonenumber;
        }

        //NAW data
        private NAW_inspecteur _nawInspecteur;

        public NAW_inspecteur NAWInspector
        {
            get
            {
                return _nawInspecteur;
            }
        }

        //Inspector in system
        private Inspecteur _inspecteur;

        public Inspecteur InspectorData
        {
            get
            {
                return _inspecteur;
            }
            set
            {
                _inspecteur = value;
            }
        }


        public string FullName
        {
            get
            {
                return FirstName + " " + InBetween + " " + LastName;
            }
        }

        public int NAWInspector_ID
        {
            get { return _nawInspecteur.ID; }
            set { _nawInspecteur.ID = value; RaisePropertyChanged("NAWInspector_ID"); }
        }

        public string FirstName
        {
            get { return _nawInspecteur.Voornaam; }
            set { _nawInspecteur.Voornaam = value; RaisePropertyChanged("FirstName"); }
        }

        public string LastName
        {
            get { return _nawInspecteur.Achternaam; }
            set { _nawInspecteur.Achternaam = value; RaisePropertyChanged("LastName"); }
        }
        public string InBetween
        {
            get { return _nawInspecteur.Tussenvoegsel; }
            set { _nawInspecteur.Tussenvoegsel = value; RaisePropertyChanged("InBetween"); }
        }

        public string HomeNumber
        {
            get { return _nawInspecteur.Huisnummer; }
            set { _nawInspecteur.Huisnummer = value; RaisePropertyChanged("HomeNumber"); }
        }

        public string ZipCode
        {
            get
            {
                return _nawInspecteur.Postcode;
            }
            set { _nawInspecteur.Postcode = value; RaisePropertyChanged("ZipCode"); }
        }

        public string StreetName
        {
            get
            {
                return _nawInspecteur.Straatnaam;
            }
            set { _nawInspecteur.Straatnaam = value; RaisePropertyChanged("StreetName"); }
        }

        public DateTime DateOfBirth
        {
            get { return _nawInspecteur.Geboortedatum; }
            set { _nawInspecteur.Geboortedatum = value; RaisePropertyChanged("DateOfBirth"); }
        }

        public string IBAN
        {
            get { return _nawInspecteur.IBAN; }
            set { _nawInspecteur.IBAN = value; RaisePropertyChanged("IBAN"); }
        }

        public string Email
        {
            get { return _nawInspecteur.Email; }
            set { _nawInspecteur.Email = value; RaisePropertyChanged("Email"); }
        }

        public int Inspector_ID
        {
            get { return _inspecteur.ID; }
            set
            {
                _inspecteur.ID = value; RaisePropertyChanged("Inspector_ID");
            }
        }

        public string UserName
        {
            get { return _inspecteur.Username; }
            set { _inspecteur.Username = value; RaisePropertyChanged("UserName"); }
        }

        public string Password
        {
            get
            {
                return _inspecteur.Wachtwoord;
            }
            set { _inspecteur.Wachtwoord = value; RaisePropertyChanged("Password"); }
        }

        public int InspectorForeignNAWID
        {
            get { return _inspecteur.NAW; }
            set { _inspecteur.NAW = NAWInspector_ID; RaisePropertyChanged("InspectorForeignNAWID"); }
        }

        public bool Active
        {
            get { return _inspecteur.Actief; }
            set { _inspecteur.Actief = value; RaisePropertyChanged("Active"); }
        }



        public string Address
        {
            get
            {
                IGeocoder geocoder = new BingMapsGeocoder(ApiKeys.BING_MAPS_KEY);
                var inspectorNAW = _UOW.NAWInspectors.Find(n => n.ID == _inspecteur.NAW).FirstOrDefault();
                var location = geocoder.Geocode(inspectorNAW.Straatnaam + " " + inspectorNAW.Huisnummer, "", "", inspectorNAW.Postcode, "Netherlands").First();

                return location.FormattedAddress;
            }
        }

        private double _travelDistance;
        public double TravelDistance
        {
            get { return _travelDistance; }
            set { _travelDistance = value; RaisePropertyChanged("TravelDistance"); }
        }
        public string Phonenumber
        {
            get { return _nawInspecteur.Telefoonnummer; }
            set { _nawInspecteur.Telefoonnummer = value; RaisePropertyChanged("Phonenuber"); }
        }

        public string ActiveText
        {
            get
            {
                if (Active)
                {
                    return "Zet op inactief";
                }
                else
                {
                    return "Zet op actief";
                }
            }
        }

        public string Color
        {
            get
            {
                if (Active)
                {
                    return "Red";
                }
                else
                {
                    return "Green";
                }
            }
        }

        public int GetInspectionCount(int year)
        {
            return InspectorData.Inspectie.Where(ins => ins.StartDate.Year == year).Count();
        }


        public void EmptyAll()
        {
            ChosenCertificates = new ObservableCollection<CertificateVM>();
            _nawInspecteur = new NAW_inspecteur();
            _inspecteur = new Inspecteur();
            DateOfBirth = DateTime.Today;
            RaisePropertyChanged(null);
        }

    }
}