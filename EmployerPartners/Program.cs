using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployerPartners
{
    static class Program
    {
        public static MainForm mf;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Utilities.TestConnection())
            {
                mf = new MainForm();
                Util.mainform = mf;
                Application.Run(mf);
            }
        }
    }
}
