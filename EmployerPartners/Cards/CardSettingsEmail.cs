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
    public partial class CardSettingsEmail : Form
    {
        string UserName {
            get { return tbUserName.Text.Trim(); }
            set { tbUserName.Text = value; }
        }

        string Password
        {
            get { return tbPassword.Text.Trim(); }
            set { tbPassword.Text = value; }
        }

        public CardSettingsEmail()
        {
            InitializeComponent();
            FillCard();
        }

        private void FillCard()
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                UserName = context.C_AppSettings.Where(x => x.Key.ToLower() == "email_username").Select(x => x.Value).First();
                Password = context.C_AppSettings.Where(x => x.Key.ToLower() == "email_password").Select(x => x.Value).First();
            }
        }

        private void chbHidePassword_CheckedChanged(object sender, EventArgs e)
        {
            tbPassword.PasswordChar = chbHidePassword.Checked ? '*' : '\0';
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (EmployerPartnersEntities context = new EmployerPartnersEntities())
            {
                var tmp = (from x in context.C_AppSettings
                                where x.Key.ToLower() == "email_username"
                                select x).First();
                tmp.Value = UserName;
                context.SaveChanges();

                tmp = (from x in context.C_AppSettings
                               where x.Key.ToLower() == "email_password"
                               select x).First();
                tmp.Value = Password;
                context.SaveChanges();

            }
        }


    }
}
