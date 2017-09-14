using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using EmployerPartners.EDMX;

namespace EmployerPartners
{
    public partial class CardNewPerson : Form
    {
        public string LastName
        {
            get { return tbLastName.Text.Trim(); }
            set { tbLastName.Text = value; }
        }
        public string FirstName
        {
            get { return tbFirstName.Text.Trim(); }
            set { tbFirstName.Text = value; }
        }
        public string SecondName
        {
            get { return tbSecondName.Text.Trim(); }
            set { tbSecondName.Text = value; }
        }
        public string PersonName
        {
            get { return tbName.Text.Trim(); }
            set { tbName.Text = value; }
        }
        public string NameInitials
        {
            get { return tbNameInitials.Text.Trim(); }
            set { tbNameInitials.Text = value; }
        }
        public string Title
        {
            get { return tbTitle.Text.Trim(); }
            set { tbTitle.Text = value; }
        }
        public int? DegreeId
        {
            get { return ComboServ.GetComboIdInt(cbDegree); }
            set { ComboServ.SetComboId(cbDegree, value); }
        }
        public int? RankId
        {
            get { return ComboServ.GetComboIdInt(cbRank); }
            set { ComboServ.SetComboId(cbRank, value); }
        }
        public int? RubricId
        {
            get { return ComboServ.GetComboIdInt(cbRubric); }
            set { ComboServ.SetComboId(cbRubric, value); }
        }
        public int? FacultyId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
        }
        public int? OrganizationId
        {
            get { return ComboServ.GetComboIdInt(cbOrganization); }
            set { ComboServ.SetComboId(cbOrganization, value); }
        }
        public int? PositionId
        {
            get { return ComboServ.GetComboIdInt(cbPosition); }
            set { ComboServ.SetComboId(cbPosition, value); }
        }
        public string Position
        {
            get { return tbPosition.Text.Trim(); }
            set { tbPosition.Text = value; }
        }
        private int? NewPersonId;
        private bool setPosition = false;

        UpdateIntHandler _hdl;

