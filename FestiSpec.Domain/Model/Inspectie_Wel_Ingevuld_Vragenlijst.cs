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
    
    public partial class Inspectie_Wel_Ingevuld_Vragenlijst
    {
        public int Inspectienummer { get; set; }
        public int Vragenlijst_ID { get; set; }
        public int Inspecteur_ID { get; set; }
    
        public virtual Inspecteur Inspecteur { get; set; }
        public virtual Inspectie Inspectie { get; set; }
        public virtual Vragenlijst Vragenlijst { get; set; }
    }
}
