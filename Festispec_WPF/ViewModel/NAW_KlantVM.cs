using Festispec_WPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FestiSpec.Domain.Model;
namespace Festispec_WPF.ViewModel
{
    public class NAW_KlantVM
    {
        private NAW_Klant _NAW_Klant;
        public string HouseNumber
        {
            get { return _NAW_Klant.Huisnummer; }
            set { _NAW_Klant.Huisnummer = value; }
        }
        public string ZipCode
        {
            get { return _NAW_Klant.Postcode; }
            set { _NAW_Klant.Postcode = value; }
        }
        public string KVKNumber
        {
            get { return _NAW_Klant.KvkNummer; }
            set { _NAW_Klant.KvkNummer = value; }
        }

        public string IBAN                                              
        {
            get { return _NAW_Klant.IBAN; }
            set { _NAW_Klant.IBAN = value; }
        }

        public string LocationNumber
        {
            get { return _NAW_Klant.Vestigingsnummer; }
            set
            {
                //if (value.Length == 12)
                //{
                    _NAW_Klant.Vestigingsnummer = value;
                //}
            }               
        }

        public string StreetName
        {
            get { return _NAW_Klant.Straatnaam; }
            set { _NAW_Klant.Straatnaam = value; }
        }

        public NAW_KlantVM(NAW_Klant nAW_Klant)
        {
            _NAW_Klant = nAW_Klant;
        }
        public NAW_KlantVM()
        {
            _NAW_Klant = new NAW_Klant();
        }


        public NAW_Klant NawData
        {
            get { return _NAW_Klant; }
            set { _NAW_Klant = value; }
        }

        public NAW_Klant toModel()
        {
            return _NAW_Klant;
        }
    }
}
