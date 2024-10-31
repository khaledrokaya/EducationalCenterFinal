using EducationalCenterFinal.Admin.Dashboard;
using EducationalCenterFinal.Admin.Staff.StudentManage;
using EducationalCenterFinal.Admin.TeacherManage;
using EducationalCenterFinal.TeacherCourse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EducationalCenterFinal
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            //!!!!!!!!! Important Dont Remove
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.MaximizeBox = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //!!!!!!!!! Important Dont Remove
            //this.TeacherCourseButton.Click += (sender, e) => this.TeacherCourseButton_Click(); // Uncomment and add TeacherId not UserId
        }

        private void StaffButton_Click(object sender, EventArgs e)
        {
            new StudentManageForm("staff").Show();
            this.Hide();
        }

        private void TeacherCourseButton_Click(int TeacherId)
        {
            new TeacherCourseForm(TeacherId).Show();
            this.Hide();
        }

        private void AdminButton_Click(object sender, EventArgs e)
        {
            new DashboardForm("admin").Show();
            this.Hide();
        }

        private void login_username_Enter(object sender, EventArgs e)
        {
            if (login_username.Text == "Username")
            {
                login_username.Text = "";
                login_username.ForeColor = Color.Black;
            }
        }

        private void login_username_Leave(object sender, EventArgs e)
        {
            if (login_username.Text == "")
            {
                login_username.Text = "Username";
                login_username.ForeColor = Color.LightGray;
            }
        }

        private void login_password_Enter(object sender, EventArgs e)
        {
            if (login_password.Text == "Password")
            {
                login_password.Text = "";
                login_password.ForeColor = Color.Black;
            }
        }

        private void login_password_Leave(object sender, EventArgs e)
        {
            if (login_password.Text == "")
            {
                login_password.Text = "Password";
                login_password.ForeColor = Color.LightGray;
            }
        }
    }
}
