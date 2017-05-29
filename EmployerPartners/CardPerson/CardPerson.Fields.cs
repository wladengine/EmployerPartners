using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace EmployerPartners
{
    public partial class CardPerson
    {
        #region Common
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
            set { tbName.Text = lblName.Text = value; }
        }
        public string NameInitials
        {
            get { return tbNameInitials.Text.Trim(); }
            set { tbNameInitials.Text = value; }
        }

        public string LastNameEng
        {
            get { return tbLastNameEng.Text.Trim(); }
            set { tbLastNameEng.Text = value; }
        }
        public string FirstNameEng
        {
            get { return tbFirstNameEng.Text.Trim(); }
            set { tbFirstNameEng.Text = value; }
        }
        public string SecondNameEng
        {
            get { return tbSecondNameEng.Text.Trim(); }
            set { tbSecondNameEng.Text = value; }
        }
        public string NameEng
        {
            get { return tbNameEng.Text.Trim(); }
            set { tbNameEng.Text = value; }
        }
        public string NameInitialsEng
        {
            get { return tbNameInitialsEng.Text.Trim(); }
            set { tbNameInitialsEng.Text = value; }
        }

        public string Account
        {
            get { return tbAccount.Text.Trim(); }
            set { tbAccount.Text = value; }
        }

        public string Email
        {
            get { return tbEmail.Text.Trim(); }
            set { tbEmail.Text = value; }
        }
        public string WebSite
        {
            get { return tbWebSite.Text.Trim(); }
            set { tbWebSite.Text = value; }
        }
        public string Phone
        {
            get { return tbPhone.Text.Trim(); }
            set { tbPhone.Text = value; }
        }
        public string Mobiles
        {
            get { return tbMobiles.Text.Trim(); }
            set { tbMobiles.Text = value; }
        }
        public string Title
        {
            get { return tbTitle.Text.Trim(); }
            set { tbTitle.Text = value; }
        }
        public string TitleEng
        {
            get { return tbTitleEng.Text.Trim(); }
            set { tbTitleEng.Text = value; }
        }
        public string Comment
        {
            get { return tbComment.Text.Trim(); }
            set { tbComment.Text = value; }
        }
        public int? CountryId
        { 
            get; 
            set; 
        }
        public string sCountry
        {
            get { return tbCountry.Text.Trim();  }
            set { tbCountry.Text = value; }
        }

        public int? PrefixId
        {
            get { return ComboServ.GetComboIdInt(cbPrefix); }
            set { ComboServ.SetComboId(cbPrefix, value); }
        }
        public int? DegreeId
        {
            get { return ComboServ.GetComboIdInt(cbDegree); }
            set { ComboServ.SetComboId(cbDegree, value); }
        }
        public int? Degree2Id
        {
            get { return ComboServ.GetComboIdInt(cbDegree2); }
            set { ComboServ.SetComboId(cbDegree2, value); }
        }
        public int? RankId
        {
            get { return ComboServ.GetComboIdInt(cbRank); }
            set { ComboServ.SetComboId(cbRank, value); }
        }
        public int? Rank2Id
        {
            get { return ComboServ.GetComboIdInt(cbRank2); }
            set { ComboServ.SetComboId(cbRank2, value); }
        }
        public int? RankHonoraryId
        {
            get { return ComboServ.GetComboIdInt(cbRankHonorary); }
            set { ComboServ.SetComboId(cbRankHonorary, value); }
        }
        public int? RankHonorary2Id
        {
            get { return ComboServ.GetComboIdInt(cbRankHonorary2); }
            set { ComboServ.SetComboId(cbRankHonorary2, value); }
        }
        public int? RankStateId
        {
            get { return ComboServ.GetComboIdInt(cbRankState); }
            set { ComboServ.SetComboId(cbRankState, value); }
        }
        public int? RankState2Id
        {
            get { return ComboServ.GetComboIdInt(cbRankState2); }
            set { ComboServ.SetComboId(cbRankState2, value); }
        }
        public int? AreaId
        {
            get { return ComboServ.GetComboIdInt(cbArea); }
            set { ComboServ.SetComboId(cbArea, value); }
        }
        public bool isGraduateSPbGU
        {
            get { return chbIsGraduate.Checked; }
            set { chbIsGraduate.Checked = value; }
        }
        public bool isAlumni
        {
            get { return chbAlumni.Checked; }
            set { chbAlumni.Checked = value; }
        }
        public bool isGAK
        {
            get { return chbGAK.Checked; }
            set { chbGAK.Checked = value; }
        }
        public bool isGAKChairMan
        {
            get { return chbGAKChairman.Checked; }
            set { chbGAKChairman.Checked = value; }
        }
        public bool isGAK2016
        {
            get { return chbGAK2016.Checked; }
            set { chbGAK2016.Checked = value; }
        }
        public bool isGAKChairMan2016
        {
            get { return chbGAKChairman2016.Checked; }
            set { chbGAKChairman2016.Checked = value; }
        }
        public bool IsPersonDataAgreed
        {
            get { return chbIsPersonDataAgreed.Checked; }
            set { chbIsPersonDataAgreed.Checked = value; }
        }
        //chbIsPersonDataChecked
        public bool IsPersonDataChecked
        {
            get { return chbIsPersonDataChecked.Checked; }
            set { chbIsPersonDataChecked.Checked = value; }
        }
        public int? GraduateYear
        {
            get {
                int year;
                if (!int.TryParse(tbGraduateYear.Text, out year) || String.IsNullOrEmpty(tbGraduateYear.Text))
                {
                    return null;
                }
                else 
                    return year;
            }
            set { tbGraduateYear.Text = value.ToString(); }
        }
        #endregion
    }
}
