namespace EducationalCenterFinal.Login
{
    partial class SignForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.sign_btn = new System.Windows.Forms.Button();
            this.sign_password = new System.Windows.Forms.TextBox();
            this.sign_username = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.sign_phone = new System.Windows.Forms.TextBox();
            this.sign_confpass = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.sign_role = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(-1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1403, 701);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(820, 101);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(513, 477);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.sign_role);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.sign_phone);
            this.panel2.Controls.Add(this.sign_confpass);
            this.panel2.Controls.Add(this.sign_btn);
            this.panel2.Controls.Add(this.sign_password);
            this.panel2.Controls.Add(this.sign_username);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(702, 701);
            this.panel2.TabIndex = 0;
            // 
            // sign_btn
            // 
            this.sign_btn.BackColor = System.Drawing.Color.Purple;
            this.sign_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sign_btn.ForeColor = System.Drawing.Color.Transparent;
            this.sign_btn.Location = new System.Drawing.Point(37, 539);
            this.sign_btn.Name = "sign_btn";
            this.sign_btn.Size = new System.Drawing.Size(600, 90);
            this.sign_btn.TabIndex = 4;
            this.sign_btn.Text = "Sign Up";
            this.sign_btn.UseVisualStyleBackColor = false;
            // 
            // sign_password
            // 
            this.sign_password.Location = new System.Drawing.Point(37, 331);
            this.sign_password.Multiline = true;
            this.sign_password.Name = "sign_password";
            this.sign_password.PasswordChar = '*';
            this.sign_password.Size = new System.Drawing.Size(600, 50);
            this.sign_password.TabIndex = 3;
            // 
            // sign_username
            // 
            this.sign_username.Location = new System.Drawing.Point(37, 275);
            this.sign_username.Multiline = true;
            this.sign_username.Name = "sign_username";
            this.sign_username.Size = new System.Drawing.Size(600, 50);
            this.sign_username.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Andalus", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(169, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(273, 91);
            this.label2.TabIndex = 1;
            this.label2.Text = "Welcome";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(247, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sign Up";
            // 
            // sign_phone
            // 
            this.sign_phone.Location = new System.Drawing.Point(37, 473);
            this.sign_phone.Multiline = true;
            this.sign_phone.Name = "sign_phone";
            this.sign_phone.Size = new System.Drawing.Size(600, 50);
            this.sign_phone.TabIndex = 6;
            // 
            // sign_confpass
            // 
            this.sign_confpass.Location = new System.Drawing.Point(37, 387);
            this.sign_confpass.Multiline = true;
            this.sign_confpass.Name = "sign_confpass";
            this.sign_confpass.PasswordChar = '*';
            this.sign_confpass.Size = new System.Drawing.Size(600, 50);
            this.sign_confpass.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(229, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 16);
            this.label3.TabIndex = 7;
            this.label3.Text = "_____________________";
            // 
            // sign_role
            // 
            this.sign_role.AccessibleRole = System.Windows.Forms.AccessibleRole.ComboBox;
            this.sign_role.FormattingEnabled = true;
            this.sign_role.Location = new System.Drawing.Point(37, 443);
            this.sign_role.Name = "sign_role";
            this.sign_role.Size = new System.Drawing.Size(600, 24);
            this.sign_role.TabIndex = 8;
            // 
            // SignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 700);
            this.Controls.Add(this.panel1);
            this.Name = "SignForm";
            this.Text = "SignForm";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button sign_btn;
        private System.Windows.Forms.TextBox sign_password;
        private System.Windows.Forms.TextBox sign_username;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox sign_phone;
        private System.Windows.Forms.TextBox sign_confpass;
        private System.Windows.Forms.ComboBox sign_role;
    }
}