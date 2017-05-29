using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using FastMember;

using KLADR;
using System.Transactions;

namespace EmployerPartners
{
    public partial class CardOrganization : Form
    {
        private int? _Id
        {
            get;
            set;
        }
        public int OrgId
        {
            get;
            set;
        }
        public int OrgNamesId
        {
            get;
            set;
        }
        //bool EditMode = false;

        UpdateVoidHandler _hndl;

        public CardOrganization(int? id, UpdateVoidHandler _hdl)
        {
            InitializeComponent();
            _hndl = _hdl;
            _Id = id;
            OrgId = (int)id;

            FillCard();
            InitControls(this);
            this.MdiParent = Util.mainform;

            SetAccessRight();
            //Utilities.SetReadMode(this);
        }
        private void SetAccessRight()
        {
            if (Util.IsOrgPersonWrite())
            {
                btnSave.Enabled = true;
                btnDelete.Enabled = true;
                btnChangeName.Enabled = true;
                btnSubdivisionAdd.Enabled = true;
                btnSubdivisionDelete.Enabled = true;
                btnSubdivisionEdit.Enabled = true;
                BtnContactEdit.Enabled = true;
                btnDeleteContact.Enabled = true;
                btnContactAdd.Enabled = true;
                btnRublrikAdd.Enabled = true;
                btnRubricDelete.Enabled = true;
                btnFacultyAdd.Enabled = true;
                btnFacultyDelete.Enabled = true;
                btnActivityAreaAdd.Enabled = true;
                btnActivityAreaDelete.Enabled = true;
                btnLPAdd.Enabled = true;
                btnLPDelete.Enabled = true;
                btnChangeName.Enabled = true;
                btnActivityAreaProfessionalAdd.Enabled = true;
                btnActivityAreaProfessionalDelete.Enabled = true;
                btnOECDAdd.Enabled = true;
                btnOECDDelete.Enabled = true;
                //btnAddOkved.Enabled = true;
                //btnDelOkved.Enabled = true;
                btnAddDogovor.Enabled = true;
                dgvDogovor.Columns[1].Visible = true;
            }
            if (Util.IsDBOwner() || Util.IsAdministrator())
            {
                btnAddOkved.Enabled = true;
                btnDelOkved.Enabled = true;
            }
        }
       
        private void SetReadMode()
        {
            foreach (Control control in this.Controls)
            {
                TextBox tb = control as TextBox;
                if (tb != null)
                {
                    tb.ReadOnly = true;
                }
                ComboBox cb = control as ComboBox;
                if (cb != null)
                {
                    cb.Enabled = false;
                }
            }
        }
        public void InitControls(Control obj)
        {
            foreach (Control x in obj.Controls)
            {
                if (x.Controls.Count > 0)
                    InitControls(x);
                x.MouseClick += new MouseEventHandler(ChangeControlVisibility);
            }
        }
        private void ExtraInit(bool add)
        {
            if (add)
            {
                tbCountry.TextChanged += tbCountry_TextChanged;
                tbCity.TextChanged += tbCity_TextChanged;
                tbRegion.TextChanged += tbRegion_TextChanged;
                tbStreet.TextChanged += tbStreet_TextChanged;
                tbHouse.TextChanged += tbHouse_TextChanged;
            }
            else
            {
                tbCountry.TextChanged -= tbCountry_TextChanged;
                tbCity.TextChanged -= tbCity_TextChanged;
                tbRegion.TextChanged -= tbRegion_TextChanged;
                tbStreet.TextChanged -= tbStreet_TextChanged;
                tbHouse.TextChanged -= tbHouse_TextChanged;
            }
        }
        private void ChangeControlVisibility(object sender, EventArgs e)
        {
            if (sender != tbCountry && sender != lbCountry)
            {
                lbCountry.Visible = false;
            }
            if (sender != tbRegion && sender != lbRegion)
            {
                lbRegion.Visible = false;
            }
            if (sender != tbCity && sender != lbCity)
            {
                lbCity.Visible = false;
            }
            if (sender != tbStreet && sender != lbStreet)
            {
                lbStreet.Visible = false;
            }
            if (sender != tbCity && sender != lbCity)
            {
                lbCity.Visible = false;
            }
            if (sender != tbStreet && sender != lbStreet)
            {
                lbStreet.Visible = false;
            }
            if (sender != tbHouse && sender != lbHouse)
            {
                lbHouse.Visible = false;
            }
        }

