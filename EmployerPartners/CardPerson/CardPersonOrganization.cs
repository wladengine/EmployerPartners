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
    public partial class CardPersonOrganization: Form
    {
        public int? _id;
        public int PersId;
        UpdateVoidHandler _hdl;

        public CardPersonOrganization()
        {
            InitializeComponent();
        }
        public CardPersonOrganization(int? Id, int persId, UpdateVoidHandler h)
        {
            InitializeComponent();
            _id = Id;
            PersId = persId;
            _hdl = h;
            this.MdiParent = Util.mainform;
            FillCard();
        }
        public void FillCard()
        {
            btnAdd.Text = (_id.HasValue) ? "Обновить" : "Добавить";
            ComboServ.FillCombo(cbOrganization, HelpClass.GetComboListByTable("dbo.Organization"), false, false);

            if (!_id.HasValue) return;
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.OrganizationPerson
                           join p in context.Organization
                           on x.OrganizationId equals p.Id
                           where x.Id == _id.Value
                           && x.PartnerPersonId == PersId
                               select new 
                               {
                                   p.Id,
                                   p.Name,
                                   x.Position,
                                   x.PositionEng,
                                   x.Comment,
                               }).FirstOrDefault();
                if (lst == null)
                    return;

                tbposition.Text = lst.Position;
                tbpositionEng.Text = lst.PositionEng;
                tbComment.Text = lst.Comment;
                ComboServ.SetComboId(cbOrganization, lst.Id);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int? OrgId = ComboServ.GetComboIdInt(cbOrganization);
            if (!OrgId.HasValue)
            { 
                MessageBox.Show("Организация не выбрана");
                return;
            }
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.OrganizationPerson
                           where x.PartnerPersonId == PersId
                           && x.Id != _id 
                           && x.OrganizationId == OrgId.Value
                           select new
                           {
                               x.Id
                           }).ToList().Count();
                if (lst > 0)
                {
                    MessageBox.Show("Такая организация уже было добавлена");
                    return;
                }
                else if (!_id.HasValue)
                {
                    OrganizationPerson org = new OrganizationPerson()
                    {
                        OrganizationId = OrgId.Value,
                        PartnerPersonId = PersId,
                        Position = tbposition.Text.Trim(),
                        PositionEng = tbpositionEng.Text.Trim(),
                        Comment = tbComment.Text.Trim(),
                    };
                    context.OrganizationPerson.Add(org);
                    context.SaveChanges();
                    _id = org.Id;
                }
                else if (_id.HasValue)
                {
                    OrganizationPerson org = context.OrganizationPerson.Where(x => x.Id == _id.Value).First();
                    org.OrganizationId = OrgId.Value;
                    org.Position = tbposition.Text.Trim();
                    org.PositionEng = tbpositionEng.Text.Trim();
                    org.Comment = tbComment.Text.Trim();
                    context.SaveChanges();
                }
                if (_hdl != null && _id.HasValue)
                    _hdl(_id);
            }
            this.Close();
        }

        private void cbPerson_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
