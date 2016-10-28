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
    public partial class PracticeStudentDopList : Form
    {
        private string PracticeFaculty
        {
            get { return tbPracticeFaculty.Text.Trim(); }
            set { tbPracticeFaculty.Text = value; }
        }
        private string LP
        {
            get { return tbLP.Text.Trim(); }
            set { tbLP.Text = value; }
        }
        private string OP
        {
            get { return tbOP.Text.Trim(); }
            set { tbOP.Text = value; }
        }
        private string OPCrypt
        {
            get { return tbOPCrypt.Text.Trim(); }
            set { tbOPCrypt.Text = value; }
        }
        private int? _PLPId
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
        private int? _FacId
        {
            get;
            set;
        }

        UpdateVoidHandler _hndl;

        public PracticeStudentDopList(int plpid, int pid, int lpid, string lp, UpdateVoidHandler _hdl)
        {
            InitializeComponent();
            _PLPId = plpid;
            _PId = pid;
            _LPId = lpid;
            LP = lp;
            _hndl = _hdl;
            FillCard();
            FillGrid();
            this.MdiParent = Util.mainform;
        }
        private void FillCard()
        {
            try
            {
                if (_PLPId.HasValue)
                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        //Практика (подразделение)
                        var Practice = (from x in context.PracticeLP
                                        join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                                        join p in context.Practice on x.PracticeId equals p.Id
                                        join fac in context.Faculty on p.FacultyId equals fac.Id
                                        where x.Id == _PLPId
                                        select fac).First();
                        PracticeFaculty = Practice.Name;
                        _FacId = Practice.Id;

                        //Образовательная программа
                        var opn = (from x in context.PracticeLPOP
                                   join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                                   join opinyear in context.ObrazProgramInYear on x.ObrazProgramInYearId equals opinyear.Id
                                   where x.PracticeLPId == _PLPId
                                   select new
                                   {
                                       //OP = " " + op.Number + "  " + op.Name,
                                       op.Name,
                                       opinyear.ObrazProgramCrypt,
                                   }).ToList();
                        if (opn.Count > 0)
                        {
                            OP = opn.First().Name;
                            OPCrypt = (String.IsNullOrEmpty(opn.First().ObrazProgramCrypt)) ? "" : opn.First().ObrazProgramCrypt;
                        }
                    }
            }
            catch (Exception)
            {
            }
        }
        private void FillGrid()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    string sqlStudent = "SELECT * FROM StudentDVZ where StudDataId not in (select StudDataId from StudentData) order by FIO";
                    var StudentTable = context.Database.SqlQuery<StudentDVZ>(sqlStudent);
                    if (StudentTable.Count() == 0)
                    {
                        MessageBox.Show("В дополнительный список входят все студенты, которых не удалось однозначно идентифицировать. " +
                            "Причины: отсутствие номера учебного плана, либо этот номер не существует в основной базе данных учебных планов. \r\n" +
                            "В настоящий момент этот список пуст.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var lst = (from x in StudentTable
                               select new
                               {
                                   Студент = x.FIO,  //stud.LastName + " " + stud.FirstName + " " + stud.SecondName,
                                   x.Id,
                                   x.StudDataId,
                                   Дата_рожд = x.DR,
                                   Курс = x.Course,
                                   Номер_УП = x.RegNomWP,
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
                    bindingSource1.DataSource = dt;
                    dgv.DataSource = bindingSource1;

                    List<string> Cols = new List<string>() { "Id", "StudDataId" };

                    foreach (string s in Cols)
                        if (dgv.Columns.Contains(s))
                            dgv.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnAddStudent")
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
                        dgv.Columns["ColumnAddStudent"].Width = 120;
                        dgv.Columns["Студент"].Frozen = true;
                        dgv.Columns["Студент"].Width = 200;
                        dgv.Columns["Курс"].Width = 60;
                        dgv.Columns["Статус"].Width = 60;
                        dgv.Columns["Состояние"].Width = 60;
                        dgv.Columns["Форма_обуч"].Width = 60;
                        dgv.Columns["Основа_обуч"].Width = 60;
                        dgv.Columns["Направление_подготовки"].Width = 300;
                        dgv.Columns["Направление"].Width = 200;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось обработать данные...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch
            {
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentRow.Index >= 0)
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
                        try
                        {
                            string FIO = (dgv.CurrentRow.Cells["Студент"].Value != null) ? dgv.CurrentRow.Cells["Студент"].Value.ToString() : "";
                            string DR = (dgv.CurrentRow.Cells["Дата_рожд"].Value != null) ? dgv.CurrentRow.Cells["Дата_рожд"].Value.ToString() : "";
                            int? StudDataId = null;
                            if (dgv.CurrentRow.Cells["StudDataId"].Value != null)
                            {
                                StudDataId = int.Parse(dgv.CurrentRow.Cells["StudDataId"].Value.ToString());
                            }

                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                var lst = (from x in context.PracticeLPStudent
                                           where (x.PracticeLPId == _PLPId) && (x.StudentFIO == FIO) && (x.DR == DR) && (x.StudDataId == StudDataId)
                                           select new
                                           {
                                               PrStId = x.Id,
                                           }).ToList().Count();
                                if (lst > 0)
                                {
                                    MessageBox.Show("Студент " + FIO + " (дата рожд. " + DR + ")" + "\r\n" + "уже находится в списке на практику ", "Инфо",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            //добавление студента в практику
                            string Course = (dgv.CurrentRow.Cells["Курс"].Value != null) ? dgv.CurrentRow.Cells["Курс"].Value.ToString() : "";
                            string RegNomWP = (dgv.CurrentRow.Cells["Номер_УП"].Value != null) ? dgv.CurrentRow.Cells["Номер_УП"].Value.ToString() : "";
                            string Department = (dgv.CurrentRow.Cells["Форма_обуч"].Value != null) ? dgv.CurrentRow.Cells["Форма_обуч"].Value.ToString() : "";
                            string StudyBasis = (dgv.CurrentRow.Cells["Основа_обуч"].Value != null) ? dgv.CurrentRow.Cells["Основа_обуч"].Value.ToString() : "";
                            string StatusName = (dgv.CurrentRow.Cells["Статус"].Value != null) ? dgv.CurrentRow.Cells["Статус"].Value.ToString() : "";
                            string DegreeName = (dgv.CurrentRow.Cells["Уровень"].Value != null) ? dgv.CurrentRow.Cells["Уровень"].Value.ToString() : "";
                            string SpecNumber = (dgv.CurrentRow.Cells["Код_направления"].Value != null) ? dgv.CurrentRow.Cells["Код_направления"].Value.ToString() : "";
                            string SpecName = (dgv.CurrentRow.Cells["Направление_подготовки"].Value != null) ? dgv.CurrentRow.Cells["Направление_подготовки"].Value.ToString() : "";
                            string FacultyName = (dgv.CurrentRow.Cells["Направление"].Value != null) ? dgv.CurrentRow.Cells["Направление"].Value.ToString() : "";
                            //string OPCrypt = (dgv.CurrentRow.Cells["Шифр_ОП"].Value != null) ? dgv.CurrentRow.Cells["Шифр_ОП"].Value.ToString() : "";
                            //string ObrazProgramCrypt = OPCrypt;

                            //int LicenseProgramId = int.Parse(dgv.CurrentRow.Cells["LicenseProgramId"].Value.ToString());
                            //int FacultyId = int.Parse(dgv.CurrentRow.Cells["FacultyId"].Value.ToString());

                            int LicenseProgramId = (int)_LPId;
                            int FacultyId = (int)_FacId;

                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                PracticeLPStudent pst = new PracticeLPStudent();
                                pst.PracticeLPId = (int)_PLPId;
                                pst.StudDataId = StudDataId;
                                pst.StudentFIO = FIO;
                                pst.DR = DR;
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
                                pst.FacultyId = FacultyId;
                                context.PracticeLPStudent.Add(pst);
                                context.SaveChanges();

                                if (_hndl != null)
                                    _hndl(_PLPId);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
        }

        private void PracticeStudentDopList_Load(object sender, EventArgs e)
        {
            try
            {
                //if (this.Parent.Width > this.Width + 30 + this.Left)
                //{
                //    this.Width = this.Parent.Width - 30 - this.Left;
                //}
                //if (this.Parent.Height > this.Height + 30 + this.Top)
                //{
                //    this.Height = this.Parent.Height - 30 - this.Top;
                //}
                if (this.Parent.Width > this.Width + 30 + this.Left + 400)
                {
                    this.Width = this.Parent.Width - 30 - this.Left - 400;
                    this.Left = this.Left + 400;
                }
                if (this.Parent.Height > this.Height + 30 + this.Top + 100)
                {
                    this.Height = this.Parent.Height - 30 - this.Top - 100;
                    this.Top = this.Top + 100;
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
