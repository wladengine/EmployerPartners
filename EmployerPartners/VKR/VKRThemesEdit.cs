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
using WordOut;
using EmployerPartners.EDMX;

namespace EmployerPartners
{
    public partial class VKRThemesEdit : Form
    {
        private int? Year
        {
            get { return ComboServ.GetComboIdInt(cbYear); }
            set { ComboServ.SetComboId(cbYear, value); }
        }
        private string Crypt
        {
            get { return ComboServ.GetComboId(cbCrypt); }
            set { ComboServ.SetComboId(cbCrypt, value); }
        }
        private string Order
        {
            get { return ComboServ.GetComboId(cbOrder); }
            set { ComboServ.SetComboId(cbOrder, value); }
        }
        private string VKRSourceSelection
        {
            get { return ComboServ.GetComboId(cbVKRSourceSelection); }
            set { ComboServ.SetComboId(cbVKRSourceSelection, value); }
        }
        private int? StudyLevelId
        {
            get { return ComboServ.GetComboIdInt(cbStudyLevel); }
            set { ComboServ.SetComboId(cbStudyLevel, value); }
        }
        private int? AggregateGroupId
        {
            get { return ComboServ.GetComboIdInt(cbAggregateGroup); }
            set { ComboServ.SetComboId(cbAggregateGroup, value); }
        }
        private int? LicenseProgramId
        {
            get { return ComboServ.GetComboIdInt(cbLicenseProgram); }
            set { ComboServ.SetComboId(cbLicenseProgram, value); }
        }
        private int? ObrazProgramId
        {
            get { return ComboServ.GetComboIdInt(cbObrazProgram); }
            set { ComboServ.SetComboId(cbObrazProgram, value); }
        }

        public int? _Id;
        private int? RowNumStartSearch;

