using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmployerPartners.EDMX;

namespace EmployerPartners
{
    public partial class PracticeMain : Form
    {
        public int? _id;

        public int? PracticeYear
        {
            get { return ComboServ.GetComboIdInt(cbPracticeYear); }
            set { ComboServ.SetComboId(cbPracticeYear, value); }
        }

        public int? PracticeId
        {
            get { return ComboServ.GetComboIdInt(cbPracticeList); }
            set { ComboServ.SetComboId(cbPracticeList, value); }
        }
        public int? StudyLevelId
        {
            get { return ComboServ.GetComboIdInt(cbLevel); }
            set { ComboServ.SetComboId(cbLevel, value); }
        }
        public int? Period
        {
            get { return ComboServ.GetComboIdInt(cbPeriod); }
            set { ComboServ.SetComboId(cbPeriod, value); }
        }

        public PracticeMain()
        {
            InitializeComponent();
            FillPeriod();
            FillPracticeYear();
            FillPracticeFacultyList();
            FillStudyLevel();
            FillLPList();
            FillGrid();
            this.MdiParent = Util.mainform;
            SetAccessRight();
        }
        private void SetAccessRight()
        {
            if (Util.IsPracticeWrite())
            {
                btnAddLP.Enabled = true;
            }
        }
        private void FillPracticeYear()
        {
            ComboServ.FillCombo(cbPracticeYear, HelpClass.GetComboListByQuery(@" SELECT DISTINCT CONVERT(varchar(100), PracticeYear) AS Id, 
                CONVERT(varchar(100), PracticeYear) AS Name 
                FROM Practice ORDER BY Name DESC "), false, false);
        }
        private void FillPeriod()
        {
            //ComboServ.FillCombo(cbPeriod, HelpClass.GetComboListByTable("dbo.Period"), true, false);
            ComboServ.FillCombo(cbPeriod, HelpClass.GetComboListByQuery(@" SELECT CONVERT(varchar(100), Id) AS Id, Name 
                FROM Period ORDER BY Id "), true, false);
        }
        private void FillPracticeFacultyList()
        {
            ComboServ.FillCombo(cbPracticeList, HelpClass.GetComboListByQuery(@" SELECT CONVERT(varchar(100), Practice.Id) AS Id, 
                CONVERT(varchar(100), PracticeYear) + '   [ ' + Faculty.Name + '  ]' as Name 
                FROM Practice  INNER JOIN Faculty ON Practice.FacultyId = Faculty.Id 
                WHERE Practice.PracticeYear = " + ((PracticeYear.HasValue) ? (PracticeYear.ToString()) : ("null"))  + " ORDER BY Faculty.Name"), false, true);
        }
        private void FillPracticeFacultyList_()
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from p in context.Practice
                           join f in context.Faculty on p.FacultyId equals f.Id
                           orderby f.Name
                           select new
                           {
                               p.Id,
                               Практика = p.PracticeYear.ToString() + "  [  " + f.Name + "  ]",
                           }).ToList();

                cbPracticeList.DataSource = lst;
                cbPracticeList.ValueMember = "Id";
                cbPracticeList.DisplayMember = "Практика";               
            }
        }
        private void FillStudyLevel()
        {
            ComboServ.FillCombo(cbLevel, HelpClass.GetComboListByTable("dbo.StudyLevel"), false, true);
        }
        private void FillLPList()
        {
            FillLPList(null);
        }

        private void FillLPList(int? id)
        {
            string Faculty = (id.HasValue) ? (" WHERE FacultyId in (SELECT FacultyId FROM Practice WHERE Id = " + id.ToString() + ")") : "";
            string Level = (StudyLevelId.HasValue) ? (" StudyLevelId = " + StudyLevelId.ToString()) : ""; 
            string Where = (id.HasValue) ? ( " WHERE   (dbo.LicenseProgram.Id in (SELECT LicenseProgramId FROM ObrazProgram " + Faculty + ") " +
                                                    "OR dbo.LicenseProgram.Id in (SELECT SecondLicenseProgramId FROM ObrazProgram " + Faculty + ")) ") : " ";
            if (StudyLevelId.HasValue)
            {
                if (id.HasValue)
                {
                    Where += " AND " + Level;
                }
                else
                {
                    Where += " WHERE " + Level;
                }
            }

            ComboServ.FillCombo(cbLP, HelpClass.GetComboListByQuery(@" SELECT CONVERT(varchar(100), dbo.LicenseProgram.Id) AS Id, 
                CASE ISNULL(dbo.LicenseProgram.Code, N'""') 
                        WHEN '""' THEN '""' ELSE dbo.LicenseProgram.Code + N' ' END + dbo.StudyLevel.Name + N' ' + dbo.LicenseProgram.Name + 
                        N' [' + dbo.ProgramType.Name + N']' + N' [' + CASE ISNULL(dbo.Qualification.Name, N'""') WHEN '""' THEN '""' ELSE dbo.Qualification.Name + N' ' END +
                         N']' AS Name
                FROM    dbo.LicenseProgram INNER JOIN
                        dbo.ProgramType ON dbo.LicenseProgram.ProgramTypeId = dbo.ProgramType.Id INNER JOIN
                        dbo.StudyLevel ON dbo.LicenseProgram.StudyLevelId = dbo.StudyLevel.Id LEFT OUTER JOIN
                        dbo.Qualification ON dbo.LicenseProgram.QualificationId = dbo.Qualification.Id "
                + Where +
                " ORDER BY dbo.LicenseProgram.Code, dbo.StudyLevel.Name, dbo.ProgramType.Id"), false, true);
        }

        private void FillGrid()
        {
            FillGrid(null);
        }

        private void FillGrid(int? id)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from plp in context.PracticeLP
                           join p in context.Practice on plp.PracticeId equals p.Id
                           join fac in context.Faculty on p.FacultyId equals fac.Id
                           join ptype in context.PracticeType on plp.PracticeTypeId equals ptype.Id into _ptype
                           from ptype in _ptype.DefaultIfEmpty()

                           join op  in context.ObrazProgram on plp.ObrazProgramId equals op.Id into _op
                           from op in _op.DefaultIfEmpty()

                           join lpop in context.PracticeLPOP on plp.Id equals lpop.PracticeLPId into _lpop
                           from lpop in _lpop.DefaultIfEmpty()
                           join opyear in context.ObrazProgramInYear on lpop.ObrazProgramInYearId equals opyear.Id into _opyear
                           from opyear in _opyear.DefaultIfEmpty()

                           join lp in context.LicenseProgram on plp.LicenseProgramId equals lp.Id 
                           join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                           join progt in context.ProgramType on lp.ProgramTypeId equals progt.Id
                           join q in context.Qualification on lp.QualificationId equals q.Id into _q
                           from q in _q.DefaultIfEmpty()
                           where (id.HasValue ? plp.PracticeId == id : true) && (PracticeYear.HasValue ? p.PracticeYear == PracticeYear : false) &&
                                    (StudyLevelId.HasValue ? lp.StudyLevelId == StudyLevelId : true)
                           orderby fac.Name, lp.Code, st.Name, progt.Id 
                           select new
                           {
                               Направление = "[ " + fac.Name + " ] " + lp.Code + "  " + st.Name + "  " + lp.Name + " [" + progt.Name + "]" + " [" + q.Name + "]",
                               plp.Id,
                               plp.PracticeId,
                               plp.LicenseProgramId,
                               //Шифр_ОП = opyear.ObrazProgramCrypt,
                               Практика = plp.DisciplineName,
                               Номер_дисциплины = plp.DisciplineNumber,
                               Курс = plp.StudyCourse,
                               Семестр =  plp.ModuleName,
                               Шифр_ОП = plp.ObrazProgramCrypt,
                               Образовательная_программа = op.Name,
                               Профиль = plp.ProfileName,
                               Реализация = plp.RealizationVariantName,
                               Траектория = plp.TrajectoryName,

                               Тип_практики = ptype.Name,
                               Начало_практики = plp.DateStart,
                               Окончание_практики = plp.DateEnd,
                               Приказ = plp.OrderDoc,
                               Номер_приказа = plp.OrderNumber,
                               Дата_приказа = plp.OrderDate,
                               Автор_приказа = plp.OrderAuthor,
                               Распоряжение = plp.InstructionDoc,
                               Номер_распоряжения = plp.InstructionNumber,
                               Дата_распоряжения = plp.InstructionDate,
                               Автор_распоряжения = plp.InstructionAuthor,
                               Руководитель_практики = plp.Supervisor,
                               Авансодержатель = plp.AdvanceHolder,
                               Комментарий = plp.Comment,

                           }).ToList();

                DataTable dt = new DataTable();
                dt = Utilities.ConvertToDataTable(lst);
                bindingSource1.DataSource = dt;
                dgv.DataSource = bindingSource1;

                List<string> Cols = new List<string>() { "Id", "PracticeId", "LicenseProgramId" };

                foreach (string s in Cols)
                    if (dgv.Columns.Contains(s))
                        dgv.Columns[s].Visible = false;
                foreach (DataGridViewColumn col in dgv.Columns)
                    col.HeaderText = col.Name.Replace("_", " ");
                try
                {
                    dgv.Columns["Направление"].Frozen = true;
                    dgv.Columns["Направление"].Width = 600;
                    dgv.Columns["Практика"].Width = 300;
                    dgv.Columns["Курс"].Width = 60;
                    dgv.Columns["Образовательная_программа"].Width = 300;
                    dgv.Columns["Тип_практики"].Width = 150;
                    dgv.Columns["Приказ"].Width = 150;
                    dgv.Columns["Распоряжение"].Width = 150;
                    dgv.Columns["Руководитель_практики"].Width = 150;
                    dgv.Columns["Авансодержатель"].Width = 150;
                    dgv.Columns["Комментарий"].Width = 150;
                }
                catch (Exception)
                {
                }

                /*if (id.HasValue)
                    foreach (DataGridViewRow rw in dgv.Rows)
                        if (rw.Cells[0].Value.ToString() == id.Value.ToString())
                        {
                            dgv.CurrentCell = rw.Cells["Направление"];
                            break;
                        }*/
            }
        }

