using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace EmployerPartners
{
    public partial class UpdateFromSrv : Form
    {
        private int? LPLevel
        {
            get { return ComboServ.GetComboIdInt(cbLPLevel); }
            set { ComboServ.SetComboId(cbLPLevel, value); }
        }
        private int? LPProgramType
        {
            get { return ComboServ.GetComboIdInt(cbLPProgramType); }
            set { ComboServ.SetComboId(cbLPProgramType, value); }
        }
        private int? LPLicense
        {
            get { return ComboServ.GetComboIdInt(cbLicense); }
            set { ComboServ.SetComboId(cbLicense, value); }
        }

        public UpdateFromSrv()
        {
            InitializeComponent();
            FillLP();
            FillLicense();
            //FillOP();
            FillOPInYear();
            //FillStudent();
            HideTabPage();
            FillCombo();
            SetAccessRight();
        }
        private void SetAccessRight()
        {
            if (Util.IsOrgPersonWrite() || Util.IsDBOwner() || Util.IsAdministrator())
            {
                btnCheckLP.Enabled = true;
                btnUpdateLP.Enabled = true;
                btnCheckOP.Enabled = true;
                btnUpdateOP.Enabled = true;
                //btnUpdateStudent.Enabled = true;
            }
        }
        private void HideTabPage()
        {
            tabControl1.TabPages.Remove(tabPage3);

        }
        private void FillCombo()
        {
            FillLPLevel();
            FillLPProgramType();
        }
        private void FillLPLevel()
        {
            ComboServ.FillCombo(cbLPLevel, HelpClass.GetComboListByTable("dbo.StudyLevel"), false, true);
//            ComboServ.FillCombo(cbLPLevel, HelpClass.GetComboListByQuery(@" select distinct [Name]  AS Id, 
//                        [Name] from dbo.StudyLevel order by Name"), false, true);
        }
        private void FillLPProgramType()
        {
            ComboServ.FillCombo(cbLPProgramType, HelpClass.GetComboListByTable("dbo.ProgramType"), false, true);
        }
        private void FillLicense()
        {
            ComboServ.FillCombo(cbLicense, HelpClass.GetComboListByQuery(@" SELECT DISTINCT CONVERT(varchar(100), Id) AS Id, 
                ('Лицензия  Серия: ' + Isnull([Series], '') + '  Номер: ' + Isnull([Number], '') + '  Рег.номер: ' +
                    IsNull(RegNum, '') + '  Дата: ' + case [DateOut] when null then '' when '' then '' else convert(char(12), [DateOut], 3) end)  As Name 
                FROM dbo.License ORDER BY Id DESC "), false, true); //cast([DateOut] as varchar) //CONVERT(char(12), GETDATE(), 3)
        }

        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Операция обновления данных потребует\r\n" + "обращения к другим серверам БД Университета \r\n" +
                "и может занять некоторое время \r\n" + "Подтверждаете продолжение? ", "Запрос на подтверждение", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
            { return; }
            //this.UseWaitCursor = true;
            pBar1.Visible = true;
            pBar1.Minimum = 30;
            pBar1.Maximum = 90;
            pBar1.Value = 45;
            pBar1.Invalidate();
            if (!Utilities.UpdateStudentDVZ())
            {
                //this.UseWaitCursor = false;
                pBar1.Visible = false;
                MessageBox.Show("Не удалось получить данные с сервера DEVELOPMENT \r\n" +
                    "[В большинстве случаев это означает, что \r\n" + "недостаточно прав доступа.]", "Отмена действия", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            pBar1.Value = 60; 
            pBar1.Invalidate();
            if (Utilities.UpdateStudentData())
            {
                //this.UseWaitCursor = false;
                pBar1.Value = 90;
                MessageBox.Show("Данные обновлены", "Успешное завершение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillStudent();
                pBar1.Visible = false;
                return;
            }
            else
            {
                //this.UseWaitCursor = false;
                pBar1.Visible = false;
                MessageBox.Show("Не удалось обновить данные... \r\n" +
                    "[В большинстве случаев это означает, что \r\n" + "недостаточно прав доступа к другим серверам баз данных.]", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void FillLP()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from lp in context.LicenseProgram
                               //join linlp in context.LicenseInLicenseProgram on lp.Id equals linlp.LicenseId into _linlp
                               //from linlp in _linlp.DefaultIfEmpty()
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               join progt in context.ProgramType on lp.ProgramTypeId equals progt.Id
                               join q in context.Qualification on lp.QualificationId equals q.Id into _q
                               from q in _q.DefaultIfEmpty()
                               where ((LPLevel.HasValue) ? lp.StudyLevelId == LPLevel : true) &&
                                        ((LPProgramType.HasValue) ? lp.ProgramTypeId == LPProgramType : true) &&
                                        ((LPLicense.HasValue) ? lp.LicenseId == LPLicense : true) 
                               orderby lp.Name, lp.Code, st.Name, progt.Id
                               select new
                               {
                                   Код = lp.Code,
                                   Направление = lp.Name,
                                   Уровень = st.Name,
                                   Тип_программы = progt.Name,
                                   Квалификация = q.Name,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSourceLP.DataSource = dt;
                    dgvLP.DataSource = bindingSourceLP;

                    foreach (DataGridViewColumn col in dgvLP.Columns)
                        col.HeaderText = col.Name.Replace("_", " ");
                    try
                    {
                        dgvLP.Columns["Код"].Frozen = true;
                        dgvLP.Columns["Направление"].Frozen = true;
                        dgvLP.Columns["Направление"].Width = 450;
                        dgvLP.Columns["Квалификация"].Width = 300;
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
        private void FillOP()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from op in context.ObrazProgram
                               join lp in context.LicenseProgram on op.LicenseProgramId equals lp.Id
                               join ps in context.ProgramStatus on op.ProgramStatusId equals ps.Id into _ps
                               from ps in _ps.DefaultIfEmpty()
                               orderby op.Name
                               select new
                               {
                                   //Образовательная_программа = "[ " + op.Number + " ] " + op.Name  + " [ " + ps.Name + " ]",
                                   Образовательная_программа = op.Name,
                                   Номер = op.Number,
                                   Статус = ps.Name,
                                   Направление = lp.Name,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSourceOP.DataSource = dt;
                    dgvOP.DataSource = bindingSourceOP;

                    foreach (DataGridViewColumn col in dgvOP.Columns)
                        col.HeaderText = col.Name.Replace("_", " ");
                    try
                    {
                        dgvOP.Columns["Образовательная_программа"].Frozen = true;
                        dgvOP.Columns["Образовательная_программа"].Width = 450;
                        dgvOP.Columns["Направление"].Width = 450;
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

        private void FillOPInYear()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from op in context.ObrazProgram
                               join opyear in context.ObrazProgramInYear on op.Id equals opyear.ObrazProgramId
                               join lp in context.LicenseProgram on op.LicenseProgramId equals lp.Id
                               join ps in context.ProgramStatus on op.ProgramStatusId equals ps.Id into _ps
                               from ps in _ps.DefaultIfEmpty()
                               orderby op.Name
                               select new
                               {
                                   //Образовательная_программа = "[ " + op.Number + " ] " + op.Name  + " [ " + ps.Name + " ]",
                                   Образовательная_программа = op.Name,
                                   //OPInYearName = opyear.Name,
                                   Номер = op.Number,
                                   Уровень = opyear.StudyLevelName,
                                   Год = opyear.Year,
                                   Шифр_ОП = opyear.ObrazProgramCrypt,
                                   Статус = ps.Name,
                                   Направление = lp.Name,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSourceOP.DataSource = dt;
                    dgvOP.DataSource = bindingSourceOP;

                    foreach (DataGridViewColumn col in dgvOP.Columns)
                        col.HeaderText = col.Name.Replace("_", " ");
                    try
                    {
                        dgvOP.Columns["Образовательная_программа"].Frozen = true;
                        dgvOP.Columns["Образовательная_программа"].Width = 450;
                        dgvOP.Columns["Направление"].Width = 450;
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

        private void btnUpdateLP_Click(object sender, EventArgs e)
        {
            //Проверка наличия новых данных
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    string sqlLP = "SELECT [Id], [Code], [Name] FROM  SRVEDUCATION.Education.ed.SP_LicenseProgram WHERE (Id not in (SELECT Id FROM dbo.LicenseProgram))";

                    var LPTable = context.Database.SqlQuery<LP>(sqlLP);

                    if (LPTable.Count() == 0)
                    {
                        if (MessageBox.Show("Нет новых данных для обновления\r\n" + "Обновить существующие записи?", "Запрос на подтверждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                        {
                            labelLP.Text = "Список";
                            FillLP();
                            return;
                        }
                    }
                }
            }
            catch (Exception)
            {
                labelLP.Text = "Список";
                MessageBox.Show("Не удалось получить данные... \r\n" +
                    "[В большинстве случаев это означает, что \r\n" + "недостаточно прав доступа к другим серверам баз данных.]", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //Добавление новых данных
            if (Utilities.UpdateLP())
            {
                MessageBox.Show("Данные обновлены", "Успешное завершение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                labelLP.Text = "Список";
                FillLP();
                return;
            }
            else
            {
                MessageBox.Show("Не удалось обновить данные... \r\n" +
                    "[В большинстве случаев это означает, что \r\n" + "недостаточно прав доступа к другим серверам баз данных.]", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        private void btnCheckLP_Click(object sender, EventArgs e)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    string sqlLP = "SELECT [Id], [Code], [Name] FROM  SRVEDUCATION.Education.ed.SP_LicenseProgram WHERE (Id not in (SELECT Id FROM dbo.LicenseProgram))";

                    var LPTable = context.Database.SqlQuery<LP>(sqlLP);

                    var lst = (from lp in LPTable
                               select new
                               {
                                   Код = lp.Code,
                                   Направление = lp.Name,
                               }).ToList();
                    if (lst.Count() == 0)
                    {
                        MessageBox.Show("Нет новых данных для обновления", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        labelLP.Text = "Список";
                        FillLP();
                        return;
                    }

                    labelLP.Text = "Новые данные для обновления";

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSourceLP.DataSource = dt;
                    dgvLP.DataSource = bindingSourceLP;
                    try
                    {
                        dgvLP.Columns["Направление"].Width = 350;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                labelLP.Text = "Список";
                MessageBox.Show("Не удалось получить данные... \r\n" + 
                    "[В большинстве случаев это означает, что \r\n" + "недостаточно прав доступа к другим серверам баз данных.]\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCheckOP_Click(object sender, EventArgs e)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //string sqlOP = "SELECT Id, Number, Name FROM  SRVEDUCATION.Education.ed.SP_ObrazProgram WHERE (Id not in (SELECT Id FROM dbo.ObrazProgram))";
                    //var OPTable = context.Database.SqlQuery<OP>(sqlOP);

                    string sqlOPInYear = "SELECT ObrazProgramInYearId, Number, Name, StudyLevelName, Year, ObrazProgramCrypt FROM  SRVEDUCATION.[Education].[ed].[extObrazProgramInYear] " +
                        "WHERE (ObrazProgramInYearId NOT IN (SELECT ObrazProgramInYearId FROM [dbo].[ObrazProgramInYear]))";

                    var OPTable = context.Database.SqlQuery<OPInYear>(sqlOPInYear);

                    var lst = (from op in OPTable
                               select new
                               {
                                   Номер = op.Number,
                                   Образовательная_программа = op.Name,
                                   Уровень = op.StudyLevelName,
                                   Год = op.Year,
                                   Шифр_ОП = op.ObrazProgramCrypt
                               }).ToList();
                    if (lst.Count() == 0)
                    {
                        MessageBox.Show("Нет новых данных для обновления", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        labelOP.Text = "Список";
                        //FillOP();
                        FillOPInYear();
                        return;
                    }

                    labelOP.Text = "Новые данные для обновления";

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSourceOP.DataSource = dt;
                    dgvOP.DataSource = bindingSourceOP;
                    try
                    {
                        dgvOP.Columns["Образовательная_программа"].Width = 350;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
                labelOP.Text = "Список";
                MessageBox.Show("Не удалось получить данные... \r\n" +
                    "[В большинстве случаев это означает, что \r\n" + "недостаточно прав доступа к другим серверам баз данных.]", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnUpdateOP_Click(object sender, EventArgs e)
        {
            //Проверка наличия новых данных
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //string sqlOP = "SELECT Id FROM  SRVEDUCATION.Education.ed.SP_ObrazProgram WHERE (Id not in (SELECT Id FROM dbo.ObrazProgram))";
                    //var OPTable = context.Database.SqlQuery<OP>(sqlOP);

                    string sqlOPInYear = "SELECT ObrazProgramInYearId, Number, Name, Year, ObrazProgramCrypt FROM  SRVEDUCATION.[Education].[ed].[extObrazProgramInYear] " +
                        "WHERE (ObrazProgramInYearId NOT IN (SELECT ObrazProgramInYearId FROM [dbo].[ObrazProgramInYear]))";

                    var OPTable = context.Database.SqlQuery<OPInYear>(sqlOPInYear);


                    if (OPTable.Count() == 0)
                    {
                        if (MessageBox.Show("Нет новых данных для обновления\r\n" + "Обновить существующие записи?", "Запрос на подтверждение", 
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                        {
                            labelOP.Text = "Список";
                            // FillOP();
                            FillOPInYear();
                            return;
                        }
                    }
                }
            }
            catch (Exception)
            {
                labelOP.Text = "Список";
                MessageBox.Show("Не удалось получить данные... \r\n" +
                    "[В большинстве случаев это означает, что \r\n" + "недостаточно прав доступа к другим серверам баз данных.]", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //Добавление новых данных
            if (Utilities.UpdateOP())
            {
                MessageBox.Show("Данные обновлены", "Успешное завершение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                labelOP.Text = "Список";
                //FillOP();
                FillOPInYear();
                return;
            }
            else
            {
                MessageBox.Show("Не удалось обновить данные... \r\n" +
                    "[В большинстве случаев это означает, что \r\n" + "недостаточно прав доступа к другим серверам баз данных.]", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        private void FillStudent()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.StudentData
                               orderby x.FIO
                               select new
                               {
                                   ФИО = x.FIO,
                                   Фамилия = x.Surname,
                                   //Дата_рожд = x.DR,
                                   Аккаунт = x.Accout,
                                   Курс = x.Course,
                                   Уровень = x.DegreeName,
                                   Форма_обуч = x.Department,
                                   Код_направления = x.SpecNumber,
                                   Направление_подготовки = x.SpecName,
                                   Учебный_план = x.WorkPlan,
                                   Статус = x.StatusName,
                                   Состояние = x.StudyingName,
                                   Направление = x.FacultyName,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSourceStudent.DataSource = dt;
                    dgvStudent.DataSource = bindingSourceStudent;

                    foreach (DataGridViewColumn col in dgvStudent.Columns)
                        col.HeaderText = col.Name.Replace("_", " ");
                    try
                    {
                        dgvStudent.Columns["Фамилия"].Visible = false;
                        dgvStudent.Columns["ФИО"].Frozen = true;
                        dgvStudent.Columns["ФИО"].Width = 250;
                        dgvStudent.Columns["Курс"].Width = 60;
                        dgvStudent.Columns["Учебный_план"].Width = 200;
                        dgvStudent.Columns["Направление_подготовки"].Width = 250;
                        dgvStudent.Columns["Направление"].Width = 200;
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

        private void UpdateFromSrv_Load(object sender, EventArgs e)
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

        private void tbSearch_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                string search = tbSearch.Text.Trim().ToUpper();
                bool exit = false;
                for (int i = 0; i < dgvStudent.RowCount; i++)
                {
                    if (exit)
                    { break; }
                    for (int j = 0; j < 2 /*dgvStudent.Columns.Count*/; j++)
                    {
                        //if (j == 0)
                        //    continue;
                        int length = 1;
                        length = dgvStudent[j, i].Value.ToString().Length;
                        length = (length <= 15) ? length : 15;
                        if (dgvStudent[j, i].Value.ToString().Substring(0, length).ToUpper().Contains(search)) //(dgvStudent[j, i].Value.ToString().ToUpper().Contains(search))
                        {
                            dgvStudent.CurrentCell = dgvStudent[(j > 0) ? j-1 : j, i];
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

        private void tbSearchOP_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string search = tbSearchOP.Text.Trim().ToUpper();
                bool exit = false;
                for (int i = 0; i < dgvOP.RowCount; i++)
                {
                    if (exit)
                    { break; }
                    for (int j = 0; j < 1 /*dgvOP.Columns.Count*/; j++)
                    {
                        if (j == 1)
                            continue;
                        if (dgvOP[j, i].Value.ToString().ToUpper().Contains(search))
                        {
                            dgvOP.CurrentCell = dgvOP[j, i];
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void cbLPLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLP();
        }

        private void cbLPProgramType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLP();
        }

        private void cbLicense_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLP();
        }
    }
}
