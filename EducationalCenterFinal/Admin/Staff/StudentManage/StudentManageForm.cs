using EducationalCenterFinal.Admin.CourseManage;
using EducationalCenterFinal.Admin.CreateAccount;
using EducationalCenterFinal.Admin.Dashboard;
using EducationalCenterFinal.Admin.EmployeeManage;
using EducationalCenterFinal.Admin.Staff.StaffCoursesManage;
using EducationalCenterFinal.Admin.TeacherManage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EducationalCenterFinal.Admin.Staff.StudentManage
{
    public partial class StudentManageForm : Form
    {
        readonly EducationCenterEntities dp = new EducationCenterEntities();
        student s = new student();
       
        public StudentManageForm(string role)
        {
            InitializeComponent();
            setUpForm();
            setUpComponents();
            //ApplyStyling();

            //Maximize window
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.MaximizeBox = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;


            this.questionsToolStripMenuItem.Click += (sender, e) => this.QuestionsToolStripMenuItem_Click(role);
            this.dashboardToolStripMenuItem.Click += (sender, e) => this.DashboardToolStripMenuItem_Click(role);

            //Disable Admin Sections
            if (role == "staff")
            {
                teachersToolStripMenuItem.Enabled = false;
                coursesToolStripMenuItem.Enabled = false;
                forgetPasswordToolStripMenuItem.Enabled = false;
                createAccountToolStripMenuItem.Enabled = false;
                employeesToolStripMenuItem.Enabled = false;
                dashboardToolStripMenuItem.Enabled = false;
            }

            //Make Manage Course MenuItems
            foreach (var course in dp.courses)
            {
                ToolStripMenuItem courseMenuItem = new ToolStripMenuItem
                {
                    Name = $"{course.courseName}ToolStripMenuItem",
                    Size = new System.Drawing.Size(134, 26),
                    Text = course.courseName,
                };
                courseMenuItem.Click += (sender, e) => CourseMenuItem_Click(course.courseId, role);
                manageToolStripMenuItem.DropDownItems.Add(courseMenuItem);
            }


            //Editing the DataGrideView HeaderText and Hiding 4 Columns (attendences , enrollment , exams , payments )
            dgvStudent.DataSource= dp.students.ToList();
            var columnHeaders = new Dictionary<string, string>
            {
                { "studentId", "ID" },
               { "studentName", "Name" },
               { "studentEmail", "Email" },
               { "studentPhone", "Phone" },
                { "studentAddress", "Address" }
            };

            foreach (var column in columnHeaders)
            {
                if (dgvStudent.Columns[column.Key] != null)
                {
                    dgvStudent.Columns[column.Key].HeaderText = column.Value;
                }
            }

            string[] hiddenColumns = { "attendances", "enrollments", "exams", "payments" };

            foreach (var columnName in hiddenColumns)
            {
                if (dgvStudent.Columns[columnName] != null)
                {
                    dgvStudent.Columns[columnName].Visible = false;
                }
            }

        }
        private void setUpForm()
        {

            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.MaximizeBox = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }
        private void setUpComponents()
        {
            //search
            txtSearch.Size = new Size(((ClientSize.Width - 440) * 1 / 4), 40);
            txtSearch.Location = new Point(ClientSize.Width - 320, 35);

            // DataGridView 
            dgvStudent.Size = new Size(((ClientSize.Width - 44) * 3 / 4), ClientSize.Height - 120);
            dgvStudent.Location = new Point(20, 90);

            // panel 
            panel1.Size = new Size(((ClientSize.Width - 250) * 1 / 4), ClientSize.Height - 120);
            panel1.Location = new Point(ClientSize.Width - 340, 90);

            // Labels and TextBoxes 
            label5.Size = new Size(((ClientSize.Width - 300) * 1 / 4), ClientSize.Height - 600);
            label5.Location = new Point(20, 35);

            
            label1.Location = new Point(20, 40);
            label1.Size = new Size(panel1.Width - 120, panel1.Height / 8);
            txtName.Location = new Point(20, label2.Height + 60);
            txtName.Size = new Size(panel1.Width - 55, panel1.Height / 18);

            label2.Location = new Point(20, 130);
            label2.Size = new Size(panel1.Width - 120, panel1.Height / 8);
            txtEmail.Location = new Point(20, 175);
            txtEmail.Size = new Size(panel1.Width - 55, panel1.Height / 18);

            label3.Location = new Point(20, 230);
            label3.Size = new Size(panel1.Width - 120, panel1.Height / 8);
            txtAddress.Location = new Point(20, 275);
            txtAddress.Size = new Size(panel1.Width - 55, panel1.Height / 18);

            label4.Location = new Point(20, 330);
            label4.Size = new Size(panel1.Width - 120, panel1.Height / 8);
            txtPhone.Location = new Point(20, 375);
            txtPhone.Size = new Size(panel1.Width - 55, panel1.Height / 18);

            
            // Buttons 
            addBtn.Location = new Point(20, 500);
            btnEdit.Location = new Point(170, 500);
            resetBtn.Location = new Point(20, 600);
            btnDelete.Location = new Point(170, 600);

            addBtn.Size = new Size(panel1.Width - 200, panel1.Height / 14);
            btnEdit.Size = new Size(panel1.Width - 200, panel1.Height / 14);
            resetBtn.Size = new Size(panel1.Width - 200, panel1.Height / 14);
            btnDelete.Size = new Size(panel1.Width - 200, panel1.Height / 14);
        }
        private void StudentManageForm_Load(object sender, EventArgs e)
        {

            //setting placeholder for searching textBox
            SetPlaceholder();

            //custmize column Headers Back Color , color , font 
            dgvStudent.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(118, 41, 82);
            dgvStudent.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvStudent.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 12, FontStyle.Bold); 

            // Ensure the header style applies
             dgvStudent.EnableHeadersVisualStyles = false;

            // Other DataGridView configurations, if needed
            dgvStudent.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudent.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvStudent.ReadOnly = true; //  setting for readonly cells
        }

        //private void ApplyStyling()
        //{

        //    foreach (DataGridViewColumn column in dgvStudent.Columns)
        //    {
        //        column.DefaultCellStyle.SelectionBackColor = Color.FromArgb(236, 236, 236);
        //        column.DefaultCellStyle.SelectionForeColor = Color.Black;
        //        column.DefaultCellStyle.Font = new Font("Arial", 11F, FontStyle.Regular);
        //        column.Width = column.HeaderText == "ID" ? 20 : ((dgvStudent.Width - dgvStudent.RowHeadersWidth - 40) / 3);
        //        column.HeaderCell.Style.Alignment = column.HeaderText == "ID" ? DataGridViewContentAlignment.MiddleCenter :  DataGridViewContentAlignment.MiddleLeft;
        //        column.DefaultCellStyle.Alignment = column.HeaderText == "ID" ? DataGridViewContentAlignment.MiddleCenter :  DataGridViewContentAlignment.MiddleLeft;
        //    }
        //}


        private void CourseMenuItem_Click(int CourseId, string role)
        {
            new StaffCourseForm(CourseId, role).Show();
            this.Hide();
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Hide();
        }

        private void ForgetPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void CreateAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CreateAccountForm().Show();
            this.Hide();
        }
        private void TeachersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TeacherManageForm().Show();
            this.Hide();
        }

        private void EmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new EmployeeManageForm().Show();
            this.Hide();
        }

        private void CoursesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CourseManageForm().Show();
            this.Hide();
        }

        private void QuestionsToolStripMenuItem_Click(string role)
        {
            new QuestionsForm(role).Show();
            this.Hide();
        }

        private void DashboardToolStripMenuItem_Click(string role)
        {
            new DashboardForm(role).Show();
            this.Hide();
        }

        private void SetPlaceholder()
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Search By ID...";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void txtSearch_GotFocus(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search By ID...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }

        }

        private void txtSearch_LostFocus(object sender, EventArgs e)
        {
            SetPlaceholder(); // Restore placeholder if textbox is empty

        }

        private void Search()
        {

            

            if (int.TryParse(txtSearch.Text, out int id))
            {
                student s = dp.students.FirstOrDefault(x => x.studentId == id);

                if (s != null)
                {
                    txtName.Text = s.studentName;
                    txtEmail.Text = s.studentEmail;
                    txtAddress.Text = s.studentAddress;
                    txtPhone.Text = s.studentPhone;
                    txtSearch.Enabled = false;
                    addBtn.Enabled = false;

                }
                else
                {
                    MessageBox.Show("Student Not Found");
                }



            }
            else
            {
                MessageBox.Show("Enter a Valid Student Id");
            }

        }

        //Searching after clicking Enter
        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                e.SuppressKeyPress = true;

                Search();
            }
        }

        private void addBtn_Click_1(object sender, EventArgs e)
        {

          if (txtName.Text == "" || txtPhone.Text == "" || txtEmail.Text == "" || txtAddress.Text == "")
          {
                MessageBox.Show("Missing Information");
          }
            else
            {
                addStudent();

                ClearStudentTextBox();

            }
        }


        private void addStudent()
        {
            s.studentName = txtName.Text;
            s.studentEmail = txtEmail.Text;
            s.studentAddress = txtAddress.Text;
            s.studentPhone = txtPhone.Text;
            dp.students.Add(s);
            dp.SaveChanges();
            //dgvStudent.DataSource = db.Students.ToList();
            MessageBox.Show("Student Saved Successfully");
        }

        private void ClearStudentTextBox()
        {

            txtName.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";

        }
        private void Reset()
        {
            if (txtSearch.Text != "Search By ID...")
            {
                txtSearch.Enabled = true;
                addBtn.Enabled=true;
                txtSearch.Text = "";
                SetPlaceholder();

            }
            ClearStudentTextBox();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSearch.Text, out int id))
            {

                student s = dp.students.FirstOrDefault(x => x.studentId == id);
                s.studentName = txtName.Text;
                s.studentEmail = txtEmail.Text;
                s.studentAddress = txtAddress.Text;
                s.studentPhone = txtPhone.Text;

                dp.SaveChanges();
                MessageBox.Show("Data Edited Successfully");
                //dgvStudent.DataSource = db.Students.ToList();
                Reset();


            }
            else
            {
                MessageBox.Show("No Data to Edit");
            }
        }

        private void resetBtn_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSearch.Text, out int id))
            {

                student s = dp.students.FirstOrDefault(x => x.studentId == id);
                dp.students.Remove(s);
                dp.SaveChanges();
                MessageBox.Show("Data Deleted Successfully");
                //dgvStudent.DataSource = db.Students.ToList();
                Reset();


            }
            else
            {
                MessageBox.Show("No Data to Delete");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
