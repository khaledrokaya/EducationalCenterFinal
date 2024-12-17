using System;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EducationalCenterFinal.Admin.Staff.StaffCoursesManage
{
    public partial class TeacherCourseAttendance : Form
    {
        public TeacherCourseAttendance(EducationCenterEntities dp, int CourseID)
        {
            InitializeComponent();
            SearchBoxPlaceHolder();
            SubmitButton.Text = "Mark";
            this.Text = SubmitButton.Text;
            SubmitButton.Click += (_sender, _e) => SubmitButton_Click(dp, CourseID);
        }

        private void SearchBoxPlaceHolder()
        {
            searchBox.Text = "Student ID...";
            searchBox.ForeColor = Color.Gray;
            searchBox.Enter += (sender, e) =>
            {
                if (searchBox.Text == "Student ID...")
                {
                    searchBox.Text = "";
                    searchBox.ForeColor = Color.Black;
                }
            };
            searchBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(searchBox.Text))
                {
                    searchBox.Text = "Student ID...";
                    searchBox.ForeColor = Color.Gray;
                }
            };
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SubmitButton_Click(EducationCenterEntities dp, int CourseID)
        {
            if (int.TryParse(searchBox.Text, out int studentId))
            {
                var isEnrolled = dp.enrollments.Any(e => e.studentId == studentId && e.courseId == CourseID);
                if (isEnrolled)
                {
                    var today = DateTime.Today;
                    var existingAttendance = dp.attendance
                        .FirstOrDefault(a => a.studentId == studentId && a.courseId == CourseID && DbFunctions.TruncateTime(a.attendanceDate) == today);

                    try
                    {
                        if (existingAttendance == null)
                        {
                            dp.attendance.Add(new attendance
                            {
                                studentId = studentId,
                                courseId = CourseID,
                                attendanceDate = DateTime.Now,
                                isPresent = true
                            });

                            dp.SaveChanges();
                            MessageBox.Show($"Attendance marked for Student {studentId} in Course {CourseID}.");
                        }
                        else
                        {
                            MessageBox.Show($"Attendance already marked for Student {studentId} today.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"Student {studentId} is not assigned to Course {CourseID}.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid Student ID.");
            }
        }
    }
}