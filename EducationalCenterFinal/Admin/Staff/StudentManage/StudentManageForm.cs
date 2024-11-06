using EducationalCenterFinal.Admin.CourseManage;
using EducationalCenterFinal.Admin.CreateAccount;
using EducationalCenterFinal.Admin.Dashboard;
using EducationalCenterFinal.Admin.EmployeeManage;
using EducationalCenterFinal.Admin.Staff.StaffCoursesManage;
using EducationalCenterFinal.Admin.TeacherManage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EducationalCenterFinal.Admin.Staff.StudentManage
{
    public partial class StudentManageForm : Form
    {
        readonly EducationCenterEntities dp = new EducationCenterEntities();
        student s = new student();
        public StudentManageForm(string role)
        {
            InitializeComponent();

            //Maximize window
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.MaximizeBox = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.questionsToolStripMenuItem.Click += (sender, e) => this.QuestionsToolStripMenuItem_Click(role);
            this.dashboardToolStripMenuItem.Click += (sender, e) => this.DashboardToolStripMenuItem_Click(role);

            //Disable Admin Sections
            if (role == "staff")
            {
                teachersToolStripMenuItem.Enabled = false;
                coursesToolStripMenuItem.Enabled = false;
                forgetPasswordToolStripMenuItem.Enabled = false;
                createAccountToolStripMenuItem.Enabled = false;
                employeesToolStripMenuItem.Enabled = false;
                dashboardToolStripMenuItem.Enabled = false;
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

            dgvStudent.DataSource= dp.students.ToList();
        }

        private void StudentManageForm_Load(object sender, EventArgs e)
        {
            SetPlaceholder();

            //custmize column Headers Back Color , color , font 
            dgvStudent.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(118, 41, 82);
            dgvStudent.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvStudent.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 12, FontStyle.Bold); 

            // Ensure the header style applies
            dgvStudent.EnableHeadersVisualStyles = false;

            // Other DataGridView configurations, if needed
            dgvStudent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudent.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudent.ReadOnly = true; //  setting for readonly cells
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

        private void DashboardToolStripMenuItem_Click(string role)
        {
            new DashboardForm(role).Show();
            this.Hide();
        }

        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void txtSearch_GotFocus(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }

        }

        private void txtSearch_LostFocus(object sender, EventArgs e)
        {
            SetPlaceholder(); // Restore placeholder if textbox is empty

        }

        private void Search()
        {
            if (int.TryParse(txtSearch.Text, out int id))
            {
                student s = dp.students.FirstOrDefault(x => x.studentId == id);

                if (s != null)
                {
                    txtName.Text = s.studentName;
                    txtEmail.Text = s.studentEmail;
                    txtAddress.Text = s.studentAddress;
                    txtPhone.Text = s.studentPhone;
                    txtSearch.Enabled = false;
                    addBtn.Enabled = false;

                }
                else
                {
                    MessageBox.Show("Student Not Found");
                }



            }
            else
            {
                MessageBox.Show("Enter a Valid Student Id");
            }

        }

        //Searching after click Enter
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
            s.studentName = txtName.Text;
            s.studentEmail = txtEmail.Text;
            s.studentAddress = txtAddress.Text;
            s.studentPhone = txtPhone.Text;
            dp.students.Add(s);
            dp.SaveChanges();
            //dgvStudent.DataSource = db.Students.ToList();
            MessageBox.Show("Student Saved Successfully");
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
            if (txtSearch.Text != "Search")
            {
                txtSearch.Enabled = true;
                addBtn.Enabled=true;
                txtSearch.Text = "";
                SetPlaceholder();

            }
            ClearStudentTextBox();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSearch.Text, out int id))
            {

                student s = dp.students.FirstOrDefault(x => x.studentId == id);
                s.studentName = txtName.Text;
                s.studentEmail = txtEmail.Text;
                s.studentAddress = txtAddress.Text;
                s.studentPhone = txtPhone.Text;

                dp.SaveChanges();
                MessageBox.Show("Data Edited Successfully");
                //dgvStudent.DataSource = db.Students.ToList();
                Reset();


            }
            else
            {
                MessageBox.Show("No Data to Edit");
            }
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSearch.Text, out int id))
            {

                student s = dp.students.FirstOrDefault(x => x.studentId == id);
                dp.students.Remove(s);
                dp.SaveChanges();
                MessageBox.Show("Data Deleted Successfully");
                //dgvStudent.DataSource = db.Students.ToList();
                Reset();


            }
            else
            {
                MessageBox.Show("No Data to Delete");
            }
        }
    }
}
