//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployerPartners.EDMX
{
    using System;
    using System.Collections.Generic;
    
    public partial class SOP_Conference_Questions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SOP_Conference_Questions()
        {
            this.SOP_Conference_Questions_Files = new HashSet<SOP_Conference_Questions_Files>();
        }
    
        public int Id { get; set; }
        public string Theme { get; set; }
        public string Author { get; set; }
        public Nullable<System.DateTime> Timestamp { get; set; }
        public Nullable<int> SOPConferenceId { get; set; }
    
        public virtual SOP_Conference SOP_Conference { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOP_Conference_Questions_Files> SOP_Conference_Questions_Files { get; set; }
    }
}
