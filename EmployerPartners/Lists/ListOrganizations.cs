using FastMember;
using OfficeOpenXml;
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

namespace EmployerPartners
{
    public partial class ListOrganizations : Form
    {
        public int? OwnershipTypeId
        {
            get { return ComboServ.GetComboIdInt(cbOwnership); }
            set { ComboServ.SetComboId(cbOwnership, value); }
        }
        public int? ActivityGoalId
        {
            get { return ComboServ.GetComboIdInt(cbActivityGoal); }
            set { ComboServ.SetComboId(cbActivityGoal, value); }
        }
        public int? NationalAffiliationId
        {
            get { return ComboServ.GetComboIdInt(cbNationalityAffilation); }
            set { ComboServ.SetComboId(cbNationalityAffilation, value); }
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

        public ListOrganizations()
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            this.Text = "Список организаций";
            FillCard();
            FillGrid();
        }

        private void FillCard()
        {
            ComboServ.FillCombo(cbOwnership, HelpClass.GetComboListByTable("dbo.OwnershipType"), false, true);
            ComboServ.FillCombo(cbActivityGoal, HelpClass.GetComboListByTable("dbo.ActivityGoal"), false, true);
            ComboServ.FillCombo(cbActivityArea, HelpClass.GetComboListByTable("dbo.ActivityArea"), false, true);
            ComboServ.FillCombo(cbNationalityAffilation, HelpClass.GetComboListByTable("dbo.NationalAffiliation"), false, true);
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
                /*//////
                string sqlOrg = "SELECT Organization.*, ActivityArea.Name AS ActivityArea, ActivityGoal.Name AS ActivityGoal, NationalAffiliation.Name AS NationalAffiliation, OwnershipType.Name AS OwnershipType, Country.Name AS Country, Region.Name AS Region " +
                    " FROM Region RIGHT JOIN (Country RIGHT JOIN (OwnershipType RIGHT JOIN (NationalAffiliation RIGHT JOIN (ActivityGoal RIGHT JOIN (ActivityArea RIGHT JOIN Organization ON ActivityArea.Id = Organization.ActivityAreaId) ON ActivityGoal.Id = Organization.ActivityGoalId) ON NationalAffiliation.Id = Organization.NationalAffiliationId) ON OwnershipType.Id = Organization.OwnershipTypeId) ON Country.Id = Organization.CountryId) ON Region.Id = Organization.RegionId ";
                    
                string sqlWhere = ((RubricId.HasValue) ? (" Organization.Id in (select OrganizationId from OrganizationRubric where RubricId = " + RubricId + ")") : "");
                string sqlOrderBy = " ORDER BY Organization.Name";
                sqlWhere = (sqlWhere == "") ? "" : (" where " + sqlWhere);
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
                               Источник = org.Source,
                               ОКВЭД = org.Okved,
                               Форма_собственности = org.OwnershipType,
                               Цель_деятельности = org.ActivityGoal,
                               Национальная_принадлежность = org.NationalAffiliation,
                               Основная_сфера_деятельности = org.ActivityArea,

                               ИНН = org.INN,
                               org.Email,
                               Телефон = org.Phone,
                               Мобильный_телефон = org.Mobiles,
                               Факс = org.Fax,
                               Web_сайт = org.WebSite,
                               Страна = org.Country,
                               Регион = org.Region,
                               Город = org.City,
                               Улица = org.Street,
                               Дом = org.House,
                               Помещение = org.Apartment,
                               Комментарий = org.Comment,
                           }).ToList();
                //////*/

                //var lstr = context.OrganizationRubric.Where(x => x.RubricId == RubricId);
                /*List<int> lstr = (from or in context.OrganizationRubric
                            where or.RubricId == RubricId
                            select or.OrganizationId
                            ).ToList();*/
                           

                var lst = (from org in context.Organization
                           join r in context.OrganizationRubric on org.Id equals r.OrganizationId into _r
                           from or in _r.DefaultIfEmpty()
                           join f in context.OrganizationFaculty on org.Id equals f.OrganizationId into _f
                           from of in _f.DefaultIfEmpty()
                           where 
                           (OwnershipTypeId.HasValue ? org.OwnershipTypeId == OwnershipTypeId : true) &&
                           (ActivityGoalId.HasValue ? org.ActivityGoalId == ActivityGoalId : true) &&
                           (NationalAffiliationId.HasValue ? org.NationalAffiliationId == NationalAffiliationId : true) &&
                           (ActivityAreaId.HasValue ? org.ActivityAreaId == ActivityAreaId : true) &&
                           (CountryId.HasValue ? org.CountryId == CountryId : true) &&
                           (RegionId.HasValue ? org.RegionId == RegionId : true) &&
                           (RubricId.HasValue ? or.RubricId == RubricId : true) &&
                           (FacultyId.HasValue ? of.FacultyId == FacultyId : true)
                           //lstr.Contains(org.Id) 
                           orderby org.Name
                           select new
                           {
                               
                               Полное_наименование = org.Name,
                               org.Id,
                               Среднее_наименование = org.MiddleName,
                               Краткое_наименование = org.ShortName,
                               Наименование_англ = org.NameEng,
                               Краткое_наименование_англ = org.ShortNameEng,
                               Источник = org.Source,
                               ОКВЭД = org.Okved,
                               Форма_собственности = org.OwnershipType.Name, 
                               Цель_деятельности = org.ActivityGoal.Name,
                               Национальная_принадлежность = org.NationalAffiliation.Name,
                               Основная_сфера_деятельности = org.ActivityArea.Name,

                               ИНН = org.INN,
                               org.Email,
                               Телефон = org.Phone,
                               Мобильный_телефон = org.Mobiles,
                               Факс = org.Fax,
                               Web_сайт = org.WebSite,
                               Страна = org.Country.Name,
                               Регион = org.Region.Name,
                               Город = org.City,
                               Улица = org.Street,
                               Дом = org.House,
                               Помещение = org.Apartment,
                               Комментарий = org.Comment,

                           }).Distinct().OrderBy(x => x.Полное_наименование).ToList();
                
                //dgv.DataSource = lst;
                bindingSource1.DataSource = lst;
                dgv.DataSource = bindingSource1;

                List<string> Cols = new List<string>() { "Id" };

                foreach (string s in Cols)
                    if (dgv.Columns.Contains(s))
                        dgv.Columns[s].Visible = false;
                foreach (DataGridViewColumn col in dgv.Columns)
                    col.HeaderText = col.Name.Replace("_", " ");
                try
                {
                    dgv.Columns["Полное_наименование"].Frozen = true;
                    dgv.Columns["Полное_наименование"].Width = 285;
                    dgv.Columns["Среднее_наименование"].Width = 175;
                }
                catch (Exception)
                {
                }

                if (id.HasValue)
                    foreach (DataGridViewRow rw in dgv.Rows)
                        if (rw.Cells[0].Value.ToString() == id.Value.ToString())
                        {
                            dgv.CurrentCell = rw.Cells["Название"];
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
                    new CardOrganization(id, new UpdateVoidHandler(FillGrid)).Show();
                }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentCell.RowIndex >= 0)
                {
                    int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                    new CardOrganization(id, new UpdateVoidHandler(FillGrid)).Show();
                }
        }

        private void btnAddPartner_Click(object sender, EventArgs e)
        {
            //new CardPartner(null, new UpdateVoidHandler(FillCard)).Show();
            new CardNewOrganization(new UpdateVoidHandler(FillGrid)).Show();
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
            //FillGrid(null);
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void btnXLS_Click(object sender, EventArgs e)
        {
            ToExcel();
        }

        private void ToExcel()
        {
            try
            {
                string filenameDate = "Список организаций";
                string filename = Util.TempFilesFolder + filenameDate + ".xlsx";
                string[] fileList = Directory.GetFiles(Util.TempFilesFolder, "Список организацийм*" + ".xlsx");

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
                    ExcelWorksheet ws = doc.Workbook.Worksheets.Add("Организации");
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
                        if (clmn.Name == "Полное_наименование")
                            ws.Column(++clmnInd).Width = 100;
                        else if (clmn.Name == "Среднее_наименование")
                            ws.Column(++clmnInd).Width = 80;
                        else if (clmn.Name == "Краткое_наименование" || clmn.Name == "Наименование_англ" || clmn.Name == "Краткое_наименование_англ")
                            ws.Column(++clmnInd).Width = 50;
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

        private void ListOrganizations_Load(object sender, EventArgs e)
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
    }

}
