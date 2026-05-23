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
    public partial class FormExternalTreatment : Form
    {
        ExternalTreatmentRepository repo;
        PatientRepository patientRepo;
        DoctorRepository doctorRepo;

        public FormExternalTreatment()
        {
            InitializeComponent();

            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;
                          AttachDbFilename=|DataDirectory|\Database1.mdf;
                          Integrated Security=True;";

            repo = new ExternalTreatmentRepository(cs);
            patientRepo = new PatientRepository();
            doctorRepo = new DoctorRepository();
        }

        private void FormExternalTreatment_Load(object sender, EventArgs e)
        {
            LoadPatients();
            LoadDoctors();
            LoadData();
        }

        private void LoadPatients()
        {
            var list = patientRepo.GetAllPatients();
            cmbPatients.DataSource = list;
            cmbPatients.DisplayMember = "Name";
            cmbPatients.ValueMember = "PatientID";
        }

        private void LoadDoctors()
        {
            var list = doctorRepo.GetAllDoctors();
            cmbDoctors.DataSource = list;
            cmbDoctors.DisplayMember = "Name";
            cmbDoctors.ValueMember = "DoctorID";
        }

        private void LoadData()
        {
            dgvExternalTreatments.DataSource = repo.GetAll();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ExternalTreatment t = new ExternalTreatment
            {
                PatientID = (int)cmbPatients.SelectedValue,
                DoctorID = (int)cmbDoctors.SelectedValue,
                TreatmentName = txtTreatmentName.Text,
                Cost = decimal.Parse(txtCost.Text),
                Notes = txtNotes.Text,
                VisitDate = dtpVisitDate.Value
            };

            repo.Add(t);
            LoadData();
            MessageBox.Show("تمت إضافة المعالجة الخارجية");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvExternalTreatments.SelectedRows.Count == 0)
            {
                MessageBox.Show("اختر معالجة أولاً");
                return;
            }

            int id = Convert.ToInt32(dgvExternalTreatments.SelectedRows[0].Cells["ExternalTreatmentID"].Value);

            ExternalTreatment t = new ExternalTreatment
            {
                ExternalTreatmentID = id,
                PatientID = (int)cmbPatients.SelectedValue,
                DoctorID = (int)cmbDoctors.SelectedValue,
                TreatmentName = txtTreatmentName.Text,
                Cost = decimal.Parse(txtCost.Text),
                Notes = txtNotes.Text,
                VisitDate = dtpVisitDate.Value
            };

            repo.Update(t);
            LoadData();
            MessageBox.Show("تم تعديل المعالجة");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvExternalTreatments.SelectedRows.Count == 0)
            {
                MessageBox.Show("اختر معالجة أولاً");
                return;
            }

            int id = Convert.ToInt32(dgvExternalTreatments.SelectedRows[0].Cells["ExternalTreatmentID"].Value);

            repo.Delete(id);
            LoadData();
            MessageBox.Show("تم حذف المعالجة");
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtTreatmentName.Clear();
            txtCost.Clear();
            txtNotes.Clear();
            dtpVisitDate.Value = DateTime.Now;
            cmbPatients.SelectedIndex = 0;
            cmbDoctors.SelectedIndex = 0;
        }

        private void dgvExternalTreatments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            txtTreatmentName.Text = dgvExternalTreatments.Rows[e.RowIndex].Cells["TreatmentName"].Value.ToString();
            txtCost.Text = dgvExternalTreatments.Rows[e.RowIndex].Cells["Cost"].Value.ToString();
            txtNotes.Text = dgvExternalTreatments.Rows[e.RowIndex].Cells["Notes"].Value.ToString();

            cmbPatients.SelectedValue = dgvExternalTreatments.Rows[e.RowIndex].Cells["PatientID"].Value;
            cmbDoctors.SelectedValue = dgvExternalTreatments.Rows[e.RowIndex].Cells["DoctorID"].Value;

            dtpVisitDate.Value = Convert.ToDateTime(dgvExternalTreatments.Rows[e.RowIndex].Cells["VisitDate"].Value);
        }
    }
}
