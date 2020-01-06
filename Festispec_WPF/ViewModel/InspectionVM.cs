
using Geocoding;
using Geocoding.Microsoft;
﻿using FestiSpec.Domain.Model;
using Festispec_WPF.Model;
using Festispec_WPF.Model.Repositories;
using Festispec_WPF.Model.UnitOfWork;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Festispec_WPF.ViewModel
{
    public class InspectionVM : ViewModelBase
    {

        private Inspectie _inspection;
        private LocationVM _location;
        private CustomerVM _customer;
        private UnitOfWork _UOW;

        public ObservableCollection<CertificateVM> ChosenCertificates { get; set; }
        public ObservableCollection<QuestionnaireVM> ChosenQuestionnaires { get; set; }

        public ObservableCollection<InspectorVM> PlannedInspectors { get; set; }
        public InspectionVM()
        {
            _UOW = ViewModelLocator.UOW;
            _inspection = new Inspectie();
            _location = new LocationVM();
            _customer = new CustomerVM();
            ChosenCertificates = new ObservableCollection<CertificateVM>();
            ChosenQuestionnaires = new ObservableCollection<QuestionnaireVM>();
            StartDate = DateTime.Now.Date;
            EndDate = DateTime.Now.Date.AddDays(1);
        }

        public InspectionVM(Inspectie inspectie)
        {
            _UOW = ViewModelLocator.UOW;
            _inspection = inspectie;

            if (_inspection.Klant_ID != 0)
            {
                _location = new LocationVM(_UOW.InspectionLocations.Get(Location_ID));
                _customer = new CustomerVM(_UOW.Customers.Get(Customer_ID));
                ChosenCertificates = new ObservableCollection<CertificateVM>(_UOW.Inspections.GetCertificatesByInspection(Inspection_ID).Select(cert => new CertificateVM(cert)));
                ChosenQuestionnaires = new ObservableCollection<QuestionnaireVM>(_UOW.Inspections.Get(Inspection_ID).Vragenlijst.Select(vr => new QuestionnaireVM(vr)).ToList());
                RefreshInspectors();
            }
            else
            {
                _location = new LocationVM(_inspection.Locatie);
                _customer = new CustomerVM(_inspection.Klant);
            }
        }

        public void RefreshInspectors()
        {
            PlannedInspectors = new ObservableCollection<InspectorVM>(_UOW.Inspections.Get(Inspection_ID).Inspecteur.Select(vr => new InspectorVM(vr)).ToList());
            RaisePropertyChanged(() => PlannedInspectors);
        }

        public Inspectie Inspection
        {
            get => _inspection;
            set
            {
                _inspection = value; RaisePropertyChanged(() => Inspection);
            }
        }
        public LocationVM Location
        {
            get => _location;
            set
            {
                _location = value; Location_ID = value.ID; RaisePropertyChanged(() => Location);
            }
        }

        public CustomerVM Customer
        {
            get => _customer;
            set
            {
                _customer = value; Customer_ID = Customer.ID; RaisePropertyChanged(() => Customer);
            }
            
        }

        public int Inspection_ID
        {
            get => _inspection.Inspectienummer;
            set
            {
                _inspection.Inspectienummer = value; RaisePropertyChanged(() => Inspection_ID);
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

        public string Address
        {
            get
            {
                IGeocoder geocoder = new BingMapsGeocoder(ApiKeys.BING_MAPS_KEY);

                if (_inspection.Klant_ID != 0)
                {
                    var inspectionNAW = _UOW.InspectionLocations.Find(l => l.ID == _inspection.Locatie_ID).FirstOrDefault();
                    var location = geocoder.Geocode(inspectionNAW.Straatnaam + " " + inspectionNAW.Huisnummer, "", "", inspectionNAW.Postcode, "Netherlands").First();

                    return location.FormattedAddress;
                }
                else
                {
                    var location = _location.StreetName + " " + _location.HomeNumber + ", " + _location.ZipCode;
                    return location;
                }
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
