using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hospital
{
    public partial class FormTreatmentMenu : Form
    {
        public FormTreatmentMenu()
        {
            InitializeComponent();
        }



        private void btnInternal_Click(object sender, EventArgs e)
        {
            FormInternalTreatment f = new FormInternalTreatment();
            f.Show();
        }

        private void btnExternal_Click(object sender, EventArgs e)
        {
            FormExternalTreatment f = new FormExternalTreatment();
            f.Show();
        }

        private void FormTreatmentMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
