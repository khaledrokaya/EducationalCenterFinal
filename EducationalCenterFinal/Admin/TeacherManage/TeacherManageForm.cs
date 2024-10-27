using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EducationalCenterFinal.Admin.TeacherManage
{
    public partial class TeacherManageForm : Form
    {
        public TeacherManageForm()
        {
            InitializeComponent();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
                this.Hide();
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
        }
    }
}
