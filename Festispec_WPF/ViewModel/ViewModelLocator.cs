/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Festispec_WPF"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using FestiSpec.Domain.Model;
using Festispec_WPF.Model;
using Festispec_WPF.Model.UnitOfWork;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System.Configuration;

namespace Festispec_WPF.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        /// 

        private UnitOfWork _unitOfWork;
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            SimpleIoc.Default.Register<EmployeeCrudVM>();
            SimpleIoc.Default.Register<InspectorCrudVM>();
            SimpleIoc.Default.Register<RapportageVM>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HomeScreenVM>();
            SimpleIoc.Default.Register<CRCustomerVM>();
            SimpleIoc.Default.Register<InspectionCrudVM>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public InspectorVM Inspector
        {
            get
            {
                return new InspectorVM();
            }
        }

        public InspectorCrudVM InspectorCrud
        {
            get
            {
                //if(ServiceLocator.Current.GetInstance<InspectorCrudVM>() == null)
                //{
                //    SimpleIoc.Default.Register<InspectorCrudVM>();
                //}
                return ServiceLocator.Current.GetInstance<InspectorCrudVM>();
            }
        }

        public RapportageVM Rapportage
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RapportageVM>();
            }
        }

        public EmployeeCrudVM EmployeeCrud
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EmployeeCrudVM>();
            }
        }


        public LoginRegisterVM GetLoginRegisterVm
        {
            get
            {
                return new LoginRegisterVM();
            }
        }
        public CRCustomerVM CRCustomer
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CRCustomerVM>();
            }
        }

        public EmployeeVM GetRegisterVm
        {
            get
            {
                return new EmployeeVM();
            }
        }

        public HomeScreenVM HomeScreen
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomeScreenVM>();
            }
        }

        public MenuVM GetMenu
        {
            get
            {
                return new MenuVM();
            }
        }
        
        public UnitOfWork UOW
        {
            get
            {
                if(_unitOfWork == null)
                {
                    //var connection = ConfigurationManager.AppSettings["UsedConnection"] ?? "FestiSpecEntities";
                    _unitOfWork = new UnitOfWork(new FestiSpecEntities());
                }
                return _unitOfWork;
            }
        }

        public InspectionCrudVM InspectionCrud
        {
            get
            {
                return ServiceLocator.Current.GetInstance<InspectionCrudVM>();
            }
        }

        public MapViewModel MapView
        {
            get
            {
                return new MapViewModel();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}