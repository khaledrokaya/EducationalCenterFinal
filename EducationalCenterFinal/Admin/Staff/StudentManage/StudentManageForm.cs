using EducationalCenterFinal.Admin.CourseManage;
using EducationalCenterFinal.Admin.CreateAccount;
using EducationalCenterFinal.Admin.EmployeeManage;
using EducationalCenterFinal.Admin.Staff.StaffCoursesManage;
using EducationalCenterFinal.Admin.TeacherManage;
using EducationalCenterFinal.SpecialForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EducationalCenterFinal.Admin.Staff.StudentManage
{
    public partial class StudentManageForm : Form
    {
        readonly EducationCenterEntities dp = new EducationCenterEntities();
        students s = new students();

        public StudentManageForm(string role)
        {
            InitializeComponent();
            dgvStudent.DataSource = dp.students.ToList();
            setUpForm();
            setUpComponents();
            LoadStudentData();
            customizeDataGridView();   
           

            //Maximize window
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.MaximizeBox = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.questionsToolStripMenuItem.Click += (sender, e) => this.QuestionsToolStripMenuItem_Click(role);
           
            //Disable Admin Sections
            if (role == "staff")
            {
                teachersToolStripMenuItem.Enabled = false;
                coursesToolStripMenuItem.Enabled = false;
                forgetPasswordToolStripMenuItem.Enabled = false;
                createAccountToolStripMenuItem.Enabled = false;
                employeesToolStripMenuItem.Enabled = false;
            }

            //Make Manage Course MenuItems
            foreach (var course in dp.courses)
            {
                ToolStripMenuItem courseMenuItem = new ToolStripMenuItem
                {
                    Name = $"{course.courseName}ToolStripMenuItem",
                    Size = new System.Drawing.Size(134, 26),
                    Text = course.courseName,
                };
                courseMenuItem.Click += (sender, e) => CourseMenuItem_Click(course.courseId, role);
                manageToolStripMenuItem.DropDownItems.Add(courseMenuItem);
            }

            PictureBox pictureBox = new PictureBox
            {
                Image = Image.FromFile(Application.StartupPath.Remove(Application.StartupPath.Length - 10) + "\\Images\\search-interface-symbol.png"),
                SizeMode = PictureBoxSizeMode.Normal,
                Location = new Point(260,12),
                Size = new Size(20,20)



            };
            txtSearch.Controls.Add(pictureBox);


        }
        private void setUpForm()
        {
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.MaximizeBox = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }
        private void setUpComponents()
        {
            //search
            txtSearch.Size = new Size(((ClientSize.Width - 400) * 1 / 4), 40);
            txtSearch.Location = new Point(ClientSize.Width - 310, 35);

            // DataGridView 
            dgvStudent.Size = new Size(((ClientSize.Width - 44) * 3 / 4), ClientSize.Height - 160);
            dgvStudent.Location = new Point(20, 90);

            // panel 
            panel1.Size = new Size(((ClientSize.Width - 250) * 1 / 4), ClientSize.Height - 160);
            panel1.Location = new Point(ClientSize.Width - 340, 90);

            // Labels and TextBoxes 
            label5.Size = new Size(((ClientSize.Width - 300) * 1 / 4), ClientSize.Height - 600);
            label5.Location = new Point(20, 35);

            label1.Location = new Point(20, 65);
            label1.Size = new Size(panel1.Width - 120, panel1.Height / 8);
            txtName.Location = new Point(20, label2.Height + 80);
            txtName.Size = new Size(panel1.Width - 55, panel1.Height / 18);

            label2.Location = new Point(20, 155);
            label2.Size = new Size(panel1.Width - 120, panel1.Height / 8);
            txtEmail.Location = new Point(20, 195);
            txtEmail.Size = new Size(panel1.Width - 55, panel1.Height / 18);

            label3.Location = new Point(20, 255);
            label3.Size = new Size(panel1.Width - 120, panel1.Height / 8);
            txtAddress.Location = new Point(20, 295);
            txtAddress.Size = new Size(panel1.Width - 55, panel1.Height / 18);

            label4.Location = new Point(20, 355);
            label4.Size = new Size(panel1.Width - 120, panel1.Height / 8);
            txtPhone.Location = new Point(20, 395);
            txtPhone.Size = new Size(panel1.Width - 55, panel1.Height / 18);

            // Buttons 
            addBtn.Location = new Point(20, 500);
            btnEdit.Location = new Point(170, 500);
            btnDelete.Location = new Point(20, 600);
            resetBtn.Location = new Point(170, 600);

            addBtn.Size = new Size(panel1.Width - 200, panel1.Height / 14);
            btnEdit.Size = new Size(panel1.Width - 200, panel1.Height / 14);
            resetBtn.Size = new Size(panel1.Width - 200, panel1.Height / 14);
            btnDelete.Size = new Size(panel1.Width - 200, panel1.Height / 14);
        }
        private void StudentManageForm_Load(object sender, EventArgs e)
        {
            //setting placeholder for searching textBox
            SetPlaceholder();
            customizeDataGridView();
        }

        private void customizeDataGridView()
        {
            //Editing the DataGrideView HeaderText and Hiding 4 Columns (attendences , enrollment , exams , payments )
            var columnHeaders = new Dictionary<string, string>
            {
               { "studentId", "ID" },

               { "studentName", "Name" },

               { "studentEmail", "Email" },

               { "studentPhone", "Phone" },

               { "studentAddress", "Address" }

            };

            foreach (var column in columnHeaders)
            {
                if (dgvStudent.Columns[column.Key] != null)
                {
                    dgvStudent.Columns[column.Key].HeaderText = column.Value;
                }
            }

            string[] hiddenColumns = { "attendance", "enrollments", "exams", "payments" };

            foreach (var columnName in hiddenColumns)
            {
                if (dgvStudent.Columns[columnName] != null)
                {
                    dgvStudent.Columns[columnName].Visible = false;
                }
            }

            // Other DataGridView configurations 
            
            dgvStudent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudent.ScrollBars = ScrollBars.Both;
            dgvStudent.SelectionMode = DataGridViewSelectionMode.RowHeaderSelect;
            dgvStudent.ReadOnly = true; //  setting for readonly cells

            // Customize column headers
            dgvStudent.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvStudent.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.FromArgb(118, 41, 81),
                Font = new Font("Arial", 13F, FontStyle.Bold),
                ForeColor = Color.White,
                WrapMode = DataGridViewTriState.True
            };
            dgvStudent.ColumnHeadersHeight = 40;
            dgvStudent.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;

            // General settings
            dgvStudent.EnableHeadersVisualStyles = false;
            dgvStudent.GridColor = Color.Black;

            

            // Customize row headers
            dgvStudent.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvStudent.RowHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                Alignment = DataGridViewContentAlignment.MiddleLeft,
                BackColor = Color.White,
                Font = new Font("Arial", 14F, FontStyle.Regular),
                ForeColor = Color.Black,
                SelectionBackColor = Color.FromArgb(236, 236, 236),
                SelectionForeColor = Color.Black,
                WrapMode = DataGridViewTriState.True
            };
            dgvStudent.RowHeadersWidth = 30;
            dgvStudent.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            // Customize row appearance
            dgvStudent.RowTemplate.Height = 55;

            foreach (DataGridViewColumn column in dgvStudent.Columns)
            {
                column.DefaultCellStyle.SelectionBackColor = Color.FromArgb(236, 236, 236);
                column.DefaultCellStyle.SelectionForeColor = Color.Black;
                column.DefaultCellStyle.Font = new Font("Arial", 11F, FontStyle.Regular);
                column.Width = column.HeaderText == "ID" ? 40 : ((dgvStudent.Width - dgvStudent.RowHeadersWidth - 40) / 4);
                column.HeaderCell.Style.Alignment = column.HeaderText == "ID" ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft;
                column.DefaultCellStyle.Alignment = column.HeaderText == "ID" ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft;
            }
        }

        private void LoadStudentData(int? filterId = null)
        {
            var student = dp.students.Select(s => new {

                ID = s.studentId,

                Name = s.studentName,

                Email = s.studentEmail,

                Phone = s.studentPhone,

                Address = s.studentAddress,
            });
            if (filterId.HasValue)
            {
                student = student.Where(s => s.ID == filterId.Value);
            }


            dgvStudent.DataSource = student.ToList();

            customizeDataGridView();


        }

        private void CourseMenuItem_Click(int CourseId, string role)
        {
            new StaffCourseForm(CourseId, role).Show();
            this.Hide();
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Hide();
        }

        private void ForgetPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ForgotPassword(dp).Show();
        }

        private void CreateAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CreateAccountForm().Show();
            this.Hide();
        }

        private void TeachersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TeacherManageForm().Show();
            this.Hide();
        }

        private void EmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new EmployeeManageForm().Show();
            this.Hide();
        }

        private void CoursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CourseManageForm().Show();
            this.Hide();
        }

        private void QuestionsToolStripMenuItem_Click(string role)
        {
            new QuestionsForm(role).Show();
            this.Hide();
        }

        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search By ID...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void txtSearch_GotFocus(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search By ID...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }

        }

        private void txtSearch_LostFocus(object sender, EventArgs e)
        {
            SetPlaceholder();

        }

        private void Search()
        {

            string searchTerm = txtSearch.Text.Trim();


            if (int.TryParse(searchTerm, out int id))
            {
                students s = dp.students.FirstOrDefault(x => x.studentId == id);

                if (s != null)
                {
                    searchByStudentId(id);
                }
                else
                {
                    MessageBox.Show("Student Not Found");
                }
            }
            else
            {
                MessageBox.Show("Enter a Valid Student Id");
                LoadStudentData();

            }
        }
        private void searchByStudentId(int StudentId)
        {

            var result = dp.students

           .Where(s => s.studentId == StudentId)

           .Select(s => new {

               ID = s.studentId,

               Name = s.studentName,

               Email = s.studentEmail,

               Phone = s.studentPhone,

               Address = s.studentAddress,


           }).ToList();

            if (result.Any())
            {
                dgvStudent.DataSource = result;

                customizeDataGridView();
            }
        }

        //Searching after clicking Enter
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                e.SuppressKeyPress = true;

                Search();
            }
        }

        private void addBtn_Click_1(object sender, EventArgs e)
        {

            if (txtName.Text == "" || txtPhone.Text == "" || txtEmail.Text == "" || txtAddress.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                addStudent();

                ClearStudentTextBox();

            }
        }

        private void addStudent()
        {
            try
            {
                try
                {
                    var emailAddress = new System.Net.Mail.MailAddress(txtEmail.Text);
                }
                catch
                {
                    MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text, @"^\d{11}$"))
                {
                    MessageBox.Show("Phone number must be exactly 11 digits.", "Invalid Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dp.students.Any(s => s.studentEmail == txtEmail.Text))
                {
                    MessageBox.Show("A student with this email already exists.", "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var newStudent = new students
                {
                    studentName = txtName.Text.Trim(),
                    studentEmail = txtEmail.Text.Trim(),
                    studentAddress = txtAddress.Text.Trim(),
                    studentPhone = txtPhone.Text.Trim()
                };

                dp.students.Add(newStudent);
                dp.SaveChanges();

                MessageBox.Show("Student Saved Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                dgvStudent.DataSource = dp.students.ToList();
                txtName.Clear();
                txtEmail.Clear();
                txtAddress.Clear();
                txtPhone.Clear();
                txtName.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ClearStudentTextBox()
        {

            txtName.Text = "";

            txtEmail.Text = "";

            txtAddress.Text = "";

            txtPhone.Text = "";

        }

        private void Reset()
        {
            if (txtSearch.Text != "Search By ID...")
            {
                txtSearch.Enabled = true;

                addBtn.Enabled = true;

                txtSearch.Text = "";

                SetPlaceholder();

            }

            ClearStudentTextBox();

            LoadStudentData();

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtEmail.Text) ||
                    string.IsNullOrWhiteSpace(txtAddress.Text) ||
                    string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    MessageBox.Show("All fields are required. Please fill in all details.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    var emailAddress = new System.Net.Mail.MailAddress(txtEmail.Text);
                }
                catch
                {
                    MessageBox.Show("Please enter a valid email address.", "Invalid Email", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text, @"^\d{11}$"))
                {
                    MessageBox.Show("Phone number must be exactly 11 digits.", "Invalid Phone Number", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (int.TryParse(txtSearch.Text, out int id))
                {
                    students s = dp.students.FirstOrDefault(x => x.studentId == id);

                    if (s != null)
                    {
                        if (dp.students.Any(st => st.studentEmail == txtEmail.Text && st.studentId != id))
                        {
                            MessageBox.Show("A student with this email already exists.", "Duplicate Email", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        s.studentName = txtName.Text.Trim();
                        s.studentEmail = txtEmail.Text.Trim();
                        s.studentAddress = txtAddress.Text.Trim();
                        s.studentPhone = txtPhone.Text.Trim();

                        dp.SaveChanges();

                        dgvStudent.DataSource = dp.students.ToList();
                        Reset();

                        MessageBox.Show("Data Edited Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Student not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter a valid student ID to search.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void resetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvStudent.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a student to delete.", "Selection Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedRow = dgvStudent.SelectedRows[0];
                int studentId = int.Parse(selectedRow.Cells["ID"].Value.ToString());

                var student = dp.students.SingleOrDefault(s => s.studentId == studentId);

                if (student != null)
                {
                    var confirmResult = MessageBox.Show("Are you sure you want to delete this student?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (confirmResult == DialogResult.Yes)
                    {
                        dp.students.Remove(student);
                        dp.SaveChanges();

                        dgvStudent.DataSource = dp.students.ToList();
                        Reset();

                        MessageBox.Show("Data Deleted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Student not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvStudent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dgvStudent_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                var row = dgvStudent.Rows[e.RowIndex];

                string id = row.Cells["ID"].Value.ToString();

                string Name = row.Cells["Name"].Value.ToString();

                string Email = row.Cells["Email"].Value.ToString();

                string Phone = row.Cells["Phone"].Value.ToString();

                string Address = row.Cells["Address"].Value.ToString();


                txtName.Text = Name;

                txtEmail.Text = Email;

                txtAddress.Text = Address;

                txtPhone.Text = Phone;


            }

        }
    }
}