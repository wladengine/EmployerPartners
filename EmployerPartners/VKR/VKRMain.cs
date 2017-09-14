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
    public partial class VKRMain : Form
    {
        public int? _id;

        public int? _VKRId
        {
            get { return ComboServ.GetComboIdInt(cbVKRYear); }
            set { ComboServ.SetComboId(cbVKRYear, value); }
        }
        public int? VKRYear
        {
            get { return ComboServ.GetComboIdInt(cbVKRYear); }
            set { ComboServ.SetComboId(cbVKRYear, value); }
        }

        public int? FacultyId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
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
        public int? OPId
        {
            get { return ComboServ.GetComboIdInt(cbOP); }
            set { ComboServ.SetComboId(cbOP, value); }
        }

        public VKRMain()
        {
            InitializeComponent();
            FillVKRYear();
            FillFacultyList();
            FillStudyLevel();
            FillLPList();
            FillGrid(_VKRId);
            this.MdiParent = Util.mainform;
            SetAccessRight();
        }
        private void SetAccessRight()
        {
            if (Util.IsVKRWrite())
            {
                btnAddOP.Enabled = true;
            }
            if (Util.IsDBOwner() || Util.IsAdministrator())
            {
                checkBoxArchive.Enabled = true;
            }
            else
            {
                checkBoxArchive.Enabled = false;
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
                        btnAddOP.Enabled = false;
                    }
                    else
                    {
                        lblArchive.Visible = false;
                        btnAddOP.Enabled = true;
                    }
                }
            }
            catch (Exception)
            {
                lblArchive.Visible = false;
                btnAddOP.Enabled = true;
            }
        }
        private void FillVKRYear()
        {
            ComboServ.FillCombo(cbVKRYear, HelpClass.GetComboListByQuery(@" SELECT DISTINCT CONVERT(varchar(100), Id) AS Id, 
                CONVERT(varchar(100), StudyYear) AS Name 
                FROM VKR ORDER BY Name DESC "), false, false);
        }
        private void FillFacultyList()
        {
            ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("dbo.Faculty"), false, true);
        }
        private void FillStudyLevel()
        {
            ComboServ.FillCombo(cbLevel, HelpClass.GetComboListByTable("dbo.StudyLevel"), false, true);
        }
        private void FillLPList()
        {
            FillLPList(null);
        }
        private void FillLPList(int? id)
        {
            string Faculty = (FacultyId.HasValue) ? (" WHERE FacultyId = " + FacultyId.ToString()) : "";
            string Level = (StudyLevelId.HasValue) ? (" StudyLevelId = " + StudyLevelId.ToString()) : "";
            string Where = (id.HasValue) ? (" WHERE   (dbo.LicenseProgram.Id in (SELECT LicenseProgramId FROM ObrazProgram " + Faculty + ") " +
                                                    "OR dbo.LicenseProgram.Id in (SELECT SecondLicenseProgramId FROM ObrazProgram " + Faculty + ")) ") : " ";
            if (StudyLevelId.HasValue)
            {
                if (id.HasValue)
                {
                    Where += " AND " + Level;
                }
                else
                {
                    Where += " WHERE " + Level;
                }
            }

            ComboServ.FillCombo(cbLP, HelpClass.GetComboListByQuery(@" SELECT CONVERT(varchar(100), dbo.LicenseProgram.Id) AS Id, 
                CASE ISNULL(dbo.LicenseProgram.Code, N'""') 
                        WHEN '""' THEN '""' ELSE dbo.LicenseProgram.Code + N' ' END + dbo.StudyLevel.Name + N' ' + dbo.LicenseProgram.Name + 
                        N' [' + dbo.ProgramType.Name + N']' + N' [' + CASE ISNULL(dbo.Qualification.Name, N'""') WHEN '""' THEN '""' ELSE dbo.Qualification.Name + N' ' END +
                         N']' AS Name
                FROM    dbo.LicenseProgram INNER JOIN
                        dbo.ProgramType ON dbo.LicenseProgram.ProgramTypeId = dbo.ProgramType.Id INNER JOIN
                        dbo.StudyLevel ON dbo.LicenseProgram.StudyLevelId = dbo.StudyLevel.Id LEFT OUTER JOIN
                        dbo.Qualification ON dbo.LicenseProgram.QualificationId = dbo.Qualification.Id "
                + Where +
                " ORDER BY dbo.LicenseProgram.Code, dbo.StudyLevel.Name, dbo.ProgramType.Id"), false, true);
        }
        private void FillOPList()
        {
            string LP = (LPId.HasValue) ? (" dbo.ObrazProgram.LicenseProgramId = " + LPId.ToString() + " or dbo.ObrazProgram.SecondLicenseProgramId = " + LPId.ToString()) : "";
            string Where = (LPId.HasValue) ? (" where " + LP) : "";

            ComboServ.FillCombo(cbOP, HelpClass.GetComboListByQuery(@" SELECT CONVERT(varchar(100), dbo.ObrazProgramInYear.Id) AS Id, 
                        '[ ' + dbo.ObrazProgram.Number + ' ] ' + dbo.ObrazProgram.Name + ' [ ' + dbo.ObrazProgramInYear.Year + ' ]' +  
                        ' [ Шифр: ' + dbo.ObrazProgramInYear.ObrazProgramCrypt + ' ]' + ' [ ' + dbo.ProgramStatus.Name + ' ]' + ' [ ' + dbo.ProgramMode.Name + ' ]'  AS Name 
                FROM    dbo.ObrazProgramInYear INNER JOIN 
                        dbo.ObrazProgram on dbo.ObrazProgramInYear.ObrazProgramId = dbo.ObrazProgram.Id 
                        INNER JOIN dbo.ProgramStatus ON dbo.ObrazProgram.ProgramStatusId = dbo.ProgramStatus.Id 
                        INNER JOIN dbo.ProgramMode ON dbo.ObrazProgram.ProgramModeId = dbo.ProgramMode.Id "
                + Where + 
                " ORDER BY dbo.ObrazProgram.Name, dbo.ObrazProgramInYear.Year DESC"), true, false);
        }
        //private void FillGrid()
        //{
        //    FillGrid(null);
        //}
        private void FillGrid(int? id)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKROP
                               join vkr in context.VKR on x.VKRId equals vkr.Id
                               join opyear in context.ObrazProgramInYear on x.ObrazProgramInYearId equals opyear.Id
                               join op in context.ObrazProgram on opyear.ObrazProgramId equals op.Id
                               //join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               //join ps in context.ProgramStatus on op.ProgramStatusId equals ps.Id
                               join pm in context.ProgramMode on op.ProgramModeId equals pm.Id
                               join lp in context.LicenseProgram on op.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               join fac in context.Faculty on op.FacultyId equals fac.Id
                               where (x.VKRId == id) && (FacultyId.HasValue ? op.FacultyId == FacultyId : true) && (StudyLevelId.HasValue ? lp.StudyLevelId == StudyLevelId : true)
                                    && (LPId.HasValue ? op.LicenseProgramId == LPId : true)
                               select new
                               {
                                   Образовательная_программа = "[ " + fac.Name + " ] " + "[ " + opyear.Number + " ] " + opyear.Name + " [ Шифр: " + opyear.ObrazProgramCrypt + " ] ", 
                                                                   // + " [ " + pm.Name + " ] ",          // + " [ " + ps.Name + " ]",
                                   Направление = "[ " + fac.Name + " ] " + lp.Code + "  " + st.Name + "  " + lp.Name,    //+ " [" + progt.Name + "]" + " [" + q.Name + "]"
                                   //Образовательная_программа = opyear.Name,
                                   x.Id,
                                   x.ObrazProgramId,
                                   x.ObrazProgramInYearId,
                                   op.LicenseProgramId,
                                   FacId = fac.Id,
                                   FacName = fac.Name
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource1.DataSource = dt;
                    dgv.DataSource = bindingSource1;

                    List<string> Cols = new List<string>() { "Id", "ObrazProgramId", "ObrazProgramInYearId", "LicenseProgramId", "FacId", "FacName" };  //{ "Id", "ObrazProgramId" };

                    foreach (string s in Cols)
                        if (dgv.Columns.Contains(s))
                            dgv.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgv.Columns)
                        col.HeaderText = col.Name.Replace("_", " ");
                    try
                    {
                        dgv.Columns["Образовательная_программа"].Frozen = true;
                        dgv.Columns["Образовательная_программа"].Width = 750;
                        dgv.Columns["Направление"].Width = 600;
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
        private void VKRMain_Load(object sender, EventArgs e)
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

        private void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLPList(FacultyId);
            FillGrid(_VKRId);
        }

        private void cbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLPList(FacultyId);
            FillGrid(_VKRId);
        }

        private void cbLP_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillOPList();
            FillGrid(_VKRId);
        }

        private void btnAddOP_Click(object sender, EventArgs e)
        {
            if (!_VKRId.HasValue)
            {
                MessageBox.Show("Не выбран год", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!OPId.HasValue)
            {
                MessageBox.Show("Не выбрана образовательная программма", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbOP.DroppedDown = true;
                return;
            }
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKROP
                               where x.VKRId == _VKRId
                               && x.ObrazProgramInYearId == OPId
                               select new
                               {
                                   x.Id,
                               }).ToList().Count();
                    if (lst > 0)
                    {
                        MessageBox.Show("Такая образовательная программа уже добавлена.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                        //if (MessageBox.Show("Такая образовательная программа уже добавлена.\r\n" +
                        //    "Продолжить тем не менее?", "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                        //{ return; }
                    }
                }

                int opid;
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ObrazProgramInYear
                               where x.Id == OPId
                               select x).First();
                    opid = lst.ObrazProgramId;
                }

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    VKROP v = new VKROP();
                    v.VKRId = (int)_VKRId;
                    v.ObrazProgramId = opid;
                    v.ObrazProgramInYearId = (int)OPId;
                    context.VKROP.Add(v);
                    context.SaveChanges();
                    _id = v.Id;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось добавить запись...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            FillGrid(_VKRId);
        }

        private void cbVKRYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            Archive();
            FillGrid(_VKRId);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillGrid(_VKRId);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv.CurrentCell != null)
                    if (dgv.CurrentCell.RowIndex >= 0)
                    {
                        int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                        int opid = int.Parse(dgv.CurrentRow.Cells["ObrazProgramId"].Value.ToString());
                        int opinyearid = int.Parse(dgv.CurrentRow.Cells["ObrazProgramInYearId"].Value.ToString());
                        int lpid = int.Parse(dgv.CurrentRow.Cells["LicenseProgramId"].Value.ToString());
                        string op = dgv.CurrentRow.Cells["Образовательная_программа"].Value.ToString();
                        string lp = dgv.CurrentRow.Cells["Направление"].Value.ToString();
                        string facname = dgv.CurrentRow.Cells["FacName"].Value.ToString();
                        if (Utilities.VKRStudentIsOpened(id))
                            return;
                        new VKRStudent(id, (int)_VKRId, opid, opinyearid, lpid, op, lp, facname).Show();
                    }
            }
            catch (Exception)
            {
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.CurrentCell != null)
                    if (dgv.CurrentCell.RowIndex >= 0)
                    {
                        int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                        int opid = int.Parse(dgv.CurrentRow.Cells["ObrazProgramId"].Value.ToString());
                        int opinyearid = int.Parse(dgv.CurrentRow.Cells["ObrazProgramInYearId"].Value.ToString());
                        int lpid = int.Parse(dgv.CurrentRow.Cells["LicenseProgramId"].Value.ToString());
                        string op = dgv.CurrentRow.Cells["Образовательная_программа"].Value.ToString();
                        string lp = dgv.CurrentRow.Cells["Направление"].Value.ToString();
                        string facname = dgv.CurrentRow.Cells["FacName"].Value.ToString();
                        if (Utilities.VKRStudentIsOpened(id))
                            return;
                        new VKRStudent(id, (int)_VKRId, opid, opinyearid, lpid, op, lp, facname).Show();
                    }
            }
            catch (Exception)
            {
            }
        }

        private void checkBoxArchive_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var vkr = context.VKR.Where(x => x.Id == _VKRId).First();
                    vkr.Archive = checkBoxArchive.Checked ? true : false;
                    context.SaveChanges();

                    MessageBox.Show("Данные сохранены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
	        }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            Archive();
        }
    }
}
