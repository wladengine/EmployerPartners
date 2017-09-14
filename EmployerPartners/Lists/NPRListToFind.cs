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
    public partial class NPRListToFind : Form
    {
        private string Faculty
        {
            get { return ComboServ.GetComboId(cbfaculty); }
            set { ComboServ.SetComboId(cbfaculty, value); }
        }
        private string Chair
        {
            get { return ComboServ.GetComboId(cbChair); }
            set { ComboServ.SetComboId(cbChair, value); }
        }

        //UpdateVoidHandler _hdl;
        UpdateStringHandler _hdl;

        public NPRListToFind(UpdateStringHandler h)
        {
            InitializeComponent();
            FillFacultyList();
            FillChairList();
            FillNPR();
            _hdl = h;
            GetDateUpdated();
            //SetAccessRight();
            this.MdiParent = Util.mainform;
        }
        private void FillFacultyList()
        {
            ComboServ.FillCombo(cbfaculty, HelpClass.GetComboListByQuery(@" SELECT DISTINCT CONVERT(varchar(255), dbo.SAP_NPR.Faculty) AS Id, 
                CONVERT(varchar(255), dbo.SAP_NPR.Faculty) AS Name 
                FROM dbo.SAP_NPR  WHERE ((dbo.SAP_NPR.Faculty is not null) and (dbo.SAP_NPR.Faculty <> '')) ORDER BY Name"), false, true);
        }
        private void FillChairList()
        {
            string faculty = "";
            if (!String.IsNullOrEmpty(Faculty))
            {
                faculty = " and (dbo.SAP_NPR.Faculty = '" + Faculty + "') ";
            }
            ComboServ.FillCombo(cbChair, HelpClass.GetComboListByQuery(@" SELECT DISTINCT CONVERT(varchar(255), dbo.SAP_NPR.FullName) AS Id, 
                CONVERT(varchar(255), dbo.SAP_NPR.FullName) AS Name 
                FROM dbo.SAP_NPR  WHERE ((dbo.SAP_NPR.FullNAme is not null) and (dbo.SAP_NPR.FullName <> '')" + faculty + ") ORDER BY Name"), false, true);
        }
        private void GetDateUpdated()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var x = context.SAP_NPR.First();
                    lblDateUpdated.Text = "По данным SAP на " + ((x.TIMESTAMP.HasValue) ? x.TIMESTAMP.Value.Date.ToString("dd.MM.yyyy") : "");
                }
            }
            catch (Exception)
            {
            }
        }
        private void FillNPR()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.SAP_NPR
                               where ((!String.IsNullOrEmpty(Faculty)) ? (x.Faculty == Faculty) : true) && //(x.FullName != "ДГПХ") &&
                                        ((!String.IsNullOrEmpty(Chair)) ? (x.FullName == Chair) : true)
                               orderby x.Lastname, x.Name, x.Surname
                               select new
                               {
                                   ФИО = ((!String.IsNullOrEmpty(x.Lastname)) ? x.Lastname : "") + " " + ((!String.IsNullOrEmpty(x.Name)) ? x.Name : "") + " " + ((!String.IsNullOrEmpty(x.Surname)) ? x.Surname : ""),
                                   Аккаунт = x.UsridAd,
                                   Степень = x.Degree,
                                   Звание = x.Titl2,
                                   Должность = x.Position,
                                   Кафедра = x.FullName,
                                   УНП = x.Faculty,
                                   Email = x.Email,
                                   x.Persnum,
                                   x.Tabnum,
                               }).Distinct().OrderBy(x => x.ФИО).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource1.DataSource = dt;
                    dgv.DataSource = bindingSource1;

                    List<string> Cols = new List<string>() { "Persnum", "Tabnum" };

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
                        dgv.Columns["ФИО"].Frozen = true;
                        dgv.Columns["ФИО"].Width = 250;
                        dgv.Columns["Степень"].Width = 200;
                        dgv.Columns["Звание"].Width = 100;
                        dgv.Columns["Должность"].Width = 200;
                        dgv.Columns["Кафедра"].Width = 250;
                        dgv.Columns["УНП"].Width = 250;
                        dgv.Columns["Email"].Width = 150;
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

        private void NPRListToFind_Load(object sender, EventArgs e)
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
                    for (int j = 0; j < 3 /*dgv.Columns.Count*/; j++)
                    {
                        if ((j == 0) || (j == 1))
                            continue;
                        int length = 1;
                        length = dgv[j, i].Value.ToString().Length;
                        length = (length <= 15) ? length : 15;
                        if (dgv[j, i].Value.ToString().Substring(0, length).ToUpper().Contains(search))
                        {
                            dgv.CurrentCell = dgv[(j > 0) ? j - 1 : j, i];
                            exit = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void cbfaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillChairList();
            FillNPR();
        }

        private void cbChair_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillNPR();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                            string Persnum = dgv.CurrentRow.Cells["Persnum"].Value.ToString();
                            string Tabnum = dgv.CurrentRow.Cells["Tabnum"].Value.ToString();
                            if (_hdl != null) 
                                _hdl(null, Tabnum);
                            this.Close();
                            return;
                        }
                        catch (Exception)
                        {
                        }

                    }
                    //if (dgv.CurrentCell.ColumnIndex == 2)
                    //{
                    //    //если будет карточка
                    //}
                }
        }
    }
}
