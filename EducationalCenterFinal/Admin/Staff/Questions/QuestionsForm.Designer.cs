using System.Windows.Forms;

namespace EducationalCenterFinal.Admin.Staff
{
    partial class QuestionsForm
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
            this.components = new System.ComponentModel.Container();
            this.QuestionsLstBox = new System.Windows.Forms.ListBox();
            this.questionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.open_Source_Education_CenterDataSet = new EducationalCenterFinal.Open_Source_Education_CenterDataSet();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SelectedQuestionLbl = new System.Windows.Forms.RichTextBox();
            this.AnswerTxtBox = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SubmitAnswerBtn = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dashboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.teachersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.studentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.employeesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coursesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.questionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.accountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forgetPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.questionTableAdapter = new EducationalCenterFinal.Open_Source_Education_CenterDataSetTableAdapters.questionTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.questionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.open_Source_Education_CenterDataSet)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // QuestionsLstBox
            // 
            this.QuestionsLstBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.QuestionsLstBox.DataSource = this.questionBindingSource;
            this.QuestionsLstBox.DisplayMember = "questionAnswer";
            this.QuestionsLstBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuestionsLstBox.FormattingEnabled = true;
            this.QuestionsLstBox.ItemHeight = 31;
            this.QuestionsLstBox.Location = new System.Drawing.Point(747, 80);
            this.QuestionsLstBox.Name = "QuestionsLstBox";
            this.QuestionsLstBox.Size = new System.Drawing.Size(645, 560);
            this.QuestionsLstBox.TabIndex = 1;
            this.QuestionsLstBox.ValueMember = "questionAnswer";
            this.QuestionsLstBox.SelectedIndexChanged += new System.EventHandler(this.QuestionsLstBox_SelectedIndexChanged);
            // 
            // questionBindingSource
            // 
            this.questionBindingSource.DataMember = "question";
            this.questionBindingSource.DataSource = this.open_Source_Education_CenterDataSet;
            // 
            // open_Source_Education_CenterDataSet
            // 
            this.open_Source_Education_CenterDataSet.DataSetName = "Open_Source_Education_CenterDataSet";
            this.open_Source_Education_CenterDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(8, 35);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(733, 654);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // SelectedQuestionLbl
            // 
            this.SelectedQuestionLbl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SelectedQuestionLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SelectedQuestionLbl.Location = new System.Drawing.Point(14, 76);
            this.SelectedQuestionLbl.Name = "SelectedQuestionLbl";
            this.SelectedQuestionLbl.Size = new System.Drawing.Size(658, 236);
            this.SelectedQuestionLbl.TabIndex = 3;
            this.SelectedQuestionLbl.Text = "";
            // 
            // AnswerTxtBox
            // 
            this.AnswerTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AnswerTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AnswerTxtBox.Location = new System.Drawing.Point(14, 369);
            this.AnswerTxtBox.Name = "AnswerTxtBox";
            this.AnswerTxtBox.Size = new System.Drawing.Size(658, 236);
            this.AnswerTxtBox.TabIndex = 4;
            this.AnswerTxtBox.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.label2.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 28);
            this.label2.TabIndex = 5;
            this.label2.Text = "Question:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.label3.Font = new System.Drawing.Font("Tahoma", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(14, 338);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 28);
            this.label3.TabIndex = 6;
            this.label3.Text = "Answer:";
            // 
            // SubmitAnswerBtn
            // 
            this.SubmitAnswerBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(41)))), ((int)(((byte)(84)))));
            this.SubmitAnswerBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SubmitAnswerBtn.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SubmitAnswerBtn.ForeColor = System.Drawing.Color.White;
            this.SubmitAnswerBtn.Location = new System.Drawing.Point(219, 618);
            this.SubmitAnswerBtn.Name = "SubmitAnswerBtn";
            this.SubmitAnswerBtn.Size = new System.Drawing.Size(183, 44);
            this.SubmitAnswerBtn.TabIndex = 7;
            this.SubmitAnswerBtn.Text = "Submit";
            this.SubmitAnswerBtn.UseVisualStyleBackColor = true;
            this.SubmitAnswerBtn.Click += new System.EventHandler(this.SubmitAnswerBtn_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dashboardToolStripMenuItem,
            this.teachersToolStripMenuItem,
            this.studentsToolStripMenuItem,
            this.employeesToolStripMenuItem,
            this.coursesToolStripMenuItem,
            this.manageToolStripMenuItem,
            this.questionsToolStripMenuItem,
            this.accountToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1406, 30);
            this.menuStrip1.TabIndex = 8;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dashboardToolStripMenuItem
            // 
            this.dashboardToolStripMenuItem.Name = "dashboardToolStripMenuItem";
            this.dashboardToolStripMenuItem.Size = new System.Drawing.Size(112, 26);
            this.dashboardToolStripMenuItem.Text = "Dashboard";
            // 
            // teachersToolStripMenuItem
            // 
            this.teachersToolStripMenuItem.Name = "teachersToolStripMenuItem";
            this.teachersToolStripMenuItem.Size = new System.Drawing.Size(91, 26);
            this.teachersToolStripMenuItem.Text = "Teacher";
            this.teachersToolStripMenuItem.Click += new System.EventHandler(this.TeachersToolStripMenuItem_Click);
            // 
            // studentsToolStripMenuItem
            // 
            this.studentsToolStripMenuItem.Name = "studentsToolStripMenuItem";
            this.studentsToolStripMenuItem.Size = new System.Drawing.Size(86, 26);
            this.studentsToolStripMenuItem.Text = "Student";
            // 
            // employeesToolStripMenuItem
            // 
            this.employeesToolStripMenuItem.Name = "employeesToolStripMenuItem";
            this.employeesToolStripMenuItem.Size = new System.Drawing.Size(103, 26);
            this.employeesToolStripMenuItem.Text = "Employee";
            this.employeesToolStripMenuItem.Click += new System.EventHandler(this.EmployeesToolStripMenuItem_Click);
            // 
            // coursesToolStripMenuItem
            // 
            this.coursesToolStripMenuItem.Name = "coursesToolStripMenuItem";
            this.coursesToolStripMenuItem.Size = new System.Drawing.Size(82, 26);
            this.coursesToolStripMenuItem.Text = "Course";
            this.coursesToolStripMenuItem.Click += new System.EventHandler(this.CoursesToolStripMenuItem_Click);
            // 
            // manageToolStripMenuItem
            // 
            this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
            this.manageToolStripMenuItem.Size = new System.Drawing.Size(88, 26);
            this.manageToolStripMenuItem.Text = "Manage";
            // 
            // questionsToolStripMenuItem
            // 
            this.questionsToolStripMenuItem.Name = "questionsToolStripMenuItem";
            this.questionsToolStripMenuItem.Size = new System.Drawing.Size(105, 26);
            this.questionsToolStripMenuItem.Text = "Questions";
            // 
            // accountToolStripMenuItem
            // 
            this.accountToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createAccountToolStripMenuItem,
            this.forgetPasswordToolStripMenuItem,
            this.logoutToolStripMenuItem});
            this.accountToolStripMenuItem.Name = "accountToolStripMenuItem";
            this.accountToolStripMenuItem.Size = new System.Drawing.Size(89, 26);
            this.accountToolStripMenuItem.Text = "Account";
            // 
            // createAccountToolStripMenuItem
            // 
            this.createAccountToolStripMenuItem.Name = "createAccountToolStripMenuItem";
            this.createAccountToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.createAccountToolStripMenuItem.Text = "Create Account";
            this.createAccountToolStripMenuItem.Click += new System.EventHandler(this.CreateAccountToolStripMenuItem_Click);
            // 
            // forgetPasswordToolStripMenuItem
            // 
            this.forgetPasswordToolStripMenuItem.Name = "forgetPasswordToolStripMenuItem";
            this.forgetPasswordToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.forgetPasswordToolStripMenuItem.Text = "Forget Password";
            this.forgetPasswordToolStripMenuItem.Click += new System.EventHandler(this.ForgetPasswordToolStripMenuItem_Click);
            // 
            // logoutToolStripMenuItem
            // 
            this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
            this.logoutToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.logoutToolStripMenuItem.Text = "Logout";
            this.logoutToolStripMenuItem.Click += new System.EventHandler(this.LogoutToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Location = new System.Drawing.Point(818, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(506, 38);
            this.panel1.TabIndex = 9;
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(41)))), ((int)(((byte)(84)))));
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.ForeColor = System.Drawing.Color.White;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Not Answered",
            "Answered"});
            this.comboBox1.Location = new System.Drawing.Point(0, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(506, 39);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // questionTableAdapter
            // 
            this.questionTableAdapter.ClearBeforeFill = true;
            // 
            // QuestionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.ClientSize = new System.Drawing.Size(1406, 701);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.SubmitAnswerBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.AnswerTxtBox);
            this.Controls.Add(this.SelectedQuestionLbl);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.QuestionsLstBox);
            this.Name = "QuestionsForm";
            this.Text = "QuestionsForm";
            this.Load += new System.EventHandler(this.QuestionsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.questionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.open_Source_Education_CenterDataSet)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ListBox QuestionsLstBox;
        private RichTextBox richTextBox1;
        private RichTextBox SelectedQuestionLbl;
        private RichTextBox AnswerTxtBox;
        private Label label2;
        private Label label3;
        private Button SubmitAnswerBtn;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem dashboardToolStripMenuItem;
        private ToolStripMenuItem teachersToolStripMenuItem;
        private ToolStripMenuItem studentsToolStripMenuItem;
        private ToolStripMenuItem employeesToolStripMenuItem;
        private ToolStripMenuItem coursesToolStripMenuItem;
        private ToolStripMenuItem manageToolStripMenuItem;
        private ToolStripMenuItem questionsToolStripMenuItem;
        private ToolStripMenuItem accountToolStripMenuItem;
        private ToolStripMenuItem createAccountToolStripMenuItem;
        private ToolStripMenuItem forgetPasswordToolStripMenuItem;
        private ToolStripMenuItem logoutToolStripMenuItem;
        private Panel panel1;
        private ComboBox comboBox1;
        private Open_Source_Education_CenterDataSet open_Source_Education_CenterDataSet;
        private BindingSource questionBindingSource;
        private Open_Source_Education_CenterDataSetTableAdapters.questionTableAdapter questionTableAdapter;
    }
}