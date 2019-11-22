using GalaSoft.MvvmLight;
using Geocoding;
using Geocoding.Microsoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec_WPF.ViewModel
{
    public class InspectionVM : ViewModelBase
    {
        private Inspectie _inspectie;

        public InspectionVM(Inspectie inspection)
        {
            _inspectie = inspection;
        }

        public DateTime StartDate
        {
            get { return _inspectie.StartDate; }
            set { RaisePropertyChanged("Name"); }
        }

        public DateTime EndDate
        {
            get { return _inspectie.EndDate; }
            set { RaisePropertyChanged("Name"); }
        }

        public int Locatie_ID
        {
            get { return _inspectie.Locatie_ID; }
            set { RaisePropertyChanged("Name"); }
        }

        public string Title
        {
            get { return _inspectie.Titel; }
            set { RaisePropertyChanged("Name"); }
        }

        public string Address
        {
            get
            {
                IGeocoder geocoder = new BingMapsGeocoder(ApiKeys.BING_MAPS_KEY);

                using (var context = new FestiSpecEntities())
                {
                    var inspectionNAW = context.Locatie.Where(l => l.ID == _inspectie.Locatie_ID).FirstOrDefault();
                    var location = geocoder.Geocode(inspectionNAW.Straatnaam + " " + inspectionNAW.Huisnummer, "", "", inspectionNAW.Postcode, "Netherlands").First();

                    return location.FormattedAddress;
                }
            }
        }
    }
}
