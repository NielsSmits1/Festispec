using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec_WPF.ViewModel
{
    public class LoginRegisterVM : ViewModelBase
    {
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }


        public LoginRegisterVM()
        {
            LoginCommand = new RelayCommand(HandleLogin);
            RegisterCommand = new RelayCommand(HandleRegister);
        }

        private void HandleLogin()
        {
            throw new Exception("Unimplemeted");
        }

        private void HandleRegister()
        {
            throw new Exception("Unimplemeted");
        }
    }
}
