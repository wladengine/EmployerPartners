﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class EmployerPartnersEntities : DbContext
    {
        public EmployerPartnersEntities()
            : base("name=EmployerPartnersEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C_AppSettings> C_AppSettings { get; set; }
        public virtual DbSet<ActivityArea> ActivityArea { get; set; }
        public virtual DbSet<ActivityGoal> ActivityGoal { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Degree> Degree { get; set; }
        public virtual DbSet<dtproperties> dtproperties { get; set; }
        public virtual DbSet<EducationDocument> EducationDocument { get; set; }
        public virtual DbSet<EducationPermit> EducationPermit { get; set; }
        public virtual DbSet<Faculty> Faculty { get; set; }
        public virtual DbSet<InboxMessage> InboxMessage { get; set; }
        public virtual DbSet<InboxMessageFile> InboxMessageFile { get; set; }
        public virtual DbSet<License> License { get; set; }
        public virtual DbSet<LicenseProgram> LicenseProgram { get; set; }
        public virtual DbSet<MobilityType> MobilityType { get; set; }
        public virtual DbSet<NationalAffiliation> NationalAffiliation { get; set; }
        public virtual DbSet<ObrazProgram> ObrazProgram { get; set; }
        public virtual DbSet<OkvedType> OkvedType { get; set; }
        public virtual DbSet<Organization> Organization { get; set; }
        public virtual DbSet<OrganizationActivityArea> OrganizationActivityArea { get; set; }
        public virtual DbSet<OrganizationContacts> OrganizationContacts { get; set; }
        public virtual DbSet<OrganizationFaculty> OrganizationFaculty { get; set; }
        public virtual DbSet<OrganizationLP> OrganizationLP { get; set; }
        public virtual DbSet<OrganizationOkved> OrganizationOkved { get; set; }
        public virtual DbSet<OrganizationPerson> OrganizationPerson { get; set; }
        public virtual DbSet<OrganizationRubric> OrganizationRubric { get; set; }
        public virtual DbSet<OwnershipType> OwnershipType { get; set; }
        public virtual DbSet<PartnerPerson> PartnerPerson { get; set; }
        public virtual DbSet<ProgramMode> ProgramMode { get; set; }
        public virtual DbSet<ProgramStatus> ProgramStatus { get; set; }
        public virtual DbSet<ProgramType> ProgramType { get; set; }
        public virtual DbSet<Qualification> Qualification { get; set; }
        public virtual DbSet<Rank> Rank { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Rubric> Rubric { get; set; }
        public virtual DbSet<Specialist> Specialist { get; set; }
        public virtual DbSet<StudyLevel> StudyLevel { get; set; }
        public virtual DbSet<StudyLevelGroup> StudyLevelGroup { get; set; }
        public virtual DbSet<StudyPlanPrintFormType> StudyPlanPrintFormType { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<WorkData> WorkData { get; set; }
        public virtual DbSet<OrganizationList> OrganizationList { get; set; }
        public virtual DbSet<OrganizationRubricList> OrganizationRubricList { get; set; }
        public virtual DbSet<PartnerPersonActivityArea> PartnerPersonActivityArea { get; set; }
        public virtual DbSet<PartnerPersonFaculty> PartnerPersonFaculty { get; set; }
        public virtual DbSet<PartnerPersonLP> PartnerPersonLP { get; set; }
        public virtual DbSet<PartnerPersonRubric> PartnerPersonRubric { get; set; }
    
        public virtual int RoleMember(string roleName, ObjectParameter result)
        {
            var roleNameParameter = roleName != null ?
                new ObjectParameter("roleName", roleName) :
                new ObjectParameter("roleName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RoleMember", roleNameParameter, result);
        }
    }
}
