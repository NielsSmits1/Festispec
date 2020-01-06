using System;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows;
using Application = System.Windows.Application;
using FestiSpec.Domain.Model;
using System.Windows;
using Application = System.Windows.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Net;

namespace Festispec_WPF.ViewModel
{
    public class LoginRegisterVM : ViewModelBase
    {
        //properties
        public string Username { get; set; }
        public string Password { get; set; }
        public string ErrorVisibility { get; set; }
        public string ErrorText { get; set; }

        //reference commands
        public ICommand LoginCommand { get; set; }
        public ICommand OfflineCommand { get; set; }

        //local variables
        private IUnitOfWork UOW;
        private OfflineMapView _offlineMapView;

        public LoginRegisterVM()
        {
            HasInternet();
            LoginCommand = new RelayCommand(HandleLogin);
            UOW = ViewModelLocator.UOW;
            OfflineCommand = new RelayCommand(HandleOffline);
        }

        private void HandleLogin()
        {
            if (Password != null && Username != null)
            {
                // Window was corrupt
                // var targetPerson = UOW.Employee.GetAll()
                //     .FirstOrDefault(e => e.Wachtwoord == Password && e.Username == Username);

                // if (targetPerson == null)
                // {
                //     Console.WriteLine("failed to login");
                //     System.Windows.Forms.MessageBox.Show("Incorrecte login gegevens", "Fout bij invoeren velden",
                //       MessageBoxButtons.OK, MessageBoxIcon.Error);
                // }
                // else
                // {
                //     Console.WriteLine("login ok");
                //     Username = "";
                //     Password = "";
                //     RaisePropertyChanged(() => Username);
                //     RaisePropertyChanged(() => Password);
                //     var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                //     HomeScreenView home = new HomeScreenView();
                //     home.Show();
                //     currentWindow.Close();
                // }
            if (HasInternet())
            {
                //Window was corrupt
                var targetPerson = UOW.Employee.GetAll()
                    .FirstOrDefault(e => e.Wachtwoord == Password && e.Username == Username);

                if (targetPerson == null)
                {
                    var username = UOW.Employee.FindUsername(Username);
                    if(username == null)
                    {
                        ErrorText = "Verkeerde gebruikersnaam";
                    }
                    else
                    {
                        ErrorText = "Verkeerd wachtwoord";
                    }

                    RaisePropertyChanged("ErrorText");
                }
                else
                {
                    var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                    Console.WriteLine("login ok");
                    Username = "";
                    Password = "";
                    RaisePropertyChanged(() => Username);
                    RaisePropertyChanged(() => Password);
                    HomeScreenView home = new HomeScreenView();
                    home.Show();
                    currentWindow.Close();
                }
            }
        }

        private void HandleOffline()
        {
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            _offlineMapView = new OfflineMapView();
            _offlineMapView.Show();
            currentWindow.Close();
        }

        private bool HasInternet()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                {
                    ErrorVisibility = "Hidden";

                    return true;
                }
            }
            catch
            {
                ErrorVisibility = "Visible";

                return false;
            }
        }
    }
}
