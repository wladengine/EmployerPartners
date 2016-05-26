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
    public partial class CardPartner_old
    {
        private int? _Id
        {
            get;
            set;
        }

        int CurrentContactRowIndex;
        int CurrentFilesRowIndex;

        int? InboxMessageId;
        UpdateVoidHandler _hndl;

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
        public string Contact
        {
            get { return tbContact.Text.Trim(); }
            set { tbContact.Text = value; }
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
        #region Contact
        private int? ContactId
        {
            get;
            set;
        }
        private Guid ContactGuidId;
        public string ContactName
        {
            get { return tbContactName.Text.Trim(); }
            set { tbContactName.Text = value; }
        }
        public string ContactNameEng
        {
            get { return tbContactNameEng.Text.Trim(); }
            set { tbContactNameEng.Text = value; }
        }
        public string ContactPosition
        {
            get { return tbContactPosition.Text.Trim(); }
            set { tbContactPosition.Text = value; }
        }
        public string ContactEmail
        {
            get { return tbContactEmail.Text.Trim(); }
            set { tbContactEmail.Text = value; }
        }
        public string ContactMobiles
        {
            get { return tbContactMobile.Text.Trim(); }
            set { tbContactMobile.Text = value; }
        }
        public string ContactPhone
        {
            get { return tbContactPhone.Text.Trim(); }
            set { tbContactPhone.Text = value; }
        }
        public string ContactComment
        {
            get { return tbContactComment.Text.Trim(); }
            set { tbContactComment.Text = value; }
        }

        #endregion
        #region File
        public string MessageTheme
        {
            get { return tbMessageTheme.Text.Trim(); }
            set { tbMessageTheme.Text = value; }
        }
        public string MessageText
        {
            get { return tbMessageText.Text.Trim(); }
            set { tbMessageText.Text = value; }
        }
        public string MessageAuthor
        {
            get { return tbMessageAuthor.Text.Trim(); }
            set { tbMessageAuthor.Text = value; }
        }
        public DateTime _MessageDate;
        public DateTime MessageDate
        {
            get { return _MessageDate; }
            set { _MessageDate = value; tbMessageDate.Text = value.ToShortDateString(); }
        }
        public List<KeyValuePair<int, string>> MessageFiles;
        public List<InboxFile> MessageFileAdd;

        #endregion
    }

    public class InboxFile
    {
        public string FileName;
        public byte[] FileData;

        public InboxFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files|*.*";
            if (ofd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                return;
            FileName = ofd.FileName.Substring(ofd.FileName.LastIndexOf('\\') + 1);
            FileStream fs = new FileStream(ofd.FileName, FileMode.Open, FileAccess.Read);
            FileData = new byte[fs.Length];
            fs.Read(FileData, 0, System.Convert.ToInt32(fs.Length));
        }
    }
    public class PartnerContactPersonList
    {
        public List<PartnerContactPerson> lst;
        int IndexFirstVisibleCell = 2;
        public DataGridView dgv;

        public PartnerContactPersonList(DataGridView d)
        {
            lst = new List<PartnerContactPerson>();
            dgv = d;
        }

        public void Add(Guid g)
        {
            lst.Add(new PartnerContactPerson(g));
            dgv.Rows.Add();
            dgv.Rows[dgv.Rows.Count-1].Cells["GuidId"].Value = g.ToString();
            dgv.CurrentCell = dgv.Rows[dgv.Rows.Count - 1].Cells[IndexFirstVisibleCell];
        }
        public void Add()
        {
            lst.Add(new PartnerContactPerson());
        }
        public PartnerContactPerson Last()
        {
            return lst.Last();
        }
        public void Remove(PartnerContactPerson s)
        {
            lst.Remove(s);
            dgv.Rows.Remove(dgv.CurrentRow);
        }

        public PartnerContactPerson Where(Guid g)
        {
            return lst.Where(x => x.GuidId == g).First();
        }
        public void Update(Guid g, int? id, string Name, string Position, string Comment, string NameEng, string Email, string Phone, string Mobile)
        {
            var c = lst.Where(x => x.GuidId == g).First();
            c.Id = id;
            c.Name = Name;
            c.Position = Position;
            c.Comment = Comment;
            c.NameEng = NameEng;
            c.Email = Email;
            c.Phone = Phone;
            c.Mobiles = Mobile;

            foreach (DataGridViewRow rw in dgv.Rows)
            {
                if (rw.Cells["GuidId"].Value.ToString() == g.ToString())
                {
                    rw.Cells["Id"].Value = c.Id.ToString();
                    rw.Cells["ФИО"].Value = c.Name;
                    rw.Cells["Должность"].Value = c.Position;
                    rw.Cells["Комментарий"].Value = c.Comment;
                }
            }
        }

        public void FindRow(Guid g)
        {
            foreach (DataGridViewRow rw in dgv.Rows)
            {
                if (rw.Cells["GuidId"].Value.ToString() == g.ToString())
                {
                    dgv.CurrentCell = rw.Cells[IndexFirstVisibleCell];
                }
            }
        }
    }
    public class PartnerContactPerson
    {
        public Guid GuidId; 
        public int? Id;
        public string Name;
        public string NameEng;
        public string Position;
        public string Comment;
        public string Email;
        public string Phone;
        public string Mobiles;

        public PartnerContactPerson()
        {
            GuidId = Guid.NewGuid();
        }
        public PartnerContactPerson(Guid g)
        {
            GuidId = g;
        } 


    }
}
