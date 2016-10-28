using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployerPartners
{
    public partial class VKRStudent : Form
    {
        private int? _Id
        {
            get;
            set;
        }
        private int? _VKRId
        {
            get;
            set;
        }
        public int VKRStCardId
        {
            get;
            set;
        }
        private int OPId
        {
            get;
            set;
        }
        private int OPInYearId
        {
            get;
            set;
        }
        private int LPId
        {
            get;
            set;
        }
        private string OP
        {
            get { return tbOP.Text.Trim(); }
            set { tbOP.Text = value; }
        }
        private string LP
        {
            get { return tbLP.Text.Trim(); }
            set { tbLP.Text = value; }
        }
        private string FacName
        {
            get;
            set;
        }
        private int? Course
        {
            get { return ComboServ.GetComboIdInt(cbCourse); }
            set { ComboServ.SetComboId(cbCourse, value); }
        }
        private string StudyingName
        {
            get { return ComboServ.GetComboId(cbStudyingName); }
            set { ComboServ.SetComboId(cbStudyingName, value); }
        }
        bool StudentDelConfirm = true;
        bool StudentShowDelConfirmSettings = true;

        public VKRStudent(int id, int vkrid, int opid, int opinyearid, int lpid, string op, string lp, string facname)
        {
            InitializeComponent();
            _Id = id;
            _VKRId = vkrid;
            VKRStCardId = id;
            OPId = opid;
            OPInYearId = opinyearid;
            LPId = lpid;
            OP = op;
            LP = lp;
            FacName = facname;
            this.MdiParent = Util.mainform;
            this.Text = "Темы ВКР: " + OP;
            //FillGrid(_Id);
            FillGrid();
            SetAccessRight();
            Archive();
        }
        private void SetAccessRight()
        {
            if (Util.IsVKRWrite())
            {
                btnDelete.Enabled = true;
                btnAddStudent.Enabled = true;
                dgv.Columns["ColumnDelStudent"].Visible = true; ;
                dgv.Columns["ColumnEditStudent"].Visible = true;
            }
        }
        private void Archive()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var vkr = context.VKR.Where(x => x.Id == _VKRId).First();

                    if ((vkr.Archive.HasValue) ? (bool)vkr.Archive : false)
                    {
                        lblArchive.Visible = true;
                        btnDelete.Enabled = false;
                        btnAddStudent.Enabled = false;
                        dgv.Columns["ColumnDelStudent"].Visible = false;
                        dgv.Columns["ColumnEditStudent"].Visible = false;
                    }
                    else
                    {
                        lblArchive.Visible = false;
                        btnDelete.Enabled = true;
                        btnAddStudent.Enabled = true;
                        dgv.Columns["ColumnDelStudent"].Visible = true;
                        dgv.Columns["ColumnEditStudent"].Visible = true;
                    }
                }
            }
            catch (Exception)
            {
                lblArchive.Visible = false;
                btnDelete.Enabled = true;
                btnAddStudent.Enabled = true;
                dgv.Columns["ColumnDelStudent"].Visible = true;
                dgv.Columns["ColumnEditStudent"].Visible = true;
            }
        }
        private void FillComboCourse()
        {
            ComboServ.FillCombo(cbCourse, HelpClass.GetComboListByQuery(@" select distinct  CONVERT(varchar(100), StudentData.Course) AS Id, CONVERT(varchar(100), StudentData.Course) as Name
                from dbo.StudentData order by Id"), false, true);
        }
        private void FillComboStudyingName()
        {
            ComboServ.FillCombo(cbStudyingName, HelpClass.GetComboListByQuery(@" select distinct  CONVERT(varchar(100), StudentData.StudyingName) AS Id, CONVERT(varchar(100), StudentData.StudyingName) as Name
                from dbo.StudentData"), false, true);
        }
        private void FillGrid()
        {
            FillGrid(null);
        }
        private void FillGrid(int? id)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKROPStudent
                               join org in context.Organization on x.OrganizationId equals org.Id into _org
                               from org in _org.DefaultIfEmpty()
                               join source in context.VKRSource on x.VKRSourceId equals source.Id into _source
                               from source in _source.DefaultIfEmpty()
                               join fac in context.Faculty on x.FacultyId equals fac.Id into _fac
                               from fac in _fac.DefaultIfEmpty()
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               //join progt in context.ProgramType on lp.ProgramTypeId equals progt.Id
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where x.VKROPId == _Id
                               orderby x.StudentFIO
                               select new
                               {
                                   Студент = x.StudentFIO,
                                   Дата_рожд = x.DR,
                                   Аккаунт_студента = x.Accout,
                                   Тема_ВКР = x.VKRName,
                                   Тема_ВКР_англ = x.VKRNameEng,
                                   Согласовано_организация = ((String.IsNullOrEmpty(org.ShortName)) ? ((String.IsNullOrEmpty(org.MiddleName)) ? org.Name : org.MiddleName) : org.ShortName),
                                   Научный_руководитель = x.Supervisor,
                                   Аккаунт_руководителя =x.SupervisorAccount,
                                   Кафедра = x.Chair,
                                   Тема_предложена = source.Name,
                                   Подразделение = fac.Name,
                                   Образовательная_программа = "[ " + op.Number + " ] " + op.Name,
                                   Направление = lp.Code + "  " + st.Name + "  " + lp.Name,
                                   Примечание = x.Comment,
                                   x.VKROPId,
                                   x.Id
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource1.DataSource = dt;
                    dgv.DataSource = bindingSource1;

                    List<string> Cols = new List<string>() { "Id", "VKROPId" };  //{ "Id", "ObrazProgramId" };

                    foreach (string s in Cols)
                        if (dgv.Columns.Contains(s))
                            dgv.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnDelStudent")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnEditStudent")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDiv")
                        {
                            col.HeaderText = "";
                        }
                    }
                    try
                    {
                        dgv.Columns["ColumnDiv"].Width = 6;
                        dgv.Columns["ColumnDelStudent"].Width = 70;
                        dgv.Columns["ColumnEditStudent"].Width = 100;
                        dgv.Columns["Студент"].Frozen = true;
                        dgv.Columns["Студент"].Width = 200;
                        dgv.Columns["Тема_ВКР"].Width = 300;
                        dgv.Columns["Тема_ВКР_англ"].Width = 300;
                        dgv.Columns["Студент"].Width = 200;
                        dgv.Columns["Согласовано_организация"].Width = 200;
                        dgv.Columns["Научный_руководитель"].Width = 200;
                        dgv.Columns["Подразделение"].Width = 200;
                        dgv.Columns["Направление"].Width = 200;
                        dgv.Columns["Образовательная_программа"].Width = 200;
                        dgv.Columns["Примечание"].Width = 200;
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
        private void FillStudentNewList()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    string sqlStudent = "SELECT * FROM StudentData ";
                    string sqlWhere = " ";
                    string sqlOrderBy = " order by FIO"; //" order by LastName, FirstName, SecondName";
                    if (Course.HasValue)
                    {
                        if (!String.IsNullOrEmpty(StudyingName))
                        {
                            sqlWhere = "where Course = " + Course.ToString() + " " + " and StudyingName = '" + StudyingName.ToString() + "' ";
                        }
                        else
                        {
                            sqlWhere = "where Course = " + Course.ToString() + " ";
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(StudyingName))
                        {
                            sqlWhere = "where StudyingName = '" + StudyingName.ToString() + "' ";
                        }
                        else
                        {
                            sqlWhere = " ";
                        }
                    }
                    //sqlWhere = sqlWhere + " and FacultyId in (select FacultyId from Practice where Id = " + _PId + ")";
                    string sqlStudentOP = " ObrazProgramInYearId in " +
                        "(select ObrazProgramInYear.ObrazProgramInYearId from VKROP inner join ObrazProgramInYear on VKROP.ObrazProgramInYearId = ObrazProgramInYear.Id " +
                        " where VKROP.Id = " + _Id.ToString() + ")";
                    //sqlWhere = sqlWhere + (checkBoxStudentOP.Checked ? sqlStudentOP : "");
                    if (sqlWhere == " ")
                    {
                        sqlWhere = " where ";
                    }
                    else
                    {
                        sqlWhere = sqlWhere + " and ";
                    }
                    sqlWhere = sqlWhere + sqlStudentOP;
                    sqlStudent = sqlStudent + sqlWhere + sqlOrderBy;

                    var StudentTable = context.Database.SqlQuery<StudentData>(sqlStudent);

                    var lst = (from x in StudentTable
                               select new
                               {
                                   Студент = x.FIO,  //stud.LastName + " " + stud.FirstName + " " + stud.SecondName,
                                   x.Id,
                                   x.StudDataId,
                                   x.LicenseProgramId,
                                   x.ObrazProgramId,
                                   x.FacultyId,
                                   Дата_рожд = x.DR,
                                   Аккаунт_студента = x.Accout,
                                   Курс = x.Course,
                                   Номер_УП = x.RegNomWP,
                                   Шифр_ОП = x.ObrazProgramCrypt,
                                   Статус = x.StatusName,
                                   Состояние = x.StudyingName,
                                   Форма_обуч = x.Department,
                                   Основа_обуч = x.StudyBasis,
                                   Уровень = x.DegreeName,
                                   Код_направления = x.SpecNumber,
                                   Направление_подготовки = x.SpecName,
                                   Направление = x.FacultyName,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource2.DataSource = dt;
                    dgvStudentNew.DataSource = bindingSource2;

                    List<string> Cols = new List<string>() { "Id", "StudDataId", "LicenseProgramId", "ObrazProgramId", "FacultyId" };

                    foreach (string s in Cols)
                        if (dgvStudentNew.Columns.Contains(s))
                            dgvStudentNew.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvStudentNew.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnAddStudent")
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
                        dgvStudentNew.Columns["ColumnDiv2"].Width = 6;
                        dgvStudentNew.Columns["ColumnAddStudent"].Width = 150;
                        dgvStudentNew.Columns["Студент"].Frozen = true;
                        dgvStudentNew.Columns["Студент"].Width = 200;
                        dgvStudentNew.Columns["Курс"].Width = 60;
                        dgvStudentNew.Columns["Статус"].Width = 60;
                        dgvStudentNew.Columns["Состояние"].Width = 60;
                        dgvStudentNew.Columns["Форма_обуч"].Width = 60;
                        dgvStudentNew.Columns["Основа_обуч"].Width = 60;
                        dgvStudentNew.Columns["Направление_подготовки"].Width = 300;
                        dgvStudentNew.Columns["Направление"].Width = 200;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось обработать данные...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void VKRStudent_Load(object sender, EventArgs e)
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

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            try
            {
                FillComboCourse();
                FillComboStudyingName();
                FillStudentNewList();
                dgvStudentNew.Visible = !dgvStudentNew.Visible;
                //lbl_dgvStudentNew.Visible = !lbl_dgvStudentNew.Visible;
                cbCourse.Visible = !cbCourse.Visible;
                lbl_cbCourse.Visible = !lbl_cbCourse.Visible;
                cbStudyingName.Visible = !cbStudyingName.Visible;
                Lbl_cbStudyingName.Visible = !Lbl_cbStudyingName.Visible;
                btnAddAllStudentToVKR.Visible = !btnAddAllStudentToVKR.Visible;
                btnStudentNewUpdate.Visible = !btnStudentNewUpdate.Visible;
                bindingNavigator2.Visible = !bindingNavigator2.Visible;
                btnAddStudent.Text = (dgvStudentNew.Visible) ? "Убрать добавление" : "Добавить студентов";
            }
            catch (Exception)
            {
                dgvStudentNew.Visible = false;
                //lbl_dgvStudentNew.Visible = false;
                cbCourse.Visible = false;
                lbl_cbCourse.Visible = false;
                btnAddAllStudentToVKR.Visible = false;
                btnStudentNewUpdate.Visible = false;
                bindingNavigator2.Visible = false;
                btnAddStudent.Text = "Добавить студентов";
            }
        }

        private void btnStudentNewUpdate_Click(object sender, EventArgs e)
        {
            FillComboCourse();
            FillComboStudyingName();
            FillStudentNewList();
        }

        private void cbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStudentNewList();
        }

        private void cbStudyingName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStudentNewList();
        }

        private void dgvStudentNew_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStudentNew.CurrentCell != null)
                if (dgvStudentNew.CurrentRow.Index >= 0)
                    if (dgvStudentNew.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            dgvStudentNew.CurrentCell = dgvStudentNew.CurrentRow.Cells["Студент"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        try
                        {
                            string FIO = (dgvStudentNew.CurrentRow.Cells["Студент"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Студент"].Value.ToString() : "";
                            string DR = (dgvStudentNew.CurrentRow.Cells["Дата_рожд"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Дата_рожд"].Value.ToString() : "";
                            int? StudDataId = null;
                            if (dgvStudentNew.CurrentRow.Cells["StudDataId"].Value != null)
                            {
                                StudDataId = int.Parse(dgvStudentNew.CurrentRow.Cells["StudDataId"].Value.ToString());
                            }

                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                var lst = (from x in context.VKROPStudent
                                           where (x.VKROPId == _Id) && (x.StudentFIO == FIO) && (x.DR == DR) && (x.StudDataId == StudDataId)
                                           select new
                                           {
                                               PrStId = x.Id,
                                           }).ToList().Count();
                                if (lst > 0)
                                {
                                    MessageBox.Show("Студент " + FIO + " (дата рожд. " + DR + ")" + "\r\n" + "уже находится в списке ВКР ", "Инфо",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            //добавление студента в список ВКР
                            string Account = (dgvStudentNew.CurrentRow.Cells["Аккаунт_студента"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Аккаунт_студента"].Value.ToString() : "";
                            string Course = (dgvStudentNew.CurrentRow.Cells["Курс"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Курс"].Value.ToString() : "";
                            string RegNomWP = (dgvStudentNew.CurrentRow.Cells["Номер_УП"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Номер_УП"].Value.ToString() : "";
                            string Department = (dgvStudentNew.CurrentRow.Cells["Форма_обуч"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Форма_обуч"].Value.ToString() : "";
                            string StudyBasis = (dgvStudentNew.CurrentRow.Cells["Основа_обуч"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Основа_обуч"].Value.ToString() : "";
                            string StatusName = (dgvStudentNew.CurrentRow.Cells["Статус"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Статус"].Value.ToString() : "";
                            string DegreeName = (dgvStudentNew.CurrentRow.Cells["Уровень"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Уровень"].Value.ToString() : "";
                            string SpecNumber = (dgvStudentNew.CurrentRow.Cells["Код_направления"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Код_направления"].Value.ToString() : "";
                            string SpecName = (dgvStudentNew.CurrentRow.Cells["Направление_подготовки"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Направление_подготовки"].Value.ToString() : "";
                            string FacultyName = (dgvStudentNew.CurrentRow.Cells["Направление"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Направление"].Value.ToString() : "";
                            string OPCrypt = (dgvStudentNew.CurrentRow.Cells["Шифр_ОП"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Шифр_ОП"].Value.ToString() : "";

                            int ObrazProgramId = OPId;
                            int LicenseProgramId = int.Parse(dgvStudentNew.CurrentRow.Cells["LicenseProgramId"].Value.ToString());
                            int FacultyId = int.Parse(dgvStudentNew.CurrentRow.Cells["FacultyId"].Value.ToString());

                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                VKROPStudent pst = new VKROPStudent();
                                pst.VKROPId = (int)_Id;
                                pst.StudDataId = StudDataId;
                                pst.StudentFIO = FIO;
                                pst.DR = DR;
                                pst.Accout = Account;
                                pst.Course = Course;
                                pst.RegNomWP = RegNomWP;
                                pst.Department = Department;
                                pst.StudyBasis = StudyBasis;
                                pst.StatusName = StatusName;
                                pst.DegreeName = DegreeName;
                                pst.SpecNumber = SpecNumber;
                                pst.SpecName = SpecName;
                                pst.FacultyName = FacultyName;
                                pst.ObrazProgramCrypt = OPCrypt;
                                pst.LicenseProgramId = LicenseProgramId;
                                pst.ObrazProgramId = ObrazProgramId;
                                pst.FacultyId = FacultyId;
                                context.VKROPStudent.Add(pst);
                                context.SaveChanges();
                                //FillGrid(_Id); 
                                FillGrid();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                string VKRName = "";
                try
                {
                    VKRName = "Образовательная программа: " + OP + "\r\n" + "Направление подготовки: " + LP;
                }
                catch (Exception)
                {
                }
                if (MessageBox.Show("Произвести удаление? \r\n" + VKRName, "Запрос на подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                try
                {
                    context.VKROP.Remove(context.VKROP.Where(x => x.Id == _Id).First());
                    context.SaveChanges();

                    //if (_hndl != null)
                    //    if (_PId.HasValue)
                    //        _hndl(_PId);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось удалить образовательную программу...\r\n" + "Обычно это связано с наличием связанных записей в других таблицах.\r\n" +
                        "Примечание: сначала необходимо удалить список ВКР по этой программе. \r\n" +
                        "Затем воспользоваться кнопкой «Удалить ОП из списка»\r\n" + "для окончательного удаления.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        private void btnAddAllStudentToVKR_Click(object sender, EventArgs e)
        {
            string VKRName = "";
            try
            {
                VKRName = "Образовательная программа: " + OP + "\r\n" + "Направление подготовки: " + LP;
            }
            catch (Exception)
            {
            }
            if (MessageBox.Show("Добавление в список ВКР всех студентов из справочника.\r\n" + VKRName + "\r\nБудут добавлены только те студенты,\r\n" +
                "которых еще нет в списке ВКР.\r\n" + "Продолжить выполнение?", "Запрос на подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            try
            {
                for (int rwInd = 0; rwInd < dgvStudentNew.Rows.Count; rwInd++)
                {
                    string FIO = (dgvStudentNew.Rows[rwInd].Cells["Студент"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Студент"].Value.ToString() : "";
                    string DR = (dgvStudentNew.Rows[rwInd].Cells["Дата_рожд"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Дата_рожд"].Value.ToString() : "";
                    int? StudDataId = null;
                    if (dgvStudentNew.Rows[rwInd].Cells["StudDataId"].Value != null)
                    {
                        StudDataId = int.Parse(dgvStudentNew.Rows[rwInd].Cells["StudDataId"].Value.ToString());
                    }
                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        var lst = (from x in context.VKROPStudent
                                   where (x.VKROPId == _Id) && (x.StudentFIO == FIO) && (x.DR == DR) && (x.StudDataId == StudDataId)
                                   select new
                                   {
                                       PrStId = x.Id,
                                   }).ToList().Count();
                        if (lst > 0)
                        {
                            //MessageBox.Show("Студент " + FIO + " (дата рожд. " + DR + ")" + "\r\n" + "уже находится в списке ВКР ", "Инфо",
                            //    MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //return;
                            continue;
                        }
                    }


                    //добавление студента в список ВКР
                    string Account = (dgvStudentNew.Rows[rwInd].Cells["Аккаунт_студента"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Аккаунт_студента"].Value.ToString() : "";
                    string Course = (dgvStudentNew.Rows[rwInd].Cells["Курс"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Курс"].Value.ToString() : "";
                    string RegNomWP = (dgvStudentNew.Rows[rwInd].Cells["Номер_УП"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Номер_УП"].Value.ToString() : "";
                    string Department = (dgvStudentNew.Rows[rwInd].Cells["Форма_обуч"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Форма_обуч"].Value.ToString() : "";
                    string StudyBasis = (dgvStudentNew.Rows[rwInd].Cells["Основа_обуч"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Основа_обуч"].Value.ToString() : "";
                    string StatusName = (dgvStudentNew.Rows[rwInd].Cells["Статус"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Статус"].Value.ToString() : "";
                    string DegreeName = (dgvStudentNew.Rows[rwInd].Cells["Уровень"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Уровень"].Value.ToString() : "";
                    string SpecNumber = (dgvStudentNew.Rows[rwInd].Cells["Код_направления"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Код_направления"].Value.ToString() : "";
                    string SpecName = (dgvStudentNew.Rows[rwInd].Cells["Направление_подготовки"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Направление_подготовки"].Value.ToString() : "";
                    string FacultyName = (dgvStudentNew.Rows[rwInd].Cells["Направление"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Направление"].Value.ToString() : "";
                    string OPCrypt = (dgvStudentNew.Rows[rwInd].Cells["Шифр_ОП"].Value != null) ? dgvStudentNew.Rows[rwInd].Cells["Шифр_ОП"].Value.ToString() : "";

                    int ObrazProgramId = OPId;
                    int LicenseProgramId = int.Parse(dgvStudentNew.Rows[rwInd].Cells["LicenseProgramId"].Value.ToString());
                    int FacultyId = int.Parse(dgvStudentNew.Rows[rwInd].Cells["FacultyId"].Value.ToString());

                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        VKROPStudent pst = new VKROPStudent();
                        pst.VKROPId = (int)_Id;
                        pst.StudDataId = StudDataId;
                        pst.StudentFIO = FIO;
                        pst.DR = DR;
                        pst.Accout = Account;
                        pst.Course = Course;
                        pst.RegNomWP = RegNomWP;
                        pst.Department = Department;
                        pst.StudyBasis = StudyBasis;
                        pst.StatusName = StatusName;
                        pst.DegreeName = DegreeName;
                        pst.SpecNumber = SpecNumber;
                        pst.SpecName = SpecName;
                        pst.FacultyName = FacultyName;
                        pst.ObrazProgramCrypt = OPCrypt;
                        pst.LicenseProgramId = LicenseProgramId;
                        pst.ObrazProgramId = ObrazProgramId;
                        pst.FacultyId = FacultyId;
                        context.VKROPStudent.Add(pst);
                        context.SaveChanges();
                        //FillGrid(_Id);
                        FillGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Операция не завершена.\r\n" + ex.Message, "Инфо");
                return;
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentRow.Index >= 0)
                {
                    if (dgv.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            dgv.CurrentCell = dgv.CurrentRow.Cells["Студент"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        int id;
                        try
                        {
                            id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                        }
                        catch (Exception)
                        {
                            return;
                        }

                        string StudentName = "";
                        try
                        {
                            StudentName = dgv.CurrentRow.Cells["Студент"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        if (StudentDelConfirm)
                            if (MessageBox.Show("Удалить выбранного студента из списка? \r\n" + StudentName, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                            { return; }
                        if (StudentShowDelConfirmSettings)
                        {
                            StudentShowDelConfirmSettings = false;
                            if (MessageBox.Show("По умолчанию перед удалением выводится \r\n" + "запрос на подтверждение. \r\n" +
                                "Это подтверждение можно отменить. \r\n" + "При каждом новом открытии данного окна \r\n" +
                                "восстанавливается вывод запроса на подтверждение. \r\n\r\n" + "Отменить вывод предупреждаещего сообщения?", "Запрос на подтверждение",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                            { StudentDelConfirm = false; }
                        }
                        try
                        {
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                context.VKROPStudent.RemoveRange(context.VKROPStudent.Where(x => x.Id == id));
                                context.SaveChanges();
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Не удалось удалить запись...\r\n" + "Обычно это связано с наличием связанных записей в других таблицах.", "Сообщение");
                        }
                        //FillGrid(_Id);
                        FillGrid();
                        if (dgvStudentNew.Visible == true)
                        {
                            //FillStudentNewList();
                        }
                        return;
                        //}
                        //else
                        //    return;
                    }
                    if (dgv.CurrentCell.ColumnIndex == 2)
                    {
                        try
                        {
                            dgv.CurrentCell = dgv.CurrentRow.Cells["Студент"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        try
                        {
                            int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                            FacName = dgv.CurrentRow.Cells["Подразделение"].Value.ToString();
                            if (Utilities.VKRStudentCardIsOpened(id))
                                return;
                            new VKRStudentCard(id, OP, LP, FacName, new UpdateVoidHandler(FillGrid)).Show();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                }
        }
    }
}
