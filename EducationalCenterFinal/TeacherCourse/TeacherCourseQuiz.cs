using System;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace EducationalCenterFinal.Admin.Staff.StaffCoursesManage
{
    public partial class TeacherCourseQuiz : Form
    {
        public TeacherCourseQuiz(EducationCenterEntities dp, int CourseID)
        {
            InitializeComponent();
            SearchBoxPlaceHolder();
            QuizPlaceHolder();
            ScorePlaceHolder();
            SubmitButton.Text = "Mark Quiz";
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
        
        private void QuizPlaceHolder()
        {
            quizNumberBox.Text = "Quiz Number...";
            quizNumberBox.ForeColor = Color.Gray;
            quizNumberBox.Enter += (sender, e) =>
            {
                if (quizNumberBox.Text == "Quiz Number...")
                {
                    quizNumberBox.Text = "";
                    quizNumberBox.ForeColor = Color.Black;
                }
            };
            quizNumberBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(quizNumberBox.Text))
                {
                    quizNumberBox.Text = "Quiz Number...";
                    quizNumberBox.ForeColor = Color.Gray;
                }
            };
        }
        
        private void ScorePlaceHolder()
        {
            scoreBox.Text = "Quiz Degree...";
            scoreBox.ForeColor = Color.Gray;
            scoreBox.Enter += (sender, e) =>
            {
                if (scoreBox.Text == "Quiz Degree...")
                {
                    scoreBox.Text = "";
                    scoreBox.ForeColor = Color.Black;
                }
            };
            scoreBox.Leave += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(scoreBox.Text))
                {
                    scoreBox.Text = "Quiz Degree...";
                    scoreBox.ForeColor = Color.Gray;
                }
            };
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SubmitButton_Click(EducationCenterEntities dp, int CourseID)
        {
            if (int.TryParse(searchBox.Text, out int studentId) &&
                int.TryParse(quizNumberBox.Text, out int quizNumber) && 
                decimal.TryParse(scoreBox.Text, out decimal score)) 
            {
                var isEnrolled = dp.enrollments.Any(e => e.studentId == studentId && e.courseId == CourseID);
                if (isEnrolled)
                {
                    var today = DateTime.Today;
                    var existingQuiz = dp.exams
                        .FirstOrDefault(q => q.studentId == studentId && q.courseId == CourseID && q.examName == quizNumber.ToString() && DbFunctions.TruncateTime(q.examDate) == today);

                    try
                    {
                        if (existingQuiz == null)
                        {
                            dp.exams.Add(new exams
                            {
                                studentId = studentId,
                                courseId = CourseID,
                                examName = quizNumber.ToString(),
                                examDate = DateTime.Now,
                                score = score
                            });

                            dp.SaveChanges();
                            MessageBox.Show($"Quiz {quizNumber} marked for Student {studentId} in Course {CourseID}. Score: {score}");
                        }
                        else
                        {
                            MessageBox.Show($"Quiz {quizNumber} already marked for Student {studentId} today.");
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
                MessageBox.Show("Please enter a valid Student ID, Quiz Number, and Score.");
            }
        }

        private void button1_Click(EducationCenterEntities dp, int CourseID)
        {
            var result = MessageBox.Show(
                "Are you sure you want to mark all students for the quiz today?",
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
                        if (int.TryParse(quizNumberBox.Text, out int quizNumber) && decimal.TryParse(scoreBox.Text, out decimal score))
                        {
                            var existingQuiz = dp.exams
                                .FirstOrDefault(q => q.studentId == studentId && q.courseId == CourseID && q.examName == quizNumber.ToString() && DbFunctions.TruncateTime(q.examDate) == today);

                            if (existingQuiz == null)
                            {
                                dp.exams.Add(new exams
                                {
                                    studentId = studentId,
                                    courseId = CourseID,
                                    examName = quizNumber.ToString(),
                                    examDate = DateTime.Now,
                                    score = score
                                });
                            }
                        }
                    }
                    dp.SaveChanges();
                    MessageBox.Show("All students have been marked for the quiz today.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while marking quizzes: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Operation cancelled. No changes were made.");
            }
        }
    }
}
