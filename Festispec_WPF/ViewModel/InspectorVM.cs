using System;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.ViewModel
{
    class InspectorVM : ViewModelBase
    {
        public InspectorVM()
        {
            this._inspecteur = new NAW_inspecteur();
        }

        internal NAW_inspecteur ToModel()
        {
            return _inspecteur;
        }

        private NAW_inspecteur _inspecteur;

        public string ID
        {
            get { return _inspecteur.ID; }
            set { _inspecteur.ID = value; RaisePropertyChanged("Id"); }
        }

        public string Voornaam
        {
            get { return _inspecteur.Voornaam; }
            set { _inspecteur.Voornaam = value; RaisePropertyChanged("Voornaam"); }
        }

        public string Tussenvoegsel
        {
            get { return _inspecteur.Tussenvoegsel; }
            set { _inspecteur.Tussenvoegsel = value; RaisePropertyChanged("Tussenvoegsel"); }
        }

        public string Achternaam
        {
            get { return _inspecteur.Achternaam; }
            set { _inspecteur.Achternaam = value; RaisePropertyChanged("Achternaam"); }
        }
        public string Postcode
        {
            get { return _inspecteur.Postcode; }
            set { _inspecteur.Postcode = value; RaisePropertyChanged("Postcode"); }
        }
        public string Huisnummer
        {
            get { return _inspecteur.Huisnummer; }
            set { _inspecteur.Huisnummer = value; RaisePropertyChanged("Huisnummer"); }
        }
        public DateTime Geboortedatum
        {
            get { return _inspecteur.Geboortedatum; }
            set { _inspecteur.Geboortedatum = value; RaisePropertyChanged("Geboortedatum"); }
        }
        public string IBAN
        {
            get { return _inspecteur.IBAN; }
            set { _inspecteur.IBAN = value; RaisePropertyChanged("IBAN"); }
        }
        public string Email
        {
            get { return _inspecteur.Email; }
            set { _inspecteur.Email = value; RaisePropertyChanged("Email"); }
        }
    }
}
