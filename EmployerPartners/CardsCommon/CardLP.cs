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
    public partial class CardLP : Form
    {
        public int? _id;
        public int ObjectId;
        UpdateVoidHandler _hdl;

        public int? RubricId
        {
            get { return ComboServ.GetComboIdInt(cbRubric); }
            set { ComboServ.SetComboId(cbRubric, value); }
        }

        public int? StudyLevelId
        {
            get { return ComboServ.GetComboIdInt(cbLevel); }
            set { ComboServ.SetComboId(cbLevel, value); }
        }
        public int? ProgramTypeId
        {
            get { return ComboServ.GetComboIdInt(cbProgramType); }
            set { ComboServ.SetComboId(cbProgramType, value); }
        }
        public int? QualificationId
        {
            get { return ComboServ.GetComboIdInt(cbQulification); }
            set { ComboServ.SetComboId(cbQulification, value); }
        }

        public CardLP()
        {
            InitializeComponent();
        }
        public CardLP(int? Id, int objId, UpdateVoidHandler h)
        {
            InitializeComponent();
            _id = Id;
            ObjectId = objId;
            _hdl = h;
            this.MdiParent = Util.mainform;
            FillCard();
        }
        public void FillControls(string TableName, int? x)
        {  
            ComboServ.FillCombo(cbName, HelpClass.GetComboListByQuery(TableName), false, false);
            if (x.HasValue)
                ComboServ.SetComboId(cbName, x);
        }

        virtual public void FillCard()
        {
            btnAdd.Text = (_id.HasValue) ? "Обновить" : "Добавить";
            ComboServ.FillCombo(cbLevel, HelpClass.GetComboListByTable("dbo.StudyLevel"), false, true);
            ComboServ.FillCombo(cbRubric, HelpClass.GetComboListByQuery(@" select distinct  CONVERT(varchar(100), Rubric.Id) AS Id, Rubric.ShortName as Name
                from dbo.Rubric order by ShortName"), true, false);
        }
        virtual public void FillLP()
        {

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int? LPId = ComboServ.GetComboIdInt(cbName);
            if (!LPId.HasValue)
            { 
                MessageBox.Show("Рубрика не выбрано");
                return;
            }
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                if (!CheckExist(context, LPId))
                    return;
                
                if (!_id.HasValue)
                {
                    InsertRec(context, LPId.Value);
                }
                else if (_id.HasValue)
                {
                    UpdateRec(context, LPId.Value);
                }
                if (_hdl != null && _id.HasValue)
                    _hdl(_id);
            }
            this.Close();
        }
        virtual public bool CheckExist(EmployerPartnersEntities context, int? ObjId)
        {
            return true;
        }
        virtual public void InsertRec(EmployerPartnersEntities context, int ObjId)
        {
        }
        virtual public void UpdateRec(EmployerPartnersEntities context, int ObjId)
        {
        }

        private void cbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillProgramType();
        }
        private void FillProgramType()
        {
            int? StudyLevelId = ComboServ.GetComboIdInt(cbLevel);
            string addquery = (StudyLevelId.HasValue) ? (" where LicenseProgram.StudyLevelId =" + StudyLevelId.Value.ToString()) : "";
            ComboServ.FillCombo(cbProgramType, HelpClass.GetComboListByQuery(@" select distinct  CONVERT(varchar(100), ProgramType.Id) AS Id, ProgramType.Name 
                from dbo.ProgramType
                join dbo.LicenseProgram on LicenseProgram.ProgramTypeId = ProgramType.Id " + addquery +
                " order by ProgramType.Name "), false, true);
        }
        private void cbProgramType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillQualification();
        }
        private void FillQualification()
        {
            int? StudyLevelId = ComboServ.GetComboIdInt(cbLevel);
            int? ProgramTypeId = ComboServ.GetComboIdInt(cbProgramType);

            string addquery_sl = (StudyLevelId.HasValue) ? (" and LicenseProgram.StudyLevelId =" + StudyLevelId.Value.ToString()) : "";
            string addquery_pt = (ProgramTypeId.HasValue) ? (" and LicenseProgram.ProgramTypeId =" + ProgramTypeId.Value.ToString()) : "";

            ComboServ.FillCombo(cbQulification, HelpClass.GetComboListByQuery(@" select distinct  CONVERT(varchar(100), Qualification.Id) AS Id, Qualification.Name 
                from dbo.Qualification
                join dbo.LicenseProgram on LicenseProgram.QualificationId = Qualification.Id " + addquery_sl + addquery_pt +
                 " order by Qualification.Name "), false, true);
        }

        private void cbQulification_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLP();
        }

        private void cbName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? LpId = ComboServ.GetComboIdInt(cbName);
            if (LpId.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var lst = (from x in context.LicenseProgram
                               where x.Id == LpId.Value
                               select new
                               {
                                   x.Code,
                                   Level = x.StudyLevel.Name,
                                   Qualification = x.Qualification.Name,
                                   PType = x.ProgramType.Name,
                               }).FirstOrDefault();
                    if (lst == null)
                        return;
                    lblCode.Text = lst.Code;
                    lblProgramType.Text = lst.PType;
                    lblQualification.Text = lst.Qualification;
                    lblStudyLevel.Text = lst.Level;
                }
        }
    }
}
