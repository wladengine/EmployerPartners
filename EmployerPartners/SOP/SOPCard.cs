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
using System.IO;


namespace EmployerPartners
{
    public partial class SOPCard : Form
    {
        int? _Id;
        string _NPRTabnum;
        int? _PPId;
        int? _OrgMailId;
        List<SOPConferenseQuestionFile> lstOrgMailFiles;
        UpdateVoidHandler _hndl;

        public SOPCard(int? id, UpdateVoidHandler hdl)
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            _Id = id;
            _hndl = hdl;
            tabControl1.TabPages.Remove(tabpExpert);
            tabControl1.TabPages.Remove(tabpMessages);

            FillForm();   
        }
        #region FillCard
        private void FillForm()
        {
            ComboServ.FillCombo(cbAggregateGroup, HelpClass.GetComboListByTable(@"dbo.SP_AggregateGroup", " order by Name"), false, true);
            ComboServ.FillCombo(cbStudyLevel, HelpClass.GetComboListByTable(@"dbo.StudyLevel", " where StudyLevel.Id in (select StudyLevelId from dbo.LicenseProgram lp join dbo.SP_AggregateGroup agg on lp.AggregateGroupId = agg.Id)"), false, true);
            ComboServ.FillCombo(cbNPR_Position, HelpClass.GetComboListByTable("dbo.SOP_Position"), true, false);
            ComboServ.FillCombo(cbPP_Position, HelpClass.GetComboListByTable("dbo.SOP_Position"), true, false);
            ComboServ.FillCombo(cbNR_PP, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), pp.Id) AS Id, 
                (pp.Name + CASE WHEN d.Acronym is NULL THEN '' ELSE ',   ' + d.Acronym END  + CASE WHEN pop.OrgName is NULL THEN '' ELSE ',   ' + pop.OrgName END) as Name
                from dbo.PartnerPerson pp left outer join dbo.Degree d on pp.DegreeId = d.Id left outer join dbo.ECD_OrganizationPersonOrgName pop on pp.Id = pop.PartnerPersonId order by Name"), true, false);

            //ComboServ.FillCombo(cbOrgMailOrganization, HelpClass.GetComboListByQuery("select CONVERT(varchar(100), Id) AS Id,  ShortName as Name from dbo.Organization order by 2"), false, false);
            //ComboServ.FillCombo(cbOrgMailAddOrganization, HelpClass.GetComboListByQuery("select CONVERT(varchar(100), Id) AS Id,  ShortName as Name from dbo.Organization order by 2"), false, false);
            //ComboServ.FillCombo(cbOrgMailEditOrganization, HelpClass.GetComboListByQuery("select CONVERT(varchar(100), Id) AS Id,  ShortName as Name from dbo.Organization order by 2"), false, false);

