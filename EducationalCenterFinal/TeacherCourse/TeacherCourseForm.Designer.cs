using System.Windows.Forms;

namespace EducationalCenterFinal.TeacherCourse
{
    partial class TeacherCourseForm
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

            this.logoutButton = new System.Windows.Forms.Button();      //Remove
            this.SuspendLayout();
            // 
            // logoutButton     //Remove
            // 
            this.logoutButton.Location = new System.Drawing.Point(277, 161);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(149, 78);
            this.logoutButton.TabIndex = 0;
            this.logoutButton.Text = "logout";
            this.logoutButton.UseVisualStyleBackColor = true;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);

            // 
            // TeacherCourseForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.logoutButton);
            this.Name = "TeacherCourseForm";
            this.Text = "TeacherCourseForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button logoutButton;
    }
}