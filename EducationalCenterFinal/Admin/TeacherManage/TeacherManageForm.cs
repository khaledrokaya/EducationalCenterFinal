using EducationalCenterFinal.Admin.CourseManage;
using EducationalCenterFinal.Admin.CreateAccount;
using EducationalCenterFinal.Admin.Dashboard;
using EducationalCenterFinal.Admin.EmployeeManage;
using EducationalCenterFinal.Admin.Staff;
using EducationalCenterFinal.Admin.Staff.StaffCoursesManage;
using EducationalCenterFinal.Admin.Staff.StudentManage;
using EducationalCenterFinal.SpecialForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            setUpForm();
            setUpComponents();
            SearchPlaceHolder();
            styleDataGridView();

            this.questionsToolStripMenuItem.Click += (sender, e) => this.QuestionsToolStripMenuItem_Click("admin");
            this.dashboardToolStripMenuItem.Click += (sender, e) => this.DashboardToolStripMenuItem_Click("admin");
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
                Image = Image.FromFile(Application.StartupPath.Remove(Application.StartupPath.Length-10) + "\\Images\\search-interface-symbol.png"),
                SizeMode = PictureBoxSizeMode.Normal,
                Location = new Point(270, 13),
                Size = new Size(20, 20)
            };
            textBox1_search.Controls.Add(pictureBox);



        }
        private void styleDataGridView()
        {
            dataGridView1.DataSource = dp.teachers.ToList();

            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(118, 41, 82);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 13F, System.Drawing.FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);
            this.dataGridView1.ColumnHeadersHeight = 40;
            this.dataGridView1.GridColor = System.Drawing.Color.Black;
            this.dataGridView1.RowTemplate.Height = 55;
            //لتغيير اسماء الاعمدة عن الا موجودة في DataBase
            var columnHeaders = new Dictionary<string, string>
            {
              { "teacherId", "ID" },
              { "teacherName", "Name" },
              { "teacherEmail", "Email" },
              { "teacherSpecialization", "Specialization" },
              { "teacherPhone", "Phone" }
            };

            foreach (var column in columnHeaders)
            {
                if (dataGridView1.Columns[column.Key] != null)
                {
                    dataGridView1.Columns[column.Key].HeaderText = column.Value;
                }
            }

            string[] hiddenColumns = { "userId", "courses", "users" };

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
                    column.Width = column.HeaderText == "ID" ? 50 : ((dataGridView1.Width - dataGridView1.RowHeadersWidth - 40) / 3);
                    column.HeaderCell.Style.Alignment = column.HeaderText == "ID" ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft;
                    column.DefaultCellStyle.Alignment = column.HeaderText == "ID" ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft;
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
            textBox1_search.Size = new Size(((ClientSize.Width - 360) * 1 / 4), 50);
            textBox1_search.Location = new Point(ClientSize.Width - 320, 20);

            // DataGridView 
            dataGridView1.Size = new Size(((ClientSize.Width - 44) * 3 / 4), ClientSize.Height - 120);
            dataGridView1.Location = new Point(20, 80);

            // panel 
            panel1.Size = new Size(((ClientSize.Width - 250) * 1 / 4), ClientSize.Height - 120);
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

        private void DashboardToolStripMenuItem_Click(string role)
        {
            new DashboardForm(role).Show();
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
            textBox1_search.Text = "Search";
            textBox1_search.ForeColor = Color.Gray;
            textBox1_search.Enter += (sender, e) =>
            {
                if (textBox1_search.Text == "Search")
                {
                    textBox1_search.Text = "";
                    textBox1_search.ForeColor = Color.Black;
                }
            };
            textBox1_search.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(textBox1_search.Text))
                {
                    textBox1_search.Text = "Search";
                    textBox1_search.ForeColor = Color.Gray;
                }
            };
            textBox1_search.TextChanged += (sender, e) =>
            {
                if (textBox1_search.Text != "Search" && textBox1_search.ForeColor == Color.Gray)
                {
                    textBox1_search.ForeColor = Color.Black;
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if(textBox1.Text==""||textBox2.Text=="" || textBox3.Text == "" || textBox4.Text == "")
           {
                MessageBox.Show("Enter Data First.");
                return;
           }
           addData();
        }

        private void addData()
        {
           
            teachers t_add = new teachers();
            t_add.teacherName = textBox1.Text;
            t_add.teacherEmail = textBox2.Text;
            t_add.teacherSpecialization = textBox3.Text;
            t_add.teacherPhone = textBox4.Text;
            t_add.userId = int.Parse(textBox5.Text);

            dp.teachers.Add(t_add);
            dp.SaveChanges();

            MessageBox.Show("Teacher Saved Successfully");
            dataGridView1.DataSource = dp.teachers.ToList();
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
                textBox1_search.Text = "Search";
                textBox1_search.ForeColor = Color.Gray;
            }
        }

        private void TeacherManageForm_Load_1(object sender, EventArgs e)
        {
            textBox1_search.Text = "Search";
            textBox1_search.ForeColor = Color.Gray;

            dataGridView1.DefaultCellStyle.ForeColor = Color.Black; // اللون الاسود للنص

            // تغيير نوع الخط وحجمه
            dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1_search.Text, out int ID))
            {
                teachers t= dp.teachers.FirstOrDefault(x => x.teacherId == ID);
                t.teacherName = textBox1.Text;
                t.teacherEmail = textBox2.Text;
                t.teacherSpecialization = textBox3.Text;
                t.teacherPhone = textBox4.Text;
                t.userId = int.Parse(textBox5.Text);
                dp.SaveChanges();
                dataGridView1.DataSource = dp.teachers.ToList();
                MessageBox.Show("Data Is Edited");
            }

        }

        private void textBox1_search_TextChanged(object sender, EventArgs e)
        {
                //Search With Name Or TeacherID
                if (!string.IsNullOrWhiteSpace(textBox1_search.Text))
                {
                    teachers t;

                    if (int.TryParse(textBox1_search.Text, out int ID))
                    {
                        t = dp.teachers.FirstOrDefault(x => x.teacherId == ID);
                    }
                    else
                    {
                        string name = textBox1_search.Text.ToLower();
                        t = dp.teachers.FirstOrDefault(x => x.teacherName.ToLower().Contains(name));
                    }
                    if (t != null)
                    {
                        textBox1.Text = t.teacherName;
                        textBox2.Text = t.teacherEmail;
                        textBox3.Text = t.teacherSpecialization;
                        textBox4.Text = t.teacherPhone;
                        textBox5.Text = t.userId.ToString();
                        button1.Enabled = false;
                    }
                }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1_search.Text, out int ID))
            {
                teachers t = dp.teachers.FirstOrDefault(x => x.teacherId == ID);
                dp.teachers.Remove(t);
                dp.SaveChanges();
                dataGridView1.DataSource = dp.teachers.ToList();
                MessageBox.Show("Data Is Deleted");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            resetForm();
            button1.Enabled = true;
        }
    }
}
