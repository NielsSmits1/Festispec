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
    public class MenuVM
    {
        // Moeten nog instanties van schermen gemaakt worden 
        /*
        private KlantWindow _klantWindow;
        private VerzoekeninplanWindow _verzoekenInplanWindow;
        private VragenlijstWindow _vragenlijstWindow;
        private KlalenderWindow _kalenderWindow;
        */

        private InspectorCrudWindow _inspecteursWindow;
        private HomeScreenView _homeScreenWindow;

        // commands
        public ICommand ShowHomeCommand { get; set; }
        public ICommand ShowKlantenCommand { get; set; }
        public ICommand ShowInplanVerzoekenCommand { get; set; }
        public ICommand ShowInspecteursCommand { get; set; }
        public ICommand ShowVragenlijstCommand { get; set; }
        public ICommand ShowKalenderCommand { get; set; }

        public MenuVM()
        {
            ShowHomeCommand = new RelayCommand(ShowHome);
            ShowKlantenCommand = new RelayCommand(ShowKlanten);
            ShowInplanVerzoekenCommand = new RelayCommand(ShowInplanVerzoeken);
            ShowInspecteursCommand = new RelayCommand(ShowInspecteurs);
            ShowVragenlijstCommand = new RelayCommand(ShowVragenlijst);
            ShowKalenderCommand = new RelayCommand(ShowKalender);
        }

        public void ShowHome()
        {
            _homeScreenWindow = new HomeScreenView();
            _homeScreenWindow.Show();
        }
        public void ShowKlanten()
        {
            throw new NotImplementedException();
        }
        public void ShowInplanVerzoeken()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