        #region FillCard
        public void FillCard()
        {
            ExtraInit(false);
            ComboServ.FillCombo(cbActivityGoal, HelpClass.GetComboListByTable("dbo.ActivityGoal"), true, false);
            ComboServ.FillCombo(cbNationalAffiliation, HelpClass.GetComboListByTable("dbo.NationalAffiliation"), true, false);
            ComboServ.FillCombo(cbOwnershipType, HelpClass.GetComboListByTable("dbo.OwnershipType"), true, false);
            ComboServ.FillCombo(cbArea, HelpClass.GetComboListByTable("dbo.ActivityArea"), true, false);
            ComboServ.FillCombo(cbAreaProfessional, HelpClass.GetComboListByTable("dbo.ActivityAreaProfessional"), true, false);
            //ComboServ.FillCombo(cbOrganizationEnglishSource, HelpClass.GetComboListByTable("dbo.OrganizationEnglishSource"), true, false);

            FillOrgEnglisSource();
            FillPartnerInfo();
            FillOrganizationArea();
            FillOrgAreaProfessional();
            FillOkvedCode();
            FillOrganizationPerson();
            FillRubrics();
            FillFaculty();
            FillLP();
            FillDogovor();
            FillOrganizationSubdivision();
            ExtraInit(true);
        }
        private void FillOrgEnglisSource()
        {
            ComboServ.FillCombo(cbOrganizationEnglishSource, HelpClass.GetComboListByQuery(@" SELECT DISTINCT CONVERT(varchar(100), Id) AS Id, Name 
                FROM dbo.OrganizationEnglishSource ORDER BY Id"), true, false);
        }
        private void FillPartnerInfo()
        {
            lbCountry.Visible = false;
            FillCountry();
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var Partner = (from x in context.Organization
                                   where x.Id == _Id
                                   select x).First();
                    OrgName = Partner.Name;
                    NameEng = Partner.NameEng;
                    MiddleName = Partner.MiddleName;
                    ShortName = Partner.ShortName;
                    ShortNameEng = Partner.ShortNameEng;

                    okved = Partner.Okved;
                    okveddop = Partner.OkvedDop;
                    oecd = Partner.OECD;
                    OwnershipTypeId = Partner.OwnershipTypeId;
                    ActivityGoalId = Partner.ActivityGoalId;
                    NationalAffiliationId = Partner.NationalAffiliationId;
                    AreaId = Partner.ActivityAreaId;
                    AreaProfessionalId = Partner.ActivityAreaProfessionalId;

                    OrganizationEnglishSourceId = Partner.OrganizationEnglishSourceId;
                    Source = Partner.Source;
                    EngSourceOfficial = (Partner.EngSourceOfficial.HasValue) ? (bool)Partner.EngSourceOfficial : false;
                    SourceCharter = (Partner.SourceCharter.HasValue) ? (bool)Partner.SourceCharter : false;
                    SourceEGRUL = (Partner.SourceEGRUL.HasValue) ? (bool)Partner.SourceEGRUL : false;
                    SourceSite = (Partner.SourceSite.HasValue) ? (bool)Partner.SourceSite : false;
                    CardChecked = (Partner.CardChecked.HasValue) ? (bool)Partner.CardChecked : false;
                    IsActual = (Partner.IsActual.HasValue) ? (bool)Partner.IsActual : false;
                    Description = Partner.Description;

                    INN = Partner.INN;
                    NameDate = (Partner.NameDate.HasValue) ? Partner.NameDate.Value.ToString("dd.MM.yyyy") : "";
                    OGRN = Partner.OGRN;
                    OGRNDate = (Partner.OGRNDate.HasValue) ? Partner.OGRNDate.Value.ToString("dd.MM.yyyy") : "";
                    CloseDate = (Partner.CloseDate.HasValue) ? Partner.CloseDate.Value.ToString("dd.MM.yyyy") : "";
                    Email = Partner.Email;
                    WebSite = Partner.WebSite;
                    Fax = Partner.Fax;
                    Phone = Partner.Phone;
                    Mobiles = Partner.Mobiles;
                    CountryId = Partner.CountryId;
                    FillCountry(CountryId);
                    RegionId = Partner.RegionId;
                    City = Partner.City;
                    FillRegion(RegionId);
                    RegionCode = (RegionId.HasValue) ? Util.lstRegionCode.Where(x => x.Key == RegionId.Value).Select(x => x.Value).First() : "";
                    CodeKLADR = Partner.CodeKLADR;
                    Street = Partner.Street;
                    lbStreet.Visible = false;
                    House = Partner.House;
                    Apartment = Partner.Apartment;
                    Address = Partner.Address;
                    lbHouse.Visible = false;
                    Code = Partner.Code;
                    Comment = Partner.Comment;
                    try
                    {
                        tbAuthor.Text = "Карточка заведена пользователем: " + Util.GetADUserName(Partner.Author) +  "  " +  Partner.DateCreated.Date.ToString("dd.MM.yyyy");
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show(ex.Message, "Инфо");  
                    }
                    
                    this.Text = "Карточка: " + ((String.IsNullOrEmpty(ShortName)) ? ((String.IsNullOrEmpty(MiddleName)) ? OrgName : MiddleName) : ShortName);
                }
        }
        private void FillDogovor()
        {
            FillDogovor(null);
        }
        private void FillDogovor(int? Id)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                try
                {
                    var lst = (from x in context.OrganizationDogovor
                               join r in context.Rubric on x.RubricId equals r.Id
                               join dtype in context.DocumentType on x.DocumentTypeId equals dtype.Id
                               where x.OrganizationId == _Id.Value
                               orderby r.Id, x.Permanent descending, x.DocumentFinish descending
                               select new
                               {
                                   Рубрика = r.Name,
                                   x.Id,
                                   x.OrganizationId,
                                   Тип_документа = dtype.Name,
                                   Договор = x.Document,
                                   Номер_договора = x.DocumentNumber,
                                   Дата_подписания = x.DocumentDate,
                                   Действует_с_момента_подписания = x.FromDocumentDate,
                                   Дата_начала = x.DocumentStart,
                                   Дата_окончания = x.DocumentFinish,
                                   Бессрочный = x.Permanent,
                                   Адрес_проведения_практики = x.Address,
                                   Примечание = x.Comment,
                                   Действующий = x.IsActual
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    dgvDogovor.DataSource = dt;

                    foreach (string s in new List<string>() { "Id", "OrganizationId" })
                        if (dgvDogovor.Columns.Contains(s))
                            dgvDogovor.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvDogovor.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnDelDogovor")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnEditDogovor")
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
                        dgvDogovor.Columns["ColumnDiv1"].Width = 6;
                        dgvDogovor.Columns["ColumnDelDogovor"].Width = 70;
                        dgvDogovor.Columns["ColumnEditDogovor"].Width = 100;
                        dgvDogovor.Columns["Рубрика"].Frozen = true;
                        dgvDogovor.Columns["Тип_документа"].Width = 80;
                        dgvDogovor.Columns["Рубрика"].Width = 150;
                        dgvDogovor.Columns["Договор"].Width = 300;
                        dgvDogovor.Columns["Бессрочный"].Width = 80;
                        dgvDogovor.Columns["Адрес_проведения_практики"].Width = 200;
                        dgvDogovor.Columns["Примечание"].Width = 200;
                    }
                    catch (Exception)
                    {
                    }
                    //if (id.HasValue)
                    //    foreach (DataGridViewRow rw in dgvDogovor.Rows)
                    //        if (rw.Cells[0].Value.ToString() == id.Value.ToString())
                    //        {
                    //            dgvDogovor.CurrentCell = rw.Cells["Рубрика"];
                    //            break;
                    //        }
                }
                catch (Exception)
                {
                }
            }
        }
        private void FillOrganizationArea()
        {
            FillOrganizationArea(null);
        }
        private void FillOrganizationArea(int? id)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.OrganizationActivityArea
                           join a in context.ActivityArea on x.ActivityAreaId equals a.Id
                           where x.OrganizationId == _Id.Value
                           orderby a.Name 
                           select new 
                           {
                               x.Id,
                               Название = a.Name,
                           }).ToList();


                DataTable dt = new DataTable();
                dt = Utilities.ConvertToDataTable(lst);
                dgvArea.DataSource = dt;
                dgvActivityArea.DataSource = dt;

                foreach (string s in new List<string>() { "Id" })
                {
                    if (dgvArea.Columns.Contains(s))
                        dgvArea.Columns[s].Visible = false;
                    if (dgvActivityArea.Columns.Contains(s))
                        dgvActivityArea.Columns[s].Visible = false;
                }
                if (id.HasValue)
                    foreach (DataGridViewRow rw in dgvArea.Rows)
                        if (rw.Cells[0].Value.ToString() == id.Value.ToString())
                        {
                            dgvArea.CurrentCell = rw.Cells["Название"];
                            dgvActivityArea.CurrentCell = rw.Cells["Название"];
                            break;
                        }
            }
        }
        private void FillOrgAreaProfessional()
        {
            FillOrgAreaProfessional(null);
        }
        private void FillOrgAreaProfessional(int? id)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.OrganizationActivityAreaProfessional
                           join a in context.ActivityAreaProfessional on x.ActivityAreaProfessionalId equals a.Id
                           where x.OrganizationId == _Id.Value
                           orderby a.Id
                           select new
                           {
                               Код = a.Code,
                               Область_деятельности = a.Name,
                               x.Id,
                           }).ToList();

                DataTable dt = new DataTable();
                dt = Utilities.ConvertToDataTable(lst);
                dgvActivityAreaProfessional.DataSource = dt;

                foreach (string s in new List<string>() { "Id" })
                {
                    if (dgvActivityAreaProfessional.Columns.Contains(s))
                        dgvActivityAreaProfessional.Columns[s].Visible = false;
                }
                foreach (DataGridViewColumn col in dgvActivityAreaProfessional.Columns)
                    col.HeaderText = col.Name.Replace("_", " ");
                try
                {
                    //dgv.Columns["Область_деятельности"].Frozen = true;
                    dgvActivityAreaProfessional.Columns["Область_деятельности"].Width = 450;
                }
                catch (Exception)
                {
                }
                //if (id.HasValue)
                //    foreach (DataGridViewRow rw in dgvArea.Rows)
                //        if (rw.Cells[0].Value.ToString() == id.Value.ToString())
                //        {
                //            dgvArea.CurrentCell = rw.Cells["Название"];
                //            dgvActivityArea.CurrentCell = rw.Cells["Название"];
                //            break;
                //        }
            }
        }
        private void FillOkvedCode()
        {
            FillOkvedCode(null);
        }
        private void FillOkvedCode(int? id)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.OrganizationOkved
                           where x.OrganizationId == _Id.Value
                           select new
                           {
                               x.Id,
                               ОКВЭД = x.Okved,
                               Тип = x.OkvedType.Name,
                               Название = x.OkvedName
                           }).ToList();


                DataTable dt = new DataTable();
                dt = Utilities.ConvertToDataTable(lst);
                dgvOkvedCodes.DataSource = dt;

                foreach (string s in new List<string>() { "Id" })
                    if (dgvOkvedCodes.Columns.Contains(s))
                        dgvOkvedCodes.Columns[s].Visible = false;
                if (id.HasValue)
                    foreach (DataGridViewRow rw in dgvOkvedCodes.Rows)
                        if (rw.Cells[0].Value.ToString() == id.Value.ToString())
                        {
                            dgvOkvedCodes.CurrentCell = rw.Cells["ОКВЭД"];
                            break;
                        }
            }
        }
        private void FillOrganizationPerson()
        {
            FillOrganizationPerson(null);
        }
        private void FillOrganizationPerson(int? id)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.OrganizationPerson
                               join p in context.PartnerPerson on x.PartnerPersonId equals p.Id
                               where x.OrganizationId == _Id.Value
                               select new
                               {
                                   x.Id,
                                   PersonId = p.Id,
                                   ФИО = p.Name,
                                   //Должность = x.Position + " (" + p.Title + ")",
                                   Должность = x.Position,
                                   Должность_англ = x.PositionEng,
                                   Комментарий = x.Comment,
                                   p.Email,
                                   Телефон = p.Phone,
                                   Мобильный = p.Mobiles,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    dgvContacts.DataSource = dt;

                    foreach (string s in new List<string>() { "Id", "PersonId" })
                        if (dgvContacts.Columns.Contains(s))
                            dgvContacts.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvContacts.Columns)
                        col.HeaderText = col.Name.Replace("_", " ");
                    if (id.HasValue)
                        foreach (DataGridViewRow rw in dgvContacts.Rows)
                            if (rw.Cells[0].Value.ToString() == id.Value.ToString())
                            {
                                dgvContacts.CurrentCell = rw.Cells["ФИО"];
                                break;
                            }
                }
            }
            catch (Exception)
            {
            }
        }

        private void FillOrganizationSubdivision()
        {
            FillOrganizationSubdivision(null);
        }
        private void FillOrganizationSubdivision(int? id)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.OrganizationSubdivision
                               //join org in context.Organization on x.OrganizationId equals org.Id
                               where x.OrganizationId == _Id.Value
                               select new
                               {
                                   x.Id,
                                   Подразделение = x.Name,
                                   Подразделение_англ = x.NameEng,
                                   Комментарий = x.Comment,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    dgvSubdivisions.DataSource = dt;

                    foreach (string s in new List<string>() { "Id" })
                        if (dgvSubdivisions.Columns.Contains(s))
                            dgvSubdivisions.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvSubdivisions.Columns)
                        col.HeaderText = col.Name.Replace("_", " ");
                    if (id.HasValue)
                        foreach (DataGridViewRow rw in dgvSubdivisions.Rows)
                            if (rw.Cells[0].Value.ToString() == id.Value.ToString())
                            {
                                dgvContacts.CurrentCell = rw.Cells["Подразделение"];
                                break;
                            }
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region CheckSaveUpdate_CommonInformation
        private bool CheckFields()
        {
            if (String.IsNullOrEmpty(OrgName))
            {
                err.SetError(tbName, "Не задано полное наименование организации");
                tabControl1.SelectedTab = tabPage1;
                return false;
            }
            else
                err.Clear();

            if (String.IsNullOrEmpty(INN))
            {
                if (MessageBox.Show("Вообще говоря, ввод ИНН является обязательным.\r\n" + "Продолжить соханение данных без уаказания ИНН?", "Запрос на подтверждение", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    return true;
                err.SetError(tbINN, "Не указан ИНН");
                tabControl1.SelectedTab = tabPage1;
                return false;
            }
            else
                err.Clear();

            if (!String.IsNullOrEmpty(INN))
            {
                try
                {
                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        var lst = (from x  in context.Organization
                                   where (x.INN == INN) && (x.Id != OrgId)
                                   select x.Id).ToList();
                        if (lst.Count() > 0)
                        {
                            var org = context.Organization.Where(x => x.INN == INN).First();
                            err.SetError(tbINN, "Такой ИНН уже существует у организации\r\n" + org.Name);
                            MessageBox.Show("Такой ИНН уже существует у организации\r\n" + org.Name,"Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            tabControl1.SelectedTab = tabPage1;
                            return false;
                        }
                    }
                }
                catch (Exception)
                {
                    return true;
                } 
                
            }
            else
                err.Clear();

            DateTime res;
            if (!String.IsNullOrEmpty(OGRNDate))
            {
                if (!DateTime.TryParse(OGRNDate, out res))
                {
                    err.SetError(tbOGRNDate, "Неправильный формат даты в поле 'Дата присвоения ОГРН' \r\n" + "Образец: 01.12.2016");
                    MessageBox.Show("Неправильный формат даты в поле 'Дата присвоения ОГРН' \r\n" + "Образец: 01.12.2016", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            else
                err.Clear();

            if (!String.IsNullOrEmpty(CloseDate))
            {
                if (!DateTime.TryParse(CloseDate, out res))
                {
                    err.SetError(tbOGRNDate, "Неправильный формат даты в поле 'Дата прекращения деятельности' \r\n" + "Образец: 01.12.2016");
                    MessageBox.Show("Неправильный формат даты в поле 'Дата прекращения деятельности' \r\n" + "Образец: 01.12.2016", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }
            }
            else
                err.Clear();

            if (!CountryId.HasValue)
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var x = (from c in context.Country
                             where c.Name == tbCountry.Text.Trim()
                             select c.Id).ToList();
                    if (x.Count() == 1)
                    {
                        CountryId = x.First();
                        err.Clear();
                    }
                    else
                    {
                        err.SetError(tbCountry, "не выбрана страна");
                        tabControl1.SelectedTab = tabPage1;
                        return false;
                    }
                }
            }
            else err.Clear();

            if (!RegionId.HasValue && CountryId == Util.countryRussiaId)
            {
                err.SetError(tbRegion, "не выбран регион");
                tabControl1.SelectedTab = tabPage1;
                return false;
            }
            else err.Clear();

            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckFields())
                return;

            string str = "";
            if (_Id == null)
            {
                if (!OrganizationRecInsert())
                    return;
                str = "Сохранение успешно завершено";
            }
            else
            {
                if (!OrganizationRecUpdate())
                    return;
                str = "Обновление данных успешно завершено";
            }

            if (_hndl != null)
                _hndl(_Id);

            Timer t = new Timer();
            t.Interval = 5 * 1000; // секунды * 1000
            t.Tick += new EventHandler(t_Tick);
            SaveSuccess.SetError(btnSave, str);
            SaveSuccess.BlinkRate = 0;
            SaveSuccess.SetIconAlignment(btnSave, ErrorIconAlignment.MiddleLeft);
            t.Start();
        }
        private bool OrganizationRecInsert()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    Guid gId = Guid.NewGuid();
                    context.Organization.Add(new Organization()
                    {
                        rowguid = gId,
                        Name = OrgName,
                        NameEng = NameEng,
                        MiddleName = MiddleName,
                        ShortName = ShortName,
                        ShortNameEng = ShortNameEng,

                        Okved = okved,
                        OkvedDop = okveddop,
                        OECD = oecd,
                        NationalAffiliationId = NationalAffiliationId,
                        OwnershipTypeId = OwnershipTypeId,
                        ActivityGoalId = ActivityGoalId,
                        ActivityAreaId = AreaId,
                        OrganizationEnglishSourceId = OrganizationEnglishSourceId,

                        INN = INN,
                        OGRN = OGRN,
                        //OGRNDate
                        //CloseDate
                        Email = Email,
                        WebSite = WebSite,
                        CountryId = CountryId,
                        RegionId = RegionId,
                        City = City,
                        Street = Street,
                        House = House,
                        Apartment = Apartment,
                        Address = Address,
                        Code = Code,
                        CodeKLADR = CodeKLADR,
                        Fax = Fax,
                        Phone = Phone,
                        Mobiles = Mobiles,
                        Comment = Comment,
                        Description = Description,
                        Source = Source,

                    });
                    context.SaveChanges();
                    _Id = context.Organization.Where(x => x.rowguid == gId).Select(x => x.Id).First();
                    return true;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка при сохранении карточки\r\n" + exc.InnerException, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
        private bool OrganizationRecUpdate()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var Org = context.Organization.Where(x => x.Id == _Id).First();
                    Org.Name = OrgName;
                    Org.NameEng = NameEng;
                    Org.MiddleName = MiddleName;
                    Org.ShortName = ShortName;
                    Org.ShortNameEng = ShortNameEng;

                    Org.Okved = okved;
                    Org.OkvedDop = okveddop;
                    Org.OECD = oecd;
                    Org.OwnershipTypeId = OwnershipTypeId;
                    Org.ActivityGoalId = ActivityGoalId;
                    Org.NationalAffiliationId = NationalAffiliationId;
                    Org.ActivityAreaId = AreaId;
                    Org.ActivityAreaProfessionalId = AreaProfessionalId;
                    Org.OrganizationEnglishSourceId = OrganizationEnglishSourceId;

                    Org.INN = INN;
                    if (!String.IsNullOrEmpty(NameDate))
                    {
                        Org.NameDate = DateTime.Parse(NameDate);
                    }
                    else
                    {
                        Org.NameDate = null;
                    }
                    Org.OGRN = OGRN;
                    if (!String.IsNullOrEmpty(OGRNDate))
                    {
                        Org.OGRNDate = DateTime.Parse(OGRNDate);
                    }
                    else
                    {
                        Org.OGRNDate = null;
                    }
                    if (!String.IsNullOrEmpty(CloseDate))
                    {
                        Org.CloseDate = DateTime.Parse(CloseDate);
                    }
                    else
                    {
                        Org.CloseDate = null;
                    }
                    Org.Email = Email;
                    Org.WebSite = WebSite;
                    Org.Fax = Fax;
                    Org.Phone = Phone;
                    Org.Mobiles = Mobiles;

                    Org.CountryId = CountryId;
                    Org.RegionId = RegionId;
                    Org.City = City;
                    Org.Street = Street;
                    Org.House = House;
                    Org.Apartment = Apartment;
                    Org.Address = Address;
                    Org.Code = Code;
                    Org.CodeKLADR = CodeKLADR;

                    Org.Comment = Comment;
                    Org.Source = Source;
                    Org.EngSourceOfficial = chkbEngSourceOfficial.Checked ? true : false;
                    Org.SourceCharter = chkbSourceCharter.Checked ? true : false;
                    Org.SourceEGRUL = chkbSourceEGRUL.Checked ? true : false;
                    Org.SourceSite = chkbSourceSite.Checked ? true : false;
                    Org.CardChecked = chkbCardChecked.Checked ? true : false;
                    Org.IsActual = chkbIsActual.Checked ? true : false;
                    Org.Description = Description;

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при сохранении карточки \r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
        
        private void t_Tick(object sender, EventArgs e)
        {
            Timer t = (Timer)sender;
            t.Stop();
            SaveSuccess.Clear();
        }
        #endregion

        #region Country
        private void FillCountry()
        {
            List<KeyValuePair<int, string>> klst = (from x in Util.lstCountry
                                                    where x.Value.ToLower().Contains(tbCountry.Text.ToLower())
                                                    select x).ToList();
            lbCountry.DataSource = klst;
            lbCountry.DisplayMember = "Value";
        }
        private void FillCountry(int? id)
        {
            tbCountry.TextChanged -= new EventHandler(tbCountry_TextChanged);
            if (id.HasValue)
            {
                tbCountry.Text = (from x in Util.lstCountry
                                  where x.Key == id.Value
                                  select x.Value).First();
            }
            tbCountry.TextChanged += new EventHandler(tbCountry_TextChanged);
        }
        private void tbCountry_TextChanged(object sender, EventArgs e)
        {
            lbCountry.Visible = true;
            CountryId = null;
            FillCountry();
        }
        private void lbCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lbCountry.SelectedValue != null)
            {
                sCountry = ((KeyValuePair<int, string>)lbCountry.SelectedItem).Value.ToString();
                CountryId = int.Parse(((KeyValuePair<int, string>)lbCountry.SelectedItem).Key.ToString());
                lbCountry.Visible = false;
                UpdateVisibleRegion();
                if (CountryId != Util.countryRussiaId)
                    CodeKLADR = "";
            }
        }
        private void lbCountry_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbCountry.SelectedValue != null)
            {
                sCountry = ((KeyValuePair<int, string>)lbCountry.SelectedItem).Value.ToString();
                CountryId = int.Parse(((KeyValuePair<int, string>)lbCountry.SelectedItem).Key.ToString());
                lbCountry.Visible = false;
                UpdateVisibleRegion();
                if (CountryId != Util.countryRussiaId)
                    CodeKLADR = "";
            }
        }
        private void tbCountry_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            lbCountry.Visible = true;
            if (CountryId == null)
            {
                CountryId = Util.countryRussiaId;
                FillCountry(CountryId);
                FillCountry();
                lbCountry.Visible = false;
                UpdateVisibleRegion();
            }
        }
        private void UpdateVisibleRegion()
        {
            if (CountryId != Util.countryRussiaId)
            {
                lbRegion.Visible = false;
                tbRegion.TextChanged -= tbRegion_TextChanged;
                tbRegion.Text = "";
                tbRegion.TextChanged += tbRegion_TextChanged;

                tbRegion.Enabled = false;
                RegionId = null;
                RegionCode = "";
            }
            else
                tbRegion.Enabled = true;
        }
        private void tbCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                lbCountry.Focus();
                if (lbCountry.Items.Count > lbCountry.SelectedIndex + 1)
                    lbCountry.SetSelected(lbCountry.SelectedIndex + 1, true);
            }
            else
                if (e.KeyCode == Keys.Enter)
                {
                    if (lbCountry.SelectedValue != null)
                    {
                        sCountry = ((KeyValuePair<int, string>)lbCountry.SelectedItem).Value.ToString();
                        CountryId = int.Parse(((KeyValuePair<int, string>)lbCountry.SelectedItem).Key.ToString());
                        lbCountry.Visible = false;
                        UpdateVisibleRegion();
                        if (CountryId != Util.countryRussiaId)
                            CodeKLADR = "";
                    }
                }
        }
        #endregion

        #region Region
        private void tbRegion_TextChanged(object sender, EventArgs e)
        {
            lbRegion.Visible = (CountryId == Util.countryRussiaId);
            RegionId = null;
            RegionCode = "";
            FillRegion();
        }
        private void FillRegion()
        {
            List<KeyValuePair<int, string>> klst = (from x in Util.lstRegion
                                                    where x.Value.ToLower().Contains(sRegion.ToLower())
                                                    select x).ToList();
            lbRegion.DataSource = klst;
            lbRegion.DisplayMember = "Value";
        }
        private void FillRegion(int? id)
        {
            if (CountryId != Util.countryRussiaId)
            {
                lbRegion.Visible = false;
                tbRegion.Text = "";
                tbRegion.Enabled = false;
                RegionId = null;
                RegionCode = "";
            }
            else
            {
                tbRegion.Enabled = true;
                tbRegion.TextChanged -= new EventHandler(tbRegion_TextChanged);
                if (id.HasValue)
                {
                    tbRegion.Text = (from x in Util.lstRegion
                                     where x.Key == id.Value
                                     select x.Value).First();
                }
                tbRegion.TextChanged += new EventHandler(tbRegion_TextChanged);
            }
        }
        private void lbRegion_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbRegion.SelectedValue != null)
            {
                sRegion = ((KeyValuePair<int, string>)lbRegion.SelectedItem).Value.ToString();
                RegionId = int.Parse(((KeyValuePair<int, string>)lbRegion.SelectedItem).Key.ToString());
                RegionCode = Util.lstRegionCode.Where(x => x.Key == RegionId).Select(x => x.Value).First();
                lbRegion.Visible = false;
            }
        }
        private void lbRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lbRegion.SelectedValue != null)
            {
                sRegion = ((KeyValuePair<int, string>)lbRegion.SelectedItem).Value.ToString();
                RegionId = int.Parse(((KeyValuePair<int, string>)lbRegion.SelectedItem).Key.ToString());
                RegionCode = Util.lstRegionCode.Where(x => x.Key == RegionId).Select(x => x.Value).First();
                lbRegion.Visible = false;
            }
        }
        private void tbRegion_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbRegion.Visible)
                if (e.KeyCode == Keys.Down)
                {
                    lbRegion.Focus();
                    if (lbRegion.Items.Count > lbRegion.SelectedIndex + 1)
                        lbRegion.SetSelected(lbRegion.SelectedIndex + 1, true);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (lbRegion.SelectedValue != null)
                    {
                        sRegion = ((KeyValuePair<int, string>)lbRegion.SelectedItem).Value.ToString();
                        RegionId = int.Parse(((KeyValuePair<int, string>)lbRegion.SelectedItem).Key.ToString());
                        RegionCode = Util.lstRegionCode.Where(x => x.Key == RegionId).Select(x => x.Value).First();
                        lbRegion.Visible = false;
                    }
                }
        }
        #endregion

        #region City
        private void lbCity_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbCity.SelectedValue != null)
            {
                City = (string)lbCity.SelectedItem;
                GetCodeKLADR();
                lbCity.Visible = false;
            }
        }
        private void tbCity_TextChanged(object sender, EventArgs e)
        {
            lbCity.Visible = (CountryId == Util.countryRussiaId);
            FillCity();
        }
        private void FillCity()
        {
            if (!string.IsNullOrEmpty(RegionCode) && CountryId == Util.countryRussiaId)
            {
                List<string> lst = KLADR.KLADR.GetCitiesInRegion(RegionCode).Where(x => x.ToLower().Contains(City.ToLower())).ToList();
                lbCity.DataSource = lst;
            }
            else if (CountryId != Util.countryRussiaId)
            {
                lbCity.DataSource = Util.lstCountryCity.Where(x => x.Key == CountryId && x.Value.ToLower().Contains(City.ToLower())).Select(x => x.Value).ToList();
                if (lbCity.Items.Count > 0)
                    lbCity.Visible = true;
            }
        }
        private void lbCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lbCity.SelectedValue != null)
            {
                City = (string)lbCity.SelectedItem;
                GetCodeKLADR();
                lbCity.Visible = false;
            }
        }
        private void tbCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbCity.Visible)
                if (e.KeyCode == Keys.Down)
                {
                    lbCity.Focus();
                    if (lbCity.Items.Count > lbCity.SelectedIndex + 1)
                        lbCity.SetSelected(lbCity.SelectedIndex + 1, true);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (lbCity.SelectedValue != null)
                    {
                        City = (string)lbCity.SelectedItem;
                        GetCodeKLADR();
                        lbCity.Visible = false;
                    }
                }
        }
        #endregion

        #region Street
        private void lbStreet_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbStreet.SelectedValue != null)
            {
                Street = (string)lbStreet.SelectedItem;
                GetCodeKLADR();
                lbStreet.Visible = false;
            }
        }
        private void tbStreet_TextChanged(object sender, EventArgs e)
        {
            lbStreet.Visible = (CountryId == Util.countryRussiaId);
            FillStreet();
            GetCodeKLADR();
        }
        private void FillStreet()
        {
            if (!String.IsNullOrEmpty(RegionCode) && CountryId == Util.countryRussiaId && !String.IsNullOrEmpty(City))
            {
                List<string> lstStreet = KLADR.KLADR.GetStreetsInCity(RegionCode, City).Where(x => x.ToLower().Contains(Street.ToLower())).ToList();
                lbStreet.DataSource = lstStreet;
            }
            else if (CountryId != Util.countryRussiaId && CountryId != null)
            {
                lbStreet.DataSource = Util.lstCityStreet.Where(x => x.Key.ToLower() == City.ToLower() && x.Value.ToLower().Contains(Street.ToLower())).Select(x => x.Value).ToList();
                if (lbStreet.Items.Count > 0)
                    lbStreet.Visible = true;
            }
        }
        private void lbStreet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lbStreet.SelectedValue != null)
            {
                Street = (string)lbStreet.SelectedItem;
                GetCodeKLADR();
                lbStreet.Visible = false;
            }
        }
        private void tbStreet_KeyDown(object sender, KeyEventArgs e)
        {
            if (lbStreet.Visible)
                if (e.KeyCode == Keys.Down)
                {
                    lbStreet.Focus();
                    if (lbStreet.Items.Count > lbStreet.SelectedIndex + 1)
                        lbStreet.SetSelected(lbStreet.SelectedIndex + 1, true);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (lbStreet.SelectedValue != null)
                    {
                        Street = (string)lbStreet.SelectedItem;
                        lbStreet.Visible = false;
                    }
                }
        }
        #endregion

        #region House
        private void lbHouse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && lbStreet.SelectedValue != null)
            {
                House = (string)lbHouse.SelectedItem;
                GetCodeKLADR();
                lbHouse.Visible = false;
            }
        }
        private void lbHouse_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbHouse.SelectedValue != null)
            {
                House = (string)lbHouse.SelectedItem;
                GetCodeKLADR();
                lbHouse.Visible = false;
            }
        }
        private void tbHouse_TextChanged(object sender, EventArgs e)
        {
            lbHouse.Visible = (CountryId == Util.countryRussiaId);
            FillHouses();
        }
        private void tbHouse_KeyDown(object sender, KeyEventArgs e)
        {
            if (CountryId == Util.countryRussiaId)
                if (e.KeyCode == Keys.Down)
                {
                    lbHouse.Focus();
                    if (lbHouse.Items.Count > lbHouse.SelectedIndex + 1)
                        lbHouse.SetSelected(lbHouse.SelectedIndex + 1, true);
                }
                else
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (lbHouse.SelectedValue != null)
                        {
                            House = (string)lbHouse.SelectedItem;
                            GetCodeKLADR();
                            lbHouse.Visible = false;
                        }
                    }
        }
        private void FillHouses()
        {
            if (!String.IsNullOrEmpty(RegionCode) && CountryId == Util.countryRussiaId && !String.IsNullOrEmpty(City) && !String.IsNullOrEmpty(Street))
            {
                List<string> lstHouses = KLADR.KLADR.GetHouses(RegionCode, City, Street).Where(x => x.ToLower().Contains(House.ToLower())).ToList();
                lbHouse.DataSource = lstHouses;
            }
        }
        #endregion

        #region PostCode & CodeKLADR
        private void GetCodeKLADR()
        {
            if (!string.IsNullOrEmpty(RegionCode) && CountryId == Util.countryRussiaId)
            {
                CodeKLADR =
                     (!String.IsNullOrEmpty(Street) && !String.IsNullOrEmpty(House)) ? KLADR.KLADR.GetKLADRCode(RegionCode, City, Street, House) :
                     (!String.IsNullOrEmpty(Street) ? KLADR.KLADR.GetKLADRCode(RegionCode, City, Street) :
                     KLADR.KLADR.GetKLADRCode(RegionCode, City))
                     ;

                Code =
                    (!String.IsNullOrEmpty(Street) && !String.IsNullOrEmpty(House)) ? KLADR.KLADR.GetPostIndex(RegionCode, City, Street, House) :
                    (!String.IsNullOrEmpty(Street) ? KLADR.KLADR.GetPostIndex(RegionCode, City, Street) :
                    KLADR.KLADR.GetPostIndex(RegionCode, City))
                    ;
            }
            else
            {
                CodeKLADR = Code = string.Empty;
            }
        }
        #endregion

        #region Okved
        private void btnAddOkved_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
            {
                MessageBox.Show("Сначала сохраните карточку Организации");
                return;
            }
            new CardOrganizationOkved(null, _Id.Value, new UpdateVoidHandler(FillOkvedCode)).Show();
        }
        private void dgvOkvedCodes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Util.IsReadOnlyAll())
                return;
            if (!Util.IsOrgPersonWrite())
                return;

            if (_Id.HasValue)
                if (dgvOkvedCodes.CurrentCell != null)
                    if (dgvOkvedCodes.CurrentRow.Index >= 0)
                    {
                        int id = int.Parse(dgvOkvedCodes.CurrentRow.Cells["Id"].Value.ToString());
                        new CardOrganizationOkved(id, _Id.Value, new UpdateVoidHandler(FillOkvedCode)).Show();
                    }
        }
        #endregion

        #region Contact
        private void btnContactAdd_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
            {
                MessageBox.Show("Сначала сохраните карточку Организации");
                return;
            }
            new CardOrganizationPerson(null, _Id.Value, new UpdateVoidHandler(FillOrganizationPerson)).Show();
        }
        private void dgvContacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Util.IsOrgPersonWrite())
            {
                if (_Id.HasValue)
                    if (dgvContacts.CurrentCell != null)
                        if (dgvContacts.CurrentRow.Index >= 0)
                        {
                            int id = int.Parse(dgvContacts.CurrentRow.Cells["Id"].Value.ToString());
                            new CardOrganizationPerson(id, _Id.Value, new UpdateVoidHandler(FillOrganizationPerson)).Show();
                        }
            }
        }
        private void btnDeleteContact_Click(object sender, EventArgs e)
        {
            if (_Id.HasValue)
                if (dgvContacts.CurrentCell != null)
                    if (dgvContacts.CurrentRow.Index >= 0)
                    {
                        if (MessageBox.Show("Удалить выбранный контакт? \r\n" + dgvContacts.CurrentRow.Cells["ФИО"].Value.ToString(), "Запрос на подтверждение", 
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                            return;

                        int id = int.Parse(dgvContacts.CurrentRow.Cells["Id"].Value.ToString());
                        using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                        {
                            context.OrganizationPerson.RemoveRange(context.OrganizationPerson.Where(x => x.Id == id));
                            context.SaveChanges();
                        }
                        FillOrganizationPerson();
                    }
        }
        private void btnPersonCardOpen_Click(object sender, EventArgs e)
        {
            if (_Id.HasValue)
                if (dgvContacts.CurrentCell != null)
                    if (dgvContacts.CurrentRow.Index >= 0)
                    {
                        int id = int.Parse(dgvContacts.CurrentRow.Cells["PersonId"].Value.ToString());
                        if (Utilities.PersonCardIsOpened(id))
                            return;
                        new CardPerson(id, new UpdateVoidHandler(FillOrganizationPerson)).Show();
                    }
        }
        #endregion

        #region Area
        private void btnAreaAdd_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
            {
                MessageBox.Show("Сначала сохраните карточку Организации");
                return;
            }
            new CardOrganizationArea(null, _Id.Value, new UpdateVoidHandler(FillOrganizationArea)).Show();
        }
        private void dgvActivityArea_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Util.IsOrgPersonWrite())
            {
                DataGridView dgv = (DataGridView)sender;
                if (_Id.HasValue)
                    if (dgv.CurrentCell != null)
                        if (dgv.CurrentRow.Index >= 0)
                        {
                            int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                            new CardOrganizationArea(id, _Id.Value, new UpdateVoidHandler(FillOrganizationArea)).Show();
                        }
            }
        }
        private void btnAreaDelete_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgvArea;
            if (sender == btnActivityAreaDelete)
                dgv = dgvActivityArea;
            else if (sender == btnAreaDelete)
                dgv = dgvArea;

            if (_Id.HasValue)
                if (dgv.CurrentCell != null)
                    if (dgv.CurrentRow.Index >= 0)
                    {
                        int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                        if (MessageBox.Show("Удалить выбранное ключевое слово? \r\n" + dgvActivityArea.CurrentRow.Cells["Название"].Value.ToString(), "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                context.OrganizationActivityArea.RemoveRange(context.OrganizationActivityArea.Where(x => x.Id == id));
                                context.SaveChanges();
                            }
                            FillOrganizationArea();
                        }
                        else
                            return;
                    }
        }
        #endregion

        #region Rubric
        public void FillRubrics()
        { FillRubrics(null); }
        public void FillRubrics(int? id)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.OrganizationRubric
                           join r in context.Rubric on x.RubricId equals r.Id
                           where x.OrganizationId == _Id
                           select new
                           {
                               x.Id,
                               Рубрика = r.Name
                           }).ToList();


                DataTable dt = new DataTable();
                dt = Utilities.ConvertToDataTable(lst);
                dgvRubric.DataSource = dt;

                foreach (string s in new List<string>() { "Id" })
                    if (dgvRubric.Columns.Contains(s))
                        dgvRubric.Columns[s].Visible = false;
                if (id.HasValue)
                    foreach (DataGridViewRow rw in dgvRubric.Rows)
                        if (rw.Cells[0].Value.ToString() == id.Value.ToString())
                        {
                            dgvRubric.CurrentCell = rw.Cells["Рубрика"];
                            break;
                        }
            }
        }
        private void btnRublrikAdd_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
            {
                MessageBox.Show("Сначала сохраните карточку Организации");
                return;
            }
            new CardOrganizationRubric(null, _Id.Value, new UpdateVoidHandler(FillRubrics)).Show();
        }
        private void btnRubricDelete_Click(object sender, EventArgs e)
        {
            if (_Id.HasValue)
                if (dgvRubric.CurrentCell != null)
                    if (dgvRubric.CurrentRow.Index >= 0)
                    {
                        int id = int.Parse(dgvRubric.CurrentRow.Cells["Id"].Value.ToString());
                        string sRubric = "";
                        try
                        {
                            sRubric = dgvRubric.CurrentRow.Cells["Рубрика"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        if (MessageBox.Show("Удалить выбранную рубрику? \r\n" + sRubric, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                context.OrganizationRubric.RemoveRange(context.OrganizationRubric.Where(x => x.Id == id));
                                context.SaveChanges();
                            }
                            FillRubrics();
                        }
                        else
                            return;
                    }
        }
        private void dgvRubric_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Util.IsOrgPersonWrite())
            {
                if (_Id.HasValue)
                    if (dgvRubric.CurrentCell != null)
                        if (dgvRubric.CurrentRow.Index >= 0)
                        {
                            int id = int.Parse(dgvRubric.CurrentRow.Cells["Id"].Value.ToString());
                            new CardOrganizationRubric(id, _Id.Value, new UpdateVoidHandler(FillRubrics)).Show();
                        }
            }
        }
        #endregion

        #region Faculty
        private void FillFaculty()
        {
            FillFaculty(null);
        }
        private void FillFaculty(int? id)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.OrganizationFaculty
                            
                           join f in context.Faculty on x.FacultyId equals f.Id

                           join r in context.Rubric on x.RubricId equals r.Id into _r
                           from r in _r.DefaultIfEmpty()

                           where x.OrganizationId == _Id
                           select new
                           {
                               x.Id,
                               Рубрика = r.ShortName,
                               Направление = f.Name
                           }).ToList();

                DataTable dt = new DataTable();
                dt = Utilities.ConvertToDataTable(lst);
                dgvFaculty.DataSource = dt;

                foreach (string s in new List<string>() { "Id" })
                    if (dgvFaculty.Columns.Contains(s))
                        dgvFaculty.Columns[s].Visible = false;
                if (id.HasValue)
                    foreach (DataGridViewRow rw in dgvFaculty.Rows)
                        if (rw.Cells[0].Value.ToString() == id.Value.ToString())
                        {
                            dgvFaculty.CurrentCell = rw.Cells["Рубрика"];
                            break;
                        }
            }
        }
        private void btnFacultyAdd_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
            {
                MessageBox.Show("Сначала сохраните карточку Организации");
                return;
            }
            new CardOrganizationFaculty(null, _Id.Value, new UpdateVoidHandler(FillFaculty)).Show();
        }
        private void btnFacultyDelete_Click(object sender, EventArgs e)
        {
            if (_Id.HasValue)
                if (dgvFaculty.CurrentCell != null)
                    if (dgvFaculty.CurrentRow.Index >= 0)
                    {
                        int id = int.Parse(dgvFaculty.CurrentRow.Cells["Id"].Value.ToString());
                        string sFac = "";
                        try
                        {
                            sFac = dgvFaculty.CurrentRow.Cells["Направление"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        string sRubric = "";
                        try
                        {
                            sRubric = dgvFaculty.CurrentRow.Cells["Рубрика"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }

                        if (MessageBox.Show("Удалить выбранное направление? \r" + sFac + "\r" + sRubric, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                context.OrganizationFaculty.RemoveRange(context.OrganizationFaculty.Where(x => x.Id == id));
                                context.SaveChanges();
                            }
                            FillFaculty();
                        }
                        else
                            return;
                    }
        }
        private void dgvFaculty_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Util.IsOrgPersonWrite())
            {
                if (_Id.HasValue)
                    if (dgvFaculty.CurrentCell != null)
                        if (dgvFaculty.CurrentRow.Index >= 0)
                        {
                            int id = int.Parse(dgvFaculty.CurrentRow.Cells["Id"].Value.ToString());
                            new CardOrganizationFaculty(id, _Id.Value, new UpdateVoidHandler(FillFaculty)).Show();
                        }
            }
        }
        #endregion

        #region LP
        public void FillLP()
        {
            FillLP(null);
        }
        public void FillLP(int? id)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.OrganizationLP
                           join f in context.LicenseProgram on x.LicenseProgramId equals f.Id
                           join r in context.Rubric on x.RubricId equals r.Id into _r
                           from r in _r.DefaultIfEmpty()
                           where x.OrganizationId == _Id
                           select new
                           {
                               x.Id,
                               Рубрика = r.ShortName,
                               Код = f.Code,
                               Уровень = f.StudyLevel.Name,
                               Направление = f.Name,
                               Тип_программы = f.ProgramType.Name,
                               Квалификация = f.Qualification.Name,
                           }).ToList();

                DataTable dt = new DataTable();
                dt = Utilities.ConvertToDataTable(lst);
                dgvLP.DataSource = dt;

                foreach (string s in new List<string>() { "Id" })
                    if (dgvLP.Columns.Contains(s))
                        dgvLP.Columns[s].Visible = false;
                foreach (DataGridViewColumn col in dgvLP.Columns)
                    col.HeaderText = col.Name.Replace("_", " ");

                if (id.HasValue)
                    foreach (DataGridViewRow rw in dgvRubric.Rows)
                        if (rw.Cells[0].Value.ToString() == id.Value.ToString())
                        {
                            dgvRubric.CurrentCell = rw.Cells["ФИО"];
                            break;
                        }
            }
        }
        private void btnLPAdd_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
            {
                MessageBox.Show("Сначала сохраните карточку Организации");
                return;
            }
            new CardOrganizationLP(null, _Id.Value, new UpdateVoidHandler(FillLP)).Show();
        }
        private void btnLPDelete_Click(object sender, EventArgs e)
        {
            if (_Id.HasValue)
                if (dgvLP.CurrentCell != null)
                    if (dgvLP.CurrentRow.Index >= 0)
                    {
                        int id = int.Parse(dgvLP.CurrentRow.Cells["Id"].Value.ToString());
                        string sCode = "";
                        try
                        {
                            sCode = dgvLP.CurrentRow.Cells["Код"].Value.ToString() + "  ";
                        }
                        catch (Exception)
                        {
                        }
                        string sLPName = ""; 
                        try
                        {
                            sLPName = dgvLP.CurrentRow.Cells["Направление"].Value.ToString();
                        }
                        catch (Exception)
                        {   
                        }
                        string sRubric = "";
                        try
                        {
                            sRubric = dgvLP.CurrentRow.Cells["Рубрика"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        if (MessageBox.Show("Удалить выбранное направление подготовки? \r" + sCode + sLPName + "\r" + sRubric, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                context.OrganizationLP.RemoveRange(context.OrganizationLP.Where(x => x.Id == id));
                                context.SaveChanges();
                            }
                            FillLP();
                        }
                        else
                            return;
                    }
        }
        private void dgvLP_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Util.IsOrgPersonWrite())
            {
                if (_Id.HasValue)
                    if (dgvLP.CurrentCell != null)
                        if (dgvLP.CurrentRow.Index >= 0)
                        {
                            int id = int.Parse(dgvLP.CurrentRow.Cells["Id"].Value.ToString());
                            new CardOrganizationLP(id, _Id.Value, new UpdateVoidHandler(FillLP)).Show();
                        }
            }
        }
        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                string OrgName = "";
                try
                {
                    OrgName = tbName.Text;
                }
                catch (Exception)
                {
                }
                if (MessageBox.Show("Удалить карточку? \r\n" + OrgName, "Запрос на подтверждение", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                try
                {
                    context.Organization.Remove(context.Organization.Where(x => x.Id == _Id).First());
                    context.SaveChanges();
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось удалить карточку...\r\n" + "Обычно это связано с тем, что у данной записи имеются связанные записи в других таблицах.", "Сообщение");
                }

            }
        }

        private void BtnContactEdit_Click(object sender, EventArgs e)
        {
            if (_Id.HasValue)
                if (dgvContacts.CurrentCell != null)
                    if (dgvContacts.CurrentRow.Index >= 0)
                    {
                        int id = int.Parse(dgvContacts.CurrentRow.Cells["Id"].Value.ToString());
                        new CardOrganizationPerson(id, _Id.Value, new UpdateVoidHandler(FillOrganizationPerson)).Show();
                    }
        }

        private void btnDelOkved_Click(object sender, EventArgs e)
        {
            if (_Id.HasValue)
                if (dgvOkvedCodes.CurrentCell != null)
                    if (dgvOkvedCodes.CurrentRow.Index >= 0)
                    {
                        if (MessageBox.Show("Удалить выбранный ОКВЭД? \r" + dgvOkvedCodes.CurrentRow.Cells["ОКВЭД"].Value.ToString() + "\r" + dgvOkvedCodes.CurrentRow.Cells["Название"].Value.ToString(), "Запрос на подтверждение", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                            return;

                        int id = int.Parse(dgvOkvedCodes.CurrentRow.Cells["Id"].Value.ToString());
                        using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                        {
                            context.OrganizationOkved.RemoveRange(context.OrganizationOkved.Where(x => x.Id == id));
                            context.SaveChanges();
                        }
                        FillOkvedCode();
                    }
        }

        private void btnAddDogovor_Click(object sender, EventArgs e)
        {
            new CardOrganizationDogovor(null, (int)_Id, new UpdateVoidHandler(FillDogovor)).Show();
        }

        private void dgvDogovor_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (dgvDogovor.CurrentCell != null)
                if (dgvDogovor.CurrentRow.Index >= 0)
                {
                    if (dgvDogovor.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            dgvDogovor.CurrentCell = dgvDogovor.CurrentRow.Cells["Рубрика"];
                        }
                        catch (Exception)
                        {
                        }
                        ///
                        int id;
                        try
                        {
                            id = int.Parse(dgvDogovor.CurrentRow.Cells["Id"].Value.ToString());
                        }
                        catch (Exception)
                        {
                            return;
                        }
                        string Rubric = "";
                        try
                        {
                            Rubric = dgvDogovor.CurrentRow.Cells["Рубрика"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        string Dogovor = "";
                        try
                        {
                            Dogovor = dgvDogovor.CurrentRow.Cells["Договор"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        Dogovor = ((Rubric == "") ? "" : (Rubric + "\r\n")) + Dogovor;
                        if (MessageBox.Show("Удалить выбранный договор из списка? \r\n" + Dogovor, "Запрос на подтверждение", 
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
                        {
                            try
                            {
                                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                                {
                                    context.OrganizationDogovor.RemoveRange(context.OrganizationDogovor.Where(x => x.Id == id));
                                    context.SaveChanges();
                                }
                            }
                            catch (Exception ec)
                            {
                                MessageBox.Show("Не удалось удалить запись...\r\n" + "Причина: " + ec.Message + "\r\n" +
                                    "Примечание: \r\n" + "обычно это связано с наличием связанных записей в других таблицах.\r\n" + 
                                    "Если для данного договора загружен файл договора,\r\n" + 
                                    "то сначала необходимо удалить файл из БД (кнопка 'Просмотр/Ред.')\r\n" + 
                                    "Если этот договор фигурирует в рубрике,\r\n" + 
                                    "то сначала необходимо удалить запись о нем в рубрике.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            FillDogovor();
                            return;
                        }
                        else
                            return;
                    }
                    if (dgvDogovor.CurrentCell.ColumnIndex == 2)
                    {
                        try
                        {
                            dgvDogovor.CurrentCell = dgvDogovor.CurrentRow.Cells["Рубрика"];
                        }
                        catch (Exception)
                        {
                        }
                        
                        try
                        {
                            int id = int.Parse(dgvDogovor.CurrentRow.Cells["Id"].Value.ToString());
                            new CardOrganizationDogovor(id, (int)_Id, new UpdateVoidHandler(FillDogovor)).Show();
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
        }

        private void btnRefreshContact_Click(object sender, EventArgs e)
        {
            FillOrganizationPerson();
        }

        private void btnAreaRefresh_Click(object sender, EventArgs e)
        {
            int? areaid = AreaId;
            ComboServ.FillCombo(cbArea, HelpClass.GetComboListByTable("dbo.ActivityArea"), true, false);
            AreaId = areaid;
        }

        private void btnChangeName_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(INN))
            {
                MessageBox.Show("Для смены наименования оранизации\r\n необходимо ввести ИНН!", "Инфо",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrEmpty(NameDate))
            {
                MessageBox.Show("Для смены наименования организации\r\n необходимо ввести Дату, с которой актуально наименование (ЕГРЮЛ)!\r\n" +
                                    "Т.е., дату, с которой актуально текущее наименование.\r\n" +
                                    "Эта дата пойдет в архив вместе с текущим наименованием.", "Инфо",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DateTime res;
            if (!String.IsNullOrEmpty(NameDate))
            {
                if (!DateTime.TryParse(NameDate, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Дата, с которой актуально наименование (ЕГРЮЛ)'\r\n" + "Образец: 01.12.2016", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            //if (String.IsNullOrEmpty(OGRN))
            //{
            //    MessageBox.Show("Для смены наименования организации\r\n необходимо ввести ОГРН!", "Инфо",
            //                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //if (String.IsNullOrEmpty(OGRNDate))
            //{
            //    MessageBox.Show("Для смены наименования организации\r\n необходимо ввести Дату присвоения ОГРН!", "Инфо",
            //                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            DateTime result;
            if (!String.IsNullOrEmpty(OGRNDate))
            {
                if (!DateTime.TryParse(OGRNDate, out result))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Дата присвоения ОГРН' \r\n" + "Образец: 01.12.2016", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            //if (String.IsNullOrEmpty(CloseDate))
            //{
            //    MessageBox.Show("Для смены наименования организации\r\n необходимо ввести Дату прекращения деятельности!", "Инфо",
            //                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            if (MessageBox.Show("Смена наименования организации \r\n (ИНН сохраняется !!!)\r\n Продолжить?", "Запрос на подтверждение",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    groupBoxNewName.Visible = true;
                    INNNew = INN;
                    OGRNNew = OGRN;
                    OGRNDateNew = OGRNDate;
                }
                catch (Exception)
                { groupBoxNewName.Visible = false; }
            }
        }

        private void btnRemoveNewOrgName_Click(object sender, EventArgs e)
        {
            try
            {
                btnChangeName.Focus();
                groupBoxNewName.Visible = false; 
            }
            catch (Exception)
            {
            }
        }

        private void btnSetNewOrgName_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(OrgNameNew))
            {
                MessageBox.Show("Для смены наименования организации\r\n необходимо ввести новое полное наименование! \r\n", "Инфо",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (String.IsNullOrEmpty(INNNew))
            {
                MessageBox.Show("Для смены наименования организации\r\n необходимо ввести ИНН! \r\n И он должен совпадать с предыдущим ИНН", "Инфо",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                if (INNNew != INN) 
                {
                    MessageBox.Show("При смене наименования организации\r\n ИНН должен совпадать с предыдущим ИНН", "Инфо",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception)
            {
            }
            if (String.IsNullOrEmpty(NameDateNew))
            {
                MessageBox.Show("Для смены наименования организации\r\n необходимо ввести Дату, с которой актуально наименование (ЕГРЮЛ):", "Инфо",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DateTime res;
            if (!String.IsNullOrEmpty(NameDateNew))
            {
                if (!DateTime.TryParse(NameDateNew, out res))
                {
                    MessageBox.Show("Неправильный формат даты в поле 'Дата, с которой актуально наименование (ЕГРЮЛ)' \r\n" + "Образец: 01.12.2016", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            //if (String.IsNullOrEmpty(OGRNNew))
            //{
            //    MessageBox.Show("Для смены наименования организации\r\n необходимо ввести ОГРН!", "Инфо",
            //                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //if (String.IsNullOrEmpty(OGRNDateNew))
            //{
            //    MessageBox.Show("Для смены наименования организации\r\n необходимо ввести Дату присвоения ОГРН!", "Инфо",
            //                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //DateTime res;
            //if (!String.IsNullOrEmpty(OGRNDateNew))
            //{
            //    if (!DateTime.TryParse(OGRNDateNew, out res))
            //    {
            //        MessageBox.Show("Неправильный формат даты в поле 'Дата присвоения ОГРН' \r\n" + "Образец: 01.12.2016", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        return;
            //    }
            //}

            if (MessageBox.Show("Выполнить смену наименования организации \r\n (ИНН сохраняется !!!)", "Запрос на подтверждение",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                if (OrganizationNamesRecInsert())
                {
                    FillPartnerInfo();
                    return;
                }
            }
        }
        private bool OrganizationNamesRecInsert()
        {
            try
            {
                using (TransactionScope distibutedTransaction = new TransactionScope())

                {
                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        OrganizationNames orgnames  = new OrganizationNames();

                        orgnames.OrganizationId = OrgId;
                        orgnames.Name = OrgName;
                        orgnames.NameEng = NameEng;
                        orgnames.MiddleName = MiddleName;
                        orgnames.ShortName = ShortName;
                        orgnames.ShortNameEng = ShortNameEng;

                        orgnames.INN = INN;
                        if (!String.IsNullOrEmpty(NameDate))
                        {
                            orgnames.NameDate = DateTime.Parse(NameDate);
                        }
                        else
                        {
                            orgnames.NameDate = null;
                        }
                        orgnames.OGRN = OGRN;
                        if (!String.IsNullOrEmpty(OGRNDate))
	                    {
		                    orgnames.OGRNDate = DateTime.Parse(OGRNDate);
	                    }
                        else
	                    {
                            orgnames.OGRNDate = null;
	                    }
                        //if (!String.IsNullOrEmpty(CloseDate))
                        //{
                        //    orgnames.CloseDate = DateTime.Parse(CloseDate);
                        //}
                        //else
                        //{
                        //    orgnames.CloseDate = null;
                        //}

                        context.OrganizationNames.Add(orgnames);
                        context.SaveChanges();
                        OrgNamesId = orgnames.Id;
                    
                        //записать новые данные в таблицу Organization
                        var org = context.Organization.Where(x => x.Id == OrgId).First();
                        org.Name = OrgNameNew;
                        org.MiddleName = MiddleNameNew;
                        org.ShortName = ShortNameNew;
                        org.NameEng = NameEngNew;
                        org.ShortNameEng = ShortNameEngNew;
                        if (!String.IsNullOrEmpty(NameDateNew))
                        {
                            org.NameDate = DateTime.Parse(NameDateNew);
                        }
                        else
                        {
                            org.NameDate = null;
                        }
                        //org.INN = INNNew;
                        //org.OGRN = OGRNNew;
                        //org.CloseDate = null;
                        context.SaveChanges();
                    }
                    distibutedTransaction.Complete();
                }
                MessageBox.Show("Данные сохранены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
            catch (Exception exc)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + exc.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }

        private void btnPreviousName_Click(object sender, EventArgs e)
        {
            //if (Utilities.PracticeOrgCardIsOpened(id))
            //    return;
            new CardOrgPreviousName(OrgId).Show();
        }

        private void btnActivityAreaProfessionalAdd_Click(object sender, EventArgs e)
        {
            if (Utilities.CardOrgAAPIsOpened(OrgId))
                return;
            new CardOrgAAreaProfessional(OrgId, new UpdateVoidHandler(FillOrgAreaProfessional)).Show();
        }

        private void btnActivityAreaProfessionalDelete_Click(object sender, EventArgs e)
        {
            if (_Id.HasValue)
                if (dgvActivityAreaProfessional.CurrentCell != null)
                    if (dgvActivityAreaProfessional.CurrentRow.Index >= 0)
                    {
                        int id = int.Parse(dgvActivityAreaProfessional.CurrentRow.Cells["Id"].Value.ToString());
                        string sAAreaP = "";
                        try
                        {
                            sAAreaP = dgvActivityAreaProfessional.CurrentRow.Cells["Область_деятельности"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        if (MessageBox.Show("Удалить выбранную область деятельности? \r\n" + sAAreaP, "Запрос на подтверждение",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                context.OrganizationActivityAreaProfessional.RemoveRange(context.OrganizationActivityAreaProfessional.Where(x => x.Id == id));
                                context.SaveChanges();
                            }
                            FillOrgAreaProfessional();
                        }
                        else
                            return;
                    }
        }

        private void CardOrganization_Load(object sender, EventArgs e)
        {
            //try
            //{
            //    if (this.Parent.Width > this.Width + 150 + this.Left)
            //    {
            //        this.Width = this.Parent.Width - 150 - this.Left;
            //    }
            //    if (this.Parent.Height > this.Height + 150 + this.Top)
            //    {
            //        this.Height = this.Parent.Height - 150 - this.Top;
            //    }
            //}
            //catch (Exception)
            //{
            //}
        }

        private void btnSubdivisionDelete_Click(object sender, EventArgs e)
        {
            if (_Id.HasValue)
                if (dgvSubdivisions.CurrentCell != null)
                    if (dgvSubdivisions.CurrentRow.Index >= 0)
                    {
                        if (MessageBox.Show("Удалить выбранное подразделение? \r\n" + dgvSubdivisions.CurrentRow.Cells["Подразделение"].Value.ToString(), "Запрос на подтверждение",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                            return;

                        int id = int.Parse(dgvSubdivisions.CurrentRow.Cells["Id"].Value.ToString());
                        using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                        {
                            context.OrganizationSubdivision.RemoveRange(context.OrganizationSubdivision.Where(x => x.Id == id));
                            context.SaveChanges();
                        }
                        FillOrganizationSubdivision();
                    }
        }

        private void btnSubdivisionAdd_Click(object sender, EventArgs e)
        {
            new CardOrganizationSubdivision(null, OrgId, new UpdateVoidHandler(FillOrganizationSubdivision)).Show();
        }

        private void btnSubdivisionEdit_Click(object sender, EventArgs e)
        {
            if (dgvSubdivisions.CurrentCell != null)
                if (dgvSubdivisions.CurrentRow.Index >= 0)
                {
                    int id = int.Parse(dgvSubdivisions.CurrentRow.Cells["Id"].Value.ToString());
                    string name = "";
                    string nameEng = "";
                    try
                    {
                        name = dgvSubdivisions.CurrentRow.Cells["Подразделение"].Value.ToString();
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        nameEng = dgvSubdivisions.CurrentRow.Cells["Подразделение_англ"].Value.ToString();
                    }
                    catch (Exception)
                    { }

                    new CardOrganizationSubdivision(id, OrgId, new UpdateVoidHandler(FillOrganizationSubdivision), name, nameEng).Show();
                }
        }

    }
}
