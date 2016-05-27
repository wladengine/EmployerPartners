using FastMember;
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
    public partial class ListOrganizations : Form
    {
        public int? OwnershipTypeId
        {
            get { return ComboServ.GetComboIdInt(cbOwnership); }
            set { ComboServ.SetComboId(cbOwnership, value); }
        }
        public int? ActivityGoalId
        {
            get { return ComboServ.GetComboIdInt(cbActivityGoal); }
            set { ComboServ.SetComboId(cbActivityGoal, value); }
        }
        public int? NationalAffiliationId
        {
            get { return ComboServ.GetComboIdInt(cbNationalityAffilation); }
            set { ComboServ.SetComboId(cbNationalityAffilation, value); }
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

        public ListOrganizations()
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            this.Text = "Список организаций";
            FillCard();
            FillGrid();
        }

        private void FillCard()
        {
            ComboServ.FillCombo(cbOwnership, HelpClass.GetComboListByTable("dbo.OwnershipType"), false, true);
            ComboServ.FillCombo(cbActivityGoal, HelpClass.GetComboListByTable("dbo.ActivityGoal"), false, true);
            ComboServ.FillCombo(cbActivityArea, HelpClass.GetComboListByTable("dbo.ActivityArea"), false, true);
            ComboServ.FillCombo(cbNationalityAffilation, HelpClass.GetComboListByTable("dbo.NationalAffiliation"), false, true);
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
                var lst = (from org in context.Organization
                           where 
                           (OwnershipTypeId.HasValue ? org.OwnershipTypeId == OwnershipTypeId : true) &&
                           (ActivityGoalId.HasValue ? org.ActivityGoalId == ActivityGoalId : true) &&
                           (NationalAffiliationId.HasValue ? org.NationalAffiliationId == NationalAffiliationId : true) &&
                           (ActivityAreaId.HasValue ? org.ActivityAreaId == ActivityAreaId : true) &&
                           (CountryId.HasValue ? org.CountryId == CountryId : true) &&
                           (RegionId.HasValue ? org.RegionId == RegionId : true) 
                           select new
                           {
                               org.Id,
                               Название = org.Name,
                               Среднее_название =  org.MiddleName,
                               Короткое_название = org.ShortName,
                               Источник = org.Source,
                               ОКВЭД = org.Okved,
                               Форма_собственности = org.OwnershipType.Name, 
                               Цель_деятельности = org.ActivityGoal.Name,
                               Национальная_принадлежность = org.NationalAffiliation.Name,

                               ИНН = org.INN,
                               org.Email,
                               Телефон = org.Phone,
                               Мобильный_телефон = org.Mobiles,
                               Факс = org.Fax,
                               Web_сайт = org.WebSite,
                               Страна = org.Country.Name,
                               Регион = org.Region.Name,
                               Город = org.City,
                               Улица = org.Street,
                               Дом = org.House,
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
                            dgv.CurrentCell = rw.Cells["Название"];
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
                    new CardOrganization(id, new UpdateVoidHandler(FillGrid)).Show();
                }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentCell.RowIndex >= 0)
                {
                    int id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                    new CardOrganization(id, new UpdateVoidHandler(FillGrid)).Show();
                }
        }

        private void btnAddPartner_Click(object sender, EventArgs e)
        {
            //new CardPartner(null, new UpdateVoidHandler(FillCard)).Show();
            new CardNewOrganization(new UpdateVoidHandler(FillGrid)).Show();
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
