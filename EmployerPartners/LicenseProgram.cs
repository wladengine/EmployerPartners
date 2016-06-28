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
    
    public partial class LicenseProgram
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LicenseProgram()
        {
            this.ObrazProgram = new HashSet<ObrazProgram>();
            this.OrganizationLP = new HashSet<OrganizationLP>();
            this.PartnerPersonLP = new HashSet<PartnerPersonLP>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string Acronym { get; set; }
        public string Code { get; set; }
        public int LicenseId { get; set; }
        public int StudyLevelId { get; set; }
        public int ProgramTypeId { get; set; }
        public string ApplicationNum { get; set; }
        public string PositionNum { get; set; }
        public string PartNum { get; set; }
        public string NormativePeriod { get; set; }
        public string NormativePeriodEng { get; set; }
        public Nullable<int> QualificationId { get; set; }
        public int AggregateGroupId { get; set; }
        public bool IsOpen { get; set; }
        public string Holder { get; set; }
        public string Author { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        public virtual License License { get; set; }
        public virtual ProgramType ProgramType { get; set; }
        public virtual Qualification Qualification { get; set; }
        public virtual StudyLevel StudyLevel { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ObrazProgram> ObrazProgram { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationLP> OrganizationLP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PartnerPersonLP> PartnerPersonLP { get; set; }
    }
}
