using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmployerPartners.EDMX;

namespace EmployerPartners
{
    public partial class UpdateStudentFromSrv : Form
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
        private string FacultyName
        {
            get { return ComboServ.GetComboId(cbFacultyName); }
            set { ComboServ.SetComboId(cbFacultyName, value); }
        }
        private string SpecName
        {
            get { return ComboServ.GetComboId(cbSpecName); }
            set { ComboServ.SetComboId(cbSpecName, value); }
        }
        private int? ObrazProgramId
        {
            get { return ComboServ.GetComboIdInt(cbObrazProgram); }
            set { ComboServ.SetComboId(cbObrazProgram, value); }
        }

        //private bool StartFilter = false;

        public UpdateStudentFromSrv()
        {
            InitializeComponent();
            FillCombo();
            FillStudent();
            GetDateUpdated();
            SetAccessRight();
        }
        private void SetAccessRight()
        {
            if (Util.IsOrgPersonWrite())
            {
                btnUpdateStudent.Enabled = true;
            }
        }
        private void GetDateUpdated()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var x = context.StudentDVZ.First();
                    lblDateUpdated.Text = "По состоянию на " + ((x.TIMESTAMP.HasValue) ? x.TIMESTAMP.Value.Date.ToString("dd.MM.yyyy") : "");
                }
            }
            catch (Exception)
            {
            }

        }
        private void FillCombo()
        {
            ComboServ.FillCombo(cbDegreeName, HelpClass.GetComboListByQuery(@" select distinct [DegreeName]  AS Id, 
                                    [DegreeName] as Name from dbo.StudentData order by [DegreeName]"), false, true);
            ComboServ.FillCombo(cbCourse, HelpClass.GetComboListByQuery(@" select distinct [Course]  AS Id, 
                                    [Course] as Name from dbo.StudentData order by [Course]"), false, true);
            ComboServ.FillCombo(cbDepartment, HelpClass.GetComboListByQuery(@" select distinct [Department]  AS Id, 
                                    [Department] as Name from dbo.StudentData order by [Department]"), false, true);
            ComboServ.FillCombo(cbStatusName, HelpClass.GetComboListByQuery(@" select distinct [StatusName]  AS Id, 
                                    [StatusName] as Name from dbo.StudentData order by [StatusName] desc"), false, true);
            ComboServ.FillCombo(cbStudyingName, HelpClass.GetComboListByQuery(@" select distinct [StudyingName]  AS Id, 
                                    [StudyingName] as Name from dbo.StudentData order by [StudyingName]"), false, true);
            ComboServ.FillCombo(cbFacultyName, HelpClass.GetComboListByQuery(@" select distinct [FacultyName]  AS Id, 
                                    [FacultyName] as Name from dbo.StudentData order by [FacultyName]"), false, true);
            ComboServ.FillCombo(cbSpecName, HelpClass.GetComboListByQuery(@" select distinct [SpecName]  AS Id, 
                                    [SpecName] as Name from dbo.StudentData order by [SpecName]"), false, true);
            ComboServ.FillCombo(cbObrazProgram, HelpClass.GetComboListByQuery(@" select CONVERT(varchar(100), [Id]) AS Id, ([Number] + '  ' + Name) as Name  
                                    from dbo.ObrazProgram where Id in (select ObrazProgramId from dbo.StudentData) order by dbo.ObrazProgram.Name"), false, true);
        }
        private void FillStudent()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.StudentData
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id into _op
                               from op in _op.DefaultIfEmpty()
                               orderby x.FIO
                               where ((!String.IsNullOrEmpty(DegreeName)) ? x.DegreeName == DegreeName : true) &&
                                        ((!String.IsNullOrEmpty(Course)) ? x.Course == Course : true) &&
                                        ((!String.IsNullOrEmpty(Department)) ? x.Department == Department : true) &&
                                        ((!String.IsNullOrEmpty(StatusName)) ? x.StatusName == StatusName : true) &&
                                        ((!String.IsNullOrEmpty(StudyingName)) ? x.StudyingName == StudyingName : true) &&
                                        ((!String.IsNullOrEmpty(FacultyName)) ? x.FacultyName == FacultyName : true) &&
                                        ((!String.IsNullOrEmpty(SpecName)) ? x.SpecName == SpecName : true) &&
                                        ((ObrazProgramId.HasValue) ? x.ObrazProgramId == ObrazProgramId : true)

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
                                   Шифр_ОП = x.ObrazProgramCrypt,
                                   Образовательная_программа = op.Name,
                                   Учебный_план = x.WorkPlan,
                                   Статус = x.StatusName,
                                   Состояние = x.StudyingName,
                                   УНП = x.FacultyName,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource1.DataSource = dt;
                    dgv.DataSource = bindingSource1;

                    foreach (DataGridViewColumn col in dgv.Columns)
                        col.HeaderText = col.Name.Replace("_", " ");
                    try
                    {
                        dgv.Columns["Фамилия"].Visible = false;
                        dgv.Columns["ФИО"].Frozen = true;
                        dgv.Columns["ФИО"].Width = 250;
                        dgv.Columns["Курс"].Width = 60;
                        dgv.Columns["Учебный_план"].Width = 200;
                        dgv.Columns["Направление_подготовки"].Width = 250;
                        dgv.Columns["Образовательная_программа"].Width = 250;
                        dgv.Columns["УНП"].Width = 200;
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
        
        private void UpdateStudentFromSrv_Load(object sender, EventArgs e)
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
                    for (int j = 0; j < 2 /*dgv.Columns.Count*/; j++)
                    {
                        //if (j == 0)
                        //    continue;
                        int length = 1;
                        length = dgv[j, i].Value.ToString().Length;
                        length = (length <= 15) ? length : 15;
                        if (dgv[j, i].Value.ToString().Substring(0, length).ToUpper().Contains(search)) //(dgv[j, i].Value.ToString().ToUpper().Contains(search))
                        {
                            dgv.CurrentCell = dgv[(j > 0) ? j - 1 : j, i];
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

        private void SetFilter()
        {
            string strFilter = "";
            strFilter += (!String.IsNullOrEmpty(DegreeName)) ? ("Уровень = '" + DegreeName + "'") : "";
            strFilter += ((strFilter == "") ? "" : " AND ") + ((!String.IsNullOrEmpty(Course)) ? ("Курс = '" + Course + "'") : "");
            
            //Установка фильтра
            if (strFilter == "")
            {
                try
                {
                    if (!String.IsNullOrEmpty(bindingSource1.Filter))
                        bindingSource1.RemoveFilter();
                    return;
                }
                catch (Exception)
                { }
            }
            else
            {
                try
                {
                    if (!String.IsNullOrEmpty(bindingSource1.Filter))
                        bindingSource1.RemoveFilter();
                    bindingSource1.Filter = strFilter;
                }
                catch (Exception)
                {}
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillStudent();
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
                FacultyName = null;
                cbFacultyName.Text = "все";
                SpecName = null;
                cbSpecName.Text = "все";
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
                GetDateUpdated();
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
    }
}
