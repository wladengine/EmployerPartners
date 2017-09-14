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
    
    public partial class PracticeLPOrganization
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PracticeLPOrganization()
        {
            this.PracticeStudent = new HashSet<PracticeStudent>();
            this.PracticeLPStudent = new HashSet<PracticeLPStudent>();
        }
    
        public int Id { get; set; }
        public int PracticeLPId { get; set; }
        public int OrganizationId { get; set; }
        public Nullable<System.DateTime> DateStart { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationAddress { get; set; }
        public string Comment { get; set; }
        public string Author { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<int> OrganizationDogovorId { get; set; }
    
        public virtual Organization Organization { get; set; }
        public virtual PracticeLP PracticeLP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PracticeStudent> PracticeStudent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PracticeLPStudent> PracticeLPStudent { get; set; }
        public virtual OrganizationDogovor OrganizationDogovor { get; set; }
    }
}
