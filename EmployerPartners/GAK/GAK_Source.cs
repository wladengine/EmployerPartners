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
    public partial class GAK_Source : Form
    {
        public GAK_Source()
        {
            InitializeComponent();
            FillGrid();
            this.MdiParent = Util.mainform;
            SetAccessRight();
        }
        private void SetAccessRight()
        {

        }
        private void FillGrid()
        {
            FillGrid(null);
        }
        private void FillGrid(int? id)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.GAK_ChairmanSource
                               orderby x.Faculty
                               select new
                               {
                                   УНП = x.Faculty,
                                   Источник_сз_председателя_УМК_или_декана = x.Source,
                                   Номера = x.Numbers,
                                   Примечание = x.Comment,
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
                        if (col.Name == "ColumnEdit")
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
                        dgv.Columns["ColumnEdit"].Width = 100;
                        dgv.Columns["УНП"].Frozen = true;
                        dgv.Columns["УНП"].Width = 250;
                        dgv.Columns["Источник_сз_председателя_УМК_или_декана"].Width = 250;
                        dgv.Columns["Номера"].Width = 250;
                        dgv.Columns["Примечание"].Width = 200;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }

        }

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgv.CurrentCell != null)
                    if (dgv.CurrentRow.Index >= 0)
                    {
                        if (dgv.CurrentCell.ColumnIndex == 1)
                        {
                            try
                            {
                                dgv.CurrentCell = dgv.CurrentRow.Cells["УНП"];
                            }
                            catch (Exception)
                            {
                            }
                            ///
                            try
                            {
                                int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                                //string FacName = dgv.CurrentRow.Cells["УНП"].Value.ToString();
                                //if (Utilities.VKRStudentCardIsOpened(id))
                                //    return;
                                //new VKRStudentCard(id, OP, LP, FacName, new UpdateVoidHandler(FillGrid)).Show();
                                new GAK_SourceEdit(id, new UpdateIntHandler(FillGrid)).Show();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                        }
                    }
            }
            catch (Exception)
            {

            }
        }
    }
}
