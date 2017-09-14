using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OfficeOpenXml;
using WordOut;
using FastMember;
using EmployerPartners.EDMX;

namespace EmployerPartners
{
    public partial class CardOrganizationStat : Form
    {
        public int _Count;

        public CardOrganizationStat()
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            FillCard();
        }
        #region Fill
        public void FillCard()
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                _Count = (from x in context.Organization
                         select x.Id).Count();
                tbOrgCount.Text = _Count.ToString();

                FillActivityAreaProfessional2(context);
                //FillActivityArea(context);
                FillOwnership(context);
                FillActivityGoal(context);
                FillRubrics(context);
                FillFaculty(context);
                FillNatAffil(context);
                FillCountry(context);
                FillLP(context);
            }
        }

        public void FillActivityArea(EmployerPartnersEntities context)
        {
            var lst = (from x in context.OrganizationActivityArea
                       join a in context.ActivityArea on x.ActivityAreaId equals a.Id
                       select new
                       {
                           a.Id,
                           a.Name,
                           x.OrganizationId,
                       }).Distinct().ToList();
            int cnt = lst.Select(x => x.OrganizationId).Distinct().Count();
            lblActivityArea.Text = cnt.ToString() + "/" + (_Count - cnt).ToString();
            
            var gr = (from l in lst
                      group l by l.Id into l
                      orderby l.Count() descending, l.First().Name
                      select new
                      {
                          Ключевое_слово = l.First().Name,
                          Кол__во_организаций = l.Count(),
                      }).ToList();
                      //}).OrderByDescending(x => x.Кол__во_организаций).ToList();

            DataTable dt = new DataTable();
            dt = Utilities.ConvertToDataTable(gr);
            dgvActivityArea.DataSource = dt;
            foreach (DataGridViewColumn col in dgvActivityArea.Columns)
                col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
        }
        public void FillActivityAreaProfessional(EmployerPartnersEntities context)
        {
            //расчет по таблице OrganizationActivityAreaProfessional
            var lst = (from x in context.OrganizationActivityAreaProfessional
                       join a in context.ActivityAreaProfessional on x.ActivityAreaProfessionalId equals a.Id
                       select new
                       {
                           a.Id,
                           a.Code,
                           a.Name,
                           x.OrganizationId,
                       }).Distinct().ToList();
            int cnt = lst.Select(x => x.OrganizationId).Distinct().Count();
            lblActivityArea.Text = cnt.ToString() + "/" + (_Count - cnt).ToString();

            var gr = (from l in lst
                      group l by l.Id into l
                      orderby l.First().Id  //l.Count() descending, l.First().Name
                      select new
                      {
                          Код = l.First().Code,
                          Область_деятельности = l.First().Name,
                          Кол__во_организаций = l.Count(),
                      }).ToList();
            //}).OrderByDescending(x => x.Кол__во_организаций).ToList();

            DataTable dt = new DataTable();
            dt = Utilities.ConvertToDataTable(gr);
            dgvActivityArea.DataSource = dt;
            foreach (DataGridViewColumn col in dgvActivityArea.Columns)
                col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
            try
            {
                dgvActivityArea.Columns["Код"].Width = 50;
                dgvActivityArea.Columns["Область_деятельности"].Width = 200;
                dgvActivityArea.Columns["Кол__во_организаций"].Width = 80;
            }
            catch (Exception)
            {
            }
        }
        public void FillActivityAreaProfessional2(EmployerPartnersEntities context)
        {
            //расчет по таблице Organization (по полю )
            var lst = (from x in context.Organization
                       join a in context.ActivityAreaProfessional on x.ActivityAreaProfessionalId equals a.Id
                       select new
                       {
                           a.Id,
                           a.Code,
                           a.Name,
                           OrganizationId = x.Id,
                       }).Distinct().ToList();
            int cnt = lst.Select(x => x.OrganizationId).Distinct().Count();
            lblActivityArea.Text = cnt.ToString() + "/" + (_Count - cnt).ToString();

            var gr = (from l in lst
                      group l by l.Id into l
                      orderby l.First().Id  //l.Count() descending, l.First().Name
                      select new
                      {
                          Код = l.First().Code,
                          Область_деятельности = l.First().Name,
                          Кол__во_организаций = l.Count(),
                      }).ToList();
            //}).OrderByDescending(x => x.Кол__во_организаций).ToList();

            DataTable dt = new DataTable();
            dt = Utilities.ConvertToDataTable(gr);
            dgvActivityArea.DataSource = dt;
            foreach (DataGridViewColumn col in dgvActivityArea.Columns)
                col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
            try
            {
                dgvActivityArea.Columns["Код"].Width = 50;
                dgvActivityArea.Columns["Область_деятельности"].Width = 200;
                dgvActivityArea.Columns["Кол__во_организаций"].Width = 80;
            }
            catch (Exception)
            {
            }
        }
        public void FillOwnership(EmployerPartnersEntities context)
        {
            var lst = (from x in context.Organization
                       join a in context.OwnershipType on x.OwnershipTypeId equals a.Id
                       select new
                       {
                           a.Id,
                           a.Name,
                           OrgId = x.Id,
                       }).Distinct().ToList();
            int cnt = lst.Select(x => x.OrgId).Distinct().Count();
            lblOwner.Text = cnt.ToString() + "/" + (_Count - cnt).ToString();
            
            var gr = (from l in lst
                      group l by l.Id into l
                      select new
                      {
                          Форма_собственности = l.First().Name,
                          Кол__во_организаций = l.Count(),
                      }).OrderByDescending(x => x.Кол__во_организаций).ToList();


            DataTable dt = new DataTable();
            dt = Utilities.ConvertToDataTable(gr);
            dgvOwner.DataSource = dt;

            foreach (DataGridViewColumn col in dgvOwner.Columns)
                col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
        }
        public void FillActivityGoal(EmployerPartnersEntities context)
        {
            var lst = (from x in context.Organization
                       join a in context.ActivityGoal on x.ActivityGoalId equals a.Id
                       select new
                       {
                           a.Id,
                           a.Name,
                           OrgId = x.Id,
                       }).Distinct().ToList();
            int cnt = lst.Select(x => x.OrgId).Distinct().Count();
            lblAcivityGoal.Text = cnt.ToString() + "/" + (_Count - cnt).ToString();
            
            var gr = (from l in lst
                      group l by l.Id into l
                      select new
                      {
                          Цель_деятельности = l.First().Name,
                          Кол__во_организаций = l.Count(),
                      }).OrderByDescending(x => x.Кол__во_организаций).ToList();


            DataTable dt = new DataTable();
            dt = Utilities.ConvertToDataTable(gr);
            dgvActivityGoal.DataSource = dt;
            foreach (DataGridViewColumn col in dgvActivityGoal.Columns)
                col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
        }
        public void FillRubrics(EmployerPartnersEntities context)
        {
            var lst = (from x in context.OrganizationRubric
                       join a in context.Rubric on x.RubricId equals a.Id
                       select new
                       {
                           a.Id,
                           a.Name,
                           x.OrganizationId,
                       }).Distinct().ToList();
            int cnt = lst.Select(x => x.OrganizationId).Distinct().Count();
            lblRubrics.Text = cnt.ToString() + "/" + (_Count - cnt).ToString();
            
            var gr = (from l in lst
                      group l by l.Id into l
                      select new
                      {
                          Рубрика = l.First().Name,
                          Кол__во_организаций = l.Count(),
                      }).OrderByDescending(x => x.Кол__во_организаций).ToList();


            DataTable dt = new DataTable();
            dt = Utilities.ConvertToDataTable(gr);
            dgvRubrics.DataSource = dt;
            foreach (DataGridViewColumn col in dgvRubrics.Columns)
                col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
        }
        public void FillFaculty(EmployerPartnersEntities context)
        {
            var lst = (from x in context.OrganizationFaculty
                       join a in context.Faculty on x.FacultyId equals a.Id
                       join r in context.Rubric on x.RubricId equals r.Id into _r
                       from r in _r.DefaultIfEmpty()

                       select new
                       {
                           a.Id,
                           a.Name,
                           RubricName = r.ShortName,
                           x.OrganizationId,
                       }).Distinct().ToList();
            int cnt = lst.Select(x => x.OrganizationId).Distinct().Count();
            lblFaculty.Text = cnt.ToString() + "/" + (_Count - cnt).ToString();

            var ggr = lst.GroupBy(x => new { x.RubricName, x.Name })
                .ToList();


            var gr = (from l in ggr
                      select new
                      {
                          Рубрика = l.First().RubricName,
                          Направление = l.First().Name,
                          Кол__во_организаций = l.Count(),
                      }).OrderByDescending(x => x.Направление).ThenByDescending(x => x.Кол__во_организаций).ToList();

            DataTable dt = new DataTable();
            dt = Utilities.ConvertToDataTable(gr);
            dgvFaculty.DataSource = dt;
            foreach (DataGridViewColumn col in dgvFaculty.Columns)
                col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
        }
        public void FillNatAffil(EmployerPartnersEntities context)
        {
            var lst = (from x in context.Organization
                       join a in context.NationalAffiliation on x.NationalAffiliationId equals a.Id
                       select new
                       {
                           a.Id,
                           a.Name,
                           OrgId = x.Id,
                       }).Distinct().ToList();
            int cnt = lst.Select(x => x.OrgId).Distinct().Count();
            lblNatAffiliation.Text = cnt.ToString() + "/" + (_Count - cnt).ToString();
            
            var gr = (from l in lst
                      group l by l.Id into l
                      select new
                      {
                          Национальная_принадлежность = l.First().Name,
                          Кол__во_организаций = l.Count(),
                      }).OrderByDescending(x => x.Кол__во_организаций).ToList();


            DataTable dt = new DataTable();
            dt = Utilities.ConvertToDataTable(gr);
            dgvNatAffil.DataSource =  dt;
            foreach (DataGridViewColumn col in dgvNatAffil.Columns)
                col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
        }
        public void FillCountry(EmployerPartnersEntities context)
        {
            var lst = (from x in context.Organization
                       join a in context.Country on x.CountryId equals a.Id
                       select new
                       {
                           a.Id,
                           a.Name,
                           OrgId = x.Id,
                       }).Distinct().ToList();
            int cnt = lst.Select(x => x.OrgId).Distinct().Count();
            lblCountry.Text = cnt.ToString() + "/" + (_Count - cnt).ToString();
            
            var gr = (from l in lst
                      group l by l.Id into l
                      select new
                      {
                          Страна = l.First().Name,
                          Кол__во_организаций = l.Count(),
                      }).OrderByDescending(x => x.Кол__во_организаций).ToList();

            DataTable dt = new DataTable();
            dt = Utilities.ConvertToDataTable(gr);
            dgvCountry.DataSource = dt;
            foreach (DataGridViewColumn col in dgvCountry.Columns)
                col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
        }
        public void FillLP(EmployerPartnersEntities context)
        {
            var lst = (from x in context.OrganizationLP
                       join a in context.LicenseProgram on x.LicenseProgramId equals a.Id
                       join r in context.Rubric on x.RubricId equals r.Id into _r
                       from r in _r.DefaultIfEmpty()

                       select new
                       {
                           a.Id,
                           a.Code,
                           Level = a.StudyLevel.Name,
                           a.Name,
                           Ptype = a.ProgramType.Name,
                           Qual = a.Qualification.Name,
                           RubricName = r.ShortName,
                           x.OrganizationId,
                       }).Distinct().ToList();
            int cnt = lst.Select(x => x.OrganizationId).Distinct().Count();
            lblLP.Text = cnt.ToString() + "/" + (_Count - cnt).ToString();
            
            var gr = (from l in lst
                      group l by l.Id into l
                      select new
                      {
                          Рубрика = l.First().RubricName,
                          Код = l.First().Code,
                          Уровень = l.First().Level,
                          Направление = l.First().Name,
                          Тип_программы = l.First().Ptype,
                          Квалификация = l.First().Qual,
                          Кол__во_организаций = l.Count(),
                      }).OrderByDescending(x => x.Кол__во_организаций).ToList();

            DataTable dt = new DataTable();
            dt = Utilities.ConvertToDataTable(gr);
            dgvLP.DataSource = dt;
            foreach (DataGridViewColumn col in dgvLP.Columns)
                col.HeaderText = col.HeaderText.Replace("__", "-").Replace("_", " ");
        }

        #endregion
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FillCard();
        }

        private void btnXLS_Click(object sender, EventArgs e)
        {
            ToExcel();
        }
        private void ToExcel()
        {
            try
            {
                string filenameDate = "Статистика по организациям";
                string filename = Util.TempFilesFolder + filenameDate + ".xlsx";
                string[] fileList = Directory.GetFiles(Util.TempFilesFolder, "Статистика по организациям*" + ".xlsx");

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
                List<KeyValuePair<string, DataGridView>> dgvlst = new List<KeyValuePair<string,DataGridView>>();
                dgvlst.Add(new KeyValuePair<string, DataGridView>("Рубрики", dgvRubrics));
                dgvlst.Add(new KeyValuePair<string, DataGridView>("Направления образования", dgvFaculty));
                dgvlst.Add(new KeyValuePair<string, DataGridView>("Формы собственности", dgvOwner));
                dgvlst.Add(new KeyValuePair<string, DataGridView>("Цель деятельности", dgvActivityGoal));
                dgvlst.Add(new KeyValuePair<string, DataGridView>("Национальная принадлежность", dgvNatAffil));
                dgvlst.Add(new KeyValuePair<string, DataGridView>("Страны", dgvCountry));
                dgvlst.Add(new KeyValuePair<string, DataGridView>("Направления подготовки", dgvLP));
                dgvlst.Add(new KeyValuePair<string, DataGridView>("Сферы деятельности", dgvActivityArea));

                using (ExcelPackage doc = new ExcelPackage(newFile))
                {
                    int rowshift = 0;
                    ExcelWorksheet ws = doc.Workbook.Worksheets.Add("Рубрики и направления");
                    Color lightGray = Color.FromName("LightGray");
                    Color darkGray = Color.FromName("DarkGray");

                    foreach (KeyValuePair<string, DataGridView> kvp in dgvlst)
                    {
                        switch (kvp.Key)
                        {
                            /*case "Рубрики":
                                rowshift = 0;
                                ws = doc.Workbook.Worksheets.Add("Рубрики и направления");
                                break;*/
                            case "Формы собственности":
                                rowshift = 0;
                                ws = doc.Workbook.Worksheets.Add("Общая статистика");
                                break;
                            case "Направления подготовки":
                                rowshift = 0;
                                ws = doc.Workbook.Worksheets.Add("Направления подготовки");
                                break;
                            case "Сферы деятельности":
                                rowshift = 0;
                                ws = doc.Workbook.Worksheets.Add("Области проф деятельности");
                                break;
                            default:
                                break;
                        }
                        //ExcelWorksheet ws = doc.Workbook.Worksheets.Add(kvp.Key);
                        DataGridView dgv = kvp.Value;

                        int colind = 0;
                        int delta = 0;
                        
                        foreach (DataGridViewColumn cl in dgv.Columns)
                        {
                            ws.Cells[1 + rowshift, ++colind].Value = cl.HeaderText.ToString();
                            ws.Cells[1 + rowshift, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                            ws.Cells[1 + rowshift, colind].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid; 
                            ws.Cells[1 + rowshift, colind].Style.Fill.BackgroundColor.SetColor(lightGray);
                        }
                        delta++;

                        for (int rwInd = 0; rwInd < dgv.Rows.Count; rwInd++)
                        {
                            DataGridViewRow rw = dgv.Rows[rwInd];
                            delta++;
                            int colInd = 0;
                            foreach (DataGridViewCell cell in rw.Cells)
                            {
                                if (cell.Value == null)
                                {
                                    ws.Cells[rwInd + 2 + rowshift, colInd + 1].Value = "";
                                }
                                else
                                {
                                    ws.Cells[rwInd + 2 + rowshift, colInd + 1].Value = cell.Value; //.ToString(); 
                                }
                                ws.Cells[rwInd + 2 + rowshift, colInd + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                                colInd++;
                            }
                        }
                        rowshift += delta + 2;
                        //форматирование
                        int clmnInd = 0;
                        foreach (DataGridViewColumn clmn in dgv.Columns)
                        {
                            ws.Column(++clmnInd).AutoFit();
                        }
                    }
                    doc.Save();
                }
                
                System.Diagnostics.Process.Start(filename);
            }
            catch
            {
            }
        }

        private void btnDOC_Click(object sender, EventArgs e)
        {
            ToDOC();
        }
        public void ToDOC()
        {
            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "Статистика по организациям";

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
                //WordDoc wd = new WordDoc(string.Format(@"{0}\Статистика по организациям.docx", Util.TemplatesFolder), true);
                WordDoc wd = new WordDoc(string.Format(filePath), true);

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //wd.SetFields("Count", _Count.ToString());
                    List<DataGridView> dgvlst = new List<DataGridView>() { dgvRubrics, dgvFaculty, dgvOwner, dgvActivityGoal, dgvNatAffil, dgvCountry, dgvLP, dgvActivityArea };
                    
                    int tbl_ind = 0;
                    foreach (DataGridView dgv in dgvlst)
                    {
                        TableDoc td = wd.Tables[tbl_ind];
                        tbl_ind++;

                        int curRow = 0;
                        foreach (DataGridViewColumn c in dgv.Columns)
                        {
                            td[c.Index, curRow] = c.HeaderText.ToString();
                        }
                        foreach (DataGridViewRow rw in dgv.Rows)
                        {
                            td.AddRow(1);
                            curRow++;
                            foreach (DataGridViewCell cell in rw.Cells)
                            {
                                if (cell.Value != null)
                                    td[cell.ColumnIndex, curRow] = cell.Value.ToString();
                            }
                        }
                        td.DeleteLastRow();
                        wd.AddParagraph(" ");
                        wd.AddParagraph(" ");
                    }
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

    }
}
