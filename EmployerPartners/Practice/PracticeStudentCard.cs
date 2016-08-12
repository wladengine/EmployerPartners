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
    public partial class PracticeStudentCard : Form
    {
        private string FIO
        {
            get { return tbFIO.Text.Trim(); }
            set { tbFIO.Text = value; }
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
        UpdateVoidHandler _hndl;

        public PracticeStudentCard(int? id, UpdateVoidHandler _hdl)
        {
            InitializeComponent();
            _Id = id;
            _hndl = _hdl;
            FillCard();
            this.MdiParent = Util.mainform;
        }
        private void FillCard()
        {
            if (_Id.HasValue)
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var stud = (from x in context.PracticeLPStudent      //context.PracticeStudent
                               where x.Id == _Id
                               select x).First();
                    FIO = stud.StudentFIO;
                    Comment = stud.Comment;
                }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_Id.HasValue)
                return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    //var pst = context.PracticeStudent.Where(x => x.Id == _Id).First();
                    var pst = context.PracticeLPStudent.Where(x => x.Id == _Id).First();

                    pst.StudentFIO = FIO;
                    pst.Comment = Comment;

                    context.SaveChanges();

                    //MessageBox.Show("Данные сохранены", "Сообщение");
                    if (_hndl != null)
                        _hndl(_Id);

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить данные...\r\n" + ex.Message, "Сообщение");
            }
        }
    }
}
