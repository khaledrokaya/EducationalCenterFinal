using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EducationalCenterFinal.TeacherCourse
{
    public partial class TeacherCourseForm : Form
    {
        public TeacherCourseForm()
        {
            InitializeComponent();
        }

        private void logoutButton_Click(object sender, EventArgs e) //Remove
        {
            this.Hide();
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }
    }
}
