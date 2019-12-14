using BingMapsRESTToolkit;
using FestiSpec.Domain.Model;
using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Geocoding;
using Geocoding.Microsoft;
using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Festispec_WPF.ViewModel
{
    public class MapViewModel : ViewModelBase
    {
        private IGeocoder geocoder = new BingMapsGeocoder(ApiKeys.BING_MAPS_KEY);
        private MapPolyline lastLine;
        private ObservableCollection<UIElement> mapElements = new ObservableCollection<UIElement>();
        public ObservableCollection<InspectorVM> Inspectors { get; set; }
        public ObservableCollection<LocationVM> Locations { get; set; }
        public ObservableCollection<CustomerVM> Customers { get; set; }
        public ObservableCollection<InspectorVM> SingleInspector { get; set; }
        public ObservableCollection<InspectionVM> Festivals { get; set; }

        public ObservableCollection<CertificateVM> AvailableCertificates { get; set; }
        public ObservableCollection<CertificateVM> LeftoverCertificates { get; set; }
        public CollectionViewSource ViewSource { get; set; }

        private CreateLocationWindow _createLocation;
        private CreateInspectionWindow _createWindow;
        public LocationVM NewLocation { get; set; }
        public InspectionVM NewInspection { get; set; }

        private UnitOfWork _UOW;

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

        private string _mapVisibility;

        public string MapVisibility
        {
            get => _mapVisibility;
            set { _mapVisibility = value; RaisePropertyChanged(() => MapVisibility); }
        }

        private string _editVisibility;

        public string EditVisibility
        {
            get => _editVisibility;
            set { _editVisibility = value; RaisePropertyChanged(() => EditVisibility); }
        }

        private string _mapErrorVisibility;
        public string MapErrorVisibility
        {
            get => _mapErrorVisibility;
            set { _mapErrorVisibility = value; RaisePropertyChanged(() => MapErrorVisibility); }
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

        public string searchText
        {
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

        private CustomerVM _selectedCustomer;

        public CustomerVM SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value; SelectedFestival.Customer = value; RaisePropertyChanged(() => SelectedCustomer); RaisePropertyChanged(() => SelectedFestival.Customer);
            }
        }

        public CustomerVM NewSelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                _selectedCustomer = value; NewInspection.Customer = value; RaisePropertyChanged(() => NewSelectedCustomer); RaisePropertyChanged(() => NewInspection.Customer);
            }
        }
        private LocationVM _selectedLocation;

        public LocationVM SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value; SelectedFestival.Location = value; RaisePropertyChanged(() => SelectedLocation); RaisePropertyChanged(() => SelectedFestival.Location);
            }
        }

        public LocationVM NewSelectedLocation
        {
            get => _selectedLocation;
            set
            {
                _selectedLocation = value; NewInspection.Location = value; RaisePropertyChanged(() => NewSelectedLocation); RaisePropertyChanged(() => NewInspection.Location);
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
                
                if(EditVisibility == "Visible" )
                {
                    _selectedFestival.ChosenCertificates = new ObservableCollection<CertificateVM>(_UOW.Inspections.GetCertificatesInspection(_selectedFestival.Inspection_ID).Select(c => new CertificateVM(c)));
                    LeftoverCertificates = new ObservableCollection<CertificateVM>(_UOW.Inspections.GetMissingCertificates(_selectedFestival.Inspection_ID).Select(cert => new CertificateVM(cert)));
                    RaisePropertyChanged(() => LeftoverCertificates);
                }
                RaisePropertyChanged(() => SelectedFestival);
            }
        }

        private CertificateVM _selectedCertificate;

       public CertificateVM SelectedCertificate
        {
            get => _selectedCertificate;
            set
            {
                if (AvailableCertificates.Contains(value))
                {
                    NewInspection.ChosenCertificates.Add(value); AvailableCertificates.Remove(value);
                }
                else
                {
                    AvailableCertificates.Add(value); NewInspection.ChosenCertificates.Remove(value);
                }
                
            }
        }

        public CertificateVM SelectedUpdateCertificate
        {
            get => _selectedCertificate;
            set
            {
                if (LeftoverCertificates.Contains(value))
                {
                    SelectedFestival.ChosenCertificates.Add(value); LeftoverCertificates.Remove(value);
                }
                else
                {
                    LeftoverCertificates.Add(value); SelectedFestival.ChosenCertificates.Remove(value);
                }
            }
        }

        public ICommand ShowInspectorCommand { get; set; }
        public ICommand ShowInspectorListCommand { get; set; }
        public ICommand ShowInspectionListCommand { get; set; }
        public ICommand PlanInspectorCommand { get; set; }
        public ICommand CancelPlanningCommand { get; set; }
        public ICommand SearchDataGrid { get; set; }
        public ICommand ShowDetailsFestivalCommand { get; set; }
        public ICommand RefreshFestivalsCommand { get; set; }
        public ICommand RefreshInspectorsCommand { get; set; }
        public ICommand testFunction { get; set; }
        public ICommand SafeEditCommand { get; set; }
        public ICommand CreateNewLocationCommand { get; set; }
        public ICommand CloseCreateCommand { get; set; }
        public ICommand OpenCreateWindowCommand { get; set; }
        public ICommand CreateNewInspectionCommand { get; set; }

        public ICommand OpenCreateLocationWindowCommand { get; set; }

        public MapViewModel()
        {
            
            ShowInspectorCommand = new RelayCommand<object>(showInspectorRoute);
            ShowInspectorListCommand = new RelayCommand(showInspectorList);
            ShowInspectionListCommand = new RelayCommand(showInspectionList);
            PlanInspectorCommand = new RelayCommand(planInspector);
            CancelPlanningCommand = new RelayCommand(cancelPlanning);
            SearchDataGrid = new RelayCommand(searchDatagrid);
            ShowDetailsFestivalCommand = new RelayCommand(showDetailsFestival);
            RefreshFestivalsCommand = new RelayCommand(LoadFestivals);
            RefreshInspectorsCommand = new RelayCommand(LoadInspectors);
            testFunction = new RelayCommand(downloadInspection);

            SafeEditCommand = new RelayCommand(complete);
            CreateNewLocationCommand = new RelayCommand(AddNewLocation);
            OpenCreateLocationWindowCommand = new RelayCommand(OpenCreateLocationWindow);
            CreateNewInspectionCommand = new RelayCommand(AddNewInspection);
            OpenCreateWindowCommand = new RelayCommand(OpenCreateWindow);
            CloseCreateCommand = new RelayCommand(CloseCreate);
            InspectorVisibility = "Hidden";
            PlanInspectorVisibility = "Hidden";
            ButtonControlVisibility = "Hidden";
            SingleInspectorVisibility = "Hidden";
            MapVisibility = "Visible";
            EditVisibility = "Hidden";
            MapErrorVisibility = "Hidden";

            searchText = "Zoek naam";

            //---INSPECTORS
            try
            {
                _UOW = new ViewModelLocator().UOW;
                LoadInspectors();

                LoadFestivals();

                ViewSource = new CollectionViewSource();
                ViewSource.Source = Inspectors;

                foreach (var inspector in Inspectors)
                {

                    var location = getInspectorLocation(inspector);

                    Pushpin pin = new Pushpin();

                    Button button = new Button();
                    button.Width = 45;
                    button.Height = 45;
                    button.Opacity = 0;
                    button.Cursor = System.Windows.Input.Cursors.Hand;
                    button.Command = ShowInspectorCommand;
                    button.CommandParameter = inspector.Inspector_ID;

                    pin.Content = button;
                    pin.Location = new Microsoft.Maps.MapControl.WPF.Location(location.Coordinates.Latitude, location.Coordinates.Longitude);

                    MapElements.Add(pin);

                }

                //---INSPECTIONS
                foreach (var festival in Festivals)
                {
                    var location = getFestivalLocation(festival);

                    Pushpin pin = new Pushpin();

                    pin.Background = new SolidColorBrush(Color.FromArgb(100, 100, 100, 100));
                    pin.Location = new Microsoft.Maps.MapControl.WPF.Location(location.Coordinates.Latitude, location.Coordinates.Longitude);

                    MapElements.Add(pin);
                }
            }
            catch (Exception)
            {
                MapErrorVisibility = "Visible";
                LoadOfflineFestival();
            }
        }
    
        private void downloadInspection()
        {
            var location = new Locatie
            {
                Huisnummer = _selectedFestival.Location.HomeNumber,
                Straatnaam = _selectedFestival.Location.StreetName,
                Postcode = _selectedFestival.Location.ZipCode
            };

            var customer = new Klant
            {
                Bedrijfsnaam = _selectedFestival.Customer.CompanyName,
            };

            var obj = new Inspectie
            {
                Titel = _selectedFestival.Title,
                Locatie = location,
                Klant = customer,
                StartDate = _selectedFestival.StartDate,
                EndDate = _selectedFestival.EndDate,
                Versie = _selectedFestival.Version
            };

            var json = new JavaScriptSerializer().Serialize(obj);
            System.IO.File.WriteAllText(@"../../inspection.json", json);
        }

        private void searchDatagrid()
        {
            if(InspectorVisibility == "Hidden")
            {
                Festivals = null;

                
                if(searchText == "")
                {
                    var InspectionList = _UOW.Inspections.GetAll().ToList().Select(i => new InspectionVM(i));
                    Festivals = new ObservableCollection<InspectionVM>(InspectionList);
                }
                else
                {
                    var InspectionList = _UOW.Inspections.GetAll().ToList().Select(f => new InspectionVM(f)).Where(f => f.Title.ToLower().Contains(searchText.ToLower()));
                    Festivals = new ObservableCollection<InspectionVM>(InspectionList);
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

                if (searchText == "")
                {
                    var inspectorList = _UOW.Inspectors.GetAll().ToList().Select(i => new InspectorVM(i));
                    Inspectors = new ObservableCollection<InspectorVM>(inspectorList);
                }
                else
                {
                    var inspectorList = _UOW.Inspectors.GetAll().ToList().Select(i => new InspectorVM(i)).Where(i => i.UserName.ToLower().Contains(searchText.ToLower()));
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
        

        private async Task calculateRoute(object inpsector_id)
        {
            MapElements.Remove(lastLine);

            var festivalLocation = getFestivalLocation(null);

                if(inpsector_id != null)
                {
                    SelectedInspector = new InspectorVM(_UOW.Inspectors.Find(i => i.ID == (int)inpsector_id).FirstOrDefault());
                }

            var inspectorLocation = getInspectorLocation(null);

            calculateSingleRoute(inspectorLocation, festivalLocation, null);
        }

        public void selectSingleInspector(object inspector_id)
        {
                var inspectorList = _UOW.Inspectors.GetAll().ToList().Select(i => new InspectorVM(i)).Where(i => i.Inspector_ID == (int)inspector_id);
                SingleInspector = new ObservableCollection<InspectorVM>(inspectorList);
                calculateSingleRoute(getInspectorLocation(inspectorList.First()), getFestivalLocation(SelectedFestival), SingleInspector.First());

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

            var r = Inspectors.OrderByDescending(ins => ins.TravelDistance);
            Inspectors = new ObservableCollection<InspectorVM>(r);

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

                if (festival != null)
                {
                    festivalNAW = _UOW.InspectionLocations.Find(l => l.ID == festival.Location_ID).FirstOrDefault();
                }
                else
                {
                    festivalNAW = _UOW.InspectionLocations.Find(l => l.ID == _selectedFestival.Location_ID).FirstOrDefault();
                }

                return geocoder.Geocode(festivalNAW.Straatnaam + " " + festivalNAW.Huisnummer, "", "", festivalNAW.Postcode, "Netherlands").First();
        }

        private Geocoding.Address getInspectorLocation(InspectorVM inspector)
        {
            NAW_inspecteur inspectorNAW;

                if(inspector != null)
                {
                    inspectorNAW = _UOW.NAWInspectors.Find(n => n.ID == inspector.InspectorForeignNAWID).FirstOrDefault();
                }
                else
                {
                    inspectorNAW = _UOW.NAWInspectors.Find(n => n.ID == _selectedInspector.InspectorForeignNAWID).FirstOrDefault();
                }
                
                return geocoder.Geocode(inspectorNAW.Straatnaam + " " + inspectorNAW.Huisnummer, "", "", inspectorNAW.Postcode, "Netherlands").First();
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

        private void switchVisibility()
        {
            var temp = EditVisibility;
            EditVisibility = MapVisibility;
            MapVisibility = temp;
            LeftoverCertificates = new ObservableCollection<CertificateVM>(_UOW.Inspections.GetMissingCertificates(_selectedFestival.Inspection_ID).Select(cert => new CertificateVM(cert)));
            RaisePropertyChanged(() => LeftoverCertificates);
        }

        private void showDetailsFestival()
        {
            //if (EditVisibility.Equals("Hidden"))
            //{
                switchVisibility();
            
            //}

        }
        private void LoadFestivals()
        {
            Festivals = new ObservableCollection<InspectionVM>(_UOW.Inspections.GetAll().Select(ins => new InspectionVM(ins)));
            Locations = new ObservableCollection<LocationVM>(_UOW.InspectionLocations.GetAll().Select(l => new LocationVM(l)));
            Customers = new ObservableCollection<CustomerVM>(_UOW.Customers.GetAll().Select(c => new CustomerVM(c)));
            RaisePropertyChanged(() => Festivals);
        }

        private void LoadOfflineFestival()
        {
            Festivals = new ObservableCollection<InspectionVM>();
            string json = File.ReadAllText(@"../../inspection.json");
            var test = JsonConvert.DeserializeObject<Inspectie>((json));
            Festivals.Add(new InspectionVM(test));
            RaisePropertyChanged(() => Festivals);
        }

        private void LoadInspectors()
        {
            Inspectors = new ObservableCollection<InspectorVM>(_UOW.Inspectors.GetAll().Select(ins => new InspectorVM(ins)));
            RaisePropertyChanged(() => Inspectors);
            //if(ViewSource != null)
            //{
            //    ViewSource.View.Refresh();
            //}
            
        }

        private void complete()
        {
            _UOW.Inspections.Get(SelectedFestival.Inspection_ID).Certificaat.Clear();
            foreach (var item in SelectedFestival.ChosenCertificates)
            {
                _UOW.Inspections.Get(SelectedFestival.Inspection_ID).Certificaat.Add(item.Certificate);
            }

            try
            {
                _UOW.Complete();
                MessageBox.Show("De aanpassingen zijn doorgevoerd", "Het is gelukt!",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                switchVisibility();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                 MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void OpenCreateWindow()
        {
            NewInspection = new InspectionVM();
            //Managers = new ObservableCollection<EmployeeVM>(_UOW.Employee.GetAllManagers().Select(emp => new EmployeeVM(emp)));
            Locations = new ObservableCollection<LocationVM>(_UOW.InspectionLocations.GetAll().Select(loc => new LocationVM(loc)));
            Customers = new ObservableCollection<CustomerVM>(_UOW.Customers.GetAll().ToList().Select(cus => new CustomerVM(cus)));
            AvailableCertificates = new ObservableCollection<CertificateVM>(_UOW.Certificates.GetAll().Select(cert => new CertificateVM(cert)));
            _createWindow = new CreateInspectionWindow();
            _createWindow.Show();
        }

        private void CloseCreate()
        {
            NewInspection = null;
            NewLocation = null;
            _createWindow.Close();
        }
        private void AddNewInspection()
        {
            NewLocation = null;
            _UOW.Inspections.Add(NewInspection.Inspection);

            foreach (var item in NewInspection.ChosenCertificates)
            {
                _UOW.Inspections.Get(NewInspection.Inspection_ID).Certificaat.Add(item.Certificate);
            }

            try
            {
                _UOW.Complete();
                _createWindow.Close();
                LoadFestivals();

            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void AddNewLocation()
        {
            _UOW.InspectionLocations.Add(NewLocation.Locatie);

            try
            {
                _UOW.Complete();
                _createLocation.Close();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Locations = new ObservableCollection<LocationVM>(_UOW.InspectionLocations.GetAll().Select(loc => new LocationVM(loc)));
            RaisePropertyChanged(() => Locations);
        }

        private void OpenCreateLocationWindow()
        {
            NewLocation = new LocationVM();
            _createLocation = new CreateLocationWindow();
            _createLocation.Show();
        }

    }
}