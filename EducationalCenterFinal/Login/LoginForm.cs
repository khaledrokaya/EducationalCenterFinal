using EducationalCenterFinal.Admin.Dashboard;
using EducationalCenterFinal.Admin.Staff.StudentManage;
using EducationalCenterFinal.Admin.TeacherManage;
using EducationalCenterFinal.TeacherCourse;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EducationalCenterFinal
{
    public partial class LoginForm : Form
    {
        readonly private EducationCenterEntities dp = new EducationCenterEntities();
        public LoginForm()
        {
            InitializeComponent();

            //PlaceHolder here
            UserEmailPlaceHolder();
            PasswordPlaceHolder();
        }

        private void UserEmailPlaceHolder()
        {
            login_username.Text = "User Email...";
            login_username.ForeColor = Color.Gray;
            login_username.Enter += (sender, e) =>
            {
                if (login_username.Text == "User Email...")
                {
                    login_username.Text = "";
                    login_username.ForeColor = Color.Black;
                }
            };
            login_username.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(login_username.Text))
                {
                    login_username.Text = "User Email...";
                    login_username.ForeColor = Color.Gray;
                }
            };
        }

        private void PasswordPlaceHolder()
        {
            login_password.Text = "Password...";
            login_password.ForeColor = Color.Gray;
            login_password.Enter += (sender, e) =>
            {
                if (login_password.Text == "Password...")
                {
                    login_password.Text = "";
                    login_password.ForeColor = Color.Black;
                }
            };
            login_password.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(login_password.Text))
                {
                    login_password.Text = "Password...";
                    login_password.ForeColor = Color.Gray;
                }
            };
        }
        private void login_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(login_username.Text) || string.IsNullOrWhiteSpace(login_password.Text))
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var dp = new EducationCenterEntities())
                {
                    var user = dp.users.Where(u => u.userEmail == login_username.Text.Trim() && u.password == login_password.Text.Trim()).FirstOrDefault();

                    if (user != null)
                    {
                        string role = user.role;
                        if (role == "admin")
                        {
                            new DashboardForm("admin").Show();
                            this.Hide();
                        }
                        else if (role == "staff")
                        {
                            new StudentManageForm("staff").Show();
                            this.Hide();
                        }
                        else if (role == "teacher")
                        {
                            int userID = dp.teachers.Where(t => t.userId == user.userId).Select(t => t.teacherId).FirstOrDefault();
                            new TeacherCourseForm(userID).Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Username/Password", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        
    }
}
