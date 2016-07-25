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

namespace EmployerPartners
{
    public partial class PracticeCard : Form
    {
        #region Fields
        public string PracticeFaculty
        {
            get { return tbPracticeFaculty.Text.Trim(); }
            set { tbPracticeFaculty.Text = value; }
        }
        public string LP
        {
            get { return tbLP.Text.Trim(); }
            set { tbLP.Text = value; }
        }
        public int? PracticeTypeId
        {
            get { return ComboServ.GetComboIdInt(cbPracticeType); }
            set { ComboServ.SetComboId(cbPracticeType, value); }
        }
        public string DateStart
        {
            get { return tbDateStart.Text.Trim(); }
            set { tbDateStart.Text = value; }
        }
        public string DateEnd
        {
            get { return tbDateEnd.Text.Trim(); }
            set { tbDateEnd.Text = value; }
        }

        public string OrderDoc
        {
            get { return tbOrderDoc.Text.Trim(); }
            set { tbOrderDoc.Text = value; }
        }
        public string OrderNumber
        {
            get { return tbOrderNumber.Text.Trim(); }
            set { tbOrderNumber.Text = value; }
        }
        public string OrderDate
        {
            get { return tbOrderDate.Text.Trim(); }
            set { tbOrderDate.Text = value; }
        }
        public string OrderAuthor
        {
            get { return tbOrderAuthor.Text.Trim(); }
            set { tbOrderAuthor.Text = value; }
        }

        public string InstructionDoc
        {
            get { return tbInstructionDoc.Text.Trim(); }
            set { tbInstructionDoc.Text = value; }
        }
        public string InstructionNumber
        {
            get { return tbInstructionNumber.Text.Trim(); }
            set { tbInstructionNumber.Text = value; }
        }
        public string InstructionDate
        {
            get { return tbInstructionDate.Text.Trim(); }
            set { tbInstructionDate.Text = value; }
        }
        public string InstructionAuthor
        {
            get { return tbInstructionAuthor.Text.Trim(); }
            set { tbInstructionAuthor.Text = value; }
        }
        public string Comment
        {
            get { return tbComment.Text.Trim(); }
            set { tbComment.Text = value; }
        }
        public string Comment1
        {
            get { return tbComment1.Text.Trim(); }
            set { tbComment1.Text = value; }
        }
        public string Supervisor
        {
            get { return tbSupervisor.Text.Trim(); }
            set { tbSupervisor.Text = value; }
        }public string AdvanceHolder
        {
            get { return tbAdvanceHolder.Text.Trim(); }
            set { tbAdvanceHolder.Text = value; }
        }
        #endregion

        private int? _Id
        {
            get;
            set;
        }
        private int? _PId
        {
            get;
            set;
        }
        private int? _LPId
        {
            get;
            set;
        }

        public int? FacId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
        }
        public int? RubricId
        {
            get { return ComboServ.GetComboIdInt(cbRubric); }
            set { ComboServ.SetComboId(cbRubric, value); }
        }
        private int? Course
        {
            get { return ComboServ.GetComboIdInt(cbCourse); }
            set { ComboServ.SetComboId(cbCourse, value); }
        }
        private int? OrgStudent
        {
            get { return ComboServ.GetComboIdInt(cbOrgStudent); }
            set { ComboServ.SetComboId(cbOrgStudent, value); }
        }

        UpdateVoidHandler _hndl;

