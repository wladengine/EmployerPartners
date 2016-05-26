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
    public partial class ListPersons : Form
    {
        public int? DegreeId
        {
            get { return ComboServ.GetComboIdInt(cbDegree); }
            set { ComboServ.SetComboId(cbDegree, value); }
        }
        public int? RankId
        {
            get { return ComboServ.GetComboIdInt(cbRank); }
            set { ComboServ.SetComboId(cbRank, value); }
        } 
        public int? ActivityAreaId
        {
            get { return ComboServ.GetComboIdInt(cbActivityArea); }
            set { ComboServ.SetComboId(cbActivityArea, value); }
        }
        public int? CountryId
        {
            get { return ComboServ.GetComboIdInt(cbCountry); }
            set { ComboServ.SetComboId(cbCountry, value); }
        }
        public int? RegionId
        {
            get { return ComboServ.GetComboIdInt(cbRegion); }
            set { ComboServ.SetComboId(cbRegion, value); }
        }

        public ListPersons()
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            this.Text = "Список физ. лиц";
            FillCard();
            FillGrid();
        }

        private void FillCard()
        {
            ComboServ.FillCombo(cbDegree, HelpClass.GetComboListByTable("dbo.Degree"), false, true);
            ComboServ.FillCombo(cbRank, HelpClass.GetComboListByTable("dbo.Rank"), false, true);
            ComboServ.FillCombo(cbActivityArea, HelpClass.GetComboListByTable("dbo.ActivityArea"), false, true);
            ComboServ.FillCombo(cbCountry, HelpClass.GetComboListByTable("dbo.Country"), false, true);
            ComboServ.FillCombo(cbRegion, HelpClass.GetComboListByTable("dbo.Region"), false, true);
        }
        private void FillGrid()
        {
            FillGrid(null);
        }

        private void FillGrid(int? id)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from org in context.PartnerPerson
                           where 
                           (DegreeId.HasValue ? org.DegreeId == DegreeId : true) &&
                           (RankId.HasValue ? org.RankId == RankId : true) &&
                           (ActivityAreaId.HasValue ? org.ActivityAreaId == ActivityAreaId : true) &&
                           (CountryId.HasValue ? org.CountryId == CountryId : true) &&
                           (RegionId.HasValue ? org.RegionId == RegionId : true) 
                           select new
                           {
                               org.Id,
                               ФИО = org.Name,
                               ФИО_англ = org.NameEng,
                               Регалии = org.Title,
                               Ученая_степень = org.Degree.Name,
                               Ученое_звание = org.Rank.Name,
                               Область_деятельности = org.ActivityArea.Name,
                               Выпускник_СПбГУ = org.IsSPbGUGraduate.HasValue && org.IsSPbGUGraduate.Value ? "да" : "нет",
                               Год_выпуска = org.SPbGUGraduateYear,
                               Ассоциация_выпускников = org.AlumniAssociation.HasValue && org.AlumniAssociation.Value ? "да" : "нет",
                               Страна = org.Country.Name,
                               Email = org.Email,
                               Телефон = org.Phone,
                               Мобильный_телефон = org.Mobiles,
                               Web_сайт = org.WebSite,
                               Комментарий = org.Comment,
                           }).ToList();
                dgv.DataSource = lst;
                List<string> Cols = new List<string>() { "Id" };

                foreach (string s in Cols)
                    if (dgv.Columns.Contains(s))
                        dgv.Columns[s].Visible = false;
                foreach (DataGridViewColumn col in dgv.Columns)
                    col.HeaderText = col.Name.Replace("_", " ");

                if (id.HasValue)
                    foreach (DataGridViewRow rw in dgv.Rows)
                        if (rw.Cells[0].Value.ToString() == id.Value.ToString())
                        {
                            dgv.CurrentCell = rw.Cells["ФИО"];
                            break;
                        }
            }

        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentRow.Index>=0)
                {
                    int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                    new CardPerson(id, new UpdateVoidHandler(FillGrid)).Show();
                }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentCell.RowIndex >= 0)
                {
                    int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                    new CardPerson(id, new UpdateVoidHandler(FillGrid)).Show();
                }
        }

        private void btnAddPartner_Click(object sender, EventArgs e)
        {
            //new CardPartner(null, new UpdateVoidHandler(FillCard)).Show();
            new CardNewPerson(new UpdateVoidHandler(FillGrid)).Show();
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
    }

}
