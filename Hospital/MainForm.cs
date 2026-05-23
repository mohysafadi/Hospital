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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            FormDoctors f = new FormDoctors();
            f.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnPatients_Click(object sender, EventArgs e)
        {
            FormPatients fp = new FormPatients();
            fp.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void btnTreatments_Click(object sender, EventArgs e)
        {
            FormTreatmentMenu f = new FormTreatmentMenu();
            f.Show();
        }
    }
}
