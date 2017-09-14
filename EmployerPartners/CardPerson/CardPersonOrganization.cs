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
    public partial class CardPersonOrganization: Form
    {
        public int? OrganizationId
        {
            get { return ComboServ.GetComboIdInt(cbOrganization); }
            set { ComboServ.SetComboId(cbOrganization, value); }
        }
        public int? PositionId
        {
            get { return ComboServ.GetComboIdInt(cbPosition); }
            set { ComboServ.SetComboId(cbPosition, value); }
        }
        public int? SubdivisionId
        {
            get { return ComboServ.GetComboIdInt(cbSubdivision); }
            set { ComboServ.SetComboId(cbSubdivision, value); }
        }
        public string Position
        {
            get { return tbposition.Text.Trim(); }
            set { tbposition.Text = value; }
        }
        public string PositionEng
        {
            get { return tbpositionEng.Text.Trim(); }
            set { tbpositionEng.Text = value; }
        }

        public int? Position2Id
        {
            get { return ComboServ.GetComboIdInt(cbPosition2); }
            set { ComboServ.SetComboId(cbPosition2, value); }
        }
        public int? Subdivision2Id
        {
            get { return ComboServ.GetComboIdInt(cbSubdivision2); }
            set { ComboServ.SetComboId(cbSubdivision2, value); }
        }
        public string Position2
        {
            get { return tbposition2.Text.Trim(); }
            set { tbposition2.Text = value; }
        }
        public string PositionEng2
        {
            get { return tbpositionEng2.Text.Trim(); }
            set { tbpositionEng2.Text = value; }
        }
        public string Comment
        {
            get { return tbComment.Text.Trim(); }
            set { tbComment.Text = value; }
        }
        public bool NotUseInDocs
        {
            get { return chbNotUseInDocs.Checked; }
            set { chbNotUseInDocs.Checked = value; }
        }
        public int? Sorting
        {
            get { return ComboServ.GetComboIdInt(cbSorting); }
            set { ComboServ.SetComboId(cbSorting, value); }
        }

        public int? _id;
        public int PersId;
        UpdateIntHandler _hdl;

        private bool setPosition = false;

        public CardPersonOrganization()
        {
            InitializeComponent();
        }
        public CardPersonOrganization(int? Id, int persId, UpdateIntHandler h)
        {
            InitializeComponent();
            _id = Id;
            PersId = persId;
            _hdl = h;
            this.MdiParent = Util.mainform;
            FillCard();
        }
        private void FillComboOrg()
        {
            //ComboServ.FillCombo(cbOrganization, HelpClass.GetComboListByTable("dbo.Organization"), false, false);
            ComboServ.FillCombo(cbOrganization, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), Id) AS Id, 
                (CASE  WHEN [MiddleName] is NULL THEN Organization.[Name] WHEN [MiddleName] = '' THEN Organization.[Name] ELSE [MiddleName] END) AS Name
                from dbo.Organization order by Name"), true, false);
        }
        private void FillComboPosition()
        {
            //ComboServ.FillCombo(cbPosition, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), Id) AS Id, 
            //       [Name] + (CASE  WHEN ([NameEng] is NULL) OR ([NameEng] = '') THEN '' ELSE '          ( ' + [NameEng] + ' )'END) AS Name
            //       from dbo.Position order by Name"), true, false);
            ComboServ.FillCombo(cbPosition, HelpClass.GetComboListByTable("dbo.Position"), true, false);
        }
        private void FillComboPosition2()
        {
            ComboServ.FillCombo(cbPosition2, HelpClass.GetComboListByTable("dbo.Position"), true, false);
        }
        private void FillComboSubdivision()
        {
                ComboServ.FillCombo(cbSubdivision, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), Id) AS Id, Name 
                   from dbo.OrganizationSubdivision where OrganizationId = " + 
                   ((OrganizationId.HasValue) ? OrganizationId.ToString() : "null")  + " order by Name"), true, false);
        }
        private void FillComboSubdivision2()
        {
            ComboServ.FillCombo(cbSubdivision2, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), Id) AS Id, Name 
                   from dbo.OrganizationSubdivision where OrganizationId = " +
               ((OrganizationId.HasValue) ? OrganizationId.ToString() : "null") + " order by Name"), true, false);
        }
        private void FillComboSorting()
        {
            ComboServ.FillCombo(cbSorting, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), Num) AS Id, CONVERT(varchar(100), Num) AS Name 
                   from dbo.Numbers order by [Num]"), true, false);
            //from dbo.Numbers where [Num] not in (select [Sorting] from dbo.OrganizationPerson where [PartnerPersonId] = " + PersId.ToString() + ")  order by [Num]
        }

        public void FillCard()
        {
            FillComboOrg();
            FillComboPosition();
            FillComboPosition2();
            FillComboSubdivision();
            FillComboSubdivision2();
            FillComboSorting();

            btnAdd.Text = (_id.HasValue) ? "Обновить" : "Добавить";

            setPosition = true;

            if (!_id.HasValue) return;

            setPosition = false;

            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.OrganizationPerson
                           join p in context.Organization on x.OrganizationId equals p.Id
                           join pos in context.Position on x.PositionId equals pos.Id into _pos
                           from pos in _pos.DefaultIfEmpty()
                           where x.Id == _id.Value
                           && x.PartnerPersonId == PersId
                               select new 
                               {
                                   p.Id,
                                   positionId = x.PositionId,
                                   position2Id = x.Position2Id,
                                   orgsubdivisionId = x.OrganizationSubdivisionId,
                                   orgsubdivision2Id = x.OrganizationSubdivision2Id,
                                   p.Name,
                                   NameHandBook = pos.Name,
                                   x.Position,
                                   NameEngHandBook = pos.NameEng,
                                   x.PositionEng,
                                   x,Position2,
                                   x.PositionEng2,
                                   x.Comment,
                                   notuseindocs = x.NotUseInDocs,
                                   sorting = x.Sorting,

                               }).FirstOrDefault();
                if (lst == null)
                    return;

                tbposition.Text = lst.Position;
                tbpositionEng.Text = lst.PositionEng;
                tbposition2.Text = lst.Position2;
                tbpositionEng2.Text = lst.PositionEng2;
                tbComment.Text = lst.Comment;
                if (lst.notuseindocs.HasValue)
                {
                    NotUseInDocs = (bool)lst.notuseindocs;
                }

                ComboServ.SetComboId(cbOrganization, lst.Id);
                ComboServ.SetComboId(cbPosition, lst.positionId);
                ComboServ.SetComboId(cbSubdivision, lst.orgsubdivisionId);
                ComboServ.SetComboId(cbPosition2, lst.position2Id);
                ComboServ.SetComboId(cbSubdivision2, lst.orgsubdivision2Id);
                ComboServ.SetComboId(cbSorting, lst.sorting);

                setPosition = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int? OrgId = ComboServ.GetComboIdInt(cbOrganization);
            if (!OrgId.HasValue)
            {
                MessageBox.Show("Не выбрана организация", "Напоминание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbOrganization.DroppedDown = true;
                return;
            }
            int? PositionId = ComboServ.GetComboIdInt(cbPosition);
            if (!PositionId.HasValue)
            {
                //MessageBox.Show("Не выбрана должность по справочнику", "Напоминание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //cbPosition.DroppedDown = true;
                //return;
                if (MessageBox.Show("Не выбрана должность по справочнику\r\n" + "Продолжить тем не менее ?", "Запрос на подтверждение",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Cancel)
                {
                    cbPosition.DroppedDown = true;
                    return;
                }
            }
            if (!SubdivisionId.HasValue)
            {
                if (MessageBox.Show("Не выбрано подразделение организации.\r\n" + "Продолжить тем не менее?", "Запрос на подтверждение",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                {
                    cbSubdivision.DroppedDown = true;
                    return;
                }
            }

            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var lst = (from x in context.OrganizationPerson
                           where x.PartnerPersonId == PersId
                           && x.Id != _id 
                           && x.OrganizationId == OrgId.Value
                           select new
                           {
                               x.Id
                           }).ToList().Count();
                if (lst > 0)
                {
                    MessageBox.Show("Такая организация уже было добавлена", "Напоминание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (!_id.HasValue)
                {
                    try
                    {
                        OrganizationPerson org = new OrganizationPerson()
                        {
                            OrganizationId = OrgId.Value,
                            PartnerPersonId = PersId,
                            PositionId = PositionId,
                            OrganizationSubdivisionId = SubdivisionId,
                            Position = tbposition.Text.Trim(),
                            PositionEng = tbpositionEng.Text.Trim(),

                            Position2Id = Position2Id,
                            OrganizationSubdivision2Id = Subdivision2Id,
                            Position2 = tbposition2.Text.Trim(),
                            PositionEng2 = tbpositionEng2.Text.Trim(),

                            Comment = Comment,

                            NotUseInDocs = NotUseInDocs,
                            Sorting = Sorting,
                        };
                        context.OrganizationPerson.Add(org);
                        context.SaveChanges();
                        _id = org.Id;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось добавить запись...\r\n" + ex.Message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else if (_id.HasValue)
                {
                    try
                    {
                        OrganizationPerson org = context.OrganizationPerson.Where(x => x.Id == _id.Value).First();
                        org.OrganizationId = OrgId.Value;
                        org.PositionId = PositionId;
                        org.OrganizationSubdivisionId = SubdivisionId;
                        org.Position = Position;
                        org.PositionEng = PositionEng;

                        org.Position2Id = Position2Id;
                        org.OrganizationSubdivision2Id = Subdivision2Id;
                        org.Position2 = Position2;
                        org.PositionEng2 = PositionEng2;

                        org.Comment = tbComment.Text.Trim();
                        org.NotUseInDocs = NotUseInDocs;
                        org.Sorting = Sorting;
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Не удалось изменить запись...\r\n" + ex.Message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                if (_hdl != null && _id.HasValue)
                    _hdl(_id);
            }
            this.Close();
        }

        private void btnPosEdit_Click(object sender, EventArgs e)
        {
            tbposition.Enabled = true;
        }
        private void btnPosEngEdit_Click(object sender, EventArgs e)
        {
            tbpositionEng.Enabled = true;
        }

        private void cbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!setPosition)
                return;
            try
            {
                int? PositionId = ComboServ.GetComboIdInt(cbPosition);
                if (!PositionId.HasValue)
                    return;

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.Position.Where(x => x.Id == (int)PositionId).First();

                    //if (String.IsNullOrEmpty(Position))             //(tbposition.Enabled)
                    //{
                        tbposition.Text = lst.Name;
                        tbpositionEng.Text = (!String.IsNullOrEmpty(lst.NameEng)) ? lst.NameEng : "";
                    //}
                    //if (String.IsNullOrEmpty(PositionEng))          //(tbpositionEng.Enabled)
                    //    tbpositionEng.Text = (!String.IsNullOrEmpty(lst.NameEng)) ? lst.NameEng : "";
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnRefreshOrgList_Click(object sender, EventArgs e)
        {
            FillComboOrg();
        }

        private void btnRefreshPosList_Click(object sender, EventArgs e)
        {
            FillComboPosition();
        }
        private void OrgListSetToFound()
        {
            OrgListSetToFound(null);
        }
        private void OrgListSetToFound(int? id)
        {
            OrganizationId = id;
        }
        private void PositionListSetToFound(int? id)
        {
            FillComboPosition();
            PositionId = id;
        }
        private void Position2ListSetToFound(int? id)
        {
            FillComboPosition2();
            Position2Id = id;
        }
        private void btnOrgHandBook_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("OrgListToFind"))
                Utilities.FormClose("OrgListToFind");
                
            new OrgListToFind(new UpdateIntHandler(OrgListSetToFound)).Show();
        }

        private void btnPosHandBook_Click(object sender, EventArgs e)
        {
            //if (Utilities.FormIsOpened("Positions"))
            //    return;
            //new Positions().Show();
            if (Utilities.FormIsOpened("PositionListToFind"))
                Utilities.FormClose("PositionListToFind");

            new PositionListToFind(new UpdateIntHandler(PositionListSetToFound)).Show();
        }

        private void cbOrganization_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillComboSubdivision();
            FillComboSubdivision2();
        }

        private void btnRefreshSubdivList_Click(object sender, EventArgs e)
        {
            FillComboSubdivision();
        }

        private void btnPosEdit2_Click(object sender, EventArgs e)
        {
            tbposition2.Enabled = true;
        }

        private void btnPosEngEdit2_Click(object sender, EventArgs e)
        {
            tbpositionEng2.Enabled = true;
        }

        private void cbPosition2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!setPosition)
                return;
            try
            {
                int? PositionId = ComboServ.GetComboIdInt(cbPosition);
                if (!PositionId.HasValue)
                    return;

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.Position.Where(x => x.Id == (int)PositionId).First();

                    //if (String.IsNullOrEmpty(Position))             //(tbposition.Enabled)
                    //{
                    tbposition2.Text = lst.Name;
                    tbpositionEng2.Text = (!String.IsNullOrEmpty(lst.NameEng)) ? lst.NameEng : "";
                    //}
                    //if (String.IsNullOrEmpty(PositionEng))          //(tbpositionEng.Enabled)
                    //    tbpositionEng.Text = (!String.IsNullOrEmpty(lst.NameEng)) ? lst.NameEng : "";
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnRefreshPosList2_Click(object sender, EventArgs e)
        {
            FillComboPosition2();
        }

        private void btnRefreshSubdivList2_Click(object sender, EventArgs e)
        {
            FillComboSubdivision2();
        }

        private void btnPosHandBook2_Click(object sender, EventArgs e)
        {
            
            if (Utilities.FormIsOpened("PositionListToFind"))
                Utilities.FormClose("PositionListToFind");

            new PositionListToFind(new UpdateIntHandler(Position2ListSetToFound)).Show();
        }
    }
}
