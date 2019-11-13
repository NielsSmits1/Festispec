using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec_WPF.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec_WPF.ViewModel
{
    public class LoginRegisterVM : ViewModelBase
    {
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginRegisterVM()
        {
            LoginCommand = new RelayCommand(HandleLogin);
            RegisterCommand = new RelayCommand(HandleRegister);
        }

        private void HandleLogin()
        {
            using (var context = new FestiSpecEntities())
            {
                var targetPerson = (from person in context.Werknemer.ToList()
                                    where person.Username == Username && person.Wachtwoord == Password
                                    select person).ToList();

                Console.WriteLine(targetPerson.Count == 0 ? "Invalid login" : "Valid login");
            }
        }

        private void HandleRegister()
        {
            throw new Exception("Unimplemeted");
        }
    }
}
