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
using WordOut;

namespace EmployerPartners
{
    public partial class CardPerson : Form
    {
        private int? _Id
        {
            get;
            set;
        }
        public int PersonId
        {
            get;
            set;
        }
        //bool EditMode = false;

        UpdateVoidHandler _hndl;

        public CardPerson(int? id, UpdateVoidHandler _hdl)
        {
            InitializeComponent();
            _hndl = _hdl;
            _Id = id;
            PersonId = (int)id;

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
                btnEditContact.Enabled = true;
                btnContactAdd.Enabled = true;
                btnDeleteContact.Enabled = true;
                btnRublrikAdd.Enabled = true;
                btnRubricDelete.Enabled = true;
                btnFacultyAdd.Enabled = true;
                btnFacultyDelete.Enabled = true;
                btnActivityAreaAdd.Enabled = true;
                btnActivityAreaDelete.Enabled = true;
                btnLPAdd.Enabled = true;
                btnLPDelete.Enabled = true;
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
            }
            else
            {
                tbCountry.TextChanged -= tbCountry_TextChanged;
            }
        }
        private void ChangeControlVisibility(object sender, EventArgs e)
        {
            if (sender != tbCountry && sender != lbCountry)
            {
                lbCountry.Visible = false;
            }
        }

        #region FillCard
        public void FillCard()
        {
            ExtraInit(false);

            ComboServ.FillCombo(cbDegree, HelpClass.GetComboListByTable("dbo.Degree"), true, false);
            ComboServ.FillCombo(cbDegree2, HelpClass.GetComboListByTable("dbo.Degree"), true, false);
            ComboServ.FillCombo(cbRank, HelpClass.GetComboListByTable("dbo.Rank"), true, false);
            ComboServ.FillCombo(cbRank2, HelpClass.GetComboListByTable("dbo.Rank"), true, false);
            ComboServ.FillCombo(cbRankHonorary, HelpClass.GetComboListByTable("dbo.RankHonorary"), true, false);
            ComboServ.FillCombo(cbRankHonorary2, HelpClass.GetComboListByTable("dbo.RankHonorary"), true, false);
            ComboServ.FillCombo(cbRankState, HelpClass.GetComboListByTable("dbo.RankState"), true, false);
            ComboServ.FillCombo(cbRankState2, HelpClass.GetComboListByTable("dbo.RankState"), true, false);
            ComboServ.FillCombo(cbArea, HelpClass.GetComboListByTable("dbo.ActivityArea"), true, false);
            ComboServ.FillCombo(cbPrefix, HelpClass.GetComboListByTable("dbo.PartnerPersonPrefix"), true, false);

            FillPartnerInfo();
            FillPersonArea();
            FillRubrics();
            FillFaculty();
            FillLP();
            FillPersonOrganization();
            ExtraInit(true);
        }
        private void FillPartnerInfo()
        {
            lbCountry.Visible = false;
            FillCountry();
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var Partner = (from x in context.PartnerPerson
                                   where x.Id == _Id
                                   select x).First();
                    LastName = Partner.LastName;
                    FirstName = Partner.FirstName;
                    SecondName = Partner.SecondName;
                    PersonName = Partner.Name;
                    NameInitials = Partner.NameInitials;
                    LastNameEng = Partner.LastNameEng;
                    FirstNameEng = Partner.FirstNameEng;
                    SecondNameEng = Partner.SecondNameEng;
                    NameEng = Partner.NameEng;
                    NameInitialsEng = Partner.NameInitialsEng;

                    isGAK = (Partner.IsGAK.HasValue) ? (bool)Partner.IsGAK : false;
                    isGAKChairMan = (Partner.IsGAKChairman.HasValue) ? (bool)Partner.IsGAKChairman : false;
                    isGAK2016 = (Partner.IsGAK2016.HasValue) ? (bool)Partner.IsGAK2016 : false;
                    isGAKChairMan2016 = (Partner.IsGAKChairman2016.HasValue) ? (bool)Partner.IsGAKChairman2016 : false;

                    IsPersonDataAgreed = (Partner.IsPersonDataAgreed.HasValue) ? (bool)Partner.IsPersonDataAgreed : false;
                    IsPersonDataChecked = (Partner.IsPersonDataChecked.HasValue) ? (bool)Partner.IsPersonDataChecked : false;

                    PrefixId = Partner.PartnerPersonPrefixId;
                    Title = Partner.Title;
                    Account = String.IsNullOrEmpty(Partner.Account) ? "pt" : Partner.Account;
                    DegreeId = Partner.DegreeId;
                    Degree2Id = Partner.Degree2Id;
                    RankId = Partner.RankId;
                    Rank2Id = Partner.Rank2Id;

                    RankHonoraryId = Partner.RankHonoraryId;
                    RankHonorary2Id = Partner.RankHonorary2Id;
                    RankStateId = Partner.RankStateId;
                    RankState2Id = Partner.RankState2Id;
                    AreaId = Partner.ActivityAreaId;

                    isGraduateSPbGU = Partner.IsSPbGUGraduate ?? false;
                    isAlumni = Partner.AlumniAssociation ?? false;
                    GraduateYear = Partner.SPbGUGraduateYear;

                    Email = Partner.Email;
                    WebSite = Partner.WebSite;
                    Phone = Partner.Phone;
                    Mobiles = Partner.Mobiles;
                    CountryId = Partner.CountryId;
                    FillCountry(CountryId);
                    Comment = Partner.Comment;

                    try
                    {
                        tbAuthor.Text = "Карточка заведена пользователем: " + Util.GetADUserName(Partner.Author) + "  " + Partner.DateCreated.Date.ToString("dd.MM.yyyy");
                    }
                    catch (Exception)
                    {
                        //MessageBox.Show(ex.Message, "Инфо");  
                    }

                    this.Text = "Карточка: " + PersonName;
                }
        }
        private void FillPersonArea()
        {
            FillPersonArea(null);
        }
        private void FillPersonArea(int? id)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.PartnerPersonActivityArea
                           join a in context.ActivityArea on x.ActivityAreaId equals a.Id
                           where x.PartnerPersonId == _Id.Value
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
        private void FillPersonOrganization()
        {
            FillPersonOrganization(null);
        }
        private void FillPersonOrganization(int? id)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.OrganizationPerson
                           join p in context.Organization on x.OrganizationId equals p.Id
                           join pos in context.Position on x.PositionId equals pos.Id into _pos
                           from pos in _pos.DefaultIfEmpty()
                           join pos2 in context.Position on x.Position2Id equals pos2.Id into _pos2
                           from pos2 in _pos2.DefaultIfEmpty()
                           join sdiv in context.OrganizationSubdivision on x.OrganizationSubdivisionId equals sdiv.Id into _sdiv
                           from sdiv in _sdiv.DefaultIfEmpty()
                           join sdiv2 in context.OrganizationSubdivision on x.OrganizationSubdivision2Id equals sdiv2.Id into _sdiv2
                           from sdiv2 in _sdiv2.DefaultIfEmpty()
                           //join subdiv in context.OrganizationSubdivision on x.OrganizationId equals subdiv.OrganizationId into _subdiv
                           //from subdiv in _subdiv.DefaultIfEmpty()
                           where x.PartnerPersonId == _Id.Value
                           orderby x.Sorting
                           select new
                           {
                               Не_использовать_в_документах = x.NotUseInDocs,
                               Сортировка = x.Sorting,
                               //Название = p.Name,
                               Название = ((String.IsNullOrEmpty(p.MiddleName)) ? ((String.IsNullOrEmpty(p.ShortName)) ? p.Name : p.ShortName) : p.MiddleName),
                               Должность_в_организации_по_справочнику = pos.Name,
                               //Подразделение_в_организации = subdiv.Name,
                               Подразделение_в_организации = sdiv.Name,
                               //Должность_в_организации_ручной_ввод = x.Position,
                               //Должность_англ_по_справочнику = pos.NameEng,
                               //Должность_англ_ручной_ввод = x.PositionEng,
                               Вторая_должность_в_организации_по_справочнику = pos2.Name,
                               Подразделение_в_организации_вторая_должность = sdiv2.Name,
                               Комментарий = x.Comment,
                               x.Id,
                               OrgId = p.Id,
                           }).ToList();

                DataTable dt = new DataTable();
                dt = Utilities.ConvertToDataTable(lst);
                dgvContacts.DataSource = dt;

                foreach (string s in new List<string>() { "Id", "OrgId" })
                    if (dgvContacts.Columns.Contains(s))
                        dgvContacts.Columns[s].Visible = false;
                foreach (DataGridViewColumn col in dgvContacts.Columns)
                    col.HeaderText = col.Name.Replace("_", " ");
                try
                {
                    dgvContacts.Columns["Не_использовать_в_документах"].Frozen = true;
                    dgvContacts.Columns["Сортировка"].Frozen = true;
                    dgvContacts.Columns["Название"].Frozen = true;
                    dgvContacts.Columns["Не_использовать_в_документах"].Width = 100;
                    dgvContacts.Columns["Сортировка"].Width = 80;
                    dgvContacts.Columns["Название"].Width = 250;
                    
                }
                catch (Exception)
                {
                }
                if (id.HasValue)
                    foreach (DataGridViewRow rw in dgvContacts.Rows)
                        if (rw.Cells[0].Value.ToString() == id.Value.ToString())
                        {
                            dgvContacts.CurrentCell = rw.Cells["Название"];
                            break;
                        }
            }
        }

        #endregion

        #region CheckSaveUpdate_CommonInformation
        private bool CheckFields()
        {
            if (String.IsNullOrEmpty(LastName))
            {
                err.SetError(tbLastName, "не введена Фамилия");
                tabControl1.SelectedTab = tabPage1;
                return false;
            }
            else
                err.Clear();
            if (String.IsNullOrEmpty(PersonName))
            {
                err.SetError(tbName, "не введено ФИО");
                tabControl1.SelectedTab = tabPage1;
                return false;
            }
            else
                err.Clear();
            if (String.IsNullOrEmpty(NameInitials))
            {
                err.SetError(tbName, "не введено ФИО-инициалы");
                tabControl1.SelectedTab = tabPage1;
                return false;
            }
            else
                err.Clear();
            if (!String.IsNullOrEmpty(Account))
            {
                if ((Account.Length != 8) && (Account != "pt"))
                {
                    err.SetError(tbAccount, "Значение 'Аккаунт' некорректно \r\n Аккаунт состоит из 8 символов, начиная с pt");
                    tabControl1.SelectedTab = tabPage1;
                    return false;
                }
                try
                {
                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        var lst = context.PartnerPerson.Where(x => x.Account == Account && x.Id != PersonId).Count();
                        if (lst > 0)
                        {
                            var pt = context.PartnerPerson.Where(x => x.Account == Account && x.Id != PersonId).First();
                            MessageBox.Show("Идентификационный номер " + Account + "\r\n" + "уже существует \r\n" + pt.Name, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            else
                err.Clear();

            //if (String.IsNullOrEmpty(Title))
            //{
            //    err.SetError(tbTitle, "не заданы регалии");
            //    tabControl1.SelectedTab = tabPage1;
            //    return false;
            //}
            //else
            //    err.Clear();

            if (!String.IsNullOrEmpty(tbGraduateYear.Text))
            {
                int tmp;
                if (!int.TryParse(tbGraduateYear.Text, out tmp))
                {
                    err.SetError(tbGraduateYear, "Значение некорректно");
                    tabControl1.SelectedTab = tabPage1;
                    return false;
                }
                else
                {
                    if (tmp < 1900 || tmp> DateTime.Now.AddYears(5).Year)
                    {
                        err.SetError(tbGraduateYear, "Значение некорректно");
                        tabControl1.SelectedTab = tabPage1;
                        return false;
                    }
                    err.Clear();
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
            return true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!CheckFields())
                return;

            string str = "";
            if (_Id == null)
            {
                if (!PartnerPersonRecInsert())
                    return;
                str = "Сохранение успешно завершено";
            }
            else
            {
                if (!PartnerPersonRecUpdate())
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
        private bool PartnerPersonRecInsert()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    Guid gId = Guid.NewGuid();
                    context.PartnerPerson.Add(new PartnerPerson()
                    {
                        rowguid = gId,
                        LastName = LastName,
                        FirstName = FirstName,
                        SecondName = SecondName,
                        Name = PersonName,
                        NameInitials = NameInitials,
                        LastNameEng = LastNameEng,
                        FirstNameEng = FirstNameEng,
                        SecondNameEng = SecondNameEng,
                        NameEng = NameEng,
                        NameInitialsEng = NameInitialsEng,

                        IsGAK = isGAK,
                        IsGAKChairman = isGAKChairMan,
                        IsGAK2016 = isGAK2016,
                        IsGAKChairman2016 = isGAKChairMan2016,

                        IsPersonDataAgreed = IsPersonDataAgreed,
                        IsPersonDataChecked = IsPersonDataChecked,
   
                        Title = Title,
                        Account = (Account == "pt") ? "" : Account,
                        RankId = RankId,
                        Rank2Id = Rank2Id,
                        RankHonoraryId = RankHonoraryId,
                        RankHonorary2Id = RankHonorary2Id,
                        RankStateId = RankStateId,
                        RankState2Id = RankState2Id,
                        DegreeId = DegreeId,
                        Degree2Id = Degree2Id,
                        ActivityAreaId = AreaId,
                        PartnerPersonPrefixId = PrefixId,

                        IsSPbGUGraduate = isGraduateSPbGU,
                        AlumniAssociation = isAlumni,
                        SPbGUGraduateYear = GraduateYear,

                        Email = Email,
                        WebSite = WebSite,
                        CountryId = CountryId,
                        Phone = Phone,
                        Mobiles = Mobiles,
                        Comment = Comment,
                    });
                    context.SaveChanges();
                    _Id = context.PartnerPerson.Where(x => x.rowguid == gId).Select(x => x.Id).First();
                    return true;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка при сохранении карточки\r\n" + exc.InnerException, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
        }
        private bool PartnerPersonRecUpdate()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var Org = context.PartnerPerson.Where(x => x.Id == _Id).First();
                    Org.LastName = LastName;
                    Org.FirstName = FirstName;
                    Org.SecondName = SecondName;
                    Org.Name = PersonName;
                    Org.NameInitials = NameInitials;
                    Org.LastNameEng = LastNameEng;
                    Org.FirstNameEng = FirstNameEng;
                    Org.SecondNameEng = SecondNameEng;
                    Org.NameEng = NameEng;
                    Org.NameInitialsEng = NameInitialsEng;

                    Org.IsGAK = isGAK;
                    Org.IsGAKChairman = isGAKChairMan;
                    Org.IsGAK2016 = isGAK2016;
                    Org.IsGAKChairman2016 = isGAKChairMan2016;

                    Org.IsPersonDataAgreed = IsPersonDataAgreed;
                    Org.IsPersonDataChecked = IsPersonDataChecked;

                    Org.Title = Title;
                    Org.Account = (Account == "pt") ? "" : Account;
                    Org.ActivityAreaId = AreaId;
                    Org.DegreeId = DegreeId;
                    Org.Degree2Id = Degree2Id;
                    Org.RankId = RankId;
                    Org.Rank2Id = Rank2Id;
                    Org.RankHonoraryId = RankHonoraryId;
                    Org.RankHonorary2Id = RankHonorary2Id;
                    Org.RankStateId = RankStateId;
                    Org.RankState2Id = RankState2Id;
                    Org.PartnerPersonPrefixId = PrefixId;

                    Org.IsSPbGUGraduate = isGraduateSPbGU;
                    Org.AlumniAssociation = isAlumni;
                    Org.SPbGUGraduateYear = GraduateYear;

                    Org.Email = Email;
                    Org.WebSite = WebSite;
                    Org.Phone = Phone;
                    Org.Mobiles = Mobiles;

                    Org.CountryId = CountryId;
                    Org.Comment = Comment;
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
            }
        }
        private void lbCountry_MouseClick(object sender, MouseEventArgs e)
        {
            if (lbCountry.SelectedValue != null)
            {
                sCountry = ((KeyValuePair<int, string>)lbCountry.SelectedItem).Value.ToString();
                CountryId = int.Parse(((KeyValuePair<int, string>)lbCountry.SelectedItem).Key.ToString());
                lbCountry.Visible = false;
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
            }
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
                    }
                }
        }
        #endregion

        #region Contacts
        private void btnContactAdd_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
            {
                MessageBox.Show("Сначала сохраните карточку Физ.лица");
                return;
            }
            new CardPersonOrganization(null, _Id.Value, new UpdateVoidHandler(FillPersonOrganization)).Show();
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
                            new CardPersonOrganization(id, _Id.Value, new UpdateVoidHandler(FillPersonOrganization)).Show();
                        }
            }
        }
        private void btnDeleteContact_Click(object sender, EventArgs e)
        {
            if (_Id.HasValue)
                if (dgvContacts.CurrentCell != null)
                    if (dgvContacts.CurrentRow.Index >= 0)
                    {
                        string sOrgName = "";
                        try
                        {
                            sOrgName = dgvContacts.CurrentRow.Cells["Название"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        string sPosition = "";
                        try
                        {
                            sPosition = "Должность в организации: " + dgvContacts.CurrentRow.Cells["Должность_в_организации"].Value.ToString();
                        }
                        catch (Exception)
                        {
                        }
                        if (MessageBox.Show("Удалить выбранную запись? \r" + sOrgName + "\r" + sPosition, "Запрос на подтверждение", 
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                            return;

                        int id = int.Parse(dgvContacts.CurrentRow.Cells["Id"].Value.ToString());
                        using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                        {
                            context.OrganizationPerson.RemoveRange(context.OrganizationPerson.Where(x => x.Id == id));
                            context.SaveChanges();
                        }
                        FillPersonOrganization();
                    }
        }
        private void btnOrganizationCardOpen_Click(object sender, EventArgs e)
        {
            if (_Id.HasValue)
                if (dgvContacts.CurrentCell != null)
                    if (dgvContacts.CurrentRow.Index >= 0)
                    {
                        int id = int.Parse(dgvContacts.CurrentRow.Cells["OrgId"].Value.ToString());
                        if (Utilities.OrgCardIsOpened(id))
                            return;
                        new CardOrganization(id, new UpdateVoidHandler(FillPersonOrganization)).Show();
                    }
        }
        #endregion

        #region Areas
        private void btnAreaAdd_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
            {
                MessageBox.Show("Сначала сохраните карточку Организации");
                return;
            }
            new CardPersonArea(null, _Id.Value, new UpdateVoidHandler(FillPersonArea)).Show();
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
                            new CardPersonArea(id, _Id.Value, new UpdateVoidHandler(FillPersonArea)).Show();
                        }
            }
        }
        private void btnAreaDelete_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgvArea;
            if (sender == btnActivityAreaDelete)
                //dgv = dgvArea;
                dgv = dgvActivityArea;
            else if (sender == btnAreaDelete)
                dgv = dgvArea;

            if (_Id.HasValue)
                if (dgv.CurrentCell != null)
                    if (dgv.CurrentRow.Index >= 0)
                    {
                        int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                        if (MessageBox.Show("Удалить выбранную сферу деятельности физического лица? \r" + dgv.CurrentRow.Cells["Название"].Value.ToString(), "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                context.PartnerPersonActivityArea.RemoveRange(context.PartnerPersonActivityArea.Where(x => x.Id == id));
                                context.SaveChanges();
                            }
                            FillPersonArea();
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
                var lst = (from x in context.PartnerPersonRubric
                           join r in context.Rubric on x.RubricId equals r.Id
                           where x.PartnerPersonId == _Id
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
            new CardPersonRubric(null, _Id.Value, new UpdateVoidHandler(FillRubrics)).Show();
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
                                context.PartnerPersonRubric.RemoveRange(context.PartnerPersonRubric.Where(x => x.Id == id));
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
                            new CardPersonRubric(id, _Id.Value, new UpdateVoidHandler(FillRubrics)).Show();
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
                var lst = (from x in context.PartnerPersonFaculty
                           join f in context.Faculty on x.FacultyId equals f.Id
                           join r in context.Rubric on x.RubricId equals r.Id into _r
                           from r in _r.DefaultIfEmpty()
                           where x.PartnerPersonId == _Id
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
            new CardPersonFaculty(null, _Id.Value, new UpdateVoidHandler(FillFaculty)).Show();
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
                        if (MessageBox.Show("Удалить выбранное направление? \r\n" + sFac + "\r\n" + sRubric, "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                        {
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                context.PartnerPersonFaculty.RemoveRange(context.PartnerPersonFaculty.Where(x => x.Id == id));
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
                            new CardPersonFaculty(id, _Id.Value, new UpdateVoidHandler(FillFaculty)).Show();
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
                var lst = (from x in context.PartnerPersonLP
                           join а in context.LicenseProgram on x.LicenseProgramId equals а.Id
                           join r in context.Rubric on x.RubricId equals r.Id into _r
                           from r in _r.DefaultIfEmpty()
                           where x.PartnerPersonId == _Id
                           select new
                           {
                               x.Id,
                               Рубрика = r.ShortName,
                               Код = а.Code,
                               Уровень = а.StudyLevel.Name,
                               Направление = а.Name,
                               Тип_программы = а.ProgramType.Name,
                               Квалификация = а.Qualification.Name,
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
            new CardPersonLP(null, _Id.Value, new UpdateVoidHandler(FillLP)).Show();
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
                                context.PartnerPersonLP.RemoveRange(context.PartnerPersonLP.Where(x => x.Id == id));
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
                            new CardPersonLP(id, _Id.Value, new UpdateVoidHandler(FillLP)).Show();
                        }
            }
        }
        #endregion

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                string FIO = "";
                try
                {
                    FIO = tbName.Text;
                }
                catch (Exception)
                {
                }
                if (MessageBox.Show("Удалить карточку? \r\n" + FIO, "Запрос на подтверждение", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                    return;
                try
                {
                    context.PartnerPerson.Remove(context.PartnerPerson.Where(x => x.Id == _Id).First());
                    context.SaveChanges();
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Не удалось удалить карточку...\r\n" + "Обычно это связано с тем, что у данной записи имеются связанные записи в других таблицах.", "Сообщение");
                }
                try
                {
                    if (_hndl != null)
                        _hndl(null);
                }
                catch (Exception)
                {
                }
            }
        }

        private void btnEditContact_Click(object sender, EventArgs e)
        {
            if (_Id.HasValue)
                if (dgvContacts.CurrentCell != null)
                    if (dgvContacts.CurrentRow.Index >= 0)
                    {
                        int id = int.Parse(dgvContacts.CurrentRow.Cells["Id"].Value.ToString());
                        new CardPersonOrganization(id, _Id.Value, new UpdateVoidHandler(FillPersonOrganization)).Show();
                    }
        }

        private void btnRefreshOrgList_Click(object sender, EventArgs e)
        {
            FillPersonOrganization();
        }

        private void btnAreaRefresh_Click(object sender, EventArgs e)
        {
            int? areaid = AreaId;
            ComboServ.FillCombo(cbArea, HelpClass.GetComboListByTable("dbo.ActivityArea"), true, false);
            AreaId = areaid;
        }
        private void MakeFIO()
        {
            string FIO = "";
            if (!String.IsNullOrEmpty(LastName))
            {
                FIO = LastName;
            }
            if (!String.IsNullOrEmpty(FirstName))
            {
                if (FIO != "")
                {
                    FIO += " " + FirstName;
                }
                else
                {
                    FIO = FirstName;
                }
            }
            if (!String.IsNullOrEmpty(SecondName))
            {
                if (FIO != "")
                {
                    FIO += " " + SecondName;
                }
                else
                {
                    FIO = SecondName;
                }
            }
            PersonName = FIO;
            this.Text = FIO;
        }
        private void MakeFIOInitials()
        {
            string FIO = "";
            if (!String.IsNullOrEmpty(LastName))
            {
                FIO = LastName;
            }
            if (!String.IsNullOrEmpty(FirstName))
            {
                if (FIO != "")
                {
                    FIO += " " + FirstName.Substring(0,1) + ".";
                }
                else
                {
                    FIO = FirstName.Substring(0, 1) + ".";
                }
            }
            if (!String.IsNullOrEmpty(SecondName))
            {
                if (FIO != "")
                {
                    //FIO += " " + SecondName.Substring(0, 1) + ".";
                    FIO += (!String.IsNullOrEmpty(FirstName)) ? SecondName.Substring(0, 1) + "." : " " + SecondName.Substring(0, 1) + ".";
                }
                else
                {
                    FIO = SecondName.Substring(0, 1) + ".";
                }
            }
            NameInitials = FIO;
            //this.Text = FIO;
        }
        private void MakeFIOEng()
        {
            string FIO = "";
            if (!String.IsNullOrEmpty(LastNameEng))
            {
                FIO = LastNameEng;
            }
            if (!String.IsNullOrEmpty(FirstNameEng))
            {
                if (FIO != "")
                {
                    FIO += " " + FirstNameEng;
                }
                else
                {
                    FIO = FirstNameEng;
                }
            }
            if (!String.IsNullOrEmpty(SecondNameEng))
            {
                if (FIO != "")
                {
                    FIO += " " + SecondNameEng;
                }
                else
                {
                    FIO = SecondNameEng;
                }
            }
            NameEng = FIO;
            //this.Text = FIO;
        }
        private void MakeFIOInitialsEng()
        {
            string FIO = "";
            if (!String.IsNullOrEmpty(LastNameEng))
            {
                FIO = LastNameEng;
            }
            if (!String.IsNullOrEmpty(FirstNameEng))
            {
                if (FIO != "")
                {
                    FIO += " " + FirstNameEng.Substring(0, 1) + ".";
                }
                else
                {
                    FIO = FirstNameEng.Substring(0, 1) + ".";
                }
            }
            if (!String.IsNullOrEmpty(SecondNameEng))
            {
                if (FIO != "")
                {
                    //FIO += " " + SecondNameEng.Substring(0, 1) + ".";
                    FIO += (!String.IsNullOrEmpty(FirstNameEng)) ? SecondNameEng.Substring(0, 1) + "." : " " + SecondNameEng.Substring(0, 1) + ".";
                }
                else
                {
                    FIO = SecondNameEng.Substring(0, 1) + ".";
                }
            }
            NameInitialsEng = FIO;
            //this.Text = FIO;
        }
        private void tbLastName_TextChanged(object sender, EventArgs e)
        {
            MakeFIO();
            MakeFIOInitials();
        }

        private void tbFirstName_TextChanged(object sender, EventArgs e)
        {
            MakeFIO();
            MakeFIOInitials();
        }

        private void tbSecondName_TextChanged(object sender, EventArgs e)
        {
            MakeFIO();
            MakeFIOInitials();
        }

        private void tbLastNameEng_TextChanged(object sender, EventArgs e)
        {
            MakeFIOEng();
            MakeFIOInitialsEng();
        }

        private void tbFirstNameEng_TextChanged(object sender, EventArgs e)
        {
            MakeFIOEng();
            MakeFIOInitialsEng();
        }

        private void tbSecondNameEng_TextChanged(object sender, EventArgs e)
        {
            MakeFIOEng();
            MakeFIOInitialsEng();
        }

        private void CardPerson_Load(object sender, EventArgs e)
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

        private void btnAgreement_Click(object sender, EventArgs e)
        {
            AgreementDoc();
        }
        private void AgreementDoc()
        {
            //извлечение шаблона из БД
            byte[] fileByteArray;
            string type;
            string name;
            string nameshort;
            string templatename = "";
            templatename = "СОГЛАСИЕ_члена ГЭК";
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
            string TempFilesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\EmployerPartners_TempFiles\";    //@"\Приказы по составам ГЭК\";
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

            WordDoc wd = new WordDoc(string.Format(filePath), true);

            try
            {
                string FIO = "";
                string Title = "";
                string TitleNext = "";
                string TitleEng = "";
                string TitleEngNext = "";
                string PersonData = "";
                string PersonDataEng = "";
                string Email = "";
                string Phone = "";

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var item = (from x in context.PartnerPerson

                               join degree in context.Degree on x.DegreeId equals degree.Id into _degree
                               from degree in _degree.DefaultIfEmpty()
                               join degree2 in context.Degree on x.Degree2Id equals degree2.Id into _degree2
                               from degree2 in _degree2.DefaultIfEmpty()

                               join rank in context.Rank on x.RankId equals rank.Id into _rank
                               from rank in _rank.DefaultIfEmpty()
                               join rank2 in context.Rank on x.Rank2Id equals rank2.Id into _rank2
                               from rank2 in _rank2.DefaultIfEmpty()

                               join rankhon in context.RankHonorary on x.RankHonoraryId equals rankhon.Id into _rankhon
                               from rankhon in _rankhon.DefaultIfEmpty()
                               join rankhon2 in context.RankHonorary on x.RankHonorary2Id equals rankhon2.Id into _rankhon2
                               from rankhon2 in _rankhon2.DefaultIfEmpty()

                               join rankstate in context.RankState on x.RankStateId equals rankstate.Id into _rankstate
                               from rankstate in _rankstate.DefaultIfEmpty()
                               join rankstate2 in context.RankState on x.RankState2Id equals rankstate2.Id into _rankstate2
                               from rankstate2 in _rankstate2.DefaultIfEmpty()

                               join orgpos in context.PersonOrgPosition on x.Id equals orgpos.PartnerPersonId into _orgpos
                               from orgpos in _orgpos.DefaultIfEmpty()

                               where (x.Id == _Id)
                               select new
                               {
                                   x.Id,
                                   FIO = x.Name,
                                   orgpos.OrgPosition,
                                   Degree = (x.DegreeId.HasValue) ? ((x.Degree2Id.HasValue) ? (degree.Name + ", " + degree2.Name) : degree.Name) : ((x.Degree2Id.HasValue) ? (degree2.Name) : ""),
                                   Rank = (x.RankId.HasValue) ? ((x.Rank2Id.HasValue) ? (rank.Name + ", " + rank2.Name) : rank.Name) : ((x.Rank2Id.HasValue) ? (rank2.Name) : ""),
                                   RankHonorary = (x.RankHonoraryId.HasValue) ? ((x.RankHonorary2Id.HasValue) ? (rankhon.Name + ", " + rankhon2.Name) : rankhon.Name) : ((x.RankHonorary2Id.HasValue) ? (rankhon2.Name) : ""),
                                   RankState = (x.RankStateId.HasValue) ? ((x.RankState2Id.HasValue) ? (rankstate.Name + ", " + rankstate2.Name) : rankstate.Name) : ((x.RankState2Id.HasValue) ? (rankstate.Name) : ""),
                                   
                                   FIOEng = String.IsNullOrEmpty(x.NameEng) ? "" : x.NameEng,
                                   orgpos.OrgPositionEng,
                                   DegreeEng = (x.DegreeId.HasValue) ? ((x.Degree2Id.HasValue) ? (degree.NameEng + ", " + degree2.NameEng) : degree.NameEng) : ((x.Degree2Id.HasValue) ? (degree2.NameEng) : ""),
                                   RankEng = (x.RankId.HasValue) ? ((x.Rank2Id.HasValue) ? (rank.NameEng + ", " + rank2.NameEng) : rank.NameEng) : ((x.Rank2Id.HasValue) ? (rank2.NameEng) : ""),
                                   RankHonoraryEng = (x.RankHonoraryId.HasValue) ? ((x.RankHonorary2Id.HasValue) ? (rankhon.NameEng + ", " + rankhon2.NameEng) : rankhon.NameEng) : ((x.RankHonorary2Id.HasValue) ? (rankhon2.NameEng) : ""),
                                   RankStateEng = (x.RankStateId.HasValue) ? ((x.RankState2Id.HasValue) ? (rankstate.NameEng + ", " + rankstate2.NameEng) : rankstate.NameEng) : ((x.RankState2Id.HasValue) ? (rankstate.NameEng) : ""),
                                   //Ученая_степень = person.Degree.Name,
                                   //Ученое_звание = person.Rank.Name,
                                   //Почетное_звание = person.RankHonorary.Name,
                                   //Государственное_или_военное_звание = person.RankState.Name,
                                   TitleDop = x.Title,
                                   TitleEngDop = x.TitleEng,
                                   Email = x.Email,
                                   Phone = x.Phone
                               }).First();
                    //Русские значения
                    Title = (!String.IsNullOrEmpty(item.Degree)) ? item.Degree : "";
                    TitleNext = (!String.IsNullOrEmpty(item.Rank)) ? item.Rank : "";
                    Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                    TitleNext = item.OrgPosition;
                    Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                    TitleNext = (!String.IsNullOrEmpty(item.RankHonorary)) ? item.RankHonorary : "";
                    Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                    TitleNext = (!String.IsNullOrEmpty(item.RankState)) ? item.RankState : "";
                    Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;
                    TitleNext = (!String.IsNullOrEmpty(item.TitleDop)) ? item.TitleDop : "";
                    Title = (!string.IsNullOrEmpty(Title)) ? ((!String.IsNullOrEmpty(TitleNext)) ? (Title + ", " + TitleNext) : Title) : TitleNext;

                    //английские значения
                    TitleEng = (!String.IsNullOrEmpty(item.DegreeEng)) ? item.DegreeEng : "";
                    TitleEngNext = (!String.IsNullOrEmpty(item.Rank)) ? item.RankEng : "";
                    TitleEng = (!string.IsNullOrEmpty(TitleEng)) ? ((!String.IsNullOrEmpty(TitleEngNext)) ? (TitleEng + ", " + TitleEngNext) : TitleEng) : TitleEngNext;
                    TitleEngNext = item.OrgPositionEng;
                    TitleEng = (!string.IsNullOrEmpty(TitleEng)) ? ((!String.IsNullOrEmpty(TitleEngNext)) ? (TitleEng + ", " + TitleEngNext) : TitleEng) : TitleEngNext;
                    TitleEngNext = (!String.IsNullOrEmpty(item.RankHonoraryEng)) ? item.RankHonoraryEng : "";
                    TitleEng = (!string.IsNullOrEmpty(TitleEng)) ? ((!String.IsNullOrEmpty(TitleEngNext)) ? (TitleEng + ", " + TitleEngNext) : TitleEng) : TitleEngNext;
                    TitleEngNext = (!String.IsNullOrEmpty(item.RankStateEng)) ? item.RankStateEng : "";
                    TitleEng = (!string.IsNullOrEmpty(TitleEng)) ? ((!String.IsNullOrEmpty(TitleEngNext)) ? (TitleEng + ", " + TitleEngNext) : TitleEng) : TitleEngNext;
                    TitleEngNext = (!String.IsNullOrEmpty(item.TitleEngDop)) ? item.TitleEngDop : "";
                    TitleEng = (!string.IsNullOrEmpty(TitleEng)) ? ((!String.IsNullOrEmpty(TitleEngNext)) ? (TitleEng + ", " + TitleEngNext) : TitleEng) : TitleEngNext;

                    FIO = item.FIO;
                    PersonData = item.FIO + ", " + Title;
                    if (item.FIOEng == "")
                    {
                        PersonDataEng = TitleEng;
                    }
                    else
                    {
                        PersonDataEng = item.FIOEng + ", " + TitleEng;
                    }

                    Email = (!String.IsNullOrEmpty(item.Email)) ? item.Email : "_________________";
                    Phone = (!String.IsNullOrEmpty(item.Phone)) ? item.Phone : "_________________";

                    wd.SetFields("FIO", item.FIO);
                    wd.SetFields("PersonData", PersonData);
                    wd.SetFields("PersonDataEng", PersonDataEng);
                    wd.SetFields("Email", Email);
                    wd.SetFields("Phone", Phone);

                }
                if (File.Exists(filePath))
                {
                    try
                    {
                        File.Delete(filePath);
                    }
                    catch
                    { }
                }
                string fpath = filePath;
                try
                {
                    filePath = TempFilesFolder + nameshort + " " + FIO + type;
                }
                catch (Exception)
                {
                    filePath = fpath;
                }

                wd.Save(filePath);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сформировать документ\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
