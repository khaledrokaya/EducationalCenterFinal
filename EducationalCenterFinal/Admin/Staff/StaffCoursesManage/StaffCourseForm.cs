using EducationalCenterFinal.Admin.CourseManage;
using EducationalCenterFinal.Admin.CreateAccount;
using EducationalCenterFinal.Admin.Dashboard;
using EducationalCenterFinal.Admin.EmployeeManage;
using EducationalCenterFinal.Admin.Staff.StudentManage;
using EducationalCenterFinal.Admin.TeacherManage;
using EducationalCenterFinal.SpecialForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EducationalCenterFinal.Admin.Staff.StaffCoursesManage
{
    public partial class StaffCourseForm : Form
    {
        private int CourseId { get; set; }
        readonly EducationCenterEntities dp = new EducationCenterEntities();

        public StaffCourseForm(int CourseID, string role)
        {
            InitializeComponent();

            this.CourseId = CourseID;

            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height - 70);
            this.WindowState = FormWindowState.Maximized;

            SetupComponents(role);
            ApplyStyling();
            SearchBoxPlaceHolder();
        }

        private void SetupComponents(string role)
        {
            this.questionsToolStripMenuItem.Click += (sender, e) => this.QuestionsToolStripMenuItem_Click(role);
            this.dashboardToolStripMenuItem.Click += (sender, e) => this.DashboardToolStripMenuItem_Click(role);
            this.studentsToolStripMenuItem.Click += (sender, e) => this.StudentsToolStripMenuItem_Click(role);
            this.dataGridView1.RowHeaderMouseDoubleClick += (sender, e) => DataGridView1_CellContentClick((int)dataGridView1.Rows[e.RowIndex].Cells[0].Value);

            if (role == "staff")
            {
                teachersToolStripMenuItem.Enabled = false;
                coursesToolStripMenuItem.Enabled = false;
                forgetPasswordToolStripMenuItem.Enabled = false;
                createAccountToolStripMenuItem.Enabled = false;
                employeesToolStripMenuItem.Enabled = false;
                dashboardToolStripMenuItem.Enabled = false;
            }

            foreach (var course in dp.courses)
            {
                ToolStripMenuItem courseMenuItem = new ToolStripMenuItem
                {
                    Name = $"{course.courseName}ToolStripMenuItem",
                    Size = new System.Drawing.Size(134, 30),
                    Text = course.courseName,
                    Enabled = CourseId != course.courseId
                };
                courseMenuItem.Click += (sender, e) => CourseMenuItem_Click(course.courseId, role);
                manageToolStripMenuItem.DropDownItems.Add(courseMenuItem);
            }
            
            PictureBox pictureBox = new PictureBox
            {
                Image = Image.FromFile(Application.StartupPath.Substring(0, 42) + "\\Admin\\Staff\\StaffCoursesManage\\search-interface-symbol.png"),
                SizeMode = PictureBoxSizeMode.Normal,
                Location = new Point(270, 13),
                Size = new Size(50, 50)
            };
            panel1.Controls.Add(pictureBox);
            LoadStudentData();

            label1.Text = dp.courses.Where(e => e.courseId == CourseId).Select(s => s.courseName).First() + " Course";

            Dictionary<string, Action> buttons= new Dictionary<string, Action>()
            {
                { "Assign", AssignButton_Click },
                { "Remove", RemoveButton_Click },
                { "Pay", PayButton_Click },
            };
            var idx = 0;
            foreach (var item in buttons)
            {
                Button button = new Button
                {
                    Text = item.Key,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand,
                    BackColor = Color.FromArgb(118, 41, 81),
                    ForeColor = Color.White,
                    Location = new Point(ClientSize.Width - panel1.Width - 150 - idx, ClientSize.Height - (ClientSize.Height - 100) - 12 - 47),
                    Size = new Size(120, 35),
                    Font = new Font("Arial", 12, FontStyle.Bold),
                    Anchor = AnchorStyles.Top | AnchorStyles.Right,
                };
                button.Click += (sender, e) => item.Value();
                Controls.Add(button);
                idx += 130;
            }
        }

        private void LoadStudentData(int? filterId = null)
        {
            var studentsInCourse = dp.students
                .Join(dp.enrollments,
                    student => student.studentId,
                    enrollment => enrollment.studentId,
                    (student, enrollment) => new { student, enrollment })
                .Where(result => result.enrollment.courseId == CourseId)
                .Select(result => new
                {
                    ID = result.student.studentId,
                    Name = result.student.studentName,
                    Phone = result.student.studentPhone,
                    Address = result.student.studentAddress,
                    Email = result.student.studentEmail,
                });
            if (filterId.HasValue)
            {
                studentsInCourse = studentsInCourse.Where(s => s.ID == filterId.Value);
            }

            dataGridView1.DataSource = studentsInCourse.ToList();
        }

        private void ApplyStyling()
        {
            dataGridView1.Size = new Size(ClientSize.Width - 24, ClientSize.Height - 100);
            dataGridView1.MinimumSize = new Size(ClientSize.Width - 24, ClientSize.Height - 100);
            dataGridView1.MaximumSize = new Size(ClientSize.Width - 24, ClientSize.Height - 100);
            dataGridView1.Location = new Point(12, ClientSize.Height - 12 - dataGridView1.Height);

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.DefaultCellStyle.SelectionBackColor = Color.FromArgb(236, 236, 236);
                column.DefaultCellStyle.SelectionForeColor = Color.Black;
                column.DefaultCellStyle.Font = new Font("Arial", 11F, FontStyle.Regular);
                column.Width = column.HeaderText == "ID" ? 40 : ((dataGridView1.Width - dataGridView1.RowHeadersWidth - 40) / 4);
                column.HeaderCell.Style.Alignment = column.HeaderText == "ID" ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft;
                column.DefaultCellStyle.Alignment = column.HeaderText == "ID" ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft;
            }

            panel1.Location = new Point(ClientSize.Width - 12 - panel1.Width, ClientSize.Height - dataGridView1.Height - 12 - 50);
        }

        private void SearchBoxPlaceHolder()
        {
            searchBox.Text = "Search By ID...";
            searchBox.ForeColor = Color.Gray;
            searchBox.Enter += (sender, e) =>
            {
                if (searchBox.Text == "Search By ID...")
                {
                    searchBox.Text = "";
                    searchBox.ForeColor = Color.Black;
                }
            };
            searchBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(searchBox.Text))
                {
                    searchBox.Text = "Search By ID...";
                    searchBox.ForeColor = Color.Gray;
                }
            };
        }

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

        private void StudentsToolStripMenuItem_Click(string role)
        {
            new StudentManageForm(role).Show();
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

        private void SearchBox_TextChanged_1(object sender, EventArgs e)
        {
            if (int.TryParse(searchBox.Text, out int id))
            {
                LoadStudentData(id);
            }
            else
            {
                LoadStudentData();
            }
        }

        private void AssignButton_Click()
        {
            var Form = new CourseManageForms(dp, CourseId, "assign");
            Form.Show();
            Form.FormClosed += (sender, e) => LoadStudentData();
        }

        private void RemoveButton_Click()
        {
            var Form = new CourseManageForms(dp, CourseId, "remove");
            Form.Show();
            Form.FormClosed += (sender, e) => LoadStudentData();
        }

        private void PayButton_Click()
        {
            var Form = new CourseManageForms(dp, CourseId, "pay");
            Form.Show();
            Form.FormClosed += (sender, e) => LoadStudentData();
        }

        private void DataGridView1_CellContentClick(int stu)
        {
            new ShowStudentDataForm(dp.students.Where(s=> s.studentId == stu).FirstOrDefault(), CourseId).Show();
        }

        private void ForgetPasswordToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            new ForgotPassword(dp).Show();
        }
    }
}