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
    public partial class CardDictionary : Form
    {
        public CardDictionary()
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            FillCard(null);
            SetAccessRight();
        }
        private void SetAccessRight()
        {
            if (Util.IsOrgPersonWrite())
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
        }
        public void FillCard(int? id)
        {
            FillCard(dgv, id);
        }
        virtual public void FillCard(DataGridView dgv, int? Id)
        {
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            new CardDictionaryItem(null, new UpdateIntHandler(FillCard), new UpdateStringHandler(AddRec)).Show();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void dgv_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Util.IsOrgPersonWrite())
            {
                Edit();
            }
        }
        private void Edit()
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentRow.Index >= 0)
                {
                    int Id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                    CardDictionaryItem crd = new CardDictionaryItem(Id, new UpdateIntHandler(FillCard), new UpdateStringHandler(UpdateRec));
                    crd.ObjectName = dgv.CurrentRow.Cells["Name"].Value.ToString();
                    crd.Show();
                }
        }
        private void Delete()
        {
            if (dgv.CurrentCell != null)
                if (dgv.CurrentRow.Index >= 0)
                {
                    if (MessageBox.Show("Удалить выбранное?","Запрос на подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != System.Windows.Forms.DialogResult.Yes)
                        return;

                    int Id = int.Parse(dgv.CurrentRow.Cells["Id"].Value.ToString());
                    DeleteRec(Id);
                    FillCard(null);
                }
        }
        virtual public void DeleteRec(int Id)
        {

        }
        virtual public void AddRec(int? id, string name)
        {

        }
        virtual public void UpdateRec(int? Id, string name)
        {

        }
    }
}
