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

        public ListPersons()
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            this.Text = "Список физических лиц";
            FillCard();
            FillGrid();

            if (Util.IsReadOnlyAll())
            {
                btnAddPartner.Enabled = false;
            }
        }

        private void FillCard()
        {
            ComboServ.FillCombo(cbDegree, HelpClass.GetComboListByTable("dbo.Degree"), false, true);
            ComboServ.FillCombo(cbRank, HelpClass.GetComboListByTable("dbo.Rank"), false, true);
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
                var lst = (from org in context.PartnerPerson
                           join r in context.PartnerPersonRubric on org.Id equals r.PartnerPersonId into _r
                           from or in _r.DefaultIfEmpty()
                           join f in context.PartnerPersonFaculty on org.Id equals f.PartnerPersonId into _f
                           from of in _f.DefaultIfEmpty()
                           where 
                           (DegreeId.HasValue ? org.DegreeId == DegreeId : true) &&
                           (RankId.HasValue ? org.RankId == RankId : true) &&
                           (ActivityAreaId.HasValue ? org.ActivityAreaId == ActivityAreaId : true) &&
                           (CountryId.HasValue ? org.CountryId == CountryId : true) &&
                           (RegionId.HasValue ? org.RegionId == RegionId : true) &&
                           (RubricId.HasValue ? or.RubricId == RubricId : true) &&
                           (FacultyId.HasValue ? of.FacultyId == FacultyId : true)
                           orderby org.Name 
                           select new
                           {
                               ФИО = org.Name,
                               org.Id,
                               ФИО_англ = org.NameEng,
                               Регалии = org.Title,
                               Ученая_степень = org.Degree.Name,
                               Ученое_звание = org.Rank.Name,
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
                           }).ToList();

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
                    dgv.Columns["Регалии"].Width = 300;
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
                    new CardPerson(id, new UpdateVoidHandler(FillGrid)).Show();
                }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentCell.RowIndex >= 0)
                {
                    int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                    new CardPerson(id, new UpdateVoidHandler(FillGrid)).Show();
                }
        }

        private void btnAddPartner_Click(object sender, EventArgs e)
        {
            //new CardPartner(null, new UpdateVoidHandler(FillCard)).Show();
            new CardNewPerson(new UpdateVoidHandler(FillGrid)).Show();
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
                        if (clmn.Name == "Регалии")
                            ws.Column(++clmnInd).Width = 100;
                        else if (clmn.Name == "Id")
                            ws.Column(++clmnInd).Width = 0;
                        else
                            ws.Column(++clmnInd).AutoFit();
                    }
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
                    for (int j = 0; j < 4 /*dgv.Columns.Count*/; j++)
                    {
                        if (j == 1)
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
