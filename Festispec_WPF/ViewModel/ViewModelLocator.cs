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
using Festispec_WPF.ViewModel.QuestionnaireFolder;
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
        public static UnitOfWork UOW;
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
            UOW = new UnitOfWork();
            SimpleIoc.Default.Register<MenuVM>();
            SimpleIoc.Default.Register<EmployeeCrudVM>();
            SimpleIoc.Default.Register<InspectorCrudVM>();
            SimpleIoc.Default.Register<RapportageVM>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<CreateQuestionaireVM>();
            SimpleIoc.Default.Register<EditQuestionnaireVM>();
            SimpleIoc.Default.Register<CRCustomerVM>();
            SimpleIoc.Default.Register<InspectionCrudVM>();
            SimpleIoc.Default.Register<MapViewModel>();
            SimpleIoc.Default.Register<HomeVM>();
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

        public QuestionaireCrudVM QuestionaireCrud
        {
            get
            {
                return new QuestionaireCrudVM();
            }
        }

        public InspectorCrudVM InspectorCrud
        {
            get
            {
                if(ServiceLocator.Current.GetInstance<InspectorCrudVM>() != null)
                {
                   ServiceLocator.Current.GetInstance<InspectorCrudVM>().Init();
                }
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

        public AppendixQuestionPageVM AppendixQuestionPageVM
        {
            get
            {
                return new AppendixQuestionPageVM();
            }
        }
        
        public EmployeeVM GetRegisterVm
        {
            get
            {
                return new EmployeeVM(EmployeeCrud);
            }
        }

        public MenuVM Menu
        {
            get
            {
                return new MenuVM();
            }
        }
        



    public CreateQuestionaireVM CreateQuestionnaire
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreateQuestionaireVM>();
            }
        }
       

        public InspectionCrudVM InspectionCrud
        {
            get
            {
                if(ServiceLocator.Current.GetInstance<InspectionCrudVM>() != null){
                    ServiceLocator.Current.GetInstance<InspectionCrudVM>().Init();
                }
                return ServiceLocator.Current.GetInstance<InspectionCrudVM>();
            }
        }

        public MapViewModel MapView
        {
            get
            {
                if(ServiceLocator.Current.GetInstance<MapViewModel>() != null)
                {
                    ServiceLocator.Current.GetInstance<MapViewModel>().Init();
                }
                return ServiceLocator.Current.GetInstance<MapViewModel>();
            }
            
        }

        public OpenQuestionPageVM OpenQuestionPageVM
        {
            get
            {
                return new OpenQuestionPageVM();
            }

        }
        public MultipleChoiceQuestionPageVM MultipleChoiceQuestionPageVM
        {
            get
            {
                return new MultipleChoiceQuestionPageVM();
            }

        }

        public MapQuestionPageVM MapQuestionPageVM
        {
            get
            {
                return new MapQuestionPageVM();
            }
        }
        public TableQuestionPageVM TableQuestionPageVM
        {
            get
            {
                return new TableQuestionPageVM();
            }
        }
        public QuestionnaireVM QuestionnaireVM
        {
            get
            {
                return new QuestionnaireVM();
            }
        }
        
        public EditQuestionnaireVM EditQuestionnaireVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<EditQuestionnaireVM>();
            }
        }
        public OfflineMapVM OfflineMapView
        {
            get
            {
                return new OfflineMapVM();
            }
        }


        public HomeVM Home
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomeVM>();
            }
        }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}