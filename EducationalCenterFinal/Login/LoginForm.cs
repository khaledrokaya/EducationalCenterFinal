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
        SqlConnection connect = new SqlConnection(@"Data Source=EL-AGAMY\SQLEXPRESS;Initial Catalog=Open_Source_Education_Center;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

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

        

        private void login_btn_Click(object sender, EventArgs e)
        {
            if (login_username.Text == "" || login_password.Text == "")
            {
                MessageBox.Show("Please fil all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();

                        String selectData = "SELECT userEmail, password, role FROM users WHERE userEmail = @userEmail AND password = @pass;";


                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@userEmail", login_username.Text);//again the first textbox is for email or name!!
                            cmd.Parameters.AddWithValue("@pass", login_password.Text);

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count >= 1)
                            {
                                string role = table.Rows[0]["role"].ToString();

                                MessageBox.Show("Logged In successfully", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                //change to main form  depend on role

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
                                    //new TeacherCourseForm(TeacherId).Show();
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
                        MessageBox.Show("Error Connecting: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
