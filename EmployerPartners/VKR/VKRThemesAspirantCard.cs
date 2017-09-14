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
    public partial class VKRThemesAspirantCard : Form
    {
        int? _Id;
        string NR_NPRTabnum, Review_NPR_Tabnum;
        int? NR_PPId, Review_PPId;
        UpdateIntHandler _hndl;

        public VKRThemesAspirantCard(int? id, UpdateIntHandler hdl)
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            _Id = id;
            _hndl = hdl;
            FillForm();
            FillCard();    
        }
        public void FillForm()
        {
            ComboServ.FillCombo(cbNR_PP, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), pp.Id) AS Id, 
                (pp.Name + CASE WHEN d.Acronym is NULL THEN '' ELSE ',   ' + d.Acronym END  + CASE WHEN pop.OrgName is NULL THEN '' ELSE ',   ' + pop.OrgName END) as Name
                from dbo.PartnerPerson pp left outer join dbo.Degree d on pp.DegreeId = d.Id left outer join dbo.ECD_OrganizationPersonOrgName pop on pp.Id = pop.PartnerPersonId order by Name"), true, false);
            ComboServ.FillCombo(cbReview_PP, HelpClass.GetComboListByQuery(@" select  CONVERT(varchar(100), pp.Id) AS Id, 
                (pp.Name + CASE WHEN d.Acronym is NULL THEN '' ELSE ',   ' + d.Acronym END  + CASE WHEN pop.OrgName is NULL THEN '' ELSE ',   ' + pop.OrgName END) as Name
                from dbo.PartnerPerson pp left outer join dbo.Degree d on pp.DegreeId = d.Id left outer join dbo.ECD_OrganizationPersonOrgName pop on pp.Id = pop.PartnerPersonId order by Name"), true, false);
        }
        public void FillCard()
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                if (_Id.HasValue)
                {

                    var Order = context.VKR_ThemesAspirantOrder.Where(x => x.Id == _Id).FirstOrDefault();
                    var Stud = context.AspirantData.Where(x => x.StudDataId == Order.StudDataId).FirstOrDefault();

                    tbAccount.Text = Order.Account;
                    tbStudentFIO.Text = Order.FIO.Trim();
                    this.Text = "Карточка ВКР: " + Order.FIO.Trim();
                    
                    tbOPCrypt.Text = Order.ObrazProgramCrypt;
                    tbOP.Text = context.ObrazProgram.Where(x => x.Id == Order.ObrazProgramId).Select(x => x.Name).First();
                    tbStudyLevel.Text = Order.DegreeName;

                    tbVKRName.Text = Order.VKRName;
                    tbVKRNameEng.Text = Order.VKRNameEng;

                }
                
                if (_Id.HasValue)
                    FillNR(context);
                if (_Id.HasValue)
                    FillReview(context);
            }
        }
        #region NR
        public void FillNR(EmployerPartnersEntities context)
        {
            var NPR = context.VKR_ThemesAspirant_NR_NPR.Where(x => x.VKR_ThemesAspirantOrderId == _Id).ToList();
            var PP = context.VKR_ThemesAspirant_NR_PartnerPerson.Where(x => x.VKR_ThemesAspirantOrderId == _Id).ToList();
            var lst = (from x in NPR
                       select new
                       {
                           Id = x.Id,
                           ФИО = ((x.Surname + " ") ?? "") + ((x.Name + " ") ?? "") + ((x.SecondName) ?? ""),
                           isNPR = true,
                           Должность = x.Position,
                           Степень = x.Degree,
                       }).ToList().Union(from x in PP
                                         select new
                                         {
                                             Id = x.Id,
                                             ФИО = ((x.Surname + " ") ?? "") + ((x.Name + " ") ?? "") + ((x.SecondName) ?? ""),
                                             isNPR = false,
                                             Должность = x.OrgPosition,
                                             Степень = x.Degree,
                                         }).ToList();
            dgvNR.DataSource = lst;
            List<string> cols = new List<string>() { "Id", "isNPR" };
            foreach (string s in cols)
                if (dgvNR.Columns.Contains(s))
                    dgvNR.Columns[s].Visible = false;

            dgvNR.Columns["ColumnNRRemove"].Width = 70;
        }
        private void btn_NR_NPR_find_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("NPRListToFind"))
                Utilities.FormClose("NPRListToFind");
            new NPRListToFind(new UpdateStringHandler(NR_NPR_SetToFound)).Show();
        }
        private void NR_NPR_SetToFound(int? id, string Tabnum)
        {
            try
            {
                NR_NPRTabnum = Tabnum;
                if (String.IsNullOrEmpty(NR_NPRTabnum))
                {
                    tbNR_NPR_FIO.Text =
                    tbNR_NPR_Degree.Text =
                    tbNR_NPR_Rank.Text =
                    tbNR_NPR_Position.Text =
                    tbNR_NPR_Faculty.Text =
                    tbNR_NPR_Chair.Text =
                    tbNR_NPR_Account.Text = "";
                    return;
                }

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.SAP_NPR.Where(x => x.Tabnum == NR_NPRTabnum).First();

                    tbNR_NPR_FIO.Text = ((lst.Lastname + " ") ?? "") + ((lst.Name + " ") ?? "") + ((lst.Surname + " ") ?? "");
                    tbNR_NPR_Degree.Text= ((!String.IsNullOrEmpty(lst.Degree)) ? lst.Degree : "");
                    tbNR_NPR_Rank.Text= ((!String.IsNullOrEmpty(lst.Titl2)) ? lst.Titl2 : "");
                    tbNR_NPR_Position.Text= ((!String.IsNullOrEmpty(lst.Position)) ? lst.Position : "");
                    tbNR_NPR_Faculty.Text= ((!String.IsNullOrEmpty(lst.Faculty)) ? lst.Faculty : "");
                    tbNR_NPR_Chair.Text= ((!String.IsNullOrEmpty(lst.FullName)) ? lst.FullName : "");
                    tbNR_NPR_Account.Text= ((!String.IsNullOrEmpty(lst.UsridAd)) ? lst.UsridAd : "");
                }
            }
            catch (Exception)
            {
            }
        }
        private void btn_NR_PP_find_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("PersonListToFind"))
                Utilities.FormClose("PersonListToFind");

            new PersonListToFind(new UpdateIntHandler(NR_PP_SetToFound)).Show();
        }
        private void NR_PP_SetToFound(int? id)
        {
            NR_PPId = id;
            ComboServ.SetComboId(cbNR_PP, id);
        }
        private void cbNR_PP_SelectedIndexChanged(object sender, EventArgs e)
        {
            NR_PPId = ComboServ.GetComboIdInt(cbNR_PP);
            try
            {
                if (!NR_PPId.HasValue)
                {
                    tb_NR_PP_FIO.Text =  
                    tb_NR_PP_Degree.Text = 
                    tb_NR_PP_Rank.Text =  
                    tb_NR_PP_OrgPos.Text =  
                    tb_NR_PP_Account.Text = "";
                    return;
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.PartnerPersonOrgPosition.Where(x => x.Id == (int)NR_PPId).First();

                    tb_NR_PP_FIO.Text = (!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "";
                    tb_NR_PP_Degree.Text = (!String.IsNullOrEmpty(lst.DegreeName)) ? lst.DegreeName : "";
                    tb_NR_PP_Rank.Text = (!String.IsNullOrEmpty(lst.RankName)) ? lst.RankName : "";
                    tb_NR_PP_OrgPos.Text = (!String.IsNullOrEmpty(lst.OrgPosition)) ? lst.OrgPosition : "";
                    tb_NR_PP_Account.Text = (!String.IsNullOrEmpty(lst.Account)) ? lst.Account : "";
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region Review
        public void FillReview(EmployerPartnersEntities context)
        {
            var NPR = context.VKR_ThemesAspirant_Review_NPR.Where(x => x.VKR_ThemesAspirantOrderId == _Id).ToList();
            var PP = context.VKR_ThemesAspirant_Review_PartnerPerson.Where(x => x.VKR_ThemesAspirantOrderId == _Id).ToList();
            var lst = (from x in NPR
                       select new
                       {
                           Id = x.Id,
                           ФИО = ((x.Surname + " ") ?? "") + ((x.Name + " ") ?? "") + ((x.SecondName) ?? ""),
                           isNPR = true,
                           Должность = x.Position,
                           Степень = x.Degree,
                       }).ToList().Union(from x in PP
                                         select new
                                         {
                                             Id = x.Id,
                                             ФИО = ((x.Surname + " ") ?? "") + ((x.Name + " ") ?? "") + ((x.SecondName) ?? ""),
                                             isNPR = false,
                                             Должность = x.OrgPosition,
                                             Степень = x.Degree,
                                         }).ToList();
            dgvReview.DataSource = lst;
            List<string> cols = new List<string>() { "Id", "isNPR" };
            foreach (string s in cols)
                if (dgvReview.Columns.Contains(s))
                    dgvReview.Columns[s].Visible = false;

            dgvReview.Columns["ColumnReviewRemove"].Width = 70;
        }
        private void btn_Review_NPR_find_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("NPRListToFind"))
                Utilities.FormClose("NPRListToFind");
            new NPRListToFind(new UpdateStringHandler(Review_NPR_SetToFound)).Show();
        }
        private void Review_NPR_SetToFound(int? id, string Tabnum)
        {
            try
            {
                Review_NPR_Tabnum = Tabnum;
                if (String.IsNullOrEmpty(Review_NPR_Tabnum))
                {
                    tbReview_NPR_FIO.Text =
                    tbReview_NPR_Degree.Text =
                    tbReview_NPR_Rank.Text =
                    tbReview_NPR_Position.Text =
                    tbReview_NPR_Faculty.Text =
                    tbReview_NPR_Chair.Text =
                    tbReview_NPR_Account.Text = "";
                    return;
                }

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.SAP_NPR.Where(x => x.Tabnum == Review_NPR_Tabnum).First();

                    tbReview_NPR_FIO.Text = ((lst.Lastname + " ") ?? "") + ((lst.Name + " ") ?? "") + ((lst.Surname + " ") ?? "");
                    tbReview_NPR_Degree.Text = ((!String.IsNullOrEmpty(lst.Degree)) ? lst.Degree : "");
                    tbReview_NPR_Rank.Text = ((!String.IsNullOrEmpty(lst.Titl2)) ? lst.Titl2 : "");
                    tbReview_NPR_Position.Text = ((!String.IsNullOrEmpty(lst.Position)) ? lst.Position : "");
                    tbReview_NPR_Faculty.Text = ((!String.IsNullOrEmpty(lst.Faculty)) ? lst.Faculty : "");
                    tbReview_NPR_Chair.Text = ((!String.IsNullOrEmpty(lst.FullName)) ? lst.FullName : "");
                    tbReview_NPR_Account.Text = ((!String.IsNullOrEmpty(lst.UsridAd)) ? lst.UsridAd : "");
                }
            }
            catch (Exception)
            {
            }
        }
        private void btn_Review_PP_find_Click(object sender, EventArgs e)
        {
            if (Utilities.FormIsOpened("PersonListToFind"))
                Utilities.FormClose("PersonListToFind");

            new PersonListToFind(new UpdateIntHandler(Review_PP_SetToFound)).Show();
        }
        private void Review_PP_SetToFound(int? id)
        {
            Review_PPId = id;
            ComboServ.SetComboId(cbReview_PP, id);
        }
        private void cbReview_PP_SelectedIndexChanged(object sender, EventArgs e)
        {
            Review_PPId = ComboServ.GetComboIdInt(cbReview_PP);
            try
            {
                if (!Review_PPId.HasValue)
                {
                    tb_Review_PP_FIO.Text =
                    tb_Review_PP_Degree.Text =
                    tb_Review_PP_Rank.Text =
                    tb_Review_PP_OrgPos.Text =
                    tb_Review_PP_Account.Text = "";
                    return;
                }
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = context.PartnerPersonOrgPosition.Where(x => x.Id == (int)Review_PPId).First();

                    tb_Review_PP_FIO.Text = (!String.IsNullOrEmpty(lst.Name)) ? lst.Name : "";
                    tb_Review_PP_Degree.Text = (!String.IsNullOrEmpty(lst.DegreeName)) ? lst.DegreeName : "";
                    tb_Review_PP_Rank.Text = (!String.IsNullOrEmpty(lst.RankName)) ? lst.RankName : "";
                    tb_Review_PP_OrgPos.Text = (!String.IsNullOrEmpty(lst.OrgPosition)) ? lst.OrgPosition : "";
                    tb_Review_PP_Account.Text = (!String.IsNullOrEmpty(lst.Account)) ? lst.Account : "";
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region save
        private void btn_NR_NPR_Add_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(NR_NPRTabnum) && _Id.HasValue)
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    if (context.VKR_ThemesAspirant_NR_NPR.Where(x=>x.Tabnum == NR_NPRTabnum && x.VKR_ThemesAspirantOrderId == _Id).Count() >0)
                    {
                        MessageBox.Show("Такой научный руководитель уже добавлен", "Ошибка");
                        return;
                    }
                    var db_NPR = context.SAP_NPR.Where(x => x.Tabnum == NR_NPRTabnum).FirstOrDefault();
                    if (db_NPR != null)
                    {
                        VKR_ThemesAspirant_NR_NPR npr = new VKR_ThemesAspirant_NR_NPR();

                        npr.VKR_ThemesAspirantOrderId = _Id.Value;
                        npr.Tabnum = db_NPR.Tabnum;
                        npr.Persnum = db_NPR.Persnum;
                        npr.Surname = db_NPR.Lastname;
                        npr.Name = db_NPR.Name;
                        npr.SecondName = db_NPR.Surname;
                        npr.Account = db_NPR.UsridAd;
                        npr.Degree = db_NPR.Degree;
                        npr.Position = db_NPR.Position;
                        npr.Rank = db_NPR.Titl2;
                        npr.Chair = db_NPR.FullName;
                        npr.Faculty = db_NPR.Faculty;

                        context.VKR_ThemesAspirant_NR_NPR.Add(npr);
                        context.SaveChanges();

                        NR_NPR_SetToFound(null, "");
                        FillNR(context);
                    }
                    else
                    {
                        MessageBox.Show("Научный руководитель не выбран или карточка не существует");
                    }
                }
            }
            else
            {
                MessageBox.Show("Научный руководитель не выбран или карточка не существует");
            }
        }
        private void btn_Review_NPR_Add_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(Review_NPR_Tabnum) && _Id.HasValue)
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    if (context.VKR_ThemesAspirant_Review_NPR.Where(x => x.Tabnum == Review_NPR_Tabnum && x.VKR_ThemesAspirantOrderId == _Id).Count() > 0)
                    {
                        MessageBox.Show("Такой рецензент уже добавлен", "Ошибка");
                        return;
                    }
                    var db_NPR = context.SAP_NPR.Where(x => x.Tabnum == Review_NPR_Tabnum).FirstOrDefault();
                    if (db_NPR != null)
                    {
                        VKR_ThemesAspirant_Review_NPR npr = new VKR_ThemesAspirant_Review_NPR();

                        npr.VKR_ThemesAspirantOrderId = _Id.Value;
                        npr.Tabnum = db_NPR.Tabnum;
                        npr.Persnum = db_NPR.Persnum;
                        npr.Surname = db_NPR.Lastname;
                        npr.Name = db_NPR.Name;
                        npr.SecondName = db_NPR.Surname;
                        npr.Account = db_NPR.UsridAd;
                        npr.Degree = db_NPR.Degree;
                        npr.Position = db_NPR.Position;
                        npr.Rank = db_NPR.Titl2;
                        npr.Chair = db_NPR.FullName;
                        npr.Faculty = db_NPR.Faculty;

                        context.VKR_ThemesAspirant_Review_NPR.Add(npr);
                        context.SaveChanges();

                        Review_NPR_SetToFound(null, null);
                        FillReview(context);
                    }
                    else
                    {
                        MessageBox.Show("Рецензент не выбран или карточка не существует");
                    }
                }
            }
            else
            {
                MessageBox.Show("Рецензент или карточка не существует");
            }
        }
        private void btn_NR_PP_Add_Click(object sender, EventArgs e)
        {
            if (!NR_PPId.HasValue || !_Id.HasValue)
            {
                MessageBox.Show("Научный руководитель не выбран или карточка не существует");
                return;
            }
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                if (context.VKR_ThemesAspirant_NR_PartnerPerson.Where(x=>x.VKR_ThemesAspirantOrderId == _Id && x.PartnerPersonId == NR_PPId).Count()>0)
                {
                    MessageBox.Show("Такой научный руководитель уже добавлен", "Ошибка");
                    return;
                }
                var db_pp = context.PartnerPersonOrgPosition.Where(x => x.Id == NR_PPId.Value).First();
                if (db_pp!=null)
                {
                    VKR_ThemesAspirant_NR_PartnerPerson pp = new VKR_ThemesAspirant_NR_PartnerPerson();
                    pp.VKR_ThemesAspirantOrderId = _Id.Value;
                    pp.PartnerPersonId = NR_PPId;

                    pp.Surname = db_pp.LastName;
                    pp.Name = db_pp.FirstName;
                    pp.SecondName = db_pp.SecondName;
                    pp.Degree = db_pp.DegreeName;
                    pp.Rank = db_pp.RankName;
                    pp.Account = db_pp.Account;
                    pp.Subdivision = db_pp.LastName;
                    pp.OrgPosition = db_pp.OrgPosition;

                    context.VKR_ThemesAspirant_NR_PartnerPerson.Add(pp);
                    context.SaveChanges();

                    NR_PP_SetToFound(null);
                    FillNR(context);
                }
                else
                    MessageBox.Show("Научный руководитель не выбран или карточка не существует");
            }
        }
        private void btn_Review_PP_Add_Click(object sender, EventArgs e)
        {
            if (!Review_PPId.HasValue || !_Id.HasValue)
            {
                MessageBox.Show("Научный рецензент не выбран или карточка не существует");
                return;
            }
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                if (context.VKR_ThemesAspirant_Review_PartnerPerson.Where(x => x.VKR_ThemesAspirantOrderId == _Id && x.PartnerPersonId == Review_PPId).Count() > 0)
                {
                    MessageBox.Show("Такой рецензент уже добавлен", "Ошибка");
                    return;
                }
                var db_pp = context.PartnerPersonOrgPosition.Where(x => x.Id == Review_PPId.Value).First();
                if (db_pp != null)
                {
                    VKR_ThemesAspirant_Review_PartnerPerson pp = new VKR_ThemesAspirant_Review_PartnerPerson();
                    pp.VKR_ThemesAspirantOrderId = _Id.Value;
                    pp.PartnerPersonId = Review_PPId;

                    pp.Surname = db_pp.LastName;
                    pp.Name = db_pp.FirstName;
                    pp.SecondName = db_pp.SecondName;
                    pp.Degree = db_pp.DegreeName;
                    pp.Rank = db_pp.RankName;
                    pp.Account = db_pp.Account;
                    pp.Subdivision = db_pp.LastName;
                    pp.OrgPosition = db_pp.OrgPosition;

                    context.VKR_ThemesAspirant_Review_PartnerPerson.Add(pp);
                    context.SaveChanges();

                    Review_PP_SetToFound(null);
                    FillReview(context);
                }
                else
                    MessageBox.Show("Научный рецензент не выбран или карточка не существует");
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                VKR_ThemesAspirantOrder vkr = context.VKR_ThemesAspirantOrder.Where(x => x.Id == _Id).FirstOrDefault();
                if (vkr== null)
                {

                }
                else
                {
                    vkr.VKRName = tbVKRName.Text.Trim();
                    vkr.VKRNameEng = tbVKRNameEng.Text.Trim();
                    vkr.DateUpdated = DateTime.Now;
                    vkr.AuthorUpdated = Util.GetUserName();
                    context.SaveChanges();
                    if (_hndl != null)
                        _hndl(_Id);
                }
            }
            
        }
        #endregion

        private void btn_NR_PP_Update_Click(object sender, EventArgs e)
        {
            FillForm();
            ComboServ.SetComboId(cbNR_PP, NR_PPId);
            ComboServ.SetComboId(cbReview_PP, Review_PPId);
        }
        private void btn_Review_PP_Update_Click(object sender, EventArgs e)
        {
            FillForm();
            ComboServ.SetComboId(cbNR_PP, NR_PPId);
            ComboServ.SetComboId(cbReview_PP, Review_PPId);
        }
        private void btn_NR_PP_Clear_Click(object sender, EventArgs e)
        {
            NR_PP_SetToFound(null);
        }
        private void btn_Review_PP_Clear_Click(object sender, EventArgs e)
        {
            Review_PP_SetToFound(null);
        }

        private void dgvNR_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNR.CurrentCell ==null)
                return;
            if (dgvNR.CurrentRow.Index < 0)
                return;

            if (dgvNR.CurrentCell.ColumnIndex == dgvNR.Columns["ColumnNRRemove"].Index)
            {
                string FIO = (string)dgvNR.CurrentRow.Cells["ФИО"].Value;
                if (MessageBox.Show(String.Format("Удалить научного руководителя <{0}>?", FIO), "", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                    return;

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    int Id = (int)dgvNR.CurrentRow.Cells["Id"].Value;
                    bool isNPR = (bool)dgvNR.CurrentRow.Cells["isNPR"].Value;
                    if (isNPR)
                    {
                        try
                        {
                            context.VKR_ThemesAspirant_NR_NPR.Remove(context.VKR_ThemesAspirant_NR_NPR.Where(x => x.Id == Id && x.VKR_ThemesAspirantOrderId == _Id).First());
                            context.SaveChanges();
                            FillNR(context);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при удалении научного руководителя");
                        }
                    }
                    else
                    {
                        try
                        {
                            context.VKR_ThemesAspirant_NR_PartnerPerson.Remove(context.VKR_ThemesAspirant_NR_PartnerPerson.Where(x => x.Id == Id && x.VKR_ThemesAspirantOrderId == _Id).First());
                            context.SaveChanges();
                            FillNR(context);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при удалении научного руководителя");
                        }
                    }
                }
            }
        }

        private void dgvReview_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvReview.CurrentCell == null)
                return;
            if (dgvReview.CurrentRow.Index < 0)
                return;

            if (dgvReview.CurrentCell.ColumnIndex == dgvReview.Columns["ColumnReviewRemove"].Index)
            {
                string FIO = (string)dgvReview.CurrentRow.Cells["ФИО"].Value;
                if (MessageBox.Show(String.Format("Удалить рецензента <{0}>?", FIO), "", MessageBoxButtons.YesNo) != System.Windows.Forms.DialogResult.Yes)
                    return;

                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    int Id = (int)dgvReview.CurrentRow.Cells["Id"].Value;
                    bool isNPR = (bool)dgvReview.CurrentRow.Cells["isNPR"].Value;
                    if (isNPR)
                    {
                        try
                        {
                            context.VKR_ThemesAspirant_Review_NPR.Remove(context.VKR_ThemesAspirant_Review_NPR.Where(x => x.Id == Id && x.VKR_ThemesAspirantOrderId == _Id).First());
                            context.SaveChanges();
                            FillReview(context);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при удалении рецензента");
                        }
                    }
                    else
                    {
                        try
                        {
                            context.VKR_ThemesAspirant_Review_PartnerPerson.Remove(context.VKR_ThemesAspirant_Review_PartnerPerson.Where(x => x.Id == Id && x.VKR_ThemesAspirantOrderId == _Id).First());
                            context.SaveChanges();
                            FillReview(context);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при удалении рецензента");
                        }
                    }
                }
            }
        }

        private void btnChangeReviewOrderNum_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Кнопка пока не работает");
        }

        private void btnSaveReviewOrderNum_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Кнопка пока не работает");
        }
    }
}
