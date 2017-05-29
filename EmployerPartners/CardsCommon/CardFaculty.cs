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
    public partial class CardFaculty : Form
    {
        
        public int? _id;
        public int ObjectId;
        UpdateVoidHandler _hdl;

        public int? RubricId
        {
            get { return ComboServ.GetComboIdInt(cbRubric); }
            set { ComboServ.SetComboId(cbRubric, value); }
        }
        public CardFaculty()
        {
            InitializeComponent();
        }
        public CardFaculty(int? Id, int objId, UpdateVoidHandler h)
        {
            InitializeComponent();
            _id = Id;
            ObjectId = objId;
            _hdl = h;
            this.MdiParent = Util.mainform;
            FillCard();
        }
        public void FillControls(string TableName, int? FacultyId, int? RubricId)
        {
            btnAdd.Text = (_id.HasValue) ? "Обновить" : "Добавить";
            ComboServ.FillCombo(cbRubric, HelpClass.GetComboListByQuery(@" select distinct  CONVERT(varchar(100), Rubric.Id) AS Id, Rubric.Name as Name
                from dbo.Rubric order by Name"), true, false);
            if (RubricId.HasValue)
                ComboServ.SetComboId(cbRubric, RubricId);
                
            ComboServ.FillCombo(cbName, HelpClass.GetComboListByTable(TableName), false, false);
            if (FacultyId.HasValue)
                ComboServ.SetComboId(cbName, FacultyId);
        }

        virtual public void FillCard()
        {
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            int? RubricId = ComboServ.GetComboIdInt(cbRubric);
            int? FacultyId = ComboServ.GetComboIdInt(cbName);
            if (!FacultyId.HasValue)
            { 
                MessageBox.Show("Не выбрано направление", "Инфо");
                return;
            }
            if (!RubricId.HasValue)
            {
                MessageBox.Show("Не выбрана рубрика", "Инфо");
                return;
            }
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                if (!CheckExist(context, FacultyId, RubricId))
                    return;
                
                if (!_id.HasValue)
                {
                    InsertRec(context, FacultyId.Value, RubricId);
                }
                else if (_id.HasValue)
                {
                    UpdateRec(context, FacultyId.Value, RubricId);
                }
                if (_hdl != null && _id.HasValue)
                    _hdl(_id);
            }
            this.Close();
        }
        virtual public bool CheckExist(EmployerPartnersEntities context, int? ObjId, int? Obj2Id)
        {
            return true;
        }
        virtual public bool CheckExist(EmployerPartnersEntities context, int? ObjId)
        {
            return true;
        }
        virtual public void InsertRec(EmployerPartnersEntities context, int ObjId, int? RubricId)
        {
        }
        virtual public void UpdateRec(EmployerPartnersEntities context, int ObjId, int? RubricId)
        {
        }
    }
}
