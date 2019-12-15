using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using Festispec_WPF.Model;
using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using FestiSpec.Domain.Model;
namespace Festispec_WPF.ViewModel
{
    public class LoginRegisterVM : ViewModelBase
    {
        //properties
        public string Username { get; set; }
        public string Password { get; set; }

        //reference commands
        public ICommand LoginCommand { get; set; }

        //local variables
        private IUnitOfWork UOW;

        public LoginRegisterVM()
        {
            LoginCommand = new RelayCommand(HandleLogin);
            UOW = ViewModelLocator.UOW;
        }

        private void HandleLogin()
        {
            //Window was corrupt
            var targetPerson = UOW.Employee.GetAll()
                .FirstOrDefault(e => e.Wachtwoord == Password && e.Username == Username);

            if (targetPerson == null)
            {
                Console.WriteLine("failed to login");
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                //TODO exit main window
                Console.WriteLine("login ok");
                Username = "";
                Password = "";
                RaisePropertyChanged(() => Username);
                RaisePropertyChanged(() => Password);
                HomeScreenView home = new HomeScreenView();
                home.Show();
            }
        }
    }
}
