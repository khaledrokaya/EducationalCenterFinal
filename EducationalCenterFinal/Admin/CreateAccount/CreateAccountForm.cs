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

namespace EducationalCenterFinal.Admin.CreateAccount
{
    public partial class CreateAccountForm : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=EL-AGAMY\SQLEXPRESS;Initial Catalog=Open_Source_Education_Center;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

        readonly EducationCenterEntities dp = new EducationCenterEntities();
        public CreateAccountForm()
        {
            InitializeComponent();

            //Maximize window
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.MaximizeBox = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.questionsToolStripMenuItem.Click += (sender, e) => this.QuestionsToolStripMenuItem_Click("admin");
            this.dashboardToolStripMenuItem.Click += (sender, e) => this.DashboardToolStripMenuItem_Click("admin");
            this.studentsToolStripMenuItem.Click += (sender, e) => this.StudentsToolStripMenuItem_Click("admin");

            //Make Manage Course MenuItems
            //foreach (var course in dp.courses)
            //{
            //    ToolStripMenuItem courseMenuItem = new ToolStripMenuItem
            //    {
            //        Name = $"{course.courseName}ToolStripMenuItem",
            //        Size = new System.Drawing.Size(134, 26),
            //        Text = course.courseName,
            //    };
            //    courseMenuItem.Click += (sender, e) => CourseMenuItem_Click(course.courseId, "admin");
            //    manageToolStripMenuItem.DropDownItems.Add(courseMenuItem);
            //}
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

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sign_btn_Click(object sender, EventArgs e)
        {

            if (sign_username.Text == "" || sign_confpass.Text == ""
                || sign_password.Text == "" || sign_phone.Text == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();
                        String checkUsername = "SELECT * FROM users WHERE userEmail = '"
                            + sign_username.Text.Trim() + "'"; // users is the table name

                        using (SqlCommand checkUser = new SqlCommand(checkUsername, connect))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(checkUser);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count >= 1)
                            {
                                MessageBox.Show(sign_username.Text + " is already exist", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                string insertData = "INSERT INTO users ( userEmail, password, role, userPhone, createAt) " +
                                    "VALUES( @userEmail, @password, @role, @phone, @date)";
                                DateTime date = DateTime.Today;

                                using (SqlCommand cmd = new SqlCommand(insertData, connect))
                                {

                                    cmd.Parameters.AddWithValue("@userEmail", sign_username.Text.Trim());// the first textbox is for email or name !!
                                    cmd.Parameters.AddWithValue("@password", sign_password.Text.Trim());
                                    cmd.Parameters.AddWithValue("@role", sign_role.Text.Trim());
                                    cmd.Parameters.AddWithValue("@phone", sign_phone.Text.Trim());
                                    cmd.Parameters.AddWithValue("@date", date);

                                    cmd.ExecuteNonQuery();

                                    MessageBox.Show("Registered successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    // TO SWITCH THE FORM 
                                    //LoginForm lForm = new LoginForm();
                                    //lForm.Show();
                                    //this.Hide();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error connecting Database: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }


            }

        }

    }
}
