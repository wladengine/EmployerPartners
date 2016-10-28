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
    public partial class VKRStudentCard : Form
    {
        private string FIO
        {
            get { return tbStudentFIO.Text.Trim(); }
            set { tbStudentFIO.Text = value; }
        }
        private string DR
        {
            get { return tbDR.Text.Trim(); }
            set { tbDR.Text = value; }
        }
        private string Account
        {
            get { return tbAccount.Text.Trim(); }
            set { tbAccount.Text = value; }
        }
        private string VKRTheme
        {
            get { return tbVKRTheme.Text.Trim(); }
            set { tbVKRTheme.Text = value; }
        }
        private string VKRThemeEng
        {
            get { return tbVKRThemeEng.Text.Trim(); }
            set { tbVKRThemeEng.Text = value; }
        }
        private int? OrgId
        {
            get { return ComboServ.GetComboIdInt(cbOrgId); }
            set { ComboServ.SetComboId(cbOrgId, value); }
        }
        private string Supervisor
        {
            get { return tbSupervisor.Text.Trim(); }
            set { tbSupervisor.Text = value; }
        }
        private string SupervisorAccount
        {
            get { return tbSupervisorAccount.Text.Trim(); }
            set { tbSupervisorAccount.Text = value; }
        }
        private string Chair
        {
            get { return tbChair.Text.Trim(); }
            set { tbChair.Text = value; }
        }
        private int? VKRSourceId
        {
            get { return ComboServ.GetComboIdInt(cbVKRSource); }
            set { ComboServ.SetComboId(cbVKRSource, value); }
        }
        private string Faculty
        {
            get { return tbFaculty.Text.Trim(); }
            set { tbFaculty.Text = value; }
        }
        private string OP
        {
            get { return tbOP.Text.Trim(); }
            set { tbOP.Text = value; }
        }
        private string LP
        {
            get { return tbLP.Text.Trim(); }
            set { tbLP.Text = value; }
        }
        private string Comment
        {
            get { return tbComment.Text.Trim(); }
            set { tbComment.Text = value; }
        }
        private int? _Id
        {
            get;
            set;
        }
        public int VKRStudentCardId
        {
            get;
            set;
        }
        UpdateVoidHandler _hndl;

        public VKRStudentCard(int id, string op, string lp, string facname, UpdateVoidHandler _hdl)
        {
            InitializeComponent();
            _Id = id;
            VKRStudentCardId = id;
            OP = op;
            LP = lp;
            Faculty = facname;
            _hndl = _hdl;
            FillOrgList();
            FillVKRSource();
            FillCard();
            this.MdiParent = Util.mainform;
        }
        private void FillOrgList()
        {
            ComboServ.FillCombo(cbOrgId, HelpClass.GetComboListByQuery(@" select distinct  CONVERT(varchar(100), Id) AS Id, 
                (CASE MiddleName WHEN NULL THEN Organization.Name WHEN '' THEN Organization.Name WHEN ' ' THEN Organization.Name ELSE Middlename END) AS Name
                from dbo.Organization order by Name"), true, false);
        }
        private void FillVKRSource()
        {
            ComboServ.FillCombo(cbVKRSource, HelpClass.GetComboListByTable("dbo.VKRSOurce"), true, false);
        }
        private void FillCard()
        {
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var stud = (from x in context.VKROPStudent
                                where x.Id == _Id
                                select x).First();
                    FIO = stud.StudentFIO;
                    DR = stud.DR;
                    Account =stud.Accout;
                    VKRTheme = stud.VKRName;
                    VKRThemeEng = stud.VKRNameEng;
                    OrgId = stud.OrganizationId;
                    Supervisor = stud.Supervisor;
                    SupervisorAccount = stud.SupervisorAccount;
                    VKRSourceId = stud.VKRSourceId;
                    Chair = stud.Chair;
                    Comment = stud.Comment;
                    try
                    {
                        this.Text = "ВКР: " + FIO;
                    }
                    catch (Exception)
                    {
                    }
                }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOrgCard_Click(object sender, EventArgs e)
        {
            if (OrgId.HasValue)
            {
                if (Utilities.OrgCardIsOpened((int)OrgId))
                    return;
                new CardOrganization(OrgId, null).Show();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
                return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var vkrst = context.VKROPStudent.Where(x => x.Id == _Id).First();

                    vkrst.VKRName = VKRTheme;
                    vkrst.VKRNameEng = VKRThemeEng;
                    vkrst.OrganizationId = OrgId;
                    vkrst.Supervisor = Supervisor;
                    vkrst.SupervisorAccount = SupervisorAccount;
                    vkrst.VKRSourceId = VKRSourceId;
                    vkrst.Chair = Chair;
                    vkrst.Comment = Comment;

                    context.SaveChanges();

                    //MessageBox.Show("Данные сохранены", "Сообщение");
                    if (_hndl != null)
                        _hndl(_Id);

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
