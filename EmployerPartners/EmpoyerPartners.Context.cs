﻿//------------------------------------------------------------------------------
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
        public virtual DbSet<PartnerPersonActivityArea> PartnerPersonActivityArea { get; set; }
        public virtual DbSet<PartnerPersonFaculty> PartnerPersonFaculty { get; set; }
        public virtual DbSet<PartnerPersonLP> PartnerPersonLP { get; set; }
        public virtual DbSet<PartnerPersonRubric> PartnerPersonRubric { get; set; }
        public virtual DbSet<HelpFiles> HelpFiles { get; set; }
        public virtual DbSet<Templates> Templates { get; set; }
        public virtual DbSet<Practice> Practice { get; set; }
        public virtual DbSet<PracticeLP> PracticeLP { get; set; }
        public virtual DbSet<PracticeLPOrganization> PracticeLPOrganization { get; set; }
        public virtual DbSet<PracticeType> PracticeType { get; set; }
        public virtual DbSet<PracticeStudent> PracticeStudent { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentData> StudentData { get; set; }
        public virtual DbSet<StudyPlanData> StudyPlanData { get; set; }
        public virtual DbSet<StudentDVZ> StudentDVZ { get; set; }
        public virtual DbSet<PracticeLPStudent> PracticeLPStudent { get; set; }
        public virtual DbSet<DocType> DocType { get; set; }
        public virtual DbSet<PracticeLPFile> PracticeLPFile { get; set; }
        public virtual DbSet<Year> Year { get; set; }
        public virtual DbSet<ObrazProgramInYear> ObrazProgramInYear { get; set; }
        public virtual DbSet<PracticeLPOP> PracticeLPOP { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<ProfileInObrazProgramInYear> ProfileInObrazProgramInYear { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<OrganizationDogovor> OrganizationDogovor { get; set; }
        public virtual DbSet<OrganizationDogovorFile> OrganizationDogovorFile { get; set; }
    
        public virtual int RoleMember(string roleName, ObjectParameter result)
        {
            var roleNameParameter = roleName != null ?
                new ObjectParameter("roleName", roleName) :
                new ObjectParameter("roleName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RoleMember", roleNameParameter, result);
        }
    
        public virtual int UpDateLicenseProgram(ObjectParameter result)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpDateLicenseProgram", result);
        }
    
        public virtual int UpDateObrazProgram(ObjectParameter result)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpDateObrazProgram", result);
        }
    
        public virtual int UpDateStudentData(ObjectParameter result)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpDateStudentData", result);
        }
    
        public virtual int UpDateStudyPlanData(ObjectParameter result)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpDateStudyPlanData", result);
        }
    }
}
