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
    
    public partial class Tabelvraag_vragenlijst
    {
        public int Vragenlijst_ID { get; set; }
        public int Tabelvraag_ID { get; set; }
        public int Positie { get; set; }
    
        public virtual Tabelvraag Tabelvraag { get; set; }
        public virtual Vragenlijst Vragenlijst { get; set; }
    }
}
