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
    
    public partial class GAK_MemberPP_log
    {
        public int Id { get; set; }
        public int GAK_Number_logId { get; set; }
        public int GAK_MemberPPId { get; set; }
        public int GAK_NumberId { get; set; }
        public Nullable<int> PartnerPersonId { get; set; }
        public string PP_FIO { get; set; }
        public string PP_Position { get; set; }
        public string PP_Degree { get; set; }
        public string PP_Rank { get; set; }
        public string PP_Account { get; set; }
        public string PP_Subdivision { get; set; }
        public string PP_Organization { get; set; }
        public string PP_OrgPosition { get; set; }
        public string Comment { get; set; }
        public string AuthorUpdated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public string Author { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public string AuthorLog { get; set; }
        public Nullable<System.DateTime> DateCreatedLog { get; set; }
        public Nullable<bool> IsAbsent { get; set; }
    
        public virtual GAK_Number_log GAK_Number_log { get; set; }
        public virtual PartnerPerson PartnerPerson { get; set; }
    }
}
