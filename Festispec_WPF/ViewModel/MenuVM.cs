using Festispec_WPF.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FestiSpec.Domain.Model;
using System.Net;

namespace Festispec_WPF.ViewModel
{
    public class MenuVM
    {
        private HomeScreenView _homeScreenWindow;
        private CustomerCrudWindow _customerCrudWindow;
        private InspectorCrudWindow _inspecteursWindow;
        private WeekplanningView _weekPlanningWindow;
        private QuestionnaireCRUD _questionnaireView;
        private InspectionOverview _overview;
        public string ErrorVisibility { get; set; }
        private MapView _planWindow;
        private EmployeeView _employeeView;
        private MainWindow _mainWindow;


        // commands
        public ICommand ShowHomeCommand { get; set; }
        public ICommand ShowKlantenCommand { get; set; }
        public ICommand ShowInplanVerzoekenCommand { get; set; }
        public ICommand ShowInspecteursCommand { get; set; }
        public ICommand ShowVragenlijstCommand { get; set; }
        public ICommand ShowKalenderCommand { get; set; }
        public ICommand ShowWerknemerCommand { get; set; }
        public ICommand LogOutCommand { get; set; }

        public ICommand ShowInspectionCommand { get; set; }

        public MenuVM()
        {
            ShowHomeCommand = new RelayCommand(ShowHome);
            ShowKlantenCommand = new RelayCommand(ShowKlanten);
            ShowInplanVerzoekenCommand = new RelayCommand(ShowInplanVerzoeken);
            ShowInspecteursCommand = new RelayCommand(ShowInspecteurs);
            ShowVragenlijstCommand = new RelayCommand(ShowVragenlijst);
            ShowKalenderCommand = new RelayCommand(ShowKalender);
            ShowInspectionCommand = new RelayCommand(InspectionList);

            HasInternet();
            ShowWerknemerCommand = new RelayCommand(ShowWerknemer);
            LogOutCommand = new RelayCommand(LogOut);
        }

        public void ShowHome()
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            _homeScreenWindow = new HomeScreenView();
            _homeScreenWindow.Show();
            currentWindow.Close();
        }
        public void ShowKlanten()
        {
            if (HasInternet())
            {
                var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                _customerCrudWindow = new CustomerCrudWindow();
                _customerCrudWindow.Show();
                currentWindow.Close();
            }
            else
            {
                LogOut();
            }
        }
        public void ShowInplanVerzoeken()
        {
            if (HasInternet())
            {
                var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                _planWindow = new MapView();
                _planWindow.Show();
                currentWindow.Close();
            }
            else
            {
                LogOut();
            }
        }
        public void ShowInspecteurs()
        {
            if (HasInternet())
            {
                var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                _inspecteursWindow = new InspectorCrudWindow();
                _inspecteursWindow.Show();
                currentWindow.Close();
            }
            else
            {
                LogOut();
            }
        }
        public void ShowVragenlijst()
        {
            if (HasInternet())
            {
                var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            _questionnaireView = new QuestionnaireCRUD();
            _questionnaireView.Show();
            currentWindow.Close();
            }
            else
            {
                LogOut();
            }
        }
        public void ShowKalender()
        {
            if (HasInternet())
            {
                var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                _weekPlanningWindow = new WeekplanningView();
                _weekPlanningWindow.Show();
                currentWindow.Close();
            }
            else
            {
                LogOut();
            }
        }
        public void ShowWerknemer()
        { 
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);

            if (HasInternet())
            {
                _employeeView = new EmployeeView();
                _employeeView.Show();
            }
            else
            {
                LogOut();
            }

            currentWindow.Close();
        }

        public void LogOut()
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            _mainWindow = new MainWindow();
            _mainWindow.Show();
            currentWindow.Close();
            for (int intCounter = App.Current.Windows.Count - 1; intCounter > 0; intCounter--)
                App.Current.Windows[intCounter].Close();

            _mainWindow = new MainWindow();
            _mainWindow.Show();
        }

        public void InspectionList()
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            if (HasInternet())
            {
                _overview = new InspectionOverview();
                _overview.Show();
            }
            else
            {
                LogOut();
            }
            currentWindow.Close();
        }

        private bool HasInternet()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204")) 
                    ErrorVisibility = "Hidden";

                return true;
            }
            catch
            {
                ErrorVisibility = "Visible";             
                return false;
            }
        }
    }
}
