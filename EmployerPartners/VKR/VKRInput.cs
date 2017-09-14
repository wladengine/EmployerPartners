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
    public partial class VKRInput : Form
    {
        private string Crypt
        {
            get { return ComboServ.GetComboId(cbCrypt); }
            set { ComboServ.SetComboId(cbCrypt, value); }
        }
        private string VKRSourceSelection
        {
            get { return ComboServ.GetComboId(cbVKRSourceSelection); }
            set { ComboServ.SetComboId(cbVKRSourceSelection, value); }
        }
        private string CryptSelection
        {
            get { return ComboServ.GetComboId(cbCryptSelection); }
            set { ComboServ.SetComboId(cbCryptSelection, value); }
        }
        private string LPSelection
        {
            get { return ComboServ.GetComboId(cbLPSelection); }
            set { ComboServ.SetComboId(cbLPSelection, value); }
        }

        public int? _Id;
        private int? RowNumStartSearch;

        public VKRInput()
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            FillCombo();
            FillVKRNPR();
            FillVKRAll();
        }

        private void FillCombo()
        {
            FillCrypt();
            FillVKRSourceSelection();
            FillCryptSelection();
            FillLPSelection();
        }
        private void FillCrypt()
        {
            ComboServ.FillCombo(cbCrypt, HelpClass.GetComboListByQuery(@" select [Key] AS Id, [Name] from dbo.VKR_Crypt order by [Sorting]"), true, false);
        }
        private void FillVKRSourceSelection()
        {
            ComboServ.FillCombo(cbVKRSourceSelection, HelpClass.GetComboListByQuery(@" select [Key] AS Id, ([Key] + ' [' + [Name] + ']') as Name from dbo.VKR_Source"), false, true);
        }
        private void FillCryptSelection()
        {
            ComboServ.FillCombo(cbCryptSelection, HelpClass.GetComboListByQuery(@" select [Key] AS Id, ([Key] + ' [' + [Name] + ']') as Name from dbo.VKR_Crypt order by [Sorting]"), false, true);
        }
        private void FillLPSelection()
        {
            ComboServ.FillCombo(cbLPSelection, HelpClass.GetComboListByQuery(@" select distinct [LicenseProgramName]  AS Id, [LicenseProgramName] as Name from dbo.VKR_Themes order by Name"), false, true);
        }
        private void FillAll()
        {
            FillVKRNPR(null);
            FillVKRAll(null);
        }
        private void FillAll(int? id)
        {
            FillVKRNPR(id);
            FillVKRAll(id);
        }
        private void FillVKRNPR()
        {
            FillVKRNPR(null);
        }
        private void FillVKRNPR(int? id)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_Themes
                               join vkrsource in context.VKR_Source on x.VKRSourceKey equals vkrsource.Key into _vkrsource
                               from vkrsource in _vkrsource.DefaultIfEmpty()
                               where x.VKRSourceKey == "П"
                               orderby x.LicenseProgramName, x.Crypt, x.StudyPlanNum, x.ObrazProgramName, x.NPR_LastName
                               select new
                               {
                                    Тема_предложена = x.VKRSourceKey,
                                    Источник_информации = x.DocumentNumber,
                                    Направление_подготовки = x.LicenseProgramName,
                                    Образовательная_программа = x.ObrazProgramName,
                                    СВ_BM_CM = x.Crypt,
                                    Номер_плана = x.StudyPlanNum,
                                    Тема_ВКР = x.VKRName,
                                    Тема_ВКР_англ = x.VKRNameEng,
                                    НПР = ((!String.IsNullOrEmpty(x.NPR_LastName)) ? (x.NPR_LastName + " ") : "") + " " +
                                               ((!String.IsNullOrEmpty(x.NPR_FirstName)) ? (x.NPR_FirstName + " ") : "") + " " +
                                               ((!String.IsNullOrEmpty(x.NPR_SecondName)) ? (x.NPR_SecondName + " ") : ""),
                                    Должность = x.NPR_Position,
                                    Степень = x.NPR_Degree,
                                    Звание = x.NPR_Rank,
                                    Аккаунт = x.NPR_Account,
                                    Кафедра = x.NPR_Chair,
                                    УНП = x.NPR_Faculty,
                                    Примечание = x.Comment,
                                    x.Id
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSourceNPR.DataSource = dt;
                    dgvNPR.DataSource = bindingSourceNPR;

                    List<string> Cols = new List<string>() { "Id" };  //{ "Id", "ObrazProgramId" };

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
                        if (col.Name == "ColumnEditNPR")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDivNPR")
                        {
                            col.HeaderText = "";
                        }
                    }

                    try
                    {
                        dgvNPR.Columns["ColumnDivNPR"].Width = 6;
                        dgvNPR.Columns["ColumnDelNPR"].Width = 70;
                        dgvNPR.Columns["ColumnEditNPR"].Width = 100;
                        dgvNPR.Columns["Тема_предложена"].Width = 80;
                        dgvNPR.Columns["Источник_информации"].Width = 80;
                        dgvNPR.Columns["СВ_BM_CM"].Width = 80;
                        dgvNPR.Columns["НПР"].Width = 200;
                        dgvNPR.Columns["Тема_ВКР"].Width = 300;
                        dgvNPR.Columns["Тема_ВКР_англ"].Width = 300;
                        dgvNPR.Columns["Образовательная_программа"].Width = 200;
                        dgvNPR.Columns["Кафедра"].Width = 200;
                        dgvNPR.Columns["УНП"].Width = 200;
                        dgvNPR.Columns["Примечание"].Width = 200;
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
        private void FillVKRAll()
        {
            FillVKRAll(null);
        }
        private void FillVKRAll(int? id)
        {
            try
            {
                string CryptSelectionRUS = "";
                switch (CryptSelection)
                {
                    case "CB":
                        CryptSelectionRUS = "СВ";    //СВ - по русски
                        break;
                    case "BM":
                        CryptSelectionRUS = "ВМ";    //ВМ -  по русски
                        break;
                    case "CM":
                        CryptSelectionRUS = "СМ";    //СМ - по русски
                        break;
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_Themes
                               //join vkrsource in context.VKR_Source on x.VKRSourceKey equals vkrsource.Key into _vkrsource
                               //from vkrsource in _vkrsource.DefaultIfEmpty()
                               where ((!String.IsNullOrEmpty(CryptSelection)) ? (x.Crypt == CryptSelection || x.Crypt == CryptSelectionRUS) : true) &&
                                   ((!String.IsNullOrEmpty(VKRSourceSelection)) ? (x.VKRSourceKey == VKRSourceSelection || x.VKRSourceKey == VKRSourceSelection) : true) && 
                                   ((!String.IsNullOrEmpty(LPSelection)) ? x.LicenseProgramName == LPSelection : true)
                               orderby x.VKRSourceKey, x.LicenseProgramName, x.Crypt, x.StudyPlanNum, x.ObrazProgramName, x.NPR_LastName
                               select new
                               {
                                   Тема_предложена = x.VKRSourceKey,
                                   Источник_информации = x.DocumentNumber,
                                   Направление_подготовки = x.LicenseProgramName,
                                   Образовательная_программа = x.ObrazProgramName,
                                   СВ_BM_CM = x.Crypt,
                                   Номер_плана = x.StudyPlanNum,
                                   Тема_ВКР = x.VKRName,
                                   Тема_ВКР_англ = x.VKRNameEng,
                                   НПР_ФИО = ((!String.IsNullOrEmpty(x.NPR_LastName)) ? (x.NPR_LastName + " ") : "") + " " +
                                              ((!String.IsNullOrEmpty(x.NPR_FirstName)) ? (x.NPR_FirstName + " ") : "") + " " +
                                              ((!String.IsNullOrEmpty(x.NPR_SecondName)) ? (x.NPR_SecondName + " ") : ""),
                                   Должность = x.NPR_Position,
                                   Степень = x.NPR_Degree,
                                   Звание = x.NPR_Rank,
                                   Аккаунт = x.NPR_Account,
                                   Кафедра = x.NPR_Chair,
                                   УНП = x.NPR_Faculty,
                                   Организация_согласовано = x.OrganizationName,
                                   Студент_ФИО = ((!String.IsNullOrEmpty(x.Student_LastName)) ? (x.Student_LastName + " ") : "") + " " +
                                              ((!String.IsNullOrEmpty(x.Student_FirstName)) ? (x.Student_FirstName + " ") : "") + " " +
                                              ((!String.IsNullOrEmpty(x.Student_SecondName)) ? (x.Student_SecondName + " ") : ""),
                                   Студент_аккаунт = x.Student_Account,
                                   Консультант_ФИО = ((!String.IsNullOrEmpty(x.Consult_LastName)) ? (x.Consult_LastName + " ") : "") + " " +
                                              ((!String.IsNullOrEmpty(x.Consult_FirstName)) ? (x.Consult_FirstName + " ") : "") + " " +
                                              ((!String.IsNullOrEmpty(x.Consult_SecondName)) ? (x.Consult_SecondName + " ") : ""),
                                   Консультант_должность = x.Consult_Position,
                                   Консультант_степень = x.Consult_Degree,
                                   Консультант_звание = x.Consult_Rank,
                                   Консультант_организация = x.Consult_OrganizationName,
                                   Примечание = x.Comment,
                                   x.Id
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSourceAll.DataSource = dt;
                    dgvAll.DataSource = bindingSourceAll;

                    List<string> Cols = new List<string>() { "Id" };  //{ "Id", "ObrazProgramId" };

                    foreach (string s in Cols)
                        if (dgvAll.Columns.Contains(s))
                            dgvAll.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvAll.Columns)
                        col.HeaderText = col.Name.Replace("_", " ");

                    try
                    {
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
                        dgvAll.Columns["Организация_согласовано"].Width = 200;
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
        private void VKRInput_Load(object sender, EventArgs e)
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
            try
            {
                tabControl1.SelectedTab = tabPage4;
            }
            catch (Exception)
            {
              
            }
        }

        private void btnNPRRefresh_Click(object sender, EventArgs e)
        {
            FillVKRNPR();
        }

        private void btnAllRefresh_Click(object sender, EventArgs e)
        {
            FillVKRAll();
        }

        private void btnNPRAddNew_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Добавление новой темы ВКР", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnOrgAddNew_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Добавление новой темы ВКР", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnStudentAddNew_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Добавление новой темы ВКР", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                            dgvNPR.CurrentCell = dgvNPR.CurrentRow.Cells["Тема_предложена"];
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

                        string vkrname = "";
                        try
                        {
                            vkrname = dgvNPR.CurrentRow.Cells["Тема_ВКР"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        string opname = "";
                        try
                        {
                            opname = dgvNPR.CurrentRow.Cells["Образовательная_программа"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        string plannum = "";
                        try
                        {
                            plannum = dgvNPR.CurrentRow.Cells["Номер_плана"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        if (MessageBox.Show("Удалить тему ВКР из списка? \r\n" + vkrname + "\r\n" + "Образовательная программа: \r\n" + opname + " " + plannum, 
                            "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                            { return; }
                        try
                        {
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                context.VKR_Themes.RemoveRange(context.VKR_Themes.Where(x => x.Id == id));
                                context.SaveChanges();
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Не удалось удалить запись...", "Сообщение");
                        }
                        FillVKRNPR();
                        FillVKRAll();
                    }
                    if (dgvNPR.CurrentCell.ColumnIndex == 2)
                    {
                        try
                        {
                            dgvNPR.CurrentCell = dgvNPR.CurrentRow.Cells["Студент"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        try
                        {
                            int id = int.Parse(dgvNPR.CurrentRow.Cells["Id"].Value.ToString());
                            if (Utilities.VKRNPRCardIsOpened(id))
                                return;
                            new VKRNPRCard(id, new UpdateIntHandler(FillAll)).Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                }
        }

        private void cbCryptSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillVKRAll();
        }

        private void cbLPSelection_SelectedIndexChanged(object sender, EventArgs e)
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
                        if (j == 1) // (j == 1 || j == 2 || j == 3)     //для кнопок
                            continue;
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
                    //{ break; }
                    { return; }
                    for (int j = 0; j < /*6*/ dgvAll.Columns.Count; j++)
                    {
                        if (j == 1) // (j == 1 || j == 2 || j == 3)     //для кнопок
                            continue;
                        if (dgvAll[j, i].Value.ToString().ToUpper().Contains(search))
                        {
                            //dgvAll[j, i].Style.BackColor = Color.White;
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
                        if (j == 1) // (j == 1 || j == 2 || j == 3)     //для кнопок
                            continue;
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
                if (col.Name == "ColumnDiv" || col.Name == "ColumnDel" || col.Name == "ColumnEdit" || col.Name == "Id")
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
            catch (Exception)
            {
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
            SupplementToDOC();
        }
        private void SupplementToDOC()
        {
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

                wd.SetFields("StudyLevel", StudyLevel);

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    string VKRList = "";

                    string sqlVKR = "select distinct Crypt, StudyPlanNum, ObrazProgramName, LicenseProgramName, VKRName from dbo.VKR_Themes";
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
                        VKRList = VKRList + "      " + i + "." + j + ". " + item.VKRName + "\r\n";
                    }
                    wd.SetFields("VKRList", VKRList);

                }
            }
            catch (WordException we)
            {
                MessageBox.Show(we.Message);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                int quan = 0;
                quan = dgvAll.Rows.Count;
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
                    //    //else if (clmn.Name == "Направление_подготовки" || clmn.Name == "Образовательная_программа" || clmn.Name == "Организация_согласовано")
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

        private void cbVKRSourceSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillVKRAll();
        }
    }
}
