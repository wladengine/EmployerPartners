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
    public partial class ListOrganizations : Form
    {

        public ListOrganizations()
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            this.Text = "Список организаций";
            FillCard();
        }

        private void FillCard()
        {
            FillCard(null);
        }
        private void FillCard(Guid? id)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from org in context.Organization
                           select new
                           {
                               org.Id,
                               Название = org.Name,
                               ИНН = org.INN,
                           }).ToList();
                dgv.DataSource = lst;
                if (dgv.Columns.Contains("Id"))
                    dgv.Columns["Id"].Visible = false;
                if (id.HasValue)
                    foreach (DataGridViewRow rw in dgv.Rows)
                        if (rw.Cells[0].Value.ToString() == id.Value.ToString())
                        {
                            dgv.CurrentCell = rw.Cells["Название"];
                            break;
                        }
            }

        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentRow.Index>=0)
                {
                    Guid id = Guid.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                    new CardPartner(id, new UpdateVoidHandler(FillCard)).Show();
                }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentCell.RowIndex >= 0)
                {
                    Guid id = Guid.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                    new CardPartner(id, new UpdateVoidHandler(FillCard)).Show();
                }
        }

        private void btnAddPartner_Click(object sender, EventArgs e)
        {
            new CardPartner(null, new UpdateVoidHandler(FillCard)).Show();
        }
    }

    public delegate void UpdateVoidHandler(Guid? id);
}
