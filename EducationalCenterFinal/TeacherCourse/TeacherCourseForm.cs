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
        public TeacherCourseForm(int TeacherId)
        {
            InitializeComponent();
            //!!!!!!!!! Important Dont Remove
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.MaximizeBox = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            //!!!!!!!!! Important Dont Remove
        }

        private void LogoutButton_Click(object sender, EventArgs e) //Remove
        {
            string quizName = txtQuizName.Text;

            foreach (DataGridViewRow row in dataGridViewStudents.Rows)
            {
                int studentId = (int)row.Cells["StudentID"].Value;
                int grade = Convert.ToInt32(row.Cells["QuizGrade"].Value);

                var student = course.GetStudentById(studentId);
                student?.RecordQuizGrade(quizName, grade);
            }
        }

        private void TeacherCourseForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            using (var context = new DataColumn())
            {
                var students = context.Students.Select(s => new
                {
                    s.ID,
                    s.Name,
                    s.Email,
                    s.Phone
                }).ToList();

                dataGridView1.DataSource = students;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("yyyy-MM-dd");

            foreach (DataGridViewRow row in dataGridViewStudents.Rows)
            {
                int studentId = (int)row.Cells["StudentID"].Value;
                bool isPresent = Convert.ToBoolean(row.Cells["Attendance"].Value);

                var student = course.GetStudentById(studentId);
                student?.AddAttendance(date, isPresent ? "Present" : "Absent");
            }
        }
    }
}
