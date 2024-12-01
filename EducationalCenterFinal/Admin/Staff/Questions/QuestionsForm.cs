using EducationalCenterFinal;
using EducationalCenterFinal.Admin.CourseManage;
using EducationalCenterFinal.Admin.CreateAccount;
using EducationalCenterFinal.Admin.Dashboard;
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

namespace EducationalCenterFinal.Admin.Staff
{
    public partial class QuestionsForm : Form
    {
        private readonly EducationCenterEntities dp = new EducationCenterEntities();
        private List<question> _questions;

        public QuestionsForm(string role)
        {
            InitializeComponent();

            this.comboBox1.SelectedIndex = 0;
            LoadUnansweredQuestions(comboBox1.SelectedIndex);

            this.dashboardToolStripMenuItem.Click += (sender, e) => this.DashboardToolStripMenuItem_Click(role);
            this.studentsToolStripMenuItem.Click += (sender, e) => this.StudentsToolStripMenuItem_Click(role);

            //Disable Admin Sections
            if (role == "staff")
            {
                teachersToolStripMenuItem.Enabled = false;
                coursesToolStripMenuItem.Enabled = false;
                forgetPasswordToolStripMenuItem.Enabled = false;
                createAccountToolStripMenuItem.Enabled = false;
                employeesToolStripMenuItem.Enabled = false;
                dashboardToolStripMenuItem.Enabled = false;
            }

            //Make Manage Course MenuItems
            foreach (var course in dp.courses)
            {
                ToolStripMenuItem courseMenuItem = new ToolStripMenuItem
                {
                    Name = $"{course.courseName}ToolStripMenuItem",
                    Size = new System.Drawing.Size(134, 26),
                    Text = course.courseName,
                };
                courseMenuItem.Click += (sender, e) => CourseMenuItem_Click(course.courseId, role);
                manageToolStripMenuItem.DropDownItems.Add(courseMenuItem);
            }

            CompontntStyling();
        }

        private void CompontntStyling()
        {
            this.ClientSize = new System.Drawing.Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            richTextBox1.Size = new Size((ClientSize.Width - 44 )/ 2, ClientSize.Height - 22);
            richTextBox1.Location = new Point(12, 20);
            SelectedQuestionLbl.Size = new Size(richTextBox1.Width - 40, richTextBox1.Height/3);
            SelectedQuestionLbl.Location = new Point(12, 75);
            AnswerTxtBox.Size = new Size(richTextBox1.Width - 40, richTextBox1.Height / 3);
            AnswerTxtBox.Location = new Point(12, SelectedQuestionLbl.Height + 130);
            QuestionsLstBox.Size = new Size((ClientSize.Width - 44) / 2, richTextBox1.Height - 120);
            QuestionsLstBox.Location = new Point(richTextBox1.Width+20, 70);
            SubmitAnswerBtn.Location = new Point((richTextBox1.Width-250)/2, (AnswerTxtBox.Height+SelectedQuestionLbl.Height + 150));
            panel1.Width = QuestionsLstBox.Width / 2;
            panel1.Location = new Point(richTextBox1.Width+200, 30);
            label3.Location = new Point(12,SelectedQuestionLbl.Height+85);
            comboBox1.DrawItem += ComboBox1_DrawItem;
        }

        private void ComboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
                return;
            e.DrawBackground();
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(118,41,84)), e.Bounds);
            e.Graphics.DrawString(comboBox1.Items[e.Index].ToString(), e.Font, Brushes.White, e.Bounds);
            e.DrawFocusRectangle();
        }

        private void LoadUnansweredQuestions(int index)
        {
            try
            {
                if (index == 0)
                {
                    _questions = dp.questions
                                       .Where(x => x.isAnswered == false)
                                       .ToList();
                }
                else
                {
                    _questions = dp.questions
                                   .Where(x => x.isAnswered == true)
                                   .ToList();
                }
                QuestionsLstBox.DataSource = _questions;
                QuestionsLstBox.DisplayMember = "questionContent";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading questions: " + ex.Message);
            }
        }

        private void CourseMenuItem_Click(int CourseId, string role)
        {
            new StaffCourseForm(CourseId, role).Show();
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

        private void EmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new EmployeeManageForm().Show();
            this.Hide();
        }

        private void StudentsToolStripMenuItem_Click(string role)
        {
            new StudentManageForm(role).Show();
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

        private void QuestionsLstBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (QuestionsLstBox.SelectedItem is question selectedQuestion)
            {
                SelectedQuestionLbl.Text = selectedQuestion.questionContent;
                AnswerTxtBox.Text = selectedQuestion.questionAnswer;
            }
            else
            {
                SelectedQuestionLbl.Text = string.Empty;
            }
        }

        private void SubmitAnswerBtn_Click(object sender, EventArgs e)
        {
            Color.FromArgb(118, 41, 82);

            if (QuestionsLstBox.SelectedItem is question selectedQuestion)
            {
                string answer = AnswerTxtBox.Text.Trim();
                if (string.IsNullOrEmpty(answer))
                {
                    MessageBox.Show("Please enter an answer.");
                    return;
                }

                try
                {
                    selectedQuestion.questionAnswer = answer;
                    selectedQuestion.isAnswered = true;
                    dp.SaveChanges();
                    MessageBox.Show("Answer submitted successfully.");
                    AnswerTxtBox.Clear();
                    LoadUnansweredQuestions(comboBox1.SelectedIndex);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error submitting answer: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a question to answer.");
            }
        }

        private void QuestionsForm_Load(object sender, EventArgs e)
        {
            LoadUnansweredQuestions(comboBox1.SelectedIndex);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUnansweredQuestions(comboBox1.SelectedIndex);
        }
    }
}
