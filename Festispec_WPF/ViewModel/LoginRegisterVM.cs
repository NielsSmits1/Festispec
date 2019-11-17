using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec_WPF.Model;
using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec_WPF.ViewModel
{
    public class LoginRegisterVM : ViewModelBase
    {
        //properties
        public string Username { get; set; }
        public string Password { get; set; }

        //reference commands
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        //local variables
        private RegisterView _windowRegisterView;
        private IUnitOfWork UOW;

        public LoginRegisterVM()
        {
            LoginCommand = new RelayCommand(HandleLogin);
            RegisterCommand = new RelayCommand(OpenRegisterWindow);
            UOW = new ViewModelLocator().UOW;
        }

        private void HandleLogin()
        {
            var targetPerson = UOW.Employee.GetAll()
                .FirstOrDefault(e => e.Wachtwoord == Password && e.Username == Username);

            if (targetPerson == null)
            {
                Console.WriteLine("failed to login");
                FailedLoginView failedLoginView = new FailedLoginView();
                failedLoginView.Show();
            }
            else
            {
                //TODO exit main window
                Console.WriteLine("login ok");
                MenuView menuView = new MenuView();
                menuView.Show();

            }

        }

        private void OpenRegisterWindow()
        {
            _windowRegisterView = new RegisterView();
            _windowRegisterView.Show();
        }
    }
}
