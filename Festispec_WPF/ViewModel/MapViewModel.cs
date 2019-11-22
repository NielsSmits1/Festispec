using BingMapsRESTToolkit;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Geocoding;
using Geocoding.Microsoft;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Festispec_WPF.ViewModel
{
    public class MapViewModel : ViewModelBase
    {
        private IGeocoder geocoder = new BingMapsGeocoder(ApiKeys.BING_MAPS_KEY);
        private MapPolyline lastLine;

        private ObservableCollection<UIElement> mapElements = new ObservableCollection<UIElement>();
        public ObservableCollection<InspectorVM> Inspectors { get; set; }
        public ObservableCollection<InspectionVM> Festivals { get; set; }
        public CollectionViewSource ViewSource { get; set; }

        #region VisibilityProperties

        private string _inspectorVisibility;

        public string InspectorVisibility
        {
            get { return _inspectorVisibility; }
            set
            {
                _inspectorVisibility = value;
                base.RaisePropertyChanged();
            }
        }

        private string _buttonControlVisibility;

        public string ButtonControlVisibility
        {
            get { return _buttonControlVisibility; }
            set
            {
                _buttonControlVisibility = value;
                base.RaisePropertyChanged();
            }
        }

        private string _inspectionVisibility;

        public string InspectionVisibility
        {
            get { return _inspectionVisibility; }
            set
            {
                _inspectionVisibility = value;
                base.RaisePropertyChanged();
            }
        }

        private string _planInspectorVisibility;

        public string PlanInspectorVisibility
        {
            get { return _planInspectorVisibility; }
            set
            {
                _planInspectorVisibility = value;
                base.RaisePropertyChanged();
            }
        }
        #endregion

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

        private InspectorVM _selectedInspector;
        public InspectorVM SelectedInspector
        {
            get { return _selectedInspector; }
            set
            {
                _selectedInspector = value;
                base.RaisePropertyChanged();
            }
        }

        private InspectionVM _selectedFestival;
        public InspectionVM SelectedFestival
        {
            get { return _selectedFestival; }
            set
            {
                _selectedFestival = value;
                base.RaisePropertyChanged();
            }
        }

        public ICommand ShowInspectorCommand { get; set; }
        public ICommand ShowInspectorListCommand { get; set; }
        public ICommand ShowInspectionListCommand { get; set; }
        public ICommand PlanInspectorCommand { get; set; }
        public ICommand CancelPlanningCommand { get; set; }

        public MapViewModel()
        {
            ShowInspectorCommand = new RelayCommand(showInspectorInfo);
            ShowInspectorListCommand = new RelayCommand(showInspectorList);
            ShowInspectionListCommand = new RelayCommand(showInspectionList);
            PlanInspectorCommand = new RelayCommand(planInspector);
            CancelPlanningCommand = new RelayCommand(cancelPlanning);

            InspectorVisibility = "Hidden";
            PlanInspectorVisibility = "Hidden";
            ButtonControlVisibility = "Hidden";

            //---INSPECTORS

            using (var context = new FestiSpecEntities())
            {
                var inspectorList = context.Inspecteur.ToList().Select(i => new InspectorVM(i));
                Inspectors = new ObservableCollection<InspectorVM>(inspectorList);

                var InspectionList = context.Inspectie.ToList().Select(i => new InspectionVM(i));
                Festivals = new ObservableCollection<InspectionVM>(InspectionList);
            }

            ViewSource = new CollectionViewSource();
            ViewSource.Source = Inspectors;

            foreach (var inspector in Inspectors)
            {
                using (var context = new FestiSpecEntities())
                {
                    var festivalNAW = context.NAW_inspecteur.Where(n => n.ID == inspector.NAW).FirstOrDefault();

                    var location = geocoder.Geocode(festivalNAW.Straatnaam + " " + festivalNAW.Huisnummer, "", "", festivalNAW.Postcode, "Netherlands").First();

                    Pushpin pin = new Pushpin();

                    //TODO button inspector info
                    Button button = new Button();
                    button.Width = 45;
                    button.Height = 45;
                    button.Opacity = 0;
                    button.Cursor = Cursors.Hand;
                    //button.Command = ShowInspectorCommand;

                    pin.Content = button;
                    pin.Location = new Microsoft.Maps.MapControl.WPF.Location(location.Coordinates.Latitude, location.Coordinates.Longitude);

                    MapElements.Add(pin);
                }
            }

            //---INSPECTIONS
            foreach (var festival in Festivals)
            {
                using (var context = new FestiSpecEntities())
                {
                    var inspectionNAW = context.Locatie.Where(n => n.ID == festival.Locatie_ID).FirstOrDefault();

                    var location = geocoder.Geocode(inspectionNAW.Straatnaam + " " + inspectionNAW.Huisnummer, "", "", inspectionNAW.Postcode, "Netherlands").First();

                    Pushpin pin = new Pushpin();

                    //TODO button clickable
                    Button button = new Button();
                    button.Width = 45;
                    button.Height = 45;
                    button.Opacity = 0;
                    button.Cursor = Cursors.Hand;

                    pin.Content = button;
                    pin.Background = new SolidColorBrush(Color.FromArgb(100,100,100,100));
                    pin.Location = new Microsoft.Maps.MapControl.WPF.Location(location.Coordinates.Latitude, location.Coordinates.Longitude);

                    MapElements.Add(pin);
                }
            }
        }

        private async Task calculateRoute()
        {
            MapElements.Remove(lastLine);

            Geocoding.Address inspectorLocation;
            Geocoding.Address festivalLocation;

            using (var context = new FestiSpecEntities())
            {
                var festivalNAW = context.Locatie.Where(l => l.ID == _selectedFestival.Locatie_ID).FirstOrDefault();
                festivalLocation = geocoder.Geocode(festivalNAW.Straatnaam + " " + festivalNAW.Huisnummer, "", "", festivalNAW.Postcode, "Netherlands").First();

                var inspectorNAW = context.NAW_inspecteur.Where(n => n.ID == _selectedInspector.NAW).FirstOrDefault();
                inspectorLocation = geocoder.Geocode(inspectorNAW.Straatnaam + " " + inspectorNAW.Huisnummer, "", "", inspectorNAW.Postcode, "Netherlands").First();

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
                        Address = inspectorLocation.FormattedAddress
                    },
                    new SimpleWaypoint(){
                        Address = festivalLocation.FormattedAddress
                    }
                },
                BingMapsKey = ApiKeys.BING_MAPS_KEY
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
                lastLine = routeLine;
                MapElements.Add(routeLine);
            }
        }

        private async void calculateDistances()
        {
            MapElements.Remove(lastLine);

            Geocoding.Address inspectorLocation;
            Geocoding.Address festivalLocation;

            using (var context = new FestiSpecEntities())
            {
                var festivalNAW = context.Locatie.Where(l => l.ID == _selectedFestival.Locatie_ID).FirstOrDefault();
                festivalLocation = geocoder.Geocode(festivalNAW.Straatnaam + " " + festivalNAW.Huisnummer, "", "", festivalNAW.Postcode, "Netherlands").First();
            }

            foreach (var inspector in Inspectors)
            {
                using (var context = new FestiSpecEntities())
                {
                    var inspectorNAW = context.NAW_inspecteur.Where(n => n.ID == inspector.NAW).FirstOrDefault();
                    inspectorLocation = geocoder.Geocode(inspectorNAW.Straatnaam + " " + inspectorNAW.Huisnummer, "", "", inspectorNAW.Postcode, "Netherlands").First();
                }

                var r1 = new RouteRequest()
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
                        new SimpleWaypoint() {
                            Address = inspectorLocation.FormattedAddress
                        },
                        new SimpleWaypoint() {
                            Address = festivalLocation.FormattedAddress
                        }
                    },
                    BingMapsKey = ApiKeys.BING_MAPS_KEY
                };

                var res = await ServiceManager.GetResponseAsync(r1);

                if (res != null &&
                    res.ResourceSets != null &&
                    res.ResourceSets.Length > 0 &&
                    res.ResourceSets[0].Resources != null &&
                    res.ResourceSets[0].Resources.Length > 0)
                {
                    var route = res.ResourceSets[0].Resources[0] as Route;
                    inspector.TravelDistance = route.TravelDistance;
                }
            }

            ViewSource.SortDescriptions.Add(new SortDescription("TravelDistance", ListSortDirection.Ascending));
            ViewSource.View.Refresh();

            PlanInspectorVisibility = "Visible";
            InspectorVisibility = "Visible";
            ButtonControlVisibility = "Visible";
            InspectionVisibility = "Hidden";
        }

        private void showInspectorInfo()
        {
            calculateRoute();
        }

        private void showInspectorList()
        {
            InspectorVisibility = "Visible";
            InspectionVisibility = "Hidden";
        }

        private void showInspectionList()
        {
            InspectionVisibility = "Visible";
            InspectorVisibility = "Hidden";
        }

        private void planInspector()
        {
           calculateDistances();
        }

        private void cancelPlanning()
        {
            ButtonControlVisibility = "Hidden";
            PlanInspectorVisibility = "Hidden";
            SelectedFestival = null;
        }
    }
}