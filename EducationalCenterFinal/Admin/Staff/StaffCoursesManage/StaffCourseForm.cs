using EducationalCenterFinal.Admin.CourseManage;
using EducationalCenterFinal.Admin.CreateAccount;
using EducationalCenterFinal.Admin.Dashboard;
using EducationalCenterFinal.Admin.EmployeeManage;
using EducationalCenterFinal.Admin.Staff.StudentManage;
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

namespace EducationalCenterFinal.Admin.Staff.StaffCoursesManage
{
    public partial class StaffCourseForm : Form
    {
        readonly EducationCenterEntities dp = new EducationCenterEntities();
        public StaffCourseForm(int CourseID, string role)
        {
            InitializeComponent();

            //Maximize window
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.MaximizeBox = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.questionsToolStripMenuItem.Click += (sender, e) => this.QuestionsToolStripMenuItem_Click(role);
            this.dashboardToolStripMenuItem.Click += (sender, e) => this.DashboardToolStripMenuItem_Click(role);
            this.studentsToolStripMenuItem.Click += (sender, e) => this.StudentsToolStripMenuItem_Click(role);

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
                    Enabled = CourseID != course.courseId
                };
                courseMenuItem.Click += (sender, e) => CourseMenuItem_Click(course.courseId, role);
                manageToolStripMenuItem.DropDownItems.Add(courseMenuItem);
            }
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

        private void StudentsToolStripMenuItem_Click(string role)
        {
            new StudentManageForm(role).Show();
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
    }
}
