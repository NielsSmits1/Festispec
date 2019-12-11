using FestiSpec.Domain.Model;
using Festispec_WPF.Model.UnitOfWork;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.ViewModel 
{
    public class ApplicantVM : ViewModelBase
    {
        private UnitOfWork _UOW;
        private Applicant _applicant;
        public ApplicantVM(Applicant applicant)
        {
            _applicant = applicant;
        }


        public string FullName
        {
            get
            {
                return FirstName + " " + InBetween + " " + LastName;
            }
        }

        public string FirstName
        {
            get { return _applicant.Voornaam; }
            set { _applicant.Voornaam = value; RaisePropertyChanged("FirstName"); }
        }

        public string LastName
        {
            get { return _applicant.Achternaam; }
            set { _applicant.Achternaam = value; RaisePropertyChanged("LastName"); }
        }
        public string InBetween
        {
            get { return _applicant.Tussenvoegsel; }
            set { _applicant.Tussenvoegsel = value; RaisePropertyChanged("InBetween"); }
        }

        public string HomeNumber
        {
            get { return _applicant.Huisnummer; }
            set { _applicant.Huisnummer = value; RaisePropertyChanged("HomeNumber"); }
        }

        public string ZipCode
        {
            get
            {
                return _applicant.Postcode;
            }
            set { _applicant.Postcode = value; RaisePropertyChanged("ZipCode"); }
        }

        public string StreetName
        {
            get
            {
                return _applicant.Straatnaam;
            }
            set { _applicant.Straatnaam = value; RaisePropertyChanged("StreetName"); }
        }

        public DateTime DateOfBirth
        {
            get { return _applicant.Geboortedatum; }
            set { _applicant.Geboortedatum = value; RaisePropertyChanged("DateOfBirth"); }
        }

        public string IBAN
        {
            get { return _applicant.IBAN; }
            set { _applicant.IBAN = value; RaisePropertyChanged("IBAN"); }
        }

        public string Email
        {
            get { return _applicant.Email; }
            set { _applicant.Email = value; RaisePropertyChanged("Email"); }
        }


        public string Phonenumber
        {
            get { return _applicant.Telefoonnummer; }
            set { _applicant.Telefoonnummer = value; RaisePropertyChanged("Phonenuber"); }
        }



    }
}
