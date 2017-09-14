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
    public partial class VKRListThemes : Form
    {
        public int? _id;

        public int? _VKRId
        {
            get { return ComboServ.GetComboIdInt(cbVKRYear); }
            set { ComboServ.SetComboId(cbVKRYear, value); }
        }

        public VKRListThemes()
        {
            InitializeComponent();
            FillVKRYear();
            FillGrid(_VKRId);
            this.MdiParent = Util.mainform;
            SetAccessRight();
        }
        private void SetAccessRight()
        {

        }
        private void FillVKRYear()
        {
            ComboServ.FillCombo(cbVKRYear, HelpClass.GetComboListByQuery(@" SELECT DISTINCT CONVERT(varchar(100), Id) AS Id, 
                CONVERT(varchar(100), StudyYear) AS Name 
                FROM VKR ORDER BY Name DESC "), false, false);
        }
        private void FillGrid(int? id)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKRThemes
                               join org in context.Organization on x.OrganizationId equals org.Id
                               join orgagreed in context.Organization on x.OrganizationId equals orgagreed.Id into _orgagreed
                               from orgagreed in _orgagreed.DefaultIfEmpty()
                               join fac in context.Faculty on x.FacultyId equals fac.Id into _fac
                               from fac in _fac.DefaultIfEmpty()
                               join lp in context.LicenseProgram on x.LicenseProgramId equals lp.Id
                               join st in context.StudyLevel on lp.StudyLevelId equals st.Id
                               //join progt in context.ProgramType on lp.ProgramTypeId equals progt.Id
                               join op in context.ObrazProgram on x.ObrazProgramId equals op.Id
                               where x.VKRId == id
                               orderby fac.Name, lp.Code, st.Name, op.Number, op.Name, org.Name, x.VKRName
                               select new
                               {
                                   x.Id,
                                   Организация = ((String.IsNullOrEmpty(org.ShortName)) ? ((String.IsNullOrEmpty(org.MiddleName)) ? org.Name : org.MiddleName) : org.ShortName),
                                   Согласовано = ((String.IsNullOrEmpty(orgagreed.ShortName)) ? ((String.IsNullOrEmpty(orgagreed.MiddleName)) ? orgagreed.Name : orgagreed.MiddleName) : orgagreed.ShortName),
                                   Тема_ВКР = x.VKRName,
                                   Тема_ВКР_англ =x.VKRNameEng,
                                   Направление = lp.Code + "  " + st.Name + "  " + lp.Name,
                                   Образовательная_программа = "[ " + op.Number + " ] " + op.Name,
                                   Подразделение = fac.Name,
                                   История_воникновения_темы = x.History,
                                   Примечание = x.Comment,
                                   x.VKRId
                               }).ToList();

                    DataTable dt = new DataTable();
                    dt = Utilities.ConvertToDataTable(lst);
                    bindingSource1.DataSource = dt;
                    dgv.DataSource = bindingSource1;


                    foreach (string s in new List<string>() { "VKRId", "Id" })
                        if (dgv.Columns.Contains(s))
                            dgv.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgv.Columns)
                        col.HeaderText = col.Name.Replace("_", " ");
                    try
                    {
                        dgv.Columns["Организация"].Frozen = true;
                        dgv.Columns["Организация"].Width = 200;
                        dgv.Columns["Тема_ВКР"].Width = 300;
                        dgv.Columns["Тема_ВКР_англ"].Width = 300;
                        dgv.Columns["Направление"].Width = 200;
                        dgv.Columns["Образовательная_программа"].Width = 200;
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
        private void VKRListThemes_Load(object sender, EventArgs e)
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

        private void cbVKRYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid(_VKRId);
        }

       
    }
}
