using System;
using System.Collections.Generic;
using System.Text;

namespace PratikKargo.Model
{
    public class Cargo
    {
        public int KargoId { get; set; }
        public string X { get; set; }
        public string Y { get; set; }

        public string Adress { get; set; }
        public string NameSurname { get; set; }
        public string PhoneNumber { get; set; }


    }
    public class CargoDistance
    {
        public int KargoId { get; set; }
        public string Distance { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public string adress { get; set; }
        public string number { get; set; }
        public string namesurname { get; set; }


   


    }
}
