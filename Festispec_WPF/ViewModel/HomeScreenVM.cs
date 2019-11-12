using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec_WPF.ViewModel
{
    class HomeScreenVM
    {
        // Moeten nog instanties van schermen gemaakt worden 
        /*
        private KlantWindow _klantWindow;
        private VerzoekeninplanWindow _verzoekenInplanWindow;
        private InspectuersWindow _inspecteursWindow;
        private VragenlijstWindow _vragenlijstWindow;
        private KlalenderWindow _kalenderWindow;
        */

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
            throw new NotImplementedException();
        }
        public void ShowInspecteurs()
        {
            throw new NotImplementedException();
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
