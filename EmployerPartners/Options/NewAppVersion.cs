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
    public partial class NewAppVersion : Form
    {
        public NewAppVersion()
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
        }

        private void btnDontRunNewVersion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStopTimer_Click(object sender, EventArgs e)
        {
            Utilities.MainTimerStop = true;
        }

        private void BtnRunNewVersion_Click(object sender, EventArgs e)
        {

        }
    }
}
