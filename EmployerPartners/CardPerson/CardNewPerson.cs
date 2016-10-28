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
    public partial class CardNewPerson: Form
    {
        UpdateVoidHandler _hdl;

        public CardNewPerson(UpdateVoidHandler h)
        {
            InitializeComponent();
            _hdl = h;
            this.MdiParent = Util.mainform;
        }

        private bool Check()
        {
            if (tbName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Введите ФИО физического лица", "Напоминание");
                return false;
            }
            if (tbTitle.Text.Trim().Length == 0)
            {
                MessageBox.Show("Введите регалии", "Напоминание");
                return false;
            }
            return true;
        }
        private bool CheckExist()
        {
            string Name = tbName.Text.Trim();
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var cnt = (from x in context.PartnerPerson
                           where x.Name == Name
                           select new { x.Id }).ToList();
                if (cnt.Count > 0)
                {
                    DialogResult msg = MessageBox.Show("Физическое лицо с таким именем уже существует. Открыть карточку существующего?\nДа - прервать добавление, открыть существующую карточку\nНет - продолжить добавление\nОтмена - прервать добавление, карточку не открывать", "", MessageBoxButtons.YesNoCancel);
                    if (msg == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (_hdl != null)
                            _hdl(cnt.First().Id);
                        new CardPerson(cnt.First().Id, _hdl).Show();
                        return false;
                    }
                    else if (msg == System.Windows.Forms.DialogResult.No)
                    {
                        return true;
                    }
                    else if (msg == System.Windows.Forms.DialogResult.Cancel)
                    {
                        return false;
                    }

                    if (_hdl != null)
                        _hdl(cnt.First().Id);
                }
            }
            return true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Check() || !CheckExist())
                return;

            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                PartnerPerson org = new PartnerPerson();
                org.rowguid = Guid.NewGuid();
                org.Name = tbName.Text.Trim();
                org.Title = tbTitle.Text.Trim();
                context.PartnerPerson.Add(org);
                context.SaveChanges();

                if (_hdl != null)
                    _hdl(org.Id);
                
                this.Close();
                new CardPerson(org.Id, _hdl).Show();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if ((tbName.Text.Trim().Length == 0) && (tbTitle.Text.Trim().Length == 0))
            {
                this.Close();
                return;
            }
            if (MessageBox.Show("Закрыть без сохранения?", "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }
    }
}
