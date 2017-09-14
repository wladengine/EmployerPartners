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
    public partial class GAK_Lists : Form
    {
        public int? GAKId
        {
            get { return ComboServ.GetComboIdInt(cbGAK); }
            set { ComboServ.SetComboId(cbGAK, value); }
        }
        public int? StudyLevelId
        {
            get { return ComboServ.GetComboIdInt(cbLevel); }
            set { ComboServ.SetComboId(cbLevel, value); }
        }
        public int? LPId
        {
            get { return ComboServ.GetComboIdInt(cbLP); }
            set { ComboServ.SetComboId(cbLP, value); }
        }
        public int? LicenseId
        {
            get { return ComboServ.GetComboIdInt(cbLicense); }
            set { ComboServ.SetComboId(cbLicense, value); }
        }
        public int? LicenseOrderId
        {
            get { return ComboServ.GetComboIdInt(cbLicenseOrder); }
            set { ComboServ.SetComboId(cbLicenseOrder, value); }
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
            get { return chbGAK.Checked; }
            set { chbGAK.Checked = value; }
        }
        public bool isGAKChairMan
        {
            get { return chbGAKChairman.Checked; }
            set { chbGAKChairman.Checked = value; }
        }
        public bool ChairmanShowColumns
        {
            get { return chbChairmanShowColumns.Checked; }
            set { chbChairmanShowColumns.Checked = value; }
        }
        bool panel1dataload = false;

        public GAK_Lists()
        {
            InitializeComponent();
            FillGAK();
            FillLicense();
            FillLicenseOrder();
            FillStudyLevel();
            FillLP();
            FillChairmanList();
            panel1dataload = true;
            FillCombo();
            FillChairmanNewList();
            this.MdiParent = Util.mainform;
            SetAccessRight();
        }

        private void SetAccessRight()
        {
            if (/*Util.IsGAKWrite()*/ Util.IsSuperUser() || Util.IsDBOwner() || Util.IsAdministrator())
            {

                dgvChairman.Columns[1].Visible = true;
                //dgvChairman.Columns[2].Visible = true;
                dgvChairmanNew.Columns[1].Visible = true;
                //dgvChairmanNew.Columns[2].Visible = true;
                btnSource.Enabled = true;
            }
        }
        private void FillGAK()
        {
            ComboServ.FillCombo(cbGAK, HelpClass.GetComboListByQuery(@" SELECT DISTINCT CONVERT(varchar(100), Id) AS Id, GAKYear AS Name 
                FROM GAK ORDER BY Name DESC "), false, false);
        }
        private void FillStudyLevel()
        {
            //ComboServ.FillCombo(cbLevel, HelpClass.GetComboListByTable("dbo.StudyLevel"), false, true);
            string license = "1";
            if (LicenseId.HasValue)
            {
                license = LicenseId.ToString();
            }
            ComboServ.FillCombo(cbLevel, HelpClass.GetComboListByQuery(@" SELECT DISTINCT CONVERT(varchar(100), Id) AS Id, Name
                FROM dbo.StudyLevel WHERE Id in (select StudyLevelId from LicenseProgram where LicenseId = " + license +")"), false, true);
        }
        private void FillLicense()
        {
            ComboServ.FillCombo(cbLicense, HelpClass.GetComboListByQuery(@" SELECT DISTINCT CONVERT(varchar(100), Id) AS Id, 
                ('Лицензия  Серия: ' + Isnull([Series], '') + '  Номер: ' + Isnull([Number], '') + '  Рег.номер: ' +
                    IsNull(RegNum, '') + '  Дата: ' + case [DateOut] when null then '' when '' then '' else convert(char(12), [DateOut], 3) end)  As Name 
                FROM dbo.License ORDER BY Id DESC "), false, true); //cast([DateOut] as varchar) //CONVERT(char(12), GETDATE(), 3)
        }
        private void FillLicenseOrder()
        {
            ComboServ.FillCombo(cbLicenseOrder, HelpClass.GetComboListByQuery(@" SELECT CONVERT(varchar(100), Id) AS Id, 
                ('Серия: ' + Isnull([Series], '') + '  Номер: ' + Isnull([Number], '') + 
                '  Дата: ' + case [DateOut] when null then '' when '' then '' else convert(char(12), [DateOut], 3) end)  As Name 
                FROM dbo.License ORDER BY DateOut DESC "), false, false); //cast([DateOut] as varchar) //CONVERT(char(12), GETDATE(), 3)
        }
        private void FillCombo()
        {
            ComboServ.FillCombo(cbRubric, HelpClass.GetComboListByTable("dbo.Rubric"), false, true);
            ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("dbo.Faculty"), false, true);
        }

        private void FillLP()
        {
            //ComboServ.FillCombo(cbLevel, HelpClass.GetComboListByTable("dbo.LicenseProgram"), false, true);
            string stLevel= "";
            string orderby = "";
            string license = "1";
            if (LicenseId.HasValue)
            {
                license = LicenseId.ToString();
            }
            if (rbtnCode.Checked)
            {
                orderby = " ORDER BY SL.[Id], LP.[Code]";
            }
            if (rbtnAlphabet.Checked)
            {
                orderby = " ORDER BY SL.[Id], LP.[Name]";
            }
            if (StudyLevelId.HasValue)
	        {
		         stLevel = "AND LP.StudyLevelId = " +  StudyLevelId.ToString();
	        }
            ComboServ.FillCombo(cbLP, HelpClass.GetComboListByQuery(@" SELECT CONVERT(varchar(100), LP.[Id]) AS Id, (SL.[Name] + '  ' + LP.[Code] + '  ' + LP.[Name]) AS Name 
                FROM dbo.LicenseProgram LP INNER JOIN dbo.StudyLevel SL ON LP.StudyLevelId = SL.Id WHERE (LP.LicenseId = " + license + ") " + stLevel + orderby), false, true);
        }

        private void FillChairmanList()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_LP_Person
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join level in context.StudyLevel on lp.StudyLevelId equals level.Id
                               join person in context.PartnerPerson on x.PartnerPersonId equals person.Id

                               join degree in context.Degree on person.DegreeId equals degree.Id into _degree
                               from degree in _degree.DefaultIfEmpty()
                               join degree2 in context.Degree on person.DegreeId equals degree2.Id into _degree2
                               from degree2 in _degree2.DefaultIfEmpty()

                               //join orgpos in context.PersonOrgPosition on x.PartnerPersonId equals orgpos.PartnerPersonId into _orgpos
                               //from orgpos in _orgpos.DefaultIfEmpty()

                               join orgname in context.ECD_OrganizationPersonOrgName on x.PartnerPersonId equals orgname.PartnerPersonId into _orgname
                               from orgname in _orgname.DefaultIfEmpty()

                               where (x.GAKId == GAKId) && ((bool)x.Chairman == true) && 
                                    (StudyLevelId.HasValue ? lp.StudyLevelId == StudyLevelId  : true) &&
                                    (LicenseId.HasValue ? (lp.LicenseId == LicenseId) : true) &&
                                    (LPId.HasValue ? x.LicenseProgramId == LPId : true)
                               orderby level.Id, (rbtnAlphabet.Checked ? lp.Name : lp.Code), person.Name
                               select new
                               {
                                   Уровень = level.Name,
                                   Код_направления = lp.Code,
                                   Направление = lp.Name,
                                   ФИО = person.Name,
                                   Председатель = x.Chairman,
                                   Недействителен = ((bool)x.IsNotActive) ? "недействителен" : "",
                                   Номер_приказа = x.OrderNumber,
                                   Дата_приказа = x.OrderDate,
                                   //Дополнение_к_приказу = x.OrderDop,
                                   Номер_в_приказе = x.PersonNumberInOrder,
                                   //Организация = orgpos.OrgPosition,
                                   Организация = orgname.OrgName,
                                   Ученая_степень = person.Degree.Name,
                                   Ученое_звание = person.Rank.Name,
                                   Почетное_звание = person.RankHonorary.Name,
                                   Государственное_или_военное_звание = person.RankState.Name,
                                   Регалии_доп_данные = person.Title,
                                   Основная_сфера_деятельности = person.ActivityArea.Name,
                                   Страна = person.Country.Name,
                                   Email = person.Email,
                                   Телефон = person.Phone,
                                   Примечание = x.Comment,
                                   x.Id,
                                   x.PartnerPersonId
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource1.DataSource = dt;
                    dgvChairman.DataSource = bindingSource1;

                    List<string> Cols = new List<string>() { "Id", "PartnerPersonId" }; 

                    foreach (string s in Cols)
                        if (dgvChairman.Columns.Contains(s))
                            dgvChairman.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvChairman.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnDelChairman")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnCardChairman")
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
                        dgvChairman.Columns["ColumnDiv1"].Width = 6;
                        dgvChairman.Columns["ColumnDelChairman"].Width = 70;
                        dgvChairman.Columns["ColumnCardChairman"].Width = 70;
                        dgvChairman.Columns["Уровень"].Frozen = true;
                        dgvChairman.Columns["Код_направления"].Frozen = true;
                        dgvChairman.Columns["Направление"].Frozen = true;
                        dgvChairman.Columns["ФИО"].Frozen = true;
                        dgvChairman.Columns["Уровень"].Width = 80;
                        dgvChairman.Columns["Код_направления"].Width = 80;
                        //dgvChairman.Columns["Направление"].Width = 200;
                        dgvChairman.Columns["ФИО"].Width = 200;
                        dgvChairman.Columns["Председатель"].Width = 80;
                        dgvChairman.Columns["Номер_в_приказе"].Width = 80;
                        dgvChairman.Columns["Организация"].Width = 400;
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

        private void FillChairmanNewList()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from org in context.PartnerPerson
                               join r in context.PartnerPersonRubric on org.Id equals r.PartnerPersonId into _r
                               from or in _r.DefaultIfEmpty()
                               join f in context.PartnerPersonFaculty on org.Id equals f.PartnerPersonId into _f
                               from of in _f.DefaultIfEmpty()
                               where
                               (RubricId.HasValue ? or.RubricId == RubricId : true) &&
                               (FacultyId.HasValue ? of.FacultyId == FacultyId : true) &&
                               ((isGAK == true) ? org.IsGAK == true : true) &&
                               ((isGAKChairMan == true) ? org.IsGAKChairman == true : true)
                               orderby org.Name
                               select new
                               {
                                   ФИО = org.Name,
                                   ФИО_англ = org.NameEng,
                                   Регистрационный_номер_ИС_Партнеры = org.Account,
                                   Входит_в_составы_ГЭК = org.IsGAK,
                                   Председатель_ГЭК = org.IsGAKChairman,
                                   Ученая_степень = org.Degree.Name,
                                   Ученое_звание = org.Rank.Name,
                                   Почетное_звание = org.RankHonorary.Name,
                                   Государственное_или_военное_звание = org.RankState.Name,
                                   Регалии_доп_данные = org.Title,
                                   Основная_сфера_деятельности = org.ActivityArea.Name,
                                   //Выпускник_СПбГУ = org.IsSPbGUGraduate.HasValue && org.IsSPbGUGraduate.Value ? "да" : "нет",
                                   //Год_выпуска = org.SPbGUGraduateYear,
                                   //Ассоциация_выпускников = org.AlumniAssociation.HasValue && org.AlumniAssociation.Value ? "да" : "нет",
                                   Страна = org.Country.Name,
                                   Email = org.Email,
                                   Телефон = org.Phone,
                                   //Мобильный_телефон = org.Mobiles,
                                   //Web_сайт = org.WebSite,
                                   Комментарий = org.Comment,
                                   org.Id,
                               }).Distinct().OrderBy(x => x.ФИО).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource2.DataSource = dt;
                    dgvChairmanNew.DataSource = bindingSource2;

                    List<string> Cols = new List<string>() { "Id" };

                    foreach (string s in Cols)
                        if (dgvChairmanNew.Columns.Contains(s))
                            dgvChairmanNew.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvChairmanNew.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnAddChairman")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnCardChairmanNew")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDiv2")
                        {
                            col.HeaderText = "";
                        }
                    }
                    try
                    {
                        dgvChairmanNew.Columns["ColumnDiv2"].Width = 6;
                        dgvChairmanNew.Columns["ColumnAddChairman"].Width = 70;
                        dgvChairmanNew.Columns["ColumnCardChairmanNew"].Width = 70;
                        dgvChairmanNew.Columns["ФИО"].Frozen = true;
                        dgvChairmanNew.Columns["ФИО"].Width = 200;
                        //dgvChairmanNew.Columns["Ученая_степень"].Width = 150;
                        //dgvChairmanNew.Columns["Ученое_звание"].Width = 150;
                        //dgvChairmanNew.Columns["Почетное_звание"].Width = 150;
                        //dgvChairmanNew.Columns["Государственное_или_военное_звание"].Width = 150;
                        dgvChairmanNew.Columns["Регалии_доп_данные"].Width = 300;
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

        private void cbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLP();
            if (panel1dataload)
                FillChairmanList();
        }
        private void rbtnCode_CheckedChanged(object sender, EventArgs e)
        {
            FillLP();
            if (panel1dataload)
                FillChairmanList();
        }
        private void rbtnAlphabet_CheckedChanged(object sender, EventArgs e)
        {
            FillLP();
            if (panel1dataload)
                FillChairmanList();
        }
        private void cbGAK_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStudyLevel();
            FillLP();
            if (panel1dataload)
                FillChairmanList();
        }
        private void cbLicense_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStudyLevel();
            FillLP();
            if (panel1dataload)
                FillChairmanList();
        }
        private void cbLP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (panel1dataload)
                FillChairmanList();
        }

        private void GAK_Lists_Load(object sender, EventArgs e)
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

        private void cbRubric_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillChairmanNewList();
        }

        private void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillChairmanNewList();
        }

        private void chbGAK_CheckedChanged(object sender, EventArgs e)
        {
            FillChairmanNewList();
        }

        private void chbGAKChairman_CheckedChanged(object sender, EventArgs e)
        {
            FillChairmanNewList();
        }

        private void chbChairmanShowColumns_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ChairmanShowColumns)
                {
                    //dgvChairman.Columns["Уровень"].Frozen = false;
                    //dgvChairman.Columns["Код_направления"].Frozen = false;
                    //dgvChairman.Columns["Направление"].Frozen = false;
                    dgvChairman.Columns["Уровень"].Visible = false;
                    dgvChairman.Columns["Код_направления"].Visible = false;
                    dgvChairman.Columns["Направление"].Visible = false;
                }
                else
                {
                    //dgvChairman.Columns["Уровень"].Frozen = true;
                    //dgvChairman.Columns["Код_направления"].Frozen = true;
                    //dgvChairman.Columns["Направление"].Frozen = true;
                    dgvChairman.Columns["Уровень"].Visible = true;
                    dgvChairman.Columns["Код_направления"].Visible = true;
                    dgvChairman.Columns["Направление"].Visible = true;
                }
                
            }
            catch (Exception)
            {
            }
        }

        private void dgvChairman_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvChairman.CurrentCell != null)
                if (dgvChairman.CurrentRow.Index >= 0)
                {
                    if (dgvChairman.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            dgvChairman.CurrentCell = dgvChairman.CurrentRow.Cells["ФИО"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        int id;
                        try
                        {
                            id = int.Parse(dgvChairman.CurrentRow.Cells["Id"].Value.ToString());
                        }
                        catch (Exception)
                        {
                            return;
                        }
                        string chairman = "";
                        try
                        {
                            chairman = dgvChairman.CurrentRow.Cells["ФИО"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        //////
                        string ordernumber = "";
                        string isnotactive = "";
                        try
                        {
                            ordernumber = dgvChairman.CurrentRow.Cells["Номер_приказа"].Value.ToString();
                            isnotactive = dgvChairman.CurrentRow.Cells["Недействителен"].Value.ToString();
                            if (ordernumber != "")
                            {
                                if (isnotactive == "")
                                {
                                    if (MessageBox.Show("Невозможно удаление " + chairman + "\r\n" + "т.к. приказ зафиксирован (" + ordernumber + ")\r\n" + "Отметить данную позицию как недействительную?\r\n" +
                                             "Примечание: последующее нажатие на кнопку 'Удалить \r\nпозволит вернуть статус 'действителен'",
                                             "Запрос на подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        try
                                        {
                                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                                            {
                                                var gakperson = context.GAK_LP_Person.Where(x => x.Id == id).First();
                                                gakperson.IsNotActive = true;
                                                context.SaveChanges();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Не удалось выполнить действие.\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        }
                                        FillChairmanList();
                                        return;
                                    }
                                    else
                                        return;
                                }
                                if (isnotactive == "недействителен")
                                {
                                    if (MessageBox.Show("Вернуть статус " + chairman + "\r\n" + "на 'действительный'?", 
                                        "Запрос на подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                                    {
                                        return;
                                    }
                                    else
                                    {
                                        try
                                        {
                                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                                            {
                                                var gakperson = context.GAK_LP_Person.Where(x => x.Id == id).First();
                                                gakperson.IsNotActive = false;
                                                context.SaveChanges();
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Не удалось выполнить действие.\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                        }
                                        FillChairmanList();
                                        return;
                                    }
                                }
                            }

                        }
                        catch (Exception)
                        {
                        }
                        //////
                        if (MessageBox.Show("Удалить выбранное лицо из списка? \r\n" + chairman, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            try
                            {
                                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                                {
                                    context.GAK_LP_Person.RemoveRange(context.GAK_LP_Person.Where(x => x.Id == id));
                                    context.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Не удалось удалить запись...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            FillChairmanList();
                            return;
                        }
                        else
                            return;
                    }
                    if (dgvChairman.CurrentCell.ColumnIndex == 2)
                    {
                        try
                        {
                            dgvChairman.CurrentCell = dgvChairman.CurrentRow.Cells["ФИО"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        try
                        {
                            int PersonId = int.Parse(dgvChairman.CurrentRow.Cells["PartnerPersonId"].Value.ToString());
                            if (Utilities.PersonCardIsOpened(PersonId)) 
                                return;
                            new  CardPerson(PersonId, null).Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Не удалось открыть карточку\r\n" + "Причина: " + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
        }

        private void dgvChairmanNew_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvChairmanNew.CurrentCell != null)
                if (dgvChairmanNew.CurrentRow.Index >= 0)
                {
                    if (dgvChairmanNew.CurrentCell.ColumnIndex == 1)
                    {
                        if (!GAKId.HasValue)
                        {
                            MessageBox.Show("Не выбран год", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cbGAK.DroppedDown = true;
                            return;
                        }
                        if (!LPId.HasValue)
                        {
                            MessageBox.Show("Не выбрано направление подготовки", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cbLP.DroppedDown = true;
                            return;
                        }
                        try
                        {
                            dgvChairmanNew.CurrentCell = dgvChairmanNew.CurrentRow.Cells["ФИО"];
                        }
                        catch (Exception)
                        {
                        }
                        //проверка наличия физ. лица в списке председателей
                        try
                        {
                            int PersonId = int.Parse(dgvChairmanNew.CurrentRow.Cells["Id"].Value.ToString());
                            string FIO = (dgvChairmanNew.CurrentRow.Cells["ФИО"].Value != null) ? dgvChairmanNew.CurrentRow.Cells["ФИО"].Value.ToString() : "";
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                var lst = (from x in context.GAK_LP_Person
                                           where (x.PartnerPersonId == PersonId) && (x.LicenseProgramId == LPId) && (x.GAKId == GAKId)
                                           select new
                                           {
                                               x.Id,
                                           }).ToList().Count();
                                if (lst > 0)
                                {
                                    MessageBox.Show("Партнер\r\n" + FIO + "\r\n" + "уже находится в списке председателей\r\n" + "по выбранному направлению\r\n" +
                                        "для выбранного года " + cbGAK.Text, "Инфо",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        //собственно добавление
                        try
                        {
                            int PersonId = int.Parse(dgvChairmanNew.CurrentRow.Cells["Id"].Value.ToString());
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                GAK_LP_Person gak = new GAK_LP_Person();
                                gak.GAKId = (int)GAKId;
                                gak.PartnerPersonId = PersonId;
                                gak.LicenseProgramId = (int)LPId;
                                gak.Chairman = true;

                                context.GAK_LP_Person.Add(gak);
                                context.SaveChanges();

                                FillChairmanList();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                    if (dgvChairmanNew.CurrentCell.ColumnIndex == 2)
                    {
                        try
                        {
                            dgvChairmanNew.CurrentCell = dgvChairmanNew.CurrentRow.Cells["ФИО"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        try
                        {
                            int PersonId = int.Parse(dgvChairmanNew.CurrentRow.Cells["Id"].Value.ToString());
                            if (Utilities.PersonCardIsOpened(PersonId))
                                return;
                            new CardPerson(PersonId, null).Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Не удалось открыть карточку\r\n" + "Причина: " + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string search = tbSearch.Text.Trim().ToUpper();
                bool exit = false;
                for (int i = 0; i < dgvChairmanNew.RowCount; i++)
                {
                    if (exit)
                    { break; }
                    for (int j = 0; j < 5 /*dgv.Columns.Count*/; j++)
                    {
                        if ((j == 0) || (j == 1) || (j == 2))
                            continue;
                        if (dgvChairmanNew[j, i].ColumnIndex == 0)
                        {
                            int length = 1;
                            length = dgvChairmanNew[j, i].Value.ToString().Length;
                            length = (length <= 15) ? length : 15;
                            if (dgvChairmanNew[j, i].Value.ToString().Substring(0, length).ToUpper().Contains(search))
                            {
                                dgvChairmanNew.CurrentCell = dgvChairmanNew[j, i];
                                exit = true;
                                break;
                            }
                        }
                        else
                        {
                            if (dgvChairmanNew[j, i].Value.ToString().ToUpper().Contains(search))
                            {
                                dgvChairmanNew.CurrentCell = dgvChairmanNew[j, i];
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

        private void btnChairmanWord_Click(object sender, EventArgs e)
        {
            //проверка наличия данных
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_LP_Person
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join level in context.StudyLevel on lp.StudyLevelId equals level.Id
                               where (x.GAKId == GAKId) && ((bool)x.Chairman == true) &&
                                    ((lp.StudyLevelId == 16) || (lp.StudyLevelId == 17) || (lp.StudyLevelId == 18) || (lp.StudyLevelId == 15) || (lp.StudyLevelId == 20)) &&
                                    (LicenseOrderId.HasValue ? lp.LicenseId == LicenseOrderId : true)
                               //orderby level.Id, (rbtnAlphabetOrder.Checked ? lp.Name : lp.Code)
                               select new
                               {
                                   x.Id
                               }).Count();
                    if (lst == 0)
                    {
                        MessageBox.Show("Нет данных...", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbLicenseOrder.DroppedDown = true;
                        return;
                    }
                }
            }
            catch (Exception)
            {
            }

            OrderChairmanToDoc();
        }

        private void OrderChairmanToDoc()
        {
            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "";
            templatename = "Приказ_ГЭК_председатели";
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

            WordDoc wd = new WordDoc(string.Format(filePath), true);
            //заполнение шаблона
            try
            {
                //WordDoc wd = new WordDoc(string.Format(filePath), true);

                //string StudyLevel = "бакалавриат";
                //wd.SetFields("StudyLevelBac", StudyLevel);
                DateTime DateCreated = DateTime.Parse("30.12.2016");

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_LP_Person
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join level in context.StudyLevel on lp.StudyLevelId equals level.Id
                               join person in context.PartnerPerson on x.PartnerPersonId equals person.Id
                               
                               join degree in context.Degree on person.DegreeId equals degree.Id into _degree
                               from degree in _degree.DefaultIfEmpty()
                               join degree2 in context.Degree on person.Degree2Id equals degree2.Id into _degree2
                               from degree2 in _degree2.DefaultIfEmpty()

                               join rank in context.Rank on person.RankId equals rank.Id into _rank
                               from rank in _rank.DefaultIfEmpty()
                               join rank2 in context.Rank on person.Rank2Id equals rank2.Id into _rank2
                               from rank2 in _rank2.DefaultIfEmpty()

                               join rankhon in context.RankHonorary on person.RankHonoraryId equals rankhon.Id into _rankhon
                               from rankhon in _rankhon.DefaultIfEmpty()
                               join rankhon2 in context.RankHonorary on person.RankHonorary2Id equals rankhon2.Id into _rankhon2
                               from rankhon2 in _rankhon2.DefaultIfEmpty()

                               join rankstate in context.RankState on person.RankStateId equals rankstate.Id into _rankstate
                               from rankstate in _rankstate.DefaultIfEmpty()
                               join rankstate2 in context.RankState on person.RankState2Id equals rankstate2.Id into _rankstate2
                               from rankstate2 in _rankstate2.DefaultIfEmpty()

                               join orgpos in context.PersonOrgPosition on x.PartnerPersonId equals orgpos.PartnerPersonId into _orgpos
                               from orgpos in _orgpos.DefaultIfEmpty()
                               where (x.GAKId == GAKId) && ((bool)x.Chairman == true) &&
                                    ((lp.StudyLevelId == 16) || (lp.StudyLevelId == 17) || (lp.StudyLevelId == 18) || (lp.StudyLevelId == 15) || (lp.StudyLevelId == 20)) &&
                                    //(StudyLevelId.HasValue ? lp.StudyLevelId == StudyLevelId : true) &&
                                    (LicenseOrderId.HasValue ? lp.LicenseId == LicenseOrderId : true) //&& (x.DateCreated < DateCreated)
                                    //(LPId.HasValue ? x.LicenseProgramId == LPId : true)
                               orderby level.Id, (rbtnAlphabetOrder.Checked ? lp.Name : lp.Code), person.Name
                               select new
                               {
                                   StLevelId = lp.StudyLevelId,
                                   StudyLevelName = level.Name,
                                   LPCode = lp.Code,
                                   LPName = lp.Name,
                                   FIO = person.Name,
                                   x.Chairman,
                                   orgpos.OrgPosition,
                                   Degree = (person.DegreeId.HasValue) ? ((person.Degree2Id.HasValue) ? (degree.Name + ", " + degree2.Name) : degree.Name) : ((person.Degree2Id.HasValue) ? (degree2.Name) : ""),
                                   Rank = (person.RankId.HasValue) ? ((person.Rank2Id.HasValue) ? (rank.Name + ", " + rank2.Name) : rank.Name) : ((person.Rank2Id.HasValue) ? (rank2.Name) : ""),
                                   RankHonorary = (person.RankHonoraryId.HasValue) ? ((person.RankHonorary2Id.HasValue) ? (rankhon.Name + ", " + rankhon2.Name) : rankhon.Name) : ((person.RankHonorary2Id.HasValue) ? (rankhon2.Name) : ""),
                                   RankState = (person.RankStateId.HasValue) ? ((person.RankState2Id.HasValue) ? (rankstate.Name + ", " + rankstate2.Name) : rankstate.Name) : ((person.RankState2Id.HasValue) ? (rankstate.Name) : ""), 
                                   //Ученая_степень = person.Degree.Name,
                                   //Ученое_звание = person.Rank.Name,
                                   //Почетное_звание = person.RankHonorary.Name,
                                   //Государственное_или_военное_звание = person.RankState.Name,
                                   TitleDop = person.Title,
                                   x.Id,
                                   x.PartnerPersonId,
                                   //x.LicenseProgramId
                               }).ToList();

                    int StLevelId = 0;
                    string StudyLevelName = "";
                    string LPCode = "";
                    int i = 0;
                    int j = 0;
                    int k = 0;
                    string ChairmanList = "";
                    string Title = "";
                    string TitleNext = "";
                    string ListBMC = "";

                    foreach (var item in lst)
                    {
                        if (item.StLevelId != StLevelId)
                        {
                            StLevelId = item.StLevelId;
                            StudyLevelName = item.StudyLevelName;
                            switch (StLevelId)
	                        {
                                case 15:
                                    wd.SetFields("StudyLevelAsp", StudyLevelName);
                                    if (ListBMC != "")
                                        wd.SetFields(ListBMC, ChairmanList); 
                                    ListBMC = "ListAsp";
                                    k = 4;
                                    break;
                                case 16:
                                    wd.SetFields("StudyLevelBac", StudyLevelName);
                                    if (ListBMC != "")
                                        wd.SetFields(ListBMC, ChairmanList); 
                                    ListBMC = "ListBac";
                                    k = 1;
                                    break;
		                        case 17:
                                    wd.SetFields("StudyLevelMag", StudyLevelName);
                                    if (ListBMC != "")
                                        wd.SetFields(ListBMC, ChairmanList); 
                                    ListBMC = "ListMag";
                                    k = 2;
                                    break;
                                case 18:
                                    wd.SetFields("StudyLevelSpec", StudyLevelName);
                                    if (ListBMC != "")
                                        wd.SetFields(ListBMC, ChairmanList); 
                                    ListBMC = "ListSpec";
                                    k = 3;
                                    break;
                                case 20:
                                    wd.SetFields("StudyLevelOrd", StudyLevelName);
                                    if (ListBMC != "")
                                        wd.SetFields(ListBMC, ChairmanList); 
                                    ListBMC = "ListOrd";
                                    k = 5;
                                    break;
	                        }
                            LPCode = "";
                            i = 0;
                            j = 0;
                            ChairmanList = "";
                            Title = "";
                            TitleNext = "";
                        }
                        if (item.LPCode != LPCode)
                        {
                            LPCode = item.LPCode;
                            i++;
                            j = 0; 
                            ChairmanList = ChairmanList + "\r\n" + k + "." + i + ". " + item.LPCode + " " + item.LPName + "\r\n";  // + "\t";
                        }
                        j++;
                        Title = "";
                        TitleNext = "";
                        Title = (!String.IsNullOrEmpty(item.Degree)) ? item.Degree : "";
                        TitleNext = (!String.IsNullOrEmpty(item.Rank)) ? item.Rank : "";
                        Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext; 
                        TitleNext = item.OrgPosition;
                        Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                        TitleNext = (!String.IsNullOrEmpty(item.RankHonorary)) ? item.RankHonorary : "";
                        Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                        TitleNext = (!String.IsNullOrEmpty(item.RankState)) ? item.RankState : "";
                        Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                        TitleNext = (!String.IsNullOrEmpty(item.TitleDop)) ? item.TitleDop : "";
                        Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext; 

                        ChairmanList = ChairmanList + "\t" + k + "." + i + "." + j + "." + item.FIO + ((!string.IsNullOrEmpty(Title)) ? (", " + Title) : "") + ";" + "\r\n";

                        //if (item.LPCode != LPCode)
                        //{
                        //    LPCode = item.LPCode;
                        //    i++;
                        //    j = 0;
                        //    if (i == 1)
                        //    {
                        //         ChairmanList = ChairmanList + item.LPCode + " " + item.LPName + "\r\n" + "\t";
                        //    }
                        //    else
                        //    {
                        //        SendKeys.Send("+{TAB}");
                        //        ChairmanList = ChairmanList + item.LPCode + " " + item.LPName + "\r\n" + "\t";
                        //    }
                        //}
                        //j++;
                        //ChairmanList = ChairmanList + item.FIO + ", " + item.OrgPosition + ";" + "\r\n";

                        ////Заморозка приказа (вынести в отдельную кнопку)
                        //string OrderNumber = "10681/1";
                        //string OrderDate = "30.12.2016";
                        //var gakperson = context.GAK_LP_Person.Where(x => x.Id == item.Id).First();
                        //if (!String.IsNullOrEmpty(OrderNumber))
                        //{
                        //    gakperson.Freeze = true;
                        //    gakperson.InOrder = true;
                        //    gakperson.OrderNumber = OrderNumber;
                        //    gakperson.OrderDate = DateTime.Parse(OrderDate);
                        //    gakperson.OrderDop = false;
                        //    gakperson.PersonNumberInOrder = k + "." + i + "." + j; // +".";
                        //}
                        //else   //Разморозка приказа
                        //{
                        //    gakperson.Freeze = null;
                        //    gakperson.InOrder = null;
                        //    gakperson.OrderNumber = null;
                        //    gakperson.OrderDate = null;
                        //    gakperson.OrderDop = null;
                        //    gakperson.PersonNumberInOrder = null;
                        //}
                        //context.SaveChanges();
                    }
                    wd.SetFields(ListBMC, ChairmanList); 
                }
            }
            catch (WordException we)
            {
                MessageBox.Show(we.Message);
            }

            //Заполнение номеров источников данных
            try
            {
                string SourceNumbers = "";

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_ChairmanSource
                               orderby x.Faculty
                               select new
                               {
                                   x.Numbers
                               }).ToList();
                
                foreach (var item in lst)
                { 
                    if (!String.IsNullOrEmpty(item.Numbers)) 
	                {
                        if (SourceNumbers == "")
                        {
                            SourceNumbers = item.Numbers.ToString();
                        }
                        else
                        {
                            SourceNumbers += ", " + item.Numbers.ToString();
                        }
	                }

                }
                if (SourceNumbers != "")
                {
                    SourceNumbers += ".";
                }


                wd.SetFields("Source", SourceNumbers);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось вывести номера источников данных\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);   
            }
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("GAK_Source"))
                return;
            new GAK_Source().Show();
        }

        private void btnShowPanel2_Click(object sender, EventArgs e)
        {
            try
            {
                splitContainer1.Panel2Collapsed = !splitContainer1.Panel2Collapsed;
            }
            catch (Exception)
            {
            }
        }

        private void btnDopChairmanDoc_Click(object sender, EventArgs e)
        {
            //проверка наличия данных
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_LP_Person
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join level in context.StudyLevel on lp.StudyLevelId equals level.Id
                               where (x.GAKId == GAKId) && ((bool)x.Chairman == true) &&
                                    ((lp.StudyLevelId == 16) || (lp.StudyLevelId == 17) || (lp.StudyLevelId == 18) || (lp.StudyLevelId == 15) || (lp.StudyLevelId == 20)) &&
                                    (LicenseOrderId.HasValue ? lp.LicenseId == LicenseOrderId : true) 
                               //orderby level.Id, (rbtnAlphabetOrder.Checked ? lp.Name : lp.Code)
                               select new
                               {
                                   x.Id
                               }).Count();
                    if (lst == 0)
                    {
                        MessageBox.Show("Нет данных...", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cbLicenseOrder.DroppedDown = true;
                        return;
                    }

                    lst = (from x in context.GAK_LP_Person
                           join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                           join level in context.StudyLevel on lp.StudyLevelId equals level.Id
                           where (x.GAKId == GAKId) && ((bool)x.Chairman == true) &&
                                ((lp.StudyLevelId == 16) || (lp.StudyLevelId == 17) || (lp.StudyLevelId == 18) || (lp.StudyLevelId == 15) || (lp.StudyLevelId == 20)) &&
                                (LicenseOrderId.HasValue ? lp.LicenseId == LicenseOrderId : true) &&
                           ((x.OrderNumber == null) || (x.OrderNumber == ""))
                           //orderby level.Id, (rbtnAlphabetOrder.Checked ? lp.Name : lp.Code)
                           select new
                           {
                               x.Id
                           }).Count();
                    if (lst == 0)
                    {
                        MessageBox.Show("Нет новых данных для дополнения к приказу...", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch (Exception)
            {
            }
            OrderDopChairmanToDoc();
        }

        private void OrderDopChairmanToDoc()
        {
            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "";
            templatename = "Приказ_ГЭК_председатели дополнение";
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

            WordDoc wd = new WordDoc(string.Format(filePath), true);
            //заполнение шаблона
            try
            {
                //WordDoc wd = new WordDoc(string.Format(filePath), true);

                //string StudyLevel = "бакалавриат";
                //wd.SetFields("StudyLevelBac", StudyLevel);
                DateTime DateCreated = DateTime.Parse("30.12.2016");

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_LP_Person
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join level in context.StudyLevel on lp.StudyLevelId equals level.Id
                               join person in context.PartnerPerson on x.PartnerPersonId equals person.Id

                               join degree in context.Degree on person.DegreeId equals degree.Id into _degree
                               from degree in _degree.DefaultIfEmpty()
                               join degree2 in context.Degree on person.Degree2Id equals degree2.Id into _degree2
                               from degree2 in _degree2.DefaultIfEmpty()

                               join rank in context.Rank on person.RankId equals rank.Id into _rank
                               from rank in _rank.DefaultIfEmpty()
                               join rank2 in context.Rank on person.Rank2Id equals rank2.Id into _rank2
                               from rank2 in _rank2.DefaultIfEmpty()

                               join rankhon in context.RankHonorary on person.RankHonoraryId equals rankhon.Id into _rankhon
                               from rankhon in _rankhon.DefaultIfEmpty()
                               join rankhon2 in context.RankHonorary on person.RankHonorary2Id equals rankhon2.Id into _rankhon2
                               from rankhon2 in _rankhon2.DefaultIfEmpty()

                               join rankstate in context.RankState on person.RankStateId equals rankstate.Id into _rankstate
                               from rankstate in _rankstate.DefaultIfEmpty()
                               join rankstate2 in context.RankState on person.RankState2Id equals rankstate2.Id into _rankstate2
                               from rankstate2 in _rankstate2.DefaultIfEmpty()

                               join orgpos in context.PersonOrgPosition on x.PartnerPersonId equals orgpos.PartnerPersonId into _orgpos
                               from orgpos in _orgpos.DefaultIfEmpty()

                               join order in context.GAK_LP_PersonOrderYear(GAKId) on x.LicenseProgramId equals order.LicenseProgramId 

                               where (x.GAKId == GAKId) &&((bool)x.Chairman == true) &&
                                    ((lp.StudyLevelId == 16) || (lp.StudyLevelId == 17) || (lp.StudyLevelId == 18) || (lp.StudyLevelId == 15) || (lp.StudyLevelId == 20)) &&
                                   //(StudyLevelId.HasValue ? lp.StudyLevelId == StudyLevelId : true) &&
                                    (LicenseOrderId.HasValue ? lp.LicenseId == LicenseOrderId : true) //&& ((x.OrderNumber == null) || (x.OrderNumber == ""))
                                    //&& (x.DateCreated < DateCreated)
                               //(LPId.HasValue ? x.LicenseProgramId == LPId : true)
                               orderby level.Id, order.Order descending, (rbtnAlphabetOrder.Checked ? lp.Name : lp.Code), x.InOrder descending, person.Name
                               select new
                               {
                                   StLevelId = lp.StudyLevelId,
                                   StudyLevelName = level.Name,
                                   LPCode = lp.Code,
                                   LPName = lp.Name,
                                   FIO = person.Name,
                                   x.Chairman,
                                   x.IsNotActive,
                                   orgpos.OrgPosition,
                                   Degree = (person.DegreeId.HasValue) ? ((person.Degree2Id.HasValue) ? (degree.Name + ", " + degree2.Name) : degree.Name) : ((person.Degree2Id.HasValue) ? (degree2.Name) : ""),
                                   Rank = (person.RankId.HasValue) ? ((person.Rank2Id.HasValue) ? (rank.Name + ", " + rank2.Name) : rank.Name) : ((person.Rank2Id.HasValue) ? (rank2.Name) : ""),
                                   RankHonorary = (person.RankHonoraryId.HasValue) ? ((person.RankHonorary2Id.HasValue) ? (rankhon.Name + ", " + rankhon2.Name) : rankhon.Name) : ((person.RankHonorary2Id.HasValue) ? (rankhon2.Name) : ""),
                                   RankState = (person.RankStateId.HasValue) ? ((person.RankState2Id.HasValue) ? (rankstate.Name + ", " + rankstate2.Name) : rankstate.Name) : ((person.RankState2Id.HasValue) ? (rankstate.Name) : ""),
                                   //Ученая_степень = person.Degree.Name,
                                   //Ученое_звание = person.Rank.Name,
                                   //Почетное_звание = person.RankHonorary.Name,
                                   //Государственное_или_военное_звание = person.RankState.Name,
                                   TitleDop = person.Title,
                                   x.OrderNumber,
                                   x.Id,
                                   x.PartnerPersonId,
                                   //x.LicenseProgramId
                               }).ToList();

                    int StLevelId = 0;
                    string StudyLevelName = "";
                    string LPCode = "";
                    int i = 0;
                    int j = 0;
                    int k = 0;
                    string ChairmanList = "";
                    string ChairmanList2 = "";
                    string ChairmanList3 = "";
                    string Title = "";
                    string TitleNext = "";
                    string ListBMC = "";

                    foreach (var item in lst)
                    {
                        if (item.StLevelId != StLevelId)
                        {
                            StLevelId = item.StLevelId;
                            StudyLevelName = item.StudyLevelName;
                            switch (StLevelId)
                            {
                                case 15:
                                    //wd.SetFields("StudyLevelAsp", StudyLevelName);
                                    if (ListBMC != "")
                                        wd.SetFields(ListBMC, ChairmanList);
                                    ListBMC = "ListAsp";
                                    k = 4;
                                    break;
                                case 16:
                                    //wd.SetFields("StudyLevelBac", StudyLevelName);
                                    if (ListBMC != "")
                                        wd.SetFields(ListBMC, ChairmanList);
                                    ListBMC = "ListBac";
                                    //ListBMC = "ListDop";
                                    k = 1;
                                    break;
                                case 17:
                                    //wd.SetFields("StudyLevelMag", StudyLevelName);
                                    if (ListBMC != "")
                                        wd.SetFields(ListBMC, ChairmanList);
                                    ListBMC = "ListMag";
                                    //ListBMC = "ListDop";
                                    k = 2;
                                    break;
                                case 18:
                                    //wd.SetFields("StudyLevelSpec", StudyLevelName);
                                    if (ListBMC != "")
                                        wd.SetFields(ListBMC, ChairmanList);
                                    ListBMC = "ListSpec";
                                    //ListBMC = "ListDop";
                                    k = 3;
                                    break;
                                case 20:
                                    //wd.SetFields("StudyLevelOrd", StudyLevelName);
                                    if (ListBMC != "")
                                        wd.SetFields(ListBMC, ChairmanList);
                                    ListBMC = "ListOrd";
                                    k = 5;
                                    break;
                            }
                            LPCode = "";
                            i = 0;
                            j = 0;
                            ChairmanList = "";
                            ChairmanList2 = "";
                            ChairmanList3 = "";
                            Title = "";
                            TitleNext = "";
                        }
                        if (item.LPCode != LPCode)
                        {
                            LPCode = item.LPCode;
                            i++;
                            j = 0;
                            if (ChairmanList3 == ChairmanList)
                            {
                                ChairmanList = ChairmanList2;
                            }
                            ChairmanList2 = ChairmanList;
                            ChairmanList = ChairmanList + "\r\n" + k + "." + i + ". " + item.LPCode + " " + item.LPName + "\r\n";  // + "\t";
                            ChairmanList3 = ChairmanList;
                        }
                        j++;
                        //if ((item.OrderNumber == null) || (item.OrderNumber == ""))
                        //{
                            Title = "";
                            TitleNext = "";
                            Title = (!String.IsNullOrEmpty(item.Degree)) ? item.Degree : "";
                            TitleNext = (!String.IsNullOrEmpty(item.Rank)) ? item.Rank : "";
                            Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                            TitleNext = item.OrgPosition;
                            Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                            TitleNext = (!String.IsNullOrEmpty(item.RankHonorary)) ? item.RankHonorary : "";
                            Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                            TitleNext = (!String.IsNullOrEmpty(item.RankState)) ? item.RankState : "";
                            Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                            TitleNext = (!String.IsNullOrEmpty(item.TitleDop)) ? item.TitleDop : "";
                            Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;

                            if ((item.OrderNumber == null) || (item.OrderNumber == ""))
                            {
                                ChairmanList = ChairmanList + "\t" + k + "." + i + "." + j + "." + item.FIO + ((!string.IsNullOrEmpty(Title)) ? (", " + Title) : "") + ";" + "\r\n";
                            }
                            else
                            {
                                if (item.IsNotActive.HasValue)
                                {
                                    if ((bool)item.IsNotActive)
                                    {
                                        ChairmanList = ChairmanList + "\t" + k + "." + i + "." + j + "." + " - исключить" + ";" + "\r\n";
                                    }
                                }
                            }
                        //}

                        //if (item.LPCode != LPCode)
                        //{
                        //    LPCode = item.LPCode;
                        //    i++;
                        //    j = 0;
                        //    if (i == 1)
                        //    {
                        //        ChairmanList = ChairmanList + item.LPCode + " " + item.LPName + "\r\n" + "\t";
                        //    }
                        //    else
                        //    {
                        //        SendKeys.Send("+{TAB}");
                        //        ChairmanList = ChairmanList + item.LPCode + " " + item.LPName + "\r\n" + "\t";
                        //    }
                        //}
                        //j++;
                        //ChairmanList = ChairmanList + item.FIO + ", " + item.OrgPosition + ";" + "\r\n";

                        ////Заморозка приказа (вынести в отдельную кнопку)
                        //string OrderNumber = "10681/1";
                        //string OrderDate = "30.12.2016";
                        //var gakperson = context.GAK_LP_Person.Where(x => x.Id == item.Id).First();
                        //if (!String.IsNullOrEmpty(OrderNumber))
                        //{
                        //    gakperson.Freeze = true;
                        //    gakperson.InOrder = true;
                        //    gakperson.OrderNumber = OrderNumber;
                        //    gakperson.OrderDate = DateTime.Parse(OrderDate);
                        //    gakperson.OrderDop = false;
                        //    gakperson.PersonNumberInOrder = k + "." + i + "." + j; // +".";
                        //}
                        //else   //Разморозка приказа
                        //{
                        //    gakperson.Freeze = null;
                        //    gakperson.InOrder = null;
                        //    gakperson.OrderNumber = null;
                        //    gakperson.OrderDate = null;
                        //    gakperson.OrderDop = null;
                        //    gakperson.PersonNumberInOrder = null;
                        //}
                        ////Заморозка дополнения к приказу (вынести в отдельную кнопку)
                        //string OrderNumber = "3167/1";
                        //string OrderDate = "10.04.2017";
                        //var gakperson = context.GAK_LP_Person.Where(x => x.Id == item.Id).First();
                        //if (!String.IsNullOrEmpty(OrderNumber))
                        //{
                        //    gakperson.Freeze = true;
                        //    gakperson.InOrder = true;
                        //    gakperson.OrderNumber = OrderNumber;
                        //    gakperson.OrderDate = DateTime.Parse(OrderDate);
                        //    gakperson.OrderDop = true;
                        //    gakperson.PersonNumberInOrder = k + "." + i + "." + j; // +".";
                        //}
                        //else   //Разморозка приказа
                        //{
                        //    //gakperson.Freeze = null;
                        //    //gakperson.InOrder = null;
                        //    //gakperson.OrderNumber = null;
                        //    //gakperson.OrderDate = null;
                        //    //gakperson.OrderDop = null;
                        //    //gakperson.PersonNumberInOrder = null;
                        //}

                        context.SaveChanges();
                    }
                    if (ChairmanList2 != "")
                    {
                        wd.SetFields(ListBMC, ChairmanList);
                    }
                    //wd.SetFields(ListBMC, ChairmanList);
                }
            }
            catch (WordException we)
            {
                MessageBox.Show(we.Message);
            }

            //Заполнение номеров источников данных
            try
            {
                string SourceNumbers = "";

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_Source
                               orderby x.Faculty
                               select new
                               {
                                   x.Numbers
                               }).ToList();

                    foreach (var item in lst)
                    {
                        if (!String.IsNullOrEmpty(item.Numbers))
                        {
                            if (SourceNumbers == "")
                            {
                                SourceNumbers = item.Numbers.ToString();
                            }
                            else
                            {
                                SourceNumbers += ", " + item.Numbers.ToString();
                            }
                        }

                    }
                    if (SourceNumbers != "")
                    {
                        SourceNumbers += ".";
                    }


                    wd.SetFields("Source", SourceNumbers);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось вывести номера источников данных\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dgvChairman_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == dgvChairman.Columns["Недействителен"].Index && e.Value.ToString() == "недействителен")
            {
                e.CellStyle.BackColor = Color.LightPink;
            }
            if (e.ColumnIndex == dgvChairman.Columns["ФИО"].Index && dgvChairman.Rows[e.RowIndex].Cells["недействителен"].Value.ToString() == "недействителен")
            {
                e.CellStyle.BackColor = Color.LightPink;
            }
        }
    }
}
