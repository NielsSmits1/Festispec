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
    
    public partial class Bijlagevraag
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bijlagevraag()
        {
            this.Bijlagevraag_vragenlijst = new HashSet<Bijlagevraag_vragenlijst>();
        }
    
        public int ID { get; set; }
        public string Vraag { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bijlagevraag_vragenlijst> Bijlagevraag_vragenlijst { get; set; }
    }
}
