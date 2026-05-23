using Hospital;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Hospital
{
    public partial class FormInternalTreatment : Form
    {
        InternalTreatmentRepository repo;
        DoctorRepository doctorRepo;
        PatientRepository patientRepo;

        public FormInternalTreatment()
        {
            InitializeComponent();

            string cs = @"Data Source=(LocalDB)\MSSQLLocalDB;
                          AttachDbFilename=|DataDirectory|\Database1.mdf;
                          Integrated Security=True;";

            repo = new InternalTreatmentRepository(cs);
            doctorRepo = new DoctorRepository();
            patientRepo = new PatientRepository();
        }
        private void ClearFields()
        {
            cmbPatients.SelectedIndex = 0;
            txtRoomNumber.Clear();
            txtTreatmentName.Clear();
            txtCost.Clear();
            txtNotes.Clear();
            dtpStartDate.Value = DateTime.Now;
            dtpEndDate.Value = DateTime.Now;
            chkNoEndDate.Checked = false;

            for (int i = 0; i < clbDoctors.Items.Count; i++)
                clbDoctors.SetItemChecked(i, false);
        }

        private void FormInternalTreatment_Load(object sender, EventArgs e)
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
            clbDoctors.Items.Clear();

            foreach (var d in list)
                clbDoctors.Items.Add(new DoctorItem(d.DoctorID, d.Name));
        }

        private void LoadData()
        {
            dgvInternalTreatments.DataSource = repo.GetAllInternalTreatments();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            InternalTreatment t = new InternalTreatment
            {
                PatientID = (int)cmbPatients.SelectedValue,
                RoomNumber = txtRoomNumber.Text,
                StartDate = dtpStartDate.Value,
                EndDate = chkNoEndDate.Checked ? dtpEndDate.Value : (DateTime?)null,
                TreatmentName = txtTreatmentName.Text,
                Cost = decimal.Parse(txtCost.Text),
                Notes = txtNotes.Text
            };

            int newID = repo.AddInternalTreatment(t);

            List<int> doctors = new List<int>();
            foreach (var item in clbDoctors.CheckedItems)
                doctors.Add(((DoctorItem)item).ID);

            repo.AddDoctorsToTreatment(newID, doctors);

            LoadData();
            MessageBox.Show("تمت إضافة المعالجة بنجاح");
        }

        private void dgvInternalTreatments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int id = Convert.ToInt32(dgvInternalTreatments.Rows[e.RowIndex].Cells["InternalTreatmentID"].Value);

            txtTreatmentName.Text = dgvInternalTreatments.Rows[e.RowIndex].Cells["TreatmentName"].Value.ToString();
            txtRoomNumber.Text = dgvInternalTreatments.Rows[e.RowIndex].Cells["RoomNumber"].Value.ToString();
            txtCost.Text = dgvInternalTreatments.Rows[e.RowIndex].Cells["Cost"].Value.ToString();
            txtNotes.Text = dgvInternalTreatments.Rows[e.RowIndex].Cells["Notes"].Value.ToString();

            cmbPatients.SelectedValue = dgvInternalTreatments.Rows[e.RowIndex].Cells["PatientID"].Value;
            dtpStartDate.Value = Convert.ToDateTime(dgvInternalTreatments.Rows[e.RowIndex].Cells["StartDate"].Value);

            if (dgvInternalTreatments.Rows[e.RowIndex].Cells["EndDate"].Value != DBNull.Value)
            {
                chkNoEndDate.Checked = true;
                dtpEndDate.Value = Convert.ToDateTime(dgvInternalTreatments.Rows[e.RowIndex].Cells["EndDate"].Value);
            }
            else
            {
                chkNoEndDate.Checked = false;
            }

            List<int> selectedDocs = repo.GetDoctorsForTreatment(id);

            for (int i = 0; i < clbDoctors.Items.Count; i++)
            {
                var item = (DoctorItem)clbDoctors.Items[i];
                clbDoctors.SetItemChecked(i, selectedDocs.Contains(item.ID));
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvInternalTreatments.SelectedRows.Count == 0)
            {
                MessageBox.Show("اختر معالجة أولاً");
                return;
            }

            int id = Convert.ToInt32(dgvInternalTreatments.SelectedRows[0].Cells["InternalTreatmentID"].Value);

            InternalTreatment t = new InternalTreatment
            {
                InternalTreatmentID = id,
                PatientID = (int)cmbPatients.SelectedValue,
                RoomNumber = txtRoomNumber.Text,
                StartDate = dtpStartDate.Value,
                EndDate = chkNoEndDate.Checked ? dtpEndDate.Value : (DateTime?)null,
                TreatmentName = txtTreatmentName.Text,
                Cost = decimal.Parse(txtCost.Text),
                Notes = txtNotes.Text
            };

            repo.UpdateInternalTreatment(t);

            repo.DeleteDoctorsForTreatment(id);

            List<int> doctors = new List<int>();
            foreach (var item in clbDoctors.CheckedItems)
                doctors.Add(((DoctorItem)item).ID);

            repo.AddDoctorsToTreatment(id, doctors);

            LoadData();
            MessageBox.Show("تم تعديل المعالجة");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvInternalTreatments.SelectedRows.Count == 0)
            {
                MessageBox.Show("اختر معالجة أولاً");
                return;
            }

            int id = Convert.ToInt32(dgvInternalTreatments.SelectedRows[0].Cells["InternalTreatmentID"].Value);

            repo.DeleteInternalTreatment(id);

            LoadData();
            MessageBox.Show("تم حذف المعالجة");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }

    public class DoctorItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public DoctorItem(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public override string ToString() => Name;
    }
}