        private void cbPracticeList_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLPList(PracticeId);
            FillGrid(PracticeId);
        }
        private void cbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLPList(PracticeId);
            FillGrid(PracticeId);
        }

        private void btnAddLP_Click(object sender, EventArgs e)
        {
            if (!PracticeId.HasValue)
            {
                MessageBox.Show("Не выбрана практика", "Инфо");
                return;
            }
            int? LPId = ComboServ.GetComboIdInt(cbLP);
            if (!LPId.HasValue)
            {
                MessageBox.Show("Не выбрано направление подготовки", "Инфо");
                return;
            }
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.PracticeLP
                               where x.PracticeId == PracticeId
                               && x.LicenseProgramId == LPId
                               select new
                               {
                                   LPId = x.Id,
                               }).ToList().Count();
                    if (lst > 0)
                    {
                        //MessageBox.Show("Такое направление подготовки уже добавлено", "Инфо");
                        //return;
                        if (MessageBox.Show("Такое направление подготовки уже добавлено.\r\n" +
                            "Продолжить тем не менее?", "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                        { return; }
                    }
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    PracticeLP p = new PracticeLP();
                    p.PracticeId = (int)PracticeId;
                    p.LicenseProgramId = (int)LPId;
                    p.PracticeTypeId = 1;
                    context.PracticeLP.Add(p);
                    context.SaveChanges();
                    _id = p.Id;
                }
                
            /*var connString = ConfigurationManager.ConnectionStrings["EmployerPartnerSQLOLEDB"].ConnectionString;
                OleDbConnection dbConn = new OleDbConnection(connString);
                dbConn.Open();
                OleDbCommand dbCommand = dbConn.CreateCommand();
                dbCommand.CommandText = "INSERT INTO PracticeLP(PracticeId, LicenseProgramId) VALUES (?, ?)";
                dbCommand.Parameters.Add("practice", OleDbType.Integer).Value = PracticeId;
                dbCommand.Parameters.Add("lp", OleDbType.Integer).Value = LPId;
                dbCommand.ExecuteNonQuery();
                dbConn.Close();*/
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось добавить запись...\r\n" + ex.Message, "Сообщение");
                return;
            }
            FillGrid(PracticeId);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.CurrentCell != null)
                    if (dgv.CurrentCell.RowIndex >= 0)
                    {
                        int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                        int pid = int.Parse(dgv.CurrentRow.Cells["PracticeId"].Value.ToString());
                        int lpid = int.Parse(dgv.CurrentRow.Cells["LicenseProgramId"].Value.ToString());
                        string lp = dgv.CurrentRow.Cells["Направление"].Value.ToString();
                        if (Utilities.PracticeCardIsOpened(id))
                            return;
                        new PracticeCard(id, pid, lpid, lp, new UpdateIntHandler(FillGrid)).Show();
                    }
            }
            catch (Exception)
            {
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.CurrentCell != null)
                    if (dgv.CurrentCell.RowIndex >= 0)
                    {
                        int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                        int pid = int.Parse(dgv.CurrentRow.Cells["PracticeId"].Value.ToString());
                        int lpid = int.Parse(dgv.CurrentRow.Cells["LicenseProgramId"].Value.ToString());
                        string lp = dgv.CurrentRow.Cells["Направление"].Value.ToString();
                        if (Utilities.PracticeCardIsOpened(id))
                            return;
                        new PracticeCard(id, pid, lpid, lp, new UpdateIntHandler(FillGrid)).Show();
                    }
            }
            catch (Exception)
            {
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillPracticeFacultyList();
            FillStudyLevel();
            FillLPList();
            FillGrid();
        }

        private void PracticeMain_Load(object sender, EventArgs e)
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

        private bool CheckXLSData()
        {
            if (!PracticeYear.HasValue)
            {
                MessageBox.Show("Не выбран год!", "Сообщение");
                return false;
            }
            if (!Period.HasValue)
            {
                MessageBox.Show("Не выбран период!", "Сообщение");
                return false;
            }
            return true;
        }

        private void btnXLS_Click(object sender, EventArgs e)
        {
            if (!CheckXLSData())
                return;

            ToExcel();
        }

        private void ToExcel()
        {
            try
            {
                string filenameDate = "Практика - отчет";
                string filename = Util.TempFilesFolder + filenameDate + ".xlsx";
                string[] fileList = Directory.GetFiles(Util.TempFilesFolder, "Практика - отчет*" + ".xlsx");

                try
                {
                    File.Delete(filename);
                    foreach (string f in fileList)
                    {
                        File.Delete(f);
                    }
                }
                catch (Exception)
                {
                }

                int fileindex = 1;
                while (File.Exists(filename))
                {
                    filename = Util.TempFilesFolder + filenameDate + " (" + fileindex + ")" + ".xlsx";
                    fileindex++;
                }
                System.IO.FileInfo newFile = new System.IO.FileInfo(filename);
                if (newFile.Exists)
                {
                    newFile.Delete();  // ensures we create a new workbook
                    newFile = new System.IO.FileInfo(filename);
                }

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //диапазон дат
                    DateTime DateStart;
                    DateTime DateEnd;
                    string PYear = (PracticeYear.HasValue) ? PracticeYear.ToString() : DateTime.Now.Year.ToString();
                    DateStart = DateTime.Parse("01.01." + PYear);
                    DateEnd = DateTime.Parse("31.12." + PYear);
                    string xlsPeriod=(Period.HasValue) ? cbPeriod.Text : "";

                    switch (Period)
                    {
                        case 1:
                            //xlsPeriod = cbPeriod.DisplayMember.ToString(); //"1-й квартал"
                            DateStart = DateTime.Parse("01.01." + PYear);
                            DateEnd = DateTime.Parse("31.03." + PYear);
                            break;
                        case 2:
                            DateStart = DateTime.Parse("01.04." + PYear);
                            DateEnd = DateTime.Parse("30.06." + PYear);
                            break;
                        case 3:
                            DateStart = DateTime.Parse("01.07." + PYear);
                            DateEnd = DateTime.Parse("30.09." + PYear);
                            break;
                        case 4:
                            DateStart = DateTime.Parse("01.10." + PYear);
                            DateEnd = DateTime.Parse("31.12." + PYear);
                            break;
                        case 5:
                            DateStart = DateTime.Parse("01.01." + PYear);
                            DateEnd = DateTime.Parse("30.06." + PYear);
                            break;
                        case 6:
                            DateStart = DateTime.Parse("01.07." + PYear);
                            DateEnd = DateTime.Parse("31.12." + PYear);
                            break;
                        case 7:
                            DateStart = DateTime.Parse("01.01." + PYear);
                            DateEnd = DateTime.Parse("31.12." + PYear);
                            break;
                        default:
                            break;
                    }

                    /*var lst = (from x in context.PracticeStudent
                               join p in context.Practice on x.PracticeId equals p.Id
                               join fac in context.Faculty on x.FacultyId equals fac.Id
                               join plpo in context.PracticeLPOrganization on x.PracticeLPOrganizationId equals plpo.Id
                               join plp in context.PracticeLP on plpo.PracticeLPId equals plp.Id
                               join o in context.Organization on plpo.OrganizationId equals o.Id
                               //join o in context.Organization on x.OrganizationId equals o.Id   //into _r from r in _r.DefaultIfEmpty()
                               //join stud in context.Student on x.StudentId equals stud.Id
                               //join lp in context.LicenseProgram on stud.LicenseProgramId equals lp.Id
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               join progt in context.ProgramType on lp.ProgramTypeId equals progt.Id
                               join q in context.Qualification on lp.QualificationId equals q.Id
                               where
                                   (x.PracticeLPOrganizationId != null) && 
                                   (PracticeYear.HasValue ? p.PracticeYear == PracticeYear : false) &&
                                   (plp.DateStart >= DateStart) && (plp.DateStart <= DateEnd) 
                               select new
                               {
                                   orgName = o.Name,
                                   o.SPbGU,
                                   //stud.LicenseProgramId,
                                   x.FacultyId,
                                   facName = fac.Name,
                                   x.LicenseProgramId,
                                   lpName ="[" + fac.Name + "] " + lp.Code + "  " + st.Name + "  " + lp.Name + " [" + progt.Name + "]" + " [" + q.Name + "]",
                                   p.PracticeYear,
                               }).ToList(); */

                    var lst = (from x in context.PracticeLPStudent
                               join plp in context.PracticeLP on x.PracticeLPId equals plp.Id
                               join p in context.Practice on plp.PracticeId equals p.Id
                               join fac in context.Faculty on x.FacultyId equals fac.Id
                               join plpo in context.PracticeLPOrganization on x.PracticeLPOrganizationId equals plpo.Id
                               join o in context.Organization on plpo.OrganizationId equals o.Id
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               join progt in context.ProgramType on lp.ProgramTypeId equals progt.Id
                               join q in context.Qualification on lp.QualificationId equals q.Id
                               where
                                   (x.PracticeLPOrganizationId != null) &&
                                   (PracticeYear.HasValue ? p.PracticeYear == PracticeYear : false) &&
                                   (plp.DateStart >= DateStart) && (plp.DateStart <= DateEnd)
                               select new
                               {
                                   orgName = o.Name,
                                   o.SPbGU,
                                   x.FacultyId,
                                   facName = fac.Name,
                                   //x.LicenseProgramId,
                                   plp.LicenseProgramId,
                                   lpName = "[" + fac.Name + "] " + lp.Code + "  " + st.Name + "  " + lp.Name + " [" + progt.Name + "]" + " [" + q.Name + "]",
                                   p.PracticeYear,
                               }).ToList();

                    var ggr = lst.GroupBy(x => new { x.orgName, x.SPbGU, x.PracticeYear, x.FacultyId, x.LicenseProgramId, x.facName, x.lpName }).ToList();

                    var gr = (from l in ggr
                              select new
                              {
                                  Направление = l.First().lpName,
                                  Организация = l.First().orgName,
                                  Кол__во_студентов = l.Count(),
                                  l.First().SPbGU,
                              }).OrderBy(x => x.Направление).ThenByDescending(x => x.SPbGU).ThenBy(x => x.Организация).ToList();

                    var orggr = (from l in ggr
                              select new
                              {
                                  Организация = l.First().orgName,
                                  Направление = l.First().lpName,
                                  Кол__во_студентов = l.Count(),
                                  l.First().SPbGU,
                              }).OrderByDescending(x => x.SPbGU).ThenBy(x => x.Организация).ThenBy(x => x.Направление).ToList();

                    var ggr2 = lst.GroupBy(x => new { x.orgName, x.SPbGU, x.PracticeYear}).ToList();

                    var orglist = (from l in ggr2
                                 select new
                                 {
                                     Организация = l.First().orgName,
                                     Кол__во_студентов = l.Count(),
                                     l.First().SPbGU,
                                 }).OrderByDescending(x => x.SPbGU).ThenBy(x => x.Организация).ToList();

                    var ggr3 = lst.GroupBy(x => new { x.orgName, x.PracticeYear }).ToList();
                    var orgcount = ggr3.Count();

                    var ggr4 = lst.GroupBy(x => new { x.LicenseProgramId, x.PracticeYear, }).ToList();
                    var lpcount = ggr4.Count();

                    /*var lst2 = (from x in context.PracticeStudent
                                join plpo in context.PracticeLPOrganization on x.PracticeLPOrganizationId equals plpo.Id
                                join plp in context.PracticeLP on plpo.PracticeLPId equals plp.Id
                                where
                                (PracticeYear.HasValue ? p.PracticeYear == PracticeYear : false) &&
                                (plp.DateStart >= DateStart) && (plp.DateStart <= DateEnd)
                                select new
                                {
                                    x.StudentFIO,
                                    x.DR
                                }).ToList();
                    var ggr5 = lst2.GroupBy(x => new { x.StudentFIO, x.DR }).ToList();
                    var stcount = ggr5.Count();*/

                    var stcount = lst.Count();

                    var ggr5 = lst.GroupBy(x => new { x.PracticeYear, x.lpName }).ToList();
                    var lplist = (from l in ggr5
                                   select new
                                   {
                                       Направление_подготовки = l.First().lpName,
                                       Кол__во_студентов = l.Count(),
                                   }).OrderBy(x => x.Направление_подготовки).ToList();

                    var ggr6 = lst.GroupBy(x => new {x.PracticeYear, x.facName}).ToList();
                    var faclist = (from l in ggr6
                                   select new
                                   {
                                       Направление_образования = l.First().facName,
                                       Кол__во_студентов = l.Count(),
                                   }).OrderBy(x => x.Направление_образования).ToList();

                using (ExcelPackage doc = new ExcelPackage(newFile))
                {
                    //лист по направлениям подготовки
                    dgvXLS.DataSource = gr;
                    foreach (DataGridViewColumn col in dgvXLS.Columns)
                        col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");

                    ExcelWorksheet ws = doc.Workbook.Worksheets.Add("Направления подготовки");

                    Color lightGray = Color.FromName("LightGray");
                    Color darkGray = Color.FromName("DarkGray");
                    Color gainsboro = Color.FromName("Gainsboro");
                    Color ghostWhite = Color.FromName("GhostWhite");


                    int colind = 0;
                    int rowshift = 2;
                    string LPName = "";
                    int lrows = 0;
                    int lnum = 0;
                    int lnum_start = 0;

                    ws.Cells[1, 1].Value = "Практика " + xlsPeriod + " " + PYear;
                    //ws.Cells[1,1].Style.Font.Bold = true;

                    for (int rwInd = 0; rwInd < dgvXLS.Rows.Count; rwInd++)
                    {
                        DataGridViewRow rw = dgvXLS.Rows[rwInd];
                        int colInd = 0;
                        foreach (DataGridViewCell cell in rw.Cells)
                        {
                            if (cell.ColumnIndex == 0)
                            {
                                if (cell.Value.ToString() != LPName)
                                {
                                    lrows = lnum - lnum_start;
                                    //итоги
                                    if (lrows > 0)
                                    {
                                        int colnum = 0;
                                        foreach (DataGridViewColumn cl in dgvXLS.Columns)
                                        {
                                            colnum++;
                                            if (cl.HeaderText.ToString() == "Кол-во студентов")
                                            {
                                                ws.Cells[rwInd + rowshift, colnum].FormulaR1C1 = "=SUM(R[-" + lrows + "]C:R[-1]C)";
                                                ws.Cells[rwInd + rowshift, colnum].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                                                ws.Cells[rwInd + rowshift, colnum].Style.Font.Bold = true;
                                            }
                                            else if (cl.HeaderText.ToString() == "Организация")
                                            {
                                                ws.Cells[rwInd + rowshift, colnum].Value = "Итого";
                                                ws.Cells[rwInd + rowshift, colnum].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                                            }
                                        }
                                        rowshift++;
                                    }

                                    rowshift++;
                                    LPName = cell.Value.ToString();
                                    ws.Cells[rwInd + rowshift, colInd + 1].Value = cell.Value;
                                    rowshift++;
                                    colind++;
                                    foreach (DataGridViewColumn cl in dgvXLS.Columns)
                                    {
                                        if ((cl.HeaderText.ToString() != "Направление") && (cl.HeaderText.ToString() !="SPbGU"))
                                        {
                                            ws.Cells[rwInd + rowshift, ++colind].Value = cl.HeaderText.ToString();
                                            ws.Cells[rwInd + rowshift, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                                            ws.Cells[rwInd + rowshift, colind].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                            ws.Cells[rwInd + rowshift, colind].Style.Fill.BackgroundColor.SetColor(lightGray);
                                        }
                                    }
                                    lnum_start = rwInd + rowshift;
                                    rowshift++;
                                    colind = 0;
                                }
                            }
                            else
                            {
                                if (dgvXLS.Columns[cell.ColumnIndex].HeaderText.ToString() != "SPbGU")
                                { 
                                ws.Cells[rwInd + rowshift, colInd + 1].Value = cell.Value; //.ToString(); 
                                ws.Cells[rwInd + rowshift, colInd + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                                }
                            }
                            colInd++;
                        }
                        lnum = rwInd + rowshift;
                    }

                    lrows = lnum - lnum_start;
                    lnum++;
                    //итоги
                    if (lrows > 0)
                    {
                        int colnum = 0;
                        foreach (DataGridViewColumn cl in dgvXLS.Columns)
                        {
                            colnum++;
                            if (cl.HeaderText.ToString() == "Кол-во студентов")
                            {
                                ws.Cells[lnum, colnum].FormulaR1C1 = "=SUM(R[-" + lrows + "]C:R[-1]C)";
                                ws.Cells[lnum, colnum].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                                ws.Cells[lnum, colnum].Style.Font.Bold = true;
                            }
                            else if (cl.HeaderText.ToString() == "Организация")
                            {
                                ws.Cells[lnum, colnum].Value = "Итого";
                                ws.Cells[lnum, colnum].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                            }
                        }
                        rowshift++;
                    }

                    //форматирование
                    int clmnInd = 0;
                    foreach (DataGridViewColumn clmn in dgvXLS.Columns)
                    {
                        if (clmn.HeaderText.ToString() == "Направление")
                            ++clmnInd;
                        //else if (clmn.HeaderText.ToString() == "SPbGU")
                          //  ws.Column(++clmnInd).Width = 0;
                        else if (clmn.HeaderText.ToString() == "Организация")
                            ws.Column(++clmnInd).Width = 150;
                        else
                            ws.Column(++clmnInd).AutoFit();
                    }

                    //лист по организациям (в разрезе направлений)
                    dgvXLS.DataSource = orggr;
                    foreach (DataGridViewColumn col in dgvXLS.Columns)
                        col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");

                    ws = doc.Workbook.Worksheets.Add("Организации-направления");

                    colind = 0;
                    rowshift = 2;
                    string OrgName = "";
                    lrows = 0;
                    lnum = 0;
                    lnum_start = 0;

                    ws.Cells[1, 1].Value = "Практика " + xlsPeriod + " " + PYear;
                    //ws.Cells[1,1].Style.Font.Bold = true;

                    for (int rwInd = 0; rwInd < dgvXLS.Rows.Count; rwInd++)
                    {
                        DataGridViewRow rw = dgvXLS.Rows[rwInd];
                        int colInd = 0;
                        foreach (DataGridViewCell cell in rw.Cells)
                        {
                            if (cell.ColumnIndex == 0)
                            {
                                if (cell.Value.ToString() != OrgName)
                                {
                                    lrows = lnum - lnum_start;
                                    //итоги
                                    if (lrows > 1)
                                    {
                                        int colnum = 0;
                                        foreach (DataGridViewColumn cl in dgvXLS.Columns)
                                        {
                                            colnum++;
                                            if (cl.HeaderText.ToString() == "Кол-во студентов")
                                            {
                                                ws.Cells[rwInd + rowshift, colnum].FormulaR1C1 = "=SUM(R[-" + lrows + "]C:R[-1]C)";
                                                ws.Cells[rwInd + rowshift, colnum].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                                                ws.Cells[rwInd + rowshift, colnum].Style.Font.Bold = true;
                                            }
                                            else if (cl.HeaderText.ToString() == "Направление")
                                            {
                                                ws.Cells[rwInd + rowshift, colnum].Value = "Итого";
                                                ws.Cells[rwInd + rowshift, colnum].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                                            }
                                        }
                                        rowshift++;
                                    }

                                    rowshift++;
                                    OrgName = cell.Value.ToString();
                                    ws.Cells[rwInd + rowshift, colInd + 1].Value = cell.Value;
                                    ws.Cells[rwInd + rowshift, colInd + 1].Style.Font.Italic = true;
                                    rowshift++;
                                    colind++;
                                    foreach (DataGridViewColumn cl in dgvXLS.Columns)
                                    {
                                        if ((cl.HeaderText.ToString() != "Организация") && (cl.HeaderText.ToString() != "SPbGU"))
                                        {
                                            ws.Cells[rwInd + rowshift, ++colind].Value = cl.HeaderText.ToString();
                                            ws.Cells[rwInd + rowshift, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                                            ws.Cells[rwInd + rowshift, colind].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                            ws.Cells[rwInd + rowshift, colind].Style.Fill.BackgroundColor.SetColor(ghostWhite);
                                        }
                                    }
                                    lnum_start = rwInd + rowshift;
                                    rowshift++;
                                    colind = 0;
                                }
                            }
                            else
                            {
                                if (dgvXLS.Columns[cell.ColumnIndex].HeaderText.ToString() != "SPbGU")
                                {
                                    ws.Cells[rwInd + rowshift, colInd + 1].Value = cell.Value; //.ToString(); 
                                    ws.Cells[rwInd + rowshift, colInd + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                                }
                            }
                            colInd++;
                        }
                        lnum = rwInd + rowshift;
                    }

                    lrows = lnum - lnum_start;
                    lnum++;
                    //итоги
                    if (lrows > 1)
                    {
                        int colnum = 0;
                        foreach (DataGridViewColumn cl in dgvXLS.Columns)
                        {
                            colnum++;
                            if (cl.HeaderText.ToString() == "Кол-во студентов")
                            {
                                ws.Cells[lnum, colnum].FormulaR1C1 = "=SUM(R[-" + lrows + "]C:R[-1]C)";
                                ws.Cells[lnum, colnum].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                                ws.Cells[lnum, colnum].Style.Font.Bold = true;
                            }
                            else if (cl.HeaderText.ToString() == "Направление")
                            {
                                ws.Cells[lnum, colnum].Value = "Итого";
                                ws.Cells[lnum, colnum].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                            }
                        }
                        rowshift++;
                    }

                    //форматирование
                    clmnInd = 0;
                    foreach (DataGridViewColumn clmn in dgvXLS.Columns)
                    {
                        if (clmn.HeaderText.ToString() == "Организация")
                            ++clmnInd;
                        //else if (clmn.HeaderText.ToString() == "SPbGU")
                        //  ws.Column(++clmnInd).Width = 0;
                        else if (clmn.HeaderText.ToString() == "Направление")
                            ws.Column(++clmnInd).Width = 125;
                        else
                            ws.Column(++clmnInd).AutoFit();
                    }
                    // конец лист по организациям в разрезе направлений

                    //лист по организациям (список)
                    dgvXLS.DataSource = orglist;

                    foreach (DataGridViewColumn col in dgvXLS.Columns)
                        col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
                    ws = doc.Workbook.Worksheets.Add("Организации-список");

                    ws.Cells[1, 1].Value = "Практика " + xlsPeriod + " " + PYear;
                    //ws.Cells[1,1].Style.Font.Bold = true;

                    rowshift = 2;
                    colind = 0;
                    int ln = 0;

                    ws.Cells[1 + rowshift, ++colind].Value = "№ п/п";
                    ws.Cells[1 + rowshift, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                    ws.Cells[1 + rowshift, colind].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    ws.Cells[1 + rowshift, colind].Style.Fill.BackgroundColor.SetColor(lightGray);
                    //ws.Column(1).AutoFit();

                    foreach (DataGridViewColumn cl in dgvXLS.Columns)
                    {
                        if (cl.HeaderText.ToString() != "SPbGU")
                        {
                            ws.Cells[1 + rowshift, ++colind].Value = cl.HeaderText.ToString();
                            ws.Cells[1 + rowshift, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                            ws.Cells[1 + rowshift, colind].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            ws.Cells[1 + rowshift, colind].Style.Fill.BackgroundColor.SetColor(gainsboro);
                        }
                    }

                    for (int rwInd = 0; rwInd < dgvXLS.Rows.Count; rwInd++)
                    {
                        int colInd = 1;
                        ln++;
                        ws.Cells[rwInd + 2 + rowshift, colInd].Value = ln;
                        ws.Cells[rwInd + 2 + rowshift, colInd].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                        
                        DataGridViewRow rw = dgvXLS.Rows[rwInd];
                        
                        foreach (DataGridViewCell cell in rw.Cells)
                        {
                            if (dgvXLS.Columns[cell.ColumnIndex].HeaderText.ToString() != "SPbGU")
                            {
                                if (cell.Value == null)
                                {
                                    ws.Cells[rwInd + 2 + rowshift, colInd + 1].Value = "";
                                }
                                else
                                {
                                    ws.Cells[rwInd + 2 + rowshift, colInd + 1].Value = cell.Value;
                                }
                                ws.Cells[rwInd + 2 + rowshift, colInd + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                                colInd++;
                            }
                        }
                    }
                    //форматирование
                    clmnInd = 1;
                    //ws.Column(clmnInd).AutoFit();
                    foreach (DataGridViewColumn clmn in dgvXLS.Columns)
                    {
                        if (clmn.HeaderText.ToString() == "Организация")
                            ws.Column(++clmnInd).Width = 150;
                        else if (clmn.HeaderText.ToString() == "SPbGU")
                            ++clmnInd;
                        else
                            ws.Column(++clmnInd).AutoFit();
                    }
                    // конец лист по организациям (список)

                    // лист статистика
                    ws = doc.Workbook.Worksheets.Add("Статистика");

                    ws.Cells[1, 1].Value = "Практика " + xlsPeriod + " " + PYear;
                    //ws.Cells[1,1].Style.Font.Bold = true;

                    rowshift = 3;
                    ws.Cells[rowshift, 1].Value = "Общая статистика";
                    ws.Cells[rowshift, 1].Style.Font.Italic = true;

                    colind = 0;
                    ws.Cells[++rowshift, ++colind].Value = "Количество организаций-партнеров, в которых проходила практика: ";
                    ws.Cells[rowshift, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                    ws.Cells[rowshift, ++colind].Value = orgcount;
                    ws.Cells[rowshift, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                    colind = 0;
                    ws.Cells[++rowshift, ++colind].Value = "Количество направлений подготовки, по которым проходила практика: ";
                    ws.Cells[rowshift, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                    ws.Cells[rowshift, ++colind].Value = lpcount;
                    ws.Cells[rowshift, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                    colind = 0;
                    ws.Cells[++rowshift, ++colind].Value = "Общее количество студентов, которые проходили практику: ";
                    ws.Cells[rowshift, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                    ws.Cells[rowshift, ++colind].Value = stcount;
                    ws.Cells[rowshift, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                    rowshift++;
                    //форматирование
                    clmnInd = 1;
                    ws.Column(clmnInd).AutoFit();

                    dgvXLS.DataSource = faclist;

                    rowshift++;
                    ws.Cells[rowshift, 1].Value = "Статистика по направлениям образования";
                    ws.Cells[rowshift, 1].Style.Font.Italic = true;

                    foreach (DataGridViewColumn col in dgvXLS.Columns)
                        col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
                    colind = 0;
                    rowshift++;
                    foreach (DataGridViewColumn cl in dgvXLS.Columns)
                    {
                        ws.Cells[rowshift, ++colind].Value = cl.HeaderText.ToString();
                        ws.Cells[rowshift, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                        ws.Cells[rowshift, colind].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[rowshift, colind].Style.Fill.BackgroundColor.SetColor(lightGray);
                    }
                    for (int rwInd = 0; rwInd < dgvXLS.Rows.Count; rwInd++)
                    {
                        rowshift++;
                        int colInd = 0;
                        DataGridViewRow rw = dgvXLS.Rows[rwInd];
                        foreach (DataGridViewCell cell in rw.Cells)
                        {
                            if (cell.Value == null)
                            {
                                ws.Cells[rowshift, colInd + 1].Value = "";
                            }
                            else
                            {
                                ws.Cells[rowshift, colInd + 1].Value = cell.Value;
                            }
                            ws.Cells[rowshift, colInd + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                            colInd++;
                        }
                    }
                    rowshift++;
                    //форматирование
                    clmnInd = 0;
                    foreach (DataGridViewColumn clmn in dgvXLS.Columns)
                    {
                        ws.Column(++clmnInd).AutoFit();
                    }

                    dgvXLS.DataSource = lplist;

                    rowshift++;
                    ws.Cells[rowshift, 1].Value = "Статистика по направлениям подготовки";
                    ws.Cells[rowshift, 1].Style.Font.Italic = true;

                    foreach (DataGridViewColumn col in dgvXLS.Columns)
                        col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
                    rowshift++;
                    colind = 0;
                    foreach (DataGridViewColumn cl in dgvXLS.Columns)
                    {
                        ws.Cells[rowshift, ++colind].Value = cl.HeaderText.ToString();
                        ws.Cells[rowshift, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                        ws.Cells[rowshift, colind].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[rowshift, colind].Style.Fill.BackgroundColor.SetColor(lightGray);
                    }
                    for (int rwInd = 0; rwInd < dgvXLS.Rows.Count; rwInd++)
                    {
                        rowshift++;
                        int colInd = 0;
                        DataGridViewRow rw = dgvXLS.Rows[rwInd];
                        foreach (DataGridViewCell cell in rw.Cells)
                        {
                            if (cell.Value == null)
                            {
                                ws.Cells[rowshift, colInd + 1].Value = "";
                            }
                            else
                            {
                                ws.Cells[rowshift, colInd + 1].Value = cell.Value;
                            }
                            ws.Cells[rowshift, colInd + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                            colInd++;
                        }
                    }
                    rowshift++;
                    //форматирование
                    clmnInd = 0;
                    foreach (DataGridViewColumn clmn in dgvXLS.Columns)
                    {
                        ws.Column(++clmnInd).AutoFit();
                    }
                    // конец лист статистика

                    doc.Save();
                }
                }
                System.Diagnostics.Process.Start(filename);
            }
            catch (Exception ec)
            {
                MessageBox.Show("Не удалось подготовить отчет...\r\n" + ec.Message, "Сообщение");
            }
        }

        private void cbPracticeYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillPracticeFacultyList();
        }

        private bool CheckXLS2Data()
        {
            if (!PracticeYear.HasValue)
            {
                MessageBox.Show("Не выбран год!", "Сообщение");
                return false;
            }
            if (!Period.HasValue)
            {
                MessageBox.Show("Не выбран квартал!", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbPeriod.DroppedDown = true;
                return false;
            }
            bool period = false;
            switch (Period)
            {
                case 1:
                    period = true;
                    break;
                case 2:
                    period = true;
                    break;
                case 3:
                    period = true;
                    break;
                case 4:
                    period = true;
                    break;
                default:
                    break;
            }
            if (period)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Не выбран квартал", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbPeriod.DroppedDown = true;
                return false;
            }
        }
        private void btnXLS2_Click(object sender, EventArgs e)
        {
            if (!CheckXLS2Data())
                return;

            ToExcel2();
        }
        private void ToExcel2()
        { 
            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "Квартальный отчет по практикам"; 

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

            System.IO.FileInfo newFile = new System.IO.FileInfo(filePath);
            //if (newFile.Exists)
            //{
            //    newFile.Delete();  // ensures we create a new workbook
            //    newFile = new System.IO.FileInfo(filePath);
            //}
            //////

            //заполнение шаблона
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //диапазон дат
                    DateTime DateStart;
                    DateTime DateEnd;
                    string PYear = (PracticeYear.HasValue) ? PracticeYear.ToString() : DateTime.Now.Year.ToString();
                    DateStart = DateTime.Parse("01.01." + PYear);
                    DateEnd = DateTime.Parse("31.12." + PYear);
                    string xlsPeriod = (Period.HasValue) ? cbPeriod.Text : "";

                    switch (Period)
                    {
                        case 1:
                            //xlsPeriod = cbPeriod.DisplayMember.ToString(); //"1-й квартал"
                            DateStart = DateTime.Parse("01.01." + PYear);
                            DateEnd = DateTime.Parse("31.03." + PYear);
                            break;
                        case 2:
                            DateStart = DateTime.Parse("01.04." + PYear);
                            DateEnd = DateTime.Parse("30.06." + PYear);
                            break;
                        case 3:
                            DateStart = DateTime.Parse("01.07." + PYear);
                            DateEnd = DateTime.Parse("30.09." + PYear);
                            break;
                        case 4:
                            DateStart = DateTime.Parse("01.10." + PYear);
                            DateEnd = DateTime.Parse("31.12." + PYear);
                            break;
                        case 5:
                            DateStart = DateTime.Parse("01.01." + PYear);
                            DateEnd = DateTime.Parse("30.06." + PYear);
                            break;
                        case 6:
                            DateStart = DateTime.Parse("01.07." + PYear);
                            DateEnd = DateTime.Parse("31.12." + PYear);
                            break;
                        case 7:
                            DateStart = DateTime.Parse("01.01." + PYear);
                            DateEnd = DateTime.Parse("31.12." + PYear);
                            break;
                        default:
                            break;
                    }
                    //проверка установки организаций для всех студентов
                    try
                    {
                        var count = (from x in context.PracticeLPStudent
                                     join plp in context.PracticeLP on x.PracticeLPId equals plp.Id
                                     join p in context.Practice on plp.PracticeId equals p.Id
                                     where
                                     (x.PracticeLPOrganizationId == null) &&
                                     (PracticeYear.HasValue ? p.PracticeYear == PracticeYear : false) &&
                                     (plp.DateStart >= DateStart) && (plp.DateStart <= DateEnd)
                                     select x.Id).Count();
                        if (count > 0)
                        {
                            if (MessageBox.Show("Не для всех студентов (" + count.ToString() + " чел.) установлена организация.\r\n" +
                                "Такие студенты не попадут в отчет.\r\n " +
                                "Продолжить тем не менее?", "Запрос на подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                                return;
                        }
                    }
                    catch (Exception)
                    {
                    }

                    var lst = (from x in context.PracticeLPStudent
                               join plp in context.PracticeLP on x.PracticeLPId equals plp.Id
                               join prtype in context.PracticeType on plp.PracticeTypeId equals prtype.Id into _prtype
                                 from prtype in _prtype.DefaultIfEmpty()
                               
                               join orgstdog in context.OrganizationDogovor on x.OrganizationDogovorId equals orgstdog.Id into _orgstdog
                               from orgstdog in _orgstdog.DefaultIfEmpty()

                               join lpop in context.PracticeLPOP on plp.Id equals lpop.PracticeLPId
                               
                               join op in context.ObrazProgram on lpop.ObrazProgramId equals op.Id
                               join opinyear in context.ObrazProgramInYear on lpop.ObrazProgramInYearId equals opinyear.Id into _opinyear
                               from opinyear in _opinyear.DefaultIfEmpty()
                               
                               join p in context.Practice on plp.PracticeId equals p.Id
                               join fac in context.Faculty on x.FacultyId equals fac.Id
                               join plpo in context.PracticeLPOrganization on x.PracticeLPOrganizationId equals plpo.Id
                               join o in context.Organization on plpo.OrganizationId equals o.Id

                               join orgdog in context.OrganizationDogovor on plpo.OrganizationDogovorId equals orgdog.Id into _orgdog
                               from orgdog in _orgdog.DefaultIfEmpty()

                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               join progt in context.ProgramType on lp.ProgramTypeId equals progt.Id
                               //join q in context.Qualification on lp.QualificationId equals q.Id
                               where
                                   (x.PracticeLPOrganizationId != null) &&
                                   (PracticeYear.HasValue ? p.PracticeYear == PracticeYear : false) &&
                                   (plp.DateStart >= DateStart) && (plp.DateStart <= DateEnd)
                               orderby fac.Name, st.Name, lp.Code, lp.Name, x.StudentFIO
                               select new
                               {
                                   FIO = x.StudentFIO,
                                   //LPOP = "ОП " + opinyear.ObrazProgramCrypt + " Направление: " + lp.Code,
                                   LPOP = lp.Code + " " + lp.Name + " (ОП: " + op.Name + ")",//opinyear.ObrazProgramCrypt,
                                   RegNomWP = x.RegNomWP,
                                   StudyForm = x.Department,
                                   StudyLevel = st.Name,
                                   Course = x.Course,
                                   PracticeType = prtype.Name,
                                   PracticeLength = "",
                                   DateStart = plpo.DateStart,
                                   DateEnd = plpo.DateEnd,
                                   //Period = plpo.DateStart,
                                   Organization = o.Name,
                                   City = o.City,
                                   OrderNumber = plp.OrderNumber,
                                   OrderDate = plp.OrderDate,
                                   InstructionNumber = plp.InstructionNumber,
                                   InstructionDate = plp.InstructionDate,
                                   DocumentNumber = (x.OrganizationDogovorId.HasValue) ? orgstdog.DocumentNumber : ((plpo.OrganizationDogovorId.HasValue) ? orgdog.DocumentNumber : ""),
                                   DocumentDate = (x.OrganizationDogovorId.HasValue) ? orgstdog.DocumentDate : ((plpo.OrganizationDogovorId.HasValue) ? orgdog.DocumentDate : null),
                               }).ToArray();

                    var lst2 = (from x in lst
                                select new
                                {
                                    x.FIO,
                                    x.LPOP,
                                    x.RegNomWP,
                                    x.StudyForm,
                                    x.StudyLevel,
                                    x.Course,
                                    x.PracticeType,
                                    x.PracticeLength,
                                    Period = ((x.DateStart.HasValue) ? x.DateStart.Value.Date.ToString("dd.MM.yyyy") : "") + " - " + ((x.DateEnd.HasValue) ? x.DateEnd.Value.Date.ToString("dd.MM.yyyy") : ""),
                                    x.Organization,
                                    x.City,
                                    x.OrderNumber,
                                    OrderDate = (x.OrderDate.HasValue) ? x.OrderDate.Value.Date.ToString("dd.MM.yyyy") : "",
                                    x.InstructionNumber,
                                    InstructionDate = (x.InstructionDate.HasValue) ? x.InstructionDate.Value.Date.ToString("dd.MM.yyyy") : "",
                                    x.DocumentNumber,
                                    DocumentDate = (x.DocumentDate.HasValue) ? x.DocumentDate.Value.Date.ToString("dd.MM.yyyy") : "",
                                }).ToList();

                    using (ExcelPackage doc = new ExcelPackage(newFile))
                    {
                        dgvXLS.DataSource = lst2;
                        ExcelWorksheet ws = doc.Workbook.Worksheets[1];
                        int num = 0;

                        for (int rwInd = 0; rwInd < dgvXLS.Rows.Count; rwInd++)
                        {
                            num += 1;
                            ws.Cells[rwInd + 7, 1].Value = num;
                            DataGridViewRow rw = dgvXLS.Rows[rwInd];
                            int colInd = 1;
                            foreach (DataGridViewCell cell in rw.Cells)
                            {
                                colInd += 1;
                                ws.Cells[rwInd + 7, colInd].Value = cell.Value;
                            }
                        }

                        doc.Save();
                    }
                } //end using context

                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Не удалось подготовить отчет...\r\n" + exc.Message, "Сообщение");
            }
        }
    }
}
