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
    
    public partial class VKRThemes
    {
        public int Id { get; set; }
        public int VKRId { get; set; }
        public int OrganizationId { get; set; }
        public Nullable<int> OrganizationAgreedId { get; set; }
        public string VKRName { get; set; }
        public string VKRNameEng { get; set; }
        public Nullable<int> LicenseProgramId { get; set; }
        public Nullable<int> ObrazProgramId { get; set; }
        public Nullable<int> FacultyId { get; set; }
        public string History { get; set; }
        public string Comment { get; set; }
        public string Author { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
    
        public virtual Faculty Faculty { get; set; }
        public virtual LicenseProgram LicenseProgram { get; set; }
        public virtual ObrazProgram ObrazProgram { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual Organization Organization1 { get; set; }
        public virtual VKR VKR { get; set; }
    }
}
