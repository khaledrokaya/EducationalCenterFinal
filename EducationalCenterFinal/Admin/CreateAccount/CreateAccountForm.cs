using EducationalCenterFinal.Admin.CourseManage;
using EducationalCenterFinal.Admin.Dashboard;
using EducationalCenterFinal.Admin.EmployeeManage;
using EducationalCenterFinal.Admin.Staff.StaffCoursesManage;
using EducationalCenterFinal.Admin.Staff.StudentManage;
using EducationalCenterFinal.Admin.Staff;
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
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;
using EducationalCenterFinal.SpecialForms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Text.RegularExpressions;

namespace EducationalCenterFinal.Admin.CreateAccount
{
    public partial class CreateAccountForm : Form
    {
        readonly private EducationCenterEntities dp = new EducationCenterEntities();
        public CreateAccountForm()
        {
            InitializeComponent();

            this.questionsToolStripMenuItem.Click += (sender, e) => this.QuestionsToolStripMenuItem_Click("admin");
            this.dashboardToolStripMenuItem.Click += (sender, e) => this.DashboardToolStripMenuItem_Click("admin");
            this.studentsToolStripMenuItem.Click += (sender, e) => this.StudentsToolStripMenuItem_Click("admin");

            //Make Manage Course MenuItems
            foreach (var course in dp.courses)
            {
                ToolStripMenuItem courseMenuItem = new ToolStripMenuItem
                {
                    Name = $"{course.courseName}ToolStripMenuItem",
                    Size = new System.Drawing.Size(134, 26),
                    Text = course.courseName,
                };
                courseMenuItem.Click += (sender, e) => CourseMenuItem_Click(course.courseId, "admin");
                manageToolStripMenuItem.DropDownItems.Add(courseMenuItem);
            }
        }

        private void CourseMenuItem_Click(int CourseId, string role)
        {
            new StaffCourseForm(CourseId, role).Show();
            this.Hide();
        }

        private void EmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new EmployeeManageForm().Show();
            this.Hide();
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Hide();
        }

        private void CoursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CourseManageForm().Show();
            this.Hide();
        }

        private void ForgetPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ForgotPassword(dp).Show();
        }

        private void TeachersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TeacherManageForm().Show();
            this.Hide();
        }

        private void DashboardToolStripMenuItem_Click(string role)
        {
            new DashboardForm(role).Show();
            this.Hide();
        }

        private void QuestionsToolStripMenuItem_Click(string role)
        {
            new QuestionsForm(role).Show();
            this.Hide();
        }

        private void StudentsToolStripMenuItem_Click(string role)
        {
            new StudentManageForm(role).Show();
            this.Hide();
        }

        private void resetForm()
        {
            sign_confpass.Text = "";
            sign_password.Text = "";
            sign_username.Text = "";
            sign_phone.Text = "";
            sign_role.Text = "";
        }

        private async void sign_btn_Click(object sender, EventArgs e)
        {

            if (sign_username.Text == "" || sign_confpass.Text == "" || sign_password.Text == "" || sign_phone.Text == "" || sign_role.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (sign_password.Text.Trim() != sign_confpass.Text.Trim())
            {
                MessageBox.Show("Password and Confirm Password do not match", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (sign_phone.Text.Length != 11)
            {
                MessageBox.Show("Phone number must be 11 number", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!Regex.IsMatch(sign_username.Text.Trim(), @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"))
            {
                MessageBox.Show("Enter valid email address", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                try
                {
                    var existingUser = dp.users.FirstOrDefault(u => u.userEmail == sign_username.Text.Trim());
                    if (existingUser != null)
                    {
                        MessageBox.Show(sign_username.Text + " already exists", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var newUser = new user
                    {
                        userEmail = sign_username.Text.Trim(),
                        password = sign_password.Text.Trim(),
                        role = sign_role.Text.Trim(),
                        userPhone = sign_phone.Text.Trim(),
                        createAt = DateTime.Today
                    };

                    dp.users.Add(newUser);
                    dp.SaveChanges();
                    resetForm();
                    MessageBox.Show($"Registered successfully with user ID: {dp.users.Where(u => u.userEmail == newUser.userEmail).Select(u => u.userId).FirstOrDefault()}", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error connecting to the database: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
