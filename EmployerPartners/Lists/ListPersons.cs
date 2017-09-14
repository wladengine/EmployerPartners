using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastMember;
using System.IO;
using OfficeOpenXml;
using EmployerPartners.EDMX;

namespace EmployerPartners
{
    public partial class ListPersons : Form
    {
        public int? DegreeId
        {
            get { return ComboServ.GetComboIdInt(cbDegree); }
            set { ComboServ.SetComboId(cbDegree, value); }
        }
        public int? RankId
        {
            get { return ComboServ.GetComboIdInt(cbRank); }
            set { ComboServ.SetComboId(cbRank, value); }
        }
        public int? RankHonoraryId
        {
            get { return ComboServ.GetComboIdInt(cbRankHonorary); }
            set { ComboServ.SetComboId(cbRankHonorary, value); }
        }
        public int? RankStateId
        {
            get { return ComboServ.GetComboIdInt(cbRankState); }
            set { ComboServ.SetComboId(cbRankState, value); }
        }
        public int? ActivityAreaId
        {
            get { return ComboServ.GetComboIdInt(cbActivityArea); }
            set { ComboServ.SetComboId(cbActivityArea, value); }
        }
        public int? CountryId
        {
            get { return ComboServ.GetComboIdInt(cbCountry); }
            set { ComboServ.SetComboId(cbCountry, value); }
        }
        public int? RegionId
        {
            get { return ComboServ.GetComboIdInt(cbRegion); }
            set { ComboServ.SetComboId(cbRegion, value); }
        }
        public int? RubricId
        {
            get { return ComboServ.GetComboIdInt(cbRubric); }
            set { ComboServ.SetComboId(cbRubric, value); }
        }
        public int? FacultyId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
        }
        public bool isGAK
        {
            get { return chbGAK.Checked && chbGAK.Enabled; }
            set { chbGAK.Checked = value; }
        }
        public bool isGAKChairMan
        {
            get { return chbGAKChairman.Checked && chbGAKChairman.Enabled; }
            set { chbGAKChairman.Checked = value; }
        }
        public bool isGAK2016
        {
            get { return chbGAK2016.Checked && chbGAK2016.Enabled; }
            set { chbGAK2016.Checked = value; }
        }
        public bool isGAKChairMan2016
        {
            get { return chbGAKChairman2016.Checked && chbGAKChairman2016.Enabled; }
            set { chbGAKChairman2016.Checked = value; }
        }
        public bool isGAKMemberOrChairman
        {
            get { return chbIsGAK2017MemberChairman.Checked; }
            set { chbIsGAK2017MemberChairman.Checked = value; }
        }

