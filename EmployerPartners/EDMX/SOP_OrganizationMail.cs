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
    
    public partial class SOP_OrganizationMail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SOP_OrganizationMail()
        {
            this.SOP_OrganizationMail_Files = new HashSet<SOP_OrganizationMail_Files>();
        }
    
        public int Id { get; set; }
        public Nullable<int> SOPId { get; set; }
        public Nullable<int> OrganizationId { get; set; }
        public Nullable<int> PartnerPersonId { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string Theme { get; set; }
        public string Text { get; set; }
        public string Number { get; set; }
        public string Author { get; set; }
        public Nullable<System.DateTime> Timestamp { get; set; }
    
        public virtual SOP SOP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SOP_OrganizationMail_Files> SOP_OrganizationMail_Files { get; set; }
    }
}
