using Festispec_WPF.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec_WPF.ViewModel
{
    public class HomeScreenVM
    {
        // Moeten nog instanties van schermen gemaakt worden 
        /*
        private KlantWindow _klantWindow;
        private VragenlijstWindow _vragenlijstWindow;
        */

        private InspectorCrudWindow _inspecteursWindow;
        private WeekplanningView _weekPlanningWindow;
        private VerzoekInplanView _verzoekInplanWindow;

        // commands
        public ICommand ShowKlantenCommand { get; set; }
        public ICommand ShowInplanVerzoekenCommand { get; set; }
        public ICommand ShowInspecteursCommand { get; set; }
        public ICommand ShowVragenlijstCommand { get; set; }
        public ICommand ShowKalenderCommand { get; set; }

        public HomeScreenVM()
        {
            ShowKlantenCommand = new RelayCommand(ShowKlanten);
            ShowInplanVerzoekenCommand = new RelayCommand(ShowInplanVerzoeken);
            ShowInspecteursCommand = new RelayCommand(ShowInspecteurs);
            ShowVragenlijstCommand = new RelayCommand(ShowVragenlijst);
            ShowKalenderCommand = new RelayCommand(ShowKalender);
        }

        public void ShowKlanten()
        {
            throw new NotImplementedException();
        }
        public void ShowInplanVerzoeken()
        {
            _verzoekInplanWindow = new VerzoekInplanView();
            _verzoekInplanWindow.Show();
        }
        public void ShowInspecteurs()
        {
            _inspecteursWindow = new InspectorCrudWindow();
            _inspecteursWindow.Show();
        }
        public void ShowVragenlijst()
        {
            throw new NotImplementedException();
        }
        public void ShowKalender()
        {
            _weekPlanningWindow = new WeekplanningView();
            _weekPlanningWindow.Show();
        }
    }
}
