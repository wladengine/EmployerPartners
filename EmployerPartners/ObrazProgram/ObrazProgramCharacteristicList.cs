using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using EmployerPartners.EDMX;

namespace EmployerPartners
{
    public partial class ObrazProgramCharacteristicList : Form
    {
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

        public int? OrganizationId
        {
            get { return ComboServ.GetComboIdInt(cbOrganization); }
            set { ComboServ.SetComboId(cbOrganization, value); }
        }
        public ObrazProgramCharacteristicList()
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            FillCombos();
            FillComboOrg();
        }
        private void FillCombos()
        {
            ComboServ.FillCombo(cbAggregateGroup, HelpClass.GetComboListByTable(@"dbo.SP_AggregateGroup", " order by Name"), false, true);
            ComboServ.FillCombo(cbStudyLevel, HelpClass.GetComboListByQuery(@"select distinct convert(nvarchar(100), StudyLevelId) as Id, StudyLevel.Crypt + ' ('+  StudyLevel.Name+')' as Name
from dbo.LicenseProgram  
join dbo.SP_AggregateGroup on SP_AggregateGroup.Id = LicenseProgram.AggregateGroupId
join dbo.StudyLevel on StudyLevel.Id = LicenseProgram.StudyLevelId order by 1"), false, true);
        }
        private void btnRefreshOrgList_Click(object sender, EventArgs e)
        {
            FillComboOrg();
        }
        private void FillComboOrg()
        {
            //ComboServ.FillCombo(cbOrganization, HelpClass.GetComboListByTable("dbo.Organization"), false, false);
            ComboServ.FillCombo(cbOrganization, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), Id) AS Id, 
                (CASE  WHEN [MiddleName] is NULL THEN Organization.[Name] WHEN [MiddleName] = '' THEN Organization.[Name] ELSE [MiddleName] END) AS Name
                from dbo.Organization order by Name"), true, false);
        }

        private void btnOrgHandBook_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("OrgListToFind"))
                Utilities.FormClose("OrgListToFind");

            new OrgListToFind(new UpdateIntHandler(OrgListSetToFound)).Show();
        }
        private void OrgListSetToFound(int? id)
        {
            OrganizationId = id;
        }

        private void cbStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLicenseProgram();
        }

        private void cbAggregateGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLicenseProgram();
        }

        private void cbLicenseProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillObrazProgram();
        }

        private void FillObrazProgram()
        {
            string Filter = (AggregateGroupId.HasValue) ? ("where LicenseProgram.AggregateGroupId = " + AggregateGroupId.ToString()) : "";
            if (StudyLevelId.HasValue)
                Filter += String.Format("{0}{1}{2}", (string.IsNullOrEmpty(Filter) ? " where " : " and "), " LicenseProgram.StudyLevelId = ", StudyLevelId);
            if (LicenseProgramId.HasValue)
                Filter += String.Format("{0}{1}{2}", (string.IsNullOrEmpty(Filter) ? " where " : " and "), " ObrazProgram.LicenseProgramId = ", LicenseProgramId);

            ComboServ.FillCombo(cbObrazProgram, HelpClass.GetComboListByQuery(String.Format(@"select distinct convert(nvarchar, ObrazProgram.Id)  AS Id, 
StudyLevel.Crypt + '.' + ObrazProgram.Number + ' ' + ObrazProgram.Name as Name 
from dbo.ObrazProgram 
join dbo.LicenseProgram on LicenseProgram.Id = ObrazProgram.LicenseProgramId 
join dbo.SP_AggregateGroup on SP_AggregateGroup.Id = LicenseProgram.AggregateGroupId
join dbo.StudyLevel on StudyLevel.Id = LicenseProgram.StudyLevelId
{0}  order by 2", Filter)), false, true);
        }

        private void FillLicenseProgram()
        {
            string Filter = (AggregateGroupId.HasValue) ? (" where LicenseProgram.AggregateGroupId = " + AggregateGroupId.ToString()) : "";
            if (StudyLevelId.HasValue)
                Filter += String.Format("{0}{1}{2}", (string.IsNullOrEmpty(Filter) ? " where " : " and "), " LicenseProgram.StudyLevelId = ", StudyLevelId);
            ComboServ.FillCombo(cbLicenseProgram, HelpClass.GetComboListByQuery(String.Format(@"select distinct convert(nvarchar, LicenseProgram.Id)  AS Id, LicenseProgram.Code+' '+LicenseProgram.Name as Name 
from  dbo.LicenseProgram 
join dbo.SP_AggregateGroup on SP_AggregateGroup.Id = LicenseProgram.AggregateGroupId
{0} order by 2", Filter)), false, true);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ObrazProgramId.HasValue)
            {
                MessageBox.Show("Образовательная программа не выбрана","Ошибка");
                return;
            }
            if (!OrganizationId.HasValue)
            {
                MessageBox.Show("Организация не выбрана", "Ошибка");
                return;
            }

            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                if (context.ObrazProgramCharacteristic.Where(x=>x.ObrazProgramId == ObrazProgramId && x.OrganizationId == OrganizationId).Count()>0)
                {
                    MessageBox.Show("Такая запись уже добавлена", "Ошибка");
                    return;
                }

                ObrazProgramCharacteristic oop = new ObrazProgramCharacteristic();
                oop.ObrazProgramId = ObrazProgramId.Value;
                oop.OrganizationId = OrganizationId.Value;

                context.ObrazProgramCharacteristic.Add(oop);
                context.SaveChanges();
                FillGrid();
            }
        }
        public void FillGrid()
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.ObrazProgramCharacteristic
                           join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                           join lp in context.LicenseProgram on op.LicenseProgramId equals lp.Id
                           join org in context.Organization on x.OrganizationId equals org.Id
                           where (StudyLevelId.HasValue ? lp.StudyLevelId == StudyLevelId.Value : true) &&
                           (AggregateGroupId.HasValue ? lp.AggregateGroupId == AggregateGroupId.Value : true) &&
                           (LicenseProgramId.HasValue ? lp.Id == LicenseProgramId.Value : true) &&
                           (ObrazProgramId.HasValue ?op.Id == ObrazProgramId.Value : true) 
                           select new
                           {
                               x.Id,
                               Образовательная_программа = op.Name,
                               Организация = org.Name,
                           }).ToList();
                dgv.DataSource = lst;
                foreach (string s in new List<string>() { "Id" })
                    if (dgv.Columns.Contains(s))
                        dgv.Columns[s].Visible = false;

                dgv.Columns["ColRemove"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns["Образовательная_программа"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgv.Columns["Организация"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                foreach (DataGridViewColumn s in dgv.Columns)
                    s.HeaderText = s.HeaderText.Replace('_', ' ');

            }
        }

        private void cbObrazProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell == null) return;
            if (dgv.CurrentRow.Index < 0) return;

            if (dgv.CurrentCell.ColumnIndex == dgv.Columns["ColRemove"].Index)
            {
                if (MessageBox.Show("Удалить выбранную организацию из характеристики образовательной программы?", "Подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        int id = (int)dgv.CurrentRow.Cells["Id"].Value;
                        context.ObrazProgramCharacteristic.Remove(context.ObrazProgramCharacteristic.Where(x => x.Id == id).First());
                        context.SaveChanges();
                        FillGrid();
                    }
                }
            }
            
        }
    }
}
