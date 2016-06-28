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
    public partial class CardNewOrganization : Form
    {
        UpdateVoidHandler _hdl;

        public CardNewOrganization(UpdateVoidHandler h)
        {
            InitializeComponent();
            _hdl = h;
        }

        private bool Check()
        {
            if (tbName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Введите наименование организации", "Напоминание");
                return false;
            }
            return true;
        }
        private bool CheckExist()
        {
            string Name = tbName.Text.Trim();
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var cnt = (from x in context.Organization
                           where x.Name == Name
                           select new { x.Id }).ToList();
                if (cnt.Count > 0)
                {
                    MessageBox.Show("Организация-партнер с таким названием уже существует.", "Инфо");
                    if (_hdl != null)
                        _hdl(cnt.First().Id);
                    return false;
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
                Organization org = new Organization();
                org.rowguid = Guid.NewGuid();
                org.Name = tbName.Text.Trim();
                context.Organization.Add(org);
                context.SaveChanges();

                if (_hdl != null)
                    _hdl(org.Id);
                
                this.Close();
                new CardOrganization(org.Id, _hdl).Show();
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Trim().Length == 0)
            {
                this.Close();
                return;
            }
            if (MessageBox.Show("Закрыть без сохранения?", "", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }
    }
}
