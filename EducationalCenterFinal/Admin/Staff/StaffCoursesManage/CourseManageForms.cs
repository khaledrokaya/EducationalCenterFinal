using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EducationalCenterFinal.Admin.Staff.StaffCoursesManage
{
    public partial class CourseManageForms : Form
    {
        public CourseManageForms(EducationCenterEntities dp, int CourseID, string Type)
        {
            InitializeComponent();
            SearchBoxPlaceHolder();
            SubmitButton.Text = Type == "assign"? "Assign" : Type == "pay" ? "Pay" : "Remove";
            this.Text = SubmitButton.Text;
            SubmitButton.Click += (_sender, _e) => SubmitButton_Click(dp, CourseID, Type);
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

        private void SubmitButton_Click(EducationCenterEntities dp, int CourseID, string Type)
        {
            if (int.TryParse(searchBox.Text, out int studentId))
            {
                student stu = dp.students.Where(s => s.studentId == studentId).FirstOrDefault();
                enrollment enr = dp.enrollments.Where(e => e.studentId == studentId && e.courseId == CourseID).FirstOrDefault();
                payment pay = dp.payments.Where(p => p.studentId == studentId && p.paymentDate.Month == DateTime.Now.Month && p.courseId == CourseID).FirstOrDefault();
                if ((Type == "assign" && stu != null) || (Type == "remove" && enr != null) || (Type == "pay"))
                {
                    try
                    {
                        if (Type == "assign")
                        {
                            if (enr == null)
                            {
                                dp.enrollments.Add(
                                    new enrollment
                                    {
                                        courseId = CourseID,
                                        studentId = int.Parse(searchBox.Text)
                                    }
                                );
                                MessageBox.Show($"Student {int.Parse(searchBox.Text)} Has Been Assigned To Course {CourseID}.");
                                dp.SaveChanges();
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show($"Student {searchBox.Text} Is Already Assigned To The Course!");
                            }
                        }
                        else if (Type == "remove")
                        {
                            dp.enrollments.Remove(enr);
                            MessageBox.Show($"Student {int.Parse(searchBox.Text)} Has Been Deleted From Course {CourseID}.");
                            dp.SaveChanges();
                            this.Close();
                        }
                        else
                        {
                            if (enr != null)
                            {
                                if (pay == null)
                                {
                                    dp.payments.Add(new payment
                                    {
                                        courseId = CourseID,
                                        studentId = int.Parse(searchBox.Text),
                                        isPaid = true,
                                        paymentDate = DateTime.Now,
                                    }
                                    );
                                    MessageBox.Show($"The Student {int.Parse(searchBox.Text)} Has Paid The Course Fees Successfully.");
                                    dp.SaveChanges();
                                    this.Close();
                                }
                                else
                                {
                                    MessageBox.Show("The Student Has Already Paid The Course Fees.");
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Student {int.Parse(searchBox.Text)} Not Assigned To This Course!");
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Something Went Wrong While ِAssigning.");
                    }
                }
                else
                {
                    MessageBox.Show("Student ID Not Found!");
                }
            }
        }
    }
}
