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
            FillLPList();
            FillGrid();
            this.MdiParent = Util.mainform;
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
                FROM Faculty INNER JOIN Practice ON Faculty.Id = Practice.FacultyId 
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

        private void FillLPList()
        {
            FillLPList(null);
        }

        private void FillLPList(int? id)
        {
            string Faculty = (id.HasValue) ? ("WHERE FacultyId in (SELECT FacultyId FROM Practice WHERE Id = " + id.ToString() + ")") : "";

            ComboServ.FillCombo(cbLP, HelpClass.GetComboListByQuery(@" SELECT CONVERT(varchar(100), dbo.LicenseProgram.Id) AS Id, 
                CASE ISNULL(dbo.LicenseProgram.Code, N'""') 
                        WHEN '""' THEN '""' ELSE dbo.LicenseProgram.Code + N' ' END + dbo.StudyLevel.Name + N' ' + dbo.LicenseProgram.Name + N' [' + dbo.ProgramType.Name + N']' + N' [' + dbo.Qualification.Name +
                         N']' AS Name
                FROM    dbo.LicenseProgram INNER JOIN
                        dbo.ProgramType ON dbo.LicenseProgram.ProgramTypeId = dbo.ProgramType.Id INNER JOIN
                        dbo.StudyLevel ON dbo.LicenseProgram.StudyLevelId = dbo.StudyLevel.Id INNER JOIN
                        dbo.Qualification ON dbo.LicenseProgram.QualificationId = dbo.Qualification.Id 
                WHERE   dbo.LicenseProgram.Id in (SELECT LicenseProgramId FROM ObrazProgram " + Faculty + ") " +
                "ORDER BY dbo.LicenseProgram.Code, dbo.StudyLevel.Name, dbo.ProgramType.Id"), false, true);
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
                           join ptype in context.PracticeType on plp.PracticeTypeId equals ptype.Id into _ptype
                           from ptype in _ptype.DefaultIfEmpty()
                           join lp in context.LicenseProgram on plp.LicenseProgramId equals lp.Id 
                           join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                           join progt in context.ProgramType on lp.ProgramTypeId equals progt.Id
                           join q in context.Qualification on lp.QualificationId equals q.Id
                           where (id.HasValue ? plp.PracticeId == id : true) && (PracticeYear.HasValue ? p.PracticeYear == PracticeYear : false)
                           orderby lp.Code, st.Name, progt.Id 
                           select new
                           {
                               Направление = lp.Code + "  " + st.Name + "  " + lp.Name + " [" + progt.Name + "]" + " [" + q.Name + "]",
                               plp.Id,
                               plp.PracticeId,
                               plp.LicenseProgramId,
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

                bindingSource1.DataSource = lst;
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
                    dgv.Columns["Направление"].Width = 350;
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
                        MessageBox.Show("Такое направление подготовки уже добавлено", "Инфо");
                        return;
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
            if (dgv.CurrentCell != null)
                if (dgv.CurrentCell.RowIndex >= 0)
                {
                    int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                    int pid = int.Parse(dgv.CurrentRow.Cells["PracticeId"].Value.ToString());
                    int lpid = int.Parse(dgv.CurrentRow.Cells["LicenseProgramId"].Value.ToString());
                    string lp = dgv.CurrentRow.Cells["Направление"].Value.ToString();
                    new PracticeCard(id, pid, lpid, lp, new UpdateVoidHandler(FillGrid)).Show();
                }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentCell.RowIndex >= 0)
                {
                    int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                    int pid = int.Parse(dgv.CurrentRow.Cells["PracticeId"].Value.ToString());
                    int lpid = int.Parse(dgv.CurrentRow.Cells["LicenseProgramId"].Value.ToString());
                    string lp = dgv.CurrentRow.Cells["Направление"].Value.ToString();
                    new PracticeCard(id, pid, lpid, lp, new UpdateVoidHandler(FillGrid)).Show();
                }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillPracticeFacultyList();
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

                    var lst = (from x in context.PracticeStudent
                               join p in context.Practice on x.PracticeId equals p.Id
                               join plpo in context.PracticeLPOrganization on x.PracticeLPOrganizationId equals plpo.Id
                               join plp in context.PracticeLP on plpo.PracticeLPId equals plp.Id
                               join o in context.Organization on plpo.OrganizationId equals o.Id
                               //join o in context.Organization on x.OrganizationId equals o.Id   //into _r from r in _r.DefaultIfEmpty()
                               join stud in context.Student on x.StudentId equals stud.Id
                               join lp in context.LicenseProgram on stud.LicenseProgramId equals lp.Id
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
                                   stud.LicenseProgramId,
                                   lpName = lp.Code + "  " + st.Name + "  " + lp.Name + " [" + progt.Name + "]" + " [" + q.Name + "]",
                                   p.PracticeYear,
                               }).ToList();
                               //}).Distinct().ToList();

                    var ggr = lst.GroupBy(x => new { x.orgName, x.SPbGU, x.PracticeYear, x.LicenseProgramId, x.lpName }).ToList();

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

                    //List<string> lplist = gr.Select(x => x.Направление).Distinct().ToList();
                
                using (ExcelPackage doc = new ExcelPackage(newFile))
                {
                    //лист по направлениям подготовки
                    dgvXLS.DataSource = gr;
                    foreach (DataGridViewColumn col in dgvXLS.Columns)
                        col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");

                    ExcelWorksheet ws = doc.Workbook.Worksheets.Add("Направления подготовки");

                    Color lightGray = Color.FromName("LightGray");
                    Color darkGray = Color.FromName("DarkGray");

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

                    //лист по организациям
                    dgvXLS.DataSource = orggr;
                    foreach (DataGridViewColumn col in dgvXLS.Columns)
                        col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");

                    ws = doc.Workbook.Worksheets.Add("Организации");

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
                                    rowshift++;
                                    colind++;
                                    foreach (DataGridViewColumn cl in dgvXLS.Columns)
                                    {
                                        if ((cl.HeaderText.ToString() != "Организация") && (cl.HeaderText.ToString() != "SPbGU"))
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
    }
}
