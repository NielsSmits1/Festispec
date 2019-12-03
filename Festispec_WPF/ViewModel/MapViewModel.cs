using BingMapsRESTToolkit;
using FestiSpec.Domain.Model;
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
        public ObservableCollection<InspectorVM> SingleInspector { get; set; }
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

        private string _singleInspectorVisibility;

        public string SingleInspectorVisibility
        {
            get { return _singleInspectorVisibility; }
            set
            {
                _singleInspectorVisibility = value;
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

        private string _searchText;

        public string searchText {
            get
            {
                return _searchText;
            }
            set
            {
                _searchText = value;
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
                RaisePropertyChanged("SelectedInspector");
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
        public ICommand SearchDataGrid { get; set; }

        public MapViewModel()
        {
            ShowInspectorCommand = new RelayCommand<object>(showInspectorRoute);
            ShowInspectorListCommand = new RelayCommand(showInspectorList);
            ShowInspectionListCommand = new RelayCommand(showInspectionList);
            PlanInspectorCommand = new RelayCommand(planInspector);
            CancelPlanningCommand = new RelayCommand(cancelPlanning);
            SearchDataGrid = new RelayCommand(searchDatagrid);

            InspectorVisibility = "Hidden";
            PlanInspectorVisibility = "Hidden";
            ButtonControlVisibility = "Hidden";
            SingleInspectorVisibility = "Hidden";

            searchText = "Zoek naam";

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
                    var location = getInspectorLocation(inspector);

                    Pushpin pin = new Pushpin();

                    Button button = new Button();
                    button.Width = 45;
                    button.Height = 45;
                    button.Opacity = 0;
                    button.Cursor = Cursors.Hand;
                    button.Command = ShowInspectorCommand;
                    button.CommandParameter = inspector.Inspector_ID;

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
                    var location = getFestivalLocation(festival);

                    Pushpin pin = new Pushpin();

                    pin.Background = new SolidColorBrush(Color.FromArgb(100,100,100,100));
                    pin.Location = new Microsoft.Maps.MapControl.WPF.Location(location.Coordinates.Latitude, location.Coordinates.Longitude);

                    MapElements.Add(pin);
                }
            }
        }

        private void searchDatagrid()
        {
            if(InspectorVisibility == "Hidden")
            {
                Festivals = null;

                using (var context = new FestiSpecEntities())
                {
                    if(searchText == "")
                    {
                        var InspectionList = context.Inspectie.ToList().Select(i => new InspectionVM(i));
                        Festivals = new ObservableCollection<InspectionVM>(InspectionList);
                    }
                    else
                    {
                        var InspectionList = context.Inspectie.ToList().Select(f => new InspectionVM(f)).Where(f => f.Title.ToLower().Contains(searchText.ToLower()));
                        Festivals = new ObservableCollection<InspectionVM>(InspectionList);
                    }
                }

                if(Festivals.Count == 0 && searchText != "")
                {
                    var festival = new InspectionVM();
                    festival.Title = "Geen zoekresultaten";
                    Festivals.Add(festival);
                }

                RaisePropertyChanged("Festivals");
            }
            else
            {
                Inspectors = null;
                ViewSource.Source = null;

                using (var context = new FestiSpecEntities())
                {
                    if (searchText == "")
                    {
                        var inspectorList = context.Inspecteur.ToList().Select(i => new InspectorVM(i));
                        Inspectors = new ObservableCollection<InspectorVM>(inspectorList);
                    }
                    else
                    {
                        var inspectorList = context.Inspecteur.ToList().Select(i => new InspectorVM(i)).Where(i => i.UserName.ToLower().Contains(searchText.ToLower()));
                        Inspectors = new ObservableCollection<InspectorVM>(inspectorList);
                    }   
                }

                if (Inspectors.Count == 0 && searchText != "")
                {
                    var inspector = new InspectorVM();
                    inspector.UserName = "Geen zoekresultaten";
                    Inspectors.Add(inspector);
                }

                ViewSource.Source = Inspectors;

                if (SelectedFestival != null)
                {
                    calculateDistances();
                }
            }
        }

        private async Task calculateRoute(object inpsector_id)
        {
            MapElements.Remove(lastLine);

            var festivalLocation = getFestivalLocation(null);

            using (var context = new FestiSpecEntities())
            {
                if(inpsector_id != null)
                {
                    SelectedInspector = new InspectorVM(context.Inspecteur.Where(i => i.ID == (int)inpsector_id).FirstOrDefault());
                }
            }

            var inspectorLocation = getInspectorLocation(null);

            calculateSingleRoute(inspectorLocation, festivalLocation, null);
        }

        public void selectSingleInspector(object inspector_id)
        {
            using (var context = new FestiSpecEntities())
            {
                var inspectorList = context.Inspecteur.ToList().Select(i => new InspectorVM(i)).Where(i => i.Inspector_ID == (int)inspector_id);
                SingleInspector = new ObservableCollection<InspectorVM>(inspectorList);
                calculateSingleRoute(getInspectorLocation(inspectorList.First()), getFestivalLocation(SelectedFestival), SingleInspector.First());
            }

            RaisePropertyChanged("SingleInspector");
        }

        private void calculateDistances()
        {
            var festivalLocation = getFestivalLocation(null);

            foreach (var inspector in Inspectors)
            {
                var inspectorLocation = getInspectorLocation(inspector);

                calculateSingleRoute(inspectorLocation, festivalLocation, inspector);
            }

            ViewSource.SortDescriptions.Add(new SortDescription("TravelDistance", ListSortDirection.Ascending));
            ViewSource.View.Refresh();

            PlanInspectorVisibility = "Visible";
            InspectorVisibility = "Visible";
            ButtonControlVisibility = "Visible";
            InspectionVisibility = "Hidden";
        }

        private async void calculateSingleRoute(Geocoding.Address inspectorLocation, Geocoding.Address festivalLocation, InspectorVM inspector)
        {
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
                        new SimpleWaypoint() {
                            Address = inspectorLocation.FormattedAddress
                        },
                        new SimpleWaypoint() {
                            Address = festivalLocation.FormattedAddress
                        }
                    },
                BingMapsKey = ApiKeys.BING_MAPS_KEY
            };

            var res = await ServiceManager.GetResponseAsync(r);

            if (res != null &&
                res.ResourceSets != null &&
                res.ResourceSets.Length > 0 &&
                res.ResourceSets[0].Resources != null &&
                res.ResourceSets[0].Resources.Length > 0)
            {
                var route = res.ResourceSets[0].Resources[0] as Route;
                if (inspector != null)
                {
                    inspector.TravelDistance = route.TravelDistance;
                }
                else
                {
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
        }

        private Geocoding.Address getFestivalLocation(InspectionVM festival)
        {
            Locatie festivalNAW;

            using (var context = new FestiSpecEntities())
            {
                if (festival != null)
                {
                    festivalNAW = context.Locatie.Where(l => l.ID == festival.Location_ID).FirstOrDefault();
                }
                else
                {
                    festivalNAW = context.Locatie.Where(l => l.ID == _selectedFestival.Location_ID).FirstOrDefault();
                }

                return geocoder.Geocode(festivalNAW.Straatnaam + " " + festivalNAW.Huisnummer, "", "", festivalNAW.Postcode, "Netherlands").First();
            }
        }

        private Geocoding.Address getInspectorLocation(InspectorVM inspector)
        {
            NAW_inspecteur inspectorNAW;

            using (var context = new FestiSpecEntities())
            {
                if(inspector != null)
                {
                    inspectorNAW = context.NAW_inspecteur.Where(n => n.ID == inspector.InspectorForeignNAWID).FirstOrDefault();
                }
                else
                {
                    inspectorNAW = context.NAW_inspecteur.Where(n => n.ID == _selectedInspector.InspectorForeignNAWID).FirstOrDefault();
                }
                
                return geocoder.Geocode(inspectorNAW.Straatnaam + " " + inspectorNAW.Huisnummer, "", "", inspectorNAW.Postcode, "Netherlands").First();
            }
        }

        private void showInspectorRoute(object inspector_id)
        {
            calculateRoute(inspector_id);

            if (inspector_id != null && _selectedFestival != null)
            {
                selectSingleInspector(inspector_id);
                SingleInspectorVisibility = "Visible";
            }
        }

        private void showInspectorList()
        {
            InspectorVisibility = "Visible";
            InspectionVisibility = "Hidden";
            SingleInspectorVisibility = "Hidden";
        }

        private void showInspectionList()
        {
            InspectionVisibility = "Visible";
            InspectorVisibility = "Hidden";
            SingleInspectorVisibility = "Hidden";
        }

        private void planInspector()
        {
            calculateDistances();
        }

        private void cancelPlanning()
        {
            ButtonControlVisibility = "Hidden";
            PlanInspectorVisibility = "Hidden";
            SingleInspectorVisibility = "Hidden";
            SelectedFestival = null;
            MapElements.Remove(lastLine);
        }
    }
}