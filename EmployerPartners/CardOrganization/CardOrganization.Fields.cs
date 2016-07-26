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
    public partial class CardOrganization
    {
        #region Common
        public string OrgName
        {
            get { return tbName.Text.Trim(); }
            set { tbName.Text = lblName.Text = value; }
        }
        public string NameEng
        {
            get { return tbNameEng.Text.Trim(); }
            set { tbNameEng.Text = value; }
        }
        public string ShortName
        {
            get { return tbShortName.Text.Trim(); }
            set { tbShortName.Text = value; }
        }
        public string ShortNameEng
        {
            get { return tbShortNameEng.Text.Trim(); }
            set { tbShortNameEng.Text = value; }
        }
        public string MiddleName
        {
            get { return tbMiddleName.Text.Trim(); }
            set { tbMiddleName.Text = value; }
        }
        public string okved
        {
            get { return tbOkved.Text.Trim(); }
            set { tbOkved.Text = value; }
        }
        public int? OwnershipTypeId
        {
            get { return ComboServ.GetComboIdInt(cbOwnershipType); }
            set { ComboServ.SetComboId(cbOwnershipType, value); }
        }
        public int? ActivityGoalId
        {
            get { return ComboServ.GetComboIdInt(cbActivityGoal); }
            set { ComboServ.SetComboId(cbActivityGoal, value); }
        }
        public int? NationalAffiliationId
        {
            get { return ComboServ.GetComboIdInt(cbNationalAffiliation); }
            set { ComboServ.SetComboId(cbNationalAffiliation, value); }
        }
        public int? AreaId
        {
            get { return ComboServ.GetComboIdInt(cbArea); }
            set { ComboServ.SetComboId(cbArea, value); }
        }
        public string INN
        {
            get { return tbINN.Text.Trim(); }
            set { tbINN.Text = value; }
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
        public string Fax
        {
            get { return tbFax.Text.Trim(); }
            set { tbFax.Text = value; }
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
        public string Comment
        {
            get { return tbComment.Text.Trim(); }
            set { tbComment.Text = value; }
        }
        public string Source
        {
            get { return tbSource.Text.Trim(); }
            set { tbSource.Text = value; }
        }
        public string Description
        {
            get { return tbDescription.Text.Trim(); }
            set { tbDescription.Text = value; }
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
        public string RegionCode;
        public int? RegionId
        { get; set; }
        public string sRegion
        {
            get { return tbRegion.Text.Trim(); }
            set { tbRegion.Text = value; }
        }
        public string City
        {
            get { return tbCity.Text.Trim(); }
            set { tbCity.Text = value; }
        }
        public string Street
        {
            get { return tbStreet.Text.Trim(); }
            set { tbStreet.Text = value; }
        }
        public string House
        {
            get { return tbHouse.Text.Trim(); }
            set { tbHouse.Text = value; }
        }
        public string Apartment
        {
            get { return tbApartment.Text.Trim(); }
            set { tbApartment.Text = value; }
        }
        public string Code
        {
            get { return tbCode.Text.Trim(); }
            set { tbCode.Text = value; }
        }
        public string CodeKLADR
        {
            get { return tbKladr.Text.Trim(); }
            set { tbKladr.Text = value; }
        }
        
        #endregion
    }
}
