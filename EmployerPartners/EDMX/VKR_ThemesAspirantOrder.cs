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
    
    public partial class VKR_ThemesAspirantOrder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VKR_ThemesAspirantOrder()
        {
            this.VKR_ThemesAspirant_NR_NPR = new HashSet<VKR_ThemesAspirant_NR_NPR>();
            this.VKR_ThemesAspirant_NR_PartnerPerson = new HashSet<VKR_ThemesAspirant_NR_PartnerPerson>();
        }
    
        public int Id { get; set; }
        public string GraduateYear { get; set; }
        public Nullable<int> StudDataId { get; set; }
        public string FIO { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string Course { get; set; }
        public Nullable<int> ObrazProgramId { get; set; }
        public Nullable<int> LicenseProgramId { get; set; }
        public Nullable<int> FacultyId { get; set; }
        public string ObrazProgramCrypt { get; set; }
        public Nullable<System.Guid> ObrazProgramInYearId { get; set; }
        public string VKRName { get; set; }
        public string VKRNameEng { get; set; }
        public string AuthorUpdated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public string AuthorFreese { get; set; }
        public Nullable<System.DateTime> DateFreeze { get; set; }
        public string Author { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string Account { get; set; }
        public string DegreeName { get; set; }
        public string StudyForm { get; set; }
        public string StudyBasis { get; set; }
        public string StatusName { get; set; }
        public string MailBox { get; set; }
        public string WorkPlan { get; set; }
        public string RegNomWP { get; set; }
        public string FIOEng { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VKR_ThemesAspirant_NR_NPR> VKR_ThemesAspirant_NR_NPR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VKR_ThemesAspirant_NR_PartnerPerson> VKR_ThemesAspirant_NR_PartnerPerson { get; set; }
    }
}