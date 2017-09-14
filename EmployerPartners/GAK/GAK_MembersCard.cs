using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmployerPartners.EDMX;

namespace EmployerPartners
{
    public partial class GAK_MembersCard : Form
    {
        private string OP
        {
            get { return tbOP.Text.Trim(); }
            set { tbOP.Text = value; }
        }
        private string GAKNumber
        {
            get { return tbGAKNumber.Text.Trim(); }
            set { tbGAKNumber.Text = value; }
        }
        private string ExamVKR
        {
            get { return tbExamVKR.Text.Trim(); }
            set { tbExamVKR.Text = value; }
        }
        private string GAKYear
        {
            get { return tbGAKYear.Text.Trim(); }
            set { tbGAKYear.Text = value; }
        }
        private string DateTimeMeeting
        {
            get { return tbDateTimeMeeting.Text.Trim(); }
            set { tbDateTimeMeeting.Text = value; }
        }
        //private string Address
        //{
        //    get { return tbAddress.Text.Trim(); }
        //    set { tbAddress.Text = value; }
        //}
        private string Address
        {
            get { return ComboServ.GetComboId(cbAddress); }
            set { ComboServ.SetComboId(cbAddress, value); }
        }
        private string Auditorium
        {
            get { return tbAuditorium.Text.Trim(); }
            set { tbAuditorium.Text = value; }
        }
        private string Coordinator
        {
            get { return tbCoordinator.Text.Trim(); }
            set { tbCoordinator.Text = value; }
        }
        private string Comment
        {
            get { return tbComment.Text.Trim(); }
            set { tbComment.Text = value; }
        }
        private string OrderNumber
        {
            get { return tbOrderNumber.Text.Trim(); }
            set { tbOrderNumber.Text = value; }
        }
        private string OrderDate
        {
            get { return tbOrderDate.Text.Trim(); }
            set { tbOrderDate.Text = value; }
        }

        //Председатель
        private int? PersonId
        {
            get { return ComboServ.GetComboIdInt(cbPerson); }
            set { ComboServ.SetComboId(cbPerson, value); }
        }
        private string ChairmanFIO
        {
            get { return tbChairmanFIO.Text.Trim(); }
            set { tbChairmanFIO.Text = value; }
        }
        private string ChairmanDegree
        {
            get { return tbChairmanDegree.Text.Trim(); }
            set { tbChairmanDegree.Text = value; }
        }
        private string ChairmanRank
        {
            get { return tbChairmanRank.Text.Trim(); }
            set { tbChairmanRank.Text = value; }
        }
        //поле объединенное OrgPosSubdiv
        private string ChairmanOrgPos
        {
            get { return tbChairmanOrgPos.Text.Trim(); }
            set { tbChairmanOrgPos.Text = value; }
        }
        //НПР новые
        private string Faculty
        {
            get { return ComboServ.GetComboId(cbFacultyNPR); }
            set { ComboServ.SetComboId(cbFacultyNPR, value); }
        }
        private string Chair
        {
            get { return ComboServ.GetComboId(cbChair); }
            set { ComboServ.SetComboId(cbChair, value); }
        }
        //Партнеры новые
        private int? RubricId
        {
            get { return ComboServ.GetComboIdInt(cbRubric); }
            set { ComboServ.SetComboId(cbRubric, value); }
        }
        private int? FacultyId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
        }
        private bool isGAK
        {
            get { return chbGAK.Checked; }
            set { chbGAK.Checked = value; }
        }
        private bool isGAKChairMan
        {
            get { return chbGAKChairman.Checked; }
            set { chbGAKChairman.Checked = value; }
        }
        private bool isGAK2016
        {
            get { return chbGAK2016.Checked; }
            set { chbGAK2016.Checked = value; }
        }
        private bool isGAKChairMan2016
        {
            get { return chbGAKChairman2016.Checked; }
            set { chbGAKChairman2016.Checked = value; }
        }
        string GAKNUmberOld;
        private int GAKId
        {
            get;
            set;
        }
        private int OPId
        {
            get;
            set;
        }
        private int? _Id
        {
            get;
            set;
        }
        public int GAK_MembersCardId
        {
            get;
            set;
        }

        UpdateIntHandler _hndl;

