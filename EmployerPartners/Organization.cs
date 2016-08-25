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
    
    public partial class Organization
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Organization()
        {
            this.InboxMessage = new HashSet<InboxMessage>();
            this.OrganizationActivityArea = new HashSet<OrganizationActivityArea>();
            this.OrganizationContacts = new HashSet<OrganizationContacts>();
            this.OrganizationFaculty = new HashSet<OrganizationFaculty>();
            this.OrganizationLP = new HashSet<OrganizationLP>();
            this.OrganizationOkved = new HashSet<OrganizationOkved>();
            this.OrganizationPerson = new HashSet<OrganizationPerson>();
            this.OrganizationRubric = new HashSet<OrganizationRubric>();
            this.PracticeLPOrganization = new HashSet<PracticeLPOrganization>();
            this.PracticeStudent = new HashSet<PracticeStudent>();
            this.OrganizationDogovor = new HashSet<OrganizationDogovor>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string ShortName { get; set; }
        public string NameEng { get; set; }
        public string ShortNameEng { get; set; }
        public string Source { get; set; }
        public string Description { get; set; }
        public Nullable<bool> SPbGU { get; set; }
        public string Okved { get; set; }
        public Nullable<int> OwnershipTypeId { get; set; }
        public Nullable<int> ActivityGoalId { get; set; }
        public Nullable<int> NationalAffiliationId { get; set; }
        public Nullable<int> ActivityAreaId { get; set; }
        public string Contact { get; set; }
        public string INN { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobiles { get; set; }
        public string Fax { get; set; }
        public string WebSite { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> RegionId { get; set; }
        public string CodeKLADR { get; set; }
        public string Code { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Comment { get; set; }
        public string Author { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.Guid rowguid { get; set; }
        public string Apartment { get; set; }
    
        public virtual ActivityArea ActivityArea { get; set; }
        public virtual ActivityGoal ActivityGoal { get; set; }
        public virtual Country Country { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InboxMessage> InboxMessage { get; set; }
        public virtual NationalAffiliation NationalAffiliation { get; set; }
        public virtual OwnershipType OwnershipType { get; set; }
        public virtual Region Region { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationActivityArea> OrganizationActivityArea { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationContacts> OrganizationContacts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationFaculty> OrganizationFaculty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationLP> OrganizationLP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationOkved> OrganizationOkved { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationPerson> OrganizationPerson { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationRubric> OrganizationRubric { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PracticeLPOrganization> PracticeLPOrganization { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PracticeStudent> PracticeStudent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationDogovor> OrganizationDogovor { get; set; }
    }
}
