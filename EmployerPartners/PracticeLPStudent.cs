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
    
    public partial class PracticeLPStudent
    {
        public int Id { get; set; }
        public int PracticeLPId { get; set; }
        public Nullable<int> PracticeLPOrganizationId { get; set; }
        public string StudentFIO { get; set; }
        public string DR { get; set; }
        public string Course { get; set; }
        public string RegNomWP { get; set; }
        public string Department { get; set; }
        public string StudyBasis { get; set; }
        public string StatusName { get; set; }
        public string Accout { get; set; }
        public string DegreeName { get; set; }
        public string SpecNumber { get; set; }
        public string SpecName { get; set; }
        public string FacultyName { get; set; }
        public int LicenseProgramId { get; set; }
        public int FacultyId { get; set; }
        public string Comment { get; set; }
        public string Author { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string ObrazProgramCrypt { get; set; }
        public Nullable<int> StudDataId { get; set; }
    
        public virtual PracticeLP PracticeLP { get; set; }
        public virtual PracticeLPOrganization PracticeLPOrganization { get; set; }
    }
}
