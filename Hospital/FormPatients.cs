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

    public partial class FormPatients : Form
    {
        HospitalSystem hs = new HospitalSystem();
        int selectedID = 0;
        public FormPatients()
        {
            InitializeComponent();
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormPatients_Load(object sender, EventArgs e)
        {
            cmbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbType.Items.Add("Internal");
            cmbType.Items.Add("External");
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Patient p = new Patient();
            p.Name = txtName.Text;
            p.Address = txtAddress.Text;
            p.BirthDate = dtBirth.Value;
            p.PatientType = cmbType.SelectedIndex + 1;
            p.IsDischarged = chkDischarged.Checked;

            hs.AddPatient(p);
            MessageBox.Show("Patient added successfully");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            dgvPatients.DataSource = hs.GetAllPatients();
        }

        private void dgvPatients_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvPatients.SelectedRows[0];

                selectedID = Convert.ToInt32(row.Cells["PatientID"].Value);
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                dtBirth.Value = Convert.ToDateTime(row.Cells["BirthDate"].Value);
                cmbType.SelectedIndex = Convert.ToInt32(row.Cells["PatientType"].Value) - 1;
                chkDischarged.Checked = Convert.ToBoolean(row.Cells["IsDischarged"].Value);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Patient p = new Patient();
            p.PatientID = selectedID;
            p.Name = txtName.Text;
            p.Address = txtAddress.Text;
            p.BirthDate = dtBirth.Value;
            p.PatientType = cmbType.SelectedIndex + 1;
            p.IsDischarged = chkDischarged.Checked;

            hs.UpdatePatient(p);
            MessageBox.Show("Patient updated successfully");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedID > 0)
            {
                hs.DeletePatient(selectedID);
                MessageBox.Show("Patient deleted successfully");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtAddress.Clear();
            cmbType.SelectedIndex = -1;
            chkDischarged.Checked = false;
            dtBirth.Value = DateTime.Now;
            selectedID = 0;
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
