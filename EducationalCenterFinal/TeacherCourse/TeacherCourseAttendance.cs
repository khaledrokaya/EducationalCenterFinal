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
            button1.Click += (_sender, _e) => button1_Click(dp, CourseID);
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
                    var existingAttendance = dp.attendances
                        .FirstOrDefault(a => a.studentId == studentId && a.courseId == CourseID && DbFunctions.TruncateTime(a.attendanceDate) == today);

                    try
                    {
                        if (existingAttendance == null)
                        {
                            dp.attendances.Add(new attendance
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

        private void button1_Click(EducationCenterEntities dp, int CourseID)
        {
            var result = MessageBox.Show(
                "Are you sure you want to mark all remaining students as absent for today?",
                "Confirm Action",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            if (result == DialogResult.Yes)
            {
                try
                {
                    var today = DateTime.Today;
                    var enrolledStudents = dp.enrollments
                        .Where(e => e.courseId == CourseID)
                        .Select(e => e.studentId)
                        .ToList();
                    foreach (var studentId in enrolledStudents)
                    {
                        var existingAttendance = dp.attendances
                            .FirstOrDefault(a => a.studentId == studentId && a.courseId == CourseID && DbFunctions.TruncateTime(a.attendanceDate) == today);

                        if (existingAttendance == null)
                        {
                            dp.attendances.Add(new attendance
                            {
                                studentId = studentId,
                                courseId = CourseID,
                                attendanceDate = DateTime.Now,
                                isPresent = false
                            });
                        }
                    }
                    dp.SaveChanges();
                    MessageBox.Show("All absent students have been marked as not present for today.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while marking absent students: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Operation cancelled. No changes were made.");
            }
        }

    }
}