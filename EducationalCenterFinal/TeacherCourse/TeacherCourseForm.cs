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
        public TeacherCourseForm(int TeacherId)
        {
            InitializeComponent();
            //!!!!!!!!! Important Dont Remove
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.MaximizeBox = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //!!!!!!!!! Important Dont Remove
        }

        private void LogoutButton_Click(object sender, EventArgs e) //Remove
        {
            new LoginForm().Show();
            this.Hide();
        }
    }
}
