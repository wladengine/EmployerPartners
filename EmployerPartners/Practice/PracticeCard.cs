using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WordOut;

namespace EmployerPartners
{
    public partial class PracticeCard : Form
    {
        #region Fields
        public string PracticeFaculty
        {
            get { return tbPracticeFaculty.Text.Trim(); }
            set { tbPracticeFaculty.Text = value; }
        }
        public string LP
        {
            get { return tbLP.Text.Trim(); }
            set { tbLP.Text = value; }
        }
        public int? PracticeTypeId
        {
            get { return ComboServ.GetComboIdInt(cbPracticeType); }
            set { ComboServ.SetComboId(cbPracticeType, value); }
        }
        public string DateStart
        {
            get { return tbDateStart.Text.Trim(); }
            set { tbDateStart.Text = value; }
        }
        public string DateEnd
        {
            get { return tbDateEnd.Text.Trim(); }
            set { tbDateEnd.Text = value; }
        }

        public string OrderDoc
        {
            get { return tbOrderDoc.Text.Trim(); }
            set { tbOrderDoc.Text = value; }
        }
        public string OrderNumber
        {
            get { return tbOrderNumber.Text.Trim(); }
            set { tbOrderNumber.Text = value; }
        }
        public string OrderDate
        {
            get { return tbOrderDate.Text.Trim(); }
            set { tbOrderDate.Text = value; }
        }
        public string OrderAuthor
        {
            get { return tbOrderAuthor.Text.Trim(); }
            set { tbOrderAuthor.Text = value; }
        }

        public string InstructionDoc
        {
            get { return tbInstructionDoc.Text.Trim(); }
            set { tbInstructionDoc.Text = value; }
        }
        public string InstructionNumber
        {
            get { return tbInstructionNumber.Text.Trim(); }
            set { tbInstructionNumber.Text = value; }
        }
        public string InstructionDate
        {
            get { return tbInstructionDate.Text.Trim(); }
            set { tbInstructionDate.Text = value; }
        }
        public string InstructionAuthor
        {
            get { return tbInstructionAuthor.Text.Trim(); }
            set { tbInstructionAuthor.Text = value; }
        }
        public string Comment
        {
            get { return tbComment.Text.Trim(); }
            set { tbComment.Text = value; }
        }
        public string Comment1
        {
            get { return tbComment1.Text.Trim(); }
            set { tbComment1.Text = value; }
        }
        public string Supervisor
        {
            get { return tbSupervisor.Text.Trim(); }
            set { tbSupervisor.Text = value; }
        }public string AdvanceHolder
        {
            get { return tbAdvanceHolder.Text.Trim(); }
            set { tbAdvanceHolder.Text = value; }
        }
        #endregion

        private int? _Id
        {
            get;
            set;
        }
        private int? _PId
        {
            get;
            set;
        }
        private int? _LPId
        {
            get;
            set;
        }

        public int? FacId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
        }
        public int? RubricId
        {
            get { return ComboServ.GetComboIdInt(cbRubric); }
            set { ComboServ.SetComboId(cbRubric, value); }
        }
        private int? Course
        {
            get { return ComboServ.GetComboIdInt(cbCourse); }
            set { ComboServ.SetComboId(cbCourse, value); }
        }
        private int? OrgStudent
        {
            get { return ComboServ.GetComboIdInt(cbOrgStudent); }
            set { ComboServ.SetComboId(cbOrgStudent, value); }
        }

        bool StudentDelConfirm = true;
        bool StudentShowDelConfirmSettings = true;

        UpdateVoidHandler _hndl;

