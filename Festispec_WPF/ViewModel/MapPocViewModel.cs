using BingMapsRESTToolkit;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Geocoding;
using Geocoding.Microsoft;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Festispec_WPF.ViewModel
{
    public class MapPocViewModel : ViewModelBase
    {
        private IGeocoder geocoder = new BingMapsGeocoder("y9hnZxzeFbanowjRfwhq~QWOfrctUka_a19PmxVHotQ~As2wF_8SrcXbm2udFzP2wlDINKErT2AFPoA_bSzfTnPVEYm5rqni2C30NJwMO7tW");

        private ObservableCollection<UIElement> mapElements = new ObservableCollection<UIElement>();

        public ObservableCollection<InspectorViewModel> Inspectors { get; set; }

        public ObservableCollection<UIElement> MapElements
        {
            get
            {
                return mapElements;
            }
            set
            {
                mapElements = value;
                base.RaisePropertyChanged();
            }
        }

        private InspectorViewModel _selectedInspector;
        public InspectorViewModel SelectedInspector
        {
            get { return _selectedInspector; }
            set
            {
                _selectedInspector = value;
                base.RaisePropertyChanged();
            }
        }

        public ICommand ShowInspectorCommand { get; set; }

        public MapPocViewModel()
        {
            ShowInspectorCommand = new RelayCommand(showInspectorInfo);

            using (var context = new FestiSpecEntities())
            {
                var inspectorList = context.Inspecteur.ToList().Select(i => new InspectorViewModel(i));
                Inspectors = new ObservableCollection<InspectorViewModel>(inspectorList);
            }

            foreach (var inspector in Inspectors)
            { 
                using (var context = new FestiSpecEntities())
                {
                    var inspectorNAW = context.NAW_inspecteur.Where(n => n.ID == inspector.NAW).FirstOrDefault();

                    var location = geocoder.Geocode("Jupiterhof " + inspectorNAW.Huisnummer, "", "", inspectorNAW.Postcode, "Netherlands").First();

                    Pushpin pin = new Pushpin();

                    Button button = new Button();
                    button.Width = 45;
                    button.Height = 45;
                    button.Opacity = 0;

                    pin.Content = button;
                    pin.Location = new Microsoft.Maps.MapControl.WPF.Location(location.Coordinates.Latitude, location.Coordinates.Longitude);

                    MapElements.Add(pin);
                } 
            }
        }

        public async Task calculateRoute()
        {
            Geocoding.Address location;

            using (var context = new FestiSpecEntities())
            {
                var inspectorNAW = context.NAW_inspecteur.Where(n => n.ID == _selectedInspector.NAW).FirstOrDefault();

                location = geocoder.Geocode("Jupiterhof " + inspectorNAW.Huisnummer, "", "", inspectorNAW.Postcode, "Netherlands").First();
            }

            var r = new RouteRequest()
            {
                RouteOptions = new RouteOptions()
                {
                    RouteAttributes = new List<RouteAttributeType>()
                {
                    RouteAttributeType.RoutePath
                }
                },
                Waypoints = new List<SimpleWaypoint>()
                {
                    new SimpleWaypoint(){
                        Address = location.FormattedAddress
                    },
                    new SimpleWaypoint(){
                        Address = "Onderwijsboulevard 2, Den Bosch, Den Bosch"
                    }
                },
                BingMapsKey = "y9hnZxzeFbanowjRfwhq~QWOfrctUka_a19PmxVHotQ~As2wF_8SrcXbm2udFzP2wlDINKErT2AFPoA_bSzfTnPVEYm5rqni2C30NJwMO7tW"
            };

            var response = await ServiceManager.GetResponseAsync(r);

            if (response != null &&
                response.ResourceSets != null &&
                response.ResourceSets.Length > 0 &&
                response.ResourceSets[0].Resources != null &&
                response.ResourceSets[0].Resources.Length > 0)
            {
                var route = response.ResourceSets[0].Resources[0] as Route;
                var coords = route.RoutePath.Line.Coordinates;
                var locs = new LocationCollection();

                for (int i = 0; i < coords.Length; i++)
                {
                    locs.Add(new Microsoft.Maps.MapControl.WPF.Location(coords[i][0], coords[i][1]));
                }

                var routeLine = new MapPolyline()
                {
                    Locations = locs,
                    Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Blue),
                    StrokeThickness = 5
                };

                MapElements.Add(routeLine);
            }
        }

        public void showInspectorInfo()
        {
            calculateRoute();
        }
    }
}
