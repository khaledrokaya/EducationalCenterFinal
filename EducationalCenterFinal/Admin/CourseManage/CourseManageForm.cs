using EducationalCenterFinal.Admin.CreateAccount;
using EducationalCenterFinal.Admin.EmployeeManage;
using EducationalCenterFinal.Admin.Staff;
using EducationalCenterFinal.Admin.Staff.StaffCoursesManage;
using EducationalCenterFinal.Admin.Staff.StudentManage;
using EducationalCenterFinal.Admin.TeacherManage;
using EducationalCenterFinal.SpecialForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace EducationalCenterFinal.Admin.CourseManage
{
    public partial class CourseManageForm : Form
    {
        readonly EducationCenterEntities dp = new EducationCenterEntities();

         cours c = new cours();
       
        
       
        public CourseManageForm()
        {
            InitializeComponent();
            //ربط الاحداث 
            this.txtSearch.Enter += new System.EventHandler(this.txtSearch_Enter);
            this.txtSearch.Leave += new System.EventHandler(this.txtSearch_Leave);
            //Maximize window
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.MaximizeBox = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

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
            //,تغيير لون الخلفية ,تغيير لون النص  
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(118, 41, 82);
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic Italic", 9, FontStyle.Bold);
            dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);

            //لتغيير اسماء الاعمدة عن الا موجودة في DataBase
            dataGridView1.DataSource = dp.courses.ToList();
            dataGridView1.Columns["courseId"].HeaderText = "ID";
            dataGridView1.Columns["courseName"].HeaderText = "Name";
            dataGridView1.Columns["Description"].HeaderText = "Description";
            dataGridView1.Columns["WorkOn"].HeaderText = "Day Of Week";
            dataGridView1.Columns["price"].HeaderText = "Price";
            dataGridView1.Columns["NoOfHours"].HeaderText = "Hour";
            dataGridView1.Columns["teacherId"].HeaderText = "Teacher ID";

            //لاخفاء اعمدة معينة
            dataGridView1.Columns["beginning"].Visible = false;
            dataGridView1.Columns["attendance"].Visible = false;
            dataGridView1.Columns["teacherId"].Visible = false;
            dataGridView1.Columns["enrollments"].Visible = false;
            dataGridView1.Columns["exams"].Visible = false;
            dataGridView1.Columns["payments"].Visible = false;
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

        private void TeachersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TeacherManageForm().Show();
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

        private void CourseManageForm_Load(object sender, EventArgs e)
        {
            
            txtSearch.Text = "Search";
            txtSearch.ForeColor = Color.Gray;
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "Search";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Search")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox8.Text == "" || textBox7.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox4.Text == "")
            {
                MessageBox.Show("Enter Data First.");
                return;
            }
            addData();
        }
        private void addData()
        {

            courses c_add = new courses();
            c_add.courseName = textBox2.Text;
            c_add.Description = textBox8.Text;
            c_add.WorkOn = textBox7.Text;
            c_add.NoOfHours = int.Parse(textBox6.Text);
            c_add.teacherId = int.Parse(textBox4.Text);
            //c_add.price = int.Parse(textBox5.text);
            dp.courses.Add(c_add);
            dp.SaveChanges();

            MessageBox.Show("Course Data Saved Successfully");
            dataGridView1.DataSource = dp.courses.ToList();


        }
        private void resetForm()
        {
            textBox2.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox4.Text = "";
            txtSearch.Text = "";
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "Search";
                txtSearch.ForeColor = Color.Gray;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSearch.Text, out int ID))
            {
                courses c = dp.courses.FirstOrDefault(x => x.courseId == ID);
                c.courseName = textBox2.Text;
                c.Description = textBox8.Text;
                c.WorkOn = textBox7.Text;
                //c.price= int.Parse(textBox5.Text);
                c.NoOfHours = int.Parse(textBox6.Text);
                c.teacherId = int.Parse(textBox4.Text);
                dp.SaveChanges();
                dataGridView1.DataSource = dp.courses.ToList();
                MessageBox.Show("Data Is Edited");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                courses c;
                if (int.TryParse(txtSearch.Text, out int ID))
                {
                    c = dp.courses.FirstOrDefault(x => x.courseId == ID);
                }
                else
                {
                    string name = txtSearch.Text.ToLower();
                    c = dp.courses.FirstOrDefault(x => x.courseName.ToLower().Contains(name));
                }
                if (c != null)
                {
                    textBox2.Text = c.courseName;
                    textBox8.Text = c.Description;
                    textBox7.Text = c.WorkOn;
                    //textBox5.Text = c.price.ToString();
                    textBox6.Text = c.NoOfHours.ToString();
                    textBox4.Text = c.teacherId.ToString();
                    button1.Enabled = false;
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSearch.Text, out int ID))
            {
                courses c = dp.courses.FirstOrDefault(x => x.courseId == ID);
                dp.courses.Remove(c);
                dp.SaveChanges();
                dataGridView1.DataSource = dp.courses.ToList();
                MessageBox.Show("Data Is Deleted");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            resetForm();
            button1.Enabled = true;
        }
    }
}

