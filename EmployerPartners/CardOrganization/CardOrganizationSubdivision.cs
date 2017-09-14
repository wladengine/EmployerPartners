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
    public partial class CardOrganizationSubdivision : Form
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
        int _OrgId;
        string _Name;
        string _NameEng;
        UpdateIntHandler _h;

        public CardOrganizationSubdivision(int? id, int orgid, UpdateIntHandler h)
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            _h = h;
            _Id = id;
            _OrgId = orgid;
            FillCard();
        }
        public CardOrganizationSubdivision(int? id, int orgid, UpdateIntHandler h, string name, string nameEng)
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            _h = h;
            _Id = id;
            _OrgId = orgid;
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
                //    OrganizationSubdivision subdivision;
                //    subdivision = context.OrganizationSubdivision.Where(x => x.Id == _Id).First();
                //    PosName = subdivision.Name;
                //    PosNameEng = subdivision.NameEng;
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
                MessageBox.Show("Не заполнено поле 'Подразделение'", "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if ((PosName == _Name) && (PosNameEng == _NameEng))
            {
                this.Close();
                return;
            }
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    OrganizationSubdivision subdivision;
                    if (_Id.HasValue)
                    {
                        //Редактирование существующей записи
                        subdivision = context.OrganizationSubdivision.Where(x => x.Id == _Id).First();
                    }
                    else
                    {
                        //Добавление новой записи
                        subdivision = new OrganizationSubdivision();
                    }
                    subdivision.OrganizationId = _OrgId;
                    subdivision.Name = PosName;
                    subdivision.NameEng = PosNameEng;

                    if (!_Id.HasValue)
                    {
                        context.OrganizationSubdivision.Add(subdivision);
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
