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
    public partial class SOPList : Form
    {
        public SOPList()
        {
            InitializeComponent();
            this.MdiParent = Util.mainform; 
            FillCard();
            FillGrid();
        }

        public void FillCard()
        {
            ComboServ.FillCombo(cbAggregateGroup, HelpClass.GetComboListByTable(@"dbo.SP_AggregateGroup", " order by Name"), false, true);
            ComboServ.FillCombo(cbStudyLevel, HelpClass.GetComboListByTable(@"dbo.StudyLevel", " where StudyLevel.Id in (select StudyLevelId from dbo.LicenseProgram lp join dbo.SP_AggregateGroup agg on lp.AggregateGroupId = agg.Id)"), false, true);
        }

        public void FillLicenseProgram()
        {
            int? AggregateGroupId = ComboServ.GetComboIdInt(cbAggregateGroup);
            int? StudyLevelId = ComboServ.GetComboIdInt(cbStudyLevel);
            string sAggregateGroup = (AggregateGroupId.HasValue) ? (" and AggregateGroupId = " + AggregateGroupId.ToString()) : " and AggregateGroupId in (select Id from dbo.SP_AggregateGroup) ";
            string sStudyLevelId = (StudyLevelId.HasValue) ? (" and StudyLevelId = " + StudyLevelId.ToString()):"";

            ComboServ.FillCombo(cbLicenseProgram, HelpClass.GetComboListByQuery(String.Format(@" select CONVERT(varchar(100), dbo.LicenseProgram.[Id]) AS Id, 
                        (dbo.LicenseProgram.[Code] + case Len(dbo.LicenseProgram.[Code]) when 6 then '    ' else (case Len(dbo.LicenseProgram.[Code]) when 5 then '        ' else '  ' end) end + 
                        dbo.StudyLevel.Name + case Len(dbo.StudyLevel.Name) when 11 then '    ' else (case Len(dbo.StudyLevel.Name) when 10 then '      ' else '  ' end) end + dbo.LicenseProgram.Name) as Name  
                                    from dbo.LicenseProgram inner join dbo.StudyLevel on dbo.LicenseProgram.StudyLevelId = dbo.StudyLevel.Id  
                                    where 1=1 {0} {1} order by dbo.LicenseProgram.Name", sAggregateGroup, sStudyLevelId)), false, true);
        }
         
        private void FillObrazProgramList() 
        {
            int? AggregateGroupId = ComboServ.GetComboIdInt(cbAggregateGroup);
            int? StudyLevelId = ComboServ.GetComboIdInt(cbStudyLevel);
            int? LicenseProgramId = ComboServ.GetComboIdInt(cbLicenseProgram);

            string licenseprogram = (LicenseProgramId.HasValue) ? ("and LicenseProgramId = " + LicenseProgramId.ToString()) : "";
            string sAggregateGroup = (AggregateGroupId.HasValue) ? (" and AggregateGroupId = " + AggregateGroupId.ToString()) : " and AggregateGroupId in (select Id from dbo.SP_AggregateGroup) ";
            string sStudyLevelId = (StudyLevelId.HasValue) ? (" and StudyLevelId = " + StudyLevelId.ToString()) : "";

            ComboServ.FillCombo(cbObrazProgram, HelpClass.GetComboListByQuery(String.Format(@" select CONVERT(varchar(100), [ObrazProgram].[Id]) AS Id,
                        (ObrazProgram.[Number] + case Len(ObrazProgram.[Number]) when 6 then '    ' else (case Len(ObrazProgram.[Number]) when 5 then '        ' else '  ' end) end + 
                        dbo.StudyLevel.Name + case Len(dbo.StudyLevel.Name) when 11 then '    ' else (case Len(dbo.StudyLevel.Name) when 10 then '      ' else '  ' end) end + ObrazProgram.Name) as Name  
                                    from dbo.ObrazProgram 
                                    join dbo.LicenseProgram  on ObrazProgram.LicenseProgramId = LicenseProgram.Id
                                    inner join dbo.StudyLevel on dbo.LicenseProgram.StudyLevelId = dbo.StudyLevel.Id  
                                    where 1=1 {0} {1} {2} order by dbo.ObrazProgram.Name", sAggregateGroup, sStudyLevelId, LicenseProgramId)), false, true);
        }

        public void FillGrid()
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                int? AggregateGroupId = ComboServ.GetComboIdInt(cbAggregateGroup);
                int? StudyLevelId = ComboServ.GetComboIdInt(cbStudyLevel);
                int? LicenseProgramId = ComboServ.GetComboIdInt(cbLicenseProgram);
                int? ObrazProgramId = ComboServ.GetComboIdInt(cbObrazProgram);

                var lst = (from x in context.SOP
                           join op in context.SOP_ObrazProgram on x.Id equals op.SOPId into _op
                           from op in _op.DefaultIfEmpty()

                           join p in context.ObrazProgram on op.ObrazProgramId equals p.Id into _p
                           from p in _p.DefaultIfEmpty()

                           join lp in context.LicenseProgram on p.LicenseProgramId equals lp.Id into _lp
                           from lp in _lp.DefaultIfEmpty()

                           where
                           (AggregateGroupId.HasValue ? lp.AggregateGroupId == AggregateGroupId : true) &&
                           (StudyLevelId.HasValue ? lp.StudyLevelId == StudyLevelId: true) && 
                           (LicenseProgramId.HasValue ? p.LicenseProgramId == LicenseProgramId: true) &&
                           (ObrazProgramId.HasValue ? p.Id == ObrazProgramId: true) 

                           select new
                           {
                               Id = x.Id,
                               Название = x.Name,
                               ОП = "         " + ((p == null)? "нет" : (p.Number+" "+p.Name)),
                           }).ToList();
                var _lst = (from x in lst
                            select new
                            {
                                Id = x.Id,
                                Совет = x.ОП,
                                isOP = true,
                            }).Union(from x in lst
                                     select new
                                     {
                                         Id = x.Id,
                                         Совет = x.Название,
                                         isOP = false,
                                     }).ToList().OrderBy(x => x.Id).ThenBy(x => x.isOP).ToList();

                dgv.DataSource = _lst;
                List<string> cols = new List<string>() { "Id","isOP" };
                foreach (string s in cols)
                    if (dgv.Columns.Contains(s))
                        dgv.Columns[s].Visible = false;
            }
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell == null) return;
            if (dgv.CurrentCell.RowIndex < 0) return;

            int Id = (int)dgv.CurrentRow.Cells["Id"].Value;
            new SOPCard(Id, new UpdateVoidHandler(FillGrid)).Show();

        }

        private void cbAggregateGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLicenseProgram();
        }

        private void cbStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLicenseProgram();
        }

        private void cbLicenseProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillObrazProgramList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("SOPCard"))
                return;
            new SOPCard(null, new UpdateVoidHandler(FillGrid)).Show();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FillGrid();
        }

        private void cbObrazProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
    }
}