            //ComboServ.FillCombo(cbOrgMailAddPartnerPerson, HelpClass.GetComboListByTable("dbo.PartnerPerson"), false, false);
            //ComboServ.FillCombo(cbOrgMailPartnerPerson, HelpClass.GetComboListByTable("dbo.PartnerPerson"), false, false);
            //ComboServ.FillCombo(cbOrgMailEditPartnerPerson, HelpClass.GetComboListByTable("dbo.PartnerPerson"), false, false);
        }
        private void FillLicenseProgram()
        {
            int? AggregateGroupId = ComboServ.GetComboIdInt(cbAggregateGroup);
            int? StudyLevelId = ComboServ.GetComboIdInt(cbStudyLevel);
            string sAggregateGroup = (AggregateGroupId.HasValue) ? (String.Format(" and LicenseProgram.AggregateGroupId = {0} ", AggregateGroupId.ToString())) : "";
            string sStudyLevelId = (StudyLevelId.HasValue) ? (" and StudyLevelId = " + StudyLevelId.ToString()) : "";

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

            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.ObrazProgram
                           join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                           join sl in context.StudyLevel on lp.StudyLevelId equals sl.Id

                           join sp in context.SOP_ObrazProgram on new {opid = x.Id, sopid= _Id.Value}  equals  new {opid = sp.ObrazProgramId, sopid = sp.SOPId} into _sp
                           from sp in _sp.DefaultIfEmpty()

                           where
                           (AggregateGroupId.HasValue ? lp.AggregateGroupId == AggregateGroupId.Value : true)
                           && (StudyLevelId.HasValue ? lp.StudyLevelId == StudyLevelId.Value : true)
                           && (LicenseProgramId.HasValue ? lp.Id == LicenseProgramId.Value : true)
                           select new

                           {
                               Id = x.Id,
                               Уровень = sl.Name,
                               Название = x.Number + " " + x.Name,
                               isAdded = (sp != null),
                           }).ToList();
                dgvOpAdd.DataSource = lst;
                List<string> cols = new List<string>() { "Id", "isAdded" };
                foreach (string s in cols)
                    if (dgvOpAdd.Columns.Contains(s))
                        dgvOpAdd.Columns[s].Visible = false;

                dgvOpAdd.Columns["ColumnOPAdd"].Width = 70;
                dgvOpAdd.Columns["Уровень"].Width = 70;
                btnOPAddAll.Enabled = dgvOpAdd.Rows.Count > 0;
            }
        }
        private void FillCard()
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                if (_Id.HasValue)
                {
                    var Sop = context.SOP.Where(x => x.Id == _Id.Value).FirstOrDefault();
                    if (Sop == null)
                    {
                        MessageBox.Show("Неведомая ошибка");
                        this.Close();
                    }

                    lblAuthorCreate.Text = String.Format("{0} ({1} в {2})", Sop.Author, Sop.Timestamp.ToShortDateString(), Sop.Timestamp.ToShortTimeString()) ?? "";
                    lblAuthorUpdate.Text = Sop.TimestampUpdate.HasValue ? 
                        ( String.Format("{0} ({1} в {2})", Sop.AuthorUpdate ?? "", Sop.TimestampUpdate.Value.ToShortDateString(),  Sop.TimestampUpdate.Value.ToShortTimeString() )):
                        "";

                    FillNR(context);
                    FillObrazPrograms(context);
                    FillConference(context);
                    FillOrgMail(context);
                } 
            }
        }
        private void FillObrazPrograms(EmployerPartnersEntities context)
        {
            var ops = (from x in context.SOP_ObrazProgram
                       join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                       join lp in context.LicenseProgram on op.LicenseProgramId equals lp.Id
                       join sl in context.StudyLevel on lp.StudyLevelId equals sl.Id
                       join fac in context.Faculty on op.FacultyId equals fac.Id

                       where x.SOPId == _Id.Value
                       select new
                       {
                           Id = x.Id,
                           Название = op.Number + " " + op.Name,
                           Направление = lp.Name,
                           Уровень = sl.Name,
                           Укрупненная_группа = fac.Name,
                       }).ToList();
            dgvOP.DataSource = ops;
            List<string> cols = new List<string>() { "Id" };
            foreach (string s in cols)
                if (dgvOP.Columns.Contains(s))
                    dgvOP.Columns[s].Visible = false;
            foreach (DataGridViewColumn col in dgvOP.Columns)
            {
                if (col.HeaderText.Contains("_"))
                    col.HeaderText = col.HeaderText.Replace("_", " ");
            }
            dgvOP.Columns["ColumnOPRemove"].Width = 70;

            var l = (from x in ops
                         group x by x.Уровень into _x
                     select new 
                     {
                         str = _x.Key,
                         lst = _x.Select(t=>t.Название).ToList(),
                     }).ToList();

            string lbltxt = "Совет образовательной программы";
            if (l.Count >1 )
             lbltxt = "Совет образовательных программ";
            foreach (var s in l)
            {
                if (s.str.EndsWith("а"))
                    lbltxt += " "+s.str.Substring(0, s.str.Length - 1) + "ы";
                else
                    lbltxt += " "+s.str + "a";

                foreach (string op in s.lst)
                {
                    lbltxt += String.Format(" \"{0}\",", op);
                }
            }
            if (!String.IsNullOrEmpty(lbltxt))
                lblName.Text = lbltxt.Substring(0, lbltxt.Length - 1);
        }
        #endregion
        #region NR
        private void FillNR(EmployerPartnersEntities context)
        {
            while (dgvNR.Rows.Count > 0)
                dgvNR.Rows.Remove(dgvNR.Rows[0]);
            while (dgvNR.Columns.Count > 1)
                dgvNR.Columns.Remove(dgvNR.Columns[1]);
            var lst = (from x in context.SOP_Members_NPR
                       join npr in context.SAP_NPR on x.Tabnum equals npr.Tabnum
                       where x.SOPId == _Id.Value
                       select new
                       {
                           x.Id,
                           x.Tabnum,
                           PartnerPersonId = (int?)null,
                           ФИО = ((npr.Lastname + " ") ?? "") + ((npr.Name + " ") ?? "") + ((npr.Surname) ?? ""),
                           PositionId = x.PositionId,
                           isNPR = true,
                           Звание = npr.Degree,
                           Степень = npr.Position,
                           Организация = "СПбГУ",
                       }).ToList().Union((from x in context.SOP_Members_PartnerPerson
                                          join pp in context.PartnerPerson on x.PartnerPersonId equals pp.Id
                                          join pp_pos in context.SOP_Position on x.PositionId equals pp_pos.Id into _pp_pos
                                          from pp_pos in _pp_pos.DefaultIfEmpty()

                                          where x.SOPId == _Id.Value
                                          select new
                                          {
                                              x.Id,
                                              Tabnum = "",
                                              PartnerPersonId = (int?)x.PartnerPersonId,
                                              ФИО = pp.Name,
                                              PositionId = x.PositionId,
                                              isNPR = false,
                                              Звание = pp.Degree.Name,
                                              Степень = pp.Rank.Name,
                                              Организация = "",
                                          }).ToList()).ToList();
            DataTable dt = new DataTable(); 
            dt = Utilities.ConvertToDataTable(lst);

            foreach (DataRow rw in dt.Rows)
            {
                if (!rw.Field<bool>("isNPR"))
                {
                    int? PPid = rw.Field<int?>("PartnerPersonId");
                    var Orgs = (from x in context.OrganizationPerson
                                join org in context.Organization on x.OrganizationId equals org.Id
                                where x.PartnerPersonId == PPid
                                select org.Name + " (" + x.Position + ")"
                                    ).ToList();
                    rw.SetField<string>("Организация", Orgs.First());
                }
            }

            DataGridViewTextBoxCell cell0 = new DataGridViewTextBoxCell();
            foreach (DataColumn s in dt.Columns)
            {
                DataGridViewColumn col01 = new DataGridViewColumn();
                col01.Name = s.ColumnName;
                col01.HeaderText = s.ColumnName;
                col01.CellTemplate = cell0;
                dgvNR.Columns.Add(col01);
            } 

            DataGridViewComboBoxColumn column1 = new DataGridViewComboBoxColumn();
            DataGridViewComboBoxCell cell1 = new DataGridViewComboBoxCell();
            cell1.DataSource = Util.lstSOPPositions;
            cell1.DisplayMember = "Value";
            cell1.ValueMember = "Key";
            column1.HeaderText = "Должность в СОП";
            column1.Name = "ColumnMemberPosition";
            column1.CellTemplate = cell1;
            column1.Width = 250;
            column1.DataSource = Util.lstSOPPositions;
            dgvNR.Columns.Add(column1);
            dgvNR.Columns["ColumnMemberPosition"].DisplayIndex = 1;
          
 
            foreach (DataRow rw in dt.Rows)
            {
                DataGridViewRow row = new DataGridViewRow();

                row.CreateCells(dgvNR);  
                row.Cells[dgvNR.Columns["Id"].Index].Value = rw.Field<int>("Id");
                row.Cells[dgvNR.Columns["PartnerPersonId"].Index].Value = rw.Field<int?>("PartnerPersonId");
                row.Cells[dgvNR.Columns["ФИО"].Index].Value = rw.Field<string>("ФИО");
                row.Cells[dgvNR.Columns["PositionId"].Index].Value = rw.Field<int?>("PositionId");
                row.Cells[dgvNR.Columns["isNPR"].Index].Value = rw.Field<bool>("isNPR");
                row.Cells[dgvNR.Columns["Звание"].Index].Value = rw.Field<string>("Звание");
                row.Cells[dgvNR.Columns["Tabnum"].Index].Value = rw.Field<string>("Tabnum");
                row.Cells[dgvNR.Columns["Степень"].Index].Value = rw.Field<string>("Степень");
                row.Cells[dgvNR.Columns["Организация"].Index].Value = rw.Field<string>("Организация");

                if (rw.Field<int?>("PositionId").HasValue)
                {
                    row.Cells[dgvNR.Columns["ColumnMemberPosition"].Index].Value = rw.Field<int>("PositionId").ToString();
                }
                else
                    row.Cells[dgvNR.Columns["ColumnMemberPosition"].Index].Value = ComboServ.NO_VALUE;
                dgvNR.Rows.Add(row);
            } 
            
            foreach (string s in new List<string>() { "Id","isNPR", "Tabnum","PartnerPersonId" , "PositionId"})
            {
                if (dgvNR.Columns.Contains(s))
                    dgvNR.Columns[s].Visible = false;
            }
            foreach (DataGridViewColumn col in dgvNR.Columns)
            {
                if (col.HeaderText.Contains("_"))
                    col.HeaderText = col.HeaderText.Replace("_", " ");
            }
            dgvNR.Columns["ColumnNRRemove"].Width = 70;
            dgvNR.Columns["ColumnMemberPosition"].Width = 90;
            dgvNR.Update();
        }
        private void btn_NR_NPR_find_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("NPRListToFind"))
                Utilities.FormClose("NPRListToFind");
            new NPRListToFind(new UpdateStringHandler(NR_NPR_SetToFound)).Show();
        }
        private void NR_NPR_SetToFound(int? id, string Tabnum)
        {
            try
            {
                _NPRTabnum = Tabnum;
                if (String.IsNullOrEmpty(_NPRTabnum))
                {
                    tbNR_NPR_FIO.Text =
                    tbNR_NPR_Degree.Text =
                    tbNR_NPR_Rank.Text =
                    tbNR_NPR_Position.Text =
                    tbNR_NPR_Faculty.Text =
                    tbNR_NPR_Chair.Text =
                    tbNR_NPR_Account.Text = "";
                    return;
                }

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.SAP_NPR.Where(x => x.Tabnum == _NPRTabnum).First();

                    tbNR_NPR_FIO.Text = ((lst.Lastname + " ") ?? "") + ((lst.Name + " ") ?? "") + ((lst.Surname + " ") ?? "");
                    tbNR_NPR_Degree.Text= ((!String.IsNullOrEmpty(lst.Degree)) ? lst.Degree : "");
                    tbNR_NPR_Rank.Text= ((!String.IsNullOrEmpty(lst.Titl2)) ? lst.Titl2 : "");
                    tbNR_NPR_Position.Text= ((!String.IsNullOrEmpty(lst.Position)) ? lst.Position : "");
                    tbNR_NPR_Faculty.Text= ((!String.IsNullOrEmpty(lst.Faculty)) ? lst.Faculty : "");
                    tbNR_NPR_Chair.Text= ((!String.IsNullOrEmpty(lst.FullName)) ? lst.FullName : "");
                    tbNR_NPR_Account.Text= ((!String.IsNullOrEmpty(lst.UsridAd)) ? lst.UsridAd : "");
                }
            }
            catch (Exception)
            {
            }
        }
        private void btn_NR_PP_find_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("PersonListToFind"))
                Utilities.FormClose("PersonListToFind");

            new PersonListToFind(new UpdateIntHandler(NR_PP_SetToFound)).Show();
        }
        private void NR_PP_SetToFound(int? id)
        {
            _PPId = id;
            ComboServ.SetComboId(cbNR_PP, id);
        }
        private void cbNR_PP_SelectedIndexChanged(object sender, EventArgs e)
        {
            _PPId = ComboServ.GetComboIdInt(cbNR_PP);
            try
            {
                if (!_PPId.HasValue)
                {
                    tb_NR_PP_FIO.Text =  
                    tb_NR_PP_Degree.Text = 
                    tb_NR_PP_Rank.Text =  
                    tb_NR_PP_OrgPos.Text =  
                    tb_NR_PP_Account.Text = "";
                    return;
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.PartnerPersonOrgPosition.Where(x => x.Id == (int)_PPId).First();

                    tb_NR_PP_FIO.Text = (!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "";
                    tb_NR_PP_Degree.Text = (!String.IsNullOrEmpty(lst.DegreeName)) ? lst.DegreeName : "";
                    tb_NR_PP_Rank.Text = (!String.IsNullOrEmpty(lst.RankName)) ? lst.RankName : "";
                    tb_NR_PP_OrgPos.Text = (!String.IsNullOrEmpty(lst.OrgPosition)) ? lst.OrgPosition : "";
                    tb_NR_PP_Account.Text = (!String.IsNullOrEmpty(lst.Account)) ? lst.Account : "";
                }
            }
            catch (Exception)
            {
            }
        }
        private void btn_NR_PP_Update_Click(object sender, EventArgs e)
        {
            FillForm();
            ComboServ.SetComboId(cbNR_PP, _PPId);
        }
        private void btn_NR_PP_Clear_Click(object sender, EventArgs e)
        {
            NR_PP_SetToFound(null);
        }
        private void dgvNR_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNR.CurrentCell == null) return;
            if (dgvNR.CurrentCell.RowIndex < 0) return;

            if (dgvNR.CurrentCell.ColumnIndex == dgvNR.Columns["ColumnMemberPosition"].Index || dgvNR.CurrentCell.ColumnIndex == dgvNR.Columns["ColumnNRRemove"].Index)
                return;

            bool isNPR = (bool)dgvNR.CurrentRow.Cells["isNPR"].Value;

            if (isNPR)
            {
                string Tabnum = dgvNR.CurrentRow.Cells["Tabnum"].Value.ToString();
            }
            else
            {
                int Id = (int)dgvNR.CurrentRow.Cells["PartnerPersonId"].Value;
                new CardPerson(Id, null).Show();
            }            
        }
        private void SOPCard_Shown(object sender, EventArgs e)
        {
            FillCard();
            //FillNR(new EmployerPartnersEntities());
        }
        private void dgvNR_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }
        #endregion

        #region save
        private bool SOPSave(EmployerPartnersEntities context)
        {
            try
            {
                SOP sop = new SOP();
                sop.Name = lblName.Text;
                sop.Author = Util.GetUserName();
                sop.AuthorUpdate = Util.GetUserName();
                sop.TimestampUpdate = DateTime.Now;
                context.SOP.Add(sop);
                context.SaveChanges();
                _Id = sop.Id;
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void SOPUpdate(EmployerPartnersEntities context)
        {
            SOP sop = context.SOP.Where(x => x.Id == _Id).FirstOrDefault();
            if (sop == null) return;
            sop.AuthorUpdate = Util.GetUserName();
            sop.TimestampUpdate = DateTime.Now;
            sop.Name = lblName.Text;
            context.SaveChanges();
            if (_hndl != null)
                _hndl();
        }
        private void btn_NR_NPR_Add_Click(object sender, EventArgs e)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                if (!_Id.HasValue)
                    if (!SOPSave(context))
                    {
                        MessageBox.Show("Ошибка при сохранении. Изменения отменены");
                        return;
                    }
                

                if (String.IsNullOrEmpty(_NPRTabnum) || !_Id.HasValue)
                {
                    MessageBox.Show("НПР не выбран или карточка не существует");
                }
                else
                {
                    if (context.SOP_Members_NPR.Where(x => x.Tabnum == _NPRTabnum && x.SOPId == _Id).Count() > 0)
                    {
                        MessageBox.Show("Такой НПР уже добавлен", "Ошибка");
                        return;
                    }
                    var db_NPR = context.SAP_NPR.Where(x => x.Tabnum == _NPRTabnum).FirstOrDefault();
                    if (db_NPR != null)
                    {
                        SOP_Members_NPR npr = new SOP_Members_NPR(); 
                        npr.SOPId = _Id.Value;
                        npr.Tabnum = db_NPR.Tabnum;
                        npr.PositionId = ComboServ.GetComboIdInt(cbNPR_Position); ;
                        npr.Author = Util.GetUserName();
                        context.SOP_Members_NPR.Add(npr);

                        SOP sop = context.SOP.Where(x => x.Id == _Id.Value).FirstOrDefault();
                        sop.AuthorUpdate = Util.GetUserName();
                        sop.TimestampUpdate = DateTime.Now;

                        context.SaveChanges();

                        NR_NPR_SetToFound(null, "");
                        FillNR(context);
                    }
                    else
                    {
                        MessageBox.Show("НПР не выбран или карточка не существует");
                    }
                }
            }

        }
        private void btn_NR_PP_Add_Click(object sender, EventArgs e)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                if (!_Id.HasValue)
                    if (!SOPSave(context))
                    {
                        MessageBox.Show("Ошибка при сохранении. Изменения отменены");
                        return;
                    }
                if (!_PPId.HasValue || !_Id.HasValue)
                {
                    MessageBox.Show("Партнер не выбран или карточка не существует");
                    return;
                }

                if (context.SOP_Members_PartnerPerson.Where(x => x.SOPId == _Id && x.PartnerPersonId == _PPId).Count() > 0)
                {
                    MessageBox.Show("Такой партнер уже добавлен", "Ошибка");
                    return;
                }
                    SOP_Members_PartnerPerson pp = new SOP_Members_PartnerPerson();
                    pp.SOPId = _Id.Value;
                    pp.Author = Util.GetUserName();
                    pp.PartnerPersonId = _PPId.Value;
                    pp.PositionId = ComboServ.GetComboIdInt(cbPP_Position); ;
                    context.SOP_Members_PartnerPerson.Add(pp);

                    SOP sop = context.SOP.Where(x => x.Id == _Id.Value).FirstOrDefault();
                    sop.AuthorUpdate = Util.GetUserName();
                    sop.TimestampUpdate = DateTime.Now;
                    context.SaveChanges();

                    NR_PP_SetToFound(null);
                    FillNR(context);
            }
        }
        private void btnOPAddAll_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Продолжить добавление " + dgvOpAdd.Rows.Count.ToString() + " образовательных программ в СОП?", "Подтверждение", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                return;
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                if (!_Id.HasValue)
                    if (!SOPSave(context))
                    {
                        MessageBox.Show("Ошибка при сохранении. Изменения отменены");
                        return;
                    }

                foreach (DataGridViewRow rw in dgvOpAdd.Rows)
                {
                    int OPId = (int)rw.Cells["Id"].Value;
                    if (context.SOP_ObrazProgram.Where(x => x.SOPId == _Id.Value && x.ObrazProgramId == OPId).Count() == 0)
                    {

                        SOP_ObrazProgram op = new SOP_ObrazProgram();
                        op.SOPId = _Id.Value;
                        op.ObrazProgramId = OPId;
                        op.Author = Util.GetUserName();
                        context.SOP_ObrazProgram.Add(op);
                    }
                }
                context.SaveChanges();
                FillObrazPrograms(context);
                SOPUpdate(context);

            }
            
        }
        private void dgvOpAdd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOpAdd.CurrentCell == null)
                return;
            if (dgvOpAdd.CurrentRow.Index < 0)
                return;

            if (dgvOpAdd.CurrentCell.ColumnIndex == dgvOpAdd.Columns["ColumnOpAdd"].Index)
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    if (!_Id.HasValue)
                        if (!SOPSave(context))
                        {
                            MessageBox.Show("Ошибка при сохранении. Изменения отменены");
                            return;
                        }

                    int ObrazProgramId = (int)dgvOpAdd.CurrentRow.Cells["Id"].Value;
                    
                    if (context.SOP_ObrazProgram.Where(x=>x.SOPId == _Id.Value && x.ObrazProgramId == ObrazProgramId).Count() == 0)
                    {
                        SOP_ObrazProgram sop = new SOP_ObrazProgram();
                        sop.SOPId = _Id.Value;
                        sop.ObrazProgramId = ObrazProgramId;
                        sop.Author = Util.GetUserName();
                        context.SOP_ObrazProgram.Add(sop);
                        context.SaveChanges();

                        FillObrazPrograms(context);
                        SOPUpdate(context);
                    }

                }
            }
        }
        #endregion

        #region remove
        private void dgvNR_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNR.CurrentCell == null)
                return;
            if (dgvNR.CurrentRow.Index < 0)
                return;

            if (dgvNR.CurrentCell.ColumnIndex == dgvNR.Columns["ColumnNRRemove"].Index)
            {
                string FIO = (string)dgvNR.CurrentRow.Cells["ФИО"].Value;
                if (MessageBox.Show(String.Format("Удалить <{0}>?", FIO), "", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                    return;

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    int Id = (int)dgvNR.CurrentRow.Cells["Id"].Value;
                    bool isNPR = (bool)dgvNR.CurrentRow.Cells["isNPR"].Value;
                    if (isNPR)
                    {
                        try
                        {
                            context.SOP_Members_NPR.Remove(context.SOP_Members_NPR.Where(x => x.Id == Id && x.SOPId == _Id).First());
                            context.SaveChanges();
                            FillNR(context);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при удалении НПР");
                        }
                    }
                    else
                    {
                        try
                        {
                            context.SOP_Members_PartnerPerson.Remove(context.SOP_Members_PartnerPerson.Where(x => x.Id == Id && x.SOPId == _Id).First());
                            context.SaveChanges();
                            FillNR(context);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при удалении партнера");
                        }
                    }
                }
            }
        }
        #endregion
       
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
        private void dgvOP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOP.CurrentCell == null) return;
            if (dgvOP.CurrentRow.Index < 0) return;

            if (dgvOP.CurrentCell.ColumnIndex == dgvOP.Columns["ColumnOPRemove"].Index)
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    int OpId = (int)dgvOP.CurrentRow.Cells["Id"].Value;
                    if (MessageBox.Show("Удалить образовательную программу из совета?", "Подтверждение", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                        return;

                    SOP_ObrazProgram sop_op = context.SOP_ObrazProgram.Where(x => x.Id == OpId).First();
                    context.SOP_ObrazProgram.Remove(sop_op);
                    context.SaveChanges();

                    FillObrazPrograms(context);
                    SOPUpdate(context);
                }
            }
        }

        #region Conference
        public void FillConference()
        { 
            FillConference (new EmployerPartnersEntities());
        }
        public void FillConference(EmployerPartnersEntities context)
        {
            var lst = (from x in context.SOP_Conference
                       where x.SOPId == _Id
                       select new
                       {
                           x.Id,
                           x.Date,
                           x.Number,
                           x.Theme,
                       }).ToList().Select(x => new
                       {
                           x.Id,
                           Дата = x.Date.Value.ToShortDateString(),
                           Номер = x.Number,
                           Тема = x.Theme,
                       }).OrderBy(x=>x.Дата).ToList();
            dgvConference.DataSource = lst;
            foreach (string s in new List<string>() { "Id" })
                if (dgvConference.Columns.Contains(s))
                    dgvConference.Columns[s].Visible = false;
            try
            {
                dgvConference.Columns["Дата"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvConference.Columns["Номер"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvConference.Columns["Тема"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            catch
            { }
        }
        private void btnConferenceAdd_Click(object sender, EventArgs e)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                if (!_Id.HasValue)
                    if (!SOPSave(context))
                    {
                        MessageBox.Show("Ошибка при сохранении. Изменения отменены");
                        return;
                    }

                DateTime ConfDate = dtpConferenceDate.Value;
                string Number = tbConferenceNumber.Text;

                var Conf = context.SOP_Conference.Where(x => x.SOPId == _Id && x.Date == ConfDate && x.Number == Number).FirstOrDefault();
                if (Conf != null)
                {
                    MessageBox.Show("Такое совещание уже существует");
                    return;
                }
                SOP_Conference conf = new SOP_Conference();
                conf.Date = ConfDate.Date;
                conf.Number = Number;
                conf.Theme = tbConferenceTheme.Text;
                conf.SOPId = _Id.Value;
                conf.AuthorCreate = Util.GetUserName();
                conf.AuthorUpdate = Util.GetUserName();
                conf.TimestampCreate = DateTime.Now;
                conf.TimestampUpdate = DateTime.Now;
                context.SOP_Conference.Add(conf);
                context.SaveChanges();

                FillConference(context);
            }

        }

        private void dgvConference_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvConference.CurrentCell == null) return;
            if (dgvConference.CurrentCell.RowIndex < 0) return;

            int ConfId = (int)dgvConference.CurrentRow.Cells["Id"].Value;
            new SopConferenceCard(ConfId, new UpdateVoidHandler(FillConference)).Show();
        }
        #endregion

        #region OrgMain
        private void FillOrgMail(EmployerPartnersEntities context)
        {
             if (!_Id.HasValue)
                    if (!SOPSave(context))
                    {
                        MessageBox.Show("Ошибка при сохранении. Изменения отменены");
                        return;
                    }

             var OrgMail = (from x in context.SOP_OrganizationMail
                            join org in context.Organization on x.OrganizationId equals org.Id
                            join pp in context.PartnerPerson on x.PartnerPersonId equals pp.Id
                            where x.SOPId == _Id.Value
                            select new
                            {
                                x.Id,
                                Организация = org.Name,
                                Сотрудник = pp.FirstName,
                                x.Email,
                                Дата = x.Date,
                                Тема = x.Theme,
                                Текст = x.Text,
                                FullText = x.Text,
                            }).ToList().Select(x => new
                            {
                                x.Id,
                                x.Организация,
                                x.Сотрудник,
                                x.Email,
                                x.Дата,
                                x.Тема,
                                Текст = x.Текст.Substring(0, 50),
                                x.FullText
                            }).ToList();
             dgvOrgMail.DataSource = OrgMail;
             List<string> cols = new List<string>() { "Id", "FullText" };
             foreach (string s in cols)
             {
                 if (dgvOrgMail.Columns.Contains(s))
                     dgvOrgMail.Columns[s].Visible = false;
             }
             foreach (DataGridViewColumn col in dgvOrgMail.Columns)
             {
                 if (col.HeaderText.Contains("_"))
                     col.HeaderText = col.HeaderText.Replace("_", " ");
             }
        }
        private void FillOrgMailGroupBox()
        {
            if (_OrgMailId.HasValue)
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var OrgMail = context.SOP_OrganizationMail.Where(x => x.Id == _OrgMailId && x.SOPId == _Id.Value).FirstOrDefault();
                    tbOrgMailEditEmail.Text = OrgMail.Email;
                    tbOrgMailEditNumber.Text = OrgMail.Number;
                    tbOrgMailEditTheme.Text = OrgMail.Theme;
                    tbOrgMailEditText.Text = OrgMail.Text;

                    ComboServ.SetComboId(cbOrgMailEditOrganization, OrgMail.OrganizationId);
                    ComboServ.SetComboId(cbOrgMailEditPartnerPerson, OrgMail.PartnerPersonId);
                }
            }
            else
            {
                tbOrgMailAddEmail.Text = "";
                tbOrgMailAddNumber.Text = "";
                tbOrgMailAddTheme.Text = "";
                tbOrgMailAddText.Text = "";
            }
        }
        private void FillOrgMailPartnerPerson(ComboBox cbPP, int? OrgId)
        {
            ComboServ.FillCombo(cbPP, HelpClass.GetComboListByQuery(@"select distinct CONVERT(varchar(100), PartnerPerson.Id) AS Id, Name from dbo.PartnerPerson "
                + (OrgId.HasValue ? (" join dbo.OrganizationPerson on OrganizationPerson.PartnerPersonId = PartnerPerson.Id where OrganizationId = " + OrgId.Value.ToString()) : "")
                +@" order by 2"), false, false);

            ComboServ.FillCombo(cbOrgMailAddPartnerPerson, HelpClass.GetComboListByTable("dbo.PartnerPerson"), false, false);
        }
        private void btnOrgMailAdd_Click(object sender, EventArgs e)
        {
            _OrgMailId = null;
            FillGroupBoxOrgMail();
        }
        private void FillGroupBoxOrgMail()
        {
            if (!_OrgMailId.HasValue)
                dgvOrgMail.ClearSelection();

            gbOrgMailAdd.Visible = !_OrgMailId.HasValue;
            gbOrgMailEdit.Visible = _OrgMailId.HasValue;
            lstOrgMailFiles = new List<SOPConferenseQuestionFile>();
            FillOrgMailGroupBox();
        }
        private void FillOrgMailEditFiles(EmployerPartnersEntities context)
        {
            var Files = context.SOP_OrganizationMail_Files.Where(x => x.SOPOrganizationMailId == _OrgMailId.Value).Select (x=>new {x.FileId, Название = x.FileName}).ToList();
            dgvOrgMailEditFiles.DataSource = Files;
            foreach (string s in new List<string>() { "FileId" })
                if (dgvOrgMailEditFiles.Columns.Contains(s))
                    dgvOrgMailEditFiles.Columns[s].Visible = false;
        }
        private void dgvOrgMail_CurrentCellChanged(object sender, EventArgs e)
        {
            if (dgvOrgMail.CurrentCell == null) return;
            if (dgvOrgMail.CurrentCell.RowIndex < 0) return;

            _OrgMailId = (int)dgvOrgMail.CurrentRow.Cells["Id"].Value;
            FillGroupBoxOrgMail();
        }
        private void cbOrgMailOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillOrgMailPartnerPerson(cbOrgMailPartnerPerson, ComboServ.GetComboIdInt(cbOrgMailOrganization));
        }
        private void cbOrgMailEditOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillOrgMailPartnerPerson(cbOrgMailEditPartnerPerson, ComboServ.GetComboIdInt(cbOrgMailEditOrganization));
        }
        private void cbOrgMailAddOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillOrgMailPartnerPerson(cbOrgMailAddPartnerPerson, ComboServ.GetComboIdInt(cbOrgMailAddOrganization));
        }
        private void btnOrgMailAddMailAdd_Click(object sender, EventArgs e)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                SOP_OrganizationMail mail = new SOP_OrganizationMail();
                mail.SOPId = _Id;
                mail.OrganizationId = ComboServ.GetComboIdInt(cbOrgMailAddOrganization);
                mail.PartnerPersonId = ComboServ.GetComboIdInt(cbOrgMailAddPartnerPerson);
                mail.Theme = tbOrgMailAddTheme.Text.Trim();
                mail.Text = tbOrgMailAddText.Text.Trim();
                mail.Number = tbOrgMailAddNumber.Text.Trim();
                mail.Date = dtpOrgMailAddDate.Value.Date;
                mail.Author = Util.GetUserName();
                mail.Timestamp = DateTime.Now;
                mail.Email = tbOrgMailAddEmail.Text.Trim();
                context.SOP_OrganizationMail.Add(mail);
                context.SaveChanges();

                foreach (var x in lstOrgMailFiles)
                {
                    SOP_OrganizationMail_Files file = new SOP_OrganizationMail_Files();
                    file.SOPOrganizationMailId = mail.Id;
                    file.FileName = x.FileName;
                    file.FileId = x.FileId;
                    context.SOP_OrganizationMail_Files.Add(file);
                    context.FileStorage.Add(new FileStorage()
                        {
                            FileData = x.FileData,
                            Id = x.FileId,
                        });
                    context.SaveChanges();
                }

                FillOrgMail(context);
                dgvOrgMail.ClearSelection();
                _OrgMailId = null;
                FillGroupBoxOrgMail();
            }
        }
        private void FillOrgMailAddFiles()
        {
            var lst = (from x in lstOrgMailFiles
                       select new
                       {
                           Id = x.FileId,
                           Файл = x.FileName,
                       }).ToList();
            dgvOrgMailAddFiles.DataSource = lst;
            foreach (string s in new List<string>(){"Id"})
            {
                if (dgvOrgMailAddFiles.Columns.Contains(s))
                    dgvOrgMailAddFiles.Columns[s].Visible = false;
            }
        }
        private void btnOrgMailAddFileAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            try
            {
                string newfilename = Util.TempFilesFolder + Guid.NewGuid().ToString().Substring(10);
                File.Copy(ofd.FileName, newfilename);

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    lstOrgMailFiles.Add(new SOPConferenseQuestionFile()
                    {
                        FileId = Guid.NewGuid(),
                        FileName = ofd.FileName.Substring(ofd.FileName.LastIndexOf("\\") + 1),
                        FileData = File.ReadAllBytes(newfilename)
                    });
                }
                File.Delete(newfilename);
                FillOrgMailAddFiles();
            }
            catch { }
        }
   
        #endregion

        private void tabpMembers_Enter(object sender, EventArgs e)
        {
            //FillNR(new EmployerPartnersEntities());
        }

        private void dgvNR_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dgvNR.CurrentRow != null && dgvNR.CurrentRow.Index >= 0)
            {
                int Id = (int)dgvNR.CurrentRow.Cells["Id"].Value;
                bool isNPR = (bool)dgvNR.CurrentRow.Cells["isNPR"].Value;

                int? PositionId = (dgvNR.CurrentRow.Cells["ColumnMemberPosition"].Value.ToString() == ComboServ.NO_VALUE) ? (int?)null : (int.Parse(dgvNR.CurrentRow.Cells["ColumnMemberPosition"].Value.ToString()));
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    if (isNPR)
                    {
                        var Member = context.SOP_Members_NPR.Where(x => x.Id == Id).First();
                        Member.PositionId = PositionId;
                        context.SaveChanges();
                    }
                    else
                    {
                        var Member = context.SOP_Members_PartnerPerson.Where(x => x.Id == Id).First();
                        Member.PositionId = PositionId;
                        context.SaveChanges();
                    }
                }
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (_Id.HasValue && MessageBox.Show("Удалить СОП?","Подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    SOP sop = context.SOP.Where(x => x.Id == _Id.Value).FirstOrDefault();
                    if (sop != null)
                    {
                        context.SOP.Remove(sop);
                        context.SaveChanges();

                        if (_hndl != null)
                            _hndl();

                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Не удалось удалить СОП","Ошибка");
                    }
                }
            }
        }

        #region Expert
        
        #endregion
    }
}
