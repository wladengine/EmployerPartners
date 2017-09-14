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
    public partial class PositionListToFind : Form
    {
        UpdateIntHandler _hdl;

        private int? RowNumStartSearch;

        public PositionListToFind(UpdateIntHandler h)
        {
            InitializeComponent();
            _hdl = h;
            FillGrid();
            SetAccessRight();
            this.MdiParent = Util.mainform;
        }
        private void SetAccessRight()
        {
            if (Util.IsSuperUser())
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
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
                    var lst = (from x in context.Position
                               orderby x.Name
                               select new
                               {
                                   x.Id,
                                   Должность = x.Name,
                                   Должность_англ = x.NameEng,
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource1.DataSource = dt;
                    dgv.DataSource = bindingSource1;

                    foreach (string s in new List<string>() { "Id" })
                        if (dgv.Columns.Contains(s))
                            dgv.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgv.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "Column1")
                        {
                            col.HeaderText = "Действие";
                        }
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
                        dgv.Columns["Column1"].Width = 70;
                        dgv.Columns["ColumnEdit"].Width = 100;
                        //dgv.Columns["Должность"].Frozen = true;
                        dgv.Columns["Должность"].Width = 400;
                        dgv.Columns["Должность_англ"].Width = 400;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("нНе удалось загрузить список \r\n" + "Причина: " + ex.Message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new PositionNew(null, new UpdateIntHandler(FillGrid)).Show();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentRow.Index >= 0)
                {
                    int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                    string name = "";
                    string nameEng = "";
                    try
                    {
                        name = dgv.CurrentRow.Cells["Должность"].Value.ToString();
                    }
                    catch (Exception)
                    { }
                    try
                    {
                        nameEng = dgv.CurrentRow.Cells["Должность_англ"].Value.ToString();
                    }
                    catch (Exception)
                    { }

                    new PositionNew(id, new UpdateIntHandler(FillGrid), name, nameEng).Show();
                }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentRow.Index >= 0)
                {
                    string pname = "";
                    try
                    {
                        pname = dgv.CurrentRow.Cells["Должность"].Value.ToString();
                    }
                    catch (Exception)
                    {
                    }
                    if (MessageBox.Show("Удалить выбранное?\r\n" + pname, "Запрос на подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                        return;

                    try
                    {
                        int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());

                        using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                        {
                            context.Position.RemoveRange(context.Position.Where(x => x.Id == id));
                            context.SaveChanges();
                        }
                        FillGrid(null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось удалить запись...\r\n" + "Причина: " + ex.Message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
        }

        private void PositionListToFind_Load(object sender, EventArgs e)
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
                            int Posid = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                            if (_hdl != null)
                                _hdl(Posid);
                            this.Close();
                            return;
                        }
                        catch (Exception)
                        {
                        }
                    }
                    if (dgv.CurrentCell.ColumnIndex == 2)
                    {
                        if (!Util.IsSuperUser())
                        {
                            MessageBox.Show("Недостаточно прав для редактирования справочника должностей.", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                        string name = "";
                        string nameEng = "";
                        try
                        {
                            name = dgv.CurrentRow.Cells["Должность"].Value.ToString();
                        }
                        catch (Exception)
                        { }
                        try
                        {
                            nameEng = dgv.CurrentRow.Cells["Должность_англ"].Value.ToString();
                        }
                        catch (Exception)
                        { }

                        new PositionNew(id, new UpdateIntHandler(FillGrid), name, nameEng).Show();
                    }
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
                    for (int j = 0; j < /*8*/ dgv.Columns.Count; j++)
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
                    for (int j = 0; j < /*8*/ dgv.Columns.Count; j++)
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

                string filter = "";
                bool Start = true;
                foreach (DataGridViewColumn col in dgv.Columns)
                {
                    if (col.Name == "ColumnDiv" || col.Name == "Column1" || col.Name == "ColumnEdit" || col.Name == "Id")
                        continue;
                    if (!Start)
                    {
                        filter += " OR ";
                        Start = false;
                    }
                    filter += col.Name + " LIKE '%" + search + "%'";
                    Start = false;
                }

                bindingSource1.Filter = filter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    for (int j = 0; j < /*8*/ dgv.Columns.Count; j++)
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
    }
}
