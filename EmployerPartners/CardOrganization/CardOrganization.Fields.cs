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
        public string OrgNameNew
        {
            get { return tbNameNew.Text.Trim(); }
            set { tbNameNew.Text = lblName.Text = value; }
        }
        public string NameEng
        {
            get { return tbNameEng.Text.Trim(); }
            set { tbNameEng.Text = value; }
        }
        public string NameEngNew
        {
            get { return tbNameEngNew.Text.Trim(); }
            set { tbNameEngNew.Text = value; }
        }
        public string ShortName
        {
            get { return tbShortName.Text.Trim(); }
            set { tbShortName.Text = value; }
        }
        public string ShortNameNew
        {
            get { return tbShortNameNew.Text.Trim(); }
            set { tbShortNameNew.Text = value; }
        }
        public string ShortNameEng
        {
            get { return tbShortNameEng.Text.Trim(); }
            set { tbShortNameEng.Text = value; }
        }
        public string ShortNameEngNew
        {
            get { return tbShortNameEngNew.Text.Trim(); }
            set { tbShortNameEngNew.Text = value; }
        }
        public string MiddleName
        {
            get { return tbMiddleName.Text.Trim(); }
            set { tbMiddleName.Text = value; }
        }
        public string MiddleNameNew
        {
            get { return tbMiddleNameNew.Text.Trim(); }
            set { tbMiddleNameNew.Text = value; }
        }
        public string okved
        {
            get { return tbOkved.Text.Trim(); }
            set { tbOkved.Text = value; }
        }
        public string okveddop
        {
            get { return tbOkvedDop.Text.Trim(); }
            set { tbOkvedDop.Text = value; }
        }
        public string oecd
        {
            get { return tbOECD.Text.Trim(); }
            set { tbOECD.Text = value; }
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
        public int? AreaProfessionalId
        {
            get { return ComboServ.GetComboIdInt(cbAreaProfessional); }
            set { ComboServ.SetComboId(cbAreaProfessional, value); }
        }
        public string INN
        {
            get { return tbINN.Text.Trim(); }
            set { tbINN.Text = value; }
        }
        public string INNNew
        {
            get { return tbINNNew.Text.Trim(); }
            set { tbINNNew.Text = value; }
        }
        public string NameDate
        {
            get { return tbNameDate.Text.Trim(); }
            set { tbNameDate.Text = value; }
        }
        public string NameDateNew
        {
            get { return tbNameDateNew.Text.Trim(); }
            set { tbNameDateNew.Text = value; }
        }
        public string OGRN
        {
            get { return tbOGRN.Text.Trim(); }
            set { tbOGRN.Text = value; }
        }
        public string OGRNNew
        {
            get { return tbOGRNNew.Text.Trim(); }
            set { tbOGRNNew.Text = value; }
        }
        public string OGRNDate
        {
            get { return tbOGRNDate.Text.Trim(); }
            set { tbOGRNDate.Text = value; }
        }
        public string OGRNDateNew
        {
            get { return tbOGRNDateNew.Text.Trim(); }
            set { tbOGRNDateNew.Text = value; }
        }
        public string CloseDate
        {
            get { return tbCloseDate.Text.Trim(); }
            set { tbCloseDate.Text = value; }
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
        public bool SourceCharter
        {
            get { return chkbSourceCharter.Checked; }
            set { chkbSourceCharter.Checked = value; }
        }
        public bool SourceEGRUL
        {
            get { return chkbSourceEGRUL.Checked; }
            set { chkbSourceEGRUL.Checked = value; }
        }
        public bool SourceSite
        {
            get { return chkbSourceSite.Checked; }
            set { chkbSourceSite.Checked = value; }
        }
        public bool CardChecked
        {
            get { return chkbCardChecked.Checked; }
            set { chkbCardChecked.Checked = value; }
        }
        public bool IsActual
        {
            get { return chkbIsActual.Checked; }
            set { chkbIsActual.Checked = value; }
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
        public string Address
        {
            get { return tbAddress.Text.Trim(); }
            set { tbAddress.Text = value; }
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
