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
    public class InspectorViewModel : ViewModelBase
    {
        private Inspecteur _inspector;

        public InspectorViewModel(Inspecteur inspector)
        {
            this._inspector = inspector;
        }

        public string Name
        {
            get { return _inspector.Gebruikersnaam; }
            set { _inspector.Gebruikersnaam = value; RaisePropertyChanged("Name"); }
        }

        public int NAW
        {
            get { return _inspector.NAW; }
            set { _inspector.NAW = value; RaisePropertyChanged("NAW"); }
        }

        public string Address
        {
            get
            {
                IGeocoder geocoder = new BingMapsGeocoder("y9hnZxzeFbanowjRfwhq~QWOfrctUka_a19PmxVHotQ~As2wF_8SrcXbm2udFzP2wlDINKErT2AFPoA_bSzfTnPVEYm5rqni2C30NJwMO7tW");

                using (var context = new FestiSpecEntities())
                {
                    var inspectorNAW = context.NAW_inspecteur.Where(n => n.ID == _inspector.NAW).FirstOrDefault();
                    var location = geocoder.Geocode("Jupiterhof " + inspectorNAW.Huisnummer, "", "", inspectorNAW.Postcode, "Netherlands").First();

                    return location.FormattedAddress;
                }
            }
        }
    }
}
