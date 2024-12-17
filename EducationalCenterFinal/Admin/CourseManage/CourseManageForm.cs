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
using System.Drawing.Drawing2D;
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
        public CourseManageForm()
        {
            InitializeComponent();
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
            txtSearch.Controls.Add(pictureBox);
        }

        private void styleDataGridView()
        {
            dataGridView1.DataSource = dp.courses.ToList();


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

            this.dataGridView1.RowTemplate.Height = 40;

            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

            dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);

            //لتغيير اسماء الاعمدة عن الا موجودة في DataBase
            var columnHeaders = new Dictionary<string, string>
                {
                    { "courseId", "ID" },

                    { "courseName", "Name" },

                    { "Description", "Description" },

                    { "WorkOn", "DayOfWeek" },

                    { "beginning", "Beginning" },

                    { "NoOfHours", "Hour" },

                     {"teacherId","T_ID" },

                    { "price", "Price" },                 

                   


                };

            foreach (var column in columnHeaders)
            {
                if (dataGridView1.Columns[column.Key] != null)
                {
                    dataGridView1.Columns[column.Key].HeaderText = column.Value;
                }
            }

            //لاخفاء اعمدة معينة

            string[] hiddenColumns = { "attendance", "enrollments", "exams", "payments", "teachers" };

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

                    column.Width = column.HeaderText == "ID" ? 50 : column.HeaderText=="T_ID"? 70: ((dataGridView1.Width - dataGridView1.RowHeadersWidth - 40) / 3);

                    column.HeaderCell.Style.Alignment = column.HeaderText == "ID" ? DataGridViewContentAlignment.MiddleCenter : column.HeaderText == "T_ID" ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft;

                    column.DefaultCellStyle.Alignment = column.HeaderText == "ID" ? DataGridViewContentAlignment.MiddleCenter : column.HeaderText == "T_ID" ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft;

                }
            }
        }
        private void LoadCourseData(int? filterId = null)
        {
            var courses = dp.courses.Select(c => new {

                ID = c.courseId,

                Name =c.courseName,

                Description = c.Description,

                DayOfWeek=c.WorkOn,

                Beginning=c.beginning,

                Hour = c.NoOfHours,

                T_ID = c.teacherId,

                Price = c.price
            });
            if (filterId.HasValue)
            {
                courses = courses.Where(c => c.ID == filterId.Value);
            }

            dataGridView1.DataSource = courses.ToList();

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
            txtSearch.Size = new Size(((ClientSize.Width - 360) * 1 / 4), 50);
            txtSearch.Location = new Point(ClientSize.Width - 320, 20);

            // DataGridView 
            dataGridView1.Size = new Size(((ClientSize.Width - 44) * 3 / 4), ClientSize.Height - 150);
            dataGridView1.Location = new Point(20, 80);

            // panel 
            panel1.Size = new Size(((ClientSize.Width - 250) * 1 / 4), ClientSize.Height - 150);
            panel1.Location = new Point(ClientSize.Width - 340, 80);

            // Labels and TextBoxes 
            label1.Size = new Size(((ClientSize.Width - 300) * 1 / 4), ClientSize.Height - 600);
            label1.Location = new Point(20, 25);

            label2.Location = new Point(25, 20);
            label2.Size = new Size(panel1.Width - 120, panel1.Height / 10);
            textBox2.Location = new Point(25, label2.Height + 33);
            textBox2.Size = new Size(panel1.Width - 55, panel1.Height / 20);

            label7.Location = new Point(25, 100);
            label7.Size = new Size(panel1.Width - 120, panel1.Height / 10);
            textBox8.Location = new Point(25, 133);
            textBox8.Size = new Size(panel1.Width - 55, panel1.Height / 20);
            
            label6.Location = new Point(25, 180);
            label6.Size = new Size(panel1.Width - 120, panel1.Height / 10);
            textBox7.Location = new Point(25, 213);
            textBox7.Size = new Size(panel1.Width - 55, panel1.Height / 20);
            
            label5.Location = new Point(25, 260);
            label5.Size = new Size(panel1.Width - 120, panel1.Height / 10);
            textBox5.Location = new Point(25, 293);
            textBox5.Size = new Size(panel1.Width - 55, panel1.Height / 20);
            
            label4.Location = new Point(25, 340);
            label4.Size = new Size(panel1.Width - 120, panel1.Height / 10);
            textBox6.Location = new Point(25, 373);
            textBox6.Size = new Size(panel1.Width - 55, panel1.Height / 20);
            
            label3.Location = new Point(25, 420);
            label3.Size = new Size(panel1.Width - 120, panel1.Height / 10);
            textBox4.Location = new Point(25, 453);
            textBox4.Size = new Size(panel1.Width - 55, panel1.Height / 20);
            
            label8.Location = new Point(25,500);
            label8.Size = new Size(panel1.Width - 120, panel1.Height / 10);
            textBox1.Location = new Point(25, 533);                    
            textBox1.Size = new Size(panel1.Width - 55, panel1.Height /20);
            
            // Buttons 
            button1.Location = new Point(25, 580);
            button2.Location = new Point(170, 580);
            button4.Location = new Point(25, 650);
            button3.Location = new Point(170, 650);

            button1.Size = new Size(panel1.Width - 210, panel1.Height / 14);
            button2.Size = new Size(panel1.Width - 210, panel1.Height / 14);
            button4.Size = new Size(panel1.Width - 210, panel1.Height / 14);
            button3.Size = new Size(panel1.Width - 210, panel1.Height / 14);

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

        private void SearchPlaceHolder()
        {
            txtSearch.Text = "Search By ID...";

            txtSearch.ForeColor = Color.Gray;

            txtSearch.Enter += (sender, e) =>
            {
                if (txtSearch.Text == "Search By ID...")
                {
                   txtSearch.Text = "";

                   txtSearch.ForeColor = Color.Black;
                }
            };
            txtSearch.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    txtSearch.Text = "Search By ID...";

                    txtSearch.ForeColor = Color.Gray;
                }
            };
            txtSearch.TextChanged += (sender, e) =>
            {
                if (txtSearch.Text != "Search By ID..." && txtSearch.ForeColor == Color.Gray)
                {
                    txtSearch.ForeColor = Color.Black;
                }
            };
        }
        private void CourseManageForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = label1;

            SearchPlaceHolder();
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "" || textBox8.Text == "" || textBox7.Text == "" || textBox5.Text == "" || textBox6.Text == ""||textBox4.Text==""||textBox1.Text=="")
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
                courses c_add = new courses();

                if (string.IsNullOrWhiteSpace(textBox4.Text))
                {
                    c_add.teacherId = null;
                }
                else if (int.TryParse(textBox4.Text, out int teacherId))
                {
                    c_add.teacherId = teacherId;
                }
                else
                {
                    MessageBox.Show("Teacher ID must be a valid number or left empty.");
                    return;
                }

                if (!string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    c_add.courseName = textBox2.Text;
                }
                else
                {
                    MessageBox.Show("Course name cannot be empty.");
                    return;
                }

                c_add.Description = textBox8.Text;

                string[] validDays = { "fri", "sat", "sun", "mon", "tus", "wed", "thu" };
                if (!string.IsNullOrWhiteSpace(textBox7.Text) &&
                    validDays.Contains(textBox7.Text.Trim().ToLower()))
                {
                    c_add.WorkOn = textBox7.Text.Trim().ToLower();
                }
                else
                {
                    MessageBox.Show("Invalid WorkOn value. It must be one of: fri, sat, sun, mon, tus, wed, thu.");
                    return;
                }

                if (decimal.TryParse(textBox5.Text, out decimal price))
                {
                    c_add.price = price;
                }
                else
                {
                    MessageBox.Show("Please enter a valid price.");
                    return;
                }

                if (decimal.TryParse(textBox6.Text, out decimal noOfHours))
                {
                    c_add.NoOfHours = noOfHours;
                }
                else
                {
                    MessageBox.Show("Please enter a valid number of hours.");
                    return;
                }

                if (TimeSpan.TryParse(textBox1.Text, out TimeSpan beginningTime))
                {
                    if (beginningTime >= TimeSpan.FromHours(8) && beginningTime <= TimeSpan.FromHours(22))
                    {
                        c_add.beginning = beginningTime;
                    }
                    else
                    {
                        MessageBox.Show("Beginning time must be between 08:00:00 and 22:00:00.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid format: Beginning time must be a valid time between 08:00:00 and 22:00:00.");
                    return;
                }

                dp.courses.Add(c_add);
                dp.SaveChanges();

                MessageBox.Show("Course Data Saved Successfully");

                LoadCourseData();
                dataGridView1.DataSource = dp.courses.ToList();
                resetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }

        private void resetForm()
        {
            textBox2.Text = "";

            textBox8.Text = "";

            textBox7.Text = "";

            textBox5.Text = "";

            textBox6.Text = "";

            textBox4.Text = "";

            textBox1.Text = "";

            txtSearch.Text = "";
            if (txtSearch.Text == "")
            {
                txtSearch.Text = "Search By ID...";

                txtSearch.ForeColor = Color.Gray;
            }
            button1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0 &&
                    int.TryParse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), out int courseId))
                {
                    var courseToEdit = dp.courses.FirstOrDefault(m => m.courseId == courseId);

                    if (courseToEdit != null)
                    {
                        if (string.IsNullOrWhiteSpace(textBox4.Text))
                        {
                            courseToEdit.teacherId = null;
                        }
                        else if (int.TryParse(textBox4.Text, out int teacherId))
                        {
                            courseToEdit.teacherId = teacherId;
                        }
                        else
                        {
                            MessageBox.Show("Teacher ID must be a valid number or left empty.");
                            return;
                        }

                        if (!string.IsNullOrWhiteSpace(textBox2.Text))
                        {
                            courseToEdit.courseName = textBox2.Text;
                        }
                        else
                        {
                            MessageBox.Show("Course name cannot be empty.");
                            return;
                        }

                        courseToEdit.Description = textBox8.Text;

                        string[] validDays = { "fri", "sat", "sun", "mon", "tus", "wed", "thu" };
                        if (!string.IsNullOrWhiteSpace(textBox7.Text) &&
                            validDays.Contains(textBox7.Text.Trim().ToLower()))
                        {
                            courseToEdit.WorkOn = textBox7.Text.Trim().ToLower();
                        }
                        else
                        {
                            MessageBox.Show("Invalid WorkOn value. It must be one of: fri, sat, sun, mon, tus, wed, thu.");
                            return;
                        }

                        if (TimeSpan.TryParse(textBox1.Text, out TimeSpan beginningTime))
                        {
                            if (beginningTime >= TimeSpan.FromHours(8) && beginningTime <= TimeSpan.FromHours(22))
                            {
                                courseToEdit.beginning = beginningTime;
                            }
                            else
                            {
                                MessageBox.Show("Beginning time must be between 08:00:00 and 22:00:00.");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid format for beginning time.");
                            return;
                        }

                        if (decimal.TryParse(textBox5.Text, out decimal price))
                        {
                            courseToEdit.price = price;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid price.");
                            return;
                        }

                        if (decimal.TryParse(textBox6.Text, out decimal noOfHours))
                        {
                            courseToEdit.NoOfHours = noOfHours;
                        }
                        else
                        {
                            MessageBox.Show("Please enter a valid number of hours.");
                            return;
                        }

                        dp.SaveChanges();

                        MessageBox.Show("Course Data Edited Successfully");

                        LoadCourseData();
                        dataGridView1.DataSource = dp.courses.ToList();
                        resetForm();
                    }
                    else
                    {
                        MessageBox.Show("Course not found.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a valid course to edit.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Search With courseId
            string searchTerm = txtSearch.Text.Trim();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                int courseId;
                if (int.TryParse(searchTerm, out courseId))
                {

                    searchByCourseId(courseId);
                }
            }
            else
            {
                // لو البحث فارغ، يتم تحميل كل البيانات
                LoadCourseData();
            }
        }

        private void searchByCourseId(int courseId)
        {

            var result = dp.courses

           .Where(c => c.courseId == courseId)
           
           .Select(c => new
           {
           
               ID = c.courseId,
               
               Name = c.courseName,
           
               Description = c.Description,
           
               DayOfWeek = c.WorkOn,

               Beginning=c.beginning,

               Hour = c.NoOfHours,

               T_ID = c.teacherId,

               Price = c.price,
           
              
               


           })
           .ToList();
            // عرض البيانات في DataGridView
            if (result.Any())
            {
                dataGridView1.DataSource = result;

                var course = result.FirstOrDefault();
                
                var columnHeaders = new Dictionary<string, string>
                {

                    { "courseId", "ID" },

                    { "courseName", "Name" },
                    
                    { "Description", "Description" },
                    
                    { "WorkOn", "DayOfWeek" },
                    
                    { "beginning", "Beginning" },
                    
                    { "NoOfHours", "Hour" },
                    
                     {"teacherId","T_ID" },
                    
                    { "price", "Price" },

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
                    if (row.Cells["ID"].Value != null && (int)row.Cells["ID"].Value == courseId)
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

                        column.Width = column.HeaderText == "ID" ? 50 : column.HeaderText=="T_ID"? 70: ((dataGridView1.Width - dataGridView1.RowHeadersWidth - 40) / 3);

                        column.HeaderCell.Style.Alignment = column.HeaderText == "ID" ? DataGridViewContentAlignment.MiddleCenter : column.HeaderText == "T_ID" ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft;

                        column.DefaultCellStyle.Alignment = column.HeaderText == "ID" ? DataGridViewContentAlignment.MiddleCenter : column.HeaderText == "T_ID" ? DataGridViewContentAlignment.MiddleCenter : DataGridViewContentAlignment.MiddleLeft;
                    }                                                                                                                                     
                }

            }
            else
            {
                MessageBox.Show("Course not found.");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            deleteButton();

            resetForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            resetForm();
            button1.Enabled = true;
        }

        private void deleteButton()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var selectedRow = dataGridView1.SelectedRows[0];

                    if (int.TryParse(selectedRow.Cells[0].Value.ToString(), out int courseId))
                    {
                        var course = dp.courses.SingleOrDefault(c => c.courseId == courseId);

                        if (course != null)
                        {
                            var confirmResult = MessageBox.Show(
                                "Are you sure you want to delete this course?",
                                "Confirm Deletion",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning);

                            if (confirmResult == DialogResult.Yes)
                            {
                                dp.courses.Remove(course);
                                dp.SaveChanges();

                                MessageBox.Show("Course deleted successfully.");

                                LoadCourseData();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Course not found.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid course ID.");
                    }
                }
                else
                {
                    MessageBox.Show("Please select one row to delete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex >= 0)
            {
                var row = dataGridView1.Rows[e.RowIndex];

                
                string courseid = row.Cells[0].Value.ToString();

                string name = row.Cells[1].Value.ToString();

                string description = row.Cells[2].Value.ToString();

                string dayofweek = row.Cells[3].Value.ToString();

                string begining = row.Cells[4].Value.ToString();

                string price = row.Cells[7].Value.ToString();

                string hour = row.Cells[5].Value.ToString();

                string teacherid = row.Cells[6].Value?.ToString();

                textBox4.Text = teacherid;

                textBox2.Text = name;

                textBox8.Text = description;

                textBox7.Text = dayofweek;

                textBox5.Text = price;

                textBox6.Text = hour;

                textBox1.Text = begining;

            }

        }
    }
}

