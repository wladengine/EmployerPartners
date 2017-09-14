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
    public partial class CardDictionaryItem : Form
    {
        UpdateIntHandler _h;
        UpdateStringHandler _s;
        int? _Id;

        public string ObjectName
        {
            get { return tbName.Text.Trim(); }
            set { tbName.Text = value; }
        }

        public CardDictionaryItem()
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
        }
        public CardDictionaryItem(int? id, UpdateIntHandler h,UpdateStringHandler s)
        {
            InitializeComponent();
            this.MdiParent = Util.mainform;
            _h = h;
            _s = s;
            _Id = id;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_s != null)
                _s(_Id, ObjectName);
            this.Close();
        }


    }
}
