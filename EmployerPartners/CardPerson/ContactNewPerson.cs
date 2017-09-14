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
    public partial class ContactNewPerson : Form
    {
        public int OrgId;
        UpdateIntHandler _hdl;

        public ContactNewPerson(int orgid, UpdateIntHandler h)
        {
            InitializeComponent();
            OrgId = orgid;
            _hdl = h;
            FillInfo();
            this.MdiParent = Util.mainform;
        }
        private void FillInfo()
        {
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    var org = context.Organization.Where(x => x.Id == OrgId).First();
                    tbTitle.Text = "контактное лицо " + ((String.IsNullOrEmpty(org.ShortName)) ? ((String.IsNullOrEmpty(org.MiddleName)) ? org.Name : org.MiddleName) : org.ShortName);
                }
            }
            catch (Exception)
            {}
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
                    DialogResult msg = MessageBox.Show("Физическое лицо с таким именем уже существует. Открыть карточку существующего?\r\n" + 
                        "Да - прервать добавление, открыть существующую карточку\nНет - продолжить добавление\nОтмена - прервать добавление, карточку не открывать", "Запрос на подтверждение", 
                        MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                    if (msg == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (Utilities.PersonCardIsOpened(cnt.First().Id))
                            return false;
                        new CardPerson(cnt.First().Id, null).Show();
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

                    //if (_hdl != null)
                    //    _hdl(cnt.First().Id);
                }
            }
            return true;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (tbName.Text.Trim().Length == 0)
            {
                this.Close();
                return;
            }
            if (MessageBox.Show("Закрыть без сохранения?", "Запрос на подтверждение", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!Check() || !CheckExist())
                return;
            try
            {
                using (EmployerPartnersEntities context = new EmployerPartnersEntities())
                {
                    PartnerPerson person = new PartnerPerson();
                    person.rowguid = Guid.NewGuid();
                    person.Name = tbName.Text.Trim();
                    person.Title = tbTitle.Text.Trim();
                    context.PartnerPerson.Add(person);
                    context.SaveChanges();

                    if (_hdl != null)
                        _hdl(person.Id);

                    this.Close();
                    //new CardPerson(person.Id, _hdl).Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось добавить запись...\r\n" + ex.Message, "Инфо", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }
    }
}
