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
    public partial class PracticeStudentCard : Form
    {
        private string FIO
        {
            get { return tbFIO.Text.Trim(); }
            set { tbFIO.Text = value; }
        }
        private string _Org
        {
            get { return tbOrg.Text.Trim(); }
            set { tbOrg.Text = value; }
        }
        private int? OrgDogId
        {
            get { return ComboServ.GetComboIdInt(cbOrgDogovor); }
            set { ComboServ.SetComboId(cbOrgDogovor, value); }
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
        public int PStudentCardId
        {
            get;
            set;
        }
        private int? _OrgId
        {
            get;
            set;
        }
        UpdateVoidHandler _hndl;

        public PracticeStudentCard(int? id, int ? orgid, string orgname, UpdateVoidHandler _hdl)
        {
            InitializeComponent();
            _Id = id;
            PStudentCardId = (int)id;
            _OrgId = orgid;
            _Org = orgname;
            _hndl = _hdl;
            FillCombo();
            FillCard();
            this.MdiParent = Util.mainform;
        }
        private void FillCombo()
        {
            ComboServ.FillCombo(cbOrgDogovor, HelpClass.GetComboListByQuery(@" select distinct  CONVERT(varchar(100), Id) AS Id, [Document] as Name
                from dbo.OrganizationDogovor where OrganizationId = " + ((_OrgId.HasValue) ? _OrgId.ToString() : "null") + " and RubricId = 5 "), true, false);
        }
        private void FillCard()
        {
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var stud = (from x in context.PracticeLPStudent
                               where x.Id == _Id
                               select x).First();
                    FIO = stud.StudentFIO;
                    OrgDogId = stud.OrganizationDogovorId;
                    Comment = stud.Comment;
                    try
                    {
                        this.Text = "Студент: " + FIO;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
                return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //var pst = context.PracticeStudent.Where(x => x.Id == _Id).First();
                    var pst = context.PracticeLPStudent.Where(x => x.Id == _Id).First();

                    pst.StudentFIO = FIO;
                    pst.OrganizationDogovorId = OrgDogId;
                    pst.Comment = Comment;

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

        private void btnOrgCard_Click(object sender, EventArgs e)
        {
            if (_OrgId.HasValue)
            {
                if (Utilities.OrgCardIsOpened((int)_OrgId))
                    return;
                new CardOrganization(_OrgId, null).Show();
            }
        }

        private void btnOrgDogovorRefresh_Click(object sender, EventArgs e)
        {
            int? orgdogid = OrgDogId;
            FillCombo();
            OrgDogId = orgdogid;
        }
    }
}
