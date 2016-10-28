//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployerPartners
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrganizationDogovor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrganizationDogovor()
        {
            this.PracticeLPOrganization = new HashSet<PracticeLPOrganization>();
            this.OrganizationDogovorFile = new HashSet<OrganizationDogovorFile>();
            this.PracticeLPStudent = new HashSet<PracticeLPStudent>();
        }
    
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int RubricId { get; set; }
        public int DocumentTypeId { get; set; }
        public string Document { get; set; }
        public Nullable<System.DateTime> DocumentStart { get; set; }
        public Nullable<System.DateTime> DocumentFinish { get; set; }
        public Nullable<bool> Permanent { get; set; }
        public string DocumentNumber { get; set; }
        public Nullable<System.DateTime> DocumentDate { get; set; }
        public string Comment { get; set; }
        public string Author { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string PartTime { get; set; }
        public Nullable<bool> IsActual { get; set; }
        public Nullable<bool> FromDocumentDate { get; set; }
    
        public virtual DocumentType DocumentType { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual Rubric Rubric { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PracticeLPOrganization> PracticeLPOrganization { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationDogovorFile> OrganizationDogovorFile { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PracticeLPStudent> PracticeLPStudent { get; set; }
    }
}
