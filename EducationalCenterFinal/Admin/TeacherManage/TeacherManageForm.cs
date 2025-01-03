﻿using EducationalCenterFinal.Admin.CourseManage;
using EducationalCenterFinal.Admin.CreateAccount;
using EducationalCenterFinal.Admin.EmployeeManage;
using EducationalCenterFinal.Admin.Staff;
using EducationalCenterFinal.Admin.Staff.StaffCoursesManage;
using EducationalCenterFinal.Admin.Staff.StudentManage;
using EducationalCenterFinal.SpecialForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace EducationalCenterFinal.Admin.TeacherManage
{
    public partial class TeacherManageForm : Form
    {
        readonly EducationCenterEntities dp = new EducationCenterEntities();
        public TeacherManageForm()
        {
            InitializeComponent();
            LoadTeacherData();
            setUpForm();
            setUpComponents();
            SearchPlaceHolder();
            styleDataGridView();

            this.questionsToolStripMenuItem.Click += (sender, e) => this.QuestionsToolStripMenuItem_Click("admin");
            this.studentsToolStripMenuItem.Click += (sender, e) => this.StudentsToolStripMenuItem_Click("admin");

            //Make Manage Course MenuItems
            foreach (var course in dp.courses)
            {
                ToolStripMenuItem courseMenuItem = new ToolStripMenuItem
                {
                    Name = $"{course.courseName}ToolStripMenuItem",
                    Size = new System.Drawing.Size(134, 26),
                    Text = course.courseName,
                };
                courseMenuItem.Click += (sender, e) => CourseMenuItem_Click(course.courseId, "admin");
                manageToolStripMenuItem.DropDownItems.Add(courseMenuItem);
            }
            PictureBox pictureBox = new PictureBox
            {
                Image = Image.FromFile(Application.StartupPath.Remove(Application.StartupPath.Length - 10) + "\\Images\\search-interface-symbol.png"),
                SizeMode = PictureBoxSizeMode.Normal,
                Location = new Point(270, 13),
                Size = new Size(20, 20)
            };
            textBox1_search.Controls.Add(pictureBox);
        }
        
        private void styleDataGridView()
        {
            dataGridView1.DataSource = dp.teachers.ToList();

            dataGridView1.ScrollBars = ScrollBars.Both;

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(118, 41, 82);

            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 13F, System.Drawing.FontStyle.Bold);

            dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);

            this.dataGridView1.ColumnHeadersHeight = 40;

            dataGridView1.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;

            dataGridView1.GridColor = Color.Black;

            this.dataGridView1.RowTemplate.Height = 55;

            dataGridView1.DefaultCellStyle.ForeColor = Color.Black; 

            dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);

            //لتغيير اسماء الاعمدة عن الا موجودة في DataBase
            var columnHeaders = new Dictionary<string, string>
            {
                { "teacherId","ID" },

                { "userId", "U_ID" },

                { "teacherName", "Name" },

                { "teacherSpecialization", "Specialization" },

                { "teacherEmail", "Email" },

                { "teacherPhone", "Phone" },

                
            };

            foreach (var column in columnHeaders)
            {
                if (dataGridView1.Columns[column.Key] != null)
                {
                    dataGridView1.Columns[column.Key].HeaderText = column.Value;
                }
            }

            string[] hiddenColumns = { "courses", "users" };

            // لوب لإخفاء الأعمدة
            foreach (var columnName in hiddenColumns)
            {
                if (dataGridView1.Columns[columnName] != null)
                {
                    dataGridView1.Columns[columnName].Visible = false;
                }
            }

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column != null)
                {

                    column.DefaultCellStyle.SelectionBackColor = Color.FromArgb(236, 236, 236);

                    column.DefaultCellStyle.SelectionForeColor = Color.Black;

                    dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(236, 236, 236);

                    dataGridView1.RowHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

                    column.DefaultCellStyle.Font = new Font("Arial", 11F, FontStyle.Regular);

                    column.Width = column.HeaderText == "ID" ? 50 : column.HeaderText == "U_ID" ? 90: ((dataGridView1.Width - dataGridView1.RowHeadersWidth - 40) / 3);

                    column.HeaderCell.Style.Alignment = column.HeaderText == "ID" ? DataGridViewContentAlignment.MiddleCenter : column.HeaderText == "U_ID" ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft;

                    column.DefaultCellStyle.Alignment = column.HeaderText == "ID" ? DataGridViewContentAlignment.MiddleCenter : column.HeaderText == "U_ID" ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft;
                }
            }
        }
        private void LoadTeacherData(int? filterId = null)
        {
            var teachers = dp.teachers.Select(t => new {

                ID = t.teacherId,

                U_ID = t.userId,

                Name = t.teacherName,

                Specialization = t.teacherSpecialization,

                Email = t.teacherEmail,

                Phone = t.teacherPhone,
            });
            if (filterId.HasValue)
            {
                teachers = teachers.Where(t => t.ID == filterId.Value);
            }
            

            dataGridView1.DataSource = teachers.ToList();

            styleDataGridView();
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
            textBox1_search.Size = new Size(((ClientSize.Width - 360) * 1 / 4), 50);
            textBox1_search.Location = new Point(ClientSize.Width - 320, 20);

            // DataGridView 
            dataGridView1.Size = new Size(((ClientSize.Width - 44) * 3 / 4), ClientSize.Height - 150);
            dataGridView1.Location = new Point(20, 80);

            // panel 
            panel1.Size = new Size(((ClientSize.Width - 250) * 1 / 4), ClientSize.Height - 150);
            panel1.Location = new Point(ClientSize.Width - 340, 80);

            // Labels and TextBoxes 
            label1.Size = new Size(((ClientSize.Width - 300) * 1 / 4), ClientSize.Height - 600);
            label1.Location = new Point(20, 25);

            label2.Location = new Point(20, 30);
            label2.Size = new Size(panel1.Width - 120, panel1.Height / 8);
            textBox1.Location = new Point(20, label2.Height + 40);
            textBox1.Size = new Size(panel1.Width - 55, panel1.Height / 18);

            label3.Location = new Point(20, 120);
            label3.Size = new Size(panel1.Width - 120, panel1.Height / 8);
            textBox2.Location = new Point(20, 155);
            textBox2.Size = new Size(panel1.Width - 55, panel1.Height / 18);

            label4.Location = new Point(20, 210);
            label4.Size = new Size(panel1.Width - 120, panel1.Height / 8);
            textBox3.Location = new Point(20, 245);
            textBox3.Size = new Size(panel1.Width - 55, panel1.Height / 18);

            label5.Location = new Point(20, 300);
            label5.Size = new Size(panel1.Width - 120, panel1.Height / 8);
            textBox4.Location = new Point(20, 335);
            textBox4.Size = new Size(panel1.Width - 55, panel1.Height / 18);

            label6.Location = new Point(20, 390);
            label6.Size = new Size(panel1.Width - 120, panel1.Height / 8);
            textBox5.Location = new Point(20, 425);
            textBox5.Size = new Size(panel1.Width - 55, panel1.Height / 18);

            // Buttons 
            button1.Location = new Point(20, 500);
            button2.Location = new Point(170, 500);
            button3.Location = new Point(20, 600);
            button4.Location = new Point(170, 600);

            button1.Size = new Size(panel1.Width - 200, panel1.Height / 14);
            button2.Size = new Size(panel1.Width - 200, panel1.Height / 14);
            button3.Size = new Size(panel1.Width - 200, panel1.Height / 14);
            button4.Size = new Size(panel1.Width - 200, panel1.Height / 14);

        }
        private void CourseMenuItem_Click(int CourseId, string role)
        {
            new StaffCourseForm(CourseId, role).Show();
            this.Hide();
        }

        private void EmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new EmployeeManageForm().Show();
            this.Hide();
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Hide();
        }

        private void ForgetPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ForgotPassword(dp).Show();
        }

        private void CreateAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CreateAccountForm().Show();
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

        private void StudentsToolStripMenuItem_Click(string role)
        {
            new StudentManageForm(role).Show();
            this.Hide();
        }

        private void SearchPlaceHolder()
        {
            textBox1_search.Text = "Search By ID...";

            textBox1_search.ForeColor = Color.Gray;

            textBox1_search.Enter += (sender, e) =>
            {
                if (textBox1_search.Text == "Search By ID...")
                {
                    textBox1_search.Text = "";

                    textBox1_search.ForeColor = Color.Black;
                }
            };
            textBox1_search.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox1_search.Text))
                {
                    textBox1_search.Text = "Search By ID...";

                    textBox1_search.ForeColor = Color.Gray;
                }
            };
            textBox1_search.TextChanged += (sender, e) =>
            {
                if (textBox1_search.Text != "Search By ID..." && textBox1_search.ForeColor == Color.Gray)
                {
                    textBox1_search.ForeColor = Color.Black;
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == ""||textBox5.Text=="")
            {
                MessageBox.Show("Enter Data First.");
                return;
            }
            addData();

        }

        private void addData()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox5.Text) ||
                    string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                if (!int.TryParse(textBox5.Text.Trim(), out int userId))
                {
                    MessageBox.Show("Invalid User ID. It must be an integer.");
                    return;
                }

                var existingTeacher = dp.teachers.FirstOrDefault(t =>
                    t.userId == userId || t.teacherEmail == textBox2.Text.Trim());

                if (existingTeacher != null)
                {
                    MessageBox.Show("A teacher with this User ID or Email already exists.");
                    return;
                }

                string phoneNumber = textBox4.Text.Trim();
                if (phoneNumber.Length != 11 || !phoneNumber.All(char.IsDigit))
                {
                    MessageBox.Show("Invalid Phone Number. It must contain exactly 11 digits.");
                    return;
                }

                if (!Regex.IsMatch(textBox2.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("Invalid Email format.");
                    return;
                }

                teachers t_add = new teachers
                {
                    userId = userId,
                    teacherName = textBox1.Text.Trim(),
                    teacherSpecialization = textBox3.Text.Trim(),
                    teacherEmail = textBox2.Text.Trim(),
                    teacherPhone = phoneNumber
                };

                dp.teachers.Add(t_add);
                dp.SaveChanges();

                MessageBox.Show("Teacher saved successfully.");

                LoadTeacherData();
                dataGridView1.DataSource = dp.teachers.ToList();

                resetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void resetForm()
        {
            textBox1.Text = "";

            textBox2.Text = "";

            textBox3.Text = "";

            textBox4.Text = "";

            textBox5.Text = "";

            textBox1_search.Text = "";

            if (textBox1_search.Text == "")
            {
                textBox1_search.Text = "Search By ID...";

                textBox1_search.ForeColor = Color.Gray;
            }
            button1.Enabled = true;
        }

        private void TeacherManageForm_Load_1(object sender, EventArgs e)
        {
            
            this.ActiveControl = label1;

            SearchPlaceHolder();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(textBox5.Text.Trim(), out int userId))
                {
                    MessageBox.Show("Invalid User ID. It must be an integer.");
                    return;
                }

                teachers t = dp.teachers.FirstOrDefault(x => x.userId == userId);
                if (t == null)
                {
                    MessageBox.Show("Teacher not found.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox1.Text) ||
                    string.IsNullOrWhiteSpace(textBox3.Text) ||
                    string.IsNullOrWhiteSpace(textBox2.Text) ||
                    string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    MessageBox.Show("Please fill in all fields.");
                    return;
                }

                string phoneNumber = textBox4.Text.Trim();
                if (phoneNumber.Length != 11 || !phoneNumber.All(char.IsDigit))
                {
                    MessageBox.Show("Invalid Phone Number. It must contain exactly 11 digits.");
                    return;
                }

                string email = textBox2.Text.Trim();
                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    MessageBox.Show("Invalid Email format.");
                    return;
                }

                var existingTeacher = dp.teachers
                    .FirstOrDefault(x => x.teacherEmail == email && x.userId != userId);

                if (existingTeacher != null)
                {
                    MessageBox.Show("A teacher with this email already exists.");
                    return;
                }

                t.teacherName = textBox1.Text.Trim();
                t.teacherSpecialization = textBox3.Text.Trim();
                t.teacherEmail = email;
                t.teacherPhone = phoneNumber;

                dp.SaveChanges();

                MessageBox.Show("Teacher data has been updated successfully.");

                LoadTeacherData();
                dataGridView1.DataSource = dp.teachers.ToList();
                resetForm();

                button1.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void textBox1_search_TextChanged(object sender, EventArgs e)
        {
            //Search With TeacherID
            string searchTerm = textBox1_search.Text.Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                int teacherId;
                if (int.TryParse(searchTerm, out teacherId))
                {

                    searchByTeacherId(teacherId);
                }
            }
            else
            {
               
                LoadTeacherData();
            }
        }

        private void searchByTeacherId(int teacherId)
        {

            var result = dp.teachers

           .Where(t => t.teacherId == teacherId)

           .Select(t => new {

            ID = t.teacherId,

            U_ID = t.userId,

            Name = t.teacherName,

            Specialization = t.teacherSpecialization,

            Email = t.teacherEmail,

            Phone = t.teacherPhone,

           
           }).ToList();

            if (result.Any())
            {
                dataGridView1.DataSource = result;

                var teacher = result.FirstOrDefault();

              
                var columnHeaders = new Dictionary<string, string>
                {
                   { "teacherId", "ID" },

                   { "userId", "U_ID" },

                   { "teacherName", "Name" },

                   { "teacherSpecialization", "Specialization" },

                   { "teacherEmail", "Email" },

                   { "teacherPhone", "Phone" },


                };

                foreach (var column in columnHeaders)
                {
                    if (dataGridView1.Columns[column.Key] != null)
                    {
                        dataGridView1.Columns[column.Key].HeaderText = column.Value;
                    }
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells["ID"].Value != null && (int)row.Cells["ID"].Value == teacherId)
                    {
                        
                        this.dataGridView1.ColumnHeadersHeight = 40;

                        dataGridView1.AdvancedColumnHeadersBorderStyle.All = DataGridViewAdvancedCellBorderStyle.Single;

                        this.dataGridView1.GridColor = System.Drawing.Color.Black;

                        this.dataGridView1.RowTemplate.Height = 55;

                        dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

                        dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);

                        row.DefaultCellStyle.SelectionBackColor = Color.FromArgb(236, 236, 236);

                        row.DefaultCellStyle.SelectionForeColor = Color.Black;

                        dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(236, 236, 236);

                        dataGridView1.RowHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

                        row.DefaultCellStyle.Font = new Font("Arial", 11F, FontStyle.Regular);
                       
                    }
                }

                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    if (column != null)
                    {

                        column.DefaultCellStyle.SelectionBackColor = Color.FromArgb(236, 236, 236);

                        column.DefaultCellStyle.SelectionForeColor = Color.Black;

                        dataGridView1.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(236, 236, 236);

                        dataGridView1.RowHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

                        column.DefaultCellStyle.Font = new Font("Arial", 11F, FontStyle.Regular);

                        column.Width = column.HeaderText == "ID" ? 50 : column.HeaderText == "U_ID" ? 90 : ((dataGridView1.Width - dataGridView1.RowHeadersWidth - 40) / 3);

                        column.HeaderCell.Style.Alignment = column.HeaderText == "ID" ? DataGridViewContentAlignment.MiddleCenter : column.HeaderText == "U_ID" ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft;

                        column.DefaultCellStyle.Alignment = column.HeaderText == "ID" ? DataGridViewContentAlignment.MiddleCenter : column.HeaderText == "U_ID" ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft;

                    }
                }

            }
            else
            {
                MessageBox.Show("Teacher not found.");
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            deleteButton();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            resetForm();
            button1.Enabled = true;
        }

        private void deleteButton()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a teacher to delete.");
                    return;
                }

                var selectedRow = dataGridView1.SelectedRows[0];

                if (!int.TryParse(selectedRow.Cells[0].Value?.ToString(), out int teacherId))
                {
                    MessageBox.Show("Invalid Teacher ID.");
                    return;
                }

                var confirmation = MessageBox.Show(
                    "Are you sure you want to delete this teacher?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmation == DialogResult.No)
                {
                    return;
                }

                var teacher = dp.teachers.SingleOrDefault(t => t.teacherId == teacherId);
                if (teacher != null)
                {
                    dp.teachers.Remove(teacher);
                    dp.SaveChanges();

                    MessageBox.Show("Teacher deleted successfully.");

                    LoadTeacherData();
                }
                else
                {
                    MessageBox.Show("Teacher not found.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];

                string id = row.Cells[0].Value.ToString();

                string userID = row.Cells[1].Value.ToString();

                string name = row.Cells[2].Value.ToString();

                string specialization = row.Cells[3].Value.ToString();

                string email = row.Cells[4].Value.ToString();

                string phone = row.Cells[5].Value.ToString();

               

                textBox5.Text = userID;

                textBox1.Text = name;

                textBox3.Text = specialization;

                textBox2.Text = email;

                textBox4.Text = phone;

            }

        }
    }
}