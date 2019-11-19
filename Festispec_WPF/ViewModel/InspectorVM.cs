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

namespace Festispec_WPF.ViewModel
{
    public class InspectorVM : ViewModelBase
    {
        public ObservableCollection<CertificateVM> ChosenCertificates { get; set;}
        public InspectorVM()
        {
            ChosenCertificates = new ObservableCollection<CertificateVM>();
            _nawInspecteur = new NAW_inspecteur();
            _inspecteur = new Inspecteur();
            _phonenumber = new Telefoonnummer_inspecteur();
        }

        public InspectorVM(NAW_inspecteur NAW)
        {
            _nawInspecteur = NAW;
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
        }

        private Telefoonnummer_inspecteur _phonenumber;

        public Telefoonnummer_inspecteur PhonenumberModel
        {
            get
            {
                return _phonenumber;
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
            get { return _nawInspecteur.Voornaam ; }
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
            set { _inspecteur.NAW = NAWInspector_ID;RaisePropertyChanged("InspectorForeignNAWID"); }
        }

        public bool Active
        {
            get { return _inspecteur.Actief; }
            set { _inspecteur.Actief = value; RaisePropertyChanged("Active"); }
        }

        public string Phonenumber
        {
            get { return _phonenumber.Telefoonnummer; }
            set { _phonenumber.Telefoonnummer = value; Phonenumber_NAWInspector_ID = 0; RaisePropertyChanged("Phonenuber");  }
        }

        public int Phonenumber_NAWInspector_ID
        {
            get { return _phonenumber.NAW_Inspecteur_ID; }
            set { _phonenumber.NAW_Inspecteur_ID = NAWInspector_ID;RaisePropertyChanged("Phonenumber_NAWInspector_ID"); }
        }

    }
}
