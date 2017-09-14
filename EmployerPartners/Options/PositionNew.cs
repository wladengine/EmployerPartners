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
    public partial class PositionNew : Form
    {
        public string PosName
        {
            get { return tbName.Text.Trim(); }
            set { tbName.Text = value; }
        }
        public string PosNameEng
        {
            get { return tbNameEng.Text.Trim(); }
            set { tbNameEng.Text = value; }
        }

        int? _Id;
        string _Name;
        string _NameEng;
        UpdateIntHandler _h;

        public PositionNew(int? id, UpdateIntHandler h)
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            _h = h;
            _Id = id;
            FillCard();
        }
        public PositionNew(int? id, UpdateIntHandler h, string name, string nameEng)
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            _h = h;
            _Id = id;
            _Name = name;
            _NameEng = nameEng;
            FillCard();
        }
        private void FillCard()
        {
            PosName = _Name;
            PosNameEng = _NameEng;

            if (_Id.HasValue)
            {
                lblHeader.Text = "Редактирование";
                btnSave.Text = "Сохранить";
                //using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                //{
                //    Position position;
                //    position = context.Position.Where(x => x.Id == _Id).First();
                //    PosName = position.Name;
                //    PosNameEng = position.NameEng;
                //}
            }
            else
            {
                lblHeader.Text = "Добавление";
                btnSave.Text = "Добавить";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(PosName))
            {
                MessageBox.Show("Не заполнено поле 'Должность'", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if ((PosName == _Name) && (PosNameEng == _NameEng))
            {
                this.Close();
                return;
            }
            try
            {
                using (EmployerPartnersEntities  context = new EmployerPartnersEntities())
                {
                    Position position;
                    if (_Id.HasValue)
                    {
                        //Редактирование существующей записи
                        position = context.Position.Where(x => x.Id == _Id).First();
                    }
                    else
                    {
                        //Добавление новой записи
                        position = new Position();
                    }
                    position.Name = PosName;
                    position.NameEng = PosNameEng;

                    if (!_Id.HasValue)
                    {
                        context.Position.Add(position);
                    }
                    context.SaveChanges();
                    //MessageBox.Show("Данные сохранены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось сохранить изменения\r\n" + "Причина:" + ex.Message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            
            if (_h != null)
                _h(_Id);
            this.Close();
        }
    }
}
