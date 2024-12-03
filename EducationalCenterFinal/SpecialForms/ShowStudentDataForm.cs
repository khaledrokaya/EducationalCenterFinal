using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EducationalCenterFinal.SpecialForms
{
    public partial class ShowStudentDataForm : Form
    {
        public ShowStudentDataForm(students dp, int CourseId)
        {
            InitializeComponent();
            this.Load += (_sender, _e) => ShowStudentDataForm_Load(dp, CourseId);
            AttendMonthCombo.SelectedIndexChanged += (_sender, _e) => AttendMonthCombo_SelectedIndexChanged(dp, CourseId);
            AttendDayCombo.SelectedIndexChanged += (_sender, _e) => AttendDayCombo_SelectedIndexChanged(dp, CourseId);
            PayMonthCombo.SelectedIndexChanged += (_sender, _e) => PayMonthCombo_SelectedIndexChanged(dp, CourseId);
            QuizCombo.SelectedIndexChanged += (_sender, _e) => QuizCombo_SelectedIndexChanged(dp, CourseId);
            LoadForm(dp);
        }

        private void LoadForm(students dp)
        {
            string[] studentData =
            {
                $"{dp.studentId}", dp.studentName, dp.studentEmail, dp.studentPhone
            };
            string[] studentDataLabel =
            {
                "ID:", "Name:", "Email:", "Phone:"
            };
            var i = 0;
            var idx = 0;
            foreach (string data in studentData)
            {
                TextBox dataBox = new TextBox();
                dataBox.BackColor = System.Drawing.Color.White;
                dataBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
                dataBox.Font = new System.Drawing.Font("Arial", 12F);
                dataBox.Location = new System.Drawing.Point(5, 11);
                dataBox.Margin = new System.Windows.Forms.Padding(0);
                dataBox.Text = data;
                dataBox.Size = new System.Drawing.Size(383, 23);
                dataBox.Enabled = false;
                dataBox.ForeColor = Color.FromArgb(10, 10, 10);

                Panel panel1 = new Panel();
                panel1.BackColor = System.Drawing.Color.White;
                panel1.Controls.Add(dataBox);
                panel1.Location = new System.Drawing.Point(12, 40 + i);
                panel1.Margin = new System.Windows.Forms.Padding(0);
                panel1.Name = "panel1";
                panel1.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
                panel1.Size = new System.Drawing.Size(ClientSize.Width - 24, 40);

                Label label1 = new Label();
                label1.Text = studentDataLabel[idx++];
                label1.AutoSize = true;
                label1.ForeColor = System.Drawing.Color.White;
                label1.Font = new Font("Arial", 14, FontStyle.Bold);
                label1.Location = new Point(10, 12 + i);

                i += 80;
                Controls.Add(label1);
                Controls.Add(panel1);
            }
        }

        private void ShowStudentDataForm_Load(students stu, int CourseId)
        {
            this.AttendMonthCombo.DataSource = stu.attendance.Where(a => a.courseId == CourseId).Select(a => a.attendanceDate.Month).Distinct().ToList();
            this.PayMonthCombo.DataSource = stu.payments.Where(p => p.courseId == CourseId).Select(p => p.paymentDate.Month).ToList();
            this.QuizCombo.DataSource = stu.exams.Where(q => q.courseId == CourseId).Select(q => q.examName).ToList();
        }

        private void AttendMonthCombo_SelectedIndexChanged(students stu, int CourseId)
        {
            this.AttendDayCombo.DataSource = stu.attendance.Where(a => a.attendanceDate.Month == int.Parse(AttendMonthCombo.Text) && a.courseId == CourseId).Select(a => a.attendanceDate.Day).ToList();
        }

        private void AttendDayCombo_SelectedIndexChanged(students stu, int CourseId)
        {
            AttendanceLabel.Text = stu.attendance.Where(a => a.attendanceDate.Month == int.Parse(AttendMonthCombo.Text) && a.attendanceDate.Day == int.Parse(AttendDayCombo.Text) && a.courseId == CourseId).Select(a => a.isPresent).FirstOrDefault().ToString();
        }

        private void PayMonthCombo_SelectedIndexChanged(students stu, int CourseId)
        {
            PaymentLabel.Text = stu.payments.Where(p => p.paymentDate.Month == int.Parse(PayMonthCombo.Text) && p.courseId == CourseId).Select(p => p.isPaid).FirstOrDefault().ToString();
        }

        private void QuizCombo_SelectedIndexChanged(students stu, int CourseId)
        {
            QuizLabel.Text = stu.exams.Where(q => q.examName == QuizCombo.Text && q.courseId == CourseId).Select(q => q.score).FirstOrDefault().ToString();
        }
    }
}
