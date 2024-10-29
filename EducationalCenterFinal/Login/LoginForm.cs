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
        }

        private void StaffButton_Click(object sender, EventArgs e)          
        {
            new StudentManageForm().Show();
            this.Hide();
        }

        private void TeacherCourseButton_Click(object sender, EventArgs e)  
        {
            new TeacherCourseForm().Show();
            this.Hide();
        }

        private void AdminButton_Click(object sender, EventArgs e)          
        {
            new TeacherManageForm().Show();
            this.Hide();
        }
    }
}
