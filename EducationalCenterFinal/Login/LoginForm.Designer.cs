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
            this.StaffButton = new System.Windows.Forms.Button();
            this.AdminButton = new System.Windows.Forms.Button();
            this.TeacherCourseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // StaffButton
            // 
            this.StaffButton.Location = new System.Drawing.Point(305, 343);
            this.StaffButton.Name = "StaffButton";
            this.StaffButton.Size = new System.Drawing.Size(151, 42);
            this.StaffButton.TabIndex = 0;
            this.StaffButton.Text = "Staff";
            this.StaffButton.UseVisualStyleBackColor = true;
            this.StaffButton.Click += new System.EventHandler(this.StaffButton_Click);
            // 
            // AdminButton
            // 
            this.AdminButton.Location = new System.Drawing.Point(305, 231);
            this.AdminButton.Name = "AdminButton";
            this.AdminButton.Size = new System.Drawing.Size(151, 42);
            this.AdminButton.TabIndex = 1;
            this.AdminButton.Text = "Admin";
            this.AdminButton.UseVisualStyleBackColor = true;
            this.AdminButton.Click += new System.EventHandler(this.AdminButton_Click);
            // 
            // TeacherCourseButton
            // 
            this.TeacherCourseButton.Location = new System.Drawing.Point(305, 144);
            this.TeacherCourseButton.Name = "TeacherCourseButton";
            this.TeacherCourseButton.Size = new System.Drawing.Size(151, 42);
            this.TeacherCourseButton.TabIndex = 2;
            this.TeacherCourseButton.Text = "Teachers";
            this.TeacherCourseButton.UseVisualStyleBackColor = true;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 700);
            this.Controls.Add(this.TeacherCourseButton);
            this.Controls.Add(this.AdminButton);
            this.Controls.Add(this.StaffButton);
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.Text = "LoginForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button StaffButton;
        private System.Windows.Forms.Button AdminButton;
        private System.Windows.Forms.Button TeacherCourseButton;
    }
}

