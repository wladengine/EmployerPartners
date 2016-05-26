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
        public string PersonName
        {
            get { return tbName.Text.Trim(); }
            set { tbName.Text = lblName.Text = value; }
        }
        public string NameEng
        {
            get { return tbNameEng.Text.Trim(); }
            set { tbNameEng.Text = value; }
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
