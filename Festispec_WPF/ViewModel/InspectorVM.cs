using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec_WPF.ViewModel
{
    public class InspectorVM : ViewModelBase
    {
        public ICommand AddInspectorCommand { get; set; }
        public InspectorVM()
        {
            _nawInspecteur = new NAW_inspecteur();
            _inspecteur = new Inspecteur();
            AddInspectorCommand = new RelayCommand(AddInspector);
        }

        public void AddInspector()
        {
            using(var context = new FestiSpecEntities())
            {
                Active = true;
                context.NAW_inspecteur.Add(_nawInspecteur);
                context.Inspecteur.Add(_inspecteur);
                context.SaveChanges();
            }
        }

        //NAW data
        private NAW_inspecteur _nawInspecteur;

        //Inspector in system
        private Inspecteur _inspecteur;

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
            get { return _inspecteur.Gebruikersnaam; }
            set { _inspecteur.Gebruikersnaam = value; RaisePropertyChanged("UserName"); }
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

    }

}