        public bool isGAK2016MemberOrChairman
        {
            get { return chbIsGAK2016MemberChairman.Checked; }
            set { chbIsGAK2016MemberChairman.Checked = value; }
        }
        public ListPersons()
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            this.Text = "Список физических лиц";
            FillCard();
            FillGridStart();
            SetAccessRight();
        }
        private void SetAccessRight()
        {
            if (Util.IsOrgPersonWrite())
            {
                btnAddPartner.Enabled = true;
            }
            if (Util.IsSuperUser())
            {
                groupBoxGAK.Visible = true;
            }
            btnSendLetter.Enabled = Util.IsLetterCreator();
        }
        private void FillCard()
        {
            ComboServ.FillCombo(cbDegree, HelpClass.GetComboListByTable("dbo.Degree"), false, true);
            ComboServ.FillCombo(cbRank, HelpClass.GetComboListByTable("dbo.Rank"), false, true);
            ComboServ.FillCombo(cbRankHonorary, HelpClass.GetComboListByTable("dbo.RankHonorary"), false, true);
            ComboServ.FillCombo(cbRankState, HelpClass.GetComboListByTable("dbo.RankState"), false, true);
            ComboServ.FillCombo(cbActivityArea, HelpClass.GetComboListByTable("dbo.ActivityArea"), false, true);
            ComboServ.FillCombo(cbCountry, HelpClass.GetComboListByTable("dbo.Country"), false, true);
            ComboServ.FillCombo(cbRegion, HelpClass.GetComboListByTable("dbo.Region"), false, true);
            ComboServ.FillCombo(cbRubric, HelpClass.GetComboListByTable("dbo.Rubric"), false, true);
            ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("dbo.Faculty"), false, true);
        }
        private void FillGrid()
        {
            FillGrid(null);
        }

        private void FillGrid(int? id)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var PP_Rubric = context.PartnerPersonRubric.Where(x => x.RubricId == RubricId).Select(x => x.PartnerPersonId);
                var PP_Faculty = context.PartnerPersonFaculty.Where(x => x.FacultyId == FacultyId).Select(x => x.PartnerPersonId);
                
                var lst = (from org in context.PartnerPerson
                           
                           where
                           (DegreeId.HasValue ? org.DegreeId == DegreeId : true) &&
                           (RankId.HasValue ? org.RankId == RankId : true) &&
                           (RankHonoraryId.HasValue ? org.RankHonoraryId == RankHonoraryId : true) &&
                           (RankStateId.HasValue ? org.RankStateId == RankStateId : true) &&
                           (ActivityAreaId.HasValue ? org.ActivityAreaId == ActivityAreaId : true) &&
                           (CountryId.HasValue ? org.CountryId == CountryId : true) &&
                           (RegionId.HasValue ? org.RegionId == RegionId : true) &&
                           
                           (RubricId.HasValue ? PP_Rubric.Contains(org.Id) : true) &&
                           (FacultyId.HasValue ? PP_Faculty.Contains(org.Id) : true) &&
                           ((isGAKMemberOrChairman) ? ((org.IsGAK ?? false) || (org.IsGAKChairman ?? false)) : true) &&
                           ((isGAK2016MemberOrChairman) ? ((org.IsGAK2016 ?? false) || (org.IsGAKChairman2016 ?? false)) : true) &&
                           ((isGAK) ? (org.IsGAK ?? false) : true) &&
                           ((isGAKChairMan) ? (org.IsGAKChairman ?? false)  : true) &&
                           ((isGAK2016) ? (org.IsGAK2016?? false) : true) &&
                           ((isGAKChairMan2016) ? (org.IsGAKChairman2016 ?? false) : true) 

                           orderby org.Name 
                           select new
                           {
                               ФИО = org.Name,
                               org.Id,
                               ФИО_англ = org.NameEng,
                               Префикс = org.PartnerPersonPrefix.Name,
                               Регистрационный_номер_ИС_Партнеры  = org.Account,
                               Ученая_степень = org.Degree.Name,
                               Ученое_звание = org.Rank.Name,
                               Почетное_звание = org.RankHonorary.Name,
                               Государственное_или_военное_звание = org.RankState.Name,
                               Регалии_доп_данные = org.Title,
                               Письмо_отправлено = false,
                               Входит_в_составы_ГЭК_2017 = org.IsGAK,
                               Председатель_ГЭК_2017 = org.IsGAKChairman,
                               Входит_в_составы_ГЭК_2016 = org.IsGAK2016,
                               Председатель_ГЭК_2016 = org.IsGAKChairman2016,
                               Получено_согласие_на_персон_данные = org.IsPersonDataAgreed,
                               Персональные_данные_проверены = org.IsPersonDataChecked,
                               Основная_сфера_деятельности = org.ActivityArea.Name,
                               Выпускник_СПбГУ = org.IsSPbGUGraduate.HasValue && org.IsSPbGUGraduate.Value ? "да" : "нет",
                               Год_выпуска = org.SPbGUGraduateYear,
                               Ассоциация_выпускников = org.AlumniAssociation.HasValue && org.AlumniAssociation.Value ? "да" : "нет",
                               Страна = org.Country.Name,
                               Email = org.Email,
                               Телефон = org.Phone,
                               Мобильный_телефон = org.Mobiles,
                               Web_сайт = org.WebSite,
                               Комментарий = org.Comment,
                               
                           }).Distinct().OrderBy(x => x.ФИО).ToList();

                DataTable dt = new DataTable();
                dt = Utilities.ConvertToDataTable(lst);

                if (chbShowColumnLetter.Checked)
                {
                    dgv.CellValueChanged += dgv_CellValueChanged;
                    foreach (DataRow  rw in dt.Rows)
                    {
                        int Id = rw.Field<int>("Id");
                        rw.SetField("Письмо_отправлено", context.PartnerPersonOrganizationLetter.Where(x => x.PartnerPersonId == Id && x.IsSend).Count() > 0);
                    }
                }
                else
                    dgv.CellValueChanged -= dgv_CellValueChanged;

                bindingSource1.DataSource = dt;
                dgv.DataSource = bindingSource1;

                foreach (string s in new List<string>() { "Id" })
                    if (dgv.Columns.Contains(s))
                        dgv.Columns[s].Visible = false;
                if (dgv.Columns.Contains("Письмо_отправлено"))
                {
                    dgv.Columns["Письмо_отправлено"].Visible = chbShowColumnLetter.Checked;
                    dgv.Columns["Письмо_отправлено"].DisplayIndex = dgv.Columns["Регалии_доп_данные"].DisplayIndex+1;
                }
                
                foreach (DataGridViewColumn col in dgv.Columns)
                    col.HeaderText = col.Name.Replace("_", " ");
                try
                {
                    dgv.Columns["ФИО"].Frozen = true;
                    dgv.Columns["ФИО"].Width = 200;
                    dgv.Columns["Префикс"].Width = 60;
                    dgv.Columns["Ученая_степень"].Width = 150;
                    dgv.Columns["Ученое_звание"].Width = 130;
                    dgv.Columns["Почетное_звание"].Width = 130;
                    dgv.Columns["Государственное_или_военное_звание"].Width = 130;
                    dgv.Columns["Регалии_доп_данные"].Width = 130;
                }
                catch (Exception)
                {
                }

                
                if (chbShowColumnLetter.Checked)
                {
                    dgv.ReadOnly = false;
                    foreach (DataGridViewRow rw in dgv.Rows)
                    {
                        rw.ReadOnly = false;
                        foreach (DataGridViewCell cell in rw.Cells)
                        {
                            cell.ReadOnly = true;
                        }
                        rw.Cells["Письмо_отправлено"].ReadOnly =  !Util.IsLetterCreator();
                    }
                }
                
            }
        }

        private void FillGridStart()
        {
            dgv.CellValueChanged -= dgv_CellValueChanged;
            FillGridStart(null);
        }
        private void FillGridStart(int? id)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from org in context.PartnerPerson
                           join r in context.PartnerPersonRubric on org.Id equals r.PartnerPersonId into _r
                           from r in _r.DefaultIfEmpty()
                           join f in context.PartnerPersonFaculty on org.Id equals f.PartnerPersonId into _f
                           from f in _f.DefaultIfEmpty()
                           orderby org.Name
                           select new
                           {
                               ФИО = org.Name,
                               org.Id,
                               ФИО_англ = org.NameEng,
                               Префикс = org.PartnerPersonPrefix.Name,
                               Регистрационный_номер_ИС_Партнеры = org.Account,
                               Ученая_степень = org.Degree.Name,
                               Ученое_звание = org.Rank.Name,
                               Почетное_звание = org.RankHonorary.Name,
                               Государственное_или_военное_звание = org.RankState.Name,
                               Регалии_доп_данные = org.Title,
                               Входит_в_составы_ГЭК_2017 = org.IsGAK,
                               Председатель_ГЭК_2017 = org.IsGAKChairman,
                               Входит_в_составы_ГЭК_2016 = org.IsGAK2016,
                               Председатель_ГЭК_2016 = org.IsGAKChairman2016,
                               Получено_согласие_на_персон_данные = org.IsPersonDataAgreed,
                               Персональные_данные_проверены = org.IsPersonDataChecked,
                               Основная_сфера_деятельности = org.ActivityArea.Name,
                               Выпускник_СПбГУ = org.IsSPbGUGraduate.HasValue && org.IsSPbGUGraduate.Value ? "да" : "нет",
                               Год_выпуска = org.SPbGUGraduateYear,
                               Ассоциация_выпускников = org.AlumniAssociation.HasValue && org.AlumniAssociation.Value ? "да" : "нет",
                               Страна = org.Country.Name,
                               Email = org.Email,
                               Телефон = org.Phone,
                               Мобильный_телефон = org.Mobiles,
                               Web_сайт = org.WebSite,
                               Комментарий = org.Comment,
                           }).Distinct().OrderBy(x => x.ФИО).ToList();

                DataTable dt = new DataTable();
                dt = Utilities.ConvertToDataTable(lst);
                bindingSource1.DataSource = dt;
                dgv.DataSource = bindingSource1;

                List<string> Cols = new List<string>() { "Id" };

                foreach (string s in Cols)
                    if (dgv.Columns.Contains(s))
                        dgv.Columns[s].Visible = false;
                foreach (DataGridViewColumn col in dgv.Columns)
                    col.HeaderText = col.Name.Replace("_", " ");
                try
                {
                    dgv.Columns["ФИО"].Frozen = true;
                    dgv.Columns["ФИО"].Width = 200;
                    dgv.Columns["Префикс"].Width = 60;
                    dgv.Columns["Ученая_степень"].Width = 150;
                    dgv.Columns["Ученое_звание"].Width = 130;
                    dgv.Columns["Почетное_звание"].Width = 130;
                    dgv.Columns["Государственное_или_военное_звание"].Width = 130;
                    dgv.Columns["Регалии_доп_данные"].Width = 130;
                }
                catch (Exception)
                {
                }

                if (id.HasValue)
                    foreach (DataGridViewRow rw in dgv.Rows)
                        if (rw.Cells[0].Value.ToString() == id.Value.ToString())
                        {
                            dgv.CurrentCell = rw.Cells["ФИО"];
                            break;
                        }
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentRow.Index>=0)
                {
                    int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                    if (Utilities.PersonCardIsOpened(id))
                        return;
                    new CardPerson(id, new UpdateIntHandler(FillGrid)).Show();
                }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentCell.RowIndex >= 0)
                {
                    int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                    if (Utilities.PersonCardIsOpened(id))
                        return;
                    new CardPerson(id, new UpdateIntHandler(FillGrid)).Show();
                }
        }

        private void btnAddPartner_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form frm in Application.OpenForms)
                    if (frm is CardNewPerson)
                    {
                        frm.Activate();
                        return;
                    }
            }
            catch (Exception)
            {
            }
            //new CardPartner(null, new UpdateVoidHandler(FillCard)).Show();
            new CardNewPerson(new UpdateIntHandler(FillGrid)).Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            int? id = null;
            if (dgv.CurrentCell != null)
                if (dgv.CurrentRow.Index >= 0)
                {
                    id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                }
            FillGrid(id);
        }

        private void btnXLS_Click(object sender, EventArgs e)
        {
            try
            {
                int quan = 0;
                quan = dgv.Rows.Count;
                if (quan > 100)
                {
                    if (MessageBox.Show("Предполагается экспорт в Excel " + quan + " записей.\r\n" + "Это может занять некоторое время..\r\n" +
                        "Продолжить ?", "Запрос на подтверждение",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                        return;
                }
            }
            catch (Exception)
            {
            }
            ToExcel();
        }

        private void ToExcel()
        {
            try
            {
                string filenameDate = "Список физических лиц";
                string filename = Util.TempFilesFolder + filenameDate + ".xlsx";
                string[] fileList = Directory.GetFiles(Util.TempFilesFolder, "Список физических лиц*" + ".xlsx");

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

                using (ExcelPackage doc = new ExcelPackage(newFile))
                {
                    ExcelWorksheet ws = doc.Workbook.Worksheets.Add("Физические лица");
                    Color lightGray = Color.FromName("LightGray");
                    Color darkGray = Color.FromName("DarkGray");

                    int colind = 0;

                    foreach (DataGridViewColumn cl in dgv.Columns)
                    {
                        ws.Cells[1, ++colind].Value = cl.HeaderText.ToString();
                        ws.Cells[1, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                        ws.Cells[1, colind].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[1, colind].Style.Fill.BackgroundColor.SetColor(lightGray);
                    }

                    for (int rwInd = 0; rwInd < dgv.Rows.Count; rwInd++)
                    {
                        DataGridViewRow rw = dgv.Rows[rwInd];
                        int colInd = 0;
                        foreach (DataGridViewCell cell in rw.Cells)
                        {
                            if (cell.Value == null)
                            {
                                ws.Cells[rwInd + 2, colInd + 1].Value = "";
                            }
                            else
                            {
                                ws.Cells[rwInd + 2, colInd + 1].Value = cell.Value; //.ToString(); 
                            }
                            ws.Cells[rwInd + 2, colInd + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                            colInd++;
                        }
                    }

                    //форматирование
                    int clmnInd = 0;
                    foreach (DataGridViewColumn clmn in dgv.Columns)
                    {
                        if (clmn.Name == "ФИО")
                            //ws.Column(++clmnInd).Width = 100;
                            ws.Column(++clmnInd).AutoFit();
                        else if (clmn.Name == "Id")
                            ws.Column(++clmnInd).Width = 0;
                       // else
                           // ws.Column(++clmnInd).AutoFit();
                    }
                    doc.Save();
                }
                System.Diagnostics.Process.Start(filename);
            }
            catch
            {
            }
        }

        private void ToExcelGAK(int year)
        {
            try
            {
                string filenameDate = "Выгрузка составов ГЭК";
                string filename = Util.TempFilesFolder + filenameDate + ".xlsx";
                string[] fileList = Directory.GetFiles(Util.TempFilesFolder, "Выгрузка составов ГЭК*" + ".xlsx");

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

                if (year == 2016)
                {
                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        var lst = (from x in context.ECD_EmpContactsDetailsUNP_GAK2016
                                   orderby x.Name
                                   select new
                                   {
                                       СПбГУ = x.SPbGU,
                                       Председатель = x.Chairman,
                                       Обращение = x.Title,
                                       ФИО = x.Name,
                                       Имя_англ = x.FirstNameEng,
                                       Фамилия_англ = x.LastNameEng,
                                       Должность = x.Position,
                                       Должность_англ = x.PositionEng,
                                       Вид_проф_деятельности = x.AAP,
                                       Вид_проф_деятельности_англ = x.AAPEng,
                                       Организация = x.OrgName,
                                       Организация_англ = x.OrgNameEng,
                                       Страна = x.CountryName,
                                       Страна_англ = x.CountryNameEng,
                                       Email = x.Email,
                                       Телефон = x.Phone,
                                       УНП = x.UNP
                                   }).ToList();

                        dgvGAK.DataSource = lst;

                        foreach (DataGridViewColumn col in dgvGAK.Columns)
                            col.HeaderText = col.Name.Replace("_", " ");
                    }
                }
                if (year == 2017)
                {
                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        var lst = (from x in context.ECD_EmpContactsDetailsUNP_GAK2017
                                   orderby x.Name
                                   select new
                                   {
                                       СПбГУ = x.SPbGU,
                                       Председатель = x.Chairman,
                                       Обращение = x.Title,
                                       ФИО = x.Name,
                                       Имя_англ = x.FirstNameEng,
                                       Фамилия_англ = x.LastNameEng,
                                       Должность = x.Position,
                                       Должность_англ = x.PositionEng,
                                       Вид_проф_деятельности = x.AAP,
                                       Вид_проф_деятельности_англ = x.AAPEng,
                                       Организация = x.OrgName,
                                       Организация_англ = x.OrgNameEng,
                                       Страна = x.CountryName,
                                       Страна_англ = x.CountryNameEng,
                                       Email = x.Email,
                                       Телефон = x.Phone,
                                       УНП = x.UNP
                                   }).ToList();

                        dgvGAK.DataSource = lst;

                        foreach (DataGridViewColumn col in dgvGAK.Columns)
                            col.HeaderText = col.Name.Replace("_", " ");
                    }
                }

                using (ExcelPackage doc = new ExcelPackage(newFile))
                {
                    ExcelWorksheet ws = doc.Workbook.Worksheets.Add("Составы ГЭК");
                    Color lightGray = Color.FromName("LightGray");
                    Color darkGray = Color.FromName("DarkGray");

                    int colind = 0;

                    foreach (DataGridViewColumn cl in dgvGAK.Columns)
                    {
                        ws.Cells[1, ++colind].Value = cl.HeaderText.ToString();
                        ws.Cells[1, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                        ws.Cells[1, colind].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[1, colind].Style.Fill.BackgroundColor.SetColor(lightGray);
                    }

                    for (int rwInd = 0; rwInd < dgvGAK.Rows.Count; rwInd++)
                    {
                        DataGridViewRow rw = dgvGAK.Rows[rwInd];
                        int colInd = 0;
                        foreach (DataGridViewCell cell in rw.Cells)
                        {
                            if (cell.Value == null)
                            {
                                ws.Cells[rwInd + 2, colInd + 1].Value = "";
                            }
                            else
                            {
                                ws.Cells[rwInd + 2, colInd + 1].Value = cell.Value; //.ToString(); 
                            }
                            ws.Cells[rwInd + 2, colInd + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                            colInd++;
                        }
                    }

                    //форматирование
                    //int clmnInd = 0;
                    //foreach (DataGridViewColumn clmn in dgv.Columns)
                    //{
                    //    clmnInd++;
                    //    if (clmn.Name == "ФИО")
                    //    {
                    //        ws.Column(clmnInd).AutoFit();
                    //    }
                    //    //if (clmn.Name == "ФИО")
                    //    //    //ws.Column(++clmnInd).Width = 100;
                    //    //    ws.Column(++clmnInd).AutoFit();
                    //    //else if (clmn.Name == "Id")
                    //    //    ws.Column(++clmnInd).Width = 0;
                    //    //else
                    //    //    ws.Column(++clmnInd).AutoFit();
                    //}
                    doc.Save();
                }
                System.Diagnostics.Process.Start(filename);
            }
            catch
            {
            }
        }

        private void ListPersons_Load(object sender, EventArgs e)
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
                    for (int j = 0; j < 9 /*dgv.Columns.Count*/; j++)
                    {
                        if (j == 1)
                            continue;
                        if (dgv[j, i].ColumnIndex == 0)
                        {
                            int length = 1;
                            length = dgv[j, i].Value.ToString().Length;
                            length = (length <= 15) ? length : 15;
                            if (dgv[j, i].Value.ToString().Substring(0, length).ToUpper().Contains(search))
                            {
                                dgv.CurrentCell = dgv[j, i];
                                exit = true;
                                break;
                            }
                        }
                        else
                        {
                            if (dgv[j, i].Value.ToString().ToUpper().Contains(search))
                            {
                                dgv.CurrentCell = dgv[j, i];
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (rbtn2016.Checked)
            {
                ToExcelGAK(2016);
                return;
            }
            if (rbtn2017.Checked)
            {
                ToExcelGAK(2017);
                return;
            }
            
        }

        private void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void chbGAK_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chbGAKChairman_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chbGAK2016_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chbGAKChairman2016_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cbRubric_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void btnSendLetter_Click(object sender, EventArgs e)
        {
            List<int> PartnerPersonlst = new List<int>();
            foreach (DataGridViewRow rw in dgv.Rows)
            {
                PartnerPersonlst.Add((int)rw.Cells["Id"].Value);
            }
            new CardPersonOrganizationLetter(PartnerPersonlst).Show();
        }
        
        private void chbIsGAK2017MemberChairman_CheckedChanged(object sender, EventArgs e)
        {
            chbGAK.Enabled = chbGAKChairman.Enabled = chbIsGAK2017MemberChairman.Checked;
        }

        private void chbIsGAK2016MemberChairman_CheckedChanged(object sender, EventArgs e)
        {
            chbGAK2016.Enabled = chbGAKChairman2016.Enabled = chbIsGAK2016MemberChairman.Checked;
        }

        private void dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.Columns.Contains("Письмо_отправлено"))
            {
                if (dgv.CurrentCell == null) return;
                if (dgv.CurrentCell.RowIndex < 0) return;
                if (dgv.CurrentCell.ColumnIndex != dgv.Columns["Письмо_отправлено"].Index) return;
                int PartnerPersonId = (int)dgv.CurrentRow.Cells["Id"].Value;
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    List<PartnerPersonOrganizationLetter> lst = context.PartnerPersonOrganizationLetter.Where(x => x.PartnerPersonId == PartnerPersonId).ToList();
                    if (lst.Count > 0)
                    {
                        foreach (var x in lst)
                        {
                            x.IsSend = (bool)dgv.CurrentRow.Cells["Письмо_отправлено"].Value;
                            x.IsSendAuthor = Util.GetUserName();
                            x.Timestamp = DateTime.Now;
                        }
                        context.SaveChanges();
                    }
                }
            }
        }

        private void dgv_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dgv.Columns.Contains("Письмо_отправлено"))
            {
                if (dgv.CurrentCell == null) return;
                if (dgv.CurrentCell.RowIndex < 0) return;
                if (dgv.CurrentCell.ColumnIndex != dgv.Columns["Письмо_отправлено"].Index) return;
                dgv.EndEdit();
            }
        }


    }

}
