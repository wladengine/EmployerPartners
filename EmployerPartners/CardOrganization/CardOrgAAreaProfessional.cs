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
    public partial class CardOrgAAreaProfessional : Form
    {
        private int OrgId
        {
            get;
            set;
        }
        public int CardOrgAAPOrgId
        {
            get;
            set;
        }
        UpdateIntHandler _hndl;

        public CardOrgAAreaProfessional(int orgid, UpdateIntHandler _hdl)
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            OrgId = orgid;
            CardOrgAAPOrgId = orgid;
            _hndl = _hdl;
            FillGrid();
        }
        private void FillGrid()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.ActivityAreaProfessional
                               select new
                               {
                                   Код = x.Code,
                                   Область_деятельности = x.Name,
                                   x.Id
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource1.DataSource = dt;
                    dgv.DataSource = bindingSource1;

                    List<string> Cols = new List<string>() { "Id" };

                    foreach (string s in Cols)
                        if (dgv.Columns.Contains(s))
                            dgv.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnAddAAP")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDiv")
                        {
                            col.HeaderText = "";
                        }
                    }
                    try
                    {
                        dgv.Columns["ColumnDiv"].Width = 6;
                        dgv.Columns["ColumnAddAAP"].Width = 120;
                        //dgv.Columns["Область_деятельности"].Frozen = true;
                        dgv.Columns["Область_деятельности"].Width = 500;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Не удается загрузить данные.\r\r" + exc.Message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentRow.Index >= 0)
                {
                    if (dgv.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            dgv.CurrentCell = dgv.CurrentRow.Cells["Область_деятельности"];
                        }
                        catch (Exception)
                        {
                        }
                        //проверка наличия области деятельности в карточке
                        try
                        {
                            int AAId = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                            string AAPName = (dgv.CurrentRow.Cells["Область_деятельности"].Value != null) ? dgv.CurrentRow.Cells["Область_деятельности"].Value.ToString() : "";
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                var lst = (from x in context.OrganizationActivityAreaProfessional
                                           where (x.ActivityAreaProfessionalId == AAId) && (x.OrganizationId == OrgId)
                                           select new
                                           {
                                               AAP = x.Id,
                                           }).ToList().Count();
                                if (lst > 0)
                                {
                                    MessageBox.Show("Область деятельности\r\n" + AAPName + "\r\n" +
                                        "уже добавлена в карточку организации. \r\n", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        //собственно добавление
                        try
                        {
                            int aapid = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                            {
                                OrganizationActivityAreaProfessional oaap = new OrganizationActivityAreaProfessional();
                                oaap.ActivityAreaProfessionalId = aapid;
                                oaap.OrganizationId = OrgId;
                                context.OrganizationActivityAreaProfessional.Add(oaap);
                                context.SaveChanges();

                                if (_hndl != null)
                                    //if (OrgId.HasValue)
                                    _hndl(OrgId);
                                this.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