        public GAK_MembersCard(int id, int opid, int gakid, UpdateIntHandler _hdl)
        {
            InitializeComponent();
            _Id = id;
            OPId = opid;
            GAKId = gakid;
            GAK_MembersCardId = id;
            _hndl = _hdl;
            this.MdiParent = Util.mainform;
            SetAccessRight();
            FillComboPerson();
            FillChairman();
            FillAddress();
            FillGridNPR();
            FILLGridPP();
            FillFacultyList();
            FillChairList();
            FillGridNPRNew();
            FillComboPPNew();
            FillCard(); //д.б. после FillComboPPNew();
            FillGridPPNew();
            GetDateUpdated();
        }
        private void SetAccessRight()
        {
            if (Util.IsGAKWrite() || Util.IsSuperUser() || Util.IsDBOwner() || Util.IsAdministrator())
            {
                try
                {
                    dgvNPR.Columns[1].Visible = true;
                    dgvNPR.Columns[2].Visible = true;
                    dgvNPRNew.Columns[1].Visible = true;
                    dgvPP.Columns[1].Visible = true;
                    dgvPP.Columns[3].Visible = true;
                    dgvPPNew.Columns[1].Visible = true;
                }
                catch (Exception)
                {
                }
                btnSave.Enabled = true;
                btnDeleteGAK.Enabled = true;
            }
            if (Util.IsGAKWrite() || Util.IsSuperUser())
            {
                btnArchive.Visible = true;
            }
        }
        private void FillComboPerson()
        {
            //            ComboServ.FillCombo(cbPerson, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), pp.Id) AS Id, 
            //                (pp.Name + CASE WHEN d.Acronym is NULL THEN '' ELSE ',   ' + d.Acronym END + ',   ' + pop.OrgPosition) as Name
            //                from dbo.PartnerPerson pp left outer join dbo.Degree d on pp.DegreeId = d.Id inner join dbo.PersonOrgPosition pop on pp.Id = pop.PartnerPersonId " + 
            //                "where pp.Id in (select PartnerPersonId from dbo.GAK_LP_Person) order by Name"), true, false);
            
//            ComboServ.FillCombo(cbPerson, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), pp.Id) AS Id, 
//                (pp.Name + CASE WHEN d.Name is NULL THEN '' ELSE ',  ' + d.Name END + CASE WHEN r.Name is NULL THEN '' ELSE ',  ' + r.Name END) as Name 
//                from dbo.PartnerPerson pp left outer join dbo.Degree d on pp.DegreeId = d.Id left outer join dbo.Rank r on pp.RankId = r.Id " + 
//                "where pp.Id in (select PartnerPersonId from dbo.GAK_LP_Person lpp " +
//                "where (lpp.LicenseProgramId in (select LicenseProgramId from dbo.ObrazProgram op where Id = " + OPId + ")) " + 
//                "and (lpp.GAKId = " + GAKId + ")) order by Name"), true, false);

//            ComboServ.FillCombo(cbPerson, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), pp.Id) AS Id, 
//                (pp.Name + CASE WHEN d.Name is NULL THEN '' ELSE ',  ' + d.Name END + CASE WHEN r.Name is NULL THEN '' ELSE ',  ' + r.Name END) as Name 
//                from dbo.PartnerPerson pp left outer join dbo.Degree d on pp.DegreeId = d.Id left outer join dbo.Rank r on pp.RankId = r.Id " +
//                "where pp.Id in (select PartnerPersonId from dbo.GAK_LP_Person lpp " +
//                "where ((lpp.LicenseProgramId in (select LicenseProgramId from dbo.ObrazProgram op where Id = " + OPId + ")) or " +
//                "(lpp.LicenseProgramId in (select LicenseProgramId from dbo.VKR_ThemesStudentOrder where ObrazProgramId  = " + OPId + "))) " +
//                "and (lpp.GAKId = " + GAKId + ")) order by Name"), true, false);

            int LPid;
            string lpCode = "";
            string opNumber = "";
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var obrazprog = context.ObrazProgram.Where(x => x.Id == OPId).First();
                    LPid = obrazprog.LicenseProgramId;
                    opNumber = (!String.IsNullOrEmpty(obrazprog.Number)) ? obrazprog.Number : "";

                    var lp = context.LicenseProgram.Where(x => x.Id == LPid).First();
                    lpCode = lp.Code;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ComboServ.FillCombo(cbPerson, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), pp.Id) AS Id, 
                (pp.Name + CASE WHEN d.Name is NULL THEN '' ELSE ',  ' + d.Name END + CASE WHEN r.Name is NULL THEN '' ELSE ',  ' + r.Name END) as Name 
                from dbo.PartnerPerson pp left outer join dbo.Degree d on pp.DegreeId = d.Id left outer join dbo.Rank r on pp.RankId = r.Id " +
                "where pp.Id in (select PartnerPersonId from dbo.GAK_LP_Person lpp " +
                "where ((lpp.LicenseProgramId in (select LicenseProgramId from dbo.ObrazProgram op where Id = " + OPId + ")) or " +
                "(lpp.LicenseProgramId in (select SecondLicenseProgramId from dbo.ObrazProgram op where Id = " + OPId + ")) or " +
                "(lpp.LicenseProgramId in (select Id from dbo.LicenseProgram where code in (select LPCode from dbo.GAK_Adapter where OPNumber = " + opNumber + "))) or " + 
                "(lpp.LicenseProgramId in (select Id from dbo.LicenseProgram where Code = '" + lpCode + "'))) " +
                "and (lpp.GAKId = " + GAKId + ")) order by Name"), true, false);
        }
        private void FillAddress()
        {
            ComboServ.FillCombo(cbAddress, HelpClass.GetComboListByQuery(@" select distinct [Name]  AS Id, Name from dbo.GAK_Address order by Name"), true, false);
        }
        private void FillFacultyList()
        {
            ComboServ.FillCombo(cbFacultyNPR, HelpClass.GetComboListByQuery(@" SELECT DISTINCT CONVERT(varchar(255), dbo.SAP_NPR.Faculty) AS Id, 
                CONVERT(varchar(255), dbo.SAP_NPR.Faculty) AS Name 
                FROM dbo.SAP_NPR  WHERE ((dbo.SAP_NPR.Faculty is not null) and (dbo.SAP_NPR.Faculty <> '')) ORDER BY Name"), false, true);
        }
        private void FillChairList()
        {
            string faculty = "";
            if (!String.IsNullOrEmpty(Faculty))
            {
                faculty = " and (dbo.SAP_NPR.Faculty = '" + Faculty + "') ";
            }
            ComboServ.FillCombo(cbChair, HelpClass.GetComboListByQuery(@" SELECT DISTINCT CONVERT(varchar(255), dbo.SAP_NPR.FullName) AS Id, 
                CONVERT(varchar(255), dbo.SAP_NPR.FullName) AS Name 
                FROM dbo.SAP_NPR  WHERE ((dbo.SAP_NPR.FullNAme is not null) and (dbo.SAP_NPR.FullName <> '')" + faculty + ") ORDER BY Name"), false, true);
        }
        private void FillComboPPNew()
        {
            ComboServ.FillCombo(cbRubric, HelpClass.GetComboListByTable("dbo.Rubric"), false, true);
            ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("dbo.Faculty"), false, true);
        }

        private void FillCard()
        {
            try
            {
                if (_Id.HasValue)
                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        var gak = (from x in context.GAK_Number
                                   join gakyear in context.GAK on x.GAKId equals gakyear.Id
                                   join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                                   join examvkr in context.GAK_ExamVKR on x.ExamVKRId equals examvkr.Id
                                   join person in context.PartnerPerson on x.PartnerPersonId equals person.Id into _person
                                   from person in _person.DefaultIfEmpty()
                                   where x.Id == _Id
                                   select x).First();
                        //OPId = gak.ObrazProgramId;
                        OP = gak.ObrazProgram.Name;
                        GAKNumber = gak.GAKNumber;
                        GAKNUmberOld = gak.GAKNumber;
                        ExamVKR = gak.GAK_ExamVKR.Name;
                        GAKYear = gak.GAK.GAKYear;
                        DateTimeMeeting = (gak.DateTimeMeeting.HasValue) ? gak.DateTimeMeeting.Value.ToString("dd.MM.yyyy HH:mm") : "";
                        Address = gak.Address;
                        Auditorium = gak.Auditorium;
                        Coordinator = gak.Coordinator;
                        Comment = gak.Comment;
                        OrderNumber = (!String.IsNullOrEmpty(gak.OrderNumber)) ? gak.OrderNumber : "";
                        OrderDate = (gak.OrderDate.HasValue) ? gak.OrderDate.Value.ToString("dd.MM.yyyy") : "";
                        PersonId = gak.PartnerPersonId;
                        FacultyId = gak.ObrazProgram.FacultyId; // установка ф-та в выборе партнеров
                        try
                        {
                            tbAuthor.Text = "Комиссия заведена пользователем: " + Util.GetADUserName(gak.Author) + "  " + ((gak.DateCreated.HasValue) ? gak.DateCreated.Value.Date.ToString("dd.MM.yyyy") : "");
                            //tbAuthorUpdated.Text = "Последние изменения произведены: " + Util.GetADUserName(gak.AuthorUpdated) + "  " + ((gak.DateUpdated.HasValue) ? gak.DateUpdated.Value.Date.ToString("dd.MM.yyyy") : "");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Инфо");  
                        }
                        try
                        {
                            this.Text = "ГЭК: карточка комиссии " + GAKNumber;
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            if (OrderNumber != "")
                            {
                                //приказ зафиксирован
                                btnSave.Enabled = false;
                                tbGAKNumber.ReadOnly = true;
                                btnDeleteGAK.Enabled = false;
                                lblFrozen.Text = "Приказ зафиксирован!"; //\r\n№ " + OrderNumber + " от " + OrderDate;
                                lblFrozen.Visible = true;
                                try
                                {
                                    dgvNPR.Columns[1].Visible = false;
                                    //dgvNPR.Columns[2].Visible = false;
                                    dgvNPRNew.Columns[1].Visible = false;
                                    dgvPP.Columns[1].Visible = false;
                                    //dgvPP.Columns[3].Visible = false;
                                    dgvPPNew.Columns[1].Visible = false;
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.Message);
            }
        }
        private void FillChairman()
        {
            try
            {
                if (!PersonId.HasValue)
                {
                    ChairmanFIO = "";
                    ChairmanDegree = "";
                    ChairmanRank = "";
                    ChairmanOrgPos = "";
                    return;
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.PartnerPersonOrgPosition.Where(x => x.Id == (int)PersonId).First();
                    ChairmanOrgPos = (!String.IsNullOrEmpty(lst.OrgPosition)) ? lst.OrgPosition : "";
                }
            }
            catch (Exception)
            {
            }
        }

        private void cbPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillChairman();
        }

        private void FillGridNPR()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_MemberNPR
                               where x.GAK_NumberId == _Id
                               orderby x.NPR_FIO
                               select new
                               {
                                   НПР_ФИО = x.NPR_FIO,
                                   НПР_аккаунт = x.NPR_Account,
                                   НПР_неявка = x.IsAbsent,
                                   НПР_степень = x.NPR_Degree,
                                   НПР_звание = x.NPR_Rank,
                                   НПР_должность = x.NPR_Position,
                                   НПР_кафедра = x.NPR_Chair,
                                   НПР_УНП = x.NPR_Faculty,
                                   x.Id,
                                   x.NPR_Persnum,
                                   x.NPR_Tabnum
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSourceNPR.DataSource = dt;
                    dgvNPR.DataSource = bindingSourceNPR;

                    List<string> Cols = new List<string>() { "Id", "NPR_Persnum", "NPR_Tabnum" };

                    foreach (string s in Cols)
                        if (dgvNPR.Columns.Contains(s))
                            dgvNPR.Columns[s].Visible = false;

                    foreach (DataGridViewColumn col in dgvNPR.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnDelNPR")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDivNPR")
                        {
                            col.HeaderText = "";
                        }
                        if (col.Name == "ColumnIsAbsentNPR")
                        {
                            col.HeaderText = "Действие";
                        }
                    }
                    try
                    {
                        dgvNPR.Columns["ColumnDivNPR"].Width = 6;
                        dgvNPR.Columns["ColumnDelNPR"].Width = 70;
                        dgvNPR.Columns["ColumnIsAbsentNPR"].Width = 70;
                        dgvNPR.Columns["НПР_ФИО"].Frozen = true;
                        dgvNPR.Columns["НПР_ФИО"].Width = 200;
                        dgvNPR.Columns["НПР_неявка"].Width = 70;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void FILLGridPP()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_MemberPP
                               join person in context.PartnerPerson on x.PartnerPersonId equals person.Id
                               where x.GAK_NumberId == _Id
                               orderby x.PP_FIO
                               select new
                               {
                                   ФИО_партнер = x.PP_FIO,
                                   Аккаунт_партнер = x.PP_Account,
                                   Неявка_партнер = x.IsAbsent,
                                   Степень_партнер = x.PP_Degree,
                                   Звание_партнер = x.PP_Rank,
                                   //Должность_партнер = x.PP_Position,
                                   //Организация_партнер = x.PP_Organization,
                                   //Подразделение_партнер = x.PP_Subdivision,
                                   Должность_организация_подразделение_партнер = x.PP_OrgPosition,
                                   Страна = person.Country.Name,
                                   Email = person.Email,
                                   Телефон = person.Phone,
                                   //Примечание = x.Comment,
                                   x.Id,
                                   x.PartnerPersonId
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSourcePP.DataSource = dt;
                    dgvPP.DataSource = bindingSourcePP;

                    List<string> Cols = new List<string>() { "Id", "PartnerPersonId" };

                    foreach (string s in Cols)
                        if (dgvPP.Columns.Contains(s))
                            dgvPP.Columns[s].Visible = false;

                    foreach (DataGridViewColumn col in dgvPP.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnDelPP")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDivPP")
                        {
                            col.HeaderText = "";
                        }
                        if (col.Name == "ColumnCardPP")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnIsAbsentPP")
                        {
                            col.HeaderText = "Действие";
                        }
                    }
                    try
                    {
                        dgvPP.Columns["ColumnDivPP"].Width = 6;
                        dgvPP.Columns["ColumnDelPP"].Width = 70;
                        dgvPP.Columns["ColumnCardPP"].Width = 70;
                        dgvPP.Columns["ColumnIsAbsentPP"].Width = 70;
                        dgvPP.Columns["ФИО_партнер"].Frozen = true;
                        dgvPP.Columns["ФИО_партнер"].Width = 200;
                        dgvPP.Columns["Неявка_партнер"].Width = 70;
                        dgvPP.Columns["Должность_организация_подразделение_партнер"].Width = 600;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void FillGridPPNew()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from org in context.PartnerPerson
                               join r in context.PartnerPersonRubric on org.Id equals r.PartnerPersonId into _r
                               from or in _r.DefaultIfEmpty()
                               join f in context.PartnerPersonFaculty on org.Id equals f.PartnerPersonId into _f
                               from of in _f.DefaultIfEmpty()
                               where
                                   //(DegreeId.HasValue ? org.DegreeId == DegreeId : true) &&
                                   //(RankId.HasValue ? org.RankId == RankId : true) &&
                                   //(RankHonoraryId.HasValue ? org.RankHonoraryId == RankHonoraryId : true) &&
                                   //(RankStateId.HasValue ? org.RankStateId == RankStateId : true) &&
                                   //(ActivityAreaId.HasValue ? org.ActivityAreaId == ActivityAreaId : true) &&
                                   //(CountryId.HasValue ? org.CountryId == CountryId : true) &&
                                   //(RegionId.HasValue ? org.RegionId == RegionId : true) &&
                               (RubricId.HasValue ? or.RubricId == RubricId : true) &&
                               (FacultyId.HasValue ? of.FacultyId == FacultyId : true) &&
                               ((isGAK == true) ? org.IsGAK == true : true) &&
                               ((isGAKChairMan == true) ? org.IsGAKChairman == true : true) &&
                               ((isGAK2016 == true) ? org.IsGAK2016 == true : true) &&
                               ((isGAKChairMan2016 == true) ? org.IsGAKChairman2016 == true : true)

                               //((isGAK == true) ? ((isGAKChairMan == true) ? ((org.IsGAK == true) || (org.IsGAKChairman == true)) : (org.IsGAK == true)) : 
                               //((isGAKChairMan == true) ? (org.IsGAKChairman == true): true))

                               orderby org.Name
                               select new
                               {
                                   ФИО = org.Name,
                                   ФИО_англ = org.NameEng,
                                   Префикс = org.PartnerPersonPrefix.Name,
                                   Регистрационный_номер_ИС_Партнеры = org.Account,
                                   Ученая_степень = org.Degree.Name,
                                   Ученое_звание = org.Rank.Name,
                                   Почетное_звание = org.RankHonorary.Name,
                                   Государственное_или_военное_звание = org.RankState.Name,
                                   //Регалии_доп_данные = org.Title,
                                   Входит_в_составы_ГЭК_2017 = org.IsGAK,
                                   Председатель_ГЭК_2017 = org.IsGAKChairman,
                                   Входит_в_составы_ГЭК_2016 = org.IsGAK2016,
                                   Председатель_ГЭК_2016 = org.IsGAKChairman2016,
                                   Основная_сфера_деятельности = org.ActivityArea.Name,
                                   //Выпускник_СПбГУ = org.IsSPbGUGraduate.HasValue && org.IsSPbGUGraduate.Value ? "да" : "нет",
                                   //Год_выпуска = org.SPbGUGraduateYear,
                                   //Ассоциация_выпускников = org.AlumniAssociation.HasValue && org.AlumniAssociation.Value ? "да" : "нет",
                                   Страна = org.Country.Name,
                                   Email = org.Email,
                                   Телефон = org.Phone,
                                   //Мобильный_телефон = org.Mobiles,
                                   //Web_сайт = org.WebSite,
                                   Комментарий = org.Comment,
                                   org.Id
                               }).Distinct().OrderBy(x => x.ФИО).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSourcePPNew.DataSource = dt;
                    dgvPPNew.DataSource = bindingSourcePPNew;

                    List<string> Cols = new List<string>() { "Id" };

                    foreach (string s in Cols)
                        if (dgvPPNew.Columns.Contains(s))
                            dgvPPNew.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvPPNew.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnAddPPNew")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDivPPNew")
                        {
                            col.HeaderText = "";
                        }
                        if (col.Name == "ColumnCardPPNew")
                        {
                            col.HeaderText = "Действие";
                        }
                    }
                    try
                    {
                        dgvPPNew.Columns["ColumnDivPPNew"].Width = 6;
                        dgvPPNew.Columns["ColumnAddPPNew"].Width = 70;
                        dgvPPNew.Columns["ColumnCardPPNew"].Width = 70;
                        dgvPPNew.Columns["ФИО"].Frozen = true;
                        dgvPPNew.Columns["ФИО"].Width = 200;
                        dgvPPNew.Columns["Префикс"].Width = 60;
                        dgvPPNew.Columns["Ученая_степень"].Width = 150;
                        dgvPPNew.Columns["Ученое_звание"].Width = 150;
                        dgvPPNew.Columns["Почетное_звание"].Width = 150;
                        dgvPPNew.Columns["Государственное_или_военное_звание"].Width = 150;
                        //dgvPPNew.Columns["Регалии_доп_данные"].Width = 300;
                    }
                    catch (Exception)
                    {
                    }

                    //if (id.HasValue)
                    //    foreach (DataGridViewRow rw in dgvPPNew.Rows)
                    //        if (rw.Cells[0].Value.ToString() == id.Value.ToString())
                    //        {
                    //            dgvPPNew.CurrentCell = rw.Cells["ФИО"];
                    //            break;
                    //        }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сформировать список физических лиц\r\n" + "Причина: " + ex.Message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private bool CheckFields()
        {
            //проверка наличия данных
            if (String.IsNullOrEmpty(GAKNumber))
            {
                MessageBox.Show("Не указан номер комиссии.", "Отмена",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }           
            //проверка наличия комиссии с введенным номером
            if (GAKNumber != GAKNUmberOld)  //т.е., номер изменился
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_Number
                               //join gak in context.GAK on x.GAKId equals gak.Id
                               where (x.GAKId == GAKId) && (x.GAKNumber == GAKNumber)  //&& (x.ObrazProgramId == ObrazProgramId) 
                               select new
                               {
                                   x.Id,
                               }).ToList().Count();
                    if (lst > 0)
                    {
                        MessageBox.Show("Комиссия с номером " + GAKNumber + " для выбранного года " + GAKYear + "\r\n" + "уже существует.", "Отмена",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        GAKNumber = GAKNUmberOld;
                        return false;
                    }
            }
            }
            //проверка формата даты/времени
            DateTime res;
            if (!String.IsNullOrEmpty(DateTimeMeeting))
            {
                if (!DateTime.TryParse(DateTimeMeeting, out res))
                {
                    MessageBox.Show("Неправильный формат даты и времени в поле 'Дата, время заседания' \r\n" + "Образец: 01.12.2017 10:00", "Отмена", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
                return;
            if (!CheckFields())
                return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    // Надо обновить два PartnerPerson - старый Председатель и новый председатель


                    var gak = context.GAK_Number.Where(x => x.Id == _Id).First();
                    
                    int? OldChairmanId = gak.PartnerPersonId;
                    int? NewCHairmainId = PersonId;

                    gak.GAKNumber = GAKNumber;
                    gak.PartnerPersonId = PersonId;
                    gak.Coordinator = Coordinator;
                    gak.Comment = Comment;
                    gak.AuthorUpdated = System.Environment.UserName;
                    gak.DateUpdated = DateTime.Now;
                    gak.Address = Address;
                    gak.Auditorium = Auditorium;
                    if (!String.IsNullOrEmpty(DateTimeMeeting))
                    {
                        gak.DateTimeMeeting = DateTime.Parse(DateTimeMeeting);
                    }
                    else
                    {
                        gak.DateTimeMeeting = null;
                    }
                    context.SaveChanges();
                    //Логирование
                    int id = (int)_Id;
                    string action = "Сохранение карточки";
                    if (Utilities.GAKLog(id, action))
                    {
                        ;//MessageBox.Show("Логирование произведено", "Успешное завершение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ;// MessageBox.Show("Не удалось произвести логирование... ", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    // Обновим Chairman
                    foreach (int? ChairmanId in new List<int?>() { OldChairmanId, NewCHairmainId })
                    {
                        if (ChairmanId.HasValue)
                        {
                            PartnerPerson Chairman = context.PartnerPerson.Where(x => x.Id == ChairmanId).First();
                            Chairman.IsGAKChairman = context.GAK_Number.Where(x => x.GAKId == 2 && x.PartnerPersonId == ChairmanId).Count() > 0;
                            //Chairman.IsGAKChairman2016 = context.GAK_Number.Where(x => x.GAKId == 1 && x.PartnerPersonId == ChairmanId).Count() > 0;
                        }
                    }
                    context.SaveChanges();

                    MessageBox.Show("Данные сохранены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (_hndl != null)
                        _hndl(_Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + ex.Message + "\r\n" + ex.InnerException.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GAK_MembersCard_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Parent.Width > this.Width + 50 + this.Left)
                {
                    this.Width = this.Parent.Width - 50 - this.Left;
                }
                if (this.Parent.Height > this.Height + 50 + this.Top)
                {
                    this.Height = this.Parent.Height - 50 - this.Top;
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnDeleteGAK_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(OrderNumber))
            {
                MessageBox.Show("Приказ зафиксирован.\r\nУдаление комиссии невозможно.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                string guknumber = (!String.IsNullOrEmpty(GAKNumber)) ? GAKNumber : "";
                if (MessageBox.Show("Удалить комиссию " + GAKNumber + "?", "Запрос на подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                try
                {
                    context.GAK_Number.Remove(context.GAK_Number.Where(x => x.Id == _Id).First());
                    context.SaveChanges();

                    if (_hndl != null)
                        _hndl(_Id);

                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось удалить комиссию...\r\n" + "Обычно это связано с наличием связанных записей в других таблицах.\r\n" +
                        "Примечание: полностью сформированная комиссия, \r\n" + "содержащая список членов комиссии, \r\n" + "удаляется в следующей последовательности:\r\n" +
                        "1. Сначала удаляются все члены комиссии\r\n как НПР, так и Партнеры\r\n" +
                        "2. После этого используется кнопка «Удалить комиссию»\r\n" + "для окончательного удаления.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        private void GetDateUpdated()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var x = context.SAP_NPR.First();
                    lblDateUpdated.Text = "по данным SAP на " + ((x.TIMESTAMP.HasValue) ? x.TIMESTAMP.Value.Date.ToString("dd.MM.yyyy") : "");
                }
            }
            catch (Exception)
            {
            }
        }
        private void FillGridNPRNew()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.SAP_NPR
                               where ((!String.IsNullOrEmpty(Faculty)) ? (x.Faculty == Faculty) : true) && (x.FullName != "ДГПХ") &&
                                        ((!String.IsNullOrEmpty(Chair)) ? (x.FullName == Chair) : true)
                               where x.FullName != "ДГПХ"
                               orderby x.Lastname, x.Name, x.Surname
                               select new
                               {
                                   ФИО = ((!String.IsNullOrEmpty(x.Lastname)) ? x.Lastname : "") + " " + ((!String.IsNullOrEmpty(x.Name)) ? x.Name : "") + " " + ((!String.IsNullOrEmpty(x.Surname)) ? x.Surname : ""),
                                   Аккаунт = x.UsridAd,
                                   Степень = x.Degree,
                                   Звание = x.Titl2,
                                   Должность = x.Position,
                                   Кафедра = x.FullName,
                                   УНП = x.Faculty,
                                   Email = x.Email,
                                   x.Persnum,
                                   x.Tabnum,
                               }).Distinct().OrderBy(x => x.ФИО).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSourceNPRNew.DataSource = dt;
                    dgvNPRNew.DataSource = bindingSourceNPRNew;

                    List<string> Cols = new List<string>() { "Persnum", "Tabnum" };

                    foreach (string s in Cols)
                        if (dgvNPRNew.Columns.Contains(s))
                            dgvNPRNew.Columns[s].Visible = false;

                    foreach (DataGridViewColumn col in dgvNPRNew.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "Column1")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDiv")
                        {
                            col.HeaderText = "";
                        }
                        if (col.Name == "ColumnCard")
                        {
                            col.HeaderText = "Действие";
                        }
                    }

                    try
                    {
                        dgvNPRNew.Columns["ColumnDiv"].Width = 6;
                        dgvNPRNew.Columns["Column1"].Width = 70;
                        dgvNPRNew.Columns["ColumnCard"].Width = 70;
                        dgvNPRNew.Columns["ФИО"].Frozen = true;
                        dgvNPRNew.Columns["ФИО"].Width = 250;
                        dgvNPRNew.Columns["Степень"].Width = 200;
                        dgvNPRNew.Columns["Звание"].Width = 100;
                        dgvNPRNew.Columns["Должность"].Width = 200;
                        dgvNPRNew.Columns["Кафедра"].Width = 250;
                        dgvNPRNew.Columns["УНП"].Width = 250;
                        dgvNPRNew.Columns["Email"].Width = 150;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string search = tbSearch.Text.Trim().ToUpper();
                bool exit = false;
                for (int i = 0; i < dgvNPRNew.RowCount; i++)
                {
                    if (exit)
                    { break; }
                    for (int j = 0; j < 3 /*dgv.Columns.Count*/; j++)
                    {
                        if ((j == 0) || (j == 1))
                            continue;
                        int length = 1;
                        length = dgvNPRNew[j, i].Value.ToString().Length;
                        length = (length <= 15) ? length : 15;
                        if (dgvNPRNew[j, i].Value.ToString().Substring(0, length).ToUpper().Contains(search))
                        {
                            dgvNPRNew.CurrentCell = dgvNPRNew[(j > 0) ? j - 1 : j, i];
                            exit = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void dgvNPRNew_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNPRNew.CurrentCell != null)
                if (dgvNPRNew.CurrentRow.Index >= 0)
                {
                    if (dgvNPRNew.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            dgvNPRNew.CurrentCell = dgvNPRNew.CurrentRow.Cells["ФИО"];
                        }
                        catch (Exception)
                        {
                        }
                        //проверка наличия НПР в списке членов комиссии
                        try
                        {
                            string Persnum = dgvNPRNew.CurrentRow.Cells["Persnum"].Value.ToString();
                            string FIO = (dgvNPRNew.CurrentRow.Cells["ФИО"].Value != null) ? dgvNPRNew.CurrentRow.Cells["ФИО"].Value.ToString() : "";

                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                var lst = (from x in context.GAK_MemberNPR
                                           where (x.NPR_Persnum == Persnum) && (x.GAK_NumberId == _Id)
                                           select new
                                           {
                                               x.Id
                                           }).ToList().Count();

                                if (lst > 0)
                                {
                                    MessageBox.Show("НПР " + FIO + "\r\n" + "уже находится в составе комиссии.", "Инфо",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        //собственно добавление
                        try
                        {
                            string Persnum = dgvNPRNew.CurrentRow.Cells["Persnum"].Value.ToString();
                            string Tabnum = dgvNPRNew.CurrentRow.Cells["Tabnum"].Value.ToString();
                            string FIO = dgvNPRNew.CurrentRow.Cells["ФИО"].Value.ToString();
                            string Account = dgvNPRNew.CurrentRow.Cells["Аккаунт"].Value.ToString();
                            string Degree = dgvNPRNew.CurrentRow.Cells["Степень"].Value.ToString();
                            string Rank = dgvNPRNew.CurrentRow.Cells["Звание"].Value.ToString();
                            string Position = dgvNPRNew.CurrentRow.Cells["Должность"].Value.ToString();
                            string Chair = dgvNPRNew.CurrentRow.Cells["Кафедра"].Value.ToString();
                            string Faculty = dgvNPRNew.CurrentRow.Cells["УНП"].Value.ToString();

                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                GAK_MemberNPR gak = new GAK_MemberNPR();
                                gak.GAK_NumberId = (int)_Id;
                                gak.NPR_Persnum = Persnum;
                                gak.NPR_Tabnum = Tabnum;
                                gak.NPR_FIO = FIO;
                                gak.NPR_Account = Account;
                                gak.NPR_Degree = Degree;
                                gak.NPR_Rank = Rank;
                                gak.NPR_Position = Position;
                                gak.NPR_Chair = Chair;
                                gak.NPR_Faculty = Faculty;

                                context.GAK_MemberNPR.Add(gak);
                                context.SaveChanges();
                                //Логирование
                                int id = (int)_Id;
                                string action = "Добавление члена ГЭК НПР";
                                if (Utilities.GAKLog(id, action))
                                {
                                    ;//MessageBox.Show("Логирование произведено", "Успешное завершение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    ;// MessageBox.Show("Не удалось произвести логирование... ", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                FillGridNPR();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                }
        }

        private void dgvNPR_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNPR.CurrentCell != null)
                if (dgvNPR.CurrentRow.Index >= 0)
                {
                    if (dgvNPR.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            dgvNPR.CurrentCell = dgvNPR.CurrentRow.Cells["НПР_ФИО"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        int id;
                        try
                        {
                            id = int.Parse(dgvNPR.CurrentRow.Cells["Id"].Value.ToString());
                        }
                        catch (Exception)
                        {
                            return;
                        }
                        string member = "";
                        try
                        {
                            member = dgvNPR.CurrentRow.Cells["НПР_ФИО"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        //////
                        //string ordernumber = "";
                        //try
                        //{
                        //    ordernumber = dgvNPR.CurrentRow.Cells["Номер_приказа"].Value.ToString();
                        //    if (ordernumber != "")
                        //    {
                        //        MessageBox.Show("Невозможно удаление " + dgvNPR + "\r\n" + "т.к. приказ зафиксирован (" + ordernumber + ")\r\n", "Инфо",
                        //            //"для выбранного года " + cbGAK.Text, "Инфо",
                        //                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //        return;
                        //    }
                        //}
                        //catch (Exception)
                        //{
                        //}
                        //////
                        if (MessageBox.Show("Удалить выбранное лицо из списка? \r\n" + member, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            try
                            {
                                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                                {
                                    context.GAK_MemberNPR.RemoveRange(context.GAK_MemberNPR.Where(x => x.Id == id));
                                    context.SaveChanges();
                                    //Логирование
                                    string action = "Удаление члена ГЭК НПР";
                                    if (Utilities.GAKLog((int)_Id, action))
                                    {
                                        ;//MessageBox.Show("Логирование произведено", "Успешное завершение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        ;// MessageBox.Show("Не удалось произвести логирование... ", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Не удалось удалить запись...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            FillGridNPR();
                            return;
                        }
                        else
                            return;
                    }
                    //редактировать неявку
                    if (dgvNPR.CurrentCell.ColumnIndex == 2)
                    {
                        try
                        {
                            int id = int.Parse(dgvNPR.CurrentRow.Cells["Id"].Value.ToString());
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                var gak = context.GAK_MemberNPR.Where(x => x.Id == id).First();
                                if (gak.IsAbsent.HasValue)
                                {
                                    gak.IsAbsent = !(bool)gak.IsAbsent;
                                }
                                else
                                {
                                    gak.IsAbsent = true;
                                }
                                context.SaveChanges();
                                FillGridNPR();
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Не удалось отредактировать запись\r\n" + "Причина: " + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
        }

        private void btnRefreshPPNew_Click(object sender, EventArgs e)
        {
            FillGridPPNew();
        }

        private void dgvPPNew_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPPNew.CurrentCell != null)
                if (dgvPPNew.CurrentRow.Index >= 0)
                {
                    if (dgvPPNew.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            dgvPPNew.CurrentCell = dgvPPNew.CurrentRow.Cells["ФИО"];
                        }
                        catch (Exception)
                        {
                        }
                        //проверка наличия физ. лица в списке Партнеров
                        try
                        {
                            int PersonId = int.Parse(dgvPPNew.CurrentRow.Cells["Id"].Value.ToString());
                            string FIO = (dgvPPNew.CurrentRow.Cells["ФИО"].Value != null) ? dgvPPNew.CurrentRow.Cells["ФИО"].Value.ToString() : "";
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                var lst = (from x in context.GAK_MemberPP
                                           where (x.PartnerPersonId == PersonId) && (x.GAK_NumberId == _Id)
                                           select new
                                           {
                                               x.Id,
                                           }).ToList().Count();
                                if (lst > 0)
                                {
                                    MessageBox.Show("Партнер\r\n" + FIO + "\r\n" + "уже находится в составе комиссии\r\n" , "Инфо",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        //собственно добавление
                        try
                        {
                            int PersonId = int.Parse(dgvPPNew.CurrentRow.Cells["Id"].Value.ToString());
                            string FIO = (dgvPPNew.CurrentRow.Cells["ФИО"].Value != null) ? dgvPPNew.CurrentRow.Cells["ФИО"].Value.ToString() : "";
                            string Account = (dgvPPNew.CurrentRow.Cells["Регистрационный_номер_ИС_Партнеры"].Value != null) ? dgvPPNew.CurrentRow.Cells["Регистрационный_номер_ИС_Партнеры"].Value.ToString() : "";
                            string Degree = (dgvPPNew.CurrentRow.Cells["Ученая_степень"].Value != null) ? dgvPPNew.CurrentRow.Cells["Ученая_степень"].Value.ToString() : "";
                            string Rank = (dgvPPNew.CurrentRow.Cells["Ученое_звание"].Value != null) ? dgvPPNew.CurrentRow.Cells["Ученое_звание"].Value.ToString() : "";
                            string OrgPosition = "";
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                try
                                {
                                    var lst = context.PersonOrgPositionMiddle.Where(x => x.PartnerPersonId == PersonId).First();
                                    OrgPosition = lst.OrgPosition;
                                }
                                catch (Exception)
                                {
                                    OrgPosition = "";
                                }
                            }

                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                //GAK_LP_Person gak = new GAK_LP_Person();
                                GAK_MemberPP gak = new GAK_MemberPP();

                                gak.GAK_NumberId = (int)_Id;
                                gak.PartnerPersonId = PersonId;
                                gak.PP_FIO = FIO;
                                gak.PP_Account = Account;
                                gak.PP_Degree = Degree;
                                gak.PP_Rank = Rank;
                                gak.PP_OrgPosition = OrgPosition;

                                context.GAK_MemberPP.Add(gak);
                                context.SaveChanges();
                                //Логирование
                                int id = (int)_Id;
                                string action = "Добавление члена ГЭК Партнер";
                                if (Utilities.GAKLog(id, action))
                                {
                                    ;//MessageBox.Show("Логирование произведено", "Успешное завершение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    ;// MessageBox.Show("Не удалось произвести логирование... ", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                FILLGridPP();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                    if (dgvPPNew.CurrentCell.ColumnIndex == 2)
                    {
                        try
                        {
                            int PersonId = int.Parse(dgvPPNew.CurrentRow.Cells["Id"].Value.ToString());
                            if (Utilities.PersonCardIsOpened(PersonId))
                                return;
                            new CardPerson(PersonId, null).Show();
                        }
                        catch
                        {
                        }
                    }
                }
        }

        private void dgvPP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPP.CurrentCell != null)
                if (dgvPP.CurrentRow.Index >= 0)
                {
                    if (dgvPP.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            dgvPP.CurrentCell = dgvPP.CurrentRow.Cells["ФИО_партнер"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        int id;
                        try
                        {
                            id = int.Parse(dgvPP.CurrentRow.Cells["Id"].Value.ToString());
                        }
                        catch (Exception)
                        {
                            return;
                        }
                        string member = "";
                        try
                        {
                            member = dgvPP.CurrentRow.Cells["ФИО_партнер"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
 
                        if (MessageBox.Show("Удалить выбранное лицо из списка? \r\n" + member, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            try
                            {
                                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                                {
                                    context.GAK_MemberPP.RemoveRange(context.GAK_MemberPP.Where(x => x.Id == id));
                                    context.SaveChanges(); 

                                    //Логирование
                                    string action = "Удаление члена ГЭК Партнер";
                                    if (Utilities.GAKLog((int)_Id, action))
                                    {
                                        ;//MessageBox.Show("Логирование произведено", "Успешное завершение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                    else
                                    {
                                        ;// MessageBox.Show("Не удалось произвести логирование... ", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Не удалось удалить запись...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            FILLGridPP();
                            return;
                        }
                        else
                            return;
                    }

                    if (dgvPP.CurrentCell.ColumnIndex == 2)
                    {
                        try
                        {
                            dgvPP.CurrentCell = dgvPP.CurrentRow.Cells["ФИО_партнер"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        try
                        {
                            int PersonId = int.Parse(dgvPP.CurrentRow.Cells["PartnerPersonId"].Value.ToString());
                            if (Utilities.PersonCardIsOpened(PersonId))
                                return;
                            new CardPerson(PersonId, null).Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Не удалось открыть карточку\r\n" + "Причина: " + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    //редактировать неявку
                    if (dgvPP.CurrentCell.ColumnIndex == 3)
                    {
                        try
                        {
                            int id = int.Parse(dgvPP.CurrentRow.Cells["Id"].Value.ToString());
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                var gak = context.GAK_MemberPP.Where(x => x.Id == id).First();
                                if (gak.IsAbsent.HasValue)
                                {
                                    gak.IsAbsent = !(bool)gak.IsAbsent;
                                }
                                else
                                {
                                    gak.IsAbsent = true;
                                }
                                context.SaveChanges();
                                FILLGridPP();
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Не удалось отредактировать запись\r\n" + "Причина: " + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
        }

        private void tbSearchPP_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string search = tbSearchPP.Text.Trim().ToUpper();
                bool exit = false;
                for (int i = 0; i < dgvPPNew.RowCount; i++)
                {
                    if (exit)
                    { break; }
                    for (int j = 0; j < 5 /*dgv.Columns.Count*/; j++)
                    {
                        if ((j == 0) || (j == 1) || (j == 2))
                            continue;
                        if (dgvPPNew[j, i].ColumnIndex == 0)
                        {
                            int length = 1;
                            length = dgvPPNew[j, i].Value.ToString().Length;
                            length = (length <= 15) ? length : 15;
                            if (dgvPPNew[j, i].Value.ToString().Substring(0, length).ToUpper().Contains(search))
                            {
                                dgvPPNew.CurrentCell = dgvPPNew[j, i];
                                exit = true;
                                break;
                            }
                        }
                        else
                        {
                            if (dgvPPNew[j, i].Value.ToString().ToUpper().Contains(search))
                            {
                                dgvPPNew.CurrentCell = dgvPPNew[j, i];
                                exit = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void cbFacultyNPR_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillChairList();
            //FillGridNPRNew();
        }

        private void cbChair_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGridNPRNew();
        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            if (OrderNumber == "")
            {
                MessageBox.Show("Данная функция предназначена \r\nдля изменения соcтавов ГЭК,\r\nкогда приказ уже зафиксирован.\r\n" +
                                    "В данном случае приказ не зафиксирован.\r\nЛюбые изменения доступны.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Данная функция предназначена для изменения соcтавов ГЭК,\r\nкогда приказ уже зафиксирован.\r\n" +
                    "Что произойдет?\r\n1. Вся информация о комиссии, включая состав ГЭК\r\nбудетсохранена.\r\n" + 
                    "2. Номер и дата приказа будут обнулены.\r\n3. Флаг фиксации будет сброшен.\r\n" + 
                    "Далее будет возможно изменять составы ГЭК.\r\nПродолжить?", 
                    "Запрос на подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                return;
            
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_Number_archive
                               where (x.GAKId == GAKId) && (x.GAKNumber == GAKNumber) && (x.OrderNumber == OrderNumber)
                               select new
                               {
                                   x.Id
                               }).ToList();
                    if (lst.Count != 0) //проверка, что уже сархивировано
                    {
                        MessageBox.Show("Состав ГЭК уже сархивирован.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //Архивирование
                        string action = "Архивирование";
                        if (Utilities.GAKArchive((int)_Id, action))
                        {
                            MessageBox.Show("Архивирование состава ГЭК произведено.", "Успешное завершение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Не удалось произвести архивирование ГЭК... ", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось завершить операцию... ", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Обнуление номера и даты приказа для данной ГЭК
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var gak = context.GAK_Number.Where(x => x.Id == _Id).First();

                    gak.Freeze = null;
                    gak.InOrder = null;
                    gak.OrderNumber = null;
                    gak.OrderDate = null;
                    gak.OrderDop = null;
                    
                    context.SaveChanges();

                    //Логирование
                    int id = (int)_Id;
                    string action_ = "архивирование состава ГЭК";
                    if (Utilities.GAKLog(id, action_))
                    {
                        ;//MessageBox.Show("Логирование произведено", "Успешное завершение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ;// MessageBox.Show("Не удалось произвести логирование... ", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    //MessageBox.Show("Данные сохранены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (_hndl != null)
                        _hndl(_Id);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось обнулить номер и дату приказа...\r\n" + ex.Message + "\r\n" + ex.InnerException.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            FillCard(); // обновить карточку

            btnSave.Enabled = true;
            tbGAKNumber.ReadOnly = true;
            btnDeleteGAK.Enabled = false;
            lblFrozen.Text = " "; //\r\n№ " + OrderNumber + " от " + OrderDate;
            lblFrozen.Visible = false;
            try
            {
                dgvNPR.Columns[1].Visible = true;
                dgvNPR.Columns[2].Visible = true;
                dgvNPRNew.Columns[1].Visible = true;
                dgvPP.Columns[1].Visible = true;
                dgvPP.Columns[3].Visible = true;
                dgvPPNew.Columns[1].Visible = true;
            }
            catch (Exception)
            {
            }
        }
    }
}