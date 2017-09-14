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
    public partial class SAP_NPS : Form
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
        public SAP_NPS()
        {
            InitializeComponent();
            FillFacultyList();
            FillChairList();
            FillNPR();
            GetDateUpdated();
            SetAccessRight();
        }
        private void SetAccessRight()
        {
            if (Util.IsDBOwner() || Util.IsAdministrator())
            {
                btnUpdate.Visible = true;
                btnUpdate.Enabled = true;
            }
            //if (Util.IsOrgPersonWrite())
            //{
            //    btnUpdate.Enabled = true;
            //}

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
                FROM dbo.SAP_NPR  WHERE ((dbo.SAP_NPR.FullNAme is not null) and (dbo.SAP_NPR.FullName <> '') and (dbo.SAP_NPR.FullName <> 'ДГПХ')" + faculty + ") ORDER BY Name"), false, true);
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
                               where ((!String.IsNullOrEmpty(Faculty)) ? (x.Faculty == Faculty) : true) && (x.FullName != "ДГПХ") &&
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
                                   //x.Tabnum,
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
                        col.HeaderText = col.Name.Replace("_", " ");
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
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SAP_NPS_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Parent.Width > this.Width + 50 + this.Left)
                {
                    this.Width = this.Parent.Width - 50 - this.Left;
                }
                if (this.Parent.Height > this.Height + 50 + this.Top)
                {
                    this.Height = this.Parent.Height - 50 - this.Top;
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
                    for (int j = 0; j < 1 /*dgv.Columns.Count*/; j++)
                    {
                        //if (j == 0)
                        //    continue;
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Операция обновления данных потребует\r\n" + "обращения к другим серверам БД Университета \r\n" +
                "и может занять некоторое время \r\n" + "Подтверждаете продолжение? ", "Запрос на подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
            { return; }
            //this.UseWaitCursor = true;
            pBar1.Visible = true;
            pBar1.Minimum = 30;
            pBar1.Maximum = 90;
            pBar1.Value = 45;
            pBar1.Invalidate();
            if (!Utilities.UpdateSAP_ALL_PERS() || !Utilities.UpdateSAP_ORGSTRUCTURE() || !Utilities.UpdateSAP_POSITIONS())
            {
                //this.UseWaitCursor = false;
                pBar1.Visible = false;
                MessageBox.Show("Не удалось получить данные...\r\n" +
                    "[В большинстве случаев это означает, что \r\n" + "недостаточно прав доступа.]", "Отмена действия", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            pBar1.Value = 60;
            pBar1.Invalidate();
            if (Utilities.UpdateSAP_NPR())
            {
                //this.UseWaitCursor = false;
                pBar1.Value = 90;
                MessageBox.Show("Данные обновлены", "Успешное завершение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FillNPR();
                GetDateUpdated();
                pBar1.Visible = false;
                return;
            }
            else
            {
                //this.UseWaitCursor = false;
                pBar1.Visible = false;
                MessageBox.Show("Не удалось обновить данные... \r\n" +
                    "[В большинстве случаев это означает, что \r\n" + "недостаточно прав доступа к другим серверам баз данных.]", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
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
    }
}
