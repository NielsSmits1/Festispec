//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FestiSpec.Domain.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Meerkeuzevraag_vragenlijst
    {
        public int Vragenlijst_ID { get; set; }
        public int Meerkeuzevraag_ID { get; set; }
        public string Antwoord { get; set; }
        public int Positie { get; set; }
    
        public virtual Meerkeuzevraag Meerkeuzevraag { get; set; }
        public virtual Vragenlijst Vragenlijst { get; set; }
    }
}
