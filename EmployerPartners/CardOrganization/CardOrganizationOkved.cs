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
    public partial class CardOrganizationOkved : Form
    {
        public int? _id;
        public int OrgId;
        UpdateVoidHandler _hdl;

        public CardOrganizationOkved(int? Id, int orgId, UpdateVoidHandler h)
        {
            InitializeComponent();
            _id = Id;
            OrgId = orgId;
            _hdl = h;
            this.MdiParent = Util.mainform;
            FillCard();
        }
        public void FillCard()
        {
            btnAdd.Text = (_id.HasValue) ? "Обновить" : "Добавить";
            ComboServ.FillCombo(cbType, HelpClass.GetComboListByTable("dbo.OkvedType"), false, false);

            if (!_id.HasValue) return;
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.OrganizationOkved
                               where x.Id == _id.Value
                               && x.OrganizationId == OrgId
                               select new 
                               {
                                   x.OkvedName,
                                   x.OkvedTypeId,
                                   x.Okved,
                               }).FirstOrDefault();
                if (lst == null)
                    return;
                tbokved.Text = lst.Okved;
                tbName.Text = lst.OkvedName;
                ComboServ.SetComboId(cbType, lst.OkvedTypeId);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.OrganizationOkved
                           where x.OrganizationId == OrgId
                           && x.Id != _id && x.Okved == tbokved.Text.Trim()
                           select new
                           {
                               x.Id
                           }).ToList().Count();
                if (lst > 0)
                {
                    MessageBox.Show("Такой ОКВЭД уже был добавлен");
                    return;
                }
                else if (!_id.HasValue)
                {
                    OrganizationOkved org = new OrganizationOkved()
                    {
                        OrganizationId = OrgId,
                        Okved = tbokved.Text.Trim(),
                        OkvedTypeId = ComboServ.GetComboIdInt(cbType) ?? 1,
                        OkvedName = tbName.Text.Trim()
                    };
                    context.OrganizationOkved.Add(org);
                    context.SaveChanges();
                    _id = org.Id;
                }
                else if (_id.HasValue)
                {
                    OrganizationOkved org = context.OrganizationOkved.Where(x => x.Id == _id.Value).First();
                    org.Okved = tbokved.Text.Trim();
                    org.OkvedTypeId = ComboServ.GetComboIdInt(cbType) ?? 1;
                    org.OkvedName = tbName.Text.Trim();
                    context.SaveChanges();
                }
                if (_hdl != null && _id.HasValue)
                    _hdl(_id);
            }
            this.Close();
        }

    }
}
