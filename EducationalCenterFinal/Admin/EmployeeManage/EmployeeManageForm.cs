using EducationalCenterFinal.Admin.CourseManage;
using EducationalCenterFinal.Admin.CreateAccount;
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

namespace EducationalCenterFinal.Admin.EmployeeManage
{
    public partial class EmployeeManageForm : Form
    {
        readonly EducationCenterEntities dp = new EducationCenterEntities();
        public EmployeeManageForm()
        {
            InitializeComponent();
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.MaximizeBox = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            foreach (var course in dp.courses)
            {
                ToolStripMenuItem courseMenuItem = new ToolStripMenuItem
                {
                    Name = $"{course.courseName}ToolStripMenuItem",
                    Size = new System.Drawing.Size(134, 26),
                    Text = course.courseName
                };
                //courseMenuItem.Click += CourseMenuItem_Click;
                manageToolStripMenuItem.DropDownItems.Add(courseMenuItem);
            }
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
        
        private void StudentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new StudentManageForm().Show();
            this.Hide();
        }

        private void CoursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CourseManageForm().Show();
            this.Hide();
        }
    }
}
