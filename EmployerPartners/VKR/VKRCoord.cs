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
    public partial class VKRCoord : Form
    {
        public int? _VKRId
        {
            get { return ComboServ.GetComboIdInt(cbVKRYear); }
            set { ComboServ.SetComboId(cbVKRYear, value); }
        }

        public VKRCoord()
        {
            InitializeComponent();
            FillVKRYear();
            FillSection();
            FillCoordinator();
            this.MdiParent = Util.mainform;
        }

        private void FillVKRYear()
        {
            ComboServ.FillCombo(cbVKRYear, HelpClass.GetComboListByQuery(@" SELECT DISTINCT CONVERT(varchar(100), Id) AS Id, 
                CONVERT(varchar(100), StudyYear) AS Name 
                FROM VKR ORDER BY Name DESC "), false, false);
        }
        private void FillSection()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var section = (from x in context.VKRSection
                                   where x.VKRId == _VKRId
                                   select new
                                   {
                                       Группа_направлений = x.Name,
                                       x.Id,
                                       x.VKRId,
                                       Координатор = x.Coordinator,
                                       Должность = x.Position,
                                       Email = x.Email,
                                       Телефон = x.Phone,
                                       Моб_телефон = x.Mobiles,
                                       Комментарий = x.Comment
                                   }).ToList();
                    dgvSection.DataSource = section;

                    List<string> Cols = new List<string>() { "Id", "VKRId" };
                    foreach (string s in Cols)
                        if (dgvSection.Columns.Contains(s))
                            dgvSection.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvSection.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnDelSection")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnEditSection")
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
                        dgvSection.Columns["ColumnDiv"].Width = 6;
                        dgvSection.Columns["ColumnDelSection"].Width = 70;
                        dgvSection.Columns["ColumnEditSection"].Width = 70;
                        dgvSection.Columns["Группа_направлений"].Frozen = true;
                        dgvSection.Columns["Группа_направлений"].Width = 300;
                        dgvSection.Columns["Координатор"].Width = 200;
                        dgvSection.Columns["Комментарий"].Width = 200;
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
        private void FillFacultyStart()
        {
            if (dgvSection.CurrentCell != null)
                if (dgvSection.CurrentRow.Index >= 0)
                {
                    int id;
                    try
                    {
                        id = int.Parse(dgvSection.CurrentRow.Cells["Id"].Value.ToString());
                        FillFaculty(id);
                    }
                    catch (Exception)
                    {
                        return;
                    }
                }
        }
        private void FillFaculty(int? Id)
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKRSectionFaculty
                               join fac in context.Faculty on x.FacultyId equals fac.Id
                               where x.VKRSectionId == Id
                               select new
                               {
                                   Направление = fac.Name,
                                   x.Id,
                                   x.VKRSectionId,
                                   x.FacultyId
                               }).ToList();
                    dgvFaculty.DataSource = lst;
                    List<string> Cols = new List<string>() { "Id", "VKRSectionId", "FacultyId" };
                    foreach (string s in Cols)
                        if (dgvFaculty.Columns.Contains(s))
                            dgvFaculty.Columns[s].Visible = false;
                    dgvFaculty.Columns["Направление"].Width = 300;
                }
            }
            catch (Exception)
            {
            }
        }

        private void FillCoordinator()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.VKRCoordinator
                               join fac in context.Faculty on x.FacultyId equals fac.Id
                               where x.VKRId == _VKRId
                               select new
                               {
                                   Направление = fac.Name,
                                   Координатор = x.Name,
                                   x.Id,
                                   x.VKRId,
                                   x.FacultyId,
                                   Должность = x.Position,
                                   Email = x.Email,
                                   Телефон = x.Phone,
                                   Моб_телефон = x.Mobiles,
                                   Комментарий = x.Comment
                               }).ToList();
                    dgvCoordinator.DataSource = lst;

                    List<string> Cols = new List<string>() { "Id", "VKRId", "FacultyId" };
                    foreach (string s in Cols)
                        if (dgvCoordinator.Columns.Contains(s))
                            dgvCoordinator.Columns[s].Visible = false;
                    foreach (DataGridViewColumn col in dgvCoordinator.Columns)
                    {
                        col.HeaderText = col.Name.Replace("_", " ");
                        if (col.Name == "ColumnDelCoordinator")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnEditCoordinator")
                        {
                            col.HeaderText = "Действие";
                        }
                        if (col.Name == "ColumnDiv1")
                        {
                            col.HeaderText = "";
                        }
                    }
                    try
                    {
                        dgvCoordinator.Columns["ColumnDiv1"].Width = 6;
                        dgvCoordinator.Columns["ColumnDelCoordinator"].Width = 70;
                        dgvCoordinator.Columns["ColumnEditCoordinator"].Width = 70;
                        dgvCoordinator.Columns["Направление"].Frozen = true;
                        dgvCoordinator.Columns["Направление"].Width = 200;
                        dgvCoordinator.Columns["Координатор"].Frozen = true;
                        dgvCoordinator.Columns["Координатор"].Width = 200;
                        dgvCoordinator.Columns["Комментарий"].Width = 200;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            { }
        }

        private void VKRCoord_Load(object sender, EventArgs e)
        {
            FillFacultyStart();
        }
    }
}
