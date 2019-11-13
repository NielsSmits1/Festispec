using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using Festispec_WPF.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec_WPF.ViewModel
{
    public class RegisterVM : ViewModelBase
    {
        public ICommand RegisterCommand { get; set; }
        private RegisterView windowRegisterView;

        public RegisterVM()
        {
            RegisterCommand = new RelayCommand(HandleRegister);

        }

        private void HandleRegister()
        {
            /* Information required: 
             * Werknemer_ID PK AA
             * Rol FK
             * EERST NAW AANMAKEN, DAARNA DE REST.
             * NAW FK
             *      Voornaam
             *      Tussenvoegsel nullable
             *      Achternaam
             *      Postcode
             *      Huisnummer
             *      Geboortedatum
             *      IBAN
             *      email
             * Username
             * Wachtwoord
             * Actief
             */
            windowRegisterView = new RegisterView();
            windowRegisterView.Show();
        }
    }
}
