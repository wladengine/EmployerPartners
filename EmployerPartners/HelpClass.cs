using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Configuration;


namespace EmployerPartners
{
    public static class HelpClass
    {
        public static List<KeyValuePair<string, string>> GetComboListByTable(string tableName)
        {
            return GetComboListByTable(tableName, null);
        }
        public static List<KeyValuePair<string, string>> GetComboListByTable(string tableName, string orderBy)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();

                    foreach (ListItem ob in context.Database.SqlQuery<ListItem>(string.Format("SELECT CONVERT(varchar(100), Id) AS Id, Name FROM {0} {1}", tableName, string.IsNullOrEmpty(orderBy) ? "ORDER BY 2" : orderBy)))
                    {
                        lst.Add(new KeyValuePair<string, string>(ob.Id, ob.Name));
                    }                   

                    return lst;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка при запросе " + exc.Message+": "+exc.InnerException, "Ошибка!");
                return null;
            }
        }
        public static List<KeyValuePair<string, string>> GetComboListByQuery(string query)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();

                    foreach (ListItem ob in context.Database.SqlQuery<ListItem>(query))
                    {
                        lst.Add(new KeyValuePair<string, string>(ob.Id, ob.Name));
                    }

                    return lst;                    
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Ошибка при запросе " + exc.Message+": "+exc.InnerException, "Ошибка!");

                return null;
            }
        }

        // заполнение DataGrid
        public static void FillDataGrid(DataGridView grid, DataView dv)
        {
            FillDataGrid(grid, dv, false);
        }
        public static void FillDataGrid(DataGridView grid, DataView dv, bool saveOrder)
        {
            string sortedColumn = string.Empty;
            ListSortDirection order = ListSortDirection.Ascending;
            bool sorted = false;
            int rowIndex = 0;

            if (saveOrder && grid.SortOrder != SortOrder.None)
            {
                sorted = true;
                sortedColumn = grid.SortedColumn.Name;
                order = grid.SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;
                rowIndex = grid.CurrentRow == null ? -1 : grid.CurrentRow.Index;
            }

            grid.DataSource = dv;
            grid.Columns["Id"].Visible = false;
            grid.Update();

            if (saveOrder && grid.Rows.Count > 0)
            {
                if (sorted && grid.Columns.Contains(sortedColumn))
                    grid.Sort(grid.Columns[sortedColumn], order);
                if (rowIndex >= 0 && rowIndex <= grid.Rows.Count)
                    grid.CurrentCell = grid[1, rowIndex];
            }
        }
        public static void FillDataGrid(DataGridView grid, BDClass bdc, string query, string filters)
        {
            FillDataGrid(grid, bdc, query, filters, "");
        }
        public static void FillDataGrid(DataGridView grid, BDClass bdc, string query, string filters, string orderby)
        {
            FillDataGrid(grid, bdc, query, filters, orderby, false);
        }
        public static void FillDataGrid(DataGridView grid, BDClass bdc, string query, string filters, string orderby, bool saveOrder)
        {
            string sortedColumn = string.Empty;
            ListSortDirection order = ListSortDirection.Ascending;
            bool sorted = false;
            int index = 0;


            if (saveOrder && grid.SortOrder != SortOrder.None)
            {
                sorted = true;
                sortedColumn = grid.SortedColumn.Name;
                order = grid.SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;
                index = grid.CurrentRow == null ? -1 : grid.CurrentRow.Index;
            }

            DataSet ds;
            DataTable dt;

            try
            {
                if (query != "")
                {
                    ds = bdc.GetDataSet(query + " " + filters + " " + orderby);
                    dt = ds.Tables[0];
                }
                else
                {
                    dt = new DataTable();
                    dt.Columns.Add("Id");
                }

                DataView dv = new DataView(dt);
                dv.AllowNew = false;

                grid.DataSource = dv;
                grid.Columns["Id"].Visible = false;
                grid.Update();

                if (saveOrder && grid.Rows.Count > 0)
                {
                    if (sorted && grid.Columns.Contains(sortedColumn))
                        grid.Sort(grid.Columns[sortedColumn], order);
                    if (index >= 0 && index <= grid.Rows.Count)
                        grid.CurrentCell = grid[1, index];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сервера: " + ex.Message, "Ошибка!");

            }
        }

        public static DataView GetDataView(DataGridView grid, BDClass bdc, string query, string filters)
        {
            return GetDataView(grid, bdc, query, filters, "");
        }
        public static DataView GetDataView(DataGridView grid, BDClass bdc, string query, string filters, string orderby)
        {
            return GetDataView(grid, bdc, query, filters, orderby, false);
        }
        public static DataView GetDataView(DataGridView grid, BDClass bdc, string query, string filters, string orderby, bool saveOrder)
        {
            DataSet ds;
            DataTable dt;

            if (query != "")
            {
                ds = bdc.GetDataSet(query + " " + filters + " " + orderby);
                dt = ds.Tables[0];
            }
            else
            {
                dt = new DataTable();
                dt.Columns.Add("Id");
            }

            DataView dv = new DataView(dt);
            dv.AllowNew = false;
            return dv;

        }


        public static void Search(DataGridView dgv, string sColumnName, string sPattern)
        {
            int start = (dgv.CurrentRow != null) ? dgv.CurrentRow.Index : 0;

            for (int i = start; i < dgv.Rows.Count; i++)
            {
                object cellValue = dgv.Rows[i].Cells[sColumnName].Value;
                // Если ячейка грида соответствует полю таблицы имеющему значение NULL,
                // то значение ячейки (объект "Value") становится null,
                // чтобы избежать "null reference exception" в момент вызова метода ToString(),
                // присваиваем Value объект string.Empty
                cellValue = (cellValue == null ? string.Empty : cellValue);

                if (cellValue.ToString().StartsWith(sPattern, true, System.Globalization.CultureInfo.CurrentCulture))
                {
                    //dgv.FirstDisplayedScrollingRowIndex = i;
                    //dgv.Rows[i].Selected = true;
                    dgv.CurrentCell = dgv[sColumnName, i];
                    break;
                }
                if (i == dgv.Rows.Count - 1)
                    i = -1;
                if (i == start-1)
                    break;
            }
        }
    }
}
