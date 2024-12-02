using EducationalCenterFinal.Admin.CourseManage;
using EducationalCenterFinal.Admin.CreateAccount;
using EducationalCenterFinal.Admin.Dashboard;
using EducationalCenterFinal.Admin.EmployeeManage;
using EducationalCenterFinal.Admin.Staff;
using EducationalCenterFinal.Admin.Staff.StaffCoursesManage;
using EducationalCenterFinal.Admin.Staff.StudentManage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //   style DataGridView 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(118, 41, 82);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic Italic", 9, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);



            //ربط الاحداث 
            this.textBox1_search.Enter += new System.EventHandler(this.textBox1_search_Enter);
            this.textBox1_search.Leave += new System.EventHandler(this.textBox1_search_Leave);
            //Maximize window
            //this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            //this.MaximizeBox = false;
            //this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

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
            //PictureBox pictureBox = new PictureBox
            //{
            //    Image = Image.FromFile(Application.StartupPath.Substring(0, 42) + "\\Images\\search-interface-symbol.png"),
            //    SizeMode = PictureBoxSizeMode.Normal,
            //    Location = new Point(270, 13),
            //    Size = new Size(50, 50)
            //};
            //textBox1_search.Controls.Add(pictureBox);
            //,تغيير لون الخلفية ,تغيير لون النص  
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(118, 41, 82);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic Italic", 9, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);
            

            //لتغيير اسماء الاعمدة عن الا موجودة في DataBase
            dataGridView1.DataSource= dp.teachers.ToList();
            dataGridView1.Columns["teacherId"].HeaderText = "ID";
            dataGridView1.Columns["teacherName"].HeaderText = "Name";
            dataGridView1.Columns["teacherEmail"].HeaderText = "Email";
            dataGridView1.Columns["teacherSpecialization"].HeaderText = "Specialization";
            dataGridView1.Columns["teacherPhone"].HeaderText ="Phone";
            //لاخفاء اعمدة معينة
            dataGridView1.Columns["userId"].Visible = false;
            dataGridView1.Columns["courses"].Visible = false;
            dataGridView1.Columns["user"].Visible = false;


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
            textBox1_search.Size = new Size(200, 30);
            textBox1_search.Location = new Point(ClientSize.Width - 220, 40);
           
            // DataGridView 
            dataGridView1.Size = new Size(ClientSize.Width - 400, ClientSize.Height - 120);
            dataGridView1.Location = new Point(20, 80);

            // panel 
            panel1.Size = new Size(320, ClientSize.Height - 120);
            panel1.Location = new Point(ClientSize.Width - 340, 80);

            // Labels and TextBoxes 
            int yOffset = 30;
            int padding = 70;

            label2.Location = new Point(20, yOffset);
            textBox1.Location = new Point(20, yOffset + 25);
            textBox1.Size = new Size(280, 30);

            label3.Location = new Point(20, yOffset + padding);
            textBox2.Location = new Point(20, yOffset + padding + 25);
            textBox2.Size = new Size(280, 30);

            label4.Location = new Point(20, yOffset + padding * 2);
            textBox3.Location = new Point(20, yOffset + padding * 2 + 25);
            textBox3.Size = new Size(280, 30);

            label5.Location = new Point(20, yOffset + padding * 3);
            textBox4.Location = new Point(20, yOffset + padding * 3 + 25);
            textBox4.Size = new Size(280, 30);

            label6.Location = new Point(20, yOffset + padding * 4);
            textBox5.Location = new Point(20, yOffset + padding * 4 + 25);
            textBox5.Size = new Size(280, 30);

            // Buttons 
            int buttonY = yOffset + padding * 5;
            button1.Location = new Point(20, buttonY);

            button2.Location = new Point(170, buttonY);
            button3.Location = new Point(20, buttonY + 40);
            button4.Location = new Point(170, buttonY + 40);

            button1.Size = new Size(120, 30);
            button2.Size = new Size(120, 30);
            button3.Size = new Size(120, 30);
            button4.Size = new Size(120, 30);
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

        private void textBox1_search_GotFocus(object sender, EventArgs e)
        {
        }
        private void textBox1_search_LostFocus(object sender, EventArgs e)
        {
        }
        private void label5_Click(object sender, EventArgs e)
        {
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
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
           
            teacher t_add = new teacher();
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
            //textBox1_search.Text = "Search";
            //textBox1_search.ForeColor = Color.Gray;


            dataGridView1.DefaultCellStyle.ForeColor = Color.Black; // اللون الاسود للنص

            // تغيير نوع الخط وحجمه
            dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
           
            if (int.TryParse(textBox1_search.Text, out int ID))
            {
                teacher t= dp.teachers.FirstOrDefault(x => x.teacherId == ID);
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void textBox1_search_TextChanged(object sender, EventArgs e)
        {
           //Search With Name Or TeacherID
                if (!string.IsNullOrWhiteSpace(textBox1_search.Text))
                {
                    teacher t;

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
        private void textBox1_search_Enter(object sender, EventArgs e)
        {
        }
        private void textBox1_search_Leave(object sender, EventArgs e)
        {
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1_search.Text, out int ID))
            {
                teacher t = dp.teachers.FirstOrDefault(x => x.teacherId == ID);
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
        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }
    }
}
