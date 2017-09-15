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
    public partial class VKRThemeCard : Form
    {
        public int? _id;
        string NR_NPRTabnum;
        public int Year
        {
            get { return (int)ndYear.Value; }
            set { ndYear.Value = value; }
        }
        public string Theme
        {
            get { return tbTheme.Text.Trim(); }
            set { tbTheme.Text = value; }
        }
        public string ThemeEng
        {
            get { return tbThemeEng.Text.Trim(); }
            set { tbThemeEng.Text = value; }
        }
        UpdateVoidHandler _hndl;
        public int? OrganizationId
        {
            set { ComboServ.SetComboId(cbOrganization, value); }
        }

        public VKRThemeCard(int? id, UpdateVoidHandler h)
        {
            InitializeComponent();
            _id = id;
            _hndl = h;
            this.MdiParent = Util.mainform;
            FillCard();
        }
        public void FillCard()
        {
            ndYear.Minimum = 0;
            ndYear.Maximum = DateTime.Now.Year + 3;
            ndYear.Value = DateTime.Now.AddMonths(-6).Year;

            FillOrganization();
            ComboServ.FillCombo(cbAggregateGroup, HelpClass.GetComboListByTable(@"dbo.SP_AggregateGroup", " order by Name"), false, false);
            ComboServ.FillCombo(cbConsultPerson, HelpClass.GetComboListByQuery( @"  select convert(nvarchar, PartnerPerson.Id) as Id,
                PartnerPerson.Name as Name  from dbo.PartnerPerson "), true, false);
            ComboServ.FillCombo(cbStudyLevel, HelpClass.GetComboListByTable(@"dbo.StudyLevel", " where StudyLevel.Id in (select StudyLevelId from dbo.LicenseProgram lp join dbo.SP_AggregateGroup agg on lp.AggregateGroupId = agg.Id)"), false, false);
            ComboServ.SetComboId(cbStudyLevel, "16");

            if (_id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var Theme = (from x in context.VKR_Themes
                                 where x.Id == _id
                                 select x).First();
                    tbTheme.Text = Theme.VKRName;
                    tbThemeEng.Text = Theme.VKRNameEng;
                    tbOrganization.Text = Theme.OrganizationName;
                    tbComment.Text = Theme.Comment;

                    tbDocument.Text = Theme.DocumentNumber;
                    tbRKNum.Text = Theme.RKNum;
                    dtpRKDate.Checked = Theme.RKDate.HasValue;
                    if (Theme.RKDate.HasValue)
                        dtpRKDate.Value = Theme.RKDate.Value;

                    chbRecievedFromGAKMember.Checked = Theme.RecievedFromGAKMember;
                    chbCanBeSelectedNextYear.Checked = Theme.CanBeSelectedNextYear ?? false;
                    chbCommandWork.Checked = Theme.CommandWork ?? false;
                    ndYear.Value = Theme.GraduateYear;

                    if (Theme.OrganizationId.HasValue)
                        ComboServ.SetComboId(cbOrganization, Theme.OrganizationId);
                    if (Theme.PartnerPersonId.HasValue)
                        ComboServ.SetComboId(cbPartnerPerson, Theme.PartnerPersonId);
                    btnOrganizationOpen.Enabled = Theme.OrganizationId.HasValue;

                    if (Theme.ConsultPersonId.HasValue)
                        ComboServ.SetComboId(cbConsultPerson, Theme.ConsultPersonId.Value);

                    tbConsultSurname.Text = Theme.Consult_Surname;
                    tbConsultName.Text = Theme.Consult_Name;
                    tbConsultSecondName.Text = Theme.Consult_SecondName;
                    tbConsultDegree.Text = Theme.Consult_Degree;
                    tbConsultRank.Text = Theme.Consult_Rank;
                    tbConsultPosition.Text = Theme.Consult_Position;

                    chbEnglOriginal.Checked = Theme.EngOriginal;

                    FillStudyPlans(context);
                    FillNR(context);
                    FillOrders(context);
                }
            else
                btnOrganizationOpen.Enabled = false;
        }

        #region FillStusyPlans
        private void FillStudyPlans(EmployerPartnersEntities context)
        {
            var lst = (from x in context.VKR_Themes_StudyPlans
                       join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                       join lp in context.LicenseProgram on op.LicenseProgramId equals lp.Id
                       join sl in context.StudyLevel on lp.StudyLevelId equals sl.Id

                       join sp in context.StudyPlanData on x.ObrazProgramId equals sp.ObrazProgramId into _sp
                       from sp in _sp.DefaultIfEmpty()

                       where x.VKRThemeId == _id
                       select new
                       {
                           x.Id,
                           Учебный_план = (sp != null) ? sp.StudyPlanNumber.Substring(3, 4) : "",
                           Уровень = sl.Name,
                           Направление = lp.Code + " " + lp.Name,
                           Образовательная_программа = sp.ObrazProgramCrypt.Substring(0, 7) + " " + op.Name,
                       }).Distinct().ToList();
            dgvStudyPlans.DataSource = lst;

            foreach (string s in new List<string>() { "Id" })
                if (dgvStudyPlans.Columns.Contains(s))
                    dgvStudyPlans.Columns[s].Visible = false;

            foreach (DataGridViewColumn s in dgvStudyPlans.Columns)
                s.HeaderText.Replace("_", " ");

            dgvStudyPlans.Columns["ColStudyPlanRemove"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStudyPlans.Columns["Уровень"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvStudyPlans.Columns["Учебный_план"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }
        private void FillLicenseProgram()
        {
            int? AggregateGroupId = ComboServ.GetComboIdInt(cbAggregateGroup);
            string sAggregateGroup = (AggregateGroupId.HasValue) ? (String.Format(" and LicenseProgram.AggregateGroupId = {0} ", AggregateGroupId.ToString())) : "";
            int? StudyLevelId = ComboServ.GetComboIdInt(cbStudyLevel);
            string sStudyLevel = (StudyLevelId.HasValue) ? (String.Format(" and LicenseProgram.StudyLevelId = {0} ", StudyLevelId.ToString())) : "";

            ComboServ.FillCombo(cbLicenseProgram, HelpClass.GetComboListByQuery(String.Format(@" select CONVERT(varchar(100), dbo.LicenseProgram.[Id]) AS Id, 
                        (dbo.LicenseProgram.[Code] + case Len(dbo.LicenseProgram.[Code]) when 6 then '    ' else (case Len(dbo.LicenseProgram.[Code]) when 5 then '        ' else '  ' end) end + 
                        + dbo.LicenseProgram.Name) as Name  
                                    from dbo.LicenseProgram 
                                    where 1=1 {0} {1} order by dbo.LicenseProgram.Name", sAggregateGroup, sStudyLevel)), false, false);
        }
        private void cbLicenseProgram_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillObrazProgramList();
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
                        ObrazProgram.Name) as Name  
                                    from dbo.ObrazProgram 
                                    join dbo.LicenseProgram  on ObrazProgram.LicenseProgramId = LicenseProgram.Id
                                    inner join dbo.StudyLevel on dbo.LicenseProgram.StudyLevelId = dbo.StudyLevel.Id  
                                    where 1=1 {0} {1} {2} order by dbo.ObrazProgram.Name", sAggregateGroup, sStudyLevelId, licenseprogram)), false, false);
        }
        private void cbStudyLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLicenseProgram();
        }
        private void cbAggregateGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLicenseProgram();
        }
        private void dgvStudyPlans_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvStudyPlans.CurrentCell == null) return;
            if (dgvStudyPlans.CurrentRow.Index < 0) return;
            if (dgvStudyPlans.CurrentCell.ColumnIndex == dgvStudyPlans.Columns["ColStudyPlanRemove"].Index)
            {
                if (MessageBox.Show(string.Format("Удалить выбранный учебный план?"), "Подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        int Id = int.Parse(dgvStudyPlans.CurrentRow.Cells["Id"].Value.ToString());
                        context.VKR_Themes_StudyPlans.Remove(context.VKR_Themes_StudyPlans.Where(x => x.Id == Id && x.VKRThemeId == _id).First());
                        context.SaveChanges();
                        FillStudyPlans(context);
                    }
                }
            }
        }
        private void btnAddStudyPlan_Click(object sender, EventArgs e)
        {
            if (!_id.HasValue)
            {
                Save();
            }
            if (!_id.HasValue)
            {
                MessageBox.Show("Ошибки при сохранении");
                return;
            }
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                int? ObrazProgramId = ComboServ.GetComboIdInt(cbObrazProgram);
                if (!ObrazProgramId.HasValue)
                {
                    MessageBox.Show("Образовательная программа не выбрана", "Ошибка");
                    return;
                }
                if (context.VKR_Themes_StudyPlans.Where(x => x.VKRThemeId == _id && x.ObrazProgramId == ObrazProgramId).Count() > 0)
                {
                    MessageBox.Show("Такой учебный план уже добавлен", "Ошибка");
                    return;
                }
                context.VKR_Themes_StudyPlans.Add(new VKR_Themes_StudyPlans()
                {
                    ObrazProgramId = ObrazProgramId,
                    VKRThemeId = _id,
                });
                context.SaveChanges();
                FillStudyPlans(context);
            }
        }
        #endregion
        #region NR
        public void FillNR(EmployerPartnersEntities context)
        {
                var lst = (from x in context.VKR_Themes_NPR
                           where x.VKRThemeId == _id
                           select new { 
                           x.Id ,
                           x.Tabnum,
                           x.NPR_Surname,
                           x.NPR_Name,
                           x.NPR_SecondName,
                           x.NPR_Chair, 
                           x.NPR_Degree,
                           }).ToList().Select(x=> new {
                               Id = x.Id,
                               Tabnum = x.Tabnum,
                               ФИО = ((x.NPR_Surname + " ") ?? "") + ((x.NPR_Name + " ") ?? "") + (x.NPR_SecondName ?? ""),
                               Кафедра = x.NPR_Chair,
                               Степерь = x.NPR_Degree,
                           }).ToList();
                dgvNPR.DataSource = lst;
                foreach (string s in new List<string>(){"Id"})
                    if (dgvNPR.Columns.Contains(s))
                        dgvNPR.Columns[s].Visible = false;
                dgvNPR.Columns["ColNRRemove"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvNPR.Columns["Tabnum"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                dgvNPR.Columns["ФИО"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        private void btnNPRFind_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("NPRListToFind"))
                Utilities.FormClose("NPRListToFind");
            new NPRListToFind(new UpdateStringHandler(NR_NPR_SetToFound)).Show();
        }
        private void NR_NPR_SetToFound(int? id, string Tabnum)
        {
            try
            {
                NR_NPRTabnum = Tabnum;
                if (String.IsNullOrEmpty(NR_NPRTabnum))
                {
                    tbNR_NPR_FIO.Text =
                    tbNR_NPR_Degree.Text =
                    tbNR_NPR_Rank.Text =
                    tbNR_NPR_Position.Text =
                    tbNR_NPR_Chair.Text =
                    tbNR_NPR_Account.Text = "";
                    return;
                }

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.SAP_NPR.Where(x => x.Tabnum == NR_NPRTabnum).First();

                    tbNR_NPR_FIO.Text = ((lst.Lastname + " ") ?? "") + ((lst.Name + " ") ?? "") + ((lst.Surname + " ") ?? "");
                    tbNR_NPR_Degree.Text = ((!String.IsNullOrEmpty(lst.Degree)) ? lst.Degree : "");
                    tbNR_NPR_Rank.Text = ((!String.IsNullOrEmpty(lst.Titl2)) ? lst.Titl2 : "");
                    tbNR_NPR_Position.Text = ((!String.IsNullOrEmpty(lst.Position)) ? lst.Position : "");
                    tbNR_NPR_Chair.Text = ((!String.IsNullOrEmpty(lst.FullName)) ? lst.FullName : "");
                    tbNR_NPR_Account.Text = ((!String.IsNullOrEmpty(lst.UsridAd)) ? lst.UsridAd : "");
                }
            }
            catch (Exception)
            {
            }
        }
        private void btnNPRAdd_Click(object sender, EventArgs e)
        {
            if (!_id.HasValue)
            {
                Save();
            }
            if (!_id.HasValue)
            {
                MessageBox.Show("Ошибки при сохранении");
                return;
            }
            if (!String.IsNullOrEmpty(NR_NPRTabnum) && _id.HasValue)
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    if (context.VKR_Themes_NPR.Where(x => x.Tabnum == NR_NPRTabnum && x.VKRThemeId == _id.Value).Count() > 0)
                    {
                        MessageBox.Show("Такой научный руководитель уже добавлен", "Ошибка");
                        return;
                    }
                    var db_NPR = context.SAP_NPR.Where(x => x.Tabnum == NR_NPRTabnum).FirstOrDefault();
                    if (db_NPR != null)
                    {
                        VKR_Themes_NPR npr = new VKR_Themes_NPR();

                        npr.VKRThemeId = _id.Value;
                        npr.Tabnum = db_NPR.Tabnum;
                        npr.NPR_Surname = db_NPR.Lastname;
                        npr.NPR_Name = db_NPR.Name;
                        npr.NPR_SecondName = db_NPR.Surname;
                        npr.NPR_Account = db_NPR.UsridAd;
                        npr.NPR_Degree = db_NPR.Degree;
                        npr.NPR_Position = db_NPR.Position;
                        npr.NPR_Rank = db_NPR.Titl2;
                        npr.NPR_Chair = db_NPR.FullName;

                        context.VKR_Themes_NPR.Add(npr);
                        context.SaveChanges();

                        NR_NPR_SetToFound(null, "");
                        FillNR(context);
                    }
                    else
                    {
                        MessageBox.Show("Научный руководитель не выбран или карточка не существует");
                    }
                }
            }
            else
            {
                MessageBox.Show("Научный руководитель не выбран или карточка не существует");
            }
        }
        private void dgvNPR_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNPR.CurrentCell == null) return;
            if (dgvNPR.CurrentRow.Index <0 ) return;
            if (dgvNPR.CurrentCell.ColumnIndex == dgvNPR.Columns["ColNRRemove"].Index)
            {
                string NPR = dgvNPR.CurrentRow.Cells["ФИО"].Value.ToString();
                if (MessageBox.Show(string.Format("Удалить <{0}>?", NPR),"Подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        int Id = int.Parse(dgvNPR.CurrentRow.Cells["Id"].Value.ToString());
                        context.VKR_Themes_NPR.Remove(context.VKR_Themes_NPR.Where(x=>x.Id == Id && x.VKRThemeId == _id).First());
                        context.SaveChanges();
                        FillNR(context);
                    }
                }
            }
        }
        #endregion
        #region Orders
        private void btnOrderAdd_Click(object sender, EventArgs e)
        {
            if (!_id.HasValue)
            {
                Save();
            }
            if (!_id.HasValue)
            {
                MessageBox.Show("Ошибки при сохранении");
                return;
            }
             if (string.IsNullOrEmpty(tbOrderNum.Text ))
            {
                MessageBox.Show("Номер приказа не введен");
                return;
            }
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                if (context.VKR_Themes_OrderNums.Where(x => x.OrderNum == tbOrderNum.Text && x.OrderDate == dtpOrderDate.Value && x.VKRThemeId == _id.Value).Count() > 0)
                {
                    MessageBox.Show("Такой приказ уже был добавлен");
                }
                else
                {
                    var VKROrder = new VKR_Themes_OrderNums()
                    {
                        OrderDate = dtpOrderDate.Value,
                        OrderNum = tbOrderNum.Text,
                        VKRThemeId = _id.Value,
                    };
                    context.VKR_Themes_OrderNums.Add(VKROrder);
                    context.SaveChanges();
                    FillOrders(context);
                }
            }
        }
        private void FillOrders(EmployerPartnersEntities context)
        {
            var lst = (from x in context.VKR_Themes_OrderNums
                       where x.VKRThemeId == _id.Value
                       select new
                       {
                           x.Id,
                           Номер_приказа = x.OrderNum,
                           Дата_приказа = x.OrderDate,
                       }).OrderBy(x => x.Номер_приказа).ToList();
            dgvOrders.DataSource = lst;
            foreach (string s in new List<string>() { "Id" })
                if (dgvOrders.Columns.Contains(s))
                    dgvOrders.Columns[s].Visible = false;
            foreach (DataGridViewColumn col in dgvOrders.Columns)
                col.HeaderText = col.HeaderText.Replace('_', ' ');
            dgvOrders.Columns["ColOrderRemove"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvOrders.Columns["Номер_приказа"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        private void dgvOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvOrders.CurrentCell == null) return;
            if (dgvOrders.CurrentRow.Index < 0) return;
            if (dgvOrders.CurrentCell.ColumnIndex == dgvOrders.Columns["ColOrderRemove"].Index)
            {
                if (MessageBox.Show(string.Format("Удалить выбранный приказ?"), "Подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        int Id = int.Parse(dgvOrders.CurrentRow.Cells["Id"].Value.ToString());
                        context.VKR_Themes_OrderNums.Remove(context.VKR_Themes_OrderNums.Where(x => x.Id == Id && x.VKRThemeId == _id).First());
                        context.SaveChanges();
                        FillOrders(context);
                    }
                }
            }
        }
        #endregion
        #region Organization
        private void FillOrganization()
        {
            ComboServ.FillCombo(cbOrganization, HelpClass.GetComboListByTable(@"dbo.Organization"), true, false);

        }
        private void btnRefreshOrgList_Click(object sender, EventArgs e)
        {
            FillOrganization();
        }
    
        private void btnOrganizationOpen_Click(object sender, EventArgs e)
        {
            int? OrgId = ComboServ.GetComboIdInt(cbOrganization);
            if (OrgId.HasValue)
            {
                try
                {
                    if (Utilities.OrgCardIsOpened(OrgId.Value))
                        return;
                    new CardOrganization(OrgId, null).Show();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Не удается открыть карточку.\r\r" + exc.Message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void cbOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOrganizationOpen.Enabled = ComboServ.GetComboIdInt(cbOrganization).HasValue;
            if (ComboServ.GetComboIdInt(cbOrganization).HasValue)
                ComboServ.FillCombo(cbPartnerPerson, HelpClass.GetComboListByQuery(String.Format(@"
                select convert(nvarchar, PartnerPerson.Id) as Id,
                PartnerPerson.Name as Name
                from dbo.PartnerPerson
                join dbo.OrganizationPerson on OrganizationPerson.PartnerPersonId = PartnerPerson.Id
                where OrganizationPerson.OrganizationId = {0} ", ComboServ.GetComboIdInt(cbOrganization).Value)), true, false);
            else
                ComboServ.FillCombo(cbPartnerPerson, new List<KeyValuePair<string, string>>(), true, false);
        }
        private void btnOrgHandBook_Click(object sender, EventArgs e)
        {
            new OrgListToFind(new UpdateIntHandler(OrgListSetToFound)).Show();
        }
        private void OrgListSetToFound(int? id)
        {
            OrganizationId = id.Value;
        }
        private void cbPartnerPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPartnerPersonCard.Enabled = ComboServ.GetComboIdInt(cbPartnerPerson).HasValue;
        }
        private void btnPartnerPersonCard_Click(object sender, EventArgs e)
        {
            int? PPId = ComboServ.GetComboIdInt(cbPartnerPerson);
            if (PPId.HasValue)
            {
                try
                {
                    if (Utilities.PersonCardIsOpened(PPId.Value))
                        return;
                    new CardPerson(PPId, null).Show();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Не удается открыть карточку.\r\r" + exc.Message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        #endregion
        #region Common
        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private bool CheckFields()
        {
            if (ndYear.Value > DateTime.Now.Year+1 || ndYear.Value < 2000)
            {
                error.SetError(ndYear, "Некорректное значение");
                lblError.Visible = true; 
                return false;
            }
            else
                error.Clear();

            if (String.IsNullOrEmpty(tbTheme.Text.Trim()) && !chbEnglOriginal.Checked)
            {
                error.SetError(tbTheme, "Отсутствует значение");
                lblError.Visible = true;
                return false;
            }
            else
                error.Clear();

            if (String.IsNullOrEmpty(tbRKNum.Text.Trim()))
            {
                error.SetError(tbRKNum, "Отсутствует значение");
                lblError.Visible = true;
                return false;
            }
            else
                error.Clear();
            
                

                if (!dtpRKDate.Checked || (dtpRKDate.Value.Year > DateTime.Now.Year || dtpRKDate.Value.Year < 2000))
                {
                    error.SetError(dtpRKDate, "Некорректное значение");
                    lblError.Visible = true; 
                    return false;
                }
                else
                    error.Clear();
            
            lblError.Visible = false; 
            return true;
        }
        private void Save()
        {
            if (!CheckFields())
            {
                return;
            }
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                if (context.VKR_Themes.Where(x=>x.VKRName == tbTheme.Text && x.GraduateYear == ndYear.Value && (!_id.HasValue || x.Id != _id.Value)).Count()>0)
                {
                    MessageBox.Show("Такая тема уже создана","Ошибка");
                    return;
                }

                VKR_Themes VKR;
                if (!_id.HasValue)
                    VKR = new VKR_Themes();
                else
                    VKR = context.VKR_Themes.Where(x => x.Id == _id).First();

                VKR.VKRName = tbTheme.Text.Trim();
                VKR.VKRNameEng = tbThemeEng.Text.Trim();
                VKR.RKNum = tbRKNum.Text;
                if (dtpRKDate.Checked)
                    VKR.RKDate = dtpRKDate.Value;
                else
                    VKR.RKDate = null;

                VKR.EngOriginal = chbEnglOriginal.Checked;
                VKR.GraduateYear = (int)ndYear.Value;
                VKR.OrganizationId = ComboServ.GetComboIdInt(cbOrganization);
                VKR.PartnerPersonId = ComboServ.GetComboIdInt(cbPartnerPerson);

                VKR.CommandWork = chbCommandWork.Checked;
                VKR.CanBeSelectedNextYear = chbCanBeSelectedNextYear.Checked;
                VKR.RecievedFromGAKMember = chbRecievedFromGAKMember.Checked;
                VKR.ConsultPersonId = ComboServ.GetComboIdInt(cbConsultPerson);
                VKR.Consult_Position = tbConsultPosition.Text;

                VKR.Consult_Surname = tbConsultSurname.Text;
                VKR.Consult_Name = tbConsultName.Text;
                VKR.Consult_SecondName = tbConsultSecondName.Text;
                VKR.Consult_Rank = tbConsultRank.Text;
                VKR.Consult_Degree = tbConsultDegree.Text;

                if (!_id.HasValue)
                {
                    VKR.Author = Util.GetUserName();
                    VKR.DateCreated = DateTime.Now;
                    context.VKR_Themes.Add(VKR);
                }
                context.SaveChanges();
                _id = VKR.Id;
                if (_hndl != null)
                    _hndl();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                if (MessageBox.Show("Подтвердите удаление темы ВКР","Подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        context.VKR_Themes.Remove(context.VKR_Themes.Where(x => x.Id == _id.Value).FirstOrDefault());
                        context.SaveChanges();
                        if (_hndl != null)
                            _hndl();
                        this.Close();
                    }
            }
            else
            {
                MessageBox.Show("Карточка еще не сохранена");
            }
        }
        private void btnDublicateCard_Click(object sender, EventArgs e)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var th = context.VKR_Themes.Where(x => x.VKRName == Theme && x.GraduateYear == Year + 1).FirstOrDefault();
                if (th != null)
                {
                    VKRThemeCard card = new VKRThemeCard(th.Id, _hndl);
                    card.Show();
                }
                else
                {
                    VKRThemeCard card = new VKRThemeCard(null, _hndl);
                    if (ComboServ.GetComboIdInt(cbOrganization).HasValue)
                        card.OrganizationId = ComboServ.GetComboIdInt(cbOrganization).Value;
                    card.Theme = this.tbTheme.Text;
                    card.ThemeEng = this.tbThemeEng.Text;
                    card.Year = this.Year + 1;
                    card.Show();
                }
            }
            
        }
        private void chbCanBeSelectedNextYear_CheckedChanged(object sender, EventArgs e)
        {
            btnDublicateCard.Enabled = chbCanBeSelectedNextYear.Checked;
        }
        #endregion
        #region Consult
        private void btnConsultPersonCardOpen_Click(object sender, EventArgs e)
        {
            int? CPId = ComboServ.GetComboIdInt(cbConsultPerson);
            if (CPId.HasValue)
            {
                try
                {
                    if (Utilities.PersonCardIsOpened(CPId.Value))
                        return;
                    new CardPerson(CPId, null).Show();
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Не удается открыть карточку.\r\r" + exc.Message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void cbConsultPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? ConsultID =  ComboServ.GetComboIdInt(cbConsultPerson);
            btnConsultPersonCardOpen.Enabled = ConsultID.HasValue;

            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                if (ConsultID.HasValue)
                {
                    var Consult = context.PartnerPerson.Where(x => x.Id == ConsultID.Value).First();
                    tbConsultSurname.Text = Consult.LastName;
                    tbConsultName.Text = Consult.FirstName;
                    tbConsultSecondName.Text = Consult.SecondName;
                    tbConsultDegree.Text = Consult.Degree.Name;
                    tbConsultRank.Text = Consult.Rank.Name;
                }
                else
                {
                    var Consult = context.VKR_Themes.Where(x => x.Id == _id.Value).First();
                    tbConsultSurname.Text = Consult.Consult_Surname;
                    tbConsultName.Text = Consult.Consult_Name;
                    tbConsultSecondName.Text = Consult.Consult_SecondName;
                    tbConsultDegree.Text = Consult.Consult_Degree;
                    tbConsultRank.Text = Consult.Consult_Rank;
                }
            }
        }
        #endregion
    }
}
