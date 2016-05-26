//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployerPartners
{
    using System;
    using System.Collections.Generic;
    
    public partial class StudyLevel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public StudyLevel()
        {
            this.LicenseProgram = new HashSet<LicenseProgram>();
            this.ProgramMode = new HashSet<ProgramMode>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameEng { get; set; }
        public string Acronym { get; set; }
        public string Crypt { get; set; }
        public string FurtherStudy { get; set; }
        public string FurtherStudyEng { get; set; }
        public string LevelGroup { get; set; }
        public Nullable<int> StudyLevelGroupId { get; set; }
        public Nullable<int> EducationPermitId { get; set; }
        public Nullable<int> EducationDocumentId { get; set; }
        public bool HasTwoLicenseProgram { get; set; }
        public Nullable<int> StudyPlanPrintFormTypeId { get; set; }
        public Nullable<int> StudyPlanIndividualPrintFormTypeId { get; set; }
        public bool IsOpen { get; set; }
        public string Holder { get; set; }
        public bool IsUseEng { get; set; }
        public bool IsUseForPrint { get; set; }
        public string Author { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        public virtual EducationDocument EducationDocument { get; set; }
        public virtual EducationPermit EducationPermit { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LicenseProgram> LicenseProgram { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProgramMode> ProgramMode { get; set; }
        public virtual StudyLevelGroup StudyLevelGroup { get; set; }
        public virtual StudyPlanPrintFormType StudyPlanPrintFormType { get; set; }
        public virtual StudyPlanPrintFormType StudyPlanPrintFormType1 { get; set; }
    }
}