        public PracticeCard(int id, int pid, int lpid, string lp, UpdateVoidHandler _hdl)
        {
            InitializeComponent();
            _Id = id;
            _PId = pid;
            _LPId = lpid;
            LP = lp;
            _hndl = _hdl;
            FillCard();
            this.MdiParent = Util.mainform;
            this.Text = "Практика: " + LP;
        }
        private void FillCard()
        {
            ComboServ.FillCombo(cbPracticeType, HelpClass.GetComboListByTable("dbo.PracticeType"), true, false);

            FillPractice();
            FillInfo();
            FillOPList();
            FillOP();
            FillOrder();
            FillInstruction();
            FillOrg();
            FillStudent();
        }
        private void FillCombo()
        {
            ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("dbo.Faculty"), false, true);
            ComboServ.FillCombo(cbRubric, HelpClass.GetComboListByTable("dbo.Rubric"), false, true);
        }
        private void FillComboStudent()
        {
            ComboServ.FillCombo(cbCourse, HelpClass.GetComboListByQuery(@" select distinct  CONVERT(varchar(100), StudentData.Course) AS Id, CONVERT(varchar(100), StudentData.Course) as Name
                from dbo.StudentData order by Id"), false, true);
        }
        private void FillComboOrgStudent()
        {
            if (!_Id.HasValue)
            {
                return;
            }
            ComboServ.FillCombo(cbOrgStudent, HelpClass.GetComboListByQuery(@" SELECT CONVERT(varchar(100), dbo.PracticeLPOrganization.Id) AS Id, 
                         dbo.PracticeLPOrganization.OrganizationName +  ' [ ' + dbo.PracticeLPOrganization.OrganizationAddress + ' ]'  AS Name 
                FROM    dbo.PracticeLPOrganization INNER JOIN
                        dbo.Organization ON dbo.PracticeLPOrganization.OrganizationId = dbo.Organization.Id 
                WHERE   dbo.PracticeLPOrganization.PracticeLPId = " + _Id.ToString()), true, false);
        }

        private void FillPractice()
        {
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var Practice = (from x in context.PracticeLP
                                    join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                                    join p in context.Practice on x.PracticeId equals p.Id
                                    join fac in context.Faculty on p.FacultyId equals fac.Id
                                    where x.Id == _Id
                                    select fac).First();
                    PracticeFaculty = Practice.Name;
                }
        }

        private void FillInfo()
        {
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var Practice = (from x in context.PracticeLP
                                    where x.Id == _Id
                                    select x).First();
                    PracticeTypeId = Practice.PracticeTypeId;
                    checkBoxOutSPb.Checked = Practice.OutSPb.HasValue ? (bool) Practice.OutSPb : false;
                    DateStart = (Practice.DateStart.HasValue) ? Practice.DateStart.Value.Date.ToString("dd.MM.yyyy") : "";
                    DateEnd = (Practice.DateEnd.HasValue) ? Practice.DateEnd.Value.Date.ToString("dd.MM.yyyy") : "";
                    OrderDoc = Practice.OrderDoc;
                    OrderNumber = Practice.OrderNumber;
                    OrderDate = (Practice.OrderDate.HasValue) ? Practice.OrderDate.Value.Date.ToString("dd.MM.yyyy") : "";
                    OrderAuthor = Practice.OrderAuthor;
                    InstructionDoc = Practice.InstructionDoc;
                    InstructionNumber = Practice.InstructionNumber;
                    InstructionDate = (Practice.InstructionDate.HasValue) ? Practice.InstructionDate.Value.Date.ToString("dd.MM.yyyy") : "";
                    InstructionAuthor = Practice.InstructionAuthor;
                    Supervisor = Practice.Supervisor;
                    AdvanceHolder = Practice.AdvanceHolder;
                    Comment = Practice.Comment;
                    Comment1 = Practice.Comment;
                }
        }

        private void FillOPList()
        {
            if (!_Id.HasValue || !_LPId.HasValue)
            {
                return;
            }
//            ComboServ.FillCombo(cbOP, HelpClass.GetComboListByQuery(@" SELECT CONVERT(varchar(100), dbo.ObrazProgram.Id) AS Id, 
//                        '[ ' + dbo.ObrazProgram.Number + ' ] ' + dbo.ObrazProgram.Name +  ' [ ' + dbo.ProgramStatus.Name + ' ]'  AS Name 
//                FROM    dbo.ObrazProgram INNER JOIN
//                        dbo.ProgramStatus ON dbo.ObrazProgram.ProgramStatusId = dbo.ProgramStatus.Id 
//                WHERE   dbo.ObrazProgram.LicenseProgramId = " + _LPId.ToString() +
//                        "or dbo.ObrazProgram.SecondLicenseProgramId = " + _LPId.ToString()), true, false);

            ComboServ.FillCombo(cbOP, HelpClass.GetComboListByQuery(@" SELECT CONVERT(varchar(100), dbo.ObrazProgramInYear.Id) AS Id, 
                        '[ ' + dbo.ObrazProgram.Number + ' ] ' + dbo.ObrazProgram.Name + ' [ ' + dbo.ObrazProgramInYear.Year + ' ]' +  
                        ' [ Шифр: ' + dbo.ObrazProgramInYear.ObrazProgramCrypt + ' ]' + ' [ ' + dbo.ProgramStatus.Name + ' ]' + ' [ ' + dbo.ProgramMode.Name + ' ]'  AS Name 
                FROM    dbo.ObrazProgramInYear INNER JOIN 
                        dbo.ObrazProgram on dbo.ObrazProgramInYear.ObrazProgramId = dbo.ObrazProgram.Id 
                        INNER JOIN dbo.ProgramStatus ON dbo.ObrazProgram.ProgramStatusId = dbo.ProgramStatus.Id 
                        INNER JOIN dbo.ProgramMode ON dbo.ObrazProgram.ProgramModeId = dbo.ProgramMode.Id
                WHERE   dbo.ObrazProgram.LicenseProgramId = " + _LPId.ToString() +
                        "or dbo.ObrazProgram.SecondLicenseProgramId = " + _LPId.ToString() +
                        " ORDER BY dbo.ObrazProgram.Name, dbo.ObrazProgramInYear.Year DESC"), true, false);
        }

        private void FillOP()
        {
            if (!_Id.HasValue)
            {
                return;
            }
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.PracticeLPOP
                           //join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                           //join opyear in context.ObrazProgramInYear on op.Id equals opyear.ObrazProgramId
                           join opyear in context.ObrazProgramInYear on x.ObrazProgramInYearId equals opyear.Id
                           join op in context.ObrazProgram on opyear.ObrazProgramId equals op.Id
                           //join ps in context.ProgramStatus on op.ProgramStatusId equals ps.Id
                           join pm in context.ProgramMode on op.ProgramModeId equals pm.Id
                           where x.PracticeLPId == _Id
                           select new
                           {
                               Образовательная_программа = "[ " + opyear.Number + " ] " + opyear.Name + " [ Шифр: " + opyear.ObrazProgramCrypt + " ] " + 
                                                                    " [ " + pm.Name + " ] ",          // + " [ " + ps.Name + " ]",
                               x.Id,
                               //x.ObrazProgramId,
                           }).ToList();

                DataTable dt = new DataTable();
                dt = Utilities.ConvertToDataTable(lst);
                dgvOP.DataSource = dt;

                List<string> Cols = new List<string>() { "Id" };  //{ "Id", "ObrazProgramId" };

                foreach (string s in Cols)
                    if (dgvOP.Columns.Contains(s))
                        dgvOP.Columns[s].Visible = false;
                foreach (DataGridViewColumn col in dgvOP.Columns)
                    col.HeaderText = col.Name.Replace("_", " ");
                try
                {
                    dgvOP.Columns["Образовательная_программа"].Frozen = true;
                    dgvOP.Columns["Образовательная_программа"].Width = 850;
                }
                catch (Exception)
                {
                }
            }
        }

        private void FillOrder()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.PracticeLPFile
                               join dn in context.DocType on x.DocTypeId equals dn.Id
                               where (x.PracticeLPId == _Id) && (x.DocTypeId == 1)
                               select new
                               {
                                   x.Id,
                                   //Тип_документа = dn.Name,
                                   Файл = x.FileName,
                                   Дата_загрузки = x.DateLoad,
                               }).ToList();
                    dgvOrder.DataSource = lst;

                    List<string> Cols = new List<string>() { "Id" };

                    foreach (string s in Cols)
                        if (dgvOrder.Columns.Contains(s))
                            dgvOrder.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvOrder.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnDelOrder")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnViewOrder")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDiv5")
                        {
                            col.HeaderText = "";
                        }
                    }
                    try
                    {
                        dgvOrder.Columns["ColumnDiv5"].Width = 6;
                        dgvOrder.Columns["ColumnDelOrder"].Width = 70;
                        dgvOrder.Columns["ColumnViewOrder"].Width = 70;
                        dgvOrder.Columns["Файл"].Frozen = true;
                        dgvOrder.Columns["Файл"].Width = 250;
                        dgvOrder.Columns["Дата_загрузки"].Width = 120;
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

        private void FillInstruction()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.PracticeLPFile
                               join dn in context.DocType on x.DocTypeId equals dn.Id
                               where (x.PracticeLPId == _Id) && (x.DocTypeId == 2)
                               select new
                               {
                                   x.Id,
                                   //Тип_документа = dn.Name,
                                   Файл = x.FileName,
                                   Дата_загрузки = x.DateLoad,
                               }).ToList();
                    dgvInstruction.DataSource = lst;

                    List<string> Cols = new List<string>() { "Id" };

                    foreach (string s in Cols)
                        if (dgvInstruction.Columns.Contains(s))
                            dgvInstruction.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvInstruction.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnDelInstruction")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnViewInstruction")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDiv6")
                        {
                            col.HeaderText = "";
                        }
                    }
                    try
                    {
                        dgvInstruction.Columns["ColumnDiv6"].Width = 6;
                        dgvInstruction.Columns["ColumnDelInstruction"].Width = 70;
                        dgvInstruction.Columns["ColumnViewInstruction"].Width = 70;
                        dgvInstruction.Columns["Файл"].Frozen = true;
                        dgvInstruction.Columns["Файл"].Width = 250;
                        dgvInstruction.Columns["Дата_загрузки"].Width = 120;
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

        private void FillOrg()
        {
            FillOrg(null);
        }
        private void FillOrg(int? id)
        {
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.PracticeLPOrganization
                               join org in context.Organization on x.OrganizationId equals org.Id
                               where x.PracticeLPId == _Id
                               orderby org.SPbGU descending, org.Name
                               select new
                               {
                                   Организация = org.Name,
                                   x.Id,
                                   x.OrganizationId,
                                   Начало_практики = x.DateStart,
                                   Окончание_практики = x.DateEnd,
                                   Адрес = x.OrganizationAddress,
                                   Организация_печать = x.OrganizationName,
                                   Комментарий = x.Comment,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource1.DataSource = dt;
                    dgvOrg.DataSource = bindingSource1;

                    List<string> Cols = new List<string>() { "Id", "OrganizationId" };

                    foreach (string s in Cols)
                        if (dgvOrg.Columns.Contains(s))
                            dgvOrg.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvOrg.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnDelOrg")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnEditOrg")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDiv1")
                        {
                            col.HeaderText = "";
                        }
                    }
                    try
                    {
                        dgvOrg.Columns["ColumnDiv1"].Width = 6;
                        dgvOrg.Columns["ColumnDelOrg"].Width = 70;
                        dgvOrg.Columns["ColumnEditOrg"].Width = 70;
                        dgvOrg.Columns["Организация"].Frozen = true;
                        dgvOrg.Columns["Организация"].Width = 300;
                        dgvOrg.Columns["Адрес"].Width = 400;
                        dgvOrg.Columns["Организация_печать"].Width = 400;
                        dgvOrg.Columns["Комментарий"].Width = 200;
                    }
                    catch (Exception)
                    {
                    }
                }
        }

        private void FillOrgNewList()
        {
            FillOrgNewList(null, null);
        }
        private void FillOrgNewList(int? FacId, int? RubricId)
        {
            try
            { 
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                string sqlOrg = "SELECT * FROM Organization ";
                string sqlWhere = " ";
                string sqlOrderBy = " order by SpbGU desc, Name";
                if (FacId.HasValue)
                {
                    if (RubricId.HasValue)
                    {
                        sqlWhere = "where Id in (select OrganizationId from OrganizationFaculty where FacultyId = " + FacId + "and RubricId = " + RubricId + ")";
                    }
                    else
                    {
                        sqlWhere = "where Id in (select OrganizationId from OrganizationFaculty where FacultyId = " + FacId + ")";
                    }
                }
                else
                {
                    if (RubricId.HasValue)
                    {
                        sqlWhere = "where Id in (select OrganizationId from OrganizationRubric where RubricId = " + RubricId + ")";
                    }
                    else
                    {

                    }
                }
                sqlOrg = sqlOrg + sqlWhere + sqlOrderBy;

                var OrgTable = context.Database.SqlQuery<Organization>(sqlOrg);

                var lst = (from org in OrgTable
                           select new
                           {
                               Полное_наименование = org.Name,
                               org.Id,
                               Среднее_наименование = org.MiddleName,
                               Краткое_наименование = org.ShortName,
                               Наименование_англ = org.NameEng,
                               Краткое_наименование_англ = org.ShortNameEng,
                               org.Email,
                               Телефон = org.Phone,
                               Web_сайт = org.WebSite,
                               Город = org.City,
                               Улица = org.Street,
                               Дом = org.House,
                               Помещение = org.Apartment,
                               Комментарий = org.Comment,

                           }).ToList();

                DataTable dt = new DataTable();
                dt = Utilities.ConvertToDataTable(lst);
                bindingSource2.DataSource = dt;
                dgv.DataSource = bindingSource2;

                List<string> Cols = new List<string>() { "Id" };

                foreach (string s in Cols)
                    if (dgv.Columns.Contains(s))
                        dgv.Columns[s].Visible = false;
                foreach (DataGridViewColumn col in dgv.Columns)
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
                }
                
                try
                {
                    dgv.Columns["ColumnDiv"].Width = 6;
                    dgv.Columns["Column1"].Width = 120;
                    dgv.Columns["Полное_наименование"].Frozen = true;
                    dgv.Columns["Полное_наименование"].Width = 300;
                    dgv.Columns["Среднее_наименование"].Width = 200;
                    dgv.Columns["Краткое_наименование"].Width = 150;
                    dgv.Columns["Наименование_англ"].Width = 150;
                    dgv.Columns["Краткое_наименование_англ"].Width = 150;
                }
                catch (Exception)
                {
                }
            }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось обработать данные...", "Сообщение");
            }
        }
        
        private void FillStudent()
        {
            FillStudent(null);
        }
        private void FillStudent(int? id)
        {
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.PracticeLPStudent       //context.PracticeStudent
                               //join stud in context.Student on x.StudentId equals stud.Id
                               join plporg in context.PracticeLPOrganization on x.PracticeLPOrganizationId equals plporg.Id into _plporg
                               from plporg in _plporg.DefaultIfEmpty()
                               join org in context.Organization on plporg.OrganizationId equals org.Id into _org
                               from org in _org.DefaultIfEmpty()
                               //where (x.PracticeId == _PId ) && (x.LicenseProgramId == _LPId)       
                               where x.PracticeLPId == _Id    
                               orderby x.StudentFIO //stud.LastName, stud.FirstName
                               select new
                               {
                                   /*Студент = stud.LastName + (!String.IsNullOrEmpty(stud.FirstName) ? " " + stud.FirstName : "") +
                                                (!String.IsNullOrEmpty(stud.SecondName) ? " " + stud.SecondName : ""),*/
                                   //Студент = stud.LastName + " " + stud.FirstName + " " + stud.SecondName,
                                   Студент = x.StudentFIO,
                                   x.Id,
                                   x.StudDataId,
                                   //x.StudentId,
                                   //x.OrganizationId,
                                   Дата_рожд = x.DR, 
                                   Курс = x.Course,
                                   Организация = org.Name,
                                   Начало_практики = plporg.DateStart,
                                   Окончание_практики = plporg.DateEnd,
                                   По_адресу = plporg.OrganizationAddress,
                                   Форма_обуч = x.Department,
                                   Основа_обуч = x.StudyBasis,
                                   Статус = x.StatusName,
                                   Шифр_ОП = x.ObrazProgramCrypt,
                                   Номер_УП = x.RegNomWP,
                                   Уровень = x.DegreeName,
                                   Код_направления = x.SpecNumber,
                                   Направление_подготовки = x.SpecName,
                                   Направление = x.FacultyName,
                                   Комментарий = x.Comment,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource3.DataSource = dt;
                    dgvStudent.DataSource = bindingSource3;

                    List<string> Cols = new List<string>() { "Id", "StudDataId" }; //{ "Id", "OrganizationId" };

                    foreach (string s in Cols)
                        if (dgvStudent.Columns.Contains(s))
                            dgvStudent.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvStudent.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnDelStudent")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnEditStudent")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnSetOrgStudent")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDiv3")
                        {
                            col.HeaderText = "";
                        }
                    }
                    try
                    {
                        dgvStudent.Columns["ColumnDiv3"].Width = 6;
                        dgvStudent.Columns["ColumnDelStudent"].Width = 70;
                        dgvStudent.Columns["ColumnEditStudent"].Width = 70;
                        dgvStudent.Columns["Студент"].Frozen = true;
                        dgvStudent.Columns["Студент"].Width = 200;
                        dgvStudent.Columns["Курс"].Width = 65;
                        dgvStudent.Columns["Организация"].Width = 250;
                        dgvStudent.Columns["По_адресу"].Width = 300;
                        dgvStudent.Columns["Направление_подготовки"].Width = 300;
                        dgvStudent.Columns["Направление"].Width = 200;
                        dgvStudent.Columns["Комментарий"].Width = 200;
                    }
                    catch (Exception)
                    {
                    }
                }
        }

        private void FillStudent_old(int? id)
        {
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.PracticeStudent
                               //join stud in context.Student on x.StudentId equals stud.Id
                               join plporg in context.PracticeLPOrganization on x.PracticeLPOrganizationId equals plporg.Id into _plporg
                               from plporg in _plporg.DefaultIfEmpty()
                               join org in context.Organization on plporg.OrganizationId equals org.Id into _org
                               from org in _org.DefaultIfEmpty()
                               where (x.PracticeId == _PId) && (x.LicenseProgramId == _LPId)       //(stud.LicenseProgramId == _LPId)
                               orderby x.StudentFIO //stud.LastName, stud.FirstName
                               select new
                               {
                                   /*Студент = stud.LastName + (!String.IsNullOrEmpty(stud.FirstName) ? " " + stud.FirstName : "") +
                                                (!String.IsNullOrEmpty(stud.SecondName) ? " " + stud.SecondName : ""),*/
                                   //Студент = stud.LastName + " " + stud.FirstName + " " + stud.SecondName,
                                   Студент = x.StudentFIO,
                                   x.Id,
                                   //x.StudentId,
                                   x.OrganizationId,
                                   Дата_рожд = x.DR,
                                   Курс = x.Course,
                                   Организация = org.Name,
                                   Начало_практики = plporg.DateStart,
                                   Окончание_практики = plporg.DateEnd,
                                   По_адресу = plporg.OrganizationAddress,
                                   Форма_обуч = x.Department,
                                   Основа_обуч = x.StudyBasis,
                                   Статус = x.StatusName,
                                   Уровень = x.DegreeName,
                                   Код_направления = x.SpecNumber,
                                   Направление_подготовки = x.SpecName,
                                   Направление = x.FacultyName,
                                   Комментарий = x.Comment,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource3.DataSource = dt;
                    dgvStudent.DataSource = bindingSource3;

                    List<string> Cols = new List<string>() { "Id", "OrganizationId" }; //{ "Id", "StudentId", "OrganizationId" };

                    foreach (string s in Cols)
                        if (dgvStudent.Columns.Contains(s))
                            dgvStudent.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvStudent.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnDelStudent")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnEditStudent")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnSetOrgStudent")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDiv3")
                        {
                            col.HeaderText = "";
                        }
                    }
                    try
                    {
                        dgvStudent.Columns["ColumnDiv3"].Width = 6;
                        dgvStudent.Columns["ColumnDelStudent"].Width = 70;
                        dgvStudent.Columns["ColumnEditStudent"].Width = 70;
                        dgvStudent.Columns["Студент"].Frozen = true;
                        dgvStudent.Columns["Студент"].Width = 200;
                        dgvStudent.Columns["Курс"].Width = 65;
                        dgvStudent.Columns["Организация"].Width = 250;
                        dgvStudent.Columns["По_адресу"].Width = 300;
                        dgvStudent.Columns["Направление_подготовки"].Width = 300;
                        dgvStudent.Columns["Направление"].Width = 200;
                        dgvStudent.Columns["Комментарий"].Width = 200;
                    }
                    catch (Exception)
                    {
                    }
                }
        }

        private void FillStudentNewList()
        {
            FillStudentNewList(null);
        }
        private void FillStudentNewList(int? Course)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    string sqlStudent = "SELECT * FROM StudentData ";
                    string sqlWhere = " ";
                    string sqlOrderBy = " order by FIO"; //" order by LastName, FirstName, SecondName";
                    if (Course.HasValue)
                    {
                        sqlWhere = "where LicenseProgramId = " + _LPId.ToString() + " and Course = " + Course.ToString() + " ";
                            //" and Id not in (select StudentId from PracticeStudent)" ;
                    }
                    else
                    {
                        sqlWhere = "where LicenseProgramId = " + _LPId.ToString() + " ";
                            //" and Id not in (select StudentId from PracticeStudent)"; 
                    }
                    sqlWhere = sqlWhere + " and FacultyId in (select FacultyId from Practice where Id = " + _PId +")";
                    string sqlStudentOP = " and ObrazProgramInYearId in " +
                        "(select ObrazProgramInYear.ObrazProgramInYearId from PracticeLPOP inner join ObrazProgramInYear on PracticeLPOP.ObrazProgramInYearId = ObrazProgramInYear.Id " + 
                        " where PracticeLPId = " + _Id.ToString() + ")";
                    sqlWhere = sqlWhere + (checkBoxStudentOP.Checked ? sqlStudentOP : ""); 
                    sqlStudent = sqlStudent + sqlWhere + sqlOrderBy;

                    //var StudentTable = context.Database.SqlQuery<Student>(sqlStudent);
                    var StudentTable = context.Database.SqlQuery<StudentData>(sqlStudent);

                    var lst = (from x in StudentTable
                               select new
                               {
                                   Студент = x.FIO,  //stud.LastName + " " + stud.FirstName + " " + stud.SecondName,
                                   x.Id,
                                   x.StudDataId,
                                   x.LicenseProgramId,
                                   x.FacultyId,
                                   Дата_рожд = x.DR,
                                   Курс = x.Course,
                                   Номер_УП = x.RegNomWP,
                                   Шифр_ОП = x.ObrazProgramCrypt,
                                   Форма_обуч = x.Department,
                                   Основа_обуч = x.StudyBasis,
                                   Статус = x.StatusName,
                                   Уровень = x.DegreeName,
                                   Код_направления = x.SpecNumber,
                                   Направление_подготовки = x.SpecName,
                                   Направление = x.FacultyName,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource4.DataSource = dt;
                    dgvStudentNew.DataSource = bindingSource4;

                    List<string> Cols = new List<string>() { "Id", "StudDataId", "LicenseProgramId", "FacultyId" };

                    foreach (string s in Cols)
                        if (dgvStudentNew.Columns.Contains(s))
                            dgvStudentNew.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvStudentNew.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnAddStudent")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDiv4")
                        {
                            col.HeaderText = "";
                        }
                    }

                    try
                    {
                        dgvStudentNew.Columns["ColumnDiv4"].Width = 6;
                        dgvStudentNew.Columns["ColumnAddStudent"].Width = 120;
                        dgvStudentNew.Columns["Студент"].Frozen = true;
                        dgvStudentNew.Columns["Студент"].Width = 200;
                        dgvStudentNew.Columns["Курс"].Width = 60;
                        dgvStudentNew.Columns["Направление_подготовки"].Width = 300;
                        dgvStudentNew.Columns["Направление"].Width = 200;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось обработать данные...", "Сообщение");
            }
        }

        private void FillStudentNewList_old(int? Course)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //string sqlStudent = "SELECT * FROM Student ";
                    string sqlStudent = "SELECT * FROM StudentData ";
                    string sqlWhere = " ";
                    string sqlOrderBy = " order by FIO"; //" order by LastName, FirstName, SecondName";
                    if (Course.HasValue)
                    {
                        sqlWhere = "where LicenseProgramId = " + _LPId.ToString() + " and Course = " + Course.ToString() + " ";
                        //" and Id not in (select StudentId from PracticeStudent)" ;
                    }
                    else
                    {
                        sqlWhere = "where LicenseProgramId = " + _LPId.ToString() + " ";
                        //" and Id not in (select StudentId from PracticeStudent)"; 
                    }
                    sqlWhere = sqlWhere + " and FacultyId in (select FacultyId from Practice where Id = " + _PId + ")";
                    sqlStudent = sqlStudent + sqlWhere + sqlOrderBy;

                    //var StudentTable = context.Database.SqlQuery<Student>(sqlStudent);
                    var StudentTable = context.Database.SqlQuery<StudentData>(sqlStudent);

                    var lst = (from x in StudentTable
                               select new
                               {
                                   Студент = x.FIO,  //stud.LastName + " " + stud.FirstName + " " + stud.SecondName,
                                   x.Id,
                                   x.LicenseProgramId,
                                   x.FacultyId,
                                   Дата_рожд = x.DR,
                                   Курс = x.Course,
                                   Форма_обуч = x.Department,
                                   Основа_обуч = x.StudyBasis,
                                   Статус = x.StatusName,
                                   Уровень = x.DegreeName,
                                   Код_направления = x.SpecNumber,
                                   Направление_подготовки = x.SpecName,
                                   Направление = x.FacultyName,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource4.DataSource = dt;
                    dgvStudentNew.DataSource = bindingSource4;

                    List<string> Cols = new List<string>() { "Id", "LicenseProgramId", "FacultyId" };

                    foreach (string s in Cols)
                        if (dgvStudentNew.Columns.Contains(s))
                            dgvStudentNew.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvStudentNew.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnAddStudent")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDiv4")
                        {
                            col.HeaderText = "";
                        }
                    }

                    try
                    {
                        dgvStudentNew.Columns["ColumnDiv4"].Width = 6;
                        dgvStudentNew.Columns["ColumnAddStudent"].Width = 120;
                        dgvStudentNew.Columns["Студент"].Frozen = true;
                        dgvStudentNew.Columns["Студент"].Width = 200;
                        dgvStudentNew.Columns["Направление_подготовки"].Width = 300;
                        dgvStudentNew.Columns["Направление"].Width = 200;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось обработать данные...", "Сообщение");
            }
        }

        private void btnAddOP_Click(object sender, EventArgs e)
        {
            int? OPId = ComboServ.GetComboIdInt(cbOP);
            if (!OPId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Инфо");
                cbOP.DroppedDown = true;
                return;
            }
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //Проверка наличия выбранной ОП
                    var lst = (from x in context.PracticeLPOP
                               where x.PracticeLPId == _Id
                               && x.ObrazProgramInYearId == OPId         //x.ObrazProgramId == OPId
                               select new
                               {
                                   LPOPId = x.Id,
                               }).ToList().Count();
                               
                    if (lst > 0)
                    {
                        MessageBox.Show("Такая образовательная программа уже добавлена", "Инфо");
                        return;
                    }
                    //Проверка наличия хотя бы одной ОП
                    var count = context.PracticeLPOP.Where(x => x.PracticeLPId == _Id).Count();
                    if (count > 0)
                    {
                        MessageBox.Show("В списке уже есть одна образовательная программа\r\n" + "В настоящей версии программы " +
                            "приказ и распоряжение формируются \r\n" + "по одной образовательной программе.\r\n" + "Если образовательных программ несколько, \r\n" +
                            "необходимо приказ и распоряжение сформировать\r\n" + "по каждой из них отдельно.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lp = context.ObrazProgramInYear.Where(x => x.Id == OPId).First();

                    PracticeLPOP p = new PracticeLPOP();
                    p.PracticeLPId = (int)_Id;
                    //p.ObrazProgramId = (int)OPId;
                    p.ObrazProgramId = lp.ObrazProgramId;
                    p.ObrazProgramInYearId = (int)OPId;
                    context.PracticeLPOP.Add(p);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось добавить запись...\r\n" + ex.Message, "Сообщение");
                return;
            }
            FillOP();
        }

        private void btnDelOP_Click(object sender, EventArgs e)
        {
            if (dgvOP.CurrentCell != null)
                if (dgvOP.CurrentRow.Index >= 0)
                {
                    int id = int.Parse(dgvOP.CurrentRow.Cells["Id"].Value.ToString());
                    string sOP = "";
                    try
                    {
                        sOP = dgvOP.CurrentRow.Cells["Образовательная_программа"].Value.ToString();
                    }
                    catch (Exception)
                    {
                    }
                    if (MessageBox.Show("Удалить образовательную программу? \r\n" + sOP, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                context.PracticeLPOP.RemoveRange(context.PracticeLPOP.Where(x => x.Id == id));
                                context.SaveChanges();
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Не удалось удалить запись...", "Сообщение");
                        }
                        FillOP();
                    }
                    else
                        return;
                }
        }

        private void btnDelPractice_Click(object sender, EventArgs e)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                string PracticeName = "";
                try
                {
                    PracticeName = tbPracticeFaculty.Text + "\r\n" + tbLP.Text;
                }
                catch (Exception)
                {
                }
                if (MessageBox.Show("Удалить практику? \r\n" + PracticeName, "Запрос на подтверждение", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                    return;
                try
                {
                    context.PracticeLP.Remove(context.PracticeLP.Where(x => x.Id == _Id).First());
                    context.SaveChanges();

                    if (_hndl != null)
                        if (_PId.HasValue)
                            _hndl(_PId);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось удалить практику...\r\n" + "Обычно это связано с наличием связанных записей в других таблицах.\r\n" +
                        "Примечание: полностью сформированная практика, \r\n" + "содержащая список организаций и студентов, \r\n" + "удаляется в следующей последовательности:\r\n" + 
                        "1. Сначала удаляются все студенты из практики\r\n" + "2. Затем удаляются все организации из практики\r\n" +
                        "3. Далее удаляются все загруженные документы по этой практике\r\n" + "(приказы и распоряжения)\r\n" +
                        "4. После этого используется кнопка «Удалить практику»\r\n" + "для окончательного удаления.", "Сообщение");
                }

            }
        }

        private bool CheckFields()
        {
            DateTime res;
            if (!String.IsNullOrEmpty(DateStart))
            {
                if (!DateTime.TryParse(DateStart, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Начало практики' \r\n" + "Образец: 01.12.2016", "Инфо");
                    return false;
                }
            }
            if (!String.IsNullOrEmpty(DateEnd))
            {
                if (!DateTime.TryParse(DateEnd, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Окончание практики' \r\n" + "Образец: 01.12.2016", "Инфо");
                    return false;
                }
            }
            if (!String.IsNullOrEmpty(OrderDate))
            {
                if (!DateTime.TryParse(OrderDate, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Дата приказа' \r\n" + "Образец: 01.12.2016", "Инфо");
                    tabControl1.SelectedTab = tabPage1;
                    return false;
                }
            }
            if (!String.IsNullOrEmpty(InstructionDate))
            {
                if (!DateTime.TryParse(InstructionDate, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Дата распоряжения' \r\n" + "Образец: 01.12.2016", "Инфо");
                    tabControl1.SelectedTab = tabPage2;
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
            {
                return;
            }
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var plp = context.PracticeLP.Where(x => x.Id == _Id).First();
                    
                    plp.PracticeTypeId = PracticeTypeId;
                    plp.OutSPb = checkBoxOutSPb.Checked ? true : false;   
                    
                    if (!String.IsNullOrEmpty(DateStart))
                    { 
                        plp.DateStart = DateTime.Parse(DateStart);
                    }
                    else
                    {
                        plp.DateStart = null;
                    }
                    if (!String.IsNullOrEmpty(DateEnd))
                    {
                        plp.DateEnd = DateTime.Parse(DateEnd);
                    }
                    else
                    {
                        plp.DateEnd = null;
                    }

                    plp.OrderDoc = OrderDoc;
                    plp.OrderNumber = OrderNumber;
                    if (!String.IsNullOrEmpty(OrderDate))
                    {
                        plp.OrderDate = DateTime.Parse(OrderDate);
                    }
                    else
                    {
                        plp.OrderDate = null;
                    }
                    plp.OrderAuthor = OrderAuthor;

                    plp.InstructionDoc = InstructionDoc;
                    plp.InstructionNumber = InstructionNumber;
                    if (!String.IsNullOrEmpty(InstructionDate))
                    {
                        plp.InstructionDate = DateTime.Parse(InstructionDate);
                    }
                    else
                    {
                        plp.InstructionDate = null;
                    }
                    plp.InstructionAuthor = InstructionAuthor;

                    plp.Supervisor = Supervisor;
                    plp.AdvanceHolder = AdvanceHolder;

                    context.SaveChanges();

                    if (_hndl != null)
                        if (_PId.HasValue)
                        _hndl(_PId);

                    MessageBox.Show("Данные сохранены", "Сообщение");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + ex.Message, "Сообщение");
            }
        }
        private bool CheckOrderData()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.PracticeLPOP.Where(x => x.PracticeLPId == _Id).Count();
                    if (lst > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return true;
            }
        }
        private void btnMakeOrder_Click(object sender, EventArgs e)
        {
            //Проверка наличия выбранной ОП
            if (!CheckOrderData())
            {
                if (MessageBox.Show("Не выбрана образовательная программа\r\n" + "В этом случае придется самостоятельно ввести в приказе \r\n" + "образовательную программу и профили \r\n" +
                    "Продолжить тем не менее?", "Запрос на подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
            ToDOC();
        }

        public void ToDOC()
        {
            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = ""; 

            if (!checkBoxOutSPb.Checked)
            {
                templatename = "Приказ_практика";
            }
            else
            {
                templatename = "Приказ_практика_выезд";
            }

            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var template = (from x in context.Templates
                                    where x.TemplateName == templatename
                                    select x).First();

                    fileByteArray = (byte[])template.FileData;
                    type = (string)template.FileType.Trim();
                    name = (string)template.FileName.Trim();
                    nameshort = name.Substring(0, name.Length - type.Length);
                }
            }
            catch (Exception exc)
            {
                if (Util.IsDBOwner())
                {
                    MessageBox.Show("Не удалось получить данные...\r\n" + exc.Message, "Сообщение");
                }
                else
                {
                    MessageBox.Show("Не удалось получить данные...", "Сообщение");
                }
                return;
            }
            string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EmployerPartners_TempFiles\";
            try
            {
                if (!Directory.Exists(TempFilesFolder))
                    Directory.CreateDirectory(TempFilesFolder);
            }
            catch (Exception ex)
            {
                if (Util.IsDBOwner())
                {
                    MessageBox.Show("Не удалось создать директорию.\r\n" + ex.Message, "Сообщение");
                }
                else
                {
                    MessageBox.Show("Не удалось открыть файл...", "Сообщение");
                }
                return;
            }

            string filePath = TempFilesFolder + name;
            string[] fileList = Directory.GetFiles(TempFilesFolder, nameshort + "*" + type);
            int suffix;
            Random rnd = new Random();
            suffix = rnd.Next();
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    foreach (string f in fileList)
                    {
                        File.Delete(f);
                    }
                }
                catch (Exception)
                {
                    filePath = TempFilesFolder + nameshort + " " + suffix + type;
                }
            }
            //Запись на диск
            try
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter binWriter = new BinaryWriter(fileStream);
                binWriter.Write(fileByteArray);
                binWriter.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось открыть файл...", "Сообщение");
                return;
            }

            //заполнение шаблона
            try
            {
                WordDoc wd = new WordDoc(string.Format(filePath), true);

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    string LPName;
                    string LPCode;
                    var lp = (from x in context.LicenseProgram
                              where x.Id == _LPId
                              select x).First();
                    LPName = lp.Name;
                    LPCode = lp.Code;

                    wd.SetFields("LPName", LPName);
                    wd.SetFields("LPCode", LPCode);
                    //FieldDoc field = wd.Fields["LPName"];
                    //field.Text = LPName;

                    string OPName;
                    string OPCrypt;
                    var opn = (from x in context.PracticeLPOP
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               join opinyear in  context.ObrazProgramInYear on x.ObrazProgramInYearId equals opinyear.Id
                               where x.PracticeLPId == _Id
                               select new
                               {
                                   //OP = " " + op.Number + "  " + op.Name,
                                   op.Name,
                                   opinyear.ObrazProgramCrypt,
                               }).ToList();
                    if (opn.Count > 0)
                    {
                        OPName = opn.First().Name;
                        wd.SetFields("OPName", OPName);
                        OPCrypt = (String.IsNullOrEmpty(opn.First().ObrazProgramCrypt)) ? "_____________" : opn.First().ObrazProgramCrypt;
                        wd.SetFields("OPCrypt", OPCrypt);
                        
                    }

                    string Profile = "";
                    var opp = (from x in context.PracticeLPOP
                               join opinyear in context.ObrazProgramInYear on x.ObrazProgramInYearId equals opinyear.Id
                               join popinyear in context.ProfileInObrazProgramInYear on opinyear.ObrazProgramInYearId equals popinyear.ObrazProgramInYearId
                               join profile in context.Profile on popinyear.ProfileId equals profile.Id
                               where x.PracticeLPId == _Id
                               select new
                               {
                                   profile.Name
                               }).ToList();

                    int k = 0;
                    if (opn.Count > 0)
                    {
                        foreach (var item in opp)
                        {
                            k++;
                            Profile += (!String.IsNullOrEmpty(item.Name)) ? ((k > 1) ? ", " : "") + " \"" + item.Name.ToString() + "\"" : "";
                        }
                    }

                    Profile = (Profile == "") ? "Профиль: ______________" : ((k > 1) ? "Профили: " : "Профиль: ") + Profile;

                    wd.SetFields("Profile", Profile);

                    string PracticeType;
                    var ptype = (from x in context.PracticeLP
                                 join pt in context.PracticeType on x.PracticeTypeId equals pt.Id into _pt
                                 from pt in _pt.DefaultIfEmpty()
                                 where x.Id == _Id
                                 select new 
                                 { 
                                    PTName=pt.AccName,
                                 }).First();
                    PracticeType = (!String.IsNullOrEmpty(ptype.PTName)) ? ptype.PTName : "______________";

                    wd.SetFields("PracticeType", PracticeType);

                    string DateStart_ ="";
                    string DateEnd_ = "";
                    string Supervisor_ = "";
                    string AdvanceHolder_ = "";
                    var plp = (from y in context.PracticeLP
                               where y.Id == _Id
                               select y).First();
                    DateStart_ = (plp.DateStart.HasValue) ? plp.DateStart.Value.Date.ToString("dd.MM.yyyy") : "";
                    DateEnd_ = (plp.DateEnd.HasValue) ? plp.DateEnd.Value.Date.ToString("dd.MM.yyyy") : "";
                    Supervisor_ = plp.Supervisor;
                    AdvanceHolder_ = plp.AdvanceHolder;
                    
                    wd.SetFields("DateStart", DateStart_);
                    wd.SetFields("DateEnd", DateEnd_);
                    wd.SetFields("Supervisor", Supervisor_);
                    if (checkBoxOutSPb.Checked && templatename == "Приказ_практика_выезд")
                    {
                        wd.SetFields("AdvanceHolder", AdvanceHolder_);
                    }

                    string OrgList = "";
                    var orglist = (from x in context.PracticeLPOrganization
                               join prlp in context.PracticeLP on x.PracticeLPId equals prlp.Id 
                               join org in context.Organization on x.OrganizationId equals org.Id
                               where x.PracticeLPId == _Id
                               orderby org.SPbGU descending
                               select new
                               {
                                   org.Name,
                                   x.OrganizationId,
                                   prlp.DateStart,
                                   prlp.DateEnd,
                                   OrgDateStart = x.DateStart,
                                   OrgDateEnd = x.DateEnd,
                                   x.OrganizationAddress,
                                   x.OrganizationName,
                               }).ToList();
                    int i = 0;
                    foreach (var item in orglist)
                    {
                        i++;
                        if (((item.DateStart == item.OrgDateStart) && (item.DateEnd == item.OrgDateEnd)) || ((item.OrgDateStart == null) || (item.OrgDateEnd == null)))
                        {
                            OrgList = OrgList + "1." + i + "." + item.Name + " по адресу: " + item.OrganizationAddress + "; \r\n";
                        }
                        else
                        {
                            OrgList = OrgList + "1." + i + "." + " на период с " +
                                    ((item.OrgDateStart.HasValue) ? item.OrgDateStart.Value.Date.ToString("dd.MM.yyyy") : "") + " по " +
                                    ((item.OrgDateEnd.HasValue) ? item.OrgDateEnd.Value.Date.ToString("dd.MM.yyyy") : "") + " " + item.Name + " по адресу: " + item.OrganizationAddress + "; \r\n";
                        }
                    }
                    wd.SetFields("OrgList", OrgList);
                }
            }
            catch (WordException we)
            {
                MessageBox.Show(we.Message);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message); ;
            }
        }

        private void btnAddOrg_Click(object sender, EventArgs e)
        {
            try
            {
                FillCombo();
                FillOrgNewList(null, null);
                dgv.Visible = !dgv.Visible;
                lbl_dgv.Visible = !lbl_dgv.Visible;
                cbRubric.Visible = !cbRubric.Visible;
                lbl_cbRubric.Visible = !lbl_cbRubric.Visible;
                cbFaculty.Visible = !cbFaculty.Visible;
                lbl_cbFaculty.Visible = !lbl_cbFaculty.Visible;
                tbSearch.Visible = !tbSearch.Visible;
                lbl_tbSearch.Visible = !lbl_tbSearch.Visible;
                bindingNavigator2.Visible = !bindingNavigator2.Visible;
                btnAddOrg.Text = (dgv.Visible) ? "Убрать добавление" : "Добавить организации";
                //new PracticeOrg().Show();
            }
            catch (Exception)
            {
                dgv.Visible = false;
                lbl_dgv.Visible = false;
                cbRubric.Visible = false;
                lbl_cbRubric.Visible = false;
                cbFaculty.Visible = false;
                lbl_cbFaculty.Visible = false;
                tbSearch.Visible = false;
                lbl_tbSearch.Visible = false;
                bindingNavigator2.Visible = false;
                btnAddOrg.Text = "Добавить организации";
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentRow.Index >= 0)
                {
                    if (dgv.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            dgv.CurrentCell = dgv.CurrentRow.Cells["Полное_наименование"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        try
                        {
                            if (DateStart == "" || DateEnd == "")
                            {
                                //MessageBox.Show("Не введены данные в поля 'Начало практики' и 'Окончание практики'", "Сообщение");
                                if (MessageBox.Show("Не введены данные в поля 'Начало практики' и 'Окончание практики' \r\n" +
                                                    "В этом случае придется вводить эти данные для каждой новой организации отдельно \r\n" +
                                                    "(после ввода дат не забудьте нажать кнопку 'Сохранить')\r\n" +
                                                    "Продолжить тем не менее? ",
                                                    "Запрос на подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                                { return; }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        //проверка наличия организации в списке на практику
                        try
                        {
                            int Orgid = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                            string OrgName = (dgv.CurrentRow.Cells["Полное_наименование"].Value != null) ? dgv.CurrentRow.Cells["Полное_наименование"].Value.ToString() : "";
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                var lst = (from x in context.PracticeLPOrganization
                                           where (x.PracticeLPId == _Id) && (x.OrganizationId == Orgid)
                                           select new
                                           {
                                               PrLPOrgId = x.Id,
                                           }).ToList().Count();
                                if (lst > 0)
                                {
                                    if (MessageBox.Show("Организация \r\n" + OrgName + "\r\n" +
                                        "уже находится в списке на практику. \r\n" +
                                        "Повторное включение организации в список производится \r\n" +
                                        "в случае, когда в рамках одной практики организация \r\n" +
                                        "принимает студентов по разным адресам.",
                                        "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                                    { return; }
                                }
                            }
                        }
                        catch (Exception ec)
                        {
                            //MessageBox.Show(ec.Message);
                        }
                        //собственно добавление
                        try
                        {
                            int Orgid = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                            string OrgName = (dgv.CurrentRow.Cells["Полное_наименование"].Value != null) ? dgv.CurrentRow.Cells["Полное_наименование"].Value.ToString() : "";
                            string OrgAddress = (dgv.CurrentRow.Cells["Город"].Value != null) ? dgv.CurrentRow.Cells["Город"].Value.ToString() : "";
                            OrgAddress += (dgv.CurrentRow.Cells["Улица"].Value != null) ? ((OrgAddress == "") ? dgv.CurrentRow.Cells["Улица"].Value.ToString() : ", " + dgv.CurrentRow.Cells["Улица"].Value.ToString()) : "";
                            OrgAddress += (dgv.CurrentRow.Cells["Дом"].Value != null) ? ((OrgAddress == "") ? dgv.CurrentRow.Cells["Дом"].Value.ToString() : ", " + dgv.CurrentRow.Cells["Дом"].Value.ToString()) : "";
                            OrgAddress += (dgv.CurrentRow.Cells["Помещение"].Value != null) ? ((OrgAddress == "") ? dgv.CurrentRow.Cells["Помещение"].Value.ToString() : ", " + dgv.CurrentRow.Cells["Помещение"].Value.ToString()) : "";
                            DateTime res;

                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                PracticeLPOrganization org = new PracticeLPOrganization();
                                org.OrganizationId = Orgid;
                                org.PracticeLPId = (int)_Id;
                                org.OrganizationName = OrgName;
                                org.OrganizationAddress = OrgAddress;
                                if (!String.IsNullOrEmpty(DateStart))
                                    if (DateTime.TryParse(DateStart, out res))
                                        org.DateStart = DateTime.Parse(DateStart);
                                if (!String.IsNullOrEmpty(DateEnd))
                                    if (DateTime.TryParse(DateEnd, out res))
                                        org.DateEnd = DateTime.Parse(DateEnd);

                                context.PracticeLPOrganization.Add(org);
                                context.SaveChanges();
                                FillOrg();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                    if (dgv.CurrentCell.ColumnIndex == 2)
                    {
                        try
                        {
                            int Orgid = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                            new CardOrganization(Orgid, null).Show();
                        }
                        catch
                        {
                        }
                    }
                }
        }

        private void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillOrgNewList(FacId, RubricId);
        }

        private void cbRubric_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillOrgNewList(FacId, RubricId);
        }

        private void dgvOrg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOrg.CurrentCell != null)
                if (dgvOrg.CurrentRow.Index >= 0)
                {
                    if (dgvOrg.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            dgvOrg.CurrentCell = dgvOrg.CurrentRow.Cells["Организация"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        int id;
                        try
                        {
                            id = int.Parse(dgvOrg.CurrentRow.Cells["Id"].Value.ToString());
                        }
                        catch (Exception)
                        {
                            return;
                        }
                        string OrgName = "";
                        try
                        {
                            OrgName = dgvOrg.CurrentRow.Cells["Организация"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        if (MessageBox.Show("Удалить выбранную организацию из списка? \r\n" + OrgName, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            try
                            {
                                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                                {
                                    context.PracticeLPOrganization.RemoveRange(context.PracticeLPOrganization.Where(x => x.Id == id));
                                    context.SaveChanges();
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Не удалось удалить запись...\r\n" + "Обычно это связано с наличием связанных записей в других таблицах.", "Сообщение");
                            }
                            FillOrg();
                            return;
                        }
                        else
                            return;
                    }
                    if (dgvOrg.CurrentCell.ColumnIndex == 2)
                    {
                        try
                        {
                            dgvOrg.CurrentCell = dgvOrg.CurrentRow.Cells["Организация"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        try
                        {
                            int id = int.Parse(dgvOrg.CurrentRow.Cells["Id"].Value.ToString());
                            int orgid = int.Parse(dgvOrg.CurrentRow.Cells["OrganizationId"].Value.ToString());
                            new PracticeOrgCard(id, orgid, new UpdateVoidHandler(FillOrg)).Show();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
        }

        private void btnOrgEdit_Click(object sender, EventArgs e)
        {
            if (dgvOrg.CurrentCell != null)
                if (dgvOrg.CurrentCell.RowIndex >= 0)
                {
                    int id = int.Parse(dgvOrg.CurrentRow.Cells["Id"].Value.ToString());
                    int orgid = int.Parse(dgvOrg.CurrentRow.Cells["OrganizationId"].Value.ToString());
                    new PracticeOrgCard(id, orgid, new UpdateVoidHandler(FillOrg)).Show();
                }
        }

        private void btnMakeInstruction_Click(object sender, EventArgs e)
        {
            //Проверка наличия выбранной ОП
            if (!CheckOrderData())
            {
                if (MessageBox.Show("Не выбрана образовательная программа\r\n" + "В этом случае придется самостоятельно ввести в распоряжении \r\n" + "образовательную программу и профили \r\n" +
                    "Продолжить тем не менее?", "Запрос на подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
            //проверка установки организаций для всех студентов
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.PracticeLPStudent
                               where (x.PracticeLPId == _Id) && (x.PracticeLPOrganizationId == null)
                               select x.Id).Count();
                    if (lst > 0)
                    {
                        if (MessageBox.Show("Не для всех студентов установлена организация.\r\n" +
                            "Такие студенты не попадут в распоряжение.\r\n " +
                            "Продолжить тем не менее?", "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                            return;
                    }
                }
            }
            catch (Exception)
            {
            }

            ToDOCInstruction();
        }
        public void ToDOCInstruction()
        {
            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "Распоряжение_практика";

            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var template = (from x in context.Templates
                                    where x.TemplateName == templatename
                                    select x).First();

                    fileByteArray = (byte[])template.FileData;
                    type = (string)template.FileType.Trim();
                    name = (string)template.FileName.Trim();
                    nameshort = name.Substring(0, name.Length - type.Length);
                }
            }
            catch (Exception exc)
            {
                if (Util.IsDBOwner())
                {
                    MessageBox.Show("Не удалось получить данные...\r\n" + exc.Message, "Сообщение");
                }
                else
                {
                    MessageBox.Show("Не удалось получить данные...", "Сообщение");
                }
                return;
            }
            string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EmployerPartners_TempFiles\";
            try
            {
                if (!Directory.Exists(TempFilesFolder))
                    Directory.CreateDirectory(TempFilesFolder);
            }
            catch (Exception ex)
            {
                if (Util.IsDBOwner())
                {
                    MessageBox.Show("Не удалось создать директорию.\r\n" + ex.Message, "Сообщение");
                }
                else
                {
                    MessageBox.Show("Не удалось открыть файл...", "Сообщение");
                }
                return;
            }

            string filePath = TempFilesFolder + name;
            string[] fileList = Directory.GetFiles(TempFilesFolder, nameshort + "*" + type);
            int suffix;
            Random rnd = new Random();
            suffix = rnd.Next();
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    foreach (string f in fileList)
                    {
                        File.Delete(f);
                    }
                }
                catch (Exception)
                {
                    filePath = TempFilesFolder + nameshort + " " + suffix + type;
                }
            }
            //Запись на диск
            try
            {
                FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                BinaryWriter binWriter = new BinaryWriter(fileStream);
                binWriter.Write(fileByteArray);
                binWriter.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось открыть файл...", "Сообщение");
                return;
            }

            //заполнение шаблона
            try
            {
                WordDoc wd = new WordDoc(string.Format(filePath), true);

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    string LPName;
                    string LPCode;
                    var lp = (from x in context.LicenseProgram
                              where x.Id == _LPId
                              select x).First();
                    LPName = lp.Name;
                    LPCode = lp.Code;

                    wd.SetFields("LPName", LPName);
                    wd.SetFields("LPCode", LPCode);

                    string OPName;
                    string OPCrypt;
                    var opn = (from x in context.PracticeLPOP
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               join opinyear in context.ObrazProgramInYear on x.ObrazProgramInYearId equals opinyear.Id
                               where x.PracticeLPId == _Id
                               select new
                               {
                                   //OP = " " + op.Number + "  " + op.Name,
                                   op.Name,
                                   opinyear.ObrazProgramCrypt,
                               }).ToList();
                    if (opn.Count > 0)
                    {
                        OPName = opn.First().Name;
                        wd.SetFields("OPName", OPName);
                        OPCrypt = (String.IsNullOrEmpty(opn.First().ObrazProgramCrypt)) ? "________" : opn.First().ObrazProgramCrypt;
                        wd.SetFields("OPCrypt", OPCrypt);
                    }

                    string Profile = "";
                    var opp = (from x in context.PracticeLPOP
                               join opinyear in context.ObrazProgramInYear on x.ObrazProgramInYearId equals opinyear.Id
                               join popinyear in context.ProfileInObrazProgramInYear on opinyear.ObrazProgramInYearId equals popinyear.ObrazProgramInYearId
                               join profile in context.Profile on popinyear.ProfileId equals profile.Id
                               where x.PracticeLPId == _Id
                               select new
                               {
                                   profile.Name
                               }).ToList();

                    int k = 0;
                    if (opn.Count > 0)
                    {
                        foreach (var item in opp)
                        {
                            k++;
                            Profile += (!String.IsNullOrEmpty(item.Name)) ? ((k > 1) ? ", " : "") + " \"" + item.Name.ToString() + "\"" : "";
                        }
                    }

                    Profile = (Profile == "") ? "Профиль: ______________" : ((k > 1) ? "Профили: " : "Профиль: ") + Profile;

                    wd.SetFields("Profile", Profile);

                    string PracticeType;
                    var ptype = (from x in context.PracticeLP
                                 join pt in context.PracticeType on x.PracticeTypeId equals pt.Id into _pt
                                 from pt in _pt.DefaultIfEmpty()
                                 where x.Id == _Id
                                 select new
                                 {
                                     PTName = pt.RodName,
                                 }).First();
                    PracticeType = (!String.IsNullOrEmpty(ptype.PTName)) ? ptype.PTName : "___________________";

                    wd.SetFields("PracticeType", PracticeType);

                    string DateStart_ = "";
                    string DateEnd_ = "";
                    string Supervisor_ = "";
                    string AdvanceHolder_ = "";
                    var plp = (from y in context.PracticeLP
                               where y.Id == _Id
                               select y).First();
                    DateStart_ = (plp.DateStart.HasValue) ? plp.DateStart.Value.Date.ToString("dd.MM.yyyy") : "";
                    DateEnd_ = (plp.DateEnd.HasValue) ? plp.DateEnd.Value.Date.ToString("dd.MM.yyyy") : "";
                    Supervisor_ = plp.Supervisor;
                    AdvanceHolder_ = plp.AdvanceHolder;

                    wd.SetFields("DateStart", DateStart_);
                    wd.SetFields("DateEnd", DateEnd_);

                    string OrgStudentList = "";
                    var orgstudentlist = (from x in context.PracticeLPStudent            //context.PracticeStudent
                                          //join stud in context.Student on x.StudentId equals stud.Id
                                          join plporg in context.PracticeLPOrganization on x.PracticeLPOrganizationId equals plporg.Id 
                                          join prlp in context.PracticeLP on plporg.PracticeLPId equals prlp.Id 
                                          join org in context.Organization on plporg.OrganizationId equals org.Id 
                                          where plporg.PracticeLPId == _Id
                                          orderby org.SPbGU descending, org.Name, plporg.OrganizationAddress, x.StudentFIO //stud.LastName, stud.FirstName
                                          select new
                                          {
                                              //FIO = stud.LastName + " " + stud.FirstName + " " + stud.SecondName,
                                              FIO = x.StudentFIO,
                                              prlp.DateStart,
                                              prlp.DateEnd,
                                              OrgDateStart = plporg.DateStart,
                                              OrgDateEnd = plporg.DateEnd,
                                              LPOrgId = plporg.Id,
                                              plporg.OrganizationAddress,
                                              plporg.OrganizationName,
                                          }).ToList();
                    int LPOrgId = 0;
                    int i = 0;
                    int j = 0;
                    foreach (var item in orgstudentlist)
                    {
                        if (item.LPOrgId != LPOrgId)
                        {
                            LPOrgId = item.LPOrgId;
                            i++;
                            j = 0;
                            if (((item.DateStart == item.OrgDateStart) && (item.DateEnd == item.OrgDateEnd)) || ((item.OrgDateStart == null) || (item.OrgDateEnd == null)))
                            {
                                OrgStudentList = OrgStudentList + "\r\n" + "1." + i + "." + " в " + item.OrganizationName + " по адресу: " + item.OrganizationAddress + ":\r\n";
                            }
                            else
                            {
                                OrgStudentList = OrgStudentList + "\r\n" + "1." + i + "." + " на период с " +
                                    ((item.OrgDateStart.HasValue) ? item.OrgDateStart.Value.Date.ToString("dd.MM.yyyy") : "") +" по " + 
                                    ((item.OrgDateEnd.HasValue) ? item.OrgDateEnd.Value.Date.ToString("dd.MM.yyyy") : "") + 
                                    " в " + item.OrganizationName + " по адресу: " + item.OrganizationAddress + ":\r\n";
                            }
                        }
                        j++;
                        OrgStudentList = OrgStudentList + "1." + i + "." + j + "." + item.FIO + "\r\n";
                        
                    }
                    wd.SetFields("OrgStudentList", OrgStudentList);
                }
            }
            catch (WordException we)
            {
                MessageBox.Show(we.Message);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message); ;
            }
        }

        private void btnOrgUpdate_Click(object sender, EventArgs e)
        {
            FillOrg();
        }

        private void btnStudentUpdate_Click(object sender, EventArgs e)
        {
            FillStudent();
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            try
            {
                FillComboStudent();
                FillStudentNewList(null);
                cbOrgStudent.Visible = false;
                lbl_cbOrgStudent.Visible = false;
                dgvStudentNew.Visible = !dgvStudentNew.Visible;
                lbl_dgvStudentNew.Visible = !lbl_dgvStudentNew.Visible;
                cbCourse.Visible = !cbCourse.Visible;
                lbl_cbCourse.Visible = !lbl_cbCourse.Visible;
                checkBoxStudentOP.Visible = !checkBoxStudentOP.Visible;
                btnAddAllStudentToPractice.Visible = !btnAddAllStudentToPractice.Visible;
                btnStudentNewUpdate.Visible = !btnStudentNewUpdate.Visible;
                bindingNavigator4.Visible = !bindingNavigator4.Visible;
                btnAddStudent.Text = (dgvStudentNew.Visible) ? "Убрать добавление" : "Добавить студентов";
                btnSetOrgStudent.Text = (cbOrgStudent.Visible) ? "Убрать распределение студентов по орг-циям" : "Распределение студентов по организациям";
                //new PracticeOrg().Show();
            }
            catch (Exception)
            {
                dgvStudentNew.Visible = false;
                lbl_dgvStudentNew.Visible = false;
                cbCourse.Visible = false;
                lbl_cbCourse.Visible = false;
                checkBoxStudentOP.Visible = false;
                btnAddAllStudentToPractice.Visible = false;
                btnStudentNewUpdate.Visible = false;
                bindingNavigator4.Visible = false;
                btnAddStudent.Text = "Добавить студентов";
            }
        }

        private void cbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStudentNewList(Course);
        }

        private void dgvStudentNew_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStudentNew.CurrentCell != null)
                if (dgvStudentNew.CurrentRow.Index >= 0)
                    if (dgvStudentNew.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            dgvStudentNew.CurrentCell = dgvStudentNew.CurrentRow.Cells["Студент"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        try
                        {
                            string FIO = (dgvStudentNew.CurrentRow.Cells["Студент"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Студент"].Value.ToString() : "";
                            string DR = (dgvStudentNew.CurrentRow.Cells["Дата_рожд"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Дата_рожд"].Value.ToString() : "";
                            int? StudDataId = null;
                            if (dgvStudentNew.CurrentRow.Cells["StudDataId"].Value != null)
                            {
                                StudDataId = int.Parse(dgvStudentNew.CurrentRow.Cells["StudDataId"].Value.ToString());
                            }

                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                var lst = (from x in context.PracticeLPStudent
                                           where (x.PracticeLPId == _Id) && (x.StudentFIO == FIO) && (x.DR == DR) && (x.StudDataId == StudDataId)
                                           select new
                                           {
                                               PrStId = x.Id,
                                           }).ToList().Count();
                                if (lst > 0)
                                {
                                    MessageBox.Show("Студент " + FIO + " (дата рожд. " + DR + ")" + "\r\n" + "уже находится в списке на практику ", "Инфо",
                                        MessageBoxButtons.OK,MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            //добавление студента в практику
                            string Course = (dgvStudentNew.CurrentRow.Cells["Курс"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Курс"].Value.ToString() : "";
                            string RegNomWP = (dgvStudentNew.CurrentRow.Cells["Номер_УП"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Номер_УП"].Value.ToString() : "";
                            string Department = (dgvStudentNew.CurrentRow.Cells["Форма_обуч"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Форма_обуч"].Value.ToString() : "";
                            string StudyBasis = (dgvStudentNew.CurrentRow.Cells["Основа_обуч"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Основа_обуч"].Value.ToString() : "";
                            string StatusName = (dgvStudentNew.CurrentRow.Cells["Статус"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Статус"].Value.ToString() : "";
                            string DegreeName = (dgvStudentNew.CurrentRow.Cells["Уровень"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Уровень"].Value.ToString() : "";
                            string SpecNumber = (dgvStudentNew.CurrentRow.Cells["Код_направления"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Код_направления"].Value.ToString() : "";
                            string SpecName = (dgvStudentNew.CurrentRow.Cells["Направление_подготовки"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Направление_подготовки"].Value.ToString() : "";
                            string FacultyName = (dgvStudentNew.CurrentRow.Cells["Направление"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Направление"].Value.ToString() : "";
                            string OPCrypt = (dgvStudentNew.CurrentRow.Cells["Шифр_ОП"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Шифр_ОП"].Value.ToString() : "";

                            int LicenseProgramId = int.Parse(dgvStudentNew.CurrentRow.Cells["LicenseProgramId"].Value.ToString());
                            int FacultyId = int.Parse(dgvStudentNew.CurrentRow.Cells["FacultyId"].Value.ToString());

                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                //PracticeStudent pst = new PracticeStudent();
                                PracticeLPStudent pst = new PracticeLPStudent();
                                //pst.PracticeId = (int)_PId;
                                pst.PracticeLPId = (int)_Id;
                                pst.StudDataId = StudDataId;
                                pst.StudentFIO = FIO;
                                pst.DR = DR;
                                pst.Course = Course;
                                pst.RegNomWP = RegNomWP;
                                pst.Department = Department;
                                pst.StudyBasis = StudyBasis;
                                pst.StatusName = StatusName;
                                pst.DegreeName = DegreeName;
                                pst.SpecNumber = SpecNumber;
                                pst.SpecName = SpecName;
                                pst.FacultyName = FacultyName;
                                pst.ObrazProgramCrypt = OPCrypt;
                                pst.LicenseProgramId = LicenseProgramId;
                                pst.FacultyId = FacultyId;
                                //context.PracticeStudent.Add(pst);
                                context.PracticeLPStudent.Add(pst);
                                context.SaveChanges();
                                FillStudent();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStudent.CurrentCell != null)
                if (dgvStudent.CurrentRow.Index >= 0)
                {
                    if (dgvStudent.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            dgvStudent.CurrentCell = dgvStudent.CurrentRow.Cells["Студент"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        int id;
                        try
                        {
                            id = int.Parse(dgvStudent.CurrentRow.Cells["Id"].Value.ToString());
                        }
                        catch (Exception)
                        {
                            return;   
                        }
                        
                        string StudentName = "";
                        try
                        {
                            StudentName = dgvStudent.CurrentRow.Cells["Студент"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        if (StudentDelConfirm)
                            if (MessageBox.Show("Удалить выбранного студента из списка? \r\n" + StudentName, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                            {return;}
                        if (StudentShowDelConfirmSettings)
                        {
                            StudentShowDelConfirmSettings = false;
                            if (MessageBox.Show("По умолчанию перед удалением выводится \r\n" + "запрос на подтверждение. \r\n" +
                                "Это подтверждение можно отменить. \r\n" + "При каждом новом открытии данного окна \r\n" +
                                "восстанавливается вывод запроса на подтверждение. \r\n\r\n" + "Отменить вывод предупреждаещего сообщения?", "Запрос на подтверждение",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                            { StudentDelConfirm = false; }
                        }
                            try
                            {
                                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                                {
                                    //context.PracticeStudent.RemoveRange(context.PracticeStudent.Where(x => x.Id == id));
                                    context.PracticeLPStudent.RemoveRange(context.PracticeLPStudent.Where(x => x.Id == id));
                                    context.SaveChanges();
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Не удалось удалить запись...\r\n" + "Обычно это связано с наличием связанных записей в других таблицах.", "Сообщение");
                            }
                            FillStudent();
                            if (dgvStudentNew.Visible == true)
                            {
                                FillStudentNewList();
                            }
                            return;
                        //}
                        //else
                        //    return;
                    }
                    if (dgvStudent.CurrentCell.ColumnIndex == 2)
                    {
                        try
                        {
                            dgvStudent.CurrentCell = dgvStudent.CurrentRow.Cells["Студент"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        try
                        {
                            int id = int.Parse(dgvStudent.CurrentRow.Cells["Id"].Value.ToString());
                            new PracticeStudentCard(id, new UpdateVoidHandler(FillStudent)).Show();
                        }
                        catch (Exception)
                        {
                        }
                        
                    }
                    if (dgvStudent.CurrentCell.ColumnIndex == 3)
                    {
                        try
                        {
                            dgvStudent.CurrentCell = dgvStudent.CurrentRow.Cells["Студент"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        if (cbOrgStudent.Visible == false)
                        {
                            MessageBox.Show("Не выбрана организация. \r\n" + "Воспользуйтесь кнопкой 'Распределение студентов по организациям'","Сообщение");
                            return;
                        }
                        //if (!OrgStudent.HasValue)
                        //{
                        //    MessageBox.Show("Не выбрана организация","Сообщение");
                        //    //cbOrgStudent.Focus();
                        //    return;
                        //}
                        try
                        {
                            int id = int.Parse(dgvStudent.CurrentRow.Cells["Id"].Value.ToString());
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                //var pst = context.PracticeStudent.Where(x => x.Id == id).First();
                                var pst = context.PracticeLPStudent.Where(x => x.Id == id).First();

                                //pst.OrganizationId = OrgStudent;
                                pst.PracticeLPOrganizationId = OrgStudent;
                            
                                context.SaveChanges();
                            
                                FillStudent();
                            }
                        }
                        catch (Exception ec)
                        {
                            MessageBox.Show("Не удалось обновить данные \r\n" + ec.Message, "Сообщение");
                        }
                    }
                }
        }

        private void btnSetOrgStudent_Click(object sender, EventArgs e)
        {
            try
            {
                FillComboOrgStudent();
                cbOrgStudent.Visible = !cbOrgStudent.Visible;
                lbl_cbOrgStudent.Visible = !lbl_cbOrgStudent.Visible;
                btnSetOrgStudent.Text = (cbOrgStudent.Visible) ? "Убрать распределение студентов по орг-циям" : "Распределение студентов по организациям";

                
            }
            catch (Exception)
            {
                cbOrgStudent.Visible = false;
                lbl_cbOrgStudent.Visible = false;
                btnSetOrgStudent.Text = "Распределение студентов по организациям";
            }
            finally
            {
                dgvStudentNew.Visible = false;
                lbl_dgvStudentNew.Visible = false;
                cbCourse.Visible = false;
                lbl_cbCourse.Visible = false;
                checkBoxStudentOP.Visible = false;
                btnAddAllStudentToPractice.Visible = false;
                btnStudentNewUpdate.Visible = false;
                bindingNavigator4.Visible = false;
                btnAddStudent.Text = (dgvStudentNew.Visible) ? "Убрать добавление" : "Добавить студентов";
            }
        }

        private void PracticeCard_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Parent.Width > this.Width + 30 + this.Left)
                {
                    this.Width = this.Parent.Width - 30 - this.Left;
                }
                if (this.Parent.Height > this.Height + 30 + this.Top)
                {
                    this.Height = this.Parent.Height - 30 - this.Top;
                }
            }
            catch (Exception)
            {
            }
        }

        private bool CheckOrderLoaded()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.PracticeLPFile
                               where (x.PracticeLPId == _Id) && (x.DocTypeId == 1)
                               select new
                               {
                                   x.Id,
                                   Файл = x.FileName,
                                   Дата_загрузки = x.DateLoad,
                               }).ToList();
                    if (lst.Count > 0)
                    { 
                        return true; 
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        private bool CheckInstructionLoaded()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.PracticeLPFile
                               where (x.PracticeLPId == _Id) && (x.DocTypeId == 2)
                               select new
                               {
                                   x.Id,
                                   Файл = x.FileName,
                                   Дата_загрузки = x.DateLoad,
                               }).ToList();
                    if (lst.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void btnOrderLoad_Click(object sender, EventArgs e)
        {
            if (CheckOrderLoaded())
            {
                if (MessageBox.Show("В БД уже есть загруженный приказ.\r\n" + "Если это новая версия приказа, то рекомендуется \r\n" +
                    "предыдущую версию удалить (кнопка 'Удалить')\r\n" + "Если это дополнение к приказу, можно продолжать.\r\n" +
                    "Выполнить загрузку документа?", "Запрос на подтверждение",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                {return;}
            }
            try
            {
                //Чтение двоичного файла с диска
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Application.StartupPath;
                if (openFileDialog.ShowDialog() != DialogResult.OK) { return; }
                string filePath = openFileDialog.FileName;
                //Параметры файла
                string name = Path.GetFileName(filePath);
                string type = Path.GetExtension(filePath);
                byte[] fileByteArray = File.ReadAllBytes(filePath);
                double kbSize = Math.Round(Convert.ToDouble(fileByteArray.Length) / 1024, 2);
                //int dbFileID = 1;
                //Запись в БД
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    PracticeLPFile docfile = new PracticeLPFile();
                    docfile.PracticeLPId = (int)_Id;
                    docfile.DocTypeId = 1;
                    docfile.FileName = name;
                    docfile.FileType = type;
                    docfile.FileData = fileByteArray;
                    docfile.DateLoad = DateTime.Now;
                    docfile.FileSizeKBytes = kbSize;
                    context.PracticeLPFile.Add(docfile);
                    context.SaveChanges();
                }
                MessageBox.Show("Файл успешно загружен в БД", "Сообщение");
                FillOrder();
            }
            catch (Exception ec)
            {

                if (Util.IsDBOwner())
                {
                    MessageBox.Show("Не удалось сохранить файл в БД...\r\n" + ec.Message, "Сообщение");
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить файл в БД", "Сообщение");
                }
                return;
            }
        }

        private void btnInstructionLoad_Click(object sender, EventArgs e)
        {
            if (CheckInstructionLoaded())
            {
                if (MessageBox.Show("В БД уже есть загруженный документ.\r\n" + "Если это новая версия распоряжения, то рекомендуется \r\n" +
                    "предыдущую версию удалить (кнопка 'Удалить')\r\n" + "Если это дополнение к распоряжению, можно продолжать.\r\n" +
                    "Выполнить загрузку документа?", "Запрос на подтверждение",
                     MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                { return; }
            }
            try
            {
                //Чтение двоичного файла с диска
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = Application.StartupPath;
                if (openFileDialog.ShowDialog() != DialogResult.OK) { return; }
                string filePath = openFileDialog.FileName;
                //Параметры файла
                string name = Path.GetFileName(filePath);
                string type = Path.GetExtension(filePath);
                byte[] fileByteArray = File.ReadAllBytes(filePath);
                double kbSize = Math.Round(Convert.ToDouble(fileByteArray.Length) / 1024, 2);
                //int dbFileID = 1;
                //Запись в БД
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    PracticeLPFile docfile = new PracticeLPFile();
                    docfile.PracticeLPId = (int)_Id;
                    docfile.DocTypeId = 2;
                    docfile.FileName = name;
                    docfile.FileType = type;
                    docfile.FileData = fileByteArray;
                    docfile.DateLoad = DateTime.Now;
                    docfile.FileSizeKBytes = kbSize;
                    context.PracticeLPFile.Add(docfile);
                    context.SaveChanges();
                }
                MessageBox.Show("Файл успешно загружен в БД", "Сообщение");
                FillInstruction();
            }
            catch (Exception ec)
            {

                if (Util.IsDBOwner())
                {
                    MessageBox.Show("Не удалось сохранить файл в БД...\r\n" + ec.Message, "Сообщение");
                }
                else
                {
                    MessageBox.Show("Не удалось сохранить файл в БД", "Сообщение");
                }
                return;
            }
        }

        private void dgvOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOrder.CurrentCell != null)
                if (dgvOrder.CurrentRow.Index >= 0)
                {
                    if (dgvOrder.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            dgvOrder.CurrentCell = dgvOrder.CurrentRow.Cells["Файл"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        int id;
                        try
                        {
                            id = int.Parse(dgvOrder.CurrentRow.Cells["Id"].Value.ToString());
                        }
                        catch (Exception)
                        {
                            return;
                        }
                        string OrderName = "";
                        try
                        {
                            OrderName = dgvOrder.CurrentRow.Cells["Файл"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        if (MessageBox.Show("Удалить выбранный документ из БД? \r\n" + OrderName, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            try
                            {
                                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                                {
                                    context.PracticeLPFile.RemoveRange(context.PracticeLPFile.Where(x => x.Id == id));
                                    context.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Не удалось удалить запись...\r\n" + ex.Message, "Сообщение");
                            }
                            FillOrder();
                            return;
                        }
                        else
                            return;
                    }
                    if (dgvOrder.CurrentCell.ColumnIndex == 2)
                    {
                        try
                        {
                            dgvOrder.CurrentCell = dgvOrder.CurrentRow.Cells["Файл"];
                        }
                        catch (Exception)
                        {
                        }
                        //////
                        int id;
                        try
                        {
                            id = int.Parse(dgvOrder.CurrentRow.Cells["Id"].Value.ToString());
                        }
                        catch (Exception)
                        {
                            return;
                        }
                        //извлечение файла из БД
                        byte[] fileByteArray;
                        string type;
                        string name;
                        string nameshort;

                        try
                        {
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                var orderfile = (from x in context.PracticeLPFile
                                                 where x.Id == id
                                                 select x).First();

                                fileByteArray = (byte[])orderfile.FileData;
                                type = (string)orderfile.FileType.Trim();
                                name = (string)orderfile.FileName.Trim();
                                nameshort = name.Substring(0, name.Length - type.Length);
                            }
                        }
                        catch (Exception exc)
                        {
                            if (Util.IsDBOwner())
                            {
                                MessageBox.Show("Не удалось получить данные...\r\n" + exc.Message, "Сообщение");
                            }
                            else
                            {
                                MessageBox.Show("Не удалось получить данные...", "Сообщение");
                            }
                            return;
                        }
                        string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EmployerPartners_TempFiles\";
                        try
                        {
                            if (!Directory.Exists(TempFilesFolder))
                                Directory.CreateDirectory(TempFilesFolder);
                        }
                        catch (Exception ex)
                        {
                            if (Util.IsDBOwner())
                            {
                                MessageBox.Show("Не удалось создать директорию.\r\n" + ex.Message, "Сообщение");
                            }
                            else
                            {
                                MessageBox.Show("Не удалось открыть файл...", "Сообщение");
                            }
                            return;
                        }

                        string filePath = TempFilesFolder + name;
                        string[] fileList = Directory.GetFiles(TempFilesFolder, nameshort + "*" + type);
                        int suffix;
                        Random rnd = new Random();
                        suffix = rnd.Next();
                        if (File.Exists(filePath))
                        {
                            try
                            {
                                File.Delete(filePath);
                                foreach (string f in fileList)
                                {
                                    File.Delete(f);
                                }
                            }
                            catch (Exception)
                            {
                                filePath = TempFilesFolder + nameshort + " " + suffix + type;
                            }
                        }
                        //Запись на диск
                        try
                        {
                            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                            BinaryWriter binWriter = new BinaryWriter(fileStream);
                            binWriter.Write(fileByteArray);
                            binWriter.Close();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Не удалось открыть файл...", "Сообщение");
                            return;
                        }
                        //Открыть файл
                        System.Diagnostics.Process.Start(@filePath);
                        //////
                    }
                }
        }

        private void dgvInstruction_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvInstruction.CurrentCell != null)
                if (dgvInstruction.CurrentRow.Index >= 0)
                {
                    if (dgvInstruction.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            dgvInstruction.CurrentCell = dgvInstruction.CurrentRow.Cells["Файл"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        int id;
                        try
                        {
                            id = int.Parse(dgvInstruction.CurrentRow.Cells["Id"].Value.ToString());
                        }
                        catch (Exception)
                        {
                            return;
                        }
                        string InstructionName = "";
                        try
                        {
                            InstructionName = dgvInstruction.CurrentRow.Cells["Файл"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        if (MessageBox.Show("Удалить выбранный документ из БД? \r\n" + InstructionName, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            try
                            {
                                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                                {
                                    context.PracticeLPFile.RemoveRange(context.PracticeLPFile.Where(x => x.Id == id));
                                    context.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Не удалось удалить запись...\r\n" + ex.Message, "Сообщение");
                            }
                            FillInstruction();
                            return;
                        }
                        else
                            return;
                    }
                    if (dgvInstruction.CurrentCell.ColumnIndex == 2)
                    {
                        try
                        {
                            dgvInstruction.CurrentCell = dgvInstruction.CurrentRow.Cells["Файл"];
                        }
                        catch (Exception)
                        {
                        }
                        //////
                        int id;
                        try
                        {
                            id = int.Parse(dgvInstruction.CurrentRow.Cells["Id"].Value.ToString());
                        }
                        catch (Exception)
                        {
                            return;
                        }
                        //извлечение файла из БД
                        byte[] fileByteArray;
                        string type;
                        string name;
                        string nameshort;

                        try
                        {
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                var instructionfile = (from x in context.PracticeLPFile
                                                 where x.Id == id
                                                 select x).First();

                                fileByteArray = (byte[])instructionfile.FileData;
                                type = (string)instructionfile.FileType.Trim();
                                name = (string)instructionfile.FileName.Trim();
                                nameshort = name.Substring(0, name.Length - type.Length);
                            }
                        }
                        catch (Exception exc)
                        {
                            if (Util.IsDBOwner())
                            {
                                MessageBox.Show("Не удалось получить данные...\r\n" + exc.Message, "Сообщение");
                            }
                            else
                            {
                                MessageBox.Show("Не удалось получить данные...", "Сообщение");
                            }
                            return;
                        }
                        string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EmployerPartners_TempFiles\";
                        try
                        {
                            if (!Directory.Exists(TempFilesFolder))
                                Directory.CreateDirectory(TempFilesFolder);
                        }
                        catch (Exception ex)
                        {
                            if (Util.IsDBOwner())
                            {
                                MessageBox.Show("Не удалось создать директорию.\r\n" + ex.Message, "Сообщение");
                            }
                            else
                            {
                                MessageBox.Show("Не удалось открыть файл...", "Сообщение");
                            }
                            return;
                        }

                        string filePath = TempFilesFolder + name;
                        string[] fileList = Directory.GetFiles(TempFilesFolder, nameshort + "*" + type);
                        int suffix;
                        Random rnd = new Random();
                        suffix = rnd.Next();
                        if (File.Exists(filePath))
                        {
                            try
                            {
                                File.Delete(filePath);
                                foreach (string f in fileList)
                                {
                                    File.Delete(f);
                                }
                            }
                            catch (Exception)
                            {
                                filePath = TempFilesFolder + nameshort + " " + suffix + type;
                            }
                        }
                        //Запись на диск
                        try
                        {
                            FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
                            BinaryWriter binWriter = new BinaryWriter(fileStream);
                            binWriter.Write(fileByteArray);
                            binWriter.Close();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Не удалось открыть файл...", "Сообщение");
                            return;
                        }
                        //Открыть файл
                        System.Diagnostics.Process.Start(@filePath);
                        //////
                    }
                }
        }

        private void checkBoxStudentOP_CheckedChanged(object sender, EventArgs e)
        {
            FillComboStudent();
            FillStudentNewList();
        }

        private void btnStudentNewUpdate_Click(object sender, EventArgs e)
        {
            FillComboStudent();
            FillStudentNewList();
        }

        private void btnAddAllStudentToPractice_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Добавление в практику всех студентов из справочника.\r\n" + "Будут добавлены только те студенты,\r\n" +
                "которых еще нет в списке на практику.\r\n" + "Продолжить выполнение?", "Запрос на подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            try
            {
                for (int rwInd = 0; rwInd < dgvStudentNew.Rows.Count; rwInd++)
                {
                    string FIO = (dgvStudentNew.Rows[rwInd].Cells["Студент"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Студент"].Value.ToString() : "";
                    string DR = (dgvStudentNew.Rows[rwInd].Cells["Дата_рожд"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Дата_рожд"].Value.ToString() : "";
                    int? StudDataId = null;
                    if (dgvStudentNew.Rows[rwInd].Cells["StudDataId"].Value != null)
                    {
                        StudDataId = int.Parse(dgvStudentNew.Rows[rwInd].Cells["StudDataId"].Value.ToString());
                    }

                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        var lst = (from x in context.PracticeLPStudent
                                   where (x.PracticeLPId == _Id) && (x.StudentFIO == FIO) && (x.DR == DR) && (x.StudDataId == StudDataId)
                                   select new
                                   {
                                       PrStId = x.Id,
                                   }).ToList().Count();
                        if (lst > 0)
                        {
                            //MessageBox.Show("Студент " + FIO + " (дата рожд. " + DR + ")" + "\r\n" + "уже находится в списке на практику ", "Инфо",
                            //    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            continue;
                        }
                    }
                    //добавление студента в практику
                    string Course = (dgvStudentNew.Rows[rwInd].Cells["Курс"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Курс"].Value.ToString() : "";
                    string RegNomWP = (dgvStudentNew.Rows[rwInd].Cells["Номер_УП"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Номер_УП"].Value.ToString() : "";
                    string Department = (dgvStudentNew.Rows[rwInd].Cells["Форма_обуч"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Форма_обуч"].Value.ToString() : "";
                    string StudyBasis = (dgvStudentNew.Rows[rwInd].Cells["Основа_обуч"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Основа_обуч"].Value.ToString() : "";
                    string StatusName = (dgvStudentNew.Rows[rwInd].Cells["Статус"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Статус"].Value.ToString() : "";
                    string DegreeName = (dgvStudentNew.Rows[rwInd].Cells["Уровень"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Уровень"].Value.ToString() : "";
                    string SpecNumber = (dgvStudentNew.Rows[rwInd].Cells["Код_направления"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Код_направления"].Value.ToString() : "";
                    string SpecName = (dgvStudentNew.Rows[rwInd].Cells["Направление_подготовки"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Направление_подготовки"].Value.ToString() : "";
                    string FacultyName = (dgvStudentNew.Rows[rwInd].Cells["Направление"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Направление"].Value.ToString() : "";
                    string OPCrypt = (dgvStudentNew.Rows[rwInd].Cells["Шифр_ОП"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Шифр_ОП"].Value.ToString() : "";

                    int LicenseProgramId = int.Parse(dgvStudentNew.Rows[rwInd].Cells["LicenseProgramId"].Value.ToString());
                    int FacultyId = int.Parse(dgvStudentNew.Rows[rwInd].Cells["FacultyId"].Value.ToString());

                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        PracticeLPStudent pst = new PracticeLPStudent();
                        pst.PracticeLPId = (int)_Id;
                        pst.StudDataId = StudDataId;
                        pst.StudentFIO = FIO;
                        pst.DR = DR;
                        pst.Course = Course;
                        pst.RegNomWP = RegNomWP;
                        pst.Department = Department;
                        pst.StudyBasis = StudyBasis;
                        pst.StatusName = StatusName;
                        pst.DegreeName = DegreeName;
                        pst.SpecNumber = SpecNumber;
                        pst.SpecName = SpecName;
                        pst.FacultyName = FacultyName;
                        pst.ObrazProgramCrypt = OPCrypt;
                        pst.LicenseProgramId = LicenseProgramId;
                        pst.FacultyId = FacultyId;
                        context.PracticeLPStudent.Add(pst);
                        context.SaveChanges();
                        FillStudent();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Операция не завершена.\r\n" + ex.Message, "Инфо");
                return;
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string search = tbSearch.Text.Trim().ToUpper();
                bool exit = false;
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (exit)
                    { break; }
                    for (int j = 0; j < 8 /*dgv.Columns.Count*/; j++)
                    {
                        if (j == 0 || j == 1 || j == 2 || j == 4)
                            continue;
                        if (dgv[j, i].Value.ToString().ToUpper().Contains(search))
                        {
                            //dgv[j, i].Style.BackColor = Color.White;
                            dgv.CurrentCell = dgv[j, i];
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
    }
}
