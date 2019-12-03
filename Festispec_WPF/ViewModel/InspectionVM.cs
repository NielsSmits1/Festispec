
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

namespace Festispec_WPF.ViewModel
{
    public class InspectionVM : ViewModelBase
    {

        private Inspectie _inspection;
        private LocationVM _location;
        private CustomerVM _customer;
        private UnitOfWork _UOW;
        public InspectionVM()
        {
            _inspection = new Inspectie();
            _location = new LocationVM();
            _customer = new CustomerVM();
            StartDate = DateTime.Now.Date;
            EndDate = DateTime.Now.Date.AddDays(1);
        }

        public InspectionVM(Inspectie inspectie)
        {
            _UOW = new ViewModelLocator().UOW;
            _inspection = inspectie;
            _location = new LocationVM(_UOW.InspectionLocations.Get(Location_ID));
            _customer = new CustomerVM(_UOW.Customers.Get(Customer_ID));
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

                using (var context = new FestiSpecEntities())
                {
                    var inspectionNAW = context.Locatie.Where(l => l.ID == _inspection.Locatie_ID).FirstOrDefault();
                    var location = geocoder.Geocode(inspectionNAW.Straatnaam + " " + inspectionNAW.Huisnummer, "", "", inspectionNAW.Postcode, "Netherlands").First();

                    return location.FormattedAddress;
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
