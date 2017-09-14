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
    public partial class PersonListToFind : Form
    {
        private int? RubricId
        {
            get { return ComboServ.GetComboIdInt(cbRubric); }
            set { ComboServ.SetComboId(cbRubric, value); }
        }
        private int? FacultyId
        {
            get { return ComboServ.GetComboIdInt(cbFaculty); }
            set { ComboServ.SetComboId(cbFaculty, value); }
        }
        private bool isGAK
        {
            get { return chbGAK.Checked; }
            set { chbGAK.Checked = value; }
        }
        private bool isGAKChairMan
        {
            get { return chbGAKChairman.Checked; }
            set { chbGAKChairman.Checked = value; }
        }
        private bool isGAK2016
        {
            get { return chbGAK2016.Checked; }
            set { chbGAK2016.Checked = value; }
        }
        private bool isGAKChairMan2016
        {
            get { return chbGAKChairman2016.Checked; }
            set { chbGAKChairman2016.Checked = value; }
        }

        UpdateIntHandler _hdl;

        public PersonListToFind(UpdateIntHandler h)
        {
            InitializeComponent();
            _hdl = h;
            FillCard();
            FillGrid();
            this.MdiParent = Util.mainform;
        }
        private void FillCard()
        {
            ComboServ.FillCombo(cbRubric, HelpClass.GetComboListByTable("dbo.Rubric"), false, true);
            ComboServ.FillCombo(cbFaculty, HelpClass.GetComboListByTable("dbo.Faculty"), false, true);
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
                var lst = (from org in context.PartnerPerson
                           join r in context.PartnerPersonRubric on org.Id equals r.PartnerPersonId into _r
                           from r in _r.DefaultIfEmpty()
                           join f in context.PartnerPersonFaculty on org.Id equals f.PartnerPersonId into _f
                           from f in _f.DefaultIfEmpty()
                           where 
                          
                           (RubricId.HasValue ? r.RubricId == RubricId : true) &&
                           (FacultyId.HasValue ? f.FacultyId == FacultyId : true) &&
                           ((isGAK == true) ? org.IsGAK == true : true) &&
                           ((isGAKChairMan == true) ? org.IsGAKChairman == true : true) &&
                           ((isGAK2016 == true) ? org.IsGAK2016 == true : true) &&
                           ((isGAKChairMan2016 == true) ? org.IsGAKChairman2016 == true : true)

                           orderby org.Name 
                           select new
                           {
                               ФИО = org.Name,
                               org.Id,
                               ФИО_англ = org.NameEng,
                               Префикс = org.PartnerPersonPrefix.Name,
                               Регистрационный_номер_ИС_Партнеры  = org.Account,
                               Ученая_степень = org.Degree.Name,
                               Ученое_звание = org.Rank.Name,
                               Почетное_звание = org.RankHonorary.Name,
                               Государственное_или_военное_звание = org.RankState.Name,
                               Регалии_доп_данные = org.Title,
                               Входит_в_составы_ГЭК_2017 = org.IsGAK,
                               Председатель_ГЭК_2017 = org.IsGAKChairman,
                               Входит_в_составы_ГЭК_2016 = org.IsGAK2016,
                               Председатель_ГЭК_2016 = org.IsGAKChairman2016,
                               Основная_сфера_деятельности = org.ActivityArea.Name,
                               
                               Страна = org.Country.Name,
                               Email = org.Email,
                               Телефон = org.Phone,
                               
                               Комментарий = org.Comment,
                           }).Distinct().OrderBy(x => x.ФИО).ToList();

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
                    dgv.Columns["ФИО"].Frozen = true;
                    dgv.Columns["ФИО"].Width = 200;
                    dgv.Columns["Префикс"].Width = 60;
                    dgv.Columns["Ученая_степень"].Width = 150;
                    dgv.Columns["Ученое_звание"].Width = 150;
                    dgv.Columns["Почетное_звание"].Width = 150;
                    dgv.Columns["Государственное_или_военное_звание"].Width = 150;
                }
                catch (Exception)
                {
                }
                
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сформировать список физических лиц\r\n" + "Причина: " + ex.Message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void PersonListToFind_Load(object sender, EventArgs e)
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            int? id = null;
            if (dgv.CurrentCell != null)
                if (dgv.CurrentRow.Index >= 0)
                {
                    id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                }
            FillGrid(id);
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
                    for (int j = 0; j < 6 /*dgv.Columns.Count*/; j++)
                    {
                        if (j == 0 || j == 1 || j == 2 )
                            continue;
                        if (dgv[j, i].ColumnIndex == 0)
                        {
                            int length = 1;
                            length = dgv[j, i].Value.ToString().Length;
                            length = (length <= 15) ? length : 15;
                            if (dgv[j, i].Value.ToString().Substring(0, length).ToUpper().Contains(search))
                            {
                                dgv.CurrentCell = dgv[j, i];
                                exit = true;
                                break;
                            }
                        }
                        else
                        {
                            if (dgv[j, i].Value.ToString().ToUpper().Contains(search))
                            {
                                dgv.CurrentCell = dgv[j, i];
                                exit = true;
                                break;
                            }
                        }
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

        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentRow.Index >= 0)
                {
                    if (dgv.CurrentCell.ColumnIndex == 1)
                    {
                        try
                        {
                            int PersonId = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                            if (_hdl != null)
                                _hdl(PersonId);
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
                            int PersonId = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                            if (Utilities.PersonCardIsOpened(PersonId))
                                return;
                            new CardPerson(PersonId, null).Show();
                        }
                        catch
                        {
                        }
                    }
                }
        }
    }
}