        public VKRThemesEdit()
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            FillCombo();
            FillVKRAll();
        }
        private void FillCombo()
        {
            ComboServ.FillCombo(cbYear, HelpClass.GetComboListByQuery(@" select distinct convert(nvarchar, [GraduateYear]) AS Id, 
                convert(nvarchar, [GraduateYear]) as Name from dbo.VKR_Themes order by 2"), false, true);
            
            ComboServ.FillCombo(cbAggregateGroup, HelpClass.GetComboListByTable(@"dbo.SP_AggregateGroup", " order by Name"), false, true);
            ComboServ.FillCombo(cbOrder, HelpClass.GetComboListByQuery(@" select distinct [OrderNumber] AS Id, 
                ([OrderNumber] +  CASE [OrderDate] WHEN null THEN '' ELSE (' от ' + CONVERT(varchar(100), OrderDate)) END) as Name from dbo.VKR_Themes
                    where (([OrderNumber] is not null) and ([OrderNumber] <> '')) order by [OrderNumber]"), true, false);
            ComboServ.FillCombo(cbVKRSourceSelection, HelpClass.GetComboListByQuery(@"select [Key] AS Id, ([Key] + ' [' + [Name] + ']') as Name from dbo.VKR_Source"), false, true);
            ComboServ.FillCombo(cbStudyLevel, HelpClass.GetComboListByQuery(@"select distinct convert(nvarchar(100), StudyLevelId) as Id, StudyLevel.Crypt + ' ('+  StudyLevel.Name+')' as Name
from dbo.VKR_Themes_StudyPlans 
join dbo.ObrazProgram on VKR_Themes_StudyPlans.ObrazProgramId = ObrazProgram.Id 
join dbo.LicenseProgram on LicenseProgram.Id = ObrazProgram.LicenseProgramId
join dbo.StudyLevel on StudyLevel.Id = LicenseProgram.StudyLevelId order by 1"), false, true);
        }
         
        private void FillLicenseProgram()
        {
            string Filter = (AggregateGroupId.HasValue) ? (" where LicenseProgram.AggregateGroupId = " + AggregateGroupId.ToString()) : "";
            if (StudyLevelId.HasValue)
                Filter += String.Format("{0}{1}{2}", (string.IsNullOrEmpty(Filter) ? " where " : " and "), " LicenseProgram.StudyLevelId = ", StudyLevelId);
            ComboServ.FillCombo(cbLicenseProgram, HelpClass.GetComboListByQuery(String.Format(@"select distinct convert(nvarchar, LicenseProgram.Id)  AS Id, LicenseProgram.Code+' '+LicenseProgram.Name as Name 
from dbo.VKR_Themes_StudyPlans 
join dbo.ObrazProgram on VKR_Themes_StudyPlans.ObrazProgramId = ObrazProgram.Id 
join dbo.LicenseProgram on ObrazProgram.LicenseProgramId = LicenseProgram.Id {0} order by 2", Filter)), false, true);
        
        }
        private void FillObrazProgram()
        {
            string Filter = (AggregateGroupId.HasValue) ? ("where LicenseProgram.AggregateGroupId = " + AggregateGroupId.ToString()) : "";
            if (StudyLevelId.HasValue)
                Filter += String.Format("{0}{1}{2}", (string.IsNullOrEmpty(Filter) ? " where " : " and "), " LicenseProgram.StudyLevelId = ", StudyLevelId);
            if (LicenseProgramId.HasValue)
                Filter += String.Format("{0}{1}{2}", (string.IsNullOrEmpty(Filter) ? " where " : " and "), " ObrazProgram.LicenseProgramId = ", LicenseProgramId);

            ComboServ.FillCombo(cbObrazProgram, HelpClass.GetComboListByQuery(String.Format(@"select distinct convert(nvarchar, ObrazProgram.Id)  AS Id, StudyLevel.Crypt+'.' +ObrazProgram.Number +' '+ObrazProgram.Name as Name 
from dbo.VKR_Themes_StudyPlans 
join dbo.ObrazProgram on VKR_Themes_StudyPlans.ObrazProgramId = ObrazProgram.Id 
join dbo.LicenseProgram on LicenseProgram.Id = ObrazProgram.LicenseProgramId 
join dbo.StudyLevel on StudyLevel.Id = LicenseProgram.StudyLevelId 
{0}  order by 2", Filter)), false, true);
        }
        
        private void FillVKRAll()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_Themes

                               join sp in context.VKR_Themes_StudyPlans on x.Id equals sp.VKRThemeId
                               join op in context.ObrazProgram on sp.ObrazProgramId equals op.Id
                               join lp in context.LicenseProgram on op.LicenseProgramId equals lp.Id
                               join sl in context.StudyLevel on lp.StudyLevelId equals sl.Id

                               join nr in context.VKR_Themes_NPR on x.Id equals nr.VKRThemeId into _nr
                               from nr in _nr.DefaultIfEmpty()

                               join org in context.Organization on x.OrganizationId equals org.Id into _org
                               from org in _org.DefaultIfEmpty()

                               where
                                  (Year.HasValue ? x.GraduateYear == Year : true) && 
                                   ((!String.IsNullOrEmpty(VKRSourceSelection)) ? x.VKRSourceKey == VKRSourceSelection : true) &&
                                   (StudyLevelId.HasValue ? (lp.StudyLevelId == StudyLevelId.Value) : true) &&
                                   (AggregateGroupId.HasValue ? (lp.AggregateGroupId == AggregateGroupId.Value) : true) &&
                                   (( LicenseProgramId.HasValue) ? lp.Id == LicenseProgramId : true) &&
                                   ((ObrazProgramId.HasValue) ? op.Id == ObrazProgramId : true)
                               orderby lp.Name, op.Name
                               select new
                               {
                                   x.Id, 
                                   x.OrganizationId,
                                   Организация = (org!=null) ? org.Name : x.OrganizationName,
                                   Тема_ВКР = x.VKRName,
                                   Тема_ВКР_англ = x.VKRNameEng,
                                   Направление_подготовки = lp.Name,
                                   Образовательная_программа = sl.Crypt + "." + op.Number + " " +op.Name,
                                   НР = (nr!= null)? ((nr.NPR_Surname+" ")??"") + ((nr.NPR_Name+" ")??"")+(nr.NPR_SecondName??"") : "",
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSourceAll.DataSource = dt;
                    dgvAll.DataSource = bindingSourceAll;
                    lblCount.Text = dgvAll.Rows.Count.ToString();

                    foreach (string s in new List<string>() { "Id", "OrganizationId" })
                        if (dgvAll.Columns.Contains(s))
                            dgvAll.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvAll.Columns)
                        col.HeaderText = col.Name.Replace("_", " ");

                    

                    try
                    {
                        dgvAll.Columns["Id"].Width = 60;
                        dgvAll.Columns["В_приказе"].Width = 90;
                        dgvAll.Columns["Тема_предложена"].Width = 80;
                        dgvAll.Columns["Источник_информации"].Width = 80;
                        dgvAll.Columns["СВ_BM_CM"].Width = 80;
                        dgvAll.Columns["НПР_ФИО"].Width = 200;
                        dgvAll.Columns["Тема_ВКР"].Width = 300;
                        dgvAll.Columns["Тема_ВКР_англ"].Width = 300;
                        dgvAll.Columns["Направление_подготовки"].Width = 200;
                        dgvAll.Columns["Образовательная_программа"].Width = 200;
                        dgvAll.Columns["Кафедра"].Width = 200;
                        dgvAll.Columns["УНП"].Width = 200;
                        dgvAll.Columns["Организация"].Width = 200;
                        dgvAll.Columns["Студент_ФИО"].Width = 200;
                        dgvAll.Columns["Консультант_ФИО"].Width = 200;
                        dgvAll.Columns["Примечание"].Width = 200;
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

        private void VKRThemesEdit_Load(object sender, EventArgs e)
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

        private void cbVKRSourceSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillVKRAll();
        }

        private void cbCryptSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLicenseProgram();
        }

        private void cbLicenseProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillObrazProgram();
        }

        private void btnRefreshGrid_Click(object sender, EventArgs e)
        {
            FillVKRAll();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string search = tbSearch.Text.Trim().ToUpper();
                bool exit = false;
                for (int i = 0; i < dgvAll.RowCount; i++)
                {
                    if (exit)
                    { break; }
                    for (int j = 0; j < /*6*/ dgvAll.Columns.Count; j++)
                    {
                        //if (j == 1) // (j == 1 || j == 2 || j == 3)     //для кнопок
                        //    continue;
                        if (dgvAll[j, i].Value.ToString().ToUpper().Contains(search))
                        {
                            //dgvAll[j, i].Style.BackColor = Color.White;
                            //dgvAll[j, i].OwningColumn.Name
                            dgvAll.CurrentCell = dgvAll[j, i];
                            exit = true;
                            RowNumStartSearch = i + 1;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnSearchNext_Click(object sender, EventArgs e)
        {
            if (!RowNumStartSearch.HasValue)
                return;
            try
            {
                string search = tbSearch.Text.Trim().ToUpper();
                bool exit = false;
                int k = (int)RowNumStartSearch;
                for (int i = k; i < dgvAll.RowCount; i++)
                {
                    if (exit)
                    { return; }
                    for (int j = 0; j < /*6*/ dgvAll.Columns.Count; j++)
                    {
                        if (dgvAll[j, i].Value.ToString().ToUpper().Contains(search))
                        {
                            dgvAll.CurrentCell = dgvAll[j, i];
                            exit = true;
                            RowNumStartSearch = i + 1;
                            break;
                        }
                    }
                }
                MessageBox.Show("Поиск завершен. Образец не найден.", "Поиск", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
            }
        }

        private void btnSearchPrevious_Click(object sender, EventArgs e)
        {
            if (!RowNumStartSearch.HasValue)
                return;
            try
            {
                string search = tbSearch.Text.Trim().ToUpper();
                bool exit = false;
                int k = (int)RowNumStartSearch - 2;
                for (int i = k; i >= 0 /*dgvAll.RowCount*/; i--)
                {
                    if (exit)
                    //{ break; }
                    { return; }
                    for (int j = 0; j < /*6*/ dgvAll.Columns.Count; j++)
                    {
                        //if (j == 1) // (j == 1 || j == 2 || j == 3)     //для кнопок
                        //    continue;
                        if (dgvAll[j, i].Value.ToString().ToUpper().Contains(search))
                        {
                            //dgvAll[j, i].Style.BackColor = Color.White;
                            dgvAll.CurrentCell = dgvAll[j, i];
                            exit = true;
                            //RowNumStartSearch = i - 1;
                            RowNumStartSearch = i + 1;
                            break;
                        }
                    }
                }
                MessageBox.Show("Поиск завершен. Образец не найден.", "Поиск", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
            }
        }

        private void btnShowSearchResult_Click(object sender, EventArgs e)
        {
            try
            {
                //dataView.RowFilter = "Name LIKE '%jo%'"     // values that contain 'jo'
                string search = tbSearch.Text.Trim();   //.ToUpper();

                string filter = "";
                bool Start = true;
                foreach (DataGridViewColumn col in dgvAll.Columns)
                {
                    if (col.Name == "ColumnDiv" || col.Name == "ColumnDel" || col.Name == "ColumnEdit" || col.Name == "Id" || col.Name == "В_приказе" || col.Name == "Дата_приказа")
                        continue;
                    if (!Start)
                    {
                        filter += " OR ";
                        Start = false;
                    }
                    filter += col.Name + " LIKE '%" + search + "%'";
                    Start = false;
                }

                bindingSourceAll.Filter = filter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemoveFilter_Click(object sender, EventArgs e)
        {
            bindingSourceAll.RemoveFilter();
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Crypt))
            {
                MessageBox.Show("Не выбран уровень подготовки", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbCrypt.DroppedDown = true;
                return;
            }
            SupplementToDOC("new");
        }
        private void SupplementToDOC(string newexist)
        {
            //Сначала проверить наличие данных
            string sql;
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                switch (newexist)
                {
                    case "new":
                        sql = "select distinct Crypt, StudyPlanNum, ObrazProgramName, LicenseProgramName, VKRName from dbo.VKR_Themes where([InOrder] = 0) or ([InOrder] is null)";
                        break;
                    case "exist":
                        sql = "select distinct Crypt, StudyPlanNum, ObrazProgramName, LicenseProgramName, VKRName from dbo.VKR_Themes where [OrderNumber] = '" + Order + "'";
                        break;
                    default:
                        sql = "";
                        break;
                }
                var VKRTable = context.Database.SqlQuery<VKRThemesUnique>(sql);
                var vkrcount = VKRTable.Count();
                if (vkrcount == 0)
                {
                    MessageBox.Show("Нет данных...", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "";
            templatename = "Утверждение тем ВКР Приложение";
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

                string StudyLevel = "";
                string CryptRUS = "";

                switch (newexist)
                {
                    case "new":
                        switch (Crypt)
                        {
                            case "CB":
                                StudyLevel = "бакалавриат";
                                CryptRUS = "СВ";    //СВ - по русски
                                break;
                            case "BM":
                                StudyLevel = "магистратура";
                                CryptRUS = "ВМ";    //ВМ -  по русски
                                break;
                            case "CM":
                                StudyLevel = "специалитет";
                                CryptRUS = "СМ";    //СМ - по русски
                                break;
                        }
                        break;
                    case "exist":
                        using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                        {
                            var lst = context.VKR_Themes.Where(x => x.OrderNumber == Order).First();
                            //switch (lst.Crypt)
                            //{
                            //    case "CB":
                            //        StudyLevel = "бакалавриат";
                            //        CryptRUS = "СВ";    //СВ - по русски
                            //        break;
                            //    case "СВ":
                            //        StudyLevel = "бакалавриат";
                            //        CryptRUS = "СВ";    //СВ - по русски
                            //        break;
                            //    case "BM":
                            //        StudyLevel = "магистратура";
                            //        CryptRUS = "ВМ";    //ВМ -  по русски
                            //        break;
                            //    case "ВМ":
                            //        StudyLevel = "магистратура";
                            //        CryptRUS = "ВМ";    //ВМ -  по русски
                            //        break;
                            //    case "CM":
                            //        StudyLevel = "специалитет";
                            //        CryptRUS = "СМ";    //СМ - по русски
                            //        break;
                            //    case "СМ":
                            //        StudyLevel = "специалитет";
                            //        CryptRUS = "СМ";    //СМ - по русски
                            //        break;
                            //}
                        }
                        break;
                }
                
                wd.SetFields("StudyLevel", StudyLevel);

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    string VKRList = "";
                    string sqlVKR;

                    switch (newexist)
                    {
                        case "new":
                            sqlVKR = "select distinct Crypt, StudyPlanNum, ObrazProgramName, LicenseProgramName, VKRName from dbo.VKR_Themes where ([InOrder] = 0) or ([InOrder] is null) ";
                            break;
                        case "exist":
                            sqlVKR = "select distinct Crypt, StudyPlanNum, ObrazProgramName, LicenseProgramName, VKRName from dbo.VKR_Themes where [OrderNumber] = '" + Order + "'";
                            break;
                        default:
                            sqlVKR = "";
                            break;
                    }
                    //string sqlVKR = "select distinct Crypt, StudyPlanNum, ObrazProgramName, LicenseProgramName, VKRName from dbo.VKR_Themes";
                    
                    //string sqlVKR = "select distinct  top 100 Crypt, StudyPlanNum, ObrazProgramName, Code, LicenseProgramName, VKRName from dbo.VKR_Themes " + 
                    //                    "inner join dbo.ObrazProgram OP on VKR_Themes.StudyPlanNum  = OP.Number " +
                    //                    "inner join dbo.LicenseProgram LP on OP.LicenseProgramId = LP.Id"; // where Crypt = '" + Crypt +"'"
                    var VKRTable = context.Database.SqlQuery<VKRThemesUnique>(sqlVKR);

                    var vkrlist = (from x in VKRTable   //context.VKR_Themes
                                   where (x.Crypt == Crypt || x.Crypt == CryptRUS) //&& (!String.IsNullOrEmpty(x.ObrazProgramName))  //&& (x.Id < 100)
                                   orderby x.StudyPlanNum
                                   select new
                                   {
                                       x.Crypt,
                                       x.StudyPlanNum,
                                       x.ObrazProgramName,
                                       //x.Code,
                                       x.LicenseProgramName,
                                       x.VKRName
                                   }).ToList();

                    string StudyPlanNum = "";
                    int i = 0;
                    int j = 0;
                    foreach (var item in vkrlist)
                    {
                        if (item.StudyPlanNum != StudyPlanNum)
                        {
                            StudyPlanNum = item.StudyPlanNum;
                            i++;
                            j = 0;
                            VKRList = VKRList + "\r\n" + i + ". " + item.Crypt + "." + item.StudyPlanNum + " " + "«" + item.ObrazProgramName + "»" + ", " +
                                        "направление " + /*item.Code + " " +*/ "«" + item.LicenseProgramName + "»" + ":" + "\r\n";
                        }
                        j++;
                        VKRList = VKRList + /*"      "*/ "\t" + i + "." + j + ". " + item.VKRName + "\r\n";
                    }
                    wd.SetFields("VKRList", VKRList);

                }
            }
            catch (WordException we)
            {
                MessageBox.Show(we.Message);
            }
        }

        private void cbOPSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillVKRAll();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                int quan = 0;
                quan = dgvAll.Rows.Count;
                if (quan > 500)
                {
                    //if (MessageBox.Show("Предполагается экспорт в Excel " + quan + " записей.\r\n" + "Это может занять некоторое время..\r\n" +
                    //    "Продолжить ?", "Запрос на подтверждение",
                    //    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    //    return;
                    MessageBox.Show("Выбран слишком большой объем данных (" + quan + " записей).\r\n" + "Уменьшите количество записей с помощью фильтров.", "Инфо",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                string filenameDate = "Темы ВКР 2017";
                string filename = Util.TempFilesFolder + filenameDate + ".xlsx";
                string[] fileList = Directory.GetFiles(Util.TempFilesFolder, "Темы ВКР 2017*" + ".xlsx");

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
                    ExcelWorksheet ws = doc.Workbook.Worksheets.Add("Темы_ВКР");
                    Color lightGray = Color.FromName("LightGray");
                    Color darkGray = Color.FromName("DarkGray");

                    int colind = 0;

                    foreach (DataGridViewColumn cl in dgvAll.Columns)
                    {
                        ws.Cells[1, ++colind].Value = cl.HeaderText.ToString();
                        ws.Cells[1, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                        ws.Cells[1, colind].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[1, colind].Style.Fill.BackgroundColor.SetColor(lightGray);
                    }

                    for (int rwInd = 0; rwInd < dgvAll.Rows.Count; rwInd++)
                    {
                        DataGridViewRow rw = dgvAll.Rows[rwInd];
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

                                string CellValue = cell.Value.ToString();
                                DateTime res;
                                if (!String.IsNullOrEmpty(CellValue))
                                {
                                    if (DateTime.TryParse(CellValue, out res))
                                    {
                                        ws.Cells[rwInd + 2, colInd + 1].Value = res.Date.ToString("dd.MM.yyyy");
                                    }
                                }

                            }
                            ws.Cells[rwInd + 2, colInd + 1].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                            colInd++;
                        }
                    }

                    //форматирование
                    //int clmnInd = 0;
                    //foreach (DataGridViewColumn clmn in dgvAll.Columns)
                    //{
                    //    //if (clmn.Name == "Тема_ВКР")
                    //    //    ws.Column(++clmnInd).Width = 60;
                    //    //else if (clmn.Name == "Тема_ВКР_англ")
                    //    //    ws.Column(++clmnInd).Width = 60;
                    //    //else if (clmn.Name == "Направление_подготовки" || clmn.Name == "Образовательная_программа" || clmn.Name == "Организация")
                    //    //    ws.Column(++clmnInd).Width = 40;
                    //    //else if (clmn.Name == "Id")
                    //    //{
                    //    //    ws.Column(++clmnInd).Width = 0;
                    //    //    clmnInd++;
                    //    //}
                    //    //else if (clmn.Name == "Источник_Устав" || clmn.Name == "Источник_ЕГРЮЛ" || clmn.Name == "Источник_Сайт" ||
                    //    //    clmn.Name == "Карточка_проверена" || clmn.Name == "Используется" || clmn.Name == "Дата_заведения_карточки")
                    //    //    ws.Column(++clmnInd).Width = 0;

                    //    //else
                    //    //ws.Column(++clmnInd).AutoFit();
                    //}
                    doc.Save();
                }
                System.Diagnostics.Process.Start(filename);
            }
            catch
            {
            }
        }

        private void btnWord2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Order))
            {
                MessageBox.Show("Не выбран приказ", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbOrder.DroppedDown = true;
                return;
            }
            SupplementToDOC("exist");
        }

        private void dgvAll_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvAll.CurrentCell == null) return;
            if (dgvAll.CurrentCell.RowIndex < 0) return;
            int Id = (int)dgvAll.CurrentRow.Cells["Id"].Value;
            if (Utilities.VKRThemeCardIsOpened(Id))
                return;
            new VKRThemeCard(Id, FillVKRAll).Show();
        }

        private void cbAggregateGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLicenseProgram();
        }

        private void VKRThemesEdit_Shown(object sender, EventArgs e)
        {
            foreach (DataGridViewRow rw in dgvAll.Rows)
            {
                if (String.IsNullOrEmpty(rw.Cells["OrganizationId"].Value.ToString()) && !String.IsNullOrEmpty(rw.Cells["Организация"].Value.ToString()))
                {
                    rw.Cells["Организация"].Style.ForeColor = Color.Red;
                }
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new VKRThemeCard(null, new UpdateVoidHandler(FillVKRAll)).Show();
        }

    }
}
