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
    
    public partial class StudyPlanData
    {
        public int Id { get; set; }
        public string StudyPlanNumber { get; set; }
        public string PlanYear { get; set; }
        public Nullable<int> ObrazProgramId { get; set; }
        public Nullable<int> LicenseProgramId { get; set; }
        public Nullable<int> FacultyId { get; set; }
        public string ObrazProgramCrypt { get; set; }
        public Nullable<System.Guid> ObrazProgramInYearId { get; set; }
    }
}
