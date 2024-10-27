using System.Windows.Forms;

namespace EducationalCenterFinal
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //!!!!!!!!! Important Dont Remove
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.MaximizeBox = false;
            //!!!!!!!!! Important Dont Remove

            this.StaffButton = new System.Windows.Forms.Button();           //Remove
            this.AdminButton = new System.Windows.Forms.Button();           //Remove
            this.TeacherCourseButton = new System.Windows.Forms.Button();   //Remove
            this.SuspendLayout();
            // 
            // StaffButton      //Remove
            // 
            this.StaffButton.Location = new System.Drawing.Point(305, 343);
            this.StaffButton.Name = "StaffButton";
            this.StaffButton.Size = new System.Drawing.Size(151, 42);
            this.StaffButton.TabIndex = 0;
            this.StaffButton.Text = "Staff";
            this.StaffButton.UseVisualStyleBackColor = true;
            this.StaffButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // AdminButton      //Remove
            // 
            this.AdminButton.Location = new System.Drawing.Point(305, 231);
            this.AdminButton.Name = "AdminButton";
            this.AdminButton.Size = new System.Drawing.Size(151, 42);
            this.AdminButton.TabIndex = 1;
            this.AdminButton.Text = "Admin";
            this.AdminButton.UseVisualStyleBackColor = true;
            this.AdminButton.Click += new System.EventHandler(this.AdminButton_Click);
            // 
            // TeacherCourseButton      //Remove
            // 
            this.TeacherCourseButton.Location = new System.Drawing.Point(305, 144);
            this.TeacherCourseButton.Name = "TeacherCourseButton";
            this.TeacherCourseButton.Size = new System.Drawing.Size(151, 42);
            this.TeacherCourseButton.TabIndex = 2;
            this.TeacherCourseButton.Text = "Teacher";
            this.TeacherCourseButton.UseVisualStyleBackColor = true;
            this.TeacherCourseButton.Click += new System.EventHandler(this.TeacherCourseButton_Click);

            // 
            // LoginForm
            // 
            this.Controls.Add(this.TeacherCourseButton);    //Remove
            this.Controls.Add(this.AdminButton);            //Remove
            this.Controls.Add(this.StaffButton);            //Remove
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button StaffButton;
        private System.Windows.Forms.Button AdminButton;
        private System.Windows.Forms.Button TeacherCourseButton;
    }
}

