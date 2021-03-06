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
    
    public partial class GAK_Member
    {
        public int Id { get; set; }
        public int GAK_NumberId { get; set; }
        public string NPR_Persnum { get; set; }
        public string NPR_Tabnum { get; set; }
        public string NPR_FIO { get; set; }
        public string NPR_Position { get; set; }
        public string NPR_Degree { get; set; }
        public string NPR_Rank { get; set; }
        public string NPR_Account { get; set; }
        public string NPR_Chair { get; set; }
        public string NPR_Faculty { get; set; }
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
    
        public virtual GAK_Number GAK_Number { get; set; }
        public virtual PartnerPerson PartnerPerson { get; set; }
    }
}
