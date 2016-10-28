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
    public partial class CardOrgPreviousName : Form
    {
        public string OrgName
        {
            get { return tbName.Text.Trim(); }
            set { tbName.Text = value; }
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
        public string NameDate
        {
            get { return tbNameDate.Text.Trim(); }
            set { tbNameDate.Text = value; }
        }
        public string INN
        {
            get { return tbINN.Text.Trim(); }
            set { tbINN.Text = value; }
        }
        public string OGRN
        {
            get { return tbOGRN.Text.Trim(); }
            set { tbOGRN.Text = value; }
        }
        public string OGRNDate
        {
            get { return tbOGRNDate.Text.Trim(); }
            set { tbOGRNDate.Text = value; }
        }
        public string CloseDate
        {
            get { return tbCloseDate.Text.Trim(); }
            set { tbCloseDate.Text = value; }
        }

        public int OrgId
        {
            get;
            set;
        }

        public CardOrgPreviousName(int orgid)
        {
            InitializeComponent();
            OrgId = orgid;
            FillCard();
            this.MdiParent = Util.mainform;
        }

        private void FillCard()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var org = (from x in context.OrganizationNames
                               where x.OrganizationId == OrgId
                               orderby x.Id descending
                               select x).First();
                    OrgName = org.Name;
                    MiddleName = org.MiddleName;
                    ShortName = org.ShortName;
                    NameEng = org.NameEng;
                    ShortNameEng = org.ShortNameEng;
                    INN = org.INN;
                    NameDate = (org.NameDate.HasValue) ? org.NameDate.Value.Date.ToString("dd.MM.yyyy") : "";
                    OGRN = org.OGRN; 
                    OGRNDate = (org.OGRNDate.HasValue) ? org.OGRNDate.Value.Date.ToString("dd.MM.yyyy") : "";
                    CloseDate = (org.CloseDate.HasValue) ? org.CloseDate.Value.Date.ToString("dd.MM.yyyy") : "";
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
