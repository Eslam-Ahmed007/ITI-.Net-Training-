using UniversityManagementApp_CodeFirst.Models;
using UniversityManagementApp_CodeFirst.Repository;

namespace UniversityManagementApp_CodeFirst
{
    public partial class Form1 : Form
    {
        // Repository instance for data operations
        private readonly IStudentRepository _studentRepository;

        // Track selected student ID
        private int _selectedStudentId = 0;

        public Form1()
        {
            InitializeComponent();
            _studentRepository = new StudentRepository();

            // Initialize form on load
            this.Load += Form1_Load;
        }

        /// <summary>
        /// Form load event - initial setup
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            LoadStudents();
            SetButtonStates(false);
            ClearInputFields();
        }

        /// <summary>
        /// Configure DataGridView settings
        /// </summary>
        private void InitializeDataGridView()
        {
            dgvStudents.AutoGenerateColumns = true;
            dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudents.MultiSelect = false;
            dgvStudents.ReadOnly = true;
            dgvStudents.AllowUserToAddRows = false;

            // Event handler for row selection
            dgvStudents.SelectionChanged += DgvStudents_SelectionChanged;
        }

        /// <summary>
        /// Load all students into DataGridView
        /// </summary>
        private void LoadStudents()
        {
            try
            {
                var students = _studentRepository.GetAllStudents();
                dgvStudents.DataSource = students;

                // Adjust column widths
                if (dgvStudents.Columns.Count > 0)
                {
                    dgvStudents.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error loading students: {ex.Message}");
            }
        }

        /// <summary>
        /// Handle DataGridView row selection
        /// </summary>
        private void DgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                var selectedRow = dgvStudents.SelectedRows[0];
                _selectedStudentId = Convert.ToInt32(selectedRow.Cells["StudentId"].Value);

                // Populate input fields with selected student data
                PopulateFields(selectedRow);
                SetButtonStates(true);
            }
            else
            {
                SetButtonStates(false);
                _selectedStudentId = 0;
            }
        }

        /// <summary>
        /// Populate input fields with selected student data
        /// </summary>
        private void PopulateFields(DataGridViewRow row)
        {
            txtFirstName.Text = row.Cells["FirstName"].Value?.ToString();
            txtLastName.Text = row.Cells["LastName"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();
            txtPhone.Text = row.Cells["Phone"].Value?.ToString();

            if (row.Cells["EnrollmentDate"].Value != null)
            {
                dtpEnrollmentDate.Value = Convert.ToDateTime(row.Cells["EnrollmentDate"].Value);
            }
        }

        /// <summary>
        /// Add button click event
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                var newStudent = new Student
                {
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    EnrollmentDate = dtpEnrollmentDate.Value
                };

                bool success = _studentRepository.AddStudent(newStudent);

                if (success)
                {
                    ShowSuccess("Student added successfully!");
                    RefreshAndClear();
                }
                else
                {
                    ShowError("Failed to add student.");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error adding student: {ex.Message}");
            }
        }

        /// <summary>
        /// Update button click event
        /// </summary>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedStudentId == 0)
            {
                ShowWarning("Please select a student to update.");
                return;
            }

            if (!ValidateInputs())
                return;

            try
            {
                var updatedStudent = new Student
                {
                    StudentId = _selectedStudentId,
                    FirstName = txtFirstName.Text.Trim(),
                    LastName = txtLastName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    EnrollmentDate = dtpEnrollmentDate.Value
                };

                bool success = _studentRepository.UpdateStudent(updatedStudent);

                if (success)
                {
                    ShowSuccess("Student updated successfully!");
                    RefreshAndClear();
                }
                else
                {
                    ShowError("Failed to update student.");
                }
            }
            catch (Exception ex)
            {
                ShowError($"Error updating student: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete button click event
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedStudentId == 0)
            {
                ShowWarning("Please select a student to delete.");
                return;
            }

            var result = MessageBox.Show(
                "Are you sure you want to delete this student?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                try
                {
                    bool success = _studentRepository.DeleteStudent(_selectedStudentId);

                    if (success)
                    {
                        ShowSuccess("Student deleted successfully!");
                        RefreshAndClear();
                    }
                    else
                    {
                        ShowError("Failed to delete student.");
                    }
                }
                catch (Exception ex)
                {
                    ShowError($"Error deleting student: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Clear button click event
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearInputFields();
            dgvStudents.ClearSelection();
            _selectedStudentId = 0;
            SetButtonStates(false);
        }

        /// <summary>
        /// Validate all input fields
        /// </summary>
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
                ShowWarning("First Name is required.");
                txtFirstName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
                ShowWarning("Last Name is required.");
                txtLastName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                ShowWarning("Email is required.");
                txtEmail.Focus();
                return false;
            }

            // Basic email validation
            if (!txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
            {
                ShowWarning("Please enter a valid email address.");
                txtEmail.Focus();
                return false;
            }

            return true;
        }

        /// <summary>
        /// Clear all input fields
        /// </summary>
        private void ClearInputFields()
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            dtpEnrollmentDate.Value = DateTime.Now;
        }

        /// <summary>
        /// Refresh data and clear fields
        /// </summary>
        private void RefreshAndClear()
        {
            LoadStudents();
            ClearInputFields();
            dgvStudents.ClearSelection();
            _selectedStudentId = 0;
            SetButtonStates(false);
        }

        /// <summary>
        /// Set button enabled/disabled states based on selection
        /// </summary>
        private void SetButtonStates(bool hasSelection)
        {
            btnAdd.Enabled = true;  // Always enabled
            btnUpdate.Enabled = hasSelection;
            btnDelete.Enabled = hasSelection;
            btnClear.Enabled = true;  // Always enabled
        }

        #region Message Helpers

        private void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowWarning(string message)
        {
            MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        #endregion
    }
}