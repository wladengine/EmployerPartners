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
    public partial class PracticeOrg : Form
    {
        public int? FacId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
        }
        public int? RubricId
        {
            get { return ComboServ.GetComboIdInt(cbRubric); }
            set { ComboServ.SetComboId(cbRubric, value); }
        }

        public PracticeOrg()
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            FillCombo();
            FillOrgList();
        }
        private void FillCombo()
        {
            ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("dbo.Faculty"), false, true);
            ComboServ.FillCombo(cbRubric, HelpClass.GetComboListByTable("dbo.Rubric"), false, true);
        }
        private void FillOrgList()
        {
            FillOrgList(null, null);
        }
        private void FillOrgList(int? FacId, int? RubricId)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                string sqlOrg = "SELECT * FROM Organization ";
                string sqlWhere = " ";
                string sqlOrderBy = " order by SpbGU desc, Name";
                if (FacId.HasValue)
                {
                    if (RubricId.HasValue)
                    {
                        sqlWhere = "where Id in (select OrganizationId from OrganizationFaculty where FacultyId = " + FacId + "and RubricId = " + RubricId + ")";
                    }
                    else
                    {
                        sqlWhere = "where Id in (select OrganizationId from OrganizationFaculty where FacultyId = " + FacId + ")";
                    }
                }
                else
                {
                    if (RubricId.HasValue)
                    {
                        sqlWhere = "where Id in (select OrganizationId from OrganizationRubric where RubricId = " + RubricId + ")";
                    }
                    else
                    {
                     
                    }
                }
                sqlOrg = sqlOrg + sqlWhere + sqlOrderBy;

                var OrgTable = context.Database.SqlQuery<Organization>(sqlOrg);

                var lst = (from org in OrgTable
                           select new 
                           {
                               //Действие = "Добавить в практику",
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

                bindingSource1.DataSource = lst;
                dgv.DataSource = bindingSource1;

                List<string> Cols = new List<string>() { "Id"};

                foreach (string s in Cols)
                    if (dgv.Columns.Contains(s))
                        dgv.Columns[s].Visible = false;
                foreach (DataGridViewColumn col in dgv.Columns)
                    col.HeaderText = col.Name.Replace("_", " ");
                //foreach (DataGridViewRow row in dgv.Rows)
                //    row.Cells[1].Value = "Добавить в практику";

                try
                {
                    /*Color lightGray = Color.FromName("LightGray");
                    Color Blue = Color.FromName("Blue");
                    dgv.Columns["Действие"].DefaultCellStyle.ForeColor = Blue;
                    dgv.Columns["Действие"].Frozen = true;
                    dgv.Columns["Действие"].Width = 120;*/
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

        private void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillOrgList(FacId, RubricId);
        }

        private void cbRubric_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillOrgList(FacId, RubricId);
        }

        private void btnAddToPractice_Click(object sender, EventArgs e)
        {
            //bool mark = false;
            string check = "false";
            foreach (DataGridViewRow row in dgv.Rows)
            {
                check = "false";
                try
                {
                    check = row.Cells["Column1"].Value.ToString();
                }
                catch (Exception)
                {
                }
                
                if (check == "true")
                {
                    using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                    {
                        PracticeLPOrganization org = new PracticeLPOrganization();
                        org.OrganizationId = int.Parse(row.Cells["Id"].Value.ToString());
                        
                        context.PracticeLPOrganization.Add(org);
                        context.SaveChanges();
                    }
                }
            }
        }

        private void dgv_DoubleClick(object sender, EventArgs e)
        {
            string col1 = "";
            try 
	        {	        
                col1 = dgv.Rows[dgv.CurrentCell.RowIndex].Cells["Column1"].Value.ToString();
            }
	        catch (Exception)
	        {
                col1 = "Значение не определено";
	        }
            MessageBox.Show("Текущая организация: \r\n" + dgv.Rows[dgv.CurrentCell.RowIndex].Cells["Полное_наименование"].Value + "\r\n" +
               "Значение Column1: " + col1);
        }
    }
}
