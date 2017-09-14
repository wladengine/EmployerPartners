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
    public partial class VKRNPRCard : Form
    {
        private string VKRSourceKey
        {
            get { return tbVKRSourceKey.Text.Trim(); }
            set { tbVKRSourceKey.Text = value; }
        }
        private string DocumentNumber
        {
            get { return tbDocumentNumber.Text.Trim(); }
            set { tbDocumentNumber.Text = value; }
        }
        private string NPR_LastName
        {
            get { return tbNPR_LastName.Text.Trim(); }
            set { tbNPR_LastName.Text = value; }
        }
        private string NPR_FirstName
        {
            get { return tbNPR_FirstName.Text.Trim(); }
            set { tbNPR_FirstName.Text = value; }
        }
        private string NPR_SecondName
        {
            get { return tbNPR_SecondName.Text.Trim(); }
            set { tbNPR_SecondName.Text = value; }
        }
        private string NPR_Position
        {
            get { return tbNPR_Position.Text.Trim(); }
            set { tbNPR_Position.Text = value; }
        }
        private string NPR_Degree
        {
            get { return tbNPR_Degree.Text.Trim(); }
            set { tbNPR_Degree.Text = value; }
        }
        private string NPR_Rank
        {
            get { return tbNPR_Rank.Text.Trim(); }
            set { tbNPR_Rank.Text = value; }
        }
        private string NPR_Account
        {
            get { return tbNPR_Account.Text.Trim(); }
            set { tbNPR_Account.Text = value; }
        }
        private string NPR_Chair
        {
            get { return tbNPR_Chair.Text.Trim(); }
            set { tbNPR_Chair.Text = value; }
        }
        private string NPR_Faculty
        {
            get { return tbNPR_Faculty.Text.Trim(); }
            set { tbNPR_Faculty.Text = value; }
        }
        private string VKRName
        {
            get { return tbVKRName.Text.Trim(); }
            set { tbVKRName.Text = value; }
        }
        private string VKRNameEng
        {
            get { return tbVKRNameEng.Text.Trim(); }
            set { tbVKRNameEng.Text = value; }
        }
        private string LicenseProgramName
        {
            get { return tbLicenseProgramName.Text.Trim(); }
            set { tbLicenseProgramName.Text = value; }
        }
        private string ObrazProgramName
        {
            get { return tbObrazProgramName.Text.Trim(); }
            set { tbObrazProgramName.Text = value; }
        }
        private string Crypt1
        {
            get { return tbCrypt.Text.Trim(); }
            set { tbCrypt.Text = value; }
        }
        private string Crypt
        {
            get { return ComboServ.GetComboId(cbCrypt); }
            set { ComboServ.SetComboId(cbCrypt, value); }
        }
        private string StudyPlanNum
        {
            get { return tbStudyPlanNum.Text.Trim(); }
            set { tbStudyPlanNum.Text = value; }
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
        public int VKRNPRCardId
        {
            get;
            set;
        }

        UpdateIntHandler _hndl;

        public VKRNPRCard(int id, UpdateIntHandler _hdl)
        {
            InitializeComponent();
            _Id = id;
            VKRNPRCardId = id;
            _hndl = _hdl;
            FillComboCrypt();
            FillCard();
            this.MdiParent = Util.mainform;
        }

        private void FillComboCrypt()
        {
            ComboServ.FillCombo(cbCrypt, HelpClass.GetComboListByQuery(@" select [Key] AS Id, [Key] as Name
                from dbo.VKR_Crypt order by [Sorting]"), false, true);
        }
        private void FillCard()
        {
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var vkr = (from x in context.VKR_Themes
                               join npr in context.VKR_Themes_NPR on x.Id equals npr.VKRThemeId
                               where x.Id == _Id
                               select new
                               {
                                   npr.NPR_Surname,
                                   npr.NPR_Name,
                                   npr.NPR_SecondName,
                                   npr.NPR_Position,
                                   npr.NPR_Degree,
                                   npr.NPR_Rank,
                                   npr.NPR_Account,
                                   npr.NPR_Chair,
                                   NPR_Faculty ="",
                                   x.VKRName,
                                   x.VKRNameEng,
                                   x.VKRSourceKey,
                                   x.DocumentNumber,
                                   x.Comment,
                               }
                               ).First();

                    VKRSourceKey = vkr.VKRSourceKey;
                    DocumentNumber = vkr.DocumentNumber;
                    NPR_LastName = vkr.NPR_Surname;
                    NPR_FirstName = vkr.NPR_Name;
                    NPR_SecondName = vkr.NPR_SecondName;
                    NPR_Position = vkr.NPR_Position;
                    NPR_Degree = vkr.NPR_Degree;
                    NPR_Rank = vkr.NPR_Rank;
                    NPR_Account = vkr.NPR_Account;
                    NPR_Chair = vkr.NPR_Chair;
                    NPR_Faculty = vkr.NPR_Faculty;
                    VKRName = vkr.VKRName;
                    VKRNameEng = vkr.VKRNameEng;
                    //LicenseProgramName = vkr.LicenseProgramName;
                   // ObrazProgramName = vkr.ObrazProgramName;
                   // Crypt = vkr.Crypt;
                   // StudyPlanNum = vkr.StudyPlanNum;
                    Comment = vkr.Comment;

                    try
                    {
                        //this.Text = "ВКР: " + FIO;
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
                MessageBox.Show("Пока ничего не работает. и не сохраняется");
                return;
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                { 
                    var vkr = context.VKR_Themes.Where(x => x.Id == _Id).First();

                    //vkr.VKRSourceKey = VKRSourceKey;
                    //vkr.DocumentNumber = DocumentNumber;
                    //vkr.NPR_LastName = NPR_LastName;
                    //vkr.NPR_FirstName = NPR_FirstName;
                    //vkr.NPR_SecondName = NPR_SecondName;
                    //vkr.NPR_Position = NPR_Position;
                    //vkr.NPR_Degree = NPR_Degree;
                    //vkr.NPR_Rank = NPR_Rank;
                    //vkr.NPR_Account = NPR_Account;
                    //vkr.NPR_Chair = NPR_Chair;
                    //vkr.NPR_Faculty = NPR_Faculty;
                    //vkr.VKRName = VKRName;
                    //vkr.VKRNameEng = VKRNameEng;
                    //vkr.LicenseProgramName = LicenseProgramName;
                    //vkr.ObrazProgramName = ObrazProgramName;
                    //vkr.Crypt = Crypt;
                    //vkr.StudyPlanNum = StudyPlanNum;
                    //vkr.Comment = Comment;
                    
                    //context.SaveChanges();

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
