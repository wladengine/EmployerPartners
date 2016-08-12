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

namespace EmployerPartners
{
    public partial class CardPerson : Form
    {
        private int? _Id
        {
            get;
            set;
        }
        UpdateVoidHandler _hndl;


        public CardPerson(int? id, UpdateVoidHandler _hdl)
        {
            InitializeComponent();
            _hndl = _hdl;
            _Id = id;

            FillCard();
            InitControls(this);
            this.MdiParent = Util.mainform;

            if (Util.IsReadOnlyAll())
            {
                btnSave.Enabled = false;
                btnDelete.Enabled = false;
                btnEditContact.Enabled = false;
                btnContactAdd.Enabled = false;
                btnDeleteContact.Enabled = false;
                btnRublrikAdd.Enabled = false;
                btnRubricDelete.Enabled = false;
                btnFacultyAdd.Enabled = false;
                btnFacultyDelete.Enabled = false;
                btnActivityAreaAdd.Enabled = false;
                btnActivityAreaDelete.Enabled = false;
                btnLPAdd.Enabled = false;
                btnLPDelete.Enabled = false;
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
            ComboServ.FillCombo(cbRank, HelpClass.GetComboListByTable("dbo.Rank"), true, false);
            ComboServ.FillCombo(cbArea, HelpClass.GetComboListByTable("dbo.ActivityArea"), true, false);

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
                    PersonName = Partner.Name;
                    NameEng = Partner.NameEng;

                    Title = Partner.Title;
                    DegreeId = Partner.DegreeId;
                    RankId = Partner.RankId;
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
                           where x.PartnerPersonId == _Id.Value
                           select new
                           {
                               x.Id,
                               OrgId = p.Id,
                               Название = p.Name,
                               Должность_в_организации = x.Position,
                               Должность_англ = x.PositionEng,
                               Комментарий = x.Comment,
                           }).ToList();

                DataTable dt = new DataTable();
                dt = Utilities.ConvertToDataTable(lst);
                dgvContacts.DataSource = dt;

                foreach (string s in new List<string>() { "Id", "OrgId" })
                    if (dgvContacts.Columns.Contains(s))
                        dgvContacts.Columns[s].Visible = false;
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
            if (String.IsNullOrEmpty(PersonName))
            {
                err.SetError(tbName, "не введено ФИО");
                tabControl1.SelectedTab = tabPage1;
                return false;
            }
            else
                err.Clear();
            if (String.IsNullOrEmpty(Title))
            {
                err.SetError(tbTitle, "не заданы регалии");
                tabControl1.SelectedTab = tabPage1;
                return false;
            }
            else
                err.Clear();

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
                        Name = PersonName,
                        NameEng = NameEng,

                        Title = Title,
                        RankId = RankId,
                        DegreeId = DegreeId,
                        ActivityAreaId = AreaId,

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
                MessageBox.Show("Ошибка при сохранении карточки\r\n" + exc.InnerException, "");
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
                    Org.Name = PersonName;
                    Org.NameEng = NameEng;

                    Org.Title = Title;
                    Org.ActivityAreaId = AreaId;
                    Org.DegreeId = DegreeId;
                    Org.RankId = RankId;

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
            catch (Exception)
            {
                MessageBox.Show("Ошибка при сохранении карточки", "");
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
            if (Util.IsReadOnlyAll())
                return;

            if (_Id.HasValue)
                if (dgvContacts.CurrentCell != null)
                    if (dgvContacts.CurrentRow.Index >= 0)
                    {
                        int id = int.Parse(dgvContacts.CurrentRow.Cells["Id"].Value.ToString());
                        new CardPersonOrganization(id, _Id.Value, new UpdateVoidHandler(FillPersonOrganization)).Show();
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
                        if (MessageBox.Show("Удалить выбранную запись? \r" + sOrgName + "\r" + sPosition, "Запрос на подтверждение", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
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
            if (Util.IsReadOnlyAll())
                return;

            DataGridView dgv = (DataGridView)sender;
            if (_Id.HasValue)
                if (dgv.CurrentCell != null)
                    if (dgv.CurrentRow.Index >= 0)
                    {
                        int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                        new CardPersonArea(id, _Id.Value, new UpdateVoidHandler(FillPersonArea)).Show();
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
            if (Util.IsReadOnlyAll())
                return;

            if (_Id.HasValue)
                if (dgvRubric.CurrentCell != null)
                    if (dgvRubric.CurrentRow.Index >= 0)
                    {
                        int id = int.Parse(dgvRubric.CurrentRow.Cells["Id"].Value.ToString());
                        new CardPersonRubric(id, _Id.Value, new UpdateVoidHandler(FillRubrics)).Show();
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
            if (Util.IsReadOnlyAll())
                return;

            if (_Id.HasValue)
                if (dgvFaculty.CurrentCell != null)
                    if (dgvFaculty.CurrentRow.Index >= 0)
                    {
                        int id = int.Parse(dgvFaculty.CurrentRow.Cells["Id"].Value.ToString());
                        new CardPersonFaculty(id, _Id.Value, new UpdateVoidHandler(FillFaculty)).Show();
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
            if (Util.IsReadOnlyAll())
                return;

            if (_Id.HasValue)
                if (dgvLP.CurrentCell != null)
                    if (dgvLP.CurrentRow.Index >= 0)
                    {
                        int id = int.Parse(dgvLP.CurrentRow.Cells["Id"].Value.ToString());
                        new CardPersonLP(id, _Id.Value, new UpdateVoidHandler(FillLP)).Show();
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
                if (MessageBox.Show("Удалить карточку? \r\n" + FIO, "Запрос на подтверждение", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
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
    }
}