        public PracticeCard(int id, int pid, int lpid, string lp, UpdateVoidHandler _hdl)
        {
            InitializeComponent();
            _Id = id;
            _PId = pid;
            _LPId = lpid;
            LP = lp;
            _hndl = _hdl;
            FillCard();
            this.MdiParent = Util.mainform;
        }
        private void FillCard()
        {
            ComboServ.FillCombo(cbPracticeType, HelpClass.GetComboListByTable("dbo.PracticeType"), true, false);

            FillPractice();
            FillInfo();
            FillOPList();
            FillOP();
            FillOrg();
            FillStudent();
        }
        private void FillCombo()
        {
            ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("dbo.Faculty"), false, true);
            ComboServ.FillCombo(cbRubric, HelpClass.GetComboListByTable("dbo.Rubric"), false, true);
        }
        private void FillComboStudent()
        {
            ComboServ.FillCombo(cbCourse, HelpClass.GetComboListByQuery(@" select distinct  CONVERT(varchar(100), Student.Course) AS Id, CONVERT(varchar(100), Student.Course) as Name
                from dbo.Student order by Id"), false, true);
        }
        private void FillComboOrgStudent()
        {
            if (!_Id.HasValue)
            {
                return;
            }
            ComboServ.FillCombo(cbOrgStudent, HelpClass.GetComboListByQuery(@" SELECT CONVERT(varchar(100), dbo.PracticeLPOrganization.Id) AS Id, 
                         dbo.PracticeLPOrganization.OrganizationName +  ' [ ' + dbo.PracticeLPOrganization.OrganizationAddress + ' ]'  AS Name 
                FROM    dbo.PracticeLPOrganization INNER JOIN
                        dbo.Organization ON dbo.PracticeLPOrganization.OrganizationId = dbo.Organization.Id 
                WHERE   dbo.PracticeLPOrganization.PracticeLPId = " + _Id.ToString()), true, false);
        }

        private void FillPractice()
        {
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var Practice = (from x in context.PracticeLP
                                    join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                                    join p in context.Practice on x.PracticeId equals p.Id
                                    join fac in context.Faculty on p.FacultyId equals fac.Id
                                    where x.Id == _Id
                                    select fac).First();
                    PracticeFaculty = Practice.Name;
                }
        }

        private void FillInfo()
        {
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var Practice = (from x in context.PracticeLP
                                    where x.Id == _Id
                                    select x).First();
                    PracticeTypeId = Practice.PracticeTypeId;
                    DateStart = (Practice.DateStart.HasValue) ? Practice.DateStart.Value.Date.ToString("dd.MM.yyyy") : "";
                    DateEnd = (Practice.DateEnd.HasValue) ? Practice.DateEnd.Value.Date.ToString("dd.MM.yyyy") : "";
                    OrderDoc = Practice.OrderDoc;
                    OrderNumber = Practice.OrderNumber;
                    OrderDate = (Practice.OrderDate.HasValue) ? Practice.OrderDate.Value.Date.ToString("dd.MM.yyyy") : "";
                    OrderAuthor = Practice.OrderAuthor;
                    InstructionDoc = Practice.InstructionDoc;
                    InstructionNumber = Practice.InstructionNumber;
                    InstructionDate = (Practice.InstructionDate.HasValue) ? Practice.InstructionDate.Value.Date.ToString("dd.MM.yyyy") : "";
                    InstructionAuthor = Practice.InstructionAuthor;
                    Supervisor = Practice.Supervisor;
                    AdvanceHolder = Practice.AdvanceHolder;
                    Comment = Practice.Comment;
                    Comment1 = Practice.Comment;
                }
        }

        private void FillOPList()
        {
            if (!_Id.HasValue || !_LPId.HasValue)
            {
                return;
            }
            ComboServ.FillCombo(cbOP, HelpClass.GetComboListByQuery(@" SELECT CONVERT(varchar(100), dbo.ObrazProgram.Id) AS Id, 
                        '[ ' + dbo.ObrazProgram.Number + ' ] ' + dbo.ObrazProgram.Name +  ' [ ' + dbo.ProgramStatus.Name + ' ]'  AS Name 
                FROM    dbo.ObrazProgram INNER JOIN
                        dbo.ProgramStatus ON dbo.ObrazProgram.ProgramStatusId = dbo.ProgramStatus.Id 
                WHERE   dbo.ObrazProgram.LicenseProgramId = " + _LPId.ToString()), true, false);
        }

        private void FillOP()
        {
            if (!_Id.HasValue)
            {
                return;
            }
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.PracticeLPOP
                           join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                           join ps in context.ProgramStatus on op.ProgramStatusId equals ps.Id
                           where x.PracticeLPId == _Id
                           select new
                           {
                               Образовательная_программа = "[ " + op.Number + " ] " + op.Name, // + " [ " + ps.Name + " ]",
                               x.Id,
                               x.ObrazProgramId,
                               //Комментарий = x.Comment,
                           }).ToList();

                dgvOP.DataSource = lst;

                List<string> Cols = new List<string>() { "Id", "ObrazProgramId" };

                foreach (string s in Cols)
                    if (dgvOP.Columns.Contains(s))
                        dgvOP.Columns[s].Visible = false;
                foreach (DataGridViewColumn col in dgvOP.Columns)
                    col.HeaderText = col.Name.Replace("_", " ");
                try
                {
                    dgvOP.Columns["Образовательная_программа"].Frozen = true;
                    dgvOP.Columns["Образовательная_программа"].Width = 650;
                    //dgvOP.Columns["Комментарий"].Width = 200;
                }
                catch (Exception)
                {
                }
            }
        }
        private void FillOrg()
        {
            FillOrg(null);
        }
        private void FillOrg(int? id)
        {
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.PracticeLPOrganization
                               join org in context.Organization on x.OrganizationId equals org.Id
                               where x.PracticeLPId == _Id
                               orderby org.SPbGU descending, org.Name
                               select new
                               {
                                   Организация = org.Name,
                                   x.Id,
                                   x.OrganizationId,
                                   Начало_практики = x.DateStart,
                                   Окончание_практики = x.DateEnd,
                                   Адрес = x.OrganizationAddress,
                                   Организация_печать = x.OrganizationName,
                                   Комментарий = x.Comment,
                               }).ToList();

                    bindingSource1.DataSource = lst;
                    dgvOrg.DataSource = bindingSource1;

                    List<string> Cols = new List<string>() { "Id", "OrganizationId" };

                    foreach (string s in Cols)
                        if (dgvOrg.Columns.Contains(s))
                            dgvOrg.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvOrg.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnDelOrg")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnEditOrg")
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
                        dgvOrg.Columns["ColumnDiv1"].Width = 6;
                        dgvOrg.Columns["ColumnDelOrg"].Width = 70;
                        dgvOrg.Columns["ColumnEditOrg"].Width = 70;
                        dgvOrg.Columns["Организация"].Frozen = true;
                        dgvOrg.Columns["Организация"].Width = 300;
                        dgvOrg.Columns["Адрес"].Width = 400;
                        dgvOrg.Columns["Организация_печать"].Width = 400;
                        dgvOrg.Columns["Комментарий"].Width = 200;
                    }
                    catch (Exception)
                    {
                    }
                }
        }

        private void FillOrgNewList()
        {
            FillOrgNewList(null, null);
        }
        private void FillOrgNewList(int? FacId, int? RubricId)
        {
            try
            { 
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                string sqlOrg = "SELECT * FROM Organization ";
                string sqlWhere = " ";
                string sqlOrderBy = " order by SpbGU desc, Name";
                if (FacId.HasValue)
                {
                    if (RubricId.HasValue)
                    {
                        sqlWhere = "where Id in (select OrganizationId from OrganizationFaculty where FacultyId = " + FacId + "and RubricId = " + RubricId + ")";
                    }
                    else
                    {
                        sqlWhere = "where Id in (select OrganizationId from OrganizationFaculty where FacultyId = " + FacId + ")";
                    }
                }
                else
                {
                    if (RubricId.HasValue)
                    {
                        sqlWhere = "where Id in (select OrganizationId from OrganizationRubric where RubricId = " + RubricId + ")";
                    }
                    else
                    {

                    }
                }
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
                               org.Email,
                               Телефон = org.Phone,
                               Web_сайт = org.WebSite,
                               Город = org.City,
                               Улица = org.Street,
                               Дом = org.House,
                               Помещение = org.Apartment,
                               Комментарий = org.Comment,

                           }).ToList();

                bindingSource2.DataSource = lst;
                dgv.DataSource = bindingSource2;

                List<string> Cols = new List<string>() { "Id" };

                foreach (string s in Cols)
                    if (dgv.Columns.Contains(s))
                        dgv.Columns[s].Visible = false;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    col.HeaderText = col.Name.Replace("_", " ");
                    if (col.Name == "Column1")
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
                    dgv.Columns["Column1"].Width = 120;
                    dgv.Columns["Полное_наименование"].Frozen = true;
                    dgv.Columns["Полное_наименование"].Width = 300;
                    dgv.Columns["Среднее_наименование"].Width = 200;
                    dgv.Columns["Краткое_наименование"].Width = 150;
                    dgv.Columns["Наименование_англ"].Width = 150;
                    dgv.Columns["Краткое_наименование_англ"].Width = 150;
                }
                catch (Exception)
                {
                }
            }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось обработать данные...", "Сообщение");
            }
        }

        private void FillStudent()
        {
            FillStudent(null);
        }
        private void FillStudent(int? id)
        {
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.PracticeStudent
                               join stud in context.Student on x.StudentId equals stud.Id
                               join plporg in context.PracticeLPOrganization on x.PracticeLPOrganizationId equals plporg.Id into _plporg
                               from plporg in _plporg.DefaultIfEmpty()
                               join org in context.Organization on plporg.OrganizationId equals org.Id into _org
                               from org in _org.DefaultIfEmpty()
                               where (x.PracticeId == _PId ) && (stud.LicenseProgramId == _LPId)
                               orderby x.StudentFIO //stud.LastName, stud.FirstName
                               select new
                               {
                                   /*Студент = stud.LastName + (!String.IsNullOrEmpty(stud.FirstName) ? " " + stud.FirstName : "") +
                                                (!String.IsNullOrEmpty(stud.SecondName) ? " " + stud.SecondName : ""),*/
                                   //Студент = stud.LastName + " " + stud.FirstName + " " + stud.SecondName,
                                   Студент = x.StudentFIO,
                                   x.Id,
                                   x.StudentId,
                                   x.OrganizationId,
                                   Дата_рожд = stud.BirthDate, 
                                   Курс = stud.Course,
                                   Организация = org.Name,
                                   Начало_практики = plporg.DateStart,
                                   Окончание_практики = plporg.DateEnd,
                                   По_адресу = plporg.OrganizationAddress,
                                   Студент_ФИО_печать = x.StudentFIO,
                                   Комментарий = x.Comment,
                               }).ToList();

                    bindingSource3.DataSource = lst;
                    dgvStudent.DataSource = bindingSource3;

                    List<string> Cols = new List<string>() { "Id", "StudentId", "OrganizationId" };

                    foreach (string s in Cols)
                        if (dgvStudent.Columns.Contains(s))
                            dgvStudent.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvStudent.Columns)
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
                        if (col.Name == "ColumnSetOrgStudent")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDiv3")
                        {
                            col.HeaderText = "";
                        }
                    }
                    try
                    {
                        dgvStudent.Columns["ColumnDiv3"].Width = 6;
                        dgvStudent.Columns["ColumnDelStudent"].Width = 70;
                        dgvStudent.Columns["ColumnEditStudent"].Width = 70;
                        dgvStudent.Columns["Студент"].Frozen = true;
                        dgvStudent.Columns["Студент"].Width = 250;
                        dgvStudent.Columns["Курс"].Width = 65;
                        dgvStudent.Columns["Организация"].Width = 250;
                        dgvStudent.Columns["По_адресу"].Width = 300;
                        dgvStudent.Columns["Студент_ФИО_печать"].Width = 250;
                        dgvStudent.Columns["Комментарий"].Width = 200;
                    }
                    catch (Exception)
                    {
                    }
                }
        }

        private void FillStudentNewList()
        {
            FillStudentNewList(null);
        }
        private void FillStudentNewList(int? Course)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    string sqlStudent = "SELECT * FROM Student ";
                    string sqlWhere = " ";
                    string sqlOrderBy = " order by LastName, FirstName, SecondName";
                    if (Course.HasValue)
                    {
                        sqlWhere = "where LicenseProgramId = " + _LPId.ToString()  + " and Course = " + Course.ToString() + 
                            " and Id not in (select StudentId from PracticeStudent)" ;
                    }
                    else
                    {
                        sqlWhere = "where LicenseProgramId = " + _LPId.ToString() +
                            " and Id not in (select StudentId from PracticeStudent)"; 
                    }
                    sqlWhere = sqlWhere + " and FacultyId in (select FacultyId from Practice where Id = " + _PId +")";
                    sqlStudent = sqlStudent + sqlWhere + sqlOrderBy;

                    var StudentTable = context.Database.SqlQuery<Student>(sqlStudent);

                    var lst = (from stud in StudentTable
                               select new
                               {
                                   Студент = stud.LastName + " " + stud.FirstName + " " + stud.SecondName,
                                   stud.Id,
                                   stud.LicenseProgramId,
                                   Дата_рожд = stud.BirthDate,
                                   Курс = stud.Course,
                                   Комментарий = stud.Comment,
                               }).ToList();

                    bindingSource4.DataSource = lst;
                    dgvStudentNew.DataSource = bindingSource4;

                    List<string> Cols = new List<string>() { "Id", "LicenseProgramId" };

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
                        if (col.Name == "ColumnDiv4")
                        {
                            col.HeaderText = "";
                        }
                    }

                    try
                    {
                        dgvStudentNew.Columns["ColumnDiv4"].Width = 6;
                        dgvStudentNew.Columns["ColumnAddStudent"].Width = 120;
                        dgvStudentNew.Columns["Студент"].Frozen = true;
                        dgvStudentNew.Columns["Студент"].Width = 250;
                        dgvStudentNew.Columns["Комментарий"].Width = 200;
                        
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось обработать данные...", "Сообщение");
            }
        }

        private void btnAddOP_Click(object sender, EventArgs e)
        {
            int? OPId = ComboServ.GetComboIdInt(cbOP);
            if (!OPId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программа", "Инфо");
                return;
            }
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.PracticeLPOP
                               where x.PracticeLPId == _Id
                               && x.ObrazProgramId == OPId
                               select new
                               {
                                   LPOPId = x.Id,
                               }).ToList().Count();
                    if (lst > 0)
                    {
                        MessageBox.Show("Такая образовательная программа уже добавлена", "Инфо");
                        return;
                    }
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    PracticeLPOP p = new PracticeLPOP();
                    p.PracticeLPId = (int)_Id;
                    p.ObrazProgramId = (int)OPId;
                    context.PracticeLPOP.Add(p);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось добавить запись...\r\n" + ex.Message, "Сообщение");
                return;
            }
            FillOP();
        }

        private void btnDelOP_Click(object sender, EventArgs e)
        {
            if (dgvOP.CurrentCell != null)
                if (dgvOP.CurrentRow.Index >= 0)
                {
                    int id = int.Parse(dgvOP.CurrentRow.Cells["Id"].Value.ToString());
                    string sOP = "";
                    try
                    {
                        sOP = dgvOP.CurrentRow.Cells["Образовательная_программа"].Value.ToString();
                    }
                    catch (Exception)
                    {
                    }
                    if (MessageBox.Show("Удалить выбранную образовательную программу? \r\n" + sOP, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        try
                        {
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                context.PracticeLPOP.RemoveRange(context.PracticeLPOP.Where(x => x.Id == id));
                                context.SaveChanges();
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Не удалось удалить запись...", "Сообщение");
                        }
                        FillOP();
                    }
                    else
                        return;
                }
        }

        private void btnDelPractice_Click(object sender, EventArgs e)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                string PracticeName = "";
                try
                {
                    PracticeName = tbPracticeFaculty.Text + "\r\n" + tbLP.Text;
                }
                catch (Exception)
                {
                }
                if (MessageBox.Show("Удалить практику? \r\n" + PracticeName, "Запрос на подтверждение", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                    return;
                try
                {
                    context.PracticeLP.Remove(context.PracticeLP.Where(x => x.Id == _Id).First());
                    context.SaveChanges();
                    if (_hndl != null)
                        if (_PId.HasValue)
                            _hndl(_PId);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось удалить практику...\r\n" + "Обычно это связано с тем, что у данной записи имеются связанные записи в других таблицах.", "Сообщение");
                }

            }
        }

        private bool CheckFields()
        {
            DateTime res;
            if (!String.IsNullOrEmpty(DateStart))
            {
                if (!DateTime.TryParse(DateStart, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Начало практики'", "Инфо");
                    return false;
                }
            }
            if (!String.IsNullOrEmpty(DateEnd))
            {
                if (!DateTime.TryParse(DateEnd, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Окончание практики'", "Инфо");
                    return false;
                }
            }
            if (!String.IsNullOrEmpty(OrderDate))
            {
                if (!DateTime.TryParse(OrderDate, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Дата приказа'", "Инфо");
                    tabControl1.SelectedTab = tabPage1;
                    return false;
                }
            }
            if (!String.IsNullOrEmpty(InstructionDate))
            {
                if (!DateTime.TryParse(InstructionDate, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Дата распоряжения'", "Инфо");
                    tabControl1.SelectedTab = tabPage2;
                    return false;
                }
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
                return;
            if (!CheckFields())
            {
                return;
            }
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var plp = context.PracticeLP.Where(x => x.Id == _Id).First();
                    
                    plp.PracticeTypeId = PracticeTypeId;
                    if (!String.IsNullOrEmpty(DateStart))
                    { 
                        plp.DateStart = DateTime.Parse(DateStart);
                    }
                    else
                    {
                        plp.DateStart = null;
                    }
                    if (!String.IsNullOrEmpty(DateEnd))
                    {
                        plp.DateEnd = DateTime.Parse(DateEnd);
                    }
                    else
                    {
                        plp.DateEnd = null;
                    }

                    plp.OrderDoc = OrderDoc;
                    plp.OrderNumber = OrderNumber;
                    if (!String.IsNullOrEmpty(OrderDate))
                    {
                        plp.OrderDate = DateTime.Parse(OrderDate);
                    }
                    else
                    {
                        plp.OrderDate = null;
                    }
                    plp.OrderAuthor = OrderAuthor;

                    plp.InstructionDoc = InstructionDoc;
                    plp.InstructionNumber = InstructionNumber;
                    if (!String.IsNullOrEmpty(InstructionDate))
                    {
                        plp.InstructionDate = DateTime.Parse(InstructionDate);
                    }
                    else
                    {
                        plp.InstructionDate = null;
                    }
                    plp.InstructionAuthor = InstructionAuthor;

                    plp.Supervisor = Supervisor;
                    plp.AdvanceHolder = AdvanceHolder;

                    context.SaveChanges();

                    if (_hndl != null)
                        if (_PId.HasValue)
                        _hndl(_PId);

                    MessageBox.Show("Данные сохранены", "Сообщение");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + ex.Message, "Сообщение");
            }
        }

        private void btnMakeOrder_Click(object sender, EventArgs e)
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
            string templatename = "Приказ_практика";

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

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    string LPName;
                    var lp = (from x in context.LicenseProgram
                              where x.Id == _LPId
                              select x).First();
                    LPName = lp.Name;

                    wd.SetFields("LPName", LPName);
                    //FieldDoc field = wd.Fields["LPName"];
                    //field.Text = LPName;
                    
                    string DateStart_ ="";
                    string DateEnd_ = "";
                    string Supervisor_ = "";
                    string AdvanceHolder_ = "";
                    var plp = (from y in context.PracticeLP
                               where y.Id == _Id
                               select y).First();
                    DateStart_ = (plp.DateStart.HasValue) ? plp.DateStart.Value.Date.ToString("dd.MM.yyyy") : "";
                    DateEnd_ = (plp.DateEnd.HasValue) ? plp.DateEnd.Value.Date.ToString("dd.MM.yyyy") : "";
                    Supervisor_ = plp.Supervisor;
                    AdvanceHolder_ = plp.AdvanceHolder;
                    
                    wd.SetFields("DateStart", DateStart_);
                    wd.SetFields("DateEnd", DateEnd_);
                    wd.SetFields("Supervisor", Supervisor_);

                    string OrgList = "";
                    var orglist = (from x in context.PracticeLPOrganization
                               join prlp in context.PracticeLP on x.PracticeLPId equals prlp.Id 
                               join org in context.Organization on x.OrganizationId equals org.Id
                               where x.PracticeLPId == _Id
                               orderby org.SPbGU descending
                               select new
                               {
                                   org.Name,
                                   x.OrganizationId,
                                   prlp.DateStart,
                                   prlp.DateEnd,
                                   OrgDateStart = x.DateStart,
                                   OrgDateEnd = x.DateEnd,
                                   x.OrganizationAddress,
                                   x.OrganizationName,
                               }).ToList();
                    int i = 0;
                    foreach (var item in orglist)
                    {
                        i++;
                        if (((item.DateStart == item.OrgDateStart) && (item.DateEnd == item.OrgDateEnd)) || ((item.OrgDateStart == null) || (item.OrgDateEnd == null)))
                        {
                            OrgList = OrgList + "1." + i + "." + item.Name + " по адресу: " + item.OrganizationAddress + "; \r\n";
                        }
                        else
                        {
                            OrgList = OrgList + "1." + i + "." + " на период с " +
                                    ((item.OrgDateStart.HasValue) ? item.OrgDateStart.Value.Date.ToString("dd.MM.yyyy") : "") + " по " +
                                    ((item.OrgDateEnd.HasValue) ? item.OrgDateEnd.Value.Date.ToString("dd.MM.yyyy") : "") + " " + item.Name + " по адресу: " + item.OrganizationAddress + "; \r\n";
                        }
                    }
                    wd.SetFields("OrgList", OrgList);
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

        private void btnAddOrg_Click(object sender, EventArgs e)
        {
            try
            {
                FillCombo();
                FillOrgNewList(null, null);
                dgv.Visible = !dgv.Visible;
                lbl_dgv.Visible = !lbl_dgv.Visible;
                cbRubric.Visible = !cbRubric.Visible;
                lbl_cbRubric.Visible = !lbl_cbRubric.Visible;
                cbFaculty.Visible = !cbFaculty.Visible;
                lbl_cbFaculty.Visible = !lbl_cbFaculty.Visible;
                bindingNavigator2.Visible = !bindingNavigator2.Visible;
                btnAddOrg.Text = (dgv.Visible) ? "Убрать добавление" : "Добавить организации";
                //new PracticeOrg().Show();
            }
            catch (Exception)
            {
                dgv.Visible = false;
                lbl_dgv.Visible = false;
                cbRubric.Visible = false;
                lbl_cbRubric.Visible = false;
                cbFaculty.Visible = false;
                lbl_cbFaculty.Visible = false;
                bindingNavigator2.Visible = false;
                btnAddOrg.Text = "Добавить организации";
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*try
            {
                if (DateStart == "" || DateEnd == "")
                {
                    MessageBox.Show("Не введены данные в поля 'Начало практики' и 'Окончание практики'", "Сообщение");
                }
            }
            catch (Exception)
            {
            }*/
            
            if (dgv.CurrentCell != null)
                if (dgv.CurrentRow.Index >= 0)
                    if (dgv.CurrentCell.ColumnIndex == 1)
                    {
                        try 
	                    {	        
                            int Orgid = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                            string OrgName = (dgv.CurrentRow.Cells["Полное_наименование"].Value != null) ? dgv.CurrentRow.Cells["Полное_наименование"].Value.ToString() : "";
                            string OrgAddress = (dgv.CurrentRow.Cells["Город"].Value != null) ? dgv.CurrentRow.Cells["Город"].Value.ToString() : "";
                            OrgAddress += (dgv.CurrentRow.Cells["Улица"].Value != null) ? ((OrgAddress == "") ? dgv.CurrentRow.Cells["Улица"].Value.ToString() : ", " + dgv.CurrentRow.Cells["Улица"].Value.ToString()) : "";
                            OrgAddress += (dgv.CurrentRow.Cells["Дом"].Value != null) ? ((OrgAddress == "") ? dgv.CurrentRow.Cells["Дом"].Value.ToString() : ", " + dgv.CurrentRow.Cells["Дом"].Value.ToString()) : "";
                            OrgAddress += (dgv.CurrentRow.Cells["Помещение"].Value != null) ? ((OrgAddress == "") ? dgv.CurrentRow.Cells["Помещение"].Value.ToString() : ", " + dgv.CurrentRow.Cells["Помещение"].Value.ToString()) : "";    
                            DateTime res;

                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                PracticeLPOrganization org = new PracticeLPOrganization();
                                org.OrganizationId = Orgid;
                                org.PracticeLPId = (int)_Id;
                                org.OrganizationName = OrgName;
                                org.OrganizationAddress = OrgAddress;
                                if (!String.IsNullOrEmpty(DateStart))
                                    if (DateTime.TryParse(DateStart, out res))
                                        org.DateStart = DateTime.Parse(DateStart);
                                if (!String.IsNullOrEmpty(DateEnd))
                                    if (DateTime.TryParse(DateEnd, out res))
                                        org.DateEnd = DateTime.Parse(DateStart);

                                context.PracticeLPOrganization.Add(org);
                                context.SaveChanges();
                                FillOrg();
                            }
                        }
	                    catch (Exception ex)
	                    {
		                    MessageBox.Show(ex.Message);
                            return;
	                    }
                    }
        }

        private void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillOrgNewList(FacId, RubricId);
        }

        private void cbRubric_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillOrgNewList(FacId, RubricId);
        }

        private void dgvOrg_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOrg.CurrentCell != null)
                if (dgvOrg.CurrentRow.Index >= 0)
                {
                    if (dgvOrg.CurrentCell.ColumnIndex == 1)
                    {
                        int id = int.Parse(dgvOrg.CurrentRow.Cells["Id"].Value.ToString());
                        string OrgName = "";
                        try
                        {
                            OrgName = dgvOrg.CurrentRow.Cells["Организация"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        if (MessageBox.Show("Удалить выбранную организацию из списка? \r\n" + OrgName, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            try
                            {
                                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                                {
                                    context.PracticeLPOrganization.RemoveRange(context.PracticeLPOrganization.Where(x => x.Id == id));
                                    context.SaveChanges();
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Не удалось удалить запись...\r\n" + "Обычно это связано с наличием связанных записей в других таблицах.", "Сообщение");
                            }
                            FillOrg();
                            return;
                        }
                        else
                            return;
                    }
                    if (dgvOrg.CurrentCell.ColumnIndex == 2)
                    {
                        int id = int.Parse(dgvOrg.CurrentRow.Cells["Id"].Value.ToString());
                        new PracticeOrgCard(id, new UpdateVoidHandler(FillOrg)).Show();
                    }
                }
        }

        private void btnOrgEdit_Click(object sender, EventArgs e)
        {
            if (dgvOrg.CurrentCell != null)
                if (dgvOrg.CurrentCell.RowIndex >= 0)
                {
                    int id = int.Parse(dgvOrg.CurrentRow.Cells["Id"].Value.ToString());
                    new PracticeOrgCard(id, new UpdateVoidHandler(FillOrg)).Show();
                }
        }

        private void btnMakeInstruction_Click(object sender, EventArgs e)
        {
            ToDOCInstruction();
        }
        public void ToDOCInstruction()
        {
            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "Распоряжение_практика";

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

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    string LPName;
                    string LPCode;
                    var lp = (from x in context.LicenseProgram
                              where x.Id == _LPId
                              select x).First();
                    LPName = lp.Name;
                    LPCode = lp.Code;

                    wd.SetFields("LPName", LPName);
                    wd.SetFields("LPCode", LPCode);
                    //FieldDoc field = wd.Fields["LPName"];
                    //field.Text = LPName;

                    string DateStart_ = "";
                    string DateEnd_ = "";
                    string Supervisor_ = "";
                    string AdvanceHolder_ = "";
                    var plp = (from y in context.PracticeLP
                               where y.Id == _Id
                               select y).First();
                    DateStart_ = (plp.DateStart.HasValue) ? plp.DateStart.Value.Date.ToString("dd.MM.yyyy") : "";
                    DateEnd_ = (plp.DateEnd.HasValue) ? plp.DateEnd.Value.Date.ToString("dd.MM.yyyy") : "";
                    Supervisor_ = plp.Supervisor;
                    AdvanceHolder_ = plp.AdvanceHolder;

                    wd.SetFields("DateStart", DateStart_);
                    wd.SetFields("DateEnd", DateEnd_);

                    string OrgStudentList = "";
                    var orgstudentlist = (from x in context.PracticeStudent
                                          //join stud in context.Student on x.StudentId equals stud.Id
                                          join plporg in context.PracticeLPOrganization on x.PracticeLPOrganizationId equals plporg.Id 
                                          join prlp in context.PracticeLP on plporg.PracticeLPId equals prlp.Id 
                                          join org in context.Organization on plporg.OrganizationId equals org.Id 
                                          where plporg.PracticeLPId == _Id
                                          orderby org.SPbGU descending, org.Name, plporg.OrganizationAddress, x.StudentFIO //stud.LastName, stud.FirstName
                                          select new
                                          {
                                              //FIO = stud.LastName + " " + stud.FirstName + " " + stud.SecondName,
                                              FIO = x.StudentFIO,
                                              prlp.DateStart,
                                              prlp.DateEnd,
                                              OrgDateStart = plporg.DateStart,
                                              OrgDateEnd = plporg.DateEnd,
                                              LPOrgId = plporg.Id,
                                              plporg.OrganizationAddress,
                                              plporg.OrganizationName,
                                          }).ToList();
                    int LPOrgId = 0;
                    int i = 0;
                    int j = 0;
                    foreach (var item in orgstudentlist)
                    {
                        if (item.LPOrgId != LPOrgId)
                        {
                            LPOrgId = item.LPOrgId;
                            i++;
                            j = 0;
                            if (((item.DateStart == item.OrgDateStart) && (item.DateEnd == item.OrgDateEnd)) || ((item.OrgDateStart == null) || (item.OrgDateEnd == null)))
                            {
                                OrgStudentList = OrgStudentList + "\r\n" + "1." + i + "." + " в " + item.OrganizationName + " по адресу: " + item.OrganizationAddress + ":\r\n";
                            }
                            else
                            {
                                OrgStudentList = OrgStudentList + "\r\n" + "1." + i + "." + " на период с " +
                                    ((item.OrgDateStart.HasValue) ? item.OrgDateStart.Value.Date.ToString("dd.MM.yyyy") : "") +" по " + 
                                    ((item.OrgDateEnd.HasValue) ? item.OrgDateEnd.Value.Date.ToString("dd.MM.yyyy") : "") + 
                                    " в " + item.OrganizationName + " по адресу: " + item.OrganizationAddress + ":\r\n";
                            }
                        }
                        j++;
                        OrgStudentList = OrgStudentList + "1." + i + "." + j + "." + item.FIO + "\r\n";
                        
                    }
                    wd.SetFields("OrgStudentList", OrgStudentList);
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

        private void btnOrgUpdate_Click(object sender, EventArgs e)
        {
            FillOrg();
        }

        private void btnStudentUpdate_Click(object sender, EventArgs e)
        {
            FillStudent();
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            try
            {
                FillComboStudent();
                FillStudentNewList(null);
                cbOrgStudent.Visible = false;
                lbl_cbOrgStudent.Visible = false;
                dgvStudentNew.Visible = !dgvStudentNew.Visible;
                lbl_dgvStudentNew.Visible = !lbl_dgvStudentNew.Visible;
                cbCourse.Visible = !cbCourse.Visible;
                lbl_cbCourse.Visible = !lbl_cbCourse.Visible;
                bindingNavigator4.Visible = !bindingNavigator4.Visible;
                btnAddStudent.Text = (dgvStudentNew.Visible) ? "Убрать добавление" : "Добавить студентов";
                btnSetOrgStudent.Text = (cbOrgStudent.Visible) ? "Убрать распределение студентов по орг-циям" : "Распределение студентов по организациям";
                //new PracticeOrg().Show();
            }
            catch (Exception)
            {
                dgvStudentNew.Visible = false;
                lbl_dgvStudentNew.Visible = false;
                cbCourse.Visible = false;
                lbl_cbCourse.Visible = false;
                bindingNavigator4.Visible = false;
                btnAddStudent.Text = "Добавить организации";
            }
        }

        private void cbCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillStudentNewList(Course);
        }

        private void dgvStudentNew_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStudentNew.CurrentCell != null)
                if (dgvStudentNew.CurrentRow.Index >= 0)
                    if (dgvStudentNew.CurrentCell.ColumnIndex == 1)
                    {
                        

                        try
                        {
                            int StudentId = int.Parse(dgvStudentNew.CurrentRow.Cells["Id"].Value.ToString());
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                var lst = (from x in context.PracticeStudent
                                           where x.PracticeId == _PId
                                           && x.StudentId == StudentId
                                           select new
                                           {
                                               PrStId = x.Id,
                                           }).ToList().Count();
                                if (lst > 0)
                                {
                                    MessageBox.Show("Студент уже добавлен в список практики", "Инфо");
                                    return;
                                }
                            }

                            string FIO = (dgvStudentNew.CurrentRow.Cells["Студент"].Value != null) ? dgvStudentNew.CurrentRow.Cells["Студент"].Value.ToString() : "";

                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                PracticeStudent pst = new PracticeStudent();
                                pst.StudentId = StudentId;
                                pst.PracticeId = (int)_PId;
                                pst.StudentFIO = FIO;
                                context.PracticeStudent.Add(pst);
                                context.SaveChanges();
                                FillStudent();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStudent.CurrentCell != null)
                if (dgvStudent.CurrentRow.Index >= 0)
                {
                    if (dgvStudent.CurrentCell.ColumnIndex == 1)
                    {
                        int id = int.Parse(dgvStudent.CurrentRow.Cells["Id"].Value.ToString());
                        string StudentName = "";
                        try
                        {
                            StudentName = dgvStudent.CurrentRow.Cells["Студент"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        if (MessageBox.Show("Удалить выбранного студента из списка? \r\n" + StudentName, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            try
                            {
                                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                                {
                                    context.PracticeStudent.RemoveRange(context.PracticeStudent.Where(x => x.Id == id));
                                    context.SaveChanges();
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Не удалось удалить запись...\r\n" + "Обычно это связано с наличием связанных записей в других таблицах.", "Сообщение");
                            }
                            FillStudent();
                            if (dgvStudentNew.Visible == true)
                            {
                                FillStudentNewList();
                            }
                            return;
                        }
                        else
                            return;
                    }
                    if (dgvStudent.CurrentCell.ColumnIndex == 2)
                    {
                        int id = int.Parse(dgvStudent.CurrentRow.Cells["Id"].Value.ToString());
                        new PracticeStudentCard(id, new UpdateVoidHandler(FillStudent)).Show();
                    }
                    if (dgvStudent.CurrentCell.ColumnIndex == 3)
                    {
                        if (cbOrgStudent.Visible == false)
                        {
                            MessageBox.Show("Не выбрана организация. \r\n" + "Воспользуйтесь кнопкой 'Распределение студентов по организациям'","Сообщение");
                            return;
                        }
                        //if (!OrgStudent.HasValue)
                        //{
                        //    MessageBox.Show("Не выбрана организация","Сообщение");
                        //    //cbOrgStudent.Focus();
                        //    return;
                        //}
                        try
                        {
                            int id = int.Parse(dgvStudent.CurrentRow.Cells["Id"].Value.ToString());
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                var pst = context.PracticeStudent.Where(x => x.Id == id).First();

                                //pst.OrganizationId = OrgStudent;
                                pst.PracticeLPOrganizationId = OrgStudent;
                            
                                context.SaveChanges();
                            
                                FillStudent();
                            }
                        }
                        catch (Exception ec)
                        {
                            MessageBox.Show("Не удалось обновить данные \r\n" + ec.Message, "Сообщение");
                        }
                    }
                }
        }

        private void btnSetOrgStudent_Click(object sender, EventArgs e)
        {
            try
            {
                FillComboOrgStudent();
                cbOrgStudent.Visible = !cbOrgStudent.Visible;
                lbl_cbOrgStudent.Visible = !lbl_cbOrgStudent.Visible;
                btnSetOrgStudent.Text = (cbOrgStudent.Visible) ? "Убрать распределение студентов по орг-циям" : "Распределение студентов по организациям";
            }
            catch (Exception)
            {
                cbOrgStudent.Visible = false;
                lbl_cbOrgStudent.Visible = false;
                btnSetOrgStudent.Text = "Распределение студентов по организациям";
            }
        }

        private void PracticeCard_Load(object sender, EventArgs e)
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