        public CardNewPerson(UpdateIntHandler h)
        {
            InitializeComponent();
            _hdl = h;
            FillInfo();
            this.MdiParent = Util.mainform;
        }
        private void FillInfo()
        {
            ComboServ.FillCombo(cbDegree, HelpClass.GetComboListByTable("dbo.Degree"), true, false);
            ComboServ.FillCombo(cbRank, HelpClass.GetComboListByTable("dbo.Rank"), true, false);
            ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("dbo.Faculty"), true, false);
            ComboServ.FillCombo(cbRubric, HelpClass.GetComboListByTable("dbo.Rubric"), true, false);
            ComboServ.FillCombo(cbPosition, HelpClass.GetComboListByTable("dbo.Position"), true, false);
            FillComboOrg();
            //FillComboPos();
            setPosition = true;
        }
        private void FillComboOrg()
        {
            ComboServ.FillCombo(cbOrganization, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), Id) AS Id, 
                (CASE  WHEN [MiddleName] is NULL THEN Organization.[Name] WHEN [MiddleName] = '' THEN Organization.[Name] ELSE [MiddleName] END) AS Name
                from dbo.Organization order by Name"), true, false);
            //ComboServ.FillCombo(cbPosition, HelpClass.GetComboListByQuery(@" select [Name] As Id, [Name] FROM dbo.Position ORDER BY [Name]"), true, false);
        }
        private void FillComboPos()
        {
            ComboServ.FillCombo(cbPosition, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), Id) AS Id, 
                [Name] + (CASE  WHEN ([NameEng] is NULL) OR ([NameEng] = '') THEN '' ELSE '          ( ' + [NameEng] + ' )'END) AS Name
                from dbo.Position order by Name"), true, false);
        }
        private bool Check()
        {
            if (LastName.Length == 0)
            {
                MessageBox.Show("Введите фамилию физического лица", "Напоминание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (PersonName.Length == 0)
            {
                MessageBox.Show("Введите ФИО физического лица", "Напоминание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (!RubricId.HasValue)
            {
                MessageBox.Show("Введите рубрику", "Напоминание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbRubric.DroppedDown = true;
                return false;
            }
            if (!FacultyId.HasValue)
            {
                MessageBox.Show("Введите УНП", "Напоминание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbFaculty.DroppedDown = true;
                return false;
            }
            if (!OrganizationId.HasValue)
            {
                MessageBox.Show("Не выбрана организация", "Напоминание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbOrganization.DroppedDown = true;
                return false;
            }
            if (!PositionId.HasValue)
            {
                if (MessageBox.Show("Не выбрана должность по справочнику\r\n" + "Продолжить тем не менее ?", "Запрос на подтверждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Cancel)
                {
                    cbPosition.DroppedDown = true;
                    return false;
                }
            }
            //if (Position.Length == 0)
            //{
            //    MessageBox.Show("не указана должность в организации", "Напоминание", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return false;
            //}
            //if (tbTitle.Text.Trim().Length == 0)
            //{
            //    MessageBox.Show("Введите регалии", "Напоминание");
            //    return false;
            //}
            return true;
        }
        private bool CheckExist()
        {
            string Name = tbName.Text.Trim();
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var cnt = (from x in context.PartnerPerson
                           where x.Name == Name
                           select new { x.Id }).ToList();
                if (cnt.Count > 0)
                {
                    DialogResult msg = MessageBox.Show("Физическое лицо с таким именем уже существует. Открыть карточку существующего?\nДа - прервать добавление, открыть существующую карточку\nНет - продолжить добавление\nОтмена - прервать добавление, карточку не открывать", "", MessageBoxButtons.YesNoCancel);
                    if (msg == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (_hdl != null)
                            _hdl(cnt.First().Id);
                        new CardPerson(cnt.First().Id, _hdl).Show();
                        return false;
                    }
                    else if (msg == System.Windows.Forms.DialogResult.No)
                    {
                        return true;
                    }
                    else if (msg == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return false;
                    }

                    if (_hdl != null)
                        _hdl(cnt.First().Id);
                }
            }
            return true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Check() || !CheckExist())
                return;
            try
            {
                using (TransactionScope distibutedTransaction = new TransactionScope())
                {
                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        //добавление нового физ. лица
                        PartnerPerson pp = new PartnerPerson();
                        pp.rowguid = Guid.NewGuid();
                        pp.LastName = LastName;
                        pp.FirstName = FirstName;
                        pp.SecondName = SecondName;
                        pp.Name = PersonName;
                        pp.NameInitials = NameInitials;
                        pp.DegreeId = DegreeId;
                        pp.RankId = RankId;
                        pp.Title = Title;

                        context.PartnerPerson.Add(pp);
                        context.SaveChanges();
                        NewPersonId = pp.Id;

                        //добавление рубрики
                        PartnerPersonRubric pprubric = new PartnerPersonRubric()
                        {
                            PartnerPersonId = (int)NewPersonId,
                            RubricId = (int)RubricId,
                        };
                        context.PartnerPersonRubric.Add(pprubric);
                        context.SaveChanges();

                        //добавление УНП
                        PartnerPersonFaculty ppfaculty = new PartnerPersonFaculty()
                        {
                            PartnerPersonId = (int)NewPersonId,
                            FacultyId = (int)FacultyId,
                            RubricId = (int)RubricId,
                        };
                        context.PartnerPersonFaculty.Add(ppfaculty);
                        context.SaveChanges();

                        //добавление контакта с организацией
                        if (PositionId.HasValue)
                        {
                            OrganizationPerson org = new OrganizationPerson()
                            {
                                OrganizationId = (int)OrganizationId,
                                PartnerPersonId = (int)NewPersonId,
                                PositionId = (int)PositionId,
                                Position = Position,
                            };
                            context.OrganizationPerson.Add(org);
                            context.SaveChanges();
                        }
                        else
                        {
                            OrganizationPerson org = new OrganizationPerson()
                            {
                                OrganizationId = (int)OrganizationId,
                                PartnerPersonId = (int)NewPersonId,
                                //PositionId = (int)PositionId,
                                Position = Position,
                            };
                            context.OrganizationPerson.Add(org);
                            context.SaveChanges();
                        }
                    }
                    distibutedTransaction.Complete();
                }
                if (_hdl != null)
                    _hdl(NewPersonId);

                this.Close();
                new CardPerson(NewPersonId, _hdl).Show();

                //MessageBox.Show("Данные сохранены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось добавить новое физ. лицо в БД. \r\n" + "Причина: " + ex.Message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((PersonName.Length == 0) && (LastName.Length == 0) && (!DegreeId.HasValue) && (!RankId.HasValue) && (!OrganizationId.HasValue) && (!RubricId.HasValue) && (!FacultyId.HasValue) && (Title.Length == 0))
            {
                this.Close();
                return;
            }
            if (MessageBox.Show("Закрыть без сохранения?", "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                this.Close();
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
            //this.Text = FIO;
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
                    FIO += " " + FirstName.Substring(0, 1) + ".";
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
                    FIO += " " + SecondName.Substring(0, 1) + ".";
                }
                else
                {
                    FIO = SecondName.Substring(0, 1) + ".";
                }
            }
            NameInitials = FIO;
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

        private void cbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnRefreshPosList_Click(object sender, EventArgs e)
        {
            ComboServ.FillCombo(cbPosition, HelpClass.GetComboListByTable("dbo.Position"), true, false);
        }

        private void PositionListSetToFound(int? id)
        {
            //FillComboPosition();
            ComboServ.FillCombo(cbPosition, HelpClass.GetComboListByTable("dbo.Position"), true, false);
            PositionId = id;
        }

        private void btnPosHandBook_Click(object sender, EventArgs e)
        {
            //if (Utilities.FormIsOpened("Positions"))
            //    return;
            //new Positions().Show();
            if (Utilities.FormIsOpened("PositionListToFind"))
                Utilities.FormClose("PositionListToFind");

            new PositionListToFind(new UpdateIntHandler(PositionListSetToFound)).Show();
        }

        private void btnRefreshOrgList_Click(object sender, EventArgs e)
        {
            FillComboOrg();
        }

        private void OrgListSetToFound()
        {
            OrgListSetToFound(null);
        }
        private void OrgListSetToFound(int? id)
        {
            OrganizationId = id;
        }
        private void btnOrgHandBook_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("OrgListToFind"))
                Utilities.FormClose("OrgListToFind");

            new OrgListToFind(new UpdateIntHandler(OrgListSetToFound)).Show();
        }

        private void ddd(object sender, EventArgs e)
        {

        }

        private void CardNewPerson_Load(object sender, EventArgs e)
        {

        }
    }
}
