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
        }

        private void LoginButton_Click(object sender, EventArgs e)          //Remove
        {
            this.Hide();
            TeacherCourseForm teacherCourseForm = new TeacherCourseForm();
            teacherCourseForm.Show();
        }

        private void TeacherCourseButton_Click(object sender, EventArgs e)  //Remove
        {
            this.Hide();
            TeacherCourseForm teacherCourseForm = new TeacherCourseForm();
            teacherCourseForm.Show();
        }

        private void AdminButton_Click(object sender, EventArgs e)          //Remove
        {
            this.Hide();
            TeacherManageForm teacherCourseForm = new TeacherManageForm();
            teacherCourseForm.Show();
        }
    }
}
