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

        private void smiPersonList_Click(object sender, EventArgs e)
        {
            new ListPersons().Show();
        }

        private void smiDegree_Click(object sender, EventArgs e)
        {
            new CardDictionaryDegree().Show();
        }

        private void smiRank_Click(object sender, EventArgs e)
        {
            new CardDictionaryRank().Show();
        }

        private void smiActivityArea_Click(object sender, EventArgs e)
        {
            new CardDictionaryActivityArea().Show();
        }

        private void smiActivityGoal_Click(object sender, EventArgs e)
        {
            new CardDictionaryActivityGoal().Show();

        }

        private void ationalityAffiliation_Click(object sender, EventArgs e)
        {
            new CardDictionaryNatAffiliation().Show();
        }

        private void smiOwnership_Click(object sender, EventArgs e)
        {
            new CardDictionaryOwnership().Show();
        }
    }
}
