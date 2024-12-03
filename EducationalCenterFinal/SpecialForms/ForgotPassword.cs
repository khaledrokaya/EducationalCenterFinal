using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EducationalCenterFinal.SpecialForms
{
    public partial class ForgotPassword : Form
    {
        public ForgotPassword(EducationCenterEntities dp)
        {
            InitializeComponent();
            SubmitButton.Click += (_sender, _e) => SubmitButton_Click(dp);
        }

        private void SubmitButton_Click(EducationCenterEntities dp)
        {
            int userID = int.Parse(UserId.Text);
            string password = NewPassword.Text;
            string reEnterPassword = ReEnterPassword.Text;
            if (password != reEnterPassword)
            {
                MessageBox.Show("Passwords Don't Match!");
                return;
            }
            try
            {
                users us = dp.users.Where(u => u.userId == userID).FirstOrDefault();
                if (us == null)
                {
                    MessageBox.Show("UserId Not Found!");
                    return;
                }
                else
                {
                    us.password = password;
                    dp.SaveChanges();
                    MessageBox.Show($"Password for user {userID} Has Changed Succussfully.");
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("Error Happened While Changing Password!");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
