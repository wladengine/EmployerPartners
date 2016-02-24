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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            var x = Util.lstCountry;
            this.Text = "Организации партнеры";
            if (Util.IsAdministrator())
            {
                smiSettings.Visible = true;
            }
            else
                smiSettings.Visible = false;
        }

        private void smiOrganizationList_Click(object sender, EventArgs e)
        {
            new ListOrganizations().Show();
        }

        private void smiEmailSettings_Click(object sender, EventArgs e)
        {
            new CardSettingsEmail().Show();
        }
    }
}
