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
    public partial class FormDoctors : Form
    {
        public FormDoctors()
        {
            InitializeComponent();
        }
        HospitalSystem hs = new HospitalSystem();
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormDoctors_Load(object sender, EventArgs e)
        {
            cmbType.Items.Add("Permanent");
            cmbType.Items.Add("Contract");
            cmbType.Items.Add("Trainee");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvDoctors.SelectedRows.Count > 0)
            {
                int id = (int)dgvDoctors.SelectedRows[0].Cells["DoctorID"].Value;
                hs.DeleteDoctor(id);

                MessageBox.Show("Doctor deleted successfully");

                dgvDoctors.DataSource = hs.GetAllDoctors();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Doctor d = new Doctor();
            d.Name = txtName.Text;
            d.Address = txtAddress.Text;
            d.BirthDate = dtBirth.Value;
            d.Salary = decimal.Parse(txtSalary.Text);
            d.DoctorType = cmbType.SelectedIndex + 1;
            d.StartTraining = dtStart.Value;

            if (chkHasEndDate.Checked)
                d.EndTraining = dtEnd.Value;
            else
                d.EndTraining = null;

            hs.AddDoctor(d);
            MessageBox.Show("Doctor added successfully");
           

            txtName.Clear();
            txtAddress.Clear();
            txtSalary.Clear();
            cmbType.SelectedIndex = -1;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            dgvDoctors.DataSource = hs.GetAllDoctors();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvDoctors.SelectedRows.Count > 0)
            {
                Doctor d = new Doctor();
                d.DoctorID = (int)dgvDoctors.SelectedRows[0].Cells["DoctorID"].Value;
                d.Name = txtName.Text;
                d.Address = txtAddress.Text;
                d.BirthDate = dtBirth.Value;
                d.Salary = decimal.Parse(txtSalary.Text);
                d.DoctorType = cmbType.SelectedIndex + 1;
                d.StartTraining = dtStart.Value;
                d.EndTraining = dtEnd.Value;

                hs.UpdateDoctor(d);

                MessageBox.Show("Doctor updated successfully");

                dgvDoctors.DataSource = hs.GetAllDoctors();
            }
        }

        private void dgvDoctors_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDoctors.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvDoctors.SelectedRows[0];

                txtName.Text = row.Cells["Name"].Value.ToString();
                txtAddress.Text = row.Cells["Address"].Value.ToString();
                txtSalary.Text = row.Cells["Salary"].Value.ToString();

                dtBirth.Value = Convert.ToDateTime(row.Cells["BirthDate"].Value);
                cmbType.SelectedIndex = Convert.ToInt32(row.Cells["DoctorType"].Value) - 1;

                if (row.Cells["StartTraining"].Value != DBNull.Value)
                    dtStart.Value = Convert.ToDateTime(row.Cells["StartTraining"].Value);

                if (row.Cells["EndTraining"].Value != DBNull.Value)
                    dtEnd.Value = Convert.ToDateTime(row.Cells["EndTraining"].Value);
            }
        }

        private void dgvDoctors_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dgvDoctors.Rows[e.RowIndex];

            txtName.Text = row.Cells["Name"].Value?.ToString();
            txtAddress.Text = row.Cells["Address"].Value?.ToString();
            dtBirth.Value = Convert.ToDateTime(row.Cells["BirthDate"].Value);

            cmbType.Text = row.Cells["DoctorType"].Value?.ToString();
            txtSalary.Text = row.Cells["Salary"].Value?.ToString();

            dtStart.Value = Convert.ToDateTime(row.Cells["StartTraining"].Value);

            if (row.Cells["EndTraining"].Value != DBNull.Value)
            {
                chkHasEndDate.Checked = true;
                dtEnd.Enabled = true;
                dtEnd.Value = Convert.ToDateTime(row.Cells["EndTraining"].Value);
            }
            else
            {
                chkHasEndDate.Checked = false;
                dtEnd.Enabled = false;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Clear();
            txtAddress.Clear();
            txtSalary.Clear();

            cmbType.SelectedIndex = -1;

            dtBirth.Value = DateTime.Now;
            dtStart.Value = DateTime.Now;

            chkHasEndDate.Checked = false;
            dtEnd.Enabled = false;
            dtEnd.Value = DateTime.Now;

            txtName.Focus();
        }

        private void dtEnd_ValueChanged(object sender, EventArgs e)
        {

        }

        private void chkHasEndDate_CheckedChanged(object sender, EventArgs e)
        {
            dtEnd.Enabled = chkHasEndDate.Checked;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
