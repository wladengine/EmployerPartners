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
    public partial class OrgListToFind : Form
    {
        UpdateIntHandler _hdl;

        private int? RowNumStartSearch;

        public OrgListToFind(UpdateIntHandler h)
        {
            InitializeComponent();
            _hdl = h;
            FillGrid();
            this.MdiParent = Util.mainform;
        }
        private void FillGrid()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from org in context.Organization
                               orderby org.Name
                               select new
                               {
                                   Полное_наименование = org.Name,
                                   org.Id,
                                   Среднее_наименование = org.MiddleName,
                                   Краткое_наименование = org.ShortName,
                                   Наименование_англ = org.NameEng,
                                   Краткое_наименование_англ = org.ShortNameEng,
                                   org.Email,
                                   Телефон = org.Phone,
                                   Web_сайт = org.WebSite,
                                   Город = org.City,
                                   Улица = org.Street,
                                   Дом = org.House,
                                   Помещение = org.Apartment,
                                   Комментарий = org.Comment,
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
                        if (col.Name == "Column1")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDiv")
                        {
                            col.HeaderText = "";
                        }
                        if (col.Name == "ColumnCard")
                        {
                            col.HeaderText = "Действие";
                        }
                    }

                    try
                    {
                        dgv.Columns["ColumnDiv"].Width = 6;
                        dgv.Columns["Column1"].Width = 80;
                        dgv.Columns["ColumnCard"].Width = 80;
                        dgv.Columns["Полное_наименование"].Frozen = true;
                        dgv.Columns["Полное_наименование"].Width = 300;
                        dgv.Columns["Среднее_наименование"].Width = 200;
                        dgv.Columns["Краткое_наименование"].Width = 150;
                        dgv.Columns["Наименование_англ"].Width = 150;
                        dgv.Columns["Краткое_наименование_англ"].Width = 150;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сформировать список организаций\r\n" + "Причина: " + ex.Message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void OrgListToFind_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Parent.Width > this.Width + 30 + this.Left)
                {
                    this.Width = this.Parent.Width - 30 - this.Left;
                }
                if (this.Parent.Height > this.Height + 30 + this.Top)
                {
                    this.Height = this.Parent.Height - 30 - this.Top;
                }
            }
            catch (Exception)
            {
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
                            int Orgid = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                            if (_hdl != null)
                                _hdl(Orgid);
                            this.Close();
                            return;
                        }
                        catch (Exception)
                        {
                        }
                        
                    }
                    if (dgv.CurrentCell.ColumnIndex == 2)
                    {
                        try
                        {
                            int Orgid = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                            if (Utilities.OrgCardIsOpened(Orgid))
                                return;
                            new CardOrganization(Orgid, null).Show();
                        }
                        catch
                        {
                        }
                    }
                }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string search = tbSearch.Text.Trim().ToUpper();
                bool exit = false;
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (exit)
                    { break; }
                    for (int j = 0; j < 8 /*dgv.Columns.Count*/; j++)
                    {
                        if (j == 0 || j == 1 || j == 2)     //для кнопок
                            continue;
                        if (dgv[j, i].Value.ToString().ToUpper().Contains(search))
                        {
                            //dgv[j, i].Style.BackColor = Color.White;
                            dgv.CurrentCell = dgv[j, i];
                            exit = true;
                            RowNumStartSearch = i + 1;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnSearchNext_Click(object sender, EventArgs e)
        {
            if (!RowNumStartSearch.HasValue)
                return;
            try
            {
                string search = tbSearch.Text.Trim().ToUpper();
                bool exit = false;
                int k = (int)RowNumStartSearch;
                for (int i = k; i < dgv.RowCount; i++)
                {
                    if (exit)
                    //{ break; }
                    { return; }
                    for (int j = 0; j < 8 /*dgv.Columns.Count*/; j++)
                    {
                        if (j == 0 || j == 1 || j == 2)     //для кнопок
                            continue;
                        if (dgv[j, i].Value.ToString().ToUpper().Contains(search))
                        {
                            //dgv[j, i].Style.BackColor = Color.White;
                            dgv.CurrentCell = dgv[j, i];
                            exit = true;
                            RowNumStartSearch = i + 1;
                            break;
                        }
                    }
                }
                MessageBox.Show("Поиск завершен. Образец не найден.", "Поиск", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
            }
        }

        private void btnSearchPrevious_Click(object sender, EventArgs e)
        {
            if (!RowNumStartSearch.HasValue)
                return;
            try
            {
                string search = tbSearch.Text.Trim().ToUpper();
                bool exit = false;
                int k = (int)RowNumStartSearch - 2;
                for (int i = k; i >= 0 /*dgv.RowCount*/; i--)
                {
                    if (exit)
                    //{ break; }
                    { return; }
                    for (int j = 0; j < 8 /*dgv.Columns.Count*/; j++)
                    {
                        if (j == 0 || j == 1 || j == 2)     //для кнопок
                            continue;
                        if (dgv[j, i].Value.ToString().ToUpper().Contains(search))
                        {
                            //dgv[j, i].Style.BackColor = Color.White;
                            dgv.CurrentCell = dgv[j, i];
                            exit = true;
                            //RowNumStartSearch = i - 1;
                            RowNumStartSearch = i + 1;
                            break;
                        }
                    }
                }
                MessageBox.Show("Поиск завершен. Образец не найден.", "Поиск", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
            }
        }

        private void btnShowSearchResult_Click(object sender, EventArgs e)
        {
            try
            {
                //dataView.RowFilter = "Name LIKE '%jo%'"     // values that contain 'jo'
                string search = tbSearch.Text.Trim();   //.ToUpper();
                bindingSource1.Filter = "Полное_наименование LIKE '%" + search + "%'" + " OR " +
                                        "Среднее_наименование LIKE '%" + search + "%'" + " OR " +
                                        "Краткое_наименование LIKE '%" + search + "%'" + " OR " +
                                        "Наименование_англ LIKE '%" + search + "%'" + " OR " +
                                        "Краткое_наименование_англ LIKE '%" + search + "%'";
            }
            catch (Exception)
            {
            }
        }

        private void btnRemoveFilter_Click(object sender, EventArgs e)
        {
            try
            {
                bindingSource1.RemoveFilter();
            }
            catch (Exception)
            { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
