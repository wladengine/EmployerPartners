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
    
    public partial class VKRCoordinator
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VKRCoordinator()
        {
            this.VKROP = new HashSet<VKROP>();
            this.VKROPTheme = new HashSet<VKROPTheme>();
        }
    
        public int Id { get; set; }
        public int VKRId { get; set; }
        public int FacultyId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobiles { get; set; }
        public string Comment { get; set; }
        public string Author { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    
        public virtual Faculty Faculty { get; set; }
        public virtual VKR VKR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VKROP> VKROP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VKROPTheme> VKROPTheme { get; set; }
    }
}
