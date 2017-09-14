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
    public partial class VKRThemesAspirant : Form
    {
        private string DegreeName
        {
            get { return ComboServ.GetComboId(cbDegreeName); }
            set { ComboServ.SetComboId(cbDegreeName, value); }
        }
        private string Course
        {
            get { return ComboServ.GetComboId(cbCourse); }
            set { ComboServ.SetComboId(cbCourse, value); }
        }
        private string Department
        {
            get { return ComboServ.GetComboId(cbDepartment); }
            set { ComboServ.SetComboId(cbDepartment, value); }
        }
        private string StatusName
        {
            get { return ComboServ.GetComboId(cbStatusName); }
            set { ComboServ.SetComboId(cbStatusName, value); }
        }
        private string StudyingName
        {
            get { return ComboServ.GetComboId(cbStudyingName); }
            set { ComboServ.SetComboId(cbStudyingName, value); }
        }
        private int? FacultyId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
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
        //перенумерация
        private string RenumOrderReviewNumber
        {
            get { return ComboServ.GetComboId(cbRenumOrderReviewNumber); }
            set { ComboServ.SetComboId(cbRenumOrderReviewNumber, value); }
        }

        //Word
        private bool NameEngDoc
        {
            get { return chbNameEngDoc.Checked; }
            set { chbNameEngDoc.Checked = value; }
        }
        private bool NameEngReviewDoc
        {
            get { return chbNameEngReviewDoc.Checked; }
            set { chbNameEngReviewDoc.Checked = value; }
        }

        //ShowColumns
        private bool StudentShowColumns
        {
            get { return chbStudentShowColumns.Checked; }
            set { chbStudentShowColumns.Checked = value; }
        }
        //Приказы
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
        private string OrderNumberReview
        {
            get { return tbOrderNumberReview.Text.Trim(); }
            set { tbOrderNumberReview.Text = value; }
        }
        private string OrderDateReview
        {
            get { return tbOrderDateReview.Text.Trim(); }
            set { tbOrderDateReview.Text = value; }
        }

        public VKRThemesAspirant()
        {
            InitializeComponent();
            FillCombo();
            FillStudent();
            SetAccessRight();
            this.MdiParent = Util.mainform;
        }
        private void SetAccessRight()
        {
            //if (Util.IsSuperUser())
            //{
            //    btnOrgXLS.Visible = true;
            //    btnReNumOrderReview.Visible = true;
            //    cbRenumOrderReviewNumber.Visible = true;
            //    lblRenumOrderReviewNumber.Visible = true;
            //    btnRenumOrderReviewRefresh.Visible = true;
            //}
        }
        private void FillCombo()
        {
            ComboServ.FillCombo(cbDegreeName, HelpClass.GetComboListByQuery(@" select distinct [DegreeName]  AS Id, 
                                    [DegreeName] as Name from dbo.VKR_ThemesAspirantOrder order by [DegreeName]"), false, true);
            ComboServ.FillCombo(cbCourse, HelpClass.GetComboListByQuery(@" select distinct [Course]  AS Id, 
                                    [Course] as Name from dbo.VKR_ThemesAspirantOrder order by [Course]"), false, true);
            ComboServ.FillCombo(cbDepartment, HelpClass.GetComboListByQuery(@" select distinct [StudyForm]  AS Id, 
                                    [StudyForm] as Name from dbo.VKR_ThemesAspirantOrder order by [StudyForm]"), false, true);
            ComboServ.FillCombo(cbStatusName, HelpClass.GetComboListByQuery(@" select distinct [StatusName]  AS Id, 
                                    [StatusName] as Name from dbo.VKR_ThemesAspirantOrder order by [StatusName] desc"), false, true);

            ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByQuery(@" select CONVERT(varchar(100), [Id]) AS Id, Name  
                                    from dbo.Faculty where Id in (select FacultyId from dbo.VKR_ThemesAspirantOrder) order by dbo.Faculty.Name"), false, true);

            FillRenumOrderReviewNumbers();
            FillLicenseProgramList();
            FillObrazProgramList();
        }
        private void FillRenumOrderReviewNumbers()
        {
            //перенумерация cbRenumOrderReviewNumber
            string obrazprogramid = "";
            if (ObrazProgramId.HasValue)
	        {
		        obrazprogramid = " ([ObrazProgramId] = " + ObrazProgramId.ToString() +")";
	        }
            else
	        {
                obrazprogramid = " ([ObrazProgramId] = null)";
	        }
            ComboServ.FillCombo(cbRenumOrderReviewNumber, HelpClass.GetComboListByQuery(@" select distinct [OrderNumberReview]  AS Id, 
                                    [OrderNumberReview] as Name from dbo.VKR_ThemesStudentOrder where " + obrazprogramid +
                                    " and ([OrderNumberReview] is not null) and ([OrderNumberReview] <> '')" + " order by [OrderNumberReview]"), true, false);
        }
        private void FillLicenseProgramList()
        {
            string faculty = "";
            if (FacultyId.HasValue)
	        {
                faculty = " where FacultyId = " + FacultyId.ToString() + " ";
	        }
            ComboServ.FillCombo(cbLicenseProgram, HelpClass.GetComboListByQuery(@" select CONVERT(varchar(100), dbo.LicenseProgram.[Id]) AS Id, 
                        (dbo.LicenseProgram.[Code] + case Len(dbo.LicenseProgram.[Code]) when 6 then '    ' else '  ' end + 
                        dbo.StudyLevel.Name + case Len(dbo.StudyLevel.Name) when 11 then '    ' else '  ' end + dbo.LicenseProgram.Name) as Name  
                                    from dbo.LicenseProgram inner join dbo.StudyLevel on dbo.LicenseProgram.StudyLevelId = dbo.StudyLevel.Id " +
                                    "where dbo.LicenseProgram.Id in (select LicenseProgramId from dbo.VKR_ThemesStudentOrder " + faculty + ") order by dbo.LicenseProgram.Name"), false, true);
        }
        private void FillObrazProgramList()
        {
            string licenseprogram = "";
            if (LicenseProgramId.HasValue)
            {
                licenseprogram = " where LicenseProgramId = " + LicenseProgramId.ToString() + " ";
                if (FacultyId.HasValue)
                {
                    licenseprogram += " and FacultyId = " + FacultyId.ToString() + " ";
                }
            }
            else
            {
                if (FacultyId.HasValue)
                {
                    licenseprogram = " where FacultyId = " + FacultyId.ToString() + " ";
                }
            }
            ComboServ.FillCombo(cbObrazProgram, HelpClass.GetComboListByQuery(@" select CONVERT(varchar(100), [Id]) AS Id, ([Number] + '  ' + Name) as Name  
                                    from dbo.ObrazProgram where Id in (select ObrazProgramId from dbo.VKR_ThemesStudentOrder " + licenseprogram + ") order by dbo.ObrazProgram.Name"), false, true);
        }
        private void FillStudent()
        {
            FillStudent(null);
        }
        private void FillStudent(int? id)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesAspirantOrder
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id into _op
                               from op in _op.DefaultIfEmpty()
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id into _lp
                               from lp in _lp.DefaultIfEmpty()
                               join fac in context.Faculty on x.FacultyId equals fac.Id into _fac
                               from fac in _fac.DefaultIfEmpty()
                               orderby x.FIO
                               where ((!String.IsNullOrEmpty(DegreeName)) ? x.DegreeName == DegreeName : true) &&
                                        ((!String.IsNullOrEmpty(Course)) ? x.Course == Course : true) &&
                                        ((!String.IsNullOrEmpty(Department)) ? x.StudyForm == Department : true) &&
                                        ((!String.IsNullOrEmpty(StatusName)) ? x.StatusName == StatusName : true) &&
                                        ((FacultyId.HasValue) ? x.FacultyId == FacultyId : true) &&
                                        ((LicenseProgramId.HasValue) ? x.LicenseProgramId == LicenseProgramId : true) &&
                                        ((ObrazProgramId.HasValue) ? x.ObrazProgramId == ObrazProgramId : true)

                               select new
                               {
                                   ФИО = x.FIO,
                                   Аккаунт = x.Account,

                                   Курс = x.Course,
                                   Уровень = x.DegreeName,
                                   Форма_обуч = x.StudyForm,
                                   Статус = x.StatusName,

                                   Код_направления = lp.Code,
                                   Направление_подготовки = lp.Name,
                                   Шифр_ОП = x.ObrazProgramCrypt,
                                   Образовательная_программа = op.Name,
                                   
                                   Email = x.MailBox,
                                   УНП = fac.Name,
                                   
                                   Тема_ВКР = x.VKRName,
                                   Тема_ВКР_англ = x.VKRNameEng,

                                   x.Id,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource1.DataSource = dt;
                    dgv.DataSource = bindingSource1;

                    foreach (DataGridViewColumn col in dgv.Columns)
                        col.HeaderText = col.Name.Replace("_", " ");
                    try
                    {
                        dgv.Columns["ФИО"].Frozen = true;
                        dgv.Columns["ФИО"].Width = 200;
                        dgv.Columns["Курс"].Width = 60;
                        dgv.Columns["Направление_подготовки"].Width = 250;
                        dgv.Columns["Образовательная_программа"].Width = 250;
                        dgv.Columns["УНП"].Width = 200;

                        dgv.Columns["Id"].Visible = false;

                        List<string> Pers_cols = new List<string>() { "Курс", "Уровень", 
                                "Форма_обуч", "Код_направления","Код_направления",
                                "Образовательная_программа", "Учебный_план", "Направление_подготовки",
                                "Шифр_ОП", "Статус","Состояние","УНП", "Email"};
                        foreach (string s in Pers_cols)
                            if (dgv.Columns.Contains(s))
                                dgv.Columns["Email"].Visible = StudentShowColumns;

                    }
                    catch (Exception ex)
                    {
                    } 
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void VKRThemesStudent_Load(object sender, EventArgs e)
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillStudent();
            //Обновить список номеров приказов
            FillRenumOrderReviewNumbers();
        }

        private void btnRemoveFilter_Click(object sender, EventArgs e)
        {
            try
            {
                this.UseWaitCursor = true;
                DegreeName = null;
                cbDegreeName.Text = "все";
                Course = null;
                cbCourse.Text = "все";
                Department = null;
                cbDepartment.Text = "все";
                StatusName = null;
                cbStatusName.Text = "все";
                StudyingName = null;
                cbStudyingName.Text = "все";
                FacultyId = null;
                cbFaculty.Text = "все";
                LicenseProgramId = null;
                cbLicenseProgram.Text = "все";
                ObrazProgramId = null;
                cbObrazProgram.Text = "все";

                FillStudent();
            }
            catch (Exception)
            {
                this.UseWaitCursor = false;
            }
            finally
            {
                this.UseWaitCursor = false;
            }
        }

        private void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLicenseProgramList();
            FillObrazProgramList();
        }

        private void cbLicenseProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillObrazProgramList();
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
                    for (int j = 0; j < 2 ; j++)
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
                }
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bindingSource2.RemoveFilter();
        }

        private void chbStudentShowColumns_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                List<string> Pers_cols = new List<string>() { "Курс", "Уровень", 
                                "Форма_обуч", "Код_направления","Код_направления",
                                "Образовательная_программа", "Учебный_план", "Направление_подготовки",
                                "Шифр_ОП", "Статус","Состояние","УНП", "Email"};
                foreach (string s in Pers_cols)
                    if (dgv.Columns.Contains(s))
                        dgv.Columns["Email"].Visible = StudentShowColumns;
            }
            catch (Exception)
            {
            }
        }

        private void chbNPRShowColumns_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
            }
        }

        private void chbReviewShowColumns_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                new VKRThemesAspirantCard(id, new UpdateIntHandler(FillStudent)).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOrderDoc_Click(object sender, EventArgs e)
        {
            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbObrazProgram.DroppedDown = true;
                return;
            }

            OrderToDoc(ObrazProgramId);
        }
        private void OrderToDoc(int? opid)
        {
            if (!opid.HasValue)
	            return;

            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "";
            templatename = "Приказ ВКР и НР";
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
            string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Приказы по ВКР и НР\";
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

            WordDoc wd = new WordDoc(string.Format(filePath), true);
            
            //заполнение шаблона
            try
            {
                string ObrazProgram = "";
                string LicenseProgram = "";
                //string UNKUNP = "";
                string Stlevel = "";
                string OPNumber = "";
                string OPName = "";
                string LPCode = "";
                string LPName = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgram
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               where x.Id == opid
                               select x).First();
                               
                            Stlevel = lst.LicenseProgram.StudyLevel.Name;
                            OPNumber = lst.Number;
                            OPName = lst.Name;
                            LPCode = lst.LicenseProgram.Code;
                            LPName = lst.LicenseProgram.Name;
                }
                switch (Stlevel)
                {
                    case "бакалавриат":
                        ObrazProgram = "бакалавриата CB." + OPNumber +".*";
                        LicenseProgram = "по направлению подготовки ";
                        break;
                    case "магистратура":
                        ObrazProgram = "магистратуры BM." + OPNumber + ".*";
                        LicenseProgram = "по направлению подготовки ";
                        break;
                    case "специалитет":
                        ObrazProgram = "специалитета CM." + OPNumber + ".*";
                        LicenseProgram = "по специальности  ";
                        break;
                    default:
                        break;
                }
                ObrazProgram += " " + "«" + OPName + "»";
                LicenseProgram += LPCode + " " + "«" + LPName + "»";

                wd.SetFields("ObrazProgram", ObrazProgram);
                wd.SetFields("LicenseProgram", LicenseProgram);
                //wd.SetFields("UNKUNP", "");

                int FacId;
                int StLevelId;
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgram
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               where x.Id == opid
                               select x).First();

                    FacId = lst.FacultyId;
                    StLevelId = lst.LicenseProgram.StudyLevel.Id;
                }

                string basis = "";
                string chairman = "";
                string send = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var quan = (from x in context.VKR_OrderSource
                               where (x.FacultyId == FacId) && (x.StudyLevelId == StLevelId) 
                               select x).Count();
                    var lst = (from x in context.VKR_OrderSource
                                where (x.FacultyId == FacId) && ((quan > 0) ? x.StudyLevelId == StLevelId : x.StudyLevelId == null)
                                select x).First();
                    basis = lst.Basis;
                    chairman = lst.ChairmanForOrder;
                    send = ((!String.IsNullOrEmpty(lst.ChairmanForSend))? lst.ChairmanForSend : "") + 
                            ((!String.IsNullOrEmpty(lst.UOPForSend)) ? "\r\n" + lst.UOPForSend : "") + 
                            ((!String.IsNullOrEmpty(lst.UUForSend)) ? "\r\n" + lst.UUForSend : "");
                }
                wd.SetFields("Chairman", chairman);
                wd.SetFields("Basis", basis);
                wd.SetFields("Send", send);

                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch
                    { }
                }
                string fpath = filePath;
                try
                {
                    filePath = TempFilesFolder + nameshort + " " + OPNumber + type;
                }
                catch (Exception)
                {
                    filePath = fpath;
                }
                
                wd.Save(filePath);

            }
            catch (Exception ex)
            {
               MessageBox.Show("Не удалось сформировать документ\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);  
            }
        }

        private void btnSupplementDoc_Click(object sender, EventArgs e)
        {
            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbObrazProgram.DroppedDown = true;
                return;
            }

            SupplementToDoc(ObrazProgramId);
        }

        private void SupplementToDoc(int? opid)
        {
            if (!opid.HasValue)
                return;

            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "";
            templatename = "Приложение к приказу ВКР и НР";
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {

                    var template = context.Templates.Where(x => x.TemplateName == templatename).Select(x => x).First();

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
            string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Приказы по ВКР и НР\";
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

            WordDoc wd = new WordDoc(string.Format(filePath), true);

            //заполнение шаблона
            try
            {
                string ObrazProgram = "";
                string LicenseProgram = "";
                //string UNKUNP = "";
                string Stlevel = "";
                string OPNumber = "";
                string OPName = "";
                string LPCode = "";
                string LPName = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgram
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               where x.Id == opid
                               select x).First();

                    Stlevel = lst.LicenseProgram.StudyLevel.Name;
                    OPNumber = lst.Number;
                    OPName = lst.Name;
                    LPCode = lst.LicenseProgram.Code;
                    LPName = lst.LicenseProgram.Name;
                }
                switch (Stlevel)
                {
                    case "бакалавриат":
                        ObrazProgram = "бакалавриата CB." + OPNumber + ".*";
                        LicenseProgram = "по направлению подготовки ";
                        break;
                    case "магистратура":
                        ObrazProgram = "магистратуры BM." + OPNumber + ".*";
                        LicenseProgram = "по направлению подготовки ";
                        break;
                    case "специалитет":
                        ObrazProgram = "специалитета CM." + OPNumber + ".*";
                        LicenseProgram = "по специальности ";
                        break;
                    default:
                        break;
                }
                ObrazProgram += " " + "«" + OPName + "»";
                LicenseProgram += LPCode + " " + "«" + LPName + "»";

                wd.SetFields("ObrazProgram", ObrazProgram);
                wd.SetFields("LicenseProgram", LicenseProgram);
                //wd.SetFields("UNKUNP", "");

                //Заполнение таблицы
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               where (x.ObrazProgramId == opid) && 
                               ((rbtnFilledStudent.Checked) ? x.StatusName == "студ" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != null : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != null : true) &&
                               ((x.OrderDop == false) || (x.OrderDop == null))
                               orderby x.FIO
                               select new
                               {
                                   Номер = "",
                                   ФИО = x.FIO,
                                   Тема_ВКР_назначенная = (NameEngDoc) ? (((!String.IsNullOrEmpty(x.VKRName_Final)) ? x.VKRName_Final : "") + ((!String.IsNullOrEmpty(x.VKRNameEng_Final)) ? "\r\n" + x.VKRNameEng_Final : "")) : x.VKRName_Final,
                                   НПР_ФИО_назначенный = x.NPR_FIO_Final,
                                   НПР_степень_назначенный = x.NPR_Degree_Final,
                                   НПР_звание_назначенный = x.NPR_Rank_Final,
                                   НПР_должность_назначенный = x.NPR_Position_Final,
                                   НПР_кафедра_назначенный = x.NPR_Chair_Final,
                                   //x.Id
                               }).ToList();
                    dgvDoc.DataSource = lst;

                    TableDoc td = wd.Tables[1];
                    int curRow = 0;
                    //foreach (DataGridViewColumn c in dgvDoc.Columns)
                    //{
                    //    td[c.Index, curRow] = c.HeaderText.ToString();
                    //}
                    curRow = 1;
                    int i = 0;
                    foreach (DataGridViewRow rw in dgvDoc.Rows)
                    {
                        td.AddRow(1);
                        curRow++;
                        i++;
                        foreach (DataGridViewCell cell in rw.Cells)
                        {
                            if (cell.Value != null)
                                if (cell.ColumnIndex == 0)
                                {
                                    td[cell.ColumnIndex, curRow] = i.ToString() + ".";
                                }
                                else
                                {
                                    td[cell.ColumnIndex, curRow] = cell.Value.ToString();
                                }
                        }
                    }
                }


                //Сохранение файла
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch
                    { }
                }
                string fpath = filePath;
                try
                {
                    filePath = TempFilesFolder + nameshort + " " + OPNumber + type;
                }
                catch (Exception)
                {
                    filePath = fpath;
                }

                wd.Save(filePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сформировать документ\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void DopSupplementToDoc(int? opid)
        {
            if (!opid.HasValue)
                return;

            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "";
            templatename = "Приложение к приказу ВКР и НР";
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {

                    var template =  context.Templates.Where(x=>x.TemplateName == templatename).Select(x=>x).First();

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
            string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Приказы по ВКР и НР\";
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

            WordDoc wd = new WordDoc(string.Format(filePath), true);

            //заполнение шаблона
            try
            {
                string ObrazProgram = "";
                string LicenseProgram = "";
                //string UNKUNP = "";
                string Stlevel = "";
                string OPNumber = "";
                string OPName = "";
                string LPCode = "";
                string LPName = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgram
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               where x.Id == opid
                               select x).First();

                    Stlevel = lst.LicenseProgram.StudyLevel.Name;
                    OPNumber = lst.Number;
                    OPName = lst.Name;
                    LPCode = lst.LicenseProgram.Code;
                    LPName = lst.LicenseProgram.Name;
                }
                switch (Stlevel)
                {
                    case "бакалавриат":
                        ObrazProgram = "бакалавриата CB." + OPNumber + ".*";
                        LicenseProgram = "по направлению подготовки ";
                        break;
                    case "магистратура":
                        ObrazProgram = "магистратуры BM." + OPNumber + ".*";
                        LicenseProgram = "по направлению подготовки ";
                        break;
                    case "специалитет":
                        ObrazProgram = "специалитета CM." + OPNumber + ".*";
                        LicenseProgram = "по специальности ";
                        break;
                    default:
                        break;
                }
                ObrazProgram += " " + "«" + OPName + "»";
                LicenseProgram += LPCode + " " + "«" + LPName + "»";

                wd.SetFields("ObrazProgram", ObrazProgram);
                wd.SetFields("LicenseProgram", LicenseProgram);
                //wd.SetFields("UNKUNP", "");

                //Заполнение таблицы
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               //join fac in context.Faculty on x.FacultyId equals fac.Id into _fac
                               //from fac in _fac.DefaultIfEmpty()
                               where (x.ObrazProgramId == opid) &&
                               ((rbtnFilledStudent.Checked) ? x.StatusName == "студ" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != null : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != null : true) &&
                               ((x.OrderNumber == null) || (x.OrderNumber == ""))

                               orderby x.FIO
                               select new
                               {
                                   Номер = "",
                                   ФИО = x.FIO,
                                   Тема_ВКР_назначенная = (NameEngDoc) ? (((!String.IsNullOrEmpty(x.VKRName_Final)) ? x.VKRName_Final : "") + ((!String.IsNullOrEmpty(x.VKRNameEng_Final)) ? "\r\n" + x.VKRNameEng_Final : "")) : x.VKRName_Final,
                                   //Тема_ВКР_назначенная = (NameEngDoc) ? (x.VKRName_Final + "\r\n" + x.VKRNameEng_Final) : x.VKRName_Final,
                                   //Тема_ВКР_англ_назначенная = x.VKRNameEng_Final,
                                   НПР_ФИО_назначенный = x.NPR_FIO_Final,
                                   НПР_степень_назначенный = x.NPR_Degree_Final,
                                   НПР_звание_назначенный = x.NPR_Rank_Final,
                                   НПР_должность_назначенный = x.NPR_Position_Final,
                                   НПР_кафедра_назначенный = x.NPR_Chair_Final,
                                   //x.Id
                               }).ToList();
                    dgvDoc.DataSource = lst;

                    //определение макс номера в приказе
                    var lst2 = (from x in context.VKR_ThemesStudentOrder
                               //join fac in context.Faculty on x.FacultyId equals fac.Id into _fac
                               //from fac in _fac.DefaultIfEmpty()
                               where (x.ObrazProgramId == opid) &&
                               ((rbtnFilledStudent.Checked) ? x.StatusName == "студ" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != null : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != null : true) &&
                               (x.StudentNumberInOrder.HasValue)
                               orderby x.StudentNumberInOrder // x.FIO
                               select new
                               {
                                   Номер = x.StudentNumberInOrder
                               }).ToList().Last();

                    TableDoc td = wd.Tables[1];
                    int curRow = 0;
                    //foreach (DataGridViewColumn c in dgvDoc.Columns)
                    //{
                    //    td[c.Index, curRow] = c.HeaderText.ToString();
                    //}
                    curRow = 1;
                    int i = 0;
                    try
                    {
                        i = int.Parse(lst2.Номер.ToString());
                    }
                    catch (Exception)
                    {
                    }
                    
                    foreach (DataGridViewRow rw in dgvDoc.Rows)
                    {
                        td.AddRow(1);
                        curRow++;
                        i++;
                        foreach (DataGridViewCell cell in rw.Cells)
                        {
                            if (cell.Value != null)
                                if (cell.ColumnIndex == 0)
                                {
                                    td[cell.ColumnIndex, curRow] = i.ToString() + ".";
                                }
                                else
                                {
                                    td[cell.ColumnIndex, curRow] = cell.Value.ToString();
                                }
                        }
                    }
                    //td.DeleteLastRow();
                    //wd.AddParagraph(" ");
                    //wd.AddParagraph(" ");

                }


                //Сохранение файла
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch
                    { }
                }
                string fpath = filePath;
                try
                {
                    filePath = TempFilesFolder + nameshort + " " + OPNumber + type;
                }
                catch (Exception)
                {
                    filePath = fpath;
                }

                wd.Save(filePath);

            }
            catch (Exception)
            {
                //MessageBox.Show("Не удалось сформировать документ\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnFreezeOrder_Click(object sender, EventArgs e)
        {
            rbtnFilledStudent.Checked = true;

            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbObrazProgram.DroppedDown = true;
                return;
            }
            //Проверка что уже зафиксировано
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.ObrazProgramId == ObrazProgramId) &&
                               ((rbtnFilledStudent.Checked) ? x.StatusName == "студ" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != null : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != null : true) &&
                               (x.OrderNumber != null) && (x.OrderNumber != "") //&& 
                               //((x.OrderDop == null) || (x.OrderDop == false))
                               orderby x.OrderDate  //x.FIO
                               select new
                               {
                                   x.Id,
                                   //x.Freeze,
                                   x.OrderNumber,
                                   x.OrderDate,
                                   ObPr = ((!String.IsNullOrEmpty(op.Number)) ? op.Number : "") + "  " + ((!String.IsNullOrEmpty(op.Name)) ? op.Name : "")
                                   //x.StudentNumberInOrder
                               }).ToList();
                    if (lst.Count != 0)
                    {
                        string op = lst.First().ObPr;
                        string ordernumber = lst.First().OrderNumber;
                        string orderdate = lst.First().OrderDate.Value.Date.ToString("dd.MM.yyyy");
                        MessageBox.Show("Приказ по выбранной образовательной программе\r\n" + op + "\r\n" + "уже зафиксирован\r\n" + 
                            "№ приказа: " + ordernumber + "\r\n" + "Дата приказа: " + orderdate, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch
            { }

            if (String.IsNullOrEmpty(OrderNumber) || String.IsNullOrEmpty(OrderDate))
            {
                MessageBox.Show("Для фиксации (\"заморозки\") данных\r\n" + "необходимо указать № и дату приказа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!CheckFields())
                return;

            if (MessageBox.Show("Фиксация приказа по ВКР и НР в базе данных\r\n" + cbObrazProgram.Text + "\r\nВыполнить?", "Запрос на подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                return;

            FreezeSupplementData(ObrazProgramId);
        }

        private bool CheckFields()
        {
            DateTime res;
            if (!String.IsNullOrEmpty(OrderDate))
            {
                if (!DateTime.TryParse(OrderDate, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Дата приказа' \r\n" + "Образец: 01.12.2017", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
        }
        private void FreezeSupplementData(int? opid)
        {
            if (!opid.HasValue)
                return;
            if (!CheckFields())
                return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               where (x.ObrazProgramId == opid) &&
                               ((rbtnFilledStudent.Checked) ? x.StatusName == "студ" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != null : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != null : true)
                               orderby x.FIO
                               select new
                               {
                                   x.Id,
                                   //x.Freeze,
                                   //x.OrderNumber,
                                   //x.OrderDate,
                                   //x.StudentNumberInOrder
                               }).ToList();

                    //DataTable dt = new DataTable();
                    //dt = Utilities.ConvertToDataTable(lst);

                    dgvDoc.DataSource = lst;

                    int id;
                    int i = 0;
                    foreach (DataGridViewRow rw in dgvDoc.Rows)
                    {
                        i++;
                        id = int.Parse(rw.Cells["Id"].Value.ToString()); 
                        var vkrst = context.VKR_ThemesStudentOrder.Where(x => x.Id == id).First();
                        if (!String.IsNullOrEmpty(OrderNumber))
                        {
                            vkrst.Freeze = true;
                            vkrst.InOrder = true;
                            vkrst.OrderNumber = OrderNumber;
                            vkrst.OrderDate = DateTime.Parse(OrderDate);
                            vkrst.OrderDop = false;
                            vkrst.StudentNumberInOrder = i;
                        }
                        else
                        {
                            vkrst.Freeze = null;
                            vkrst.InOrder = null;
                            vkrst.OrderNumber = null;
                            vkrst.OrderDate = null;
                            vkrst.OrderDop = null;
                            vkrst.StudentNumberInOrder = null;
                        }
                        context.SaveChanges();
                    }
                    FillStudent();
                    OrderNumber = "";
                    OrderDate = "";
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show("Не удалось зафиксировать данные...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FreezeSupplementDopData(int? opid)
        {
            if (!opid.HasValue)
                return;
            if (!CheckFields())
                return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               where (x.ObrazProgramId == opid) &&
                               ((rbtnFilledStudent.Checked) ? x.StatusName == "студ" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != null : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != null : true) &&
                               ((x.OrderNumber == null) || (x.OrderNumber == ""))
                               orderby x.FIO
                               select new
                               {
                                   x.Id,
                                   //x.Freeze,
                                   //x.OrderNumber,
                                   //x.OrderDate,
                                   //x.StudentNumberInOrder
                               }).ToList();

                    //DataTable dt = new DataTable();
                    //dt = Utilities.ConvertToDataTable(lst);

                    dgvDoc.DataSource = lst;

                    //определение макс номера в приказе
                    var lst2 = (from x in context.VKR_ThemesStudentOrder
                                //join fac in context.Faculty on x.FacultyId equals fac.Id into _fac
                                //from fac in _fac.DefaultIfEmpty()
                                where (x.ObrazProgramId == opid) &&
                                ((rbtnFilledStudent.Checked) ? x.StatusName == "студ" : true) &&
                                ((rbtnFilledStudent.Checked) ? x.VKRName_Final != "" : true) &&
                                ((rbtnFilledStudent.Checked) ? x.VKRName_Final != null : true) &&
                                ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != "" : true) &&
                                ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != null : true) &&
                                (x.StudentNumberInOrder.HasValue)
                                orderby x.StudentNumberInOrder // x.FIO
                                select new
                                {
                                    Номер = x.StudentNumberInOrder
                                }).ToList().Last();
                    int i = 0;
                    try
                    {
                        i = int.Parse(lst2.Номер.ToString());
                    }
                    catch (Exception)
                    {
                    }

                    int id;
                    foreach (DataGridViewRow rw in dgvDoc.Rows)
                    {
                        i++;
                        id = int.Parse(rw.Cells["Id"].Value.ToString());
                        var vkrst = context.VKR_ThemesStudentOrder.Where(x => x.Id == id).First();
                        if (!String.IsNullOrEmpty(OrderNumber))
                        {
                            vkrst.Freeze = true;
                            vkrst.InOrder = true;
                            vkrst.OrderNumber = OrderNumber;
                            vkrst.OrderDate = DateTime.Parse(OrderDate);
                            vkrst.OrderDop = true;
                            vkrst.StudentNumberInOrder = i;
                        }
                        else
                        {
                            vkrst.Freeze = null;
                            vkrst.InOrder = null;
                            vkrst.OrderNumber = null;
                            vkrst.OrderDate = null;
                            vkrst.OrderDop = null;
                            vkrst.StudentNumberInOrder = null;
                        }
                        context.SaveChanges();
                    }
                    FillStudent();
                    OrderNumber = "";
                    OrderDate = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось зафиксировать данные...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UnFreezeSupplementDopData(int? opid)
        {
            if (!opid.HasValue)
                return;
            if (!CheckFields())
                return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               where (x.ObrazProgramId == opid) &&
                               ((rbtnFilledStudent.Checked) ? x.StatusName == "студ" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != null : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != null : true) &&
                               (x.OrderNumber != null) && (x.OrderNumber != "") && (x.OrderDop == true)
                               orderby x.FIO
                               select new
                               {
                                   x.Id,
                                   //x.Freeze,
                                   //x.OrderNumber,
                                   //x.OrderDate,
                                   //x.StudentNumberInOrder
                               }).ToList();

                    dgvDoc.DataSource = lst;

                    int id;
                    foreach (DataGridViewRow rw in dgvDoc.Rows)
                    {
                        id = int.Parse(rw.Cells["Id"].Value.ToString());
                        var vkrst = context.VKR_ThemesStudentOrder.Where(x => x.Id == id).First();
                        
                        vkrst.Freeze = null;
                        vkrst.InOrder = null;
                        vkrst.OrderNumber = null;
                        vkrst.OrderDate = null;
                        vkrst.OrderDop = null;
                        vkrst.StudentNumberInOrder = null;
                        
                        context.SaveChanges();
                    }
                    FillStudent();
                    OrderNumber = "";
                    OrderDate = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось \"разморозить\" данные...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUnFreezeOrder_Click(object sender, EventArgs e)
        {
            rbtnFilledStudent.Checked = true;

            OrderNumber = "";
            OrderDate = "";

            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbObrazProgram.DroppedDown = true;
                return;
            }

            //проверка наличия доп приказов
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.ObrazProgramId == ObrazProgramId) &&
                               ((rbtnFilledStudent.Checked) ? x.StatusName == "студ" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != null : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != null : true) &&
                               (x.OrderNumber != null) && (x.OrderNumber != "") && (x.OrderDop == true)
                               //orderby x.FIO
                               select new
                               {
                                   x.Id,
                                   //x.Freeze,
                                   //x.OrderNumber,
                                   //x.OrderDate,
                                   //ObPr = ((!String.IsNullOrEmpty(op.Number)) ? op.Number : "") + "  " + ((!String.IsNullOrEmpty(op.Name)) ? op.Name : "")
                                   //x.StudentNumberInOrder
                               }).ToList();
                    if (lst.Count != 0)
                    {
                        //string op = lst.First().ObPr;
                        //string ordernumber = lst.First().OrderNumber;
                        //string orderdate = lst.First().OrderDate.Value.Date.ToString("dd.MM.yyyy");
                        MessageBox.Show("Не \"разморожены\" дополнения к приказу.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось проверить наличие дополнений к приказу.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("\"Разморозка\" приказа по ВКР и НР в базе данных\r\n" + cbObrazProgram.Text + "\r\nВыполнить?", "Запрос на подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                return;

            FreezeSupplementData(ObrazProgramId);
        }

        private void btnDopSupplementDoc_Click(object sender, EventArgs e)
        {
            rbtnFilledStudent.Checked = true;

            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbObrazProgram.DroppedDown = true;
                return;
            }

            //DopSupplement = true;
            DopSupplementToDoc(ObrazProgramId);
        }

        private void btnFreezeOrderDop_Click(object sender, EventArgs e)
        {
            rbtnFilledStudent.Checked = true;

            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbObrazProgram.DroppedDown = true;
                return;
            }
            //Проверка фиксации основного приказа
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.ObrazProgramId == ObrazProgramId) &&
                               ((rbtnFilledStudent.Checked) ? x.StatusName == "студ" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != null : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != null : true) &&
                               (x.OrderNumber != null) && (x.OrderNumber != "") &&
                               ((x.OrderDop == null) || (x.OrderDop == false))
                               //orderby x.FIO
                               select new
                               {
                                   x.Id,
                                   //x.Freeze,
                                   //x.OrderNumber,
                                   //x.OrderDate,
                                   //ObPr = ((!String.IsNullOrEmpty(op.Number)) ? op.Number : "") + "  " + ((!String.IsNullOrEmpty(op.Name)) ? op.Name : "")
                                   //x.StudentNumberInOrder
                               }).ToList();
                    if (lst.Count == 0)
                    {
                        //string op = lst.First().ObPr;
                        //string ordernumber = lst.First().OrderNumber;
                        //string orderdate = lst.First().OrderDate.Value.Date.ToString("dd.MM.yyyy");
                        MessageBox.Show("Не зафиксирован основной приказ.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var lst2 = (from x in context.VKR_ThemesStudentOrder
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.ObrazProgramId == ObrazProgramId) &&
                               ((rbtnFilledStudent.Checked) ? x.StatusName == "студ" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != null : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != null : true) &&
                               ((x.OrderNumber == null) || (x.OrderNumber == "")) 
                               //orderby x.FIO
                               select new
                               {
                                   x.Id,
                                   //x.Freeze,
                                   x.OrderNumber,
                                   x.OrderDate,
                                   ObPr = ((!String.IsNullOrEmpty(op.Number)) ? op.Number : "") + "  " + ((!String.IsNullOrEmpty(op.Name)) ? op.Name : "")
                                   //x.StudentNumberInOrder
                               }).ToList();
                    if (lst2.Count == 0)
                    {
                        //string op = lst.First().ObPr;
                        //string ordernumber = lst.First().OrderNumber;
                        //string orderdate = lst.First().OrderDate.Value.Date.ToString("dd.MM.yyyy");
                        MessageBox.Show("Нет данных для дополнительного фиксирования.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch
            { }

            if (String.IsNullOrEmpty(OrderNumber) || String.IsNullOrEmpty(OrderDate))
            {
                MessageBox.Show("Для фиксации (\"заморозки\") данных\r\n" + "необходимо указать № и дату приказа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!CheckFields())
                return;

            if (MessageBox.Show("Фиксация дополнения к приказу по ВКР и НР в базе данных\r\n" + cbObrazProgram.Text + "\r\nВыполнить?", "Запрос на подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                return;

            FreezeSupplementDopData(ObrazProgramId);
        }

        private void btnUnFreezeOrderDop_Click(object sender, EventArgs e)
        {
            rbtnFilledStudent.Checked = true;

            OrderNumber = "";
            OrderDate = "";

            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbObrazProgram.DroppedDown = true;
                return;
            }

            //проверка наличия дополнений
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.ObrazProgramId == ObrazProgramId) &&
                               ((rbtnFilledStudent.Checked) ? x.StatusName == "студ" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.VKRName_Final != null : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != "" : true) &&
                               ((rbtnFilledStudent.Checked) ? x.NPR_FIO_Final != null : true) &&
                               (x.OrderNumber != null) && (x.OrderNumber != "") && (x.OrderDop == true)
                               //orderby x.FIO
                               select new
                               {
                                   x.Id,
                                   //x.Freeze,
                                   //x.OrderNumber,
                                   //x.OrderDate,
                                   //ObPr = ((!String.IsNullOrEmpty(op.Number)) ? op.Number : "") + "  " + ((!String.IsNullOrEmpty(op.Name)) ? op.Name : "")
                                   //x.StudentNumberInOrder
                               }).ToList();
                    if (lst.Count == 0)
                    {
                        //string op = lst.First().ObPr;
                        //string ordernumber = lst.First().OrderNumber;
                        //string orderdate = lst.First().OrderDate.Value.Date.ToString("dd.MM.yyyy");
                        MessageBox.Show("Нет зафиксированных дополнений к основному приказу.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch (Exception)
            {
            }
            if (MessageBox.Show("\"Разморозка\" дополнений к приказу по ВКР и НР в базе данных\r\n" + 
                    "Примечание: в данной версии \"размораживаются\" сразу все дополнения.\r\n" + cbObrazProgram.Text + "\r\nВыполнить?", "Запрос на подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                return;

            UnFreezeSupplementDopData(ObrazProgramId);
        }

        private void chbOrgShowColumns_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
            }
        }

        private void chbReviewShowColumnsDop_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
            }
        }

        private void cbConsultShowColumns_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception)
            {
            }
        }

        private void btnSupplementReviewDoc_Click(object sender, EventArgs e)
        {
            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbObrazProgram.DroppedDown = true;
                return;
            }

            SupplementReviewToDoc(ObrazProgramId);
        }
        private void SupplementReviewToDoc(int? opid)
        {
            if (!opid.HasValue)
                return;

            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "";
            templatename = "Приложение к приказу ВКР НР Рецензенты Орг-ции";
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
            string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Приказы по ВКР и НР\";
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

            WordDoc wd = new WordDoc(string.Format(filePath), true);

            //заполнение шаблона
            try
            {
                string ObrazProgram = "";
                string LicenseProgram = "";
                //string UNKUNP = "";
                string Stlevel = "";
                string OPNumber = "";
                string OPName = "";
                string LPCode = "";
                string LPName = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgram
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               where x.Id == opid
                               select x).First();

                    Stlevel = lst.LicenseProgram.StudyLevel.Name;
                    OPNumber = lst.Number;
                    OPName = lst.Name;
                    LPCode = lst.LicenseProgram.Code;
                    LPName = lst.LicenseProgram.Name;
                }
                switch (Stlevel)
                {
                    case "бакалавриат":
                        ObrazProgram = "бакалавриата CB." + OPNumber + ".*";
                        LicenseProgram = "по направлению подготовки ";
                        break;
                    case "магистратура":
                        ObrazProgram = "магистратуры BM." + OPNumber + ".*";
                        LicenseProgram = "по направлению подготовки ";
                        break;
                    case "специалитет":
                        ObrazProgram = "специалитета CM." + OPNumber + ".*";
                        LicenseProgram = "по специальности ";
                        break;
                    default:
                        break;
                }
                ObrazProgram += " " + "«" + OPName + "»";
                LicenseProgram += LPCode + " " + "«" + LPName + "»";

                wd.SetFields("ObrazProgram", ObrazProgram);
                wd.SetFields("LicenseProgram", LicenseProgram);
                //wd.SetFields("UNKUNP", "");

                //Заполнение таблицы
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               //join fac in context.Faculty on x.FacultyId equals fac.Id into _fac
                               //from fac in _fac.DefaultIfEmpty()
                               join org in context.Organization on x.OrganizationId equals org.Id into _org
                               from org in _org.DefaultIfEmpty()
                               join org2 in context.Organization on x.OrganizationId2 equals org2.Id into _org2
                               from org2 in _org2.DefaultIfEmpty()
                               join org3 in context.Organization on x.OrganizationId3 equals org3.Id into _org3
                               from org3 in _org3.DefaultIfEmpty()
                               join person in context.PartnerPersonOrgPosition on x.Review_PartnerPersonId equals person.Id into _person
                               from person in _person.DefaultIfEmpty()
                               join person2 in context.PartnerPersonOrgPosition on x.Review_PartnerPersonId2 equals person2.Id into _person2
                               from person2 in _person2.DefaultIfEmpty()
                               where (x.ObrazProgramId == opid) && (x.IsActive == true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.StatusName == "студ" : true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.VKRName_Final != "" : true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.VKRName_Final != null : true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.NPR_FIO_Final != "" : true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.NPR_FIO_Final != null : true) &&
                               ((rbtnFilledStudentReview.Checked) ? ((x.OrganizationId != null) || (x.OrganizationId2 != null) || (x.OrganizationId3 != null)) : true) &&
                               ((rbtnFilledStudentReview.Checked) ? ((x.Review_NPR_Persnum != "") && (x.Review_NPR_Persnum != null)) || ((x.Review_NPR_Persnum2 != "") && (x.Review_NPR_Persnum2 != null)) || (x.Review_PartnerPersonId.HasValue) || (x.Review_PartnerPersonId2.HasValue) : true)

                               //&& ((x.OrderDop == false) || (x.OrderDop == null))
                               orderby x.FIO
                               select new
                               {
                                   Номер = "",
                                   ФИО = x.FIO,
                                   Тема_ВКР_назначенная = ((x.VKRName_Changed != "") && (x.VKRName_Changed != null)) ?
                                   ((NameEngReviewDoc) ? (((!String.IsNullOrEmpty(x.VKRName_Changed)) ? x.VKRName_Changed : "") + ((!String.IsNullOrEmpty(x.VKRNameEng_Changed)) ? "\r\n" + x.VKRNameEng_Changed : "")) : x.VKRName_Changed) :
                                   ((NameEngReviewDoc) ? (((!String.IsNullOrEmpty(x.VKRName_Final)) ? x.VKRName_Final : "") + ((!String.IsNullOrEmpty(x.VKRNameEng_Final)) ? "\r\n" + x.VKRNameEng_Final : "")) : x.VKRName_Final),
                                   //Тема_ВКР_назначенная = (NameEngDoc) ? (x.VKRName_Final + "\r\n" + x.VKRNameEng_Final) : x.VKRName_Final,
                                   //Тема_ВКР_англ_назначенная = x.VKRNameEng_Final,
                                   НПР_ФИО_назначенный = ((x.NPR_Changed_Persnum != "") && (x.NPR_Changed_Persnum != null)) ?
                                        (x.NPR_FIO_Changed + ((!String.IsNullOrEmpty(x.NPR_Degree_Changed)) ? ", " + x.NPR_Degree_Changed : "") +
                                        ((!String.IsNullOrEmpty(x.NPR_Rank_Changed)) ? ", " + x.NPR_Rank_Changed : "") + ((!String.IsNullOrEmpty(x.NPR_Position_Changed)) ? ", " + x.NPR_Position_Changed : "") +
                                        ((!String.IsNullOrEmpty(x.NPR_Chair_Changed)) ? ", " + x.NPR_Chair_Changed : "")) : 
                                        (x.NPR_FIO_Final + ((!String.IsNullOrEmpty(x.NPR_Degree_Final)) ? ", " + x.NPR_Degree_Final : "") +
                                        ((!String.IsNullOrEmpty(x.NPR_Rank_Final)) ? ", " + x.NPR_Rank_Final : "") + ((!String.IsNullOrEmpty(x.NPR_Position_Final)) ? ", " + x.NPR_Position_Final : "") +
                                        ((!String.IsNullOrEmpty(x.NPR_Chair_Final)) ? ", " + x.NPR_Chair_Final : "")),
                                   
                                   НПР_Рецензент = ((!String.IsNullOrEmpty(x.Review_NPR_Persnum)) ? x.Review_NPR_FIO + ((!String.IsNullOrEmpty(x.Review_NPR_Degree)) ? ", " + x.Review_NPR_Degree : "") +
                                        ((!String.IsNullOrEmpty(x.Review_NPR_Rank)) ? ", " + x.Review_NPR_Rank : "") + ((!String.IsNullOrEmpty(x.Review_NPR_Position)) ? ", " + x.Review_NPR_Position : "") +
                                        ((!String.IsNullOrEmpty(x.Review_NPR_Chair)) ? ", " + x.Review_NPR_Chair : "") : "") + 
                                        ((!String.IsNullOrEmpty(x.Review_NPR_Persnum2)) ? ((!String.IsNullOrEmpty(x.Review_NPR_Persnum)) ? ", " : "") + x.Review_NPR_FIO2 + ((!String.IsNullOrEmpty(x.Review_NPR_Degree2)) ? ", " + x.Review_NPR_Degree2 : "") +
                                        ((!String.IsNullOrEmpty(x.Review_NPR_Rank2)) ? ", " + x.Review_NPR_Rank2 : "") + ((!String.IsNullOrEmpty(x.Review_NPR_Position2)) ? ", " + x.Review_NPR_Position2 : "") +
                                        ((!String.IsNullOrEmpty(x.Review_NPR_Chair2)) ? ", " + x.Review_NPR_Chair2 : "") : "") +
                                        //Рецензенты из "Партнера"
                                        ((x.Review_PartnerPersonId.HasValue) ? person.Name + ((!String.IsNullOrEmpty(person.DegreeName)) ? ", " + person.DegreeName : "") + 
                                        ((!String.IsNullOrEmpty(person.RankName)) ? ", " + person.RankName : "") + 
                                        ((!String.IsNullOrEmpty(person.OrgPosition)) ? ", " + person.OrgPosition : "") : "") + 
                                        ((x.Review_PartnerPersonId2.HasValue) ? ((x.Review_PartnerPersonId.HasValue) ? ", " : "") + person2.Name + ((!String.IsNullOrEmpty(person2.DegreeName)) ? ", " + person2.DegreeName : "") + 
                                        ((!String.IsNullOrEmpty(person2.RankName)) ? ", " + person2.RankName : "") + 
                                        ((!String.IsNullOrEmpty(person2.OrgPosition)) ? ", " + person2.OrgPosition : "") : ""),
                                         
                                   НПР_Организация = ((x.OrganizationId.HasValue) ? (org.Name + ((!String.IsNullOrEmpty(x.OrganizationDocument)) ? "\r\n" + x.OrganizationDocument : "")) : "") +
                                    ((x.OrganizationId2.HasValue) ? (((x.OrganizationId.HasValue) ? ", " : "") + org2.Name + ((!String.IsNullOrEmpty(x.OrganizationDocument2)) ? "\r\n" + x.OrganizationDocument2 : "")) : "") + 
                                    ((x.OrganizationId3.HasValue) ? ((((x.OrganizationId.HasValue) || (x.OrganizationId2.HasValue)) ? ", " : "") + org3.Name + ((!String.IsNullOrEmpty(x.OrganizationDocument3)) ? "\r\n" + x.OrganizationDocument3 : "")) : ""),
                                   //x.Id
                               }).ToList();
                    dgvReviewDoc.DataSource = lst;

                    TableDoc td = wd.Tables[1];
                    int curRow = 0;
                    //foreach (DataGridViewColumn c in dgvDoc.Columns)
                    //{
                    //    td[c.Index, curRow] = c.HeaderText.ToString();
                    //}
                    curRow = 1;
                    int i = 0;
                    foreach (DataGridViewRow rw in dgvReviewDoc.Rows)
                    {
                        td.AddRow(1);
                        curRow++;
                        i++;
                        foreach (DataGridViewCell cell in rw.Cells)
                        {
                            if (cell.Value != null)
                                if (cell.ColumnIndex == 0)
                                {
                                    td[cell.ColumnIndex, curRow] = i.ToString() + ".";
                                }
                                else
                                {
                                    td[cell.ColumnIndex, curRow] = cell.Value.ToString();
                                }
                        }
                    }
                    //td.DeleteLastRow();
                    //wd.AddParagraph(" ");
                    //wd.AddParagraph(" ");

                }


                //Сохранение файла
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch
                    { }
                }
                string fpath = filePath;
                try
                {
                    filePath = TempFilesFolder + nameshort + " " + OPNumber + type;
                }
                catch (Exception)
                {
                    filePath = fpath;
                }

                wd.Save(filePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сформировать документ\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (e.ColumnIndex == dgv.Columns["Нет_в_справочнике"].Index && e.Value.ToString() == "нет в справочнике")
            //{
            //    e.CellStyle.BackColor = Color.LightPink;
            //}
            //if (e.ColumnIndex == dgv.Columns["ФИО"].Index && dgv.Rows[e.RowIndex].Cells["Нет_в_справочнике"].Value.ToString() == "нет в справочнике")
            //{
            //    e.CellStyle.BackColor = Color.LightPink;
            //}
        }

        private void chbNPRChangedShowColumns_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void btnOrderReviewDoc_Click(object sender, EventArgs e)
        {
            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbObrazProgram.DroppedDown = true;
                return;
            }

            OrderReviewToDoc(ObrazProgramId);
        }
        private void OrderReviewToDoc(int? opid)
        {
            if (!opid.HasValue)
                return;

            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "";
            templatename = "Приказ ВКР НР Рецензенты Орг-ции";
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
            string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Приказы по ВКР и НР\";
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

            WordDoc wd = new WordDoc(string.Format(filePath), true);

            //заполнение шаблона
            try
            {
                string ObrazProgram = "";
                string ObrazProgramHeader = "";
                string LicenseProgram = "";
                //string UNKUNP = "";
                string Stlevel = "";
                string OPNumber = "";
                string OPName = "";
                string LPCode = "";
                string LPName = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgram
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               where x.Id == opid
                               select x).First();

                    Stlevel = lst.LicenseProgram.StudyLevel.Name;
                    OPNumber = lst.Number;
                    OPName = lst.Name;
                    LPCode = lst.LicenseProgram.Code;
                    LPName = lst.LicenseProgram.Name;
                }
                switch (Stlevel)
                {
                    case "бакалавриат":
                        ObrazProgram = "бакалавриата CB." + OPNumber + ".*";
                        ObrazProgramHeader = "бакалавриата (шифр CB." + OPNumber + ".*)";
                        LicenseProgram = "по направлению подготовки ";
                        break;
                    case "магистратура":
                        ObrazProgram = "магистратуры BM." + OPNumber + ".*";
                        ObrazProgramHeader = "магистратуры (шифр BM." + OPNumber + ".*)";
                        LicenseProgram = "по направлению подготовки ";
                        break;
                    case "специалитет":
                        ObrazProgram = "специалитета CM." + OPNumber + ".*";
                        ObrazProgramHeader = "специалитета (шифр CM." + OPNumber + ".*)";
                        LicenseProgram = "по специальности  ";
                        break;
                    default:
                        break;
                }
                ObrazProgram += " " + "«" + OPName + "»";
                LicenseProgram += LPCode + " " + "«" + LPName + "»";

                wd.SetFields("ObrazProgramHeader", ObrazProgramHeader);
                wd.SetFields("ObrazProgram", ObrazProgram);
                wd.SetFields("LicenseProgram", LicenseProgram);
                //wd.SetFields("UNKUNP", "");

                int FacId;
                int StLevelId;
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgram
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               where x.Id == opid
                               select x).First();

                    FacId = lst.FacultyId;
                    StLevelId = lst.LicenseProgram.StudyLevel.Id;
                }

                string basis = "";
                string chairman = "";
                string send = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var quan = (from x in context.VKR_OrderSource
                                where (x.FacultyId == FacId) && (x.StudyLevelId == StLevelId)
                                select x).Count();
                    var lst = (from x in context.VKR_OrderSource
                               where (x.FacultyId == FacId) && ((quan > 0) ? x.StudyLevelId == StLevelId : x.StudyLevelId == null)
                               select x).First();
                    basis = lst.Basis;
                    chairman = lst.ChairmanForOrder;
                    send = ((!String.IsNullOrEmpty(lst.ChairmanForSend)) ? lst.ChairmanForSend : "") +
                            ((!String.IsNullOrEmpty(lst.UOPForSend)) ? "\r\n" + lst.UOPForSend : "") +
                            ((!String.IsNullOrEmpty(lst.UUForSend)) ? "\r\n" + lst.UUForSend : "");
                }
                wd.SetFields("Send", send);

                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch
                    { }
                }
                string fpath = filePath;
                try
                {
                    filePath = TempFilesFolder + nameshort + " " + OPNumber + type;
                }
                catch (Exception)
                {
                    filePath = fpath;
                }

                wd.Save(filePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сформировать документ\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnOrgXLS_Click(object sender, EventArgs e)
        {
            OrgToExcel();
        }

        private void OrgToExcel()
        {
            try
            {
                string filenameDate = "Список организаций ВКР";
                string filename = Util.TempFilesFolder + filenameDate + ".xlsx";
                string[] fileList = Directory.GetFiles(Util.TempFilesFolder, "Список организаций ВКР*" + ".xlsx");

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

                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        var lst = (from x in context.VKR_OrganizationList
                                   orderby x.Name
                                   select new
                                   {
                                       x.Id,
                                       Полное_наименование = x.Name,
                                       Среднее_наименование = x.MiddleName,
                                       Краткое_наименование = x.ShortName,
                                       Наименование_англ = x.NameEng,
                                       Краткое_наименование_англ = x.ShortNameEng,
                                       Англ_наимен_из_офиц_источника = ((bool)x.EngSourceOfficial) ? "официальное" : "",
                                   }).ToList();

                        dgvExcel.DataSource = lst;
                        foreach (DataGridViewColumn col in dgvExcel.Columns)
                            col.HeaderText = col.Name.Replace("_", " ");
                    }

                    int colind = 0;

                    foreach (DataGridViewColumn cl in dgvExcel.Columns)
                    {
                        ws.Cells[1, ++colind].Value = cl.HeaderText.ToString();
                        ws.Cells[1, colind].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin, darkGray);
                        ws.Cells[1, colind].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        ws.Cells[1, colind].Style.Fill.BackgroundColor.SetColor(lightGray);
                    }

                    for (int rwInd = 0; rwInd < dgvExcel.Rows.Count; rwInd++)
                    {
                        DataGridViewRow rw = dgvExcel.Rows[rwInd];
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
                    int clmnInd = 1;
                    foreach (DataGridViewColumn clmn in dgvExcel.Columns)
                    {
                        if (clmn.Name == "Полное_наименование")
                            ws.Column(++clmnInd).Width = 60;
                        else if (clmn.Name == "Среднее_наименование")
                            ws.Column(++clmnInd).Width = 60;
                        else if (clmn.Name == "Краткое_наименование" || clmn.Name == "Наименование_англ" || clmn.Name == "Краткое_наименование_англ")
                            ws.Column(++clmnInd).Width = 40;
                      
                    }
                    doc.Save();
                }
                System.Diagnostics.Process.Start(filename);
            }
            catch
            {
            }
        }

        private void btnFreezeOrderReview_Click(object sender, EventArgs e)
        {
            rbtnFilledStudent.Checked = true;

            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbObrazProgram.DroppedDown = true;
                return;
            }
            //Проверка что уже зафиксировано
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.ObrazProgramId == ObrazProgramId) && (x.IsActive == true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.StatusName == "студ" : true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.VKRName_Final != "" : true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.VKRName_Final != null : true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.NPR_FIO_Final != "" : true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.NPR_FIO_Final != null : true) &&
                               ((rbtnFilledStudentReview.Checked) ? ((x.OrganizationId != null) || (x.OrganizationId2 != null) || (x.OrganizationId3 != null)) : true) &&
                               ((rbtnFilledStudentReview.Checked) ? ((x.Review_NPR_Persnum != "") && (x.Review_NPR_Persnum != null)) || ((x.Review_NPR_Persnum2 != "") && (x.Review_NPR_Persnum2 != null)) || (x.Review_PartnerPersonId.HasValue) || (x.Review_PartnerPersonId2.HasValue) : true)//&& 
                               orderby x.OrderDate  //x.FIO
                               select new
                               {
                                   x.Id,
                                   //x.Freeze,
                                   x.OrderNumberReview,
                                   x.OrderDateReview,
                                   ObPr = ((!String.IsNullOrEmpty(op.Number)) ? op.Number : "") + "  " + ((!String.IsNullOrEmpty(op.Name)) ? op.Name : "")
                               }).ToList();
                    if (lst.Count != 0)
                    {
                        string op = lst.First().ObPr;
                        string ordernumberreview = lst.First().OrderNumberReview;
                        string orderdatereview = lst.First().OrderDateReview.Value.Date.ToString("dd.MM.yyyy");
                        MessageBox.Show("Приказ (рецензенты и организации)\r\n по выбранной образовательной программе\r\n" + op + "\r\n" + "уже зафиксирован\r\n" +
                            "№ приказа: " + ordernumberreview + "\r\n" + "Дата приказа: " + orderdatereview, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch
            { }

            if (String.IsNullOrEmpty(OrderNumberReview) || String.IsNullOrEmpty(OrderDateReview))
            {
                MessageBox.Show("Для фиксации (\"заморозки\") данных\r\n" + "необходимо указать № и дату приказа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!CheckFieldsReview())
                return;

            if (MessageBox.Show("Фиксация приказа по ВКР и НР (рецензенты и организации) в базе данных\r\n" + cbObrazProgram.Text + "\r\nВыполнить?", "Запрос на подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                return;

            FreezeSupplementDataReview(ObrazProgramId);

        }
        private bool CheckFieldsReview()
        {
            DateTime res;
            if (!String.IsNullOrEmpty(OrderDateReview))
            {
                if (!DateTime.TryParse(OrderDateReview, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Дата приказа' \r\n" + "Образец: 01.12.2017", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            return true;
        }
        private void FreezeSupplementDataReview(int? opid)
        {
            if (!opid.HasValue)
                return;
            if (!CheckFieldsReview())
                return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               where (x.ObrazProgramId == opid) && (x.IsActive == true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.StatusName == "студ" : true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.VKRName_Final != "" : true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.VKRName_Final != null : true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.NPR_FIO_Final != "" : true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.NPR_FIO_Final != null : true) &&
                               ((rbtnFilledStudentReview.Checked) ? ((x.OrganizationId != null) || (x.OrganizationId2 != null) || (x.OrganizationId3 != null)) : true) &&
                               ((rbtnFilledStudentReview.Checked) ? ((x.Review_NPR_Persnum != "") && (x.Review_NPR_Persnum != null)) || ((x.Review_NPR_Persnum2 != "") && (x.Review_NPR_Persnum2 != null)) || (x.Review_PartnerPersonId.HasValue) || (x.Review_PartnerPersonId2.HasValue) : true)
                               orderby x.FIO
                               select new
                               {
                                   x.Id,
                                 
                               }).ToList();

                    dgvReviewDoc.DataSource = lst;

                    int id;
                    int i = 0;
                    foreach (DataGridViewRow rw in dgvReviewDoc.Rows)
                    {
                        i++;
                        id = int.Parse(rw.Cells["Id"].Value.ToString());
                        var vkrst = context.VKR_ThemesStudentOrder.Where(x => x.Id == id).First();
                        if (!String.IsNullOrEmpty(OrderNumberReview))
                        {
                            vkrst.FreezeReview = true;
                            vkrst.InOrderReview = true;
                            vkrst.OrderNumberReview = OrderNumberReview;
                            vkrst.OrderDateReview = DateTime.Parse(OrderDateReview);
                            vkrst.OrderDopReview = false;
                            vkrst.StudentNumberInOrderReview = i;
                        }
                        else
                        {
                            vkrst.FreezeReview = null;
                            vkrst.InOrderReview = null;
                            vkrst.OrderNumberReview = null;
                            vkrst.OrderDateReview = null;
                            vkrst.OrderDopReview = null;
                            vkrst.StudentNumberInOrderReview = null;
                        }
                        context.SaveChanges();
                    }
                    FillStudent();
                    OrderNumberReview = "";
                    OrderDateReview = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось зафиксировать данные...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //////
        private void UnFreezeSupplementDataReview(int? opid)
        {
            if (!opid.HasValue)
                return;
            if (!CheckFieldsReview())
                return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               where (x.ObrazProgramId == opid) //&& (x.IsActive == true) &&
                               orderby x.FIO
                               select new
                               {
                                   x.Id,
                               }).ToList();

                    dgvReviewDoc.DataSource = lst;

                    int id;
                    int i = 0;
                    foreach (DataGridViewRow rw in dgvReviewDoc.Rows)
                    {
                        i++;
                        id = int.Parse(rw.Cells["Id"].Value.ToString());
                        var vkrst = context.VKR_ThemesStudentOrder.Where(x => x.Id == id).First();

                        vkrst.FreezeReview = null;
                        vkrst.InOrderReview = null;
                        vkrst.OrderNumberReview = null;
                        vkrst.OrderDateReview = null;
                        vkrst.OrderDopReview = null;
                        vkrst.StudentNumberInOrderReview = null;

                        context.SaveChanges();
                    }
                    FillStudent();
                    OrderNumberReview = "";
                    OrderDateReview = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось \"разморозить\" данные...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //////

        private void btnUnFreezeOrderReview_Click(object sender, EventArgs e)
        {
            rbtnFilledStudentReview.Checked = true;

            OrderNumberReview = "";
            OrderDateReview = "";

            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbObrazProgram.DroppedDown = true;
                return;
            }

            //проверка наличия доп приказов
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.ObrazProgramId == ObrazProgramId) &&
                               ((rbtnFilledStudentReview.Checked) ? x.StatusName == "студ" : true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.VKRName_Final != "" : true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.VKRName_Final != null : true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.NPR_FIO_Final != "" : true) &&
                               ((rbtnFilledStudentReview.Checked) ? x.NPR_FIO_Final != null : true) &&
                               ((rbtnFilledStudentReview.Checked) ? ((x.OrganizationId != null) || (x.OrganizationId2 != null) || (x.OrganizationId3 != null)) : true) &&
                               ((rbtnFilledStudentReview.Checked) ? ((x.Review_NPR_Persnum != "") && (x.Review_NPR_Persnum != null)) || ((x.Review_NPR_Persnum2 != "") && (x.Review_NPR_Persnum2 != null)) || (x.Review_PartnerPersonId.HasValue) || (x.Review_PartnerPersonId2.HasValue) : true) &&
                               (x.OrderNumberReview != null) && (x.OrderNumberReview != "") && (x.OrderDopReview == true)
                               //orderby x.FIO
                               select new
                               {
                                   x.Id,
                                 
                               }).ToList();
                    if (lst.Count != 0)
                    {
                        MessageBox.Show("Не \"разморожены\" дополнения к приказу.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось проверить наличие дополнений к приказу.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            
            if (MessageBox.Show("\"Разморозка\" приказа по ВКР и НР (рецензенты и организации) в базе данных\r\n" + cbObrazProgram.Text + "\r\nВыполнить?", "Запрос на подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                return;

            UnFreezeSupplementDataReview(ObrazProgramId);
        }

        private void btnOrderReviewChangesDoc_Click(object sender, EventArgs e)
        {
            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbObrazProgram.DroppedDown = true;
                return;
            }
            //проверка наличия приказа о рецензентах
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.ObrazProgramId == ObrazProgramId) &&
                               (x.OrderNumberReview != null) && (x.OrderNumberReview != "") //&& (x.OrderDopReview == true)
                               select new
                               {
                                   x.Id,
                               }).ToList();
                    if (lst.Count == 0)
                    {
                        MessageBox.Show("Не зафиксирован (отсутствует) приказ о рецензентах.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось проверить наличие приказа о рецензентах.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //проверка наличия данных об изменениях
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.ObrazProgramId == ObrazProgramId) &&
                               (x.OrderNumberReview != null) && (x.OrderNumberReview != "") && //&& (x.OrderDopReview == true)
                               (
                               ((x.NPR_FIO_Changed2 != null) && (x.NPR_FIO_Changed2 != "")) || ((x.VKRName_Changed2 != null) && (x.VKRName_Changed2 != "")) || x.OrganizationId_Changed2.HasValue ||
                               ((x.Review_NPR_FIO_Changed2 != null) && (x.Review_NPR_FIO_Changed2 != "")) || ((x.Review_NPR_FIO2_Changed2 != null) && (x.Review_NPR_FIO2_Changed2 != "")) || 
                               x.Review_PartnerPersonId_Changed2.HasValue || x.Review_PartnerPersonId2_Changed2.HasValue
                               )
                               select new
                               {
                                   x.Id,
                               }).ToList();
                    if (lst.Count == 0)
                    {
                        MessageBox.Show("Нет данных для внесения изменений в приказ о рецензентах", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось проверить наличие данных \r\nдля внесения изменений в приказ о рецензентах.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OrderReviewChangesToDoc(ObrazProgramId);

            SupplementReviewChangesToDoc(ObrazProgramId, null); //null - печать только изменений, 1 (любое число) - печать новой редакции (всего документа)
        }
        private void OrderReviewChangesToDoc(int? opid)
        {
            if (!opid.HasValue)
                return;

            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "";
            templatename = "Приказ ВКР НР Рецензенты Изменения";
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
            string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Приказы по ВКР и НР\";
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

            WordDoc wd = new WordDoc(string.Format(filePath), true);

            //заполнение шаблона
            try
            {
                string ObrazProgram = "";
                string ObrazProgramHeader = "";
                string LicenseProgram = "";
                //string UNKUNP = "";
                string Stlevel = "";
                string OPNumber = "";
                string OPName = "";
                string LPCode = "";
                string LPName = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgram
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               where x.Id == opid
                               select x).First();

                    Stlevel = lst.LicenseProgram.StudyLevel.Name;
                    OPNumber = lst.Number;
                    OPName = lst.Name;
                    LPCode = lst.LicenseProgram.Code;
                    LPName = lst.LicenseProgram.Name;
                }
                switch (Stlevel)
                {
                    case "бакалавриат":
                        ObrazProgram = "бакалавриата CB." + OPNumber + ".*";
                        ObrazProgramHeader = "бакалавриата (шифр CB." + OPNumber + ".*)";
                        LicenseProgram = "по направлению подготовки ";
                        break;
                    case "магистратура":
                        ObrazProgram = "магистратуры BM." + OPNumber + ".*";
                        ObrazProgramHeader = "магистратуры (шифр BM." + OPNumber + ".*)";
                        LicenseProgram = "по направлению подготовки ";
                        break;
                    case "специалитет":
                        ObrazProgram = "специалитета CM." + OPNumber + ".*";
                        ObrazProgramHeader = "специалитета (шифр CM." + OPNumber + ".*)";
                        LicenseProgram = "по специальности  ";
                        break;
                    default:
                        break;
                }
                ObrazProgram += " " + "«" + OPName + "»";
                LicenseProgram += LPCode + " " + "«" + LPName + "»";

                wd.SetFields("ObrazProgramHeader", ObrazProgramHeader);
                wd.SetFields("ObrazProgram", ObrazProgram);
                //wd.SetFields("LicenseProgram", LicenseProgram);
                //wd.SetFields("UNKUNP", "");

                //номер приказа
                string Order = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               where (x.ObrazProgramId == ObrazProgramId) &&
                               (x.OrderNumberReview != null) && (x.OrderNumberReview != "") && (x.OrderDopReview == false)
                               select x).First();
                    Order = "от " + ((lst.OrderDateReview.HasValue) ? lst.OrderDateReview.Value.ToString("dd.MM.yyyy") : "") + " № " + lst.OrderNumberReview;
                }

                wd.SetFields("OrderNumber", Order);
                wd.SetFields("OrderNumber2", Order);

                //номера пуктов приказа, где содержатся изменения
                string LineNumbers = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               where (x.ObrazProgramId == opid) && (x.IsActive == true) &&
                               (x.OrderNumberReview != null) && (x.OrderNumberReview != "") &&
                               (x.OrderNumberReview != null) && (x.OrderNumberReview != "") &&
                               (
                               ((x.NPR_FIO_Changed2 != null) && (x.NPR_FIO_Changed2 != "")) || ((x.VKRName_Changed2 != null) && (x.VKRName_Changed2 != "")) || x.OrganizationId_Changed2.HasValue ||
                               ((x.Review_NPR_FIO_Changed2 != null) && (x.Review_NPR_FIO_Changed2 != "")) || ((x.Review_NPR_FIO2_Changed2 != null) && (x.Review_NPR_FIO2_Changed2 != "")) ||
                               x.Review_PartnerPersonId_Changed2.HasValue || x.Review_PartnerPersonId2_Changed2.HasValue
                               )
                               orderby x.FIO
                               select new
                               {
                                   Номер = x.StudentNumberInOrderReview,
                               }).ToList();
                    try
                    {
                        int i = 0;
                        foreach (var item in lst)
                        {
                            if (item.Номер.HasValue)
                            {
                                LineNumbers += (i > 0) ? ", " + item.Номер.ToString() : item.Номер.ToString() ;
                                i++;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }

                wd.SetFields("LineNumbers", LineNumbers);

                //основания и рассылка
                int FacId;
                int StLevelId;
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgram
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               //join fac in context.Faculty on x.FacultyId equals fac.Id
                               where x.Id == opid
                               select x).First();

                    FacId = lst.FacultyId;
                    StLevelId = lst.LicenseProgram.StudyLevel.Id;
                }

                string basis = "";
                string chairman = "";
                string send = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var quan = (from x in context.VKR_OrderSource
                                where (x.FacultyId == FacId) && (x.StudyLevelId == StLevelId)
                                select x).Count();
                    var lst = (from x in context.VKR_OrderSource
                               where (x.FacultyId == FacId) && ((quan > 0) ? x.StudyLevelId == StLevelId : x.StudyLevelId == null)
                               select x).First();
                    basis = lst.Basis;
                    chairman = lst.ChairmanForOrder;
                    send = ((!String.IsNullOrEmpty(lst.ChairmanForSend)) ? lst.ChairmanForSend : "") +
                            ((!String.IsNullOrEmpty(lst.UOPForSend)) ? "\r\n" + lst.UOPForSend : "") +
                            ((!String.IsNullOrEmpty(lst.UUForSend)) ? "\r\n" + lst.UUForSend : "");
                }
                //wd.SetFields("Chairman", chairman);
                //wd.SetFields("Basis", basis);
                wd.SetFields("Send", send);

                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch
                    { }
                }
                string fpath = filePath;
                try
                {
                    filePath = TempFilesFolder + nameshort + " " + OPNumber + type;
                }
                catch (Exception)
                {
                    filePath = fpath;
                }

                wd.Save(filePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сформировать документ\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        //////
        private void SupplementReviewChangesToDoc(int? opid, int? newedition)
        {
            if (!opid.HasValue)
                return;

            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "";
            if (newedition.HasValue)
            {
                templatename = "Приложение к приказу ВКР НР Рецензенты - ноаая ред";
            }
            else
            {
                templatename = "Приложение к приказу ВКР НР Рецензенты Изменения";
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
            string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Приказы по ВКР и НР\";
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

            WordDoc wd = new WordDoc(string.Format(filePath), true);

            //заполнение шаблона
            try
            {
                string ObrazProgram = "";
                string LicenseProgram = "";
                //string UNKUNP = "";
                string Stlevel = "";
                string OPNumber = "";
                string OPName = "";
                string LPCode = "";
                string LPName = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgram
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               where x.Id == opid
                               select x).First();

                    Stlevel = lst.LicenseProgram.StudyLevel.Name;
                    OPNumber = lst.Number;
                    OPName = lst.Name;
                    LPCode = lst.LicenseProgram.Code;
                    LPName = lst.LicenseProgram.Name;
                }
                switch (Stlevel)
                {
                    case "бакалавриат":
                        ObrazProgram = "бакалавриата CB." + OPNumber + ".*";
                        LicenseProgram = "по направлению подготовки ";
                        break;
                    case "магистратура":
                        ObrazProgram = "магистратуры BM." + OPNumber + ".*";
                        LicenseProgram = "по направлению подготовки ";
                        break;
                    case "специалитет":
                        ObrazProgram = "специалитета CM." + OPNumber + ".*";
                        LicenseProgram = "по специальности ";
                        break;
                    default:
                        break;
                }
                ObrazProgram += " " + "«" + OPName + "»";
                LicenseProgram += LPCode + " " + "«" + LPName + "»";

                wd.SetFields("ObrazProgram", ObrazProgram);
                wd.SetFields("LicenseProgram", LicenseProgram);
                //wd.SetFields("UNKUNP", "");

                //Заполнение таблицы
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               //join fac in context.Faculty on x.FacultyId equals fac.Id into _fac
                               //from fac in _fac.DefaultIfEmpty()
                               join org in context.Organization on x.OrganizationId equals org.Id into _org
                               from org in _org.DefaultIfEmpty()
                               join org2 in context.Organization on x.OrganizationId2 equals org2.Id into _org2
                               from org2 in _org2.DefaultIfEmpty()
                               join org3 in context.Organization on x.OrganizationId3 equals org3.Id into _org3
                               from org3 in _org3.DefaultIfEmpty()
                               join org_Changed2 in context.Organization on x.OrganizationId_Changed2 equals org_Changed2.Id into _org_Changed2
                               from org_Changed2 in _org_Changed2.DefaultIfEmpty()
                               join person in context.PartnerPersonOrgPosition on x.Review_PartnerPersonId equals person.Id into _person
                               from person in _person.DefaultIfEmpty()
                               join person2 in context.PartnerPersonOrgPosition on x.Review_PartnerPersonId2 equals person2.Id into _person2
                               from person2 in _person2.DefaultIfEmpty()
                               join person_Changed2 in context.PartnerPersonOrgPosition on x.Review_PartnerPersonId_Changed2 equals person_Changed2.Id into _person_Changed2
                               from person_Changed2 in _person_Changed2.DefaultIfEmpty()
                               join person2_Changed2 in context.PartnerPersonOrgPosition on x.Review_PartnerPersonId2_Changed2 equals person2_Changed2.Id into _person2_Changed2
                               from person2_Changed2 in _person2_Changed2.DefaultIfEmpty()
                               where (x.ObrazProgramId == opid) && (x.IsActive == true) &&
                               (x.OrderNumberReview != null) && (x.OrderNumberReview != "") //&& (x.OrderDopReview == false)
                               //&&
                               //((rbtnFilledStudentReview.Checked) ? x.StatusName == "студ" : true) &&
                               //((rbtnFilledStudentReview.Checked) ? x.VKRName_Final != "" : true) &&
                               //((rbtnFilledStudentReview.Checked) ? x.VKRName_Final != null : true) &&
                               //((rbtnFilledStudentReview.Checked) ? x.NPR_FIO_Final != "" : true) &&
                               //((rbtnFilledStudentReview.Checked) ? x.NPR_FIO_Final != null : true) &&
                               //((rbtnFilledStudentReview.Checked) ? ((x.OrganizationId != null) || (x.OrganizationId2 != null) || (x.OrganizationId3 != null)) : true) &&
                               //((rbtnFilledStudentReview.Checked) ? ((x.Review_NPR_Persnum != "") && (x.Review_NPR_Persnum != null)) || ((x.Review_NPR_Persnum2 != "") && (x.Review_NPR_Persnum2 != null)) || (x.Review_PartnerPersonId.HasValue) || (x.Review_PartnerPersonId2.HasValue) : true)
                               &&
                               (x.OrderNumberReview != null) && (x.OrderNumberReview != "") 
                               && ((newedition.HasValue) ? true :
                               (
                               ((x.NPR_FIO_Changed2 != null) && (x.NPR_FIO_Changed2 != "")) || ((x.VKRName_Changed2 != null) && (x.VKRName_Changed2 != "")) || x.OrganizationId_Changed2.HasValue ||
                               ((x.Review_NPR_FIO_Changed2 != null) && (x.Review_NPR_FIO_Changed2 != "")) || ((x.Review_NPR_FIO2_Changed2 != null) && (x.Review_NPR_FIO2_Changed2 != "")) ||
                               x.Review_PartnerPersonId_Changed2.HasValue || x.Review_PartnerPersonId2_Changed2.HasValue
                               ))

                               orderby x.FIO
                               select new
                               {
                                   //Номер = "",
                                   Номер = x.StudentNumberInOrderReview,
                                   ФИО = x.FIO,
                                   //Тема_ВКР_назначенная = ((x.VKRName_Changed != "") && (x.VKRName_Changed != null)) ?
                                   //((NameEngReviewDoc) ? (((!String.IsNullOrEmpty(x.VKRName_Changed)) ? x.VKRName_Changed : "") + ((!String.IsNullOrEmpty(x.VKRNameEng_Changed)) ? "\r\n" + x.VKRNameEng_Changed : "")) : x.VKRName_Changed) :
                                   //((NameEngReviewDoc) ? (((!String.IsNullOrEmpty(x.VKRName_Final)) ? x.VKRName_Final : "") + ((!String.IsNullOrEmpty(x.VKRNameEng_Final)) ? "\r\n" + x.VKRNameEng_Final : "")) : x.VKRName_Final),
                                   ////Тема_ВКР_назначенная = (NameEngDoc) ? (x.VKRName_Final + "\r\n" + x.VKRNameEng_Final) : x.VKRName_Final,
                                   ////Тема_ВКР_англ_назначенная = x.VKRNameEng_Final,

                                   Тема_ВКР_измененная = ((x.VKRName_Changed2 != "") && (x.VKRName_Changed2 != null)) ?
                                       ((NameEngReviewDoc) ? (((!String.IsNullOrEmpty(x.VKRName_Changed2)) ? x.VKRName_Changed2 : "") + ((!String.IsNullOrEmpty(x.VKRNameEng_Changed2)) ? "\r\n" + x.VKRNameEng_Changed2 : "")) : x.VKRName_Changed2) :
                                   ((x.VKRName_Changed != "") && (x.VKRName_Changed != null)) ?
                                   ((NameEngReviewDoc) ? (((!String.IsNullOrEmpty(x.VKRName_Changed)) ? x.VKRName_Changed : "") + ((!String.IsNullOrEmpty(x.VKRNameEng_Changed)) ? "\r\n" + x.VKRNameEng_Changed : "")) : x.VKRName_Changed) :
                                   ((NameEngReviewDoc) ? (((!String.IsNullOrEmpty(x.VKRName_Final)) ? x.VKRName_Final : "") + ((!String.IsNullOrEmpty(x.VKRNameEng_Final)) ? "\r\n" + x.VKRNameEng_Final : "")) : x.VKRName_Final),
                                   
                                   //НПР_ФИО_назначенный = ((x.NPR_Changed_Persnum != "") && (x.NPR_Changed_Persnum != null)) ?
                                   //     (x.NPR_FIO_Changed + ((!String.IsNullOrEmpty(x.NPR_Degree_Changed)) ? ", " + x.NPR_Degree_Changed : "") +
                                   //     ((!String.IsNullOrEmpty(x.NPR_Rank_Changed)) ? ", " + x.NPR_Rank_Changed : "") + ((!String.IsNullOrEmpty(x.NPR_Position_Changed)) ? ", " + x.NPR_Position_Changed : "") +
                                   //     ((!String.IsNullOrEmpty(x.NPR_Chair_Changed)) ? ", " + x.NPR_Chair_Changed : "")) :
                                   //     (x.NPR_FIO_Final + ((!String.IsNullOrEmpty(x.NPR_Degree_Final)) ? ", " + x.NPR_Degree_Final : "") +
                                   //     ((!String.IsNullOrEmpty(x.NPR_Rank_Final)) ? ", " + x.NPR_Rank_Final : "") + ((!String.IsNullOrEmpty(x.NPR_Position_Final)) ? ", " + x.NPR_Position_Final : "") +
                                   //     ((!String.IsNullOrEmpty(x.NPR_Chair_Final)) ? ", " + x.NPR_Chair_Final : "")),

                                   НПР_ФИО_измененный = ((x.NPR_Changed_Persnum2 != "") && (x.NPR_Changed_Persnum2 != null)) ?
                                        (x.NPR_FIO_Changed2 + ((!String.IsNullOrEmpty(x.NPR_Degree_Changed2)) ? ", " + x.NPR_Degree_Changed2 : "") +
                                        ((!String.IsNullOrEmpty(x.NPR_Rank_Changed2)) ? ", " + x.NPR_Rank_Changed2 : "") + ((!String.IsNullOrEmpty(x.NPR_Position_Changed2)) ? ", " + x.NPR_Position_Changed2 : "") +
                                        ((!String.IsNullOrEmpty(x.NPR_Chair_Changed2)) ? ", " + x.NPR_Chair_Changed2 : "")) :
                                    ((x.NPR_Changed_Persnum != "") && (x.NPR_Changed_Persnum != null)) ?
                                    (x.NPR_FIO_Changed + ((!String.IsNullOrEmpty(x.NPR_Degree_Changed)) ? ", " + x.NPR_Degree_Changed : "") +
                                    ((!String.IsNullOrEmpty(x.NPR_Rank_Changed)) ? ", " + x.NPR_Rank_Changed : "") + ((!String.IsNullOrEmpty(x.NPR_Position_Changed)) ? ", " + x.NPR_Position_Changed : "") +
                                    ((!String.IsNullOrEmpty(x.NPR_Chair_Changed)) ? ", " + x.NPR_Chair_Changed : "")) :
                                    (x.NPR_FIO_Final + ((!String.IsNullOrEmpty(x.NPR_Degree_Final)) ? ", " + x.NPR_Degree_Final : "") +
                                    ((!String.IsNullOrEmpty(x.NPR_Rank_Final)) ? ", " + x.NPR_Rank_Final : "") + ((!String.IsNullOrEmpty(x.NPR_Position_Final)) ? ", " + x.NPR_Position_Final : "") +
                                    ((!String.IsNullOrEmpty(x.NPR_Chair_Final)) ? ", " + x.NPR_Chair_Final : "")),

                                   //НПР_Рецензент = ((!String.IsNullOrEmpty(x.Review_NPR_Persnum)) ? x.Review_NPR_FIO + ((!String.IsNullOrEmpty(x.Review_NPR_Degree)) ? ", " + x.Review_NPR_Degree : "") +
                                   //     ((!String.IsNullOrEmpty(x.Review_NPR_Rank)) ? ", " + x.Review_NPR_Rank : "") + ((!String.IsNullOrEmpty(x.Review_NPR_Position)) ? ", " + x.Review_NPR_Position : "") +
                                   //     ((!String.IsNullOrEmpty(x.Review_NPR_Chair)) ? ", " + x.Review_NPR_Chair : "") : "") +
                                   //     ((!String.IsNullOrEmpty(x.Review_NPR_Persnum2)) ? ((!String.IsNullOrEmpty(x.Review_NPR_Persnum)) ? ", " : "") + x.Review_NPR_FIO2 + ((!String.IsNullOrEmpty(x.Review_NPR_Degree2)) ? ", " + x.Review_NPR_Degree2 : "") +
                                   //     ((!String.IsNullOrEmpty(x.Review_NPR_Rank2)) ? ", " + x.Review_NPR_Rank2 : "") + ((!String.IsNullOrEmpty(x.Review_NPR_Position2)) ? ", " + x.Review_NPR_Position2 : "") +
                                   //     ((!String.IsNullOrEmpty(x.Review_NPR_Chair2)) ? ", " + x.Review_NPR_Chair2 : "") : "") +
                                   //    //Рецензенты из "Партнера"
                                   //     ((x.Review_PartnerPersonId.HasValue) ? person.Name + ((!String.IsNullOrEmpty(person.DegreeName)) ? ", " + person.DegreeName : "") +
                                   //     ((!String.IsNullOrEmpty(person.RankName)) ? ", " + person.RankName : "") +
                                   //     ((!String.IsNullOrEmpty(person.OrgPosition)) ? ", " + person.OrgPosition : "") : "") +
                                   //     ((x.Review_PartnerPersonId2.HasValue) ? ((x.Review_PartnerPersonId.HasValue) ? ", " : "") + person2.Name + ((!String.IsNullOrEmpty(person2.DegreeName)) ? ", " + person2.DegreeName : "") +
                                   //     ((!String.IsNullOrEmpty(person2.RankName)) ? ", " + person2.RankName : "") +
                                   //     ((!String.IsNullOrEmpty(person2.OrgPosition)) ? ", " + person2.OrgPosition : "") : ""),

                                   НПР_Рецензент_измененный = ((!String.IsNullOrEmpty(x.Review_NPR_Persnum_Changed2)) ? x.Review_NPR_FIO_Changed2 + ((!String.IsNullOrEmpty(x.Review_NPR_Degree_Changed2)) ? ", " + x.Review_NPR_Degree_Changed2 : "") +
                                        ((!String.IsNullOrEmpty(x.Review_NPR_Rank_Changed2)) ? ", " + x.Review_NPR_Rank_Changed2 : "") + ((!String.IsNullOrEmpty(x.Review_NPR_Position_Changed2)) ? ", " + x.Review_NPR_Position_Changed2 : "") +
                                        ((!String.IsNullOrEmpty(x.Review_NPR_Chair_Changed2)) ? ", " + x.Review_NPR_Chair_Changed2 : "") :
                                      ((x.Review_NotUsePrevious_Changed2 == true) ? "" : 
                                    ((!String.IsNullOrEmpty(x.Review_NPR_Persnum)) ? x.Review_NPR_FIO + ((!String.IsNullOrEmpty(x.Review_NPR_Degree)) ? ", " + x.Review_NPR_Degree : "") +
                                    ((!String.IsNullOrEmpty(x.Review_NPR_Rank)) ? ", " + x.Review_NPR_Rank : "") + ((!String.IsNullOrEmpty(x.Review_NPR_Position)) ? ", " + x.Review_NPR_Position : "") +
                                    ((!String.IsNullOrEmpty(x.Review_NPR_Chair)) ? ", " + x.Review_NPR_Chair : "") : ""))) +
                                        ((!String.IsNullOrEmpty(x.Review_NPR_Persnum2_Changed2)) ? ((!String.IsNullOrEmpty(x.Review_NPR_Persnum_Changed2)) ? ", " : "") + x.Review_NPR_FIO2_Changed2 + ((!String.IsNullOrEmpty(x.Review_NPR_Degree2_Changed2)) ? ", " + x.Review_NPR_Degree2_Changed2 : "") +
                                        ((!String.IsNullOrEmpty(x.Review_NPR_Rank2_Changed2)) ? ", " + x.Review_NPR_Rank2_Changed2 : "") + ((!String.IsNullOrEmpty(x.Review_NPR_Position2_Changed2)) ? ", " + x.Review_NPR_Position2_Changed2 : "") +
                                        ((!String.IsNullOrEmpty(x.Review_NPR_Chair2_Changed2)) ? ", " + x.Review_NPR_Chair2_Changed2 : "") : 
                                      ((x.Review_NotUsePrevious_Changed2 == true) ? "" : 
                                    ((!String.IsNullOrEmpty(x.Review_NPR_Persnum2)) ? ((!String.IsNullOrEmpty(x.Review_NPR_Persnum)) ? ", " : "") + x.Review_NPR_FIO2 + ((!String.IsNullOrEmpty(x.Review_NPR_Degree2)) ? ", " + x.Review_NPR_Degree2 : "") +
                                    ((!String.IsNullOrEmpty(x.Review_NPR_Rank2)) ? ", " + x.Review_NPR_Rank2 : "") + ((!String.IsNullOrEmpty(x.Review_NPR_Position2)) ? ", " + x.Review_NPR_Position2 : "") +
                                    ((!String.IsNullOrEmpty(x.Review_NPR_Chair2)) ? ", " + x.Review_NPR_Chair2 : "") : ""))) +
                                       //Рецензенты из "Партнера"
                                        ((x.Review_PartnerPersonId_Changed2.HasValue) ? person_Changed2.Name + ((!String.IsNullOrEmpty(person_Changed2.DegreeName)) ? ", " + person_Changed2.DegreeName : "") +
                                        ((!String.IsNullOrEmpty(person_Changed2.RankName)) ? ", " + person_Changed2.RankName : "") +
                                        ((!String.IsNullOrEmpty(person_Changed2.OrgPosition)) ? ", " + person_Changed2.OrgPosition : "") : 
                                      ((x.Review_NotUsePrevious_Changed2 == true) ? "" : 
                                    ((x.Review_PartnerPersonId.HasValue) ? person.Name + ((!String.IsNullOrEmpty(person.DegreeName)) ? ", " + person.DegreeName : "") +
                                    ((!String.IsNullOrEmpty(person.RankName)) ? ", " + person.RankName : "") +
                                    ((!String.IsNullOrEmpty(person.OrgPosition)) ? ", " + person.OrgPosition : "") : ""))) +
                                        ((x.Review_PartnerPersonId2_Changed2.HasValue) ? ((x.Review_PartnerPersonId_Changed2.HasValue) ? ", " : "") + person2_Changed2.Name + ((!String.IsNullOrEmpty(person2_Changed2.DegreeName)) ? ", " + person2_Changed2.DegreeName : "") +
                                        ((!String.IsNullOrEmpty(person2_Changed2.RankName)) ? ", " + person2_Changed2.RankName : "") +
                                        ((!String.IsNullOrEmpty(person2_Changed2.OrgPosition)) ? ", " + person2_Changed2.OrgPosition : "") :
                                      ((x.Review_NotUsePrevious_Changed2 == true) ? "" : 
                                    ((x.Review_PartnerPersonId2.HasValue) ? ((x.Review_PartnerPersonId.HasValue) ? ", " : "") + person2.Name + ((!String.IsNullOrEmpty(person2.DegreeName)) ? ", " + person2.DegreeName : "") +
                                    ((!String.IsNullOrEmpty(person2.RankName)) ? ", " + person2.RankName : "") +
                                    ((!String.IsNullOrEmpty(person2.OrgPosition)) ? ", " + person2.OrgPosition : "") : ""))),

                                   //НПР_Организация = ((x.OrganizationId.HasValue) ? (org.Name + ((!String.IsNullOrEmpty(x.OrganizationDocument)) ? "\r\n" + x.OrganizationDocument : "")) : "") +
                                   // ((x.OrganizationId2.HasValue) ? (((x.OrganizationId.HasValue) ? ", " : "") + org2.Name + ((!String.IsNullOrEmpty(x.OrganizationDocument2)) ? "\r\n" + x.OrganizationDocument2 : "")) : "") +
                                   // ((x.OrganizationId3.HasValue) ? ((((x.OrganizationId.HasValue) || (x.OrganizationId2.HasValue)) ? ", " : "") + org3.Name + ((!String.IsNullOrEmpty(x.OrganizationDocument3)) ? "\r\n" + x.OrganizationDocument3 : "")) : ""),

                                   НПР_Организация_изменение = 
                                   (x.OrganizationNotAgreed_Changed2 == true) ? "" : 
                                        (
                                   (
                                   (x.OrganizationId_Changed2.HasValue) ? 
                                    (
                                        (x.OrganizationDop_Changed2 == true) ? (org_Changed2.Name + ((!String.IsNullOrEmpty(x.OrganizationDocument_Changed2)) ? "\r\n" + x.OrganizationDocument_Changed2 : "") +
                                        "\r\n" + ((x.OrganizationId.HasValue) ? (org.Name + ((!String.IsNullOrEmpty(x.OrganizationDocument)) ? "\r\n" + x.OrganizationDocument : "")) : "") ) :
                                        (org_Changed2.Name + ((!String.IsNullOrEmpty(x.OrganizationDocument_Changed2)) ? "\r\n" + x.OrganizationDocument_Changed2 : ""))
                                    ) :
                                   ((x.OrganizationId.HasValue) ? (org.Name + ((!String.IsNullOrEmpty(x.OrganizationDocument)) ? "\r\n" + x.OrganizationDocument : "")) : "")
                                   ) +
                                ((x.OrganizationId2.HasValue) ? (((x.OrganizationId.HasValue) ? ", " : "") + org2.Name + ((!String.IsNullOrEmpty(x.OrganizationDocument2)) ? "\r\n" + x.OrganizationDocument2 : "")) : "") +
                                ((x.OrganizationId3.HasValue) ? ((((x.OrganizationId.HasValue) || (x.OrganizationId2.HasValue)) ? ", " : "") + org3.Name + ((!String.IsNullOrEmpty(x.OrganizationDocument3)) ? "\r\n" + x.OrganizationDocument3 : "")) : "")
                                        ),
                                   //x.Id
                               }).ToList();

                    

                    dgvReviewDoc.DataSource = lst;

                    TableDoc td = wd.Tables[1];
                    int curRow = 0;
                    //foreach (DataGridViewColumn c in dgvDoc.Columns)
                    //{
                    //    td[c.Index, curRow] = c.HeaderText.ToString();
                    //}
                    curRow = 1;
                    int i = 0;
                    foreach (DataGridViewRow rw in dgvReviewDoc.Rows)
                    {
                        td.AddRow(1);
                        curRow++;
                        i++;
                        foreach (DataGridViewCell cell in rw.Cells)
                        {
                            if (cell.Value != null)
                                //if (cell.ColumnIndex == 0)
                                //{
                                //    td[cell.ColumnIndex, curRow] = i.ToString() + ".";
                                //}
                                //else
                                //{
                                //    td[cell.ColumnIndex, curRow] = cell.Value.ToString();
                                //}
                                td[cell.ColumnIndex, curRow] = cell.Value.ToString();
                        }
                    }
                    //td.DeleteLastRow();
                    //wd.AddParagraph(" ");
                    //wd.AddParagraph(" ");

                }


                //Сохранение файла
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch
                    { }
                }
                string fpath = filePath;
                try
                {
                    filePath = TempFilesFolder + nameshort + " " + OPNumber + type;
                    //filePath = TempFilesFolder + nameshort + " Изменения " + OPNumber + type;
                }
                catch (Exception)
                {
                    filePath = fpath;
                }

                wd.Save(filePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сформировать документ\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnOrderReviewNewEditionDoc_Click(object sender, EventArgs e)
        {
            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbObrazProgram.DroppedDown = true;
                return;
            }
            //проверка наличия приказа о рецензентах
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.ObrazProgramId == ObrazProgramId) &&
                               (x.OrderNumberReview != null) && (x.OrderNumberReview != "") //&& (x.OrderDopReview == true)
                               select new
                               {
                                   x.Id,
                               }).ToList();
                    if (lst.Count == 0)
                    {
                        MessageBox.Show("Не зафиксирован (отсутствует) приказ о рецензентах.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось проверить наличие приказа о рецензентах.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //проверка наличия данных об изменениях
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where (x.ObrazProgramId == ObrazProgramId) &&
                               (x.OrderNumberReview != null) && (x.OrderNumberReview != "") && //&& (x.OrderDopReview == true)
                               (
                               ((x.NPR_FIO_Changed2 != null) && (x.NPR_FIO_Changed2 != "")) || ((x.VKRName_Changed2 != null) && (x.VKRName_Changed2 != "")) || x.OrganizationId_Changed2.HasValue ||
                               ((x.Review_NPR_FIO_Changed2 != null) && (x.Review_NPR_FIO_Changed2 != "")) || ((x.Review_NPR_FIO2_Changed2 != null) && (x.Review_NPR_FIO2_Changed2 != "")) ||
                               x.Review_PartnerPersonId_Changed2.HasValue || x.Review_PartnerPersonId2_Changed2.HasValue
                               )
                               select new
                               {
                                   x.Id,
                               }).ToList();
                    if (lst.Count == 0)
                    {
                        if (MessageBox.Show("Нет данных для внесения изменений в приказ о рецензентах.\r\nПродолжить тем не менее?", "Запрос на подтверждение", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                            return;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось проверить наличие данных \r\nдля внесения изменений в приказ о рецензентах.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            OrderReviewNewEditionToDoc(ObrazProgramId);

            SupplementReviewChangesToDoc(ObrazProgramId, 1); //null - печать только изменений, 1 (любое число) - печать новой редакции (всего документа)
        }

        private void OrderReviewNewEditionToDoc(int? opid)
        {
            if (!opid.HasValue)
                return;

            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "";
            templatename = "Приказ ВКР НР Рецензенты - новая редакция";
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
            string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Приказы по ВКР и НР\";
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

            WordDoc wd = new WordDoc(string.Format(filePath), true);

            //заполнение шаблона
            try
            {
                string ObrazProgram = "";
                string ObrazProgramHeader = "";
                string LicenseProgram = "";
                //string UNKUNP = "";
                string Stlevel = "";
                string OPNumber = "";
                string OPName = "";
                string LPCode = "";
                string LPName = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgram
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               where x.Id == opid
                               select x).First();

                    Stlevel = lst.LicenseProgram.StudyLevel.Name;
                    OPNumber = lst.Number;
                    OPName = lst.Name;
                    LPCode = lst.LicenseProgram.Code;
                    LPName = lst.LicenseProgram.Name;
                }
                switch (Stlevel)
                {
                    case "бакалавриат":
                        ObrazProgram = "бакалавриата CB." + OPNumber + ".*";
                        ObrazProgramHeader = "бакалавриата (шифр CB." + OPNumber + ".*)";
                        LicenseProgram = "по направлению подготовки ";
                        break;
                    case "магистратура":
                        ObrazProgram = "магистратуры BM." + OPNumber + ".*";
                        ObrazProgramHeader = "магистратуры (шифр BM." + OPNumber + ".*)";
                        LicenseProgram = "по направлению подготовки ";
                        break;
                    case "специалитет":
                        ObrazProgram = "специалитета CM." + OPNumber + ".*";
                        ObrazProgramHeader = "специалитета (шифр CM." + OPNumber + ".*)";
                        LicenseProgram = "по специальности  ";
                        break;
                    default:
                        break;
                }
                ObrazProgram += " " + "«" + OPName + "»";
                LicenseProgram += LPCode + " " + "«" + LPName + "»";

                wd.SetFields("ObrazProgramHeader", ObrazProgramHeader);
                wd.SetFields("ObrazProgram", ObrazProgram);
                //wd.SetFields("LicenseProgram", LicenseProgram);
                //wd.SetFields("UNKUNP", "");

                //номер приказа
                string Order = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               where (x.ObrazProgramId == ObrazProgramId) &&
                               (x.OrderNumberReview != null) && (x.OrderNumberReview != "") && (x.OrderDopReview == false)
                               select x).First();
                    Order = "от " + ((lst.OrderDateReview.HasValue) ? lst.OrderDateReview.Value.ToString("dd.MM.yyyy") : "") + " № " + lst.OrderNumberReview;
                }

                wd.SetFields("OrderNumber", Order);
                wd.SetFields("OrderNumber2", Order);

                ////номера пуктов приказа, где содержатся изменения
                //string LineNumbers = "";
                //using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                //{
                //    var lst = (from x in context.VKR_ThemesStudentOrder
                //               where (x.ObrazProgramId == opid) && (x.IsActive == true) &&
                //               (x.OrderNumberReview != null) && (x.OrderNumberReview != "") &&
                //               (x.OrderNumberReview != null) && (x.OrderNumberReview != "") &&
                //               (
                //               ((x.NPR_FIO_Changed2 != null) && (x.NPR_FIO_Changed2 != "")) || ((x.VKRName_Changed2 != null) && (x.VKRName_Changed2 != "")) || x.OrganizationId_Changed2.HasValue ||
                //               ((x.Review_NPR_FIO_Changed2 != null) && (x.Review_NPR_FIO_Changed2 != "")) || ((x.Review_NPR_FIO2_Changed2 != null) && (x.Review_NPR_FIO2_Changed2 != "")) ||
                //               x.Review_PartnerPersonId_Changed2.HasValue || x.Review_PartnerPersonId2_Changed2.HasValue
                //               )
                //               orderby x.FIO
                //               select new
                //               {
                //                   Номер = x.StudentNumberInOrderReview,
                //               }).ToList();
                //    try
                //    {
                //        int i = 0;
                //        foreach (var item in lst)
                //        {
                //            if (item.Номер.HasValue)
                //            {
                //                LineNumbers += (i > 0) ? ", " + item.Номер.ToString() : item.Номер.ToString();
                //                i++;
                //            }
                //        }
                //    }
                //    catch (Exception)
                //    {
                //    }
                //}

                //wd.SetFields("LineNumbers", LineNumbers);

                //основания и рассылка
                int FacId;
                int StLevelId;
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgram
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               //join fac in context.Faculty on x.FacultyId equals fac.Id
                               where x.Id == opid
                               select x).First();

                    FacId = lst.FacultyId;
                    StLevelId = lst.LicenseProgram.StudyLevel.Id;
                }

                string basis = "";
                string chairman = "";
                string send = "";
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var quan = (from x in context.VKR_OrderSource
                                where (x.FacultyId == FacId) && (x.StudyLevelId == StLevelId)
                                select x).Count();
                    var lst = (from x in context.VKR_OrderSource
                               where (x.FacultyId == FacId) && ((quan > 0) ? x.StudyLevelId == StLevelId : x.StudyLevelId == null)
                               select x).First();
                    basis = lst.Basis;
                    chairman = lst.ChairmanForOrder;
                    send = ((!String.IsNullOrEmpty(lst.ChairmanForSend)) ? lst.ChairmanForSend : "") +
                            ((!String.IsNullOrEmpty(lst.UOPForSend)) ? "\r\n" + lst.UOPForSend : "") +
                            ((!String.IsNullOrEmpty(lst.UUForSend)) ? "\r\n" + lst.UUForSend : "");
                }
                //wd.SetFields("Chairman", chairman);
                //wd.SetFields("Basis", basis);
                wd.SetFields("Send", send);

                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch
                    { }
                }
                string fpath = filePath;
                try
                {
                    filePath = TempFilesFolder + nameshort + " " + OPNumber + type;
                }
                catch (Exception)
                {
                    filePath = fpath;
                }

                wd.Save(filePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сформировать документ\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnReNumOrderReview_Click(object sender, EventArgs e)
        {
            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbObrazProgram.DroppedDown = true;
                return;
            }

            if (MessageBox.Show("Данная функция производит перенумерацию студентов.\r\n" + 
                "Отбираются только те, для которых установлен № приказа.\r\n" +
                "Это позволяет привести порядковые номера студентов в приказе и в БД в соответствие.\r\n" + 
                "Продолжить выполнение?", "Запрос на подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                return;

            try
            {
                if (String.IsNullOrEmpty(RenumOrderReviewNumber))
                {
                    MessageBox.Show("Не выбран № приказа!","Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbRenumOrderReviewNumber.DroppedDown = true;
                    return;
                }
            }
            catch (Exception)
            {
            }
            

            int? opid = ObrazProgramId;
            if (!opid.HasValue)
                return;
            //if (!CheckFieldsReview())
            //    return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKR_ThemesStudentOrder
                               where (x.ObrazProgramId == opid) //&& (x.IsActive == true) &&
                               //&& (x.OrderNumberReview != null) && (x.OrderNumberReview != "")
                               && (x.OrderNumberReview == RenumOrderReviewNumber)
                               orderby x.FIO
                               select new
                               {
                                   x.Id,
                               }).ToList();

                    dgvReviewDoc.DataSource = lst;

                    int id;
                    int i = 0;
                    foreach (DataGridViewRow rw in dgvReviewDoc.Rows)
                    {
                        i++;
                        id = int.Parse(rw.Cells["Id"].Value.ToString());
                        var vkrst = context.VKR_ThemesStudentOrder.Where(x => x.Id == id).First();

                        vkrst.StudentNumberInOrderReview = i;

                        context.SaveChanges();
                    }
                    FillStudent();
                    //OrderNumberReview = "";
                    //OrderDateReview = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось произвести перенумерацию...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRenumOrderReviewRefresh_Click(object sender, EventArgs e)
        {
            //Обновить список номеров приказов
            FillRenumOrderReviewNumbers();
        }

        private void cbObrazProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //Обновить список номеров приказов
            FillRenumOrderReviewNumbers();
            }
            catch (Exception)
            {
            }
        }

    }
}
