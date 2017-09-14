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
    public partial class GAK_SourceEdit : Form
    {
        private string Faculty
        {
            get { return tbFaculty.Text.Trim(); }
            set { tbFaculty.Text = value; }
        }
        private string Source
        {
            get { return tbSource.Text.Trim(); }
            set { tbSource.Text = value; }
        }
        private string Number
        {
            get { return tbNumber.Text.Trim(); }
            set { tbNumber.Text = value; }
        }
        private string Comment
        {
            get { return tbComment.Text.Trim(); }
            set { tbComment.Text = value; }
        }
        private int? _Id
        {
            get;
            set;
        }

        UpdateIntHandler _hndl;

        public GAK_SourceEdit(int id, UpdateIntHandler _hdl)
        {
            InitializeComponent();
            _Id = id;
            _hndl = _hdl;
            this.MdiParent = Util.mainform;
            FillCard();
        }
        private void FillCard()
        {
            try
            {
                if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var gak = (from x in context.GAK_ChairmanSource
                                where x.Id == _Id
                                select x).First();
                    Faculty = gak.Faculty;
                    Source = gak.Source;
                    Number = gak.Numbers;
                    Comment = gak.Comment;
                    try
                    {
                        this.Text = "Источники ГАК: " + Faculty;
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Сохранить изменения?", "Запрос на подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.No) 
            {
                this.Close();
            }
            else
            {
                SaveChanges();
            }
            
        }
        private void SaveChanges()
        {
            if (!_Id.HasValue)
                return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var gak = context.GAK_ChairmanSource.Where(x => x.Id == _Id).First();

                    gak.Faculty = Faculty;
                    gak.Source = Source;
                    gak.Numbers = Number;
                    gak.Comment = Comment;

                    context.SaveChanges();

                    //MessageBox.Show("Данные сохранены", "Сообщение");
                    if (_hndl != null)
                        _hndl(_Id);

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }
    }
}
