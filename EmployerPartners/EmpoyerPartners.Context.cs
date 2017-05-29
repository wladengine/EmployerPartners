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
        public virtual DbSet<EPLog> EPLog { get; set; }
        public virtual DbSet<VKR> VKR { get; set; }
        public virtual DbSet<VKRCoordinator> VKRCoordinator { get; set; }
        public virtual DbSet<VKRSection> VKRSection { get; set; }
        public virtual DbSet<VKRSectionCoordinator> VKRSectionCoordinator { get; set; }
        public virtual DbSet<VKRSectionFaculty> VKRSectionFaculty { get; set; }
        public virtual DbSet<VKROP> VKROP { get; set; }
        public virtual DbSet<VKRThemes> VKRThemes { get; set; }
        public virtual DbSet<VKROPStudent> VKROPStudent { get; set; }
        public virtual DbSet<VKROPTheme> VKROPTheme { get; set; }
        public virtual DbSet<VKRSource> VKRSource { get; set; }
        public virtual DbSet<ActivityAreaProfessional> ActivityAreaProfessional { get; set; }
        public virtual DbSet<OrganizationNames> OrganizationNames { get; set; }
        public virtual DbSet<OrganizationActivityAreaProfessional> OrganizationActivityAreaProfessional { get; set; }
        public virtual DbSet<SAP_ALL_PERS> SAP_ALL_PERS { get; set; }
        public virtual DbSet<SAP_NPR> SAP_NPR { get; set; }
        public virtual DbSet<SAP_ORGSTRUCTURE> SAP_ORGSTRUCTURE { get; set; }
        public virtual DbSet<SAP_POSITIONS> SAP_POSITIONS { get; set; }
        public virtual DbSet<SAP_GROUPS> SAP_GROUPS { get; set; }
        public virtual DbSet<C_Settings> C_Settings { get; set; }
        public virtual DbSet<VKR_Source> VKR_Source { get; set; }
        public virtual DbSet<VKR_Themes> VKR_Themes { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<RankState> RankState { get; set; }
        public virtual DbSet<VKR_Crypt> VKR_Crypt { get; set; }
        public virtual DbSet<RankHonorary> RankHonorary { get; set; }
        public virtual DbSet<OrganizationSubdivision> OrganizationSubdivision { get; set; }
        public virtual DbSet<GAK> GAK { get; set; }
        public virtual DbSet<PersonOrgPosition> PersonOrgPosition { get; set; }
        public virtual DbSet<LicenseInLicenseProgram> LicenseInLicenseProgram { get; set; }
        public virtual DbSet<GAK_ChairmanSource> GAK_ChairmanSource { get; set; }
        public virtual DbSet<PartnerPersonPrefix> PartnerPersonPrefix { get; set; }
        public virtual DbSet<VKR_ThemesStudent> VKR_ThemesStudent { get; set; }
        public virtual DbSet<VKR_OrderSource> VKR_OrderSource { get; set; }
        public virtual DbSet<ECD_EmpContactsDetailsUNP_GAK2016> ECD_EmpContactsDetailsUNP_GAK2016 { get; set; }
        public virtual DbSet<EmployerContactDetails_GAK2016> EmployerContactDetails_GAK2016 { get; set; }
        public virtual DbSet<EmployerContactDetails_GAK2017> EmployerContactDetails_GAK2017 { get; set; }
        public virtual DbSet<ECD_EmpContactsDetailsUNP_GAK2017> ECD_EmpContactsDetailsUNP_GAK2017 { get; set; }
        public virtual DbSet<PartnerPersonOrgPosition> PartnerPersonOrgPosition { get; set; }
        public virtual DbSet<VKR_Student_Not_In_StudData_Id> VKR_Student_Not_In_StudData_Id { get; set; }
        public virtual DbSet<GAK_ExamVKR> GAK_ExamVKR { get; set; }
        public virtual DbSet<GAK_Member> GAK_Member { get; set; }
        public virtual DbSet<GAK_MemberNPR> GAK_MemberNPR { get; set; }
        public virtual DbSet<GAK_MemberPP> GAK_MemberPP { get; set; }
        public virtual DbSet<GAK_Number> GAK_Number { get; set; }
        public virtual DbSet<GAK_LP_Person> GAK_LP_Person { get; set; }
        public virtual DbSet<PersonOrgPositionMiddle> PersonOrgPositionMiddle { get; set; }
        public virtual DbSet<ECD_OrganizationPersonOrgName> ECD_OrganizationPersonOrgName { get; set; }
        public virtual DbSet<GAK_Source> GAK_Source { get; set; }
        public virtual DbSet<VKR_OrganizationList> VKR_OrganizationList { get; set; }
        public virtual DbSet<GAK_MemberNPR_log> GAK_MemberNPR_log { get; set; }
        public virtual DbSet<GAK_MemberPP_log> GAK_MemberPP_log { get; set; }
        public virtual DbSet<GAK_Number_log> GAK_Number_log { get; set; }
        public virtual DbSet<VKR_ThemesStudentOrder> VKR_ThemesStudentOrder { get; set; }
        public virtual DbSet<GAK_MemberNPR_archive> GAK_MemberNPR_archive { get; set; }
        public virtual DbSet<GAK_MemberPP_archive> GAK_MemberPP_archive { get; set; }
        public virtual DbSet<GAK_Number_archive> GAK_Number_archive { get; set; }
        public virtual DbSet<OrganizationEnglishSource> OrganizationEnglishSource { get; set; }
        public virtual DbSet<ext_GAK_2017> ext_GAK_2017 { get; set; }
        public virtual DbSet<GAK_StatOP> GAK_StatOP { get; set; }
        public virtual DbSet<GAK_StatOP_Itog> GAK_StatOP_Itog { get; set; }
        public virtual DbSet<GAK_StatChairmanPPNPR_Itog> GAK_StatChairmanPPNPR_Itog { get; set; }
    
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
    
        public virtual int UpDateSAP_NPR(ObjectParameter result)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpDateSAP_NPR", result);
        }
    
        [DbFunction("EmployerPartnersEntities", "GAK_LP_PersonOrderYear")]
        public virtual IQueryable<GAK_LP_PersonOrderYear_Result> GAK_LP_PersonOrderYear(Nullable<int> gAKId)
        {
            var gAKIdParameter = gAKId.HasValue ?
                new ObjectParameter("GAKId", gAKId) :
                new ObjectParameter("GAKId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GAK_LP_PersonOrderYear_Result>("[EmployerPartnersEntities].[GAK_LP_PersonOrderYear](@GAKId)", gAKIdParameter);
        }
    
        public virtual int GAKLog(Nullable<int> id, string action, ObjectParameter result)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var actionParameter = action != null ?
                new ObjectParameter("action", action) :
                new ObjectParameter("action", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GAKLog", idParameter, actionParameter, result);
        }
    
        public virtual int GAKArchive(Nullable<int> id, string action, ObjectParameter result)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            var actionParameter = action != null ?
                new ObjectParameter("action", action) :
                new ObjectParameter("action", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("GAKArchive", idParameter, actionParameter, result);
        }
    }
}
