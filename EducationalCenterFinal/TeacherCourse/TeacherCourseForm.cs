using EducationalCenterFinal.Admin.Staff.StaffCoursesManage;
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

namespace EducationalCenterFinal.TeacherCourse
{
    public partial class TeacherCourseForm : Form
    {
        private teachers Teacher { get; set; }
        readonly private EducationCenterEntities dp = new EducationCenterEntities();
        public TeacherCourseForm(int TeacherId)
        {
            InitializeComponent();
            this.Teacher = dp.teachers.Where(t => t.teacherId == TeacherId).FirstOrDefault();
            var firstCourse = Teacher.courses.FirstOrDefault();
            if (firstCourse == null)
            {
                MessageBox.Show("The teacher is not assigned to any courses.", "No Courses", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            TeacherCourseForm_Load();
            LoadStudentData(firstCourse);
            ApplyStyling();
        }

        private void TeacherCourseForm_Load()
        {
            label1.Text = $"Teacher: {Teacher.teacherName}";
            label2.Text = $"Course: {Teacher.courses.FirstOrDefault().courseName}";
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height - 70);
            this.MinimumSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.WorkingArea.Height- 70);
            this.dataGridView1.RowHeaderMouseDoubleClick += (sender, e) => DataGridView1_CellContentClick(Teacher, (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            LogoutButton.Location = new System.Drawing.Point(this.ClientSize.Width - 12 - LogoutButton.Width , 30);
            quizButton.Location = new System.Drawing.Point((this.ClientSize.Width - quizButton.Width + button1.Width + 20) / 2, 30);
            button1.Location = new System.Drawing.Point((this.ClientSize.Width - button1.Width - quizButton.Width - 20) / 2, 30);
        }

        private void LoadStudentData(courses firstCourse, int? filterId = null)
        {
            try
            {
                var studentsInCourse = dp.students
                    .Join(dp.enrollments, student => student.studentId, enrollment => enrollment.studentId,
                        (student, enrollment) => new { student, enrollment })
                    .Where(result => result.enrollment.courseId == firstCourse.courseId)
                    .Select(result => new
                    {
                        ID = result.student.studentId,
                        Name = result.student.studentName,
                        Phone = result.student.studentPhone,
                        Address = result.student.studentAddress,
                        Email = result.student.studentEmail,
                    }).ToList();

                dataGridView1.DataSource = studentsInCourse;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while loading student data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyStyling()
        {
            dataGridView1.Size = new Size(ClientSize.Width - 24, ClientSize.Height - 120);
            dataGridView1.MinimumSize = new Size(ClientSize.Width - 24, ClientSize.Height - 120);
            dataGridView1.MaximumSize = new Size(ClientSize.Width - 24, ClientSize.Height - 120);
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
        }

        private void DataGridView1_CellContentClick(teachers Teacher, int stu)
        {
            new ShowStudentDataForm(dp.students.Where(s => s.studentId == stu).FirstOrDefault(), Teacher.courses.FirstOrDefault().courseId).Show();
        }

        private void QuizButton_Click(object sender, EventArgs e)
        {
            new TeacherCourseQuiz(dp, Teacher.courses.FirstOrDefault().courseId).Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new TeacherCourseAttendance(dp, Teacher.courses.FirstOrDefault().courseId).Show();
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            new LoginForm().Show();
            this.Hide();
        }
    }
